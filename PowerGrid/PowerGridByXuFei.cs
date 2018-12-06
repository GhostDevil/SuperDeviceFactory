using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using SuperFramework.SuperConvert;

namespace PowerGrid
{
    /// <summary>
    /// <para>说明：旭飞电网</para>
    /// <para>时间：2016-11-08</para>
    /// <para>作者：痞子少爷</para>
    /// </summary>
    public class PowerGridByXuFei
    {
        #region 委托事件
        /// <summary>
        /// 日志委托
        /// </summary>
        /// <param name="msg">日志信息</param>
        public delegate void XFRevMessage(string msg);
        /// <summary>
        /// 日志信息事件
        /// </summary>
        public event XFRevMessage XFMessage;
        /// <summary>
        /// 防区状态委托
        /// </summary>
        /// <param name="data">电网数据</param>
        public delegate void XFPowerZoomMassage(XFPowerGridData data);
        /// <summary>
        /// 防区状态事件
        /// </summary>
        public event XFPowerZoomMassage XFZoomStatus;
        /// <summary>
        /// 电网连接状态委托
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="time">状态时间</param>
        /// <param name="msgType">电网连接状态</param>
        public delegate void XFPowerGridConnStatus(string ip,DateTime time, XFPowerState msgType);
        /// <summary>
        /// 电网连接状态
        /// </summary>
        public event XFPowerGridConnStatus XFPowerGridConnection;
        /// <summary>
        /// 电网状态委托
        /// </summary>
        /// <param name="msgType">电网消息类型</param>
        /// <param name="allData">电网运行状态</param>
        public delegate void XFPowerGridStatus(XFPowerState msgType, List<XFPowerGridData> allData);
        /// <summary>
        /// 电网状态事件
        /// </summary>
        public event XFPowerGridStatus XFPowerGridFormatStatus;
        #endregion

        #region 全局变量
        /// <summary>
        /// 连接状态状态
        /// </summary>
        bool connectStatusSend = true;
        /// <summary>
        /// 断开连接状态改变
        /// </summary>
        bool disconnectStatusSend = true;
        /// <summary>
        /// 重连状态改变
        /// </summary>
        bool reconnectionStatusSend = true;
        /// <summary>
        /// 网络检查时间
        /// </summary>
        DateTime checkTime = DateTime.Now;
        /// <summary>
        /// 通讯对象
        /// </summary>
        SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        /// <summary>
        /// 网络检查timer
        /// </summary>
        System.Timers.Timer checkNet = null;
        /// <summary>
        /// 设备IP地址
        /// </summary>
        string powerIp = "";
        /// <summary>
        /// 防区总数
        /// </summary>
        public static int ZoomCount = 0;
        /// <summary>
        /// 一次完整防区状态和
        /// </summary>
        int stateCount = 0;
        /// <summary>
        /// 状态
        /// </summary>
        int state = 0;
        /// <summary>
        /// 所有防区状态信息
        /// </summary>
        List<XFPowerGridData> allZoomData = new List<XFPowerGridData>();
        #endregion

        #region 状态类型
        /// <summary>
        /// 电网状态
        /// </summary>
        public enum XFPowerState
        {
            // 分机状态 0：在线 1：撤防2：触网3:  开路4:  短路5:  通信故障
            正常 = 0,
            撤防 = 1,
            触网 = 2,
            开路 = 3,
            短路 = 4,
            /// <summary>
            /// 主机与pc机分机通信故障
            /// </summary>
            断网 = 5,
            断连 = 6,
            异常 = 7,
            断电 = 8,
        }

