using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace SuperDeviceFactory
{
    /// <summary>
    /// 说明：汉科云端震动光钎入侵报警
    /// 时间：2016-08-10
    /// 作者：痞子少爷
    /// 模式：客户端
    /// </summary>
    public class VibrationOpticalFibersHKYR
    {
        public delegate void RevMessage(string msg);
        public event RevMessage Message;
        /// <summary>
        /// 防区报警委托
        /// </summary>
        /// <param name="AlarmData">报警数据</param>
        public delegate void AlarmMassage(AlarmData data);
        /// <summary>
        /// 防区报警事件
        /// </summary>
        public event AlarmMassage Alarm;
        public delegate void DelegateRevMessage(string message);
        static SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        //static System.Timers.Timer checkNet = null;
        static string powerIp = "";

        /// <summary>
        /// 状态委托
        /// </summary>
        /// <param name="txt">时间</param>
        /// <param name="msgType">运行状态</param>
        /// <param name="state">连接状态</param>
        public delegate void RunStatus(string txt, MsgType msgType, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state);
        /// <summary>
        /// 服务运行状态事件
        /// </summary>
        public event RunStatus ServerRunStatus;
        /// <summary>
        /// 防区数量
        /// </summary>
        static int Count = 0;
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MsgType
        {
            正常 = 0,
            断网 = 5,
            停服 = 6
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="ip">通讯服务ip</param>
        /// <param name="port">通讯端口</param>
        /// <param name="zoomCount">防区数量</param>
        /// <returns>true：成功 false：失败</returns>
        public bool InitVOF(string ip, int port, int zoomCount)
        {
            try
            {
                powerIp = ip;
                Count = zoomCount;
                if (client == null)
                {
                    client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ip.Trim(), port,heartbeatData:"",bufferSize:500);
                    client.OnReceviceByte += Client_OnReceviceByte;
                    client.OnStateInfo += Client_OnStateInfo;
                    client.OnExceptionMsg += Client_OnExceptionMsg;
                }
                //if (checkNet == null)
                //{
                //    checkNet = new System.Timers.Timer() { Interval = 1000 * 3 };
                //    checkNet.Elapsed += checkNet_Elapsed;
                //}
                //(new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
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
                    if (strList.Count > 0)
                    {
                        byte[] bs = strList[0];
                        MyEvent(bs);
                        strList.Remove(bs);
                    }
                    Thread.Sleep(20);
                }
                catch (Exception) { }
            }
            strList.Clear();
        }
        //private void checkNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{

        //    TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(DateTime.Now, checkTime);
        //    if (span.TotalSeconds > 10)
        //    {
        //        ServerRunStatus?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (MsgType)Enum.Parse(typeof(MsgType), "4"));
        //        checkTime = DateTime.Now.AddMinutes(-1);
        //    }
        //}

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
                    isSendData = true;
                    (new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                    ServerRunStatus?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (MsgType)Enum.Parse(typeof(MsgType), "0"),state);
                    //checkNet.Start();
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    //checkNet.Stop();
                    isSendData = false;
                    ServerRunStatus?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (MsgType)Enum.Parse(typeof(MsgType), "5"), state);
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    //checkNet.Stop();
                    isSendData = false;
                    ServerRunStatus?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (MsgType)Enum.Parse(typeof(MsgType), "5"), state);
                }
            }
            catch (Exception) { }
        }
        static object lookObj = new object();
        /// <summary>
        /// 数据集合
        /// </summary>
        static List<byte[]> strList = new List<byte[]>();    
        static DateTime checkTime = new DateTime();
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
                        strList.Add(date);
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
        //static AlarmData oldData = null;
        private void MyEvent(object sender)
        {
            try
            {
                if (sender == null)
                    return;
                byte[] date = sender as byte[];
                string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
                //SuperFramework.SuperFile.FileRWHelper<VibrationOpticalFibersHKYR>.AppendText("c:\\123.txt", strMsg);
                strMsg = strMsg.Replace("\n", "");
                AlarmData data = SuperFramework.SuperConvert.JSONHelper.JsonToObject<AlarmData>(strMsg);
                if (data != null)
                {
                    //if (oldData != null)
                    //{
                    //    if (oldData.section != data.section && oldData.action != data.action)
                    //        Alarm?.Invoke(data);
                    //}
                    //else
                        Alarm?.Invoke(data);
                }
                //oldData = data;
            }
            catch (Exception) { }
            checkTime = DateTime.Now;
        }

        /// <summary>
        /// 连接服务
        /// </summary>
        /// <returns>true：成功 false：失败</returns>
        public bool ConnectServer()
        {
            bool b = false;
            if (client.StartConnection())
                b = true;
            return b;
        }
        /// <summary>
        /// 获取防区数量 
        /// </summary>
        public void GetZoomCount()
        {
            if (client != null)
            {
                string s = SuperFramework.SuperConvert.JSONHelper.ObjectToJson(new ZoomCount()) + "\n";
                client.SendData(s);
            }
        }
        void ShowMsg(string str)
        {
            Message?.Invoke(string.Format("{0} 时间：{1}{2}", str, DateTime.Now, Environment.NewLine));
        }
        /// <summary>
        /// 停止服务连接
        /// </summary>
        public void StopConection()
        {

            //if (checkNet != null)
            //    checkNet.Stop();
            isSendData = false;
            strList.Clear();
            client.StopConnection();

        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public void Disposed()
        {

            //if (checkNet != null)
            //{
            //    checkNet.Stop();
            //    checkNet = null;
            //}
            isSendData = false;
            client = null;
        }

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

        #region 数据对象
        /// <summary>
        /// 报警数据对象
        /// </summary>
        [Serializable]
        public class AlarmData
        {
            
            /// <summary>
            /// 格式为数字, 含发送当时年月日时分秒与毫秒
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 格式为数字, 事件发生时间, 年月日时分秒与毫秒
            /// </summary>
            public long time { get; set; }
            /// <summary>
            /// 格式为字符, 报警值为alarm 解除为alarm_discard 
            /// </summary>
            public string action { get; set; }
            /// <summary>
            /// 格式为数字, 事件类型(0为防区断线, 1为报警)
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 格式为数字, 发生事件防区
            /// </summary>
            public int section { get; set; }
            /// <summary>
            /// 防区数量
            /// </summary>
            public int value { get; set; }
        }
        /// <summary>
        /// 防区数量
        /// </summary>
        [Serializable]
        public class ZoomCount
        {
            /// <summary>
            /// 格式为字符, 请求时值为get_section_count,yyyymmddHHmmssfff
            /// </summary>
            public string action { get; set; } = "get_section_count"; //string.Format("get_section_count,{0}", long.Parse(DateTime.Now.ToString("yyyymmddHHmmssfff")));
            /// <summary>
            /// ID
            /// </summary>
            public long id { get; set; } = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            /// <summary>
            /// 防区数量
            /// </summary>
            public int value { get; set; }
        }
        #endregion

    }
}