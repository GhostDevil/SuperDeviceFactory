using SuperFramework.SuperSocket.TCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
namespace PowerGrid
{
    /// <summary>
    /// 说明：河南正达电网
    /// 时间：2016-07-13
    /// 作者：痞子少爷
    /// </summary>
    public class PowerGridByZD
    {
        /// <summary>
        /// 通讯对象
        /// </summary>
        static TCPSyncSocketServer hySever = null;
        /// <summary>
        /// 客户端对象
        /// </summary>
        public static List<ClientInfo> clientInfo = new List<ClientInfo>();
        /// <summary>
        /// 新客户端上线时返回客户端事件委托
        /// </summary>
        /// <param name="temp">Socket对象</param>
        public delegate void AddClientEventHandler(Socket temp);
        /// <summary>
        /// 新客户端上线时返回客户端事件
        /// </summary>
        public event AddClientEventHandler OnlineClient;
        /// <summary>
        /// 客户端下线时返回客户端事件
        /// </summary>
        public event AddClientEventHandler OfflineClient;
        /// <summary>
        /// 连接状态事件委托
        /// </summary>
        /// <param name="ip">Ip地址</param>
        /// <param name="state"></param>
        public delegate void ClientStateInfo(string ip, TCPSyncSocketEnum.SocketState state);
        /// <summary>
        /// 接收数据事件
        /// </summary>
        public event RecvMessageData RecvData;
        /// <summary>
        /// 接收数据事件委托
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public delegate void RecvMessageData(ZDPowerData data, string ip);
        /// <summary>
        /// 连接状态事件
        /// </summary>
        public event ClientStateInfo StateInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void StartServer(string ip, int port)
        {
            if (hySever == null)
            {
                hySever = new TCPSyncSocketServer(port, ip);
                try
                {
                    hySever.OnReceviceByte += (o, a, c) =>
                    {
                        try
                        {
                            ////if(c>0)
                            //try
                            //{
                            //    if (c > 0)
                            //    {
                            //        lock (lookObj)
                            //        {
                            //            datas.Add(new ZDPowerData.PowerData() { o = o, data = a, lenght = c });
                            //        }
                            //    }
                            //}
                            //catch (Exception) { }
                            (new Thread(new ParameterizedThreadStart(handle)) { IsBackground = true }).Start(new List<object>() {o,a,c});
                           //DataHandle(o, a, c);
                            //SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "Zzzdgyjk000210Zzzdgyjk");
                        }
                        catch (Exception) { }
                    };
                    hySever.OnOfflineClient += (c) =>
                    {
                        try
                        {
                            //clientInfo.RemoveAt(clientInfo.FindIndex(o => o.Ip == ((IPEndPoint)c.RemoteEndPoint).Address.ToString()));
                            OfflineClient?.Invoke(c);
                            ClientInfo pd = clientInfo.Find(a => a.Ip == ((IPEndPoint)c.RemoteEndPoint).Address.ToString());
                            if (pd != null)
                            {
                                pd.OnlineTime = DateTime.Now;
                                pd.Status = "离线中";
                            }
                        }
                        catch (Exception) { }
                    };
                    hySever.OnOnlineClient += (o) =>
                    {
                        OnlineClient?.Invoke(o);
                        SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "stxzjzdstart1002");
                        SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "zzzdgyjk000210Zzzzdgyjk" + Environment.NewLine);
                        ClientInfo pd = clientInfo.Find(a => a.Ip == ((IPEndPoint)o.RemoteEndPoint).Address.ToString());
                        if (pd != null)
                        {
                            pd.OnlineTime = DateTime.Now;
                            pd.Status = "在线上";
                        }
                    };
                    hySever.OnStateInfo += (o, a) =>
                    {
                        StateInfo?.Invoke(o, a);
                    };
                    //hySever.OnExceptionMsg += (o) =>
                    //{
                    //    //try
                    //    //{
                    //    //    txtFxMsg.Dispatcher.BeginInvoke(new Action(() =>
                    //    //    {
                    //    //        txtFxMsg.AppendText(Environment.NewLine + AppLogHelper.GetLogStr("----->>>" + string.Format("{0} {1}\r\n", o, DateTime.Now.ToString()), "错误消息"));
                    //    //    }));
                    //    //}
                    //    //catch (Exception) { }
                    //};
                }
                catch (InvalidOperationException) { }
                catch (Exception) { }
            }
            hySever.StartListen();
            //isSend = true;
            //(new Thread(GetData) {  IsBackground=true}).Start();
        }

        private void handle(object obj)
        {
            List<object> objs = obj as List<object>;
            Socket o = objs[0] as Socket;
            byte[] a = objs[1] as byte[];
            int c = (int)objs[2];
            DataHandle(o, a, c);
            //throw new NotImplementedException();
        }

        ///// <summary>
        ///// 是否处理数据
        ///// </summary>
        //static bool isSend = true;
        ///// <summary>
        ///// 数据集合
        ///// </summary>
        //static List<ZDPowerData.PowerData> datas = new List<ZDPowerData.PowerData>();
        //private void GetData()
        //{
        //    while (isSend)
        //    {
        //        try
        //        {
        //            if (datas.Count > 0)
        //            {
        //                ZDPowerData.PowerData pd = datas.Count - 1<0?datas[0]:datas[datas.Count - 1];
        //                DataHandle(pd.o, pd.data, pd.lenght);
        //                datas.Remove(pd);
        //            }
        //            if (datas.Count > 10)
        //                datas.Clear();
        //            Thread.Sleep(500);
        //        }
        //        catch (Exception) { }
        //    }
        //    datas.Clear();
        //}

        /// <summary>
        /// 记录电网状态
        /// </summary>
        static List<ZDPowerData> oldData = new List<ZDPowerData>();
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="o">socket对象</param>
        /// <param name="a1">字节数据</param>
        /// <param name="length">长度</param>
        private void DataHandle(Socket o, byte[] a1, int length)
        {
            try
            {
                string data = Encoding.UTF8.GetString(a1, 0, length);
                if (data.Contains("zzzdgykzjksjend\r\n") && data.Contains("zzzdgykzjksjstart"))
                {
                    data = data.Replace("zzzdgykzjksjend\r\n", "").Replace("zzzdgykzjksjstart", "");
                    data = data.Substring(21);
                    int x = data.Length / 27;
                    int state = 0;
                    for (int i = 0; i < x; i++)
                    {
                        string str = data.Substring(i * 27, 27);
                        string boxId = str.Substring(1, 2);
                        if (int.Parse(boxId) == 0)
                            break;
                        ZDPowerData newZDPowerData = new ZDPowerData()
                        {
                            Id = str.Substring(0, 1),
                            AlarmStatus = str.Substring(3, 2),
                            BoxId = boxId,
                            AfterPowerFlow = double.Parse(str.Substring(21, 4).Insert(3, ".")).ToString(),
                            AfterVoltage = double.Parse(str.Substring(15, 6)).ToString(),
                            BeforePowerFlow = double.Parse(str.Substring(11, 4).Insert(3, ".")).ToString(),
                            BeforeVoltage = double.Parse(str.Substring(5, 6)).ToString(),
                            ConnectionStatus = str.Substring(25, 1),
                            Position = ((IPEndPoint)o.RemoteEndPoint).Address.ToString(),
                            EventTime = DateTime.Now
                        };
                        if (i > 0)
                            newZDPowerData.ConnectionStatus = state.ToString();
                        if (newZDPowerData.ConnectionStatus == "1" || newZDPowerData.AlarmStatus == "01" || newZDPowerData.AlarmStatus == "15")
                        {
                            state = 1;
                            newZDPowerData.AfterPowerFlow =
                            newZDPowerData.AfterVoltage =
                            newZDPowerData.BeforePowerFlow =
                            newZDPowerData.BeforeVoltage = "0";
                        }
                        int oldDataFirst = oldData.FindIndex(v => v.BoxId == newZDPowerData.BoxId && v.Position == newZDPowerData.Position);
                        if (oldDataFirst >= 0)
                        {
                            if (newZDPowerData.AlarmStatus != "00" || newZDPowerData.ConnectionStatus != "0")
                            {
                                if (oldData[oldDataFirst].AlarmStatus == newZDPowerData.AlarmStatus && oldData[oldDataFirst].ConnectionStatus == newZDPowerData.ConnectionStatus)
                                    continue;
                                else
                                    oldData[oldDataFirst] = newZDPowerData;
                            }
                            else
                                oldData[oldDataFirst] = newZDPowerData;
                        }
                        else
                            oldData.Add(newZDPowerData);
                        RecvData?.Invoke(newZDPowerData, ((IPEndPoint)o.RemoteEndPoint).Address.ToString());
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "zzzdgyjk000210Zzzzdgyjk" + Environment.NewLine);
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServer()
        {
            try
            {
                if (hySever != null)
                    hySever.StopListen();
                hySever = null;
                //isSend = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="ip">客户端ip</param>
        /// <param name="port">客户端端口</param>
        /// <param name="str">发送内容</param>
        public void SendData(string ip, int port, string str)
        {
            if (hySever != null)
                hySever.SendData(ip, port, str);
        }

        #region 实体类
        /// <summary>
        /// 上线客户端信息
        /// </summary>
        public class ClientInfo : INotifyPropertyChanged
        {
            /// <summary>
            /// ip地址
            /// </summary>
            private string ip;
            /// <summary>
            /// 状态
            /// </summary>
            private string status;
            /// <summary>
            /// 上线事件
            /// </summary>
            private DateTime onlineTime;
            /// <summary>
            /// ip地址
            /// </summary>
            public string Ip
            {
                get
                {
                    return ip;
                }

                set
                {
                    ip = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 状态
            /// </summary>
            public string Status
            {
                get
                {
                    return status;
                }

                set
                {
                    status = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 上线时间
            /// </summary>
            public DateTime OnlineTime
            {
                get
                {
                    return onlineTime;
                }

                set
                {
                    onlineTime = value; NotifyPropertyChanged();
                }
            }

            /// <summary>
            /// 改事件在更改组件上的属性时发生
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// 调用此方法设置每个属性的访问其
            /// </summary>
            /// <param name="propertyName">属性应用到可选propertyName 参数名</param>
            public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        /// <summary>
        /// 正达电网数据对象
        /// </summary>
        public class ZDPowerData : INotifyPropertyChanged
        {
            /// <summary>
            /// 电网位置
            /// </summary>
            private string position;
            /// <summary>
            /// 主机编号
            /// </summary>
            private string id;
            /// <summary>
            /// 箱号
            /// </summary>
            private string boxId;
            /// <summary>
            /// 报警后电压
            /// </summary>
            private string afterVoltage;
            /// <summary>
            /// 报警后电流
            /// </summary>
            private string afterPowerFlow;
            /// <summary>
            /// 报警前电压
            /// </summary>
            private string beforeVoltage;
            /// <summary>
            /// 报警前电流
            /// </summary>
            private string beforePowerFlow;
            /// <summary>
            /// 通讯状态
            /// </summary>
            private string connectionStatus;
            /// <summary>
            /// 报警状态
            /// </summary>
            private string alarmStatus;
            /// <summary>
            /// 事件时间
            /// </summary>
            private DateTime eventTime;
            /// <summary>
            /// 是否发送报警
            /// </summary>
            private bool isSendAlarm;
            /// <summary>
            /// 通讯状态 0-正常 1-异常
            /// </summary>
            public string ConnectionStatus
            {
                get
                {
                    return connectionStatus;
                }

                set
                {
                    connectionStatus = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 报警后电流
            /// </summary>
            public string AfterPowerFlow
            {
                get
                {
                    return afterPowerFlow;
                }

                set
                {
                    afterPowerFlow = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 报警后电压
            /// </summary>
            public string AfterVoltage
            {
                get
                {
                    return afterVoltage;
                }

                set
                {
                    afterVoltage = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 主机编号
            /// </summary>
            public string Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value; NotifyPropertyChanged();
                }
            }

            /// <summary>
            /// 电网位置
            /// </summary>
            public string Position
            {
                get
                {
                    return position;
                }

                set
                {
                    position = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 报警状态 00无任何报警 01 断网 02 触网 03 短网 04 高压关断 15双短网 08抄录
            /// </summary>
            public string AlarmStatus
            {
                get
                {
                    return alarmStatus;
                }

                set
                {
                    alarmStatus = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 报警前电压
            /// </summary>
            public string BeforeVoltage
            {
                get
                {
                    return beforeVoltage;
                }

                set
                {
                    beforeVoltage = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 报警前电流
            /// </summary>
            public string BeforePowerFlow
            {
                get
                {
                    return beforePowerFlow;
                }

                set
                {
                    beforePowerFlow = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 箱号
            /// </summary>
            public string BoxId
            {
                get
                {
                    return boxId;
                }

                set
                {
                    boxId = value; NotifyPropertyChanged();
                }
            }
            /// <summary>
            /// 事件时间
            /// </summary>
            public DateTime EventTime
            {
                get
                {
                    return eventTime;
                }

                set
                {
                    eventTime = value;
                }
            }
            /// <summary>
            /// 是否发送报警
            /// </summary>
            public bool IsSendAlarm
            {
                get
                {
                    return isSendAlarm;
                }

                set
                {
                    isSendAlarm = value;
                }
            }

            /// <summary>
            /// 改事件在更改组件上的属性时发生
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// 调用此方法设置每个属性的访问其
            /// </summary>
            /// <param name="propertyName">属性应用到可选propertyName 参数名</param>
            public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            /// <summary>
            /// 电网状态
            /// </summary>
            public enum ElectricityState
            {
                正常 = 0,
                断网 = 1,
                触网 = 2,
                短网 = 3,
                高压关断 = 4,
                双短网 = 15,
                抄录=8,
            }
            /// <summary>
            /// 通讯状态
            /// </summary>
            public enum SocketState
            {
                正常 = 00,
                异常 = 01
            }
        }
        /// <summary>
        /// 数据对象
        /// </summary>
        public class PowerData
        {
            /// <summary>
            /// socket 对象
            /// </summary>
            public Socket o { get; set; }
            /// <summary>
            /// 数据
            /// </summary>
            public byte[] data { get; set; }
            /// <summary>
            /// 数据长度
            /// </summary>
            public int lenght { get; set; }
        }
        #endregion
    }
}
