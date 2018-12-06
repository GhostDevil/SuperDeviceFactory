using SuperFramework.SuperConvert;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SuperDeviceFactory.InfraredAlarm
{
    /// <summary>
    /// 说明：DS7400X红外对照报警
    /// 时间：2017-08-18
    /// 作者：痞子少爷
    /// </summary>
    public class DS7400X1Alarm
    {
        #region 事件委托
        /// <summary>
        /// 日志委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void DSRevMessage(string msg);
        /// <summary>
        /// 日志事件
        /// </summary>
        public event DSRevMessage DSMessage;
        /// <summary>
        /// 防区信息
        /// </summary>
        /// <param name="data">事件信息</param>
        public delegate void DSZoomMassage(DSEventData data);
        /// <summary>
        /// 防区事件
        /// </summary>
        public event DSZoomMassage DSZoomEvent;
        ///// <summary>
        ///// 系统状态事件委托
        ///// </summary>
        ///// <param name="time">状态时间</param>
        ///// <param name="deviceIp">设备ip</param>
        ///// <param name="sysStateCode">系统状态码,按位表示，0-正常 1-故障，类型从枚举SysStatus获取</param>
        ///// <param name="stateStr">状态字符串</param>
        //public delegate void DSSysRunemtatus(DateTime time, string deviceIp, string sysStateCode, string stateStr);
        /// <summary>
        /// 系统状态事件委托
        /// </summary>
        /// <param name="status">状态字符串</param>
        /// <param name="statusCode">系统状态码,按位表示，0-正常 1-故障，类型从枚举SysStatus获取</param>
        public delegate void DSSysRunemtatus(DsSystemState status,string statusCode);
        /// <summary>
        /// 系统状态事件
        /// </summary>
        public event DSSysRunemtatus DSSystemStatus;
        /// <summary>
        /// 通讯连接状态委托
        /// </summary>
        /// <param name="time">状态时间</param>
        /// <param name="deviceIp">设备ip</param>
        /// <param name="msgType">通讯状态</param>
        public delegate void DSSysConnectionStatus(DateTime time, string deviceIp, DSConnectionState msgType);
        /// <summary>
        /// 通讯状态事件
        /// </summary>
        public event DSSysConnectionStatus DSSystemConnection;
        /// <summary>
        /// 防区数量
        /// </summary>
        public static int ZoomCount = 0;
        #endregion

        #region 事件类型
        /// <summary>
        /// 通讯连接状态
        /// </summary>
        public enum DSConnectionState
        {
            正常 = 0,
            断开 = 1,
            重连 = 2,
            异常 = 5,
            //停服 = 6
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum DSMessageType
        {
            /// <summary>
            /// 防区状态事件
            /// </summary>
            ZoomStatus = 1,
            /// <summary>
            /// 防区操作事件
            /// </summary>
            ZoomOperationStatus,
            /// <summary>
            /// 报警状态事件
            /// </summary>
            AlarmStatus,
            /// <summary>
            /// 报警状态恢复事件
            /// </summary>
            AlarmClearStatus,
            /// <summary>
            /// 系统信息事件
            /// </summary>
            SystemLogEvent,
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public enum DSEventType
        {
            火警报警 = 01,
            键盘火警报警 = 02,
            键盘紧急报警 = 03,
            键盘求助报警 = 04,
            挟持码操作 = 05,
            未授权进入 = 06,
            盗警防区报警 = 07,
            检测故障 = 08,
            火警故障 = 09,
            火警防区故障 = 10,
            盗警防区故障,
            消警,
            撤防操作成功,
            布防操作成功,
            强制旁路操作,
            防区旁路操作,
            防区报警恢复,
            防区操作失败,
            布防操作失败,
            撤防操作失败,
            事件记录满,
            测试信息,
            防区故障恢复,
            防区短路恢复,
            防区开路状态,
            防区正常状态,
            MX设备布防,
            MX设备撤防,
            MX设备1防区报警,
            MX设备2防区报警,
            MX设备3防区报警,
            MX设备1防区恢复,
            MX设备2防区恢复,
            MX设备3防区恢复
        }
        /// <summary>
        /// 系统状态类型
        /// </summary>
        public enum DSSysStatus
        {
            交流电状态 = 0,
            备用电池状态,
            报告发送状态,
            控制状态,
            MPX总线状态,
            无线接收状态,
            辅助电源状态,
            Option设备状态
        }
        /// <summary>
        /// 防区控制
        /// </summary>
        public enum DSControlType
        {
            /// <summary>
            /// 布防
            /// </summary>
            Deployment = 1,
            /// <summary>
            /// 撤防
            /// </summary>
            Disarm = 2,
            /// <summary>
            /// 旁路
            /// </summary>
            Bypass = 3,
            /// <summary>
            /// 消警
            /// </summary>
            ClearAlarm = 4

        }
        #endregion

        #region 全局变量
        /// <summary>
        /// look锁对象
        /// </summary>
        static object lookObj = new object();
        /// <summary>
        /// 数据集合
        /// </summary>
        static Queue<byte[]> strList = new Queue<byte[]>();
        /// <summary>
        /// 检查时间
        /// </summary>
        static DateTime checkTime = DateTime.Now;
        /// <summary>
        /// 连接状态状态
        /// </summary>
        static bool connectStatusSend = true;
        /// <summary>
        /// 断开状态改变
        /// </summary>
        static bool disconnectStatusSend = true;
        /// <summary>
        /// 重连状态改变
        /// </summary>
        static bool reconnectionStatusSend = true;
        /// <summary>
        /// 通讯对象
        /// </summary>
        static SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        /// <summary>
        /// 检查通讯状态
        /// </summary>
        static System.Timers.Timer checkNet = null;
        /// <summary>
        /// 设备ip
        /// </summary>
        static string deviceIp = "";
        #endregion

        /// <summary>
        /// 连接报警通讯
        /// </summary>
        /// <param name="ip">通讯ip</param>
        /// <param name="port">通讯端口</param>
        /// <param name="zoomCount">防区数量</param>
        /// <returns></returns>
        public bool StartConnectionAlarm(string ip, int port, int zoomCount)
        {
            try
            {
                deviceIp = ip;
                ZoomCount = zoomCount;
                if (null == client)
                {
                    client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ip.Trim(), port, 100);
                    client.OnReceviceByte += Client_OnReceviceByte;
                    client.OnStateInfo += Client_OnStateInfo;
                    client.OnExceptionMsg += Client_OnExceptionMsg;
                }
                ConnectPowerServer();
                if (null == checkNet)
                {
                    checkNet = new System.Timers.Timer() { Interval = 1000 * 3 };
                    checkNet.Elapsed += checkNet_Elapsed;
                }
                (new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        static bool isSendData = true;
        private void GetData()
        {
            while (isSendData)
            {
                try
                {
                    lock (strList)
                    {
                        if (strList.Count > 0)
                        {
                            byte[] bs = strList.Dequeue();
                            MyEvent(bs);

                        }
                    }
                    Thread.Sleep(10);
                }
                catch (Exception) { }
            }
            strList.Clear();
        }
        private void checkNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(DateTime.Now, checkTime);
            if (span.TotalSeconds >= 120)
            {
                DSSystemConnection?.Invoke(DateTime.Now, deviceIp, (DSConnectionState)Enum.Parse(typeof(DSConnectionState), "5"));
                checkTime = DateTime.Now;
            }
        }

        private void Client_OnExceptionMsg(string msg)
        {
            try
            {
                ShowMsg(msg);
            }
            catch (Exception) { }
        }
        private void Client_OnStateInfo(string msg, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state)
        {
            try
            {
                ShowMsg(msg);
                if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Connected)
                {
                    checkNet.Start();
                    isSendData = true;
                    if (connectStatusSend)
                    {
                        (new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                        DSSystemConnection?.Invoke(DateTime.Now, deviceIp, (DSConnectionState)Enum.Parse(typeof(DSConnectionState), "0"));
                        connectStatusSend = false;
                        disconnectStatusSend = reconnectionStatusSend = true;
                    }
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    checkNet.Stop();
                    isSendData = false;
                    if (disconnectStatusSend)
                    {
                        DSSystemConnection?.Invoke(DateTime.Now, deviceIp, (DSConnectionState)Enum.Parse(typeof(DSConnectionState), "1"));
                        connectStatusSend = reconnectionStatusSend = true;
                        disconnectStatusSend = false;
                    }
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    checkNet.Stop();
                    isSendData = false;
                    if (reconnectionStatusSend)
                    {
                        DSSystemConnection?.Invoke(DateTime.Now, deviceIp, (DSConnectionState)Enum.Parse(typeof(DSConnectionState), "2"));
                        reconnectionStatusSend = false;
                        connectStatusSend = disconnectStatusSend = true;
                    }
                }
            }
            catch (Exception) { }
        }

        private void Client_OnReceviceByte(Socket temp, byte[] date, int length)
        {

            try
            {
                if (length == 0)
                    return;
                checkTime = DateTime.Now;
                try
                {
                    lock (lookObj)
                    {
                        //strList.Add(date);
                        strList.Enqueue(date);
                    }
                }
                catch (Exception) { }
            }
            catch (SocketException)
            {
                return;
            }
            catch (Exception e)
            {

                ShowMsg("异常：" + e.Message);
                return;
            }
            checkTime = DateTime.Now;
        }
        private void MyEvent(object sender)
        {
            try
            {
                if (sender == null)
                    return;
                byte[] date = sender as byte[];
                string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
                strMsg = byteToHexStr(date, date.Length);
                strMsg = strMsg.Trim('\0');
                int x = 0;
                string w = "";
                foreach (char item in strMsg)
                {
                    if (x == 1)
                    {
                        w += item + " ";
                        x = 0;
                    }
                    else
                    {
                        w += item;
                        x++;
                    }
                }
                w = w.Substring(0, w.Length - " ".Length);
                string[] msg = w.Split(' ');
                if (msg.Length > 0)
                {
                    if (msg[0] == "A0")//系统状态
                    {
                        byte b = new byte();
                        b = (byte)int.Parse(msg[1]);
                        string str = "", code = "";
                        DsSystemState state = new DsSystemState();
                        state = GetSysRunState(b, out str, out code);
                        state.DeviceIp = deviceIp;
                        DSSystemStatus?.Invoke(state,code);
                    }
                    else if (msg[0] == "85")//防区状态+操作状态
                    {
                        for (int i = 0; i < msg.Length / 4; i++)
                        {
                            try
                            {
                                DSMessageType type = DSMessageType.AlarmStatus;
                                switch (Enum.Parse(typeof(DSEventType), ConvertBase(msg[1 + i * 4], 16, 10)))
                                {
                                    case DSEventType.火警报警:
                                    case DSEventType.键盘火警报警:
                                    case DSEventType.键盘紧急报警:
                                    case DSEventType.键盘求助报警:
                                    case DSEventType.挟持码操作:
                                    case DSEventType.未授权进入:
                                    case DSEventType.盗警防区报警:
                                    case DSEventType.MX设备1防区报警:
                                    case DSEventType.MX设备2防区报警:
                                    case DSEventType.MX设备3防区报警:
                                        type = DSMessageType.AlarmStatus;
                                        break;
                                    case DSEventType.MX设备1防区恢复:
                                    case DSEventType.MX设备2防区恢复:
                                    case DSEventType.MX设备3防区恢复:
                                    case DSEventType.防区故障恢复:
                                    case DSEventType.防区短路恢复:
                                    case DSEventType.防区报警恢复:
                                        type = DSMessageType.AlarmClearStatus;
                                        break;
                                    case DSEventType.消警:
                                    case DSEventType.强制旁路操作:
                                    case DSEventType.防区旁路操作:
                                    case DSEventType.防区操作失败:
                                    case DSEventType.布防操作失败:
                                    case DSEventType.撤防操作失败:
                                    case DSEventType.MX设备布防:
                                    case DSEventType.MX设备撤防:
                                        type = DSMessageType.ZoomOperationStatus;
                                        break;
                                    case DSEventType.撤防操作成功:
                                    case DSEventType.布防操作成功:
                                    case DSEventType.防区开路状态:
                                    case DSEventType.防区正常状态:
                                    case DSEventType.火警故障:
                                    case DSEventType.火警防区故障:
                                    case DSEventType.盗警防区故障:
                                        type = DSMessageType.ZoomStatus;
                                        break;
                                    case DSEventType.检测故障:
                                    case DSEventType.事件记录满:
                                    case DSEventType.测试信息:
                                        type = DSMessageType.SystemLogEvent;
                                        break;
                                    default:
                                        break;
                                }
                                DSZoomEvent?.Invoke(new DSEventData() { deviceIp = deviceIp, zoomId = (int.Parse(ConvertBase(msg[2 + i * 4], 16, 10)) + 1).ToString(), eventNo = ConvertBase(msg[1 + i * 4], 16, 10), eventType = (DSEventType)Enum.Parse(typeof(DSEventType), ConvertBase(msg[1 + i * 4], 16, 10)), msgType = type, time = DateTime.Now });
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
            catch (Exception) { }
            checkTime = DateTime.Now;
        }
        /// <summary>
        /// 获取系统运行状态
        /// </summary>
        /// <param name="state">状态字节</param>
        /// <param name="str">输出状态字符串</param>
        /// <param name="statusCode">状态代码</param>
        /// <returns>返回系统运行状态</returns>
        public DsSystemState GetSysRunState(byte state, out string str, out string statusCode)
        {
            str = "";
            statusCode = "";
            statusCode = ByteHelper.ByteToBit(state);
            byte[] bys = ByteHelper.GetBooleanArray(state);
            DsSystemState status = new DsSystemState();
            status.StatusTime = DateTime.Now;
            for (int i = 0; i < bys.Length; i++)
            {
                string name = Enum.GetName(typeof(DSSysStatus), i);
                string ste = (int.Parse(bys[i].ToString()) == 0 ? "正常" : "故障");
                str += (Environment.NewLine + "【" + (i + 1) + "：" + str + "：" + ste + "】");
                DSSysStatus sys = (DSSysStatus)Enum.Parse(typeof(DSSysStatus), i.ToString());
                switch (sys)
                {
                    case DSSysStatus.交流电状态:
                        status.ACStatus = ste;
                        break;
                    case DSSysStatus.备用电池状态:
                        status.Backipbattery = ste;
                        break;
                    case DSSysStatus.报告发送状态:
                        status.Reportsent = ste;
                        break;
                    case DSSysStatus.控制状态:
                        status.Control = ste;
                        break;
                    case DSSysStatus.MPX总线状态:
                        status.MPXStatus = ste;
                        break;
                    case DSSysStatus.无线接收状态:
                        status.Wirelessreceiving = ste;
                        break;
                    case DSSysStatus.辅助电源状态:
                        status.Auxiliarypower = ste;
                        break;
                    case DSSysStatus.Option设备状态:
                        status.OptionStatus = ste;
                        break;
                    default:
                        break;
                }
            }

            str += Environment.NewLine;
            return status;
        }

        ///// <summary>
        ///// 防区控制
        ///// </summary>
        ///// <param name="id">防区号</param>
        //public void ZoomControl(int id,ControlType type)
        //{
        //    if (client != null)
        //    {
        //        byte[] by = new byte[4];
        //        by[0]=85;
        //        switch (type)
        //        {
        //            case ControlType.Deployment:
        //                by[1]=14;
        //                break;
        //            case ControlType.Disarm:
        //                by[1] = 13;
        //                break;
        //            case ControlType.Bypass:
        //                by[1] = 16;
        //                break;
        //            case ControlType.ClearAlarm:
        //                by[1] = 12;
        //                break;
        //            default:
        //                break;
        //        }
        //        by[2] = byte.Parse(id.ToString());
        //        by[3] = 74;
        //    }
        //}
        private bool ConnectPowerServer()
        {
            bool b = false;
            if (client.StartConnection())
                b = true;
            return b;
        }
        ///// <summary>
        ///// 防区消息
        ///// </summary>
        //static string ZoomMsg = "  ";
        void ShowMsg(string str)
        {
            DSMessage?.Invoke(string.Format("{0} 时间：{1}{2}", str, DateTime.Now, Environment.NewLine));
        }
        /// <summary>
        /// 停止服务连接
        /// </summary>
        public void StopConection()
        {
            if (checkNet != null)
                checkNet.Stop();
            isSendData = false;
            strList.Clear();
            client.StopConnection();
            Disposed();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        void Disposed()
        {

            if (checkNet != null)
            {
                checkNet.Stop();
                checkNet = null;
            }
            isSendData = false;
            strList.Clear();
            client = null;
        }

        #region 事件数据结构
        /// <summary>
        /// 系统运行状态
        /// </summary>
        public struct DsSystemState
        {
            /// <summary>
            /// 交流电状态
            /// </summary>
            public string ACStatus { get; set; }
            /// <summary>
            /// 备用电池状态
            /// </summary>
            public string Backipbattery { get; set; }
            /// <summary>
            /// 报告发送状态
            /// </summary>
            public string Reportsent { get; set; }
            /// <summary>
            /// 控制状态
            /// </summary>
            public string Control { get; set; }
            /// <summary>
            /// mpx总线状态
            /// </summary>
            public string MPXStatus { get; set; }
            /// <summary>
            /// 无线接收状态
            /// </summary>
            public string Wirelessreceiving { get; set; }
            /// <summary>
            /// 辅助电源状态
            /// </summary>
            public string Auxiliarypower { get; set; }
            /// <summary>
            /// Option设备状态
            /// </summary>
            public string OptionStatus { get; set; }
            /// <summary>
            /// 状态时间
            /// </summary>
            public DateTime StatusTime { get; set; }
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
        }
        /// <summary>
        /// 系统状态数据结构
        /// </summary>
        public struct DSSysStatusData
        {
            /// <summary>
            /// 状态时间
            /// </summary>
            public DateTime Time { get; set; }
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 系统状态码
            /// </summary>
            public string SysStateCode { get; set; }
            /// <summary>
            /// 系统状态 （状态类型，0-正常 1-故障）
            /// </summary>
            public Dictionary<DSSysStatus,int>  Status { get; set; }
        }
        /// <summary>
        /// 事件数据结构
        /// </summary>
        public struct DSEventData
        {
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string deviceIp { get; set; }
            /// <summary>
            /// 防区号或者分区号
            /// </summary>
            public string zoomId { get; set; }
            /// <summary>
            /// 事件码
            /// </summary>
            public string eventNo { get; set; }
            /// <summary>
            /// 事件类型
            /// </summary>
            public DSEventType eventType { get; set; }
            /// <summary>
            /// 消息类型
            /// </summary>
            public DSMessageType msgType { get; set; }
            /// <summary>
            /// 事件时间
            /// </summary>
            public DateTime time { get; set; }
        }
        #endregion

        #region 字节数组转16进制字符串
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string byteToHexStr(byte[] bytes, int leng)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < leng; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        #endregion

        #region  各进制数间转换 
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        private string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {
                return "0";
            }
        }
        #endregion
    }
}
