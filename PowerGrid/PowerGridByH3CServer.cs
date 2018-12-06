using SuperFramework.SuperConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PowerGrid
{
    public class PowerGridByH3CServer
    {
        #region 委托事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public delegate void H3CRevMessage(string msg);
        /// <summary>
        /// 
        /// </summary>
        public event H3CRevMessage H3CMessage;
        /// <summary>
        /// 防区状态委托
        /// </summary>
        /// <param name="data">防区状态</param>
        public delegate void H3CPowerZoomMassage(H3CPowerGridData data);
        /// <summary>
        /// 防区状态事件
        /// </summary>
        public event H3CPowerZoomMassage H3CZoomStatus;
        /// <summary>
        /// 电网连接状态委托
        /// </summary>
        /// <param name="time">状态时间</param>
        /// <param name="msgType">电网连接状态</param>
        public delegate void H3CPowerGridConnStatus(DateTime time, H3CMsgType msgType);
        /// <summary>
        /// 电网连接状态
        /// </summary>
        public event H3CPowerGridConnStatus H3CPowerGridConnection;
        /// <summary>
        /// 电网状态委托
        /// </summary>
        /// <param name="txt">电网运行状态</param>
        public delegate void H3CPowerGridStatus(H3CMsgType msgType, List<H3CPowerGridData> allZoomData);
        /// <summary>
        /// 电网状态事件
        /// </summary>
        public event H3CPowerGridStatus H3CPowerGridFormatStatus;
        #endregion

        #region 全局变量
        /// <summary>
        /// 连接状态状态
        /// </summary>
        static bool connectStatusSend = true;
        /// <summary>
        /// 断开连接状态改变
        /// </summary>
        static bool disconnectStatusSend = true;
        /// <summary>
        /// 重连状态改变
        /// </summary>
        static bool reconnectionStatusSend = true;
        /// <summary>
        /// 防区数量
        /// </summary>
        static int ZoomCount = 0;
        /// <summary>
        /// 所有防区状态信息
        /// </summary>
        static List<H3CPowerGridData> allZoomData = new List<H3CPowerGridData>();
        /// <summary>
        /// 一次完整防区状态和
        /// </summary>
        static int stateCount = 0;
        /// <summary>
        /// 电网通讯对象
        /// </summary>
        static SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        /// <summary>
        /// 网络检查Timer
        /// </summary>
        static System.Timers.Timer checkNet = null;
        /// <summary>
        /// 电网设备IP
        /// </summary>
        static string powerIp = "";
        /// <summary>
        /// look 锁
        /// </summary>
        static object lookObj = new object();
        /// <summary>
        /// 是否发送
        /// </summary>
        static bool isSendData = true;
        /// <summary>
        /// 数据集合
        /// </summary>
        static Queue<byte[]> strList = new Queue<byte[]>();
        //static List<byte[]> strList = new List<byte[]>();
        /// <summary>
        /// 检查网络时间
        /// </summary>
        static DateTime checkTime = DateTime.Now;
        /// <summary>
        /// 记录上一次电网编号
        /// </summary>
        static string oldId = "0";
        /// <summary>
        /// 电网防区ID
        /// </summary>
        static string id = "";
        /// <summary>
        /// 电网数据内容
        /// </summary>
        static string txt = "  ";
        #endregion

        #region 状态类型
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum H3CMsgType
        {
            正常 = 0,
            断线 = 1,
            短路 = 2,
            触网 = 3,
            断电 = 4,
            断网 = 5,
            停服 = 6,
            异常 = 7
        }
        #endregion

        /// <summary>
        /// 初始化TCP通讯
        /// </summary>
        public bool InitTcpForPowerGrid(string ip, int port, int zoomCount)
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
                (new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void GetData()
        {
            while (isSendData)
            {
                try
                {
                    if (strList.Count > 0)
                    {
                        byte[] bs = null;
                        lock (lookObj)
                        {
                            bs = strList.Dequeue();
                        }
                        MyEvent(bs);
                    }
                    Thread.Sleep(50);
                }
                catch (Exception) { }
            }
            strList.Clear();
        }
        bool isSendNetState = false;
        private void CheckNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(DateTime.Now, checkTime);
            if (span.TotalSeconds > 30)
            {
                if (!isSendNetState)
                {
                    //H3CPowerGridConnection?.Invoke(DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "4"));
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
        private void Client_OnStateInfo(string msg, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state)
        {
            try
            {
                ShowMsg(msg);
                if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Connected)
                {
                    if (connectStatusSend)
                    {
                        H3CPowerGridConnection?.Invoke(DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "0"));
                        (new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                        connectStatusSend = false;
                        disconnectStatusSend = reconnectionStatusSend = true;
                    }
                    checkNet.Start();
                    isSendData = true;
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    if (disconnectStatusSend)
                    {
                        H3CPowerGridConnection?.Invoke(DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "6"));
                        connectStatusSend = reconnectionStatusSend = true;
                        disconnectStatusSend = false;
                    }
                    checkNet.Stop();
                    isSendData = false;
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    if (reconnectionStatusSend)
                    {
                        H3CPowerGridConnection?.Invoke(DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "5"));
                        reconnectionStatusSend = false;
                        connectStatusSend = disconnectStatusSend = true;
                    }
                    checkNet.Stop();
                    isSendData = false;
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
            isSendNetState = false;
        }

        private void MyEvent(object sender)
        {
            try
            {
                if (sender == null)
                    return;
                byte[] date = sender as byte[];
                string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
                strMsg = ByteHelper.ByteToHexStr(date, date.Length);
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

                if (msg.Length == 8)
                {
                    if (msg[0] == "FF" && msg[1] == "0C" && msg[2] == "02")
                        id = Hexadecimal.ConvertBase(msg[3], 16, 10);
                }
                else if (msg.Length == 6)
                {
                    if (id != "" && id != "0")
                    {
                        if (id == oldId)
                            return;
                        oldId = id;
                        string statusNo = Hexadecimal.ConvertBase(msg[0], 16, 10);
                        string status = Enum.GetName(typeof(H3CMsgType), int.Parse(statusNo));
                        string dianya = Hexadecimal.ConvertBase(msg[1] + msg[2], 16, 10);
                        if (dianya.Length < 4)
                        {
                            int count = dianya.Length;
                            for (int i = 0; i < 4 - count; i++)
                                dianya = "0" + dianya;
                        }
                        string dianliu = (double.Parse(Hexadecimal.ConvertBase(msg[3], 16, 10)) / 10).ToString("#0.0");
                        H3CPowerGridData data = new H3CPowerGridData();
                        data.DeviceIp = powerIp;
                        data.EventType = (H3CMsgType)Enum.Parse(typeof(H3CMsgType), statusNo);
                        data.PowerFlow = dianliu;
                        data.PowerGridState = int.Parse(statusNo);
                        data.Voltage = dianya;
                        data.ZoomId = int.Parse(id);
                        if (dianliu == "0" && dianya == "0" && status == "正常")
                            return;
                        H3CZoomStatus?.Invoke(data);
                        dianliu += " ";
                        dianya += " ";
                        ShowMsg(string.Format("{0}:电压{1} V 电流{2} A 状态 {3}", id, dianya, dianliu, status));
                        if (id == "")
                            return;
                        stateCount += int.Parse(statusNo);
                        if (int.Parse(id) <= ZoomCount)
                        {
                            txt += string.Format("{0}:电压{1}电流{2}状态 {3};", id, dianya, dianliu, status);
                            allZoomData.Add(data);
                        }
                        if (id == ZoomCount.ToString())
                        {
                            if (allZoomData.Count == ZoomCount)
                                H3CPowerGridFormatStatus?.Invoke((H3CMsgType)Enum.Parse(typeof(H3CMsgType), stateCount == 0 ? "0" : "7"), allZoomData);
                            txt = "  ";
                            allZoomData.Clear();
                            //Thread.Sleep(3000);
                        }
                        id = "";
                    }
                }

            }
            catch (Exception) { }
            checkTime = DateTime.Now;
        }


        private bool ConnectPowerServer()
        {
            bool b = false;
            if (client.StartConnection())
                b = true;
            return b;
        }


        void ShowMsg(string str)
        {
            H3CMessage?.Invoke(string.Format("{0} 时间：{1}{2}", str, DateTime.Now, Environment.NewLine));
        }
        /// <summary>
        /// 停止电网服务连接
        /// </summary>
        public void StopPowerConection()
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
        #region 电网数据对象

        /// <summary>
        /// 电网数据对象
        /// </summary>
        public class H3CPowerGridData
        {
            /// <summary>
            /// 电网防区
            /// </summary>
            public int ZoomId { get; set; }
            /// <summary>
            /// 电网状态码
            /// </summary>
            public int PowerGridState { get; set; }

            /// <summary>
            /// 打击电量 mC
            /// </summary>
            public int CombatPower { get; set; }
            /// <summary>
            /// 电压 V
            /// </summary>
            public string Voltage { get; set; }

            /// <summary>
            /// 电流 A
            /// </summary>
            public string PowerFlow { get; set; }
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 事件类型
            /// </summary>
            public H3CMsgType EventType { get; set; }

        }
        #endregion
    }
}
