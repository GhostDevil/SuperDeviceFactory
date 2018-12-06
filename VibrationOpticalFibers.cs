using SuperFramework.SuperSocket.TCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using SuperFramework.SuperConvert;

namespace SuperDeviceFactory
{
    /// <summary>
    /// 说明：FS1016震动光钎入侵报警
    /// 时间：2016-07-29
    /// 作者：痞子少爷
    /// 模式：客户端
    /// </summary>
    public class VibrationOpticalFibers
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
        /// <param name="Ip">Ip地址</param>
        public delegate void ClientStateInfo(string msg, TCPSyncSocketEnum.SocketState state);
        /// <summary>
        /// 接收数据事件
        /// </summary>
        public event RecvMessageData RecvData;
        /// <summary>
        /// 接收数据事件委托
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public delegate void RecvMessageData(StruFS1016AlarmInfo data, string ip);
        /// <summary>
        /// 连接状态事件
        /// </summary>
        public event ClientStateInfo StateInfo;
        //static object lookObj = new object();
        public void StartServer(string ip, int port)
        {
            if (hySever == null)
            {
                hySever = new TCPSyncSocketServer(port, ip, 1024 * 1024 * 6);
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
                            DataHandle(o, a, c);
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
                        //SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "stxzjzdstart1002");
                        //SendData(((IPEndPoint)o.RemoteEndPoint).Address.ToString(), ((IPEndPoint)o.RemoteEndPoint).Port, "zzzdgyjk000210Zzzzdgyjk" + Environment.NewLine);
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
            (new Thread(SendHert) { IsBackground = true }).Start();
        }

        private void SendHert()
        {
            while (true)
            {
                for (int i = 0; i < clientInfo.Count; i++)
                {
                    if (clientInfo[i].Status == "在线上")
                        if (hySever.IsStartListening)
                            SendData(clientInfo[i].Ip, clientInfo[i].Port, ConvertHelper.StructToBytes(new StruNetHeartBeatInfo() { ccHeader = new StruCCHeader() {  header_type=1} }));
                }
                Thread.Sleep(200);
            }

        }


        /// <summary>
        /// 报警数据
        /// </summary>
        static List<StruFS1016AlarmInfo> oldData = new List<StruFS1016AlarmInfo>();
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
                //string s=ConvertHelper.BytesToString(a1, Encoding.UTF8).Replace("\0","");
                //a1 = ConvertHelper.StringToBytes(s, Encoding.UTF8);
                //object objs = new StruFS1016AlarmInfo();
                // ConvertHelper.ByteArrayToStructureEndian(a1, ref objs, 0);//ConvertHelper.BytesToStruct<StruFS1016AlarmInfo>(a1); //ConvertHelper.BytesToStruct(a1, typeof(StruFS1016AlarmInfo));
                StruFS1016AlarmInfo obj = ConvertHelper.BytesToStruct<StruFS1016AlarmInfo>(a1);//(StruFS1016AlarmInfo)objs;
                StruNetHeartBeatInfo heart = new StruNetHeartBeatInfo();
                if (obj.ccHeader.sync_code != null)
                {
                    if (obj.ccHeader.header_type == 1)
                        return;
                    RecvData?.Invoke(obj, ((IPEndPoint)o.RemoteEndPoint).Address.ToString());
                }
                else
                    heart = ConvertHelper.BytesToStruct<StruNetHeartBeatInfo>(a1);//ConvertHelper.BytesToStruct(a1, typeof(StruNetHeartBeatInfo));
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="str"></param>
        public void SendData(string ip, int port, string str)
        {
            if (hySever != null)
                hySever.SendData(ip, port, str);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="str"></param>
        public void SendData(string ip, int port, byte[] str)
        {
            if (hySever != null)
                hySever.SendData(ip, port, str);
        }

        #region 数据结构体

        /// <summary>
        ///客户端共有信息头
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi,  Pack = 1)]
        public struct StruCCHeader
        {
            /// <summary>
            /// 同步码，16字符 固定=SocketTcpSync123
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sync_code;
            /// <summary>
            /// 客户端类型: =6：代表FS1016 
            /// </summary>

            public int client_type;
            /// <summary>
            /// FS1016设备编号,如果网内只有1台FS1016,就用1表示，如果网内有多台FS1016设备，就分别用1、2、3……n，表示各台FS1016设备
            /// </summary>
            public int client_code;
            /// <summary>
            /// 信息头类型0未知信息头 1 心跳包 2指令 ，报警数据为指令
            /// </summary>
            public int header_type;
            /// <summary>
            /// 发送时间，14个字符：20160605190909代表2016-06-05 19:09:09   
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string datetime;
            /// <summary>
            /// 保留使用，可用于差错校验码、同步、传输数据
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string chk_code;
        }
        /// <summary>
        /// 心跳包
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct StruNetHeartBeatInfo
        {
            /// <summary>
            /// 心跳包
            /// </summary>
            public StruCCHeader ccHeader;
        }
        /// <summary>
        /// 报警与运行信息的数据结构
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct StruFS1016AlarmInfo
        {
            /// <summary>
            /// 客户端共有信息头
            /// </summary>
            public StruCCHeader ccHeader;
            /// <summary>
            /// 设备编号，用于区分不同FS1016设备
            /// </summary>
            public int dev_code;
            /// <summary>
            /// 数据计数器 用于表示这次传输的报警记录的个数
            /// </summary>
            public int data_count;
            /// <summary>
            /// 保留，暂时无意义。0表示一个扰动监测事件数据包，1表示响应上位机查询的应答包，-1表示无效数据包
            /// </summary>
            public int InfoTypeFalg;
            /// <summary>
            /// 事件ID
            /// </summary>

            public int AlarmEventID;
            /// <summary>
            /// 报警类型（0表示入侵事件，1表示故障事件，2防拆事件）
            /// </summary>

            public int AlarmType;
            /// <summary>
            /// 报警发生时间，14个字符：20160605190909代表2016-06-05 19:09:09
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string AlarmOccurTime;
            /// <summary> 
            /// 报警处理时间，14个字符：20160605190909代表2016-06-05 19:09:09
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string AlarmdealTime;
            /// <summary>
            /// 处理报警人
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dealname;
            /// <summary>
            /// 处理事件类型 0表示有入侵，1表示无入侵，2待定
            /// </summary>

            public int dealtypeid;
            /// <summary>
            /// 报警的子板ID号， 1代表第一个子板，2代表第二个子板，3代表第三个子板，4代表第四个子板 ，1个 设备编号最多4个子板
            /// </summary>
            public int modelid;
            /// <summary>
            /// 报警的防区号ID，1个子板有4个防区
            /// </summary>
            public int Model_fid;
            /// <summary>
            /// 保留字段
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string remark;
            /// <summary>
            /// 保留字段
            /// </summary>
            public int Isdel;
            /// <summary>
            /// 保留字段
            /// </summary>
            public int Channels;
            /// <summary>
            /// 保留字段
            /// </summary>
            public int channels1;
            /// <summary>
            /// 保留字段
            /// </summary>
            public int Hosttimes;
            /// <summary>
            /// 保留字段
            /// </summary>
            public int hosttimes1;
            /// <summary>
            /// 保留字段
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string res_dump;
        }
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
            /// 端口
            /// </summary>
            private int port;
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
            /// 端口
            /// </summary>
            public int Port
            {
                get
                {
                    return port;
                }

                set
                {
                    port = value;
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
        #endregion
    }
}
