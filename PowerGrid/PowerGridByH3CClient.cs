using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SuperFramework.SuperConvert;
using System.Collections.Concurrent;

namespace PowerGrid
{
    /// <summary>
    /// <para>说明：时代光华电网</para>
    /// <para>时间：2017-12-10</para>
    /// <para>作者：痞子少爷</para>
    /// </summary>
    public class PowerGridByH3CClient
    {
        #region 委托事件
        /// <summary>
        /// 消息委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void H3CRevMessage(string msg);
        /// <summary>
        /// 消息事件
        /// </summary>
        public event H3CRevMessage H3CMessage;
        /// <summary>
        /// 防区状态委托
        /// </summary>
        /// <param name="data">防区状态</param>
        public delegate void H3CPowerZoomMassage(H3CPowerGridData data);
        /// <summary>
        /// 单个防区状态事件
        /// </summary>
        public event H3CPowerZoomMassage H3CZoomStatus;
        /// <summary>
        /// 电网连接状态委托
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="time">状态时间</param>
        /// <param name="msgType">电网连接状态</param>
        public delegate void H3CPowerGridConnStatus(string ip, DateTime time, H3CMsgType msgType);
        /// <summary>
        /// 电网连接状态
        /// </summary>
        public event H3CPowerGridConnStatus H3CConnection;
        /// <summary>
        /// 电网状态委托
        /// </summary>
        /// <param name="msgType">电网运行状态</param>
        /// <param name="allZoomData">防区状态</param>
        public delegate void H3CPowerGridStatus(H3CMsgType msgType, List<H3CPowerGridData> allZoomData);
        /// <summary>
        /// 电网所有防区状态事件
        /// </summary>
        public event H3CPowerGridStatus H3CAllZoomStatus;
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
        /// 单个设备防区数量
        /// </summary>
        int zCount = 0;
        /// <summary>
        /// 设备编号
        /// </summary>
        int dNo = 0;
        /// <summary>
        /// 所有防区状态信息
        /// </summary>
        List<H3CPowerGridData> allZoomData = new List<H3CPowerGridData>();
        /// <summary>
        /// 一次完整防区状态和
        /// </summary>
        int stateCount = 0;
        /// <summary>
        /// 电网通讯对象
        /// </summary>
        SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        /// <summary>
        /// 网络检查Timer
        /// </summary>
        System.Timers.Timer checkNet = null;
        /// <summary>
        /// 电网设备IP
        /// </summary>
        string powerIp = "";
        /// <summary>
        /// look 锁
        /// </summary>
        object lookObj = new object();
        /// <summary>
        /// 是否发送
        /// </summary>
        bool isSendData = true;
        /// <summary>
        /// 数据集合
        /// </summary>
        ConcurrentQueue<byte[]> strList = new ConcurrentQueue<byte[]>();
        //static List<byte[]> strList = new List<byte[]>();
        /// <summary>
        /// 检查网络时间
        /// </summary>
        DateTime checkTime = DateTime.Now;
        /// <summary>
        /// 记录上一次电网编号
        /// </summary>
        string oldId = "0";
        /// <summary>
        /// 电网防区ID
        /// </summary>
        string zoomId = "";
        /// <summary>
        /// 数据处理次数
        /// </summary>
        int handleDataCount = 0;
        ///// <summary>
        ///// 电网数据间隔
        ///// </summary>
        // int dataDelay =60;
        ///// <summary>
        ///// 发送间隔
        ///// </summary>
        //int sendIndex = 0;
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
            断连 = 6,
            异常 = 7
        }
        #endregion