        /// <summary>
        /// 电网状态
        /// </summary>
        public enum XFPowerStatus
        {
            状态正常 = 00,
            分机无连接 = 01,
            主机已关闭 = 03,
            主机与pc机通信故障 = 04,
            雨天模式 = 241,
            晴天模式 = 240
        }
        /// <summary>
        /// 分机状态
        /// </summary>
        public enum XFExtensionState
        {
            在线 = 0,
            撤防 = 1,
            触网 = 2,
            开路 = 3,
            短路 = 4,
            通讯故障 = 5
        }
        #endregion
        /// <summary>
        /// 开始TCP通讯电网设备
        /// </summary>
        /// <param name="ip">通讯ip</param>
        /// <param name="port">通讯端口</param>
        /// <param name="zoomCount">防区数量</param>
        /// <returns>true：成功 false：失败</returns>
        public bool StartPowerGrid(string ip, int port, int zoomCount)
        {
            try
            {
                powerIp = ip;
                ZoomCount = zoomCount;
                if (client == null)
                {
                    client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ip.Trim(), port, 100);
                    client.OnReceviceByte += Client_OnReceviceByte;
                    client.OnStateInfo += Client_OnStateInfo;
                    client.OnExceptionMsg += Client_OnExceptionMsg;
                }
                ConnectPowerServer();
                if (checkNet == null)
                {
                    checkNet = new System.Timers.Timer() { Interval = 1000 * 3 };
                    checkNet.Elapsed += CheckNet_Elapsed;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        bool isSendNetState = false; 
        private void CheckNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(checkTime,DateTime.Now);
            if (span.TotalSeconds >= 60)
            {
                if (!isSendNetState)
                {
                    XFPowerGridConnection?.Invoke(powerIp,DateTime.Now, (XFPowerState)Enum.Parse(typeof(XFPowerState), "8"));
                    checkTime = DateTime.Now;
                    isSendNetState = true;
                }
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
        //int count = 0;
        private void Client_OnStateInfo(string msg, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state)
        {
            try
            {
                ShowMsg(msg);
                if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Connected)
                {
                    if (connectStatusSend)
                    {
                        XFPowerGridConnection?.Invoke(powerIp, DateTime.Now, (XFPowerState)Enum.Parse(typeof(XFPowerState), "0"));
                        connectStatusSend = isSendNetState = false;
                        disconnectStatusSend = reconnectionStatusSend = true;
                    }
                    checkNet.Start();

                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    checkNet.Stop();
                    if (disconnectStatusSend)
                    {
                        XFPowerGridConnection?.Invoke(powerIp, DateTime.Now, (XFPowerState)Enum.Parse(typeof(XFPowerState), "6"));
                        connectStatusSend = reconnectionStatusSend = true;
                        disconnectStatusSend = isSendNetState = false;
                    }
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    checkNet.Stop();
                    if (reconnectionStatusSend)
                    {
                        XFPowerGridConnection?.Invoke(powerIp, DateTime.Now, (XFPowerState)Enum.Parse(typeof(XFPowerState), "5"));
                        reconnectionStatusSend = isSendNetState = false;
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
                {
                    return;
                }
            }
            catch (SocketException x)
            {
                ShowMsg("异常：" + x.Message);
                return;
            }
            catch (Exception e)
            {

                ShowMsg("异常：" + e.Message);
                return;
            }
            
            MyEvent(date);
        }
        /// <summary>
        /// 电网数据解析
        /// </summary>
        /// <param name="sender">数据</param>
        private void MyEvent(object sender)
        {
            try
            {
                XFPowerGridData pg = new XFPowerGridData();
                byte[] date = sender as byte[];
                string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
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
                if (msg.Length == 18)
                {
                    if (msg[0] == "3A" && msg[16] == "0A")
                    {
                        pg.LowerComputerId = int.Parse(Hexadecimal.ConvertBase(msg[1], 16, 10));
                        pg.IsClearAlarm = int.Parse(Hexadecimal.ConvertBase(msg[14], 16, 10));
                        pg.LowerComputerIsPowerOn = int.Parse(Hexadecimal.ConvertBase(msg[15], 16, 10));
                        pg.ExtensionComputerId = int.Parse(Hexadecimal.ConvertBase(msg[17], 16, 10));
                        pg.PowerGridState = int.Parse(Hexadecimal.ConvertBase(msg[2], 16, 10));
                        if (pg.ExtensionComputerId == 0 && pg.PowerGridState == 0)
                            return;
                        pg.CombatPower = int.Parse(Hexadecimal.ConvertBase(msg[3], 16, 10));
                        pg.Triggerthreshold = int.Parse(Hexadecimal.ConvertBase(msg[4], 16, 10));
                        pg.ExtensionState = int.Parse(Hexadecimal.ConvertBase(msg[5], 16, 10));
                        ////下位机状态
                        //if (pg.PowerGridState == 0)
                            state = pg.ExtensionState;
                        //else if (pg.PowerGridState == 4 || pg.PowerGridState == 1 || pg.PowerGridState == 3) //主机状态
                        //    state = pg.ExtensionState = 5;
                        pg.PowerFlow = int.Parse(Hexadecimal.ConvertBase(msg[8], 16, 10)) * 256 + int.Parse(Hexadecimal.ConvertBase(msg[9], 16, 10));
                        if (pg.ExtensionComputerId == 0 || (pg.PowerFlow == 0 && pg.Voltage == 0))
                            return;

                        if (int.Parse(Hexadecimal.ConvertBase(msg[10], 16, 10)) * 256 + int.Parse(Hexadecimal.ConvertBase(msg[11], 16, 10)) == 0)
                            pg.Voltage = (int.Parse(Hexadecimal.ConvertBase(msg[6], 16, 10)) * 256 + int.Parse(Hexadecimal.ConvertBase(msg[7], 16, 10))) % 1000 + 4700;
                        else
                            pg.Voltage = (int.Parse(Hexadecimal.ConvertBase(msg[6], 16, 10)) * 256 + int.Parse(Hexadecimal.ConvertBase(msg[7], 16, 10))) % 1000 + 10000;

                        pg.DeviceIp = powerIp;
                        pg.EventType = (XFPowerState)Enum.Parse(typeof(XFPowerState), state.ToString());
                        XFZoomStatus?.Invoke(pg);
                        checkTime = DateTime.Now;
                        isSendNetState = false;
                    }
                }
                else
                    return;
                string status = Enum.GetName(typeof(XFPowerState), state);
                w = string.Format("{0}:电压{1} {4} 电流{2} {5} 状态 {3};", pg.ExtensionComputerId, pg.Voltage, pg.PowerFlow, status, "V", "mA");
                ShowMsg(w);
                if (pg.ExtensionComputerId <= ZoomCount)
                    allZoomData.Add(pg);
                stateCount += state;
                if (pg.ExtensionComputerId == ZoomCount)
                {
                    if(allZoomData.Count==ZoomCount)
                        XFPowerGridFormatStatus?.Invoke((XFPowerState)Enum.Parse(typeof(XFPowerState), stateCount == 0 ? "0" : "7"), allZoomData);
                    allZoomData.Clear();
                }
               // checkTime = DateTime.Now;
            }
            catch (Exception) { }
        }


        bool ConnectPowerServer()
        {
            bool b = false;
            if (client.StartConnection())
            {
                b = true;
            }

            return b;
        }

        void ShowMsg(string str)
        {
            XFMessage?.Invoke(string.Format("{0} 时间：{1}{2}", str, DateTime.Now, Environment.NewLine));
        }
        /// <summary>
        /// 停止电网服务连接
        /// </summary>
        public void StopPowerConection()
        {

            if (checkNet != null)
                checkNet.Stop();
            if (client != null)
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
            if (client != null)
                client.Dispose();
            client = null;

        }

        #region 电网数据对象

        /// <summary>
        /// 电网数据对象
        /// </summary>
        public class XFPowerGridData
        {
            /// <summary>
            /// 是否清除过警情
            /// </summary>
            public int IsClearAlarm { get; set; }
            /// <summary>
            /// 下位机电源是否开启
            /// </summary>
            public int LowerComputerIsPowerOn { get; set; }

            /// <summary>
            /// 下位机地址
            /// </summary>
            public int LowerComputerId { get; set; }

            /// <summary>
            /// 分机地址
            /// </summary>
            public int ExtensionComputerId { get; set; }

            /// <summary>
            /// 电网状态 00表示电网状态正常，01分机无连接,03表示主机已关闭 04表示主机与pc机通信故障 F1 雨天模式 F0 晴天模式
            /// </summary>
            public int PowerGridState { get; set; }

            /// <summary>
            /// 打击电量 mC
            /// </summary>
            public int CombatPower { get; set; }

            /// <summary>
            /// 触发阈值 mA
            /// </summary>
            public int Triggerthreshold { get; set; }

            /// <summary>
            /// 分机状态 0：在线 1：撤防2：触网3:  开路4:  短路5:  通信故障
            /// </summary>
            public int ExtensionState { get; set; }
            /// <summary>
            /// 电压 V
            /// </summary>
            public float Voltage { get; set; }

            /// <summary>
            /// 电流 mA
            /// </summary>
            public float PowerFlow { get; set; }
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 事件类型
            /// </summary>
            public XFPowerState EventType { get; set; }

        }
        #endregion

    }
}
