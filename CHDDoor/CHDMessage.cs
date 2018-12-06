using System;
using System.Net.Sockets;
using System.Text;

namespace SuperDeviceFactory.CHDDoor
{
    /// <summary>
    /// 说明：纽贝尔门禁消息联动
    /// 时间：2017-08-18
    /// 作者：痞子少爷
    /// </summary>
    public class CHDMessage
    {
        #region 委托事件
        /// <summary>
        /// 日志委托
        /// </summary>
        /// <param name="msg">日志信息</param>
        public delegate void CHDRevMessage(string msg);
        /// <summary>
        /// 日志信息事件
        /// </summary>
        public event CHDRevMessage CHDLogMessage;
        /// <summary>
        /// 门禁事件委托
        /// </summary>
        /// <param name="data">事件信息</param>
        public delegate void CHDDoorMassage(Message data);
        /// <summary>
        /// 门禁动态事件
        /// </summary>
        public event CHDDoorMassage CHDEvent;
        /// <summary>
        /// 门禁服务通讯状态委托
        /// </summary>
        /// <param name="time">状态时间</param>
        /// <param name="msgType">连接状态</param>
        public delegate void CHDConnStatus(DateTime time, CHDMsgType msgType);
        /// <summary>
        /// 门禁服务通讯状态
        /// </summary>
        public event CHDConnStatus CHDConnection;

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
        /// 网络检查时间
        /// </summary>
        static DateTime checkTime = new DateTime();
        /// <summary>
        /// 通讯对象
        /// </summary>
        static SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        /// <summary>
        /// 网络检查timer
        /// </summary>
        static System.Timers.Timer checkNet = null;
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public static string chdServerIp = "";

        #endregion

        #region 状态类型
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum CHDMsgType
        {
            正常 = 0,
            断网 = 5,
            停服 = 6,
            异常 = 7
        }
        #endregion

        /// <summary>
        /// 开始TCP通讯
        /// </summary>
        /// <param name="ip">通讯ip</param>
        /// <param name="port">通讯端口</param>
        /// <returns>true：成功 false：失败</returns>
        public bool StartCHDServer(string ip, int port = 6000)
        {
            try
            {
                chdServerIp = ip;
                if (client == null)
                {
                    client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ip.Trim(), port, 1024);
                    client.OnReceviceByte += Client_OnReceviceByte;
                    client.OnStateInfo += Client_OnStateInfo;
                    client.OnExceptionMsg += Client_OnExceptionMsg;
                }
                ConnectPowerServer();
                if (checkNet == null)
                {
                    checkNet = new System.Timers.Timer() { Interval = 1000 * 3 };
                    checkNet.Elapsed += checkNet_Elapsed;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkNet_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            TimeSpan span = SuperFramework.SuperDate.DateHelper.DateDiff2(DateTime.Now, checkTime);
            if (span.TotalSeconds >= 30)
            {
                CHDConnection?.Invoke(DateTime.Now, (CHDMsgType)Enum.Parse(typeof(CHDMsgType), "8"));
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
                    if (connectStatusSend)
                    {
                        CHDConnection?.Invoke(DateTime.Now, (CHDMsgType)Enum.Parse(typeof(CHDMsgType), "0"));
                        connectStatusSend = false;
                        disconnectStatusSend = reconnectionStatusSend = true;
                    }
                    checkNet.Start();

                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Disconnect)
                {
                    checkNet.Stop();
                    if (disconnectStatusSend)
                    {
                        CHDConnection?.Invoke(DateTime.Now, (CHDMsgType)Enum.Parse(typeof(CHDMsgType), "6"));
                        connectStatusSend = reconnectionStatusSend = true;
                        disconnectStatusSend = false;
                    }
                }
                else if (state == SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState.Reconnection)
                {
                    checkNet.Stop();
                    if (reconnectionStatusSend)
                    {
                        CHDConnection?.Invoke(DateTime.Now, (CHDMsgType)Enum.Parse(typeof(CHDMsgType), "5"));
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
        /// 门禁事件解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyEvent(object sender)
        {
            try
            {
                byte[] date = sender as byte[];
                string strMsg = Encoding.Default.GetString(date, 0, date.Length);// 将接受到的字节数据转化成字符串；
                if (strMsg.IndexOf("NULL") > 0)
                    return;
                strMsg = strMsg.Substring(strMsg.IndexOf("\">") + 2);
                strMsg = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Message>" + strMsg;
                strMsg = strMsg.Replace("MESSAGE", "Message").Replace("<Message", "<Message xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
                //Messgae msg = SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize(typeof(Messgae), strMsg) as Messgae;
                if (strMsg.IndexOf("合法刷卡") > 0)
                    strMsg += "";
                Message msg = SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<Message>(strMsg);
                if (msg.EventName == null) return;
                if (msg != null)
                    CHDEvent?.Invoke(msg);

            }
            catch (Exception) { }
            finally { checkTime = DateTime.Now; }
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
            CHDLogMessage?.Invoke(string.Format("{0} 时间：{1}{2}", str, DateTime.Now, Environment.NewLine));
        }
        /// <summary>
        /// 停止连接
        /// </summary>
        public void StopConection()
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

        #region 事件对象
        /// <summary>
        /// 消息对像
        /// </summary>
        [Serializable]
        public class Message
        {
            /// <summary>
            /// 事件ID
            /// </summary>
            public string RecordID { get; set; }
            /// <summary>
            /// 门禁ID
            /// </summary>
            public string DoorID { get; set; }
            /// <summary>
            /// 门禁编号
            /// </summary>
            public string DoorNo { get; set; }
            /// <summary>
            /// 门禁进出
            /// </summary>
            public string IoState { get; set; }
            /// <summary>
            /// 事件ID
            /// </summary>
            public string EventID { get; set; }
            /// <summary>
            /// 事件等级
            /// </summary>
            public string EventLevel { get; set; }
            /// <summary>
            /// 事件时间
            /// </summary>
            public string EventTime { get; set; }
            /// <summary>
            /// 事件名称
            /// </summary>
            public string EventName { get; set; }
            /// <summary>
            /// 刷卡卡号
            /// </summary>
            public string EventSource { get; set; }
            /// <summary>
            /// 人员id
            /// </summary>
            public string EmpID { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string EmpNo { get; set; }
            /// <summary>
            /// 人员姓名
            /// </summary>
            public string EmpName { get; set; }
            /// <summary>
            /// 卡片类型
            /// </summary>
            public string EmpType { get; set; }
            /// <summary>
            /// 人员性别 1男 2女 
            /// </summary>
            public string Sex { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Duty { get; set; }
            /// <summary>
            /// 人员警号
            /// </summary>
            public string CertifNo { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public string DeptID { get; set; }
            /// <summary>
            /// 部门编号
            /// </summary>
            public string DeptNo { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DeptName { get; set; }
            /// <summary>
            /// 移动电话
            /// </summary>
            public string MobilePhone { get; set; }
        }
        #endregion
    }
}