        /// <summary>
        /// 初始化电网
        /// </summary>
        /// <param name="ip">通讯ip</param>
        /// <param name="port">通讯端口</param>
        /// <param name="zoomCount">单设备防区量</param>
        /// <param name="deviceNo">设备编号</param>
        /// <returns>TRUE：成功 FALSE：失败</returns>
        public bool InitH3CPowerGrid(string ip, int port, int zoomCount = 6, int deviceNo = 1)
        {
            try
            {
                powerIp = ip;
                dNo = deviceNo;
                zCount = zoomCount;
                if (client == null)
                {
                    client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ip.Trim(), port, 50);
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
                //(new Thread(new ThreadStart(GetData)) { IsBackground = true }).Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void HandleData()
        {
            while (isSendData)
            {
                try
                {
                    if (strList.Count > 0)
                    {
                        byte[] bs = null;
                        //lock (lookObj)
                        //{
                        if (strList.TryDequeue(out bs))
                        {
                            byte[] date = bs as byte[];
                            string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
                                                                                             //strMsg = SuperFramework.ByteHelper.ByteToHexStrs(date, date.Length);
                                                                                             //strMsg = strMsg.Trim('\0');
                            MyEvent(ByteHelper.ByteToHexStr(date, date.Length, true));
                        }
                        //}
                    }
                }
                catch (Exception) { }
                Thread.Sleep(100);
            }
            strList = new ConcurrentQueue<byte[]>();
        }
        bool isSendNetState = false;
        private void CheckNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(DateTime.Now, checkTime);
            if (span.TotalSeconds >= 60 * 2)
            {
                if (!isSendNetState)
                {
                    H3CConnection?.Invoke(powerIp, DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "4"));
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
                    isSendData = true;
                    if (connectStatusSend)
                    {
                        H3CConnection?.Invoke(powerIp, DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "0"));
                        (new Thread(new ThreadStart(HandleData)) { IsBackground = true }).Start();
                        connectStatusSend = isSendNetState = false;
                        disconnectStatusSend = reconnectionStatusSend = true;
                    }
                    checkNet.Start();

                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    if (disconnectStatusSend)
                    {
                        H3CConnection?.Invoke(powerIp, DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "6"));
                        connectStatusSend = reconnectionStatusSend = true;
                        disconnectStatusSend = isSendNetState = false;
                        handleDataCount = 0;
                    }
                    checkNet.Stop();
                    isSendData = false;
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    if (reconnectionStatusSend)
                    {
                        H3CConnection?.Invoke(powerIp, DateTime.Now, (H3CMsgType)Enum.Parse(typeof(H3CMsgType), "5"));
                        reconnectionStatusSend = isSendNetState = false;
                        connectStatusSend = disconnectStatusSend = true;
                        handleDataCount = 0;
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
                    //lock (lookObj)
                    //{
                    strList.Enqueue(date);
                    //}
                }
                catch (Exception) { }
            }
            catch (SocketException ex)
            {
                ShowMsg("异常：" + ex.Message);
            }
            catch (Exception e)
            {
                ShowMsg("异常：" + e.Message);
            }
            finally
            {
                checkTime = DateTime.Now;
                isSendNetState = false;
            }
        }

        private void MyEvent(string strMsg)
        {
            try
            {

                //w = w.Substring(0, w.Length - " ".Length);
                //for (int i = 0; i < msg.Length; i++)
                //{
                //    strss += msg[i];
                //}
                //strss += Environment.NewLine;
                string[] msg = strMsg.Split(' ');//
                if (msg.Length == 6 || msg.Length == 7 || msg.Length == 8)
                    StartHandleData(msg);
                else if (msg.Length >= 8)
                {
                    int xx = strMsg.IndexOf("FF 0C 02");
                    string strsL = "", strsR = "";
                    if (xx > 0)
                    {
                        strsR = strMsg.Substring(xx, strMsg.Length - xx - 1);
                        strsL = strMsg.Substring(0, xx);
                        strsL = strsL.Substring(0, strsL.Length - " ".Length);
                        string[] strl = strsL.Split(' ');
                        if (strl.Length == 6)
                        {
                            StartHandleData(strl);
                            return;
                        }
                        else if (strl.Length > 6)
                        {
                            int index = strl.Length - 6;
                            string[] newStrl = new string[6];
                            int y = 0;
                            for (int i = index; i < strl.Length; i++)
                            {
                                newStrl[y] = strl[i];
                                y++;
                            }
                            StartHandleData(newStrl);
                        }
                        if (strsR.Length >= 6)
                            MyEvent(strsR);
                    }
                    else if (xx == 0)
                    {
                        if (msg[0] == "FF" && msg[1] == "0C" && msg[2] == "02")
                        {
                            int a = 8;
                            int maxIndex = 8;
                            int num = 1;
                            int minIndex = 0;
                            string[] newMsg = new string[a];
                            while (msg.Length >= maxIndex)
                            {
                                //if (msg.Length < maxIndex)
                                //    break;
                                int startIndex = 0;
                                for (int i = minIndex; i < maxIndex; i++)
                                {
                                    newMsg[startIndex] = msg[i];
                                    startIndex++;
                                }
                                StartHandleData(newMsg);
                                minIndex += maxIndex;
                                if (a == 8)
                                    a = 6;
                                else if (a == 6)
                                    a = 8;
                                if (a == 6)
                                {
                                    maxIndex += num % 2 == 0 ? 6 : 8;
                                    newMsg = new string[num % 2 == 0 ? 6 : 8];
                                }
                                else if (a == 8)
                                {
                                    maxIndex += num % 2 == 0 ? 8 : 6;
                                    newMsg = new string[num % 2 == 0 ? 8 : 6];
                                }
                                Thread.Sleep(10);
                            }
                        }
                    }
                }
                //else if (msg[0] == "FF" && msg[1] == "0C" && msg[2] == "02")
                //{
                //    int a = 1;
                //    int b = 8;
                //    while (msg.Length >= b)
                //    {
                //        string[] newMsg = new string[8];
                //        if (a % 2 == 0)
                //            newMsg = new string[6];
                //        b += a % 2 == 0?8:6;
                //        msg.CopyTo(newMsg, a + newMsg.Length - 1);
                //        StartHandleData(newMsg);
                //    }
                //}

            }
            catch (Exception) { }
        }
        //string strss = "";
        void StartHandleData(string[] msg)
        {
            if (msg[0] == null) return;
            if (msg.Length == 6 ||  msg.Length == 7)
            {
                if (zoomId != "" && zoomId != "0")
                {
                    NormalData(msg);
                }
            }
            else if (msg.Length == 8)
            {
                if (msg[0] == "FF" && msg[1] == "0C" && msg[2] == "02")
                {
                    string zid = Hexadecimal.ConvertBase(msg[3], 16, 10);
                    if (zid != "0" && !string.IsNullOrWhiteSpace(zid))
                    {
                        if (int.Parse(oldId) < zCount * dNo && int.Parse(oldId) >= int.Parse(zid))
                        {
                            ExceptionData();
                        }
                    }
                    zoomId = Hexadecimal.ConvertBase(msg[3], 16, 10);
                }
            }
        }
        /// <summary>
        /// 正常数据
        /// </summary>
        void  NormalData(string[] msg)
        {
            //if (zoomId == oldId)
            //    return;
            
            string statusNo = Hexadecimal.ConvertBase(msg[0], 16, 10);
            if (string.IsNullOrWhiteSpace(statusNo))
                return;
            string status = Enum.GetName(typeof(H3CMsgType), int.Parse(statusNo));
            if (string.IsNullOrWhiteSpace(status))
                return;
            oldId = zoomId;
            string dianya = Hexadecimal.ConvertBase(msg[1] + msg[2], 16, 10);
            StringHelper.RepairZero(dianya, 4);
            string dianliu = (double.Parse(Hexadecimal.ConvertBase(msg[3], 16, 10)) / 10).ToString("#0.0");
            if (dianliu == "0.0" && dianya == "0" && status == "正常")
            {
                //checkTime = DateTime.Now;
                // return;
                status = "异常";
                statusNo = "7";
            }
            H3CPowerGridData data = new H3CPowerGridData()
            {
                DeviceIp = powerIp,
                EventType = (H3CMsgType)Enum.Parse(typeof(H3CMsgType), statusNo),
                PowerFlow = dianliu,
                PowerGridState = int.Parse(statusNo),
                Voltage = dianya,
                ZoomId = int.Parse(zoomId)
            };
            SendZoomData(data);
        }

        //private void SendData(string statusNo, string status, H3CPowerGridData data)
        //{
            
        //    if (zoomId == "")
        //        return;
        //    oldId = zoomId;
        //    stateCount += int.Parse(statusNo);
        //    SendAllData(int.Parse(zoomId), data);
        //    //if (handleDataCount != 0)
        //    //{
        //    //    H3CZoomStatus?.Invoke(data);
        //    //    ShowMsg(string.Format("{0}:电压{1} V 电流{2} A 状态 {3}", data.ZoomId, data.Voltage, data.PowerFlow, status));
        //    //}
        //    //if (int.Parse(zoomId) <= zCount || int.Parse(zoomId) - zCount <= zCount)
        //    //    allZoomData.Add(data);
        //    ////if (zoomId == OneZoomNum.ToString() || int.Parse(zoomId) - OneZoomNum == OneZoomNum)
        //    //if (allZoomData.Count == zCount || allZoomData.Count - zCount == zCount)
        //    //{
        //    //    if(handleDataCount!=0)
        //    //        H3CAllZoomStatus?.Invoke((H3CMsgType)Enum.Parse(typeof(H3CMsgType), stateCount == 0 ? "0" : "7"), allZoomData);
        //    //    allZoomData.Clear();
        //    //    //oldId = "0";
        //    //    handleDataCount = 1;
        //    //}
        //    //// zoomId = "";
        //}

        /// <summary>
        /// 异常数据
        /// </summary>
        void ExceptionData()
        {
            if (allZoomData.Count != zCount && allZoomData.Count != zCount * 2)
            {
                //int countMin = allZoomData[0].ZoomId;
                //int countMax = allZoomData[allZoomData.Count - 1].ZoomId;
                //if (dNo > 1)
                //    countMin = countMin - zCount;
                for (int i = 1; i <= zCount; i++)//int.Parse(oldId) - 
                {
                    if (allZoomData.FindAll(o => o.ZoomId == i).Count <= 0)
                    {
                        //zoomId = i.ToString();
                        ExceptionData(i);
                    }
                    Thread.Sleep(10);
                }
                //for (int i = 1; i < countMin; i++)//int.Parse(oldId) - 
                //{
                //    //zoomId = i.ToString();
                //    if (allZoomData.FindIndex(o => o.ZoomId == i) < 0)
                //        ExceptionData(i);
                //    Thread.Sleep(10);
                //}
                //int c = dNo > 1? zCount * dNo - countMax: countMax;
                //for (int i = c + 1; i <= zCount; i++)//int.Parse(oldId) + 1
                //{
                //    //oldId = i.ToString();
                //    if (allZoomData.FindIndex(o => o.ZoomId == i) < 0)
                //        ExceptionData(i);
                //    Thread.Sleep(10);
                //}
            }
        }
        void ExceptionData(int i)
        {
            H3CPowerGridData datatemp = new H3CPowerGridData() { DeviceIp = powerIp, ZoomId = i, EventType = H3CMsgType.异常, CombatPower = 0, PowerFlow = "0.0", Voltage = "0000", PowerGridState = 7 };

            SendZoomData(datatemp);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="datatemp"></param>
        void SendZoomData(H3CPowerGridData datatemp)
        {
           // oldId =zoomId;
            stateCount += datatemp.PowerGridState;
            H3CMsgType type = (H3CMsgType)Enum.Parse(typeof(H3CMsgType), datatemp.PowerGridState.ToString());
            if (handleDataCount != 0)
            {
                //if (datatemp.DeviceIp == "10.0.177.204")
                //{
                //    int y = 0;
                //}
                H3CZoomStatus?.Invoke(datatemp);
                ShowMsg(string.Format("{0}:电压{1} V 电流{2} A 状态 {3}", datatemp.ZoomId, datatemp.Voltage, datatemp.PowerFlow, type.ToString()));
            }
            if (allZoomData.Count < zCount || allZoomData.Count - zCount < zCount)
                allZoomData.Add(datatemp);
            //if ((zoomId == zCount.ToString() || int.Parse(zoomId) - zCount == zCount) && allZoomData.Count== zCount)
            if (allZoomData.Count == zCount || allZoomData.Count - zCount == zCount)
            {
                if (handleDataCount != 0)
                    H3CAllZoomStatus?.Invoke((H3CMsgType)Enum.Parse(typeof(H3CMsgType), stateCount == 0 ? "0" : "7"), allZoomData);
                allZoomData.Clear();
                oldId = "0";
                handleDataCount = 1;
            }
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
            if(client!=null)
                client.StopConnection();
            //SuperFramework.SuperFile.FileRWHelper.WriteText("c:\\txt.txt", strss, Encoding.UTF8);
            Thread.Sleep(5000);
            Disposed();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        void Disposed()
        {
            checkNet = null;
            isSendData = false;
            strList = new ConcurrentQueue<byte[]>();
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