using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;


namespace SuperDeviceFactory.FixedAlarm
{
    /// <summary>
    /// 说明：英安特报警主机
    /// 时间：2014-12-12
    /// 作者：痞子少爷
    /// </summary>
    public class VbusAPI
    {
        #region 字段事件
        /// <summary>
        /// 报警回调事件
        /// </summary>
        /// <param name="vbusaddr">主机服务器Ip地址</param>
        /// <param name="year">事件时间-年</param>
        /// <param name="month">事件时间-月</param>
        /// <param name="day">事件时间-日</param>
        /// <param name="hour">事件时间-时</param>
        /// <param name="minute">事件时间-分</param>
        /// <param name="second">事件时间-秒</param>
        /// <param name="events">事件代码</param>
        /// <param name="port">主机端口</param>
        /// <param name="hostaddr">主机地址</param>
        /// <param name="deviceaddr">模块地址</param>
        /// <param name="subsystem">子系统号</param>
        /// <param name="zone">防区号</param>
        /// <param name="zonetype">防区类型</param>
        public delegate void ReturnAllEventCallback(int vbusaddr, int year, int month, int day, int hour, int minute, int second, int events, int port, int hostaddr, int deviceaddr, int subsystem, int zone, int zonetype);
        /// <summary>
        /// 报警回调事件
        /// </summary>
        public static event ReturnAllEventCallback Alarm;

        const string path = @"DLL\VBUSDLL\VBUSSDK.DLL";
        
        static int clientNo = -1;
        static int ServerPoint = -1;
        static int ClientPoint = -1;
        static int ClientAddr = -1;
        /// <summary>
        /// 通讯服务器IP地址
        /// </summary>
        static string serverIp = "";
        /// <summary>
        /// 重连定时器
        /// </summary>
        private static System.Timers.Timer vbusTimer = null;
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="clientAddr">客户端编号，0-15</param>
        /// <param name="serverPoint">服务器通讯地址</param>
        /// <param name="clientPoint">客户端通讯地址</param>
        /// <returns>0 初始化失败，1 初始化成功</returns>
        public static int InitSDK(int clientAddr, int serverPoint, int clientPoint)
        {

            int x = InitSDK(clientAddr, Alarm);
            clientNo = 1;
            ClientAddr = clientAddr;
            ClientPoint = clientPoint;
            ServerPoint = serverPoint;
            vbusTimer = new System.Timers.Timer() { Interval = 1000 * 5 };
            vbusTimer.Elapsed += vbusTimer_Elapsed;
          
            return x;
        }

        static void vbusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //DisconnectFromVBUSservice();
            //0x01:准备网络套接字失败 0x02:连接服务器失败 0x03:地址被占用,登录不成功 0x04:被限制登录 0x05:未注册的应用管理平台 0x06:已经登录 0x07:客户端主动断开连接

            int x = ConnectToVBUSservice(GetByteFromString(serverIp, (serverIp.Length - 3) * 2, Encoding.ASCII), ClientPoint);
            try
            {
                switch (x)
                {

                    case 0x01:
                    case 0x02:
                        DisconnectFromVBUSservice();
                        FreeSDK();
                        InitSDK(ClientAddr, Alarm);
                        x = ConnectToVBUSservice(GetByteFromString(serverIp, (serverIp.Length - 3) * 2, Encoding.ASCII), ClientPoint);
                        break;
                    case 0x03:
                        DisconnectFromVBUSservice();
                        x = ConnectToVBUSservice(GetByteFromString(serverIp, (serverIp.Length - 3) * 2, Encoding.ASCII), ClientPoint);
                        break;
                    case 0x05:
                        FreeSDK();
                        InitSDK(ClientAddr, Alarm);
                        x = ConnectToVBUSservice(GetByteFromString(serverIp, (serverIp.Length - 3) * 2, Encoding.ASCII), ClientPoint);
                        break;
                    case 0x07:
                        FreeSDK();
                        InitSDK(ClientAddr, Alarm);
                        x = ConnectToVBUSservice(GetByteFromString(serverIp, (serverIp.Length - 3) * 2, Encoding.ASCII), ClientPoint);
                        break;
                    case 0x06:
                    case 0x04:
                        break;
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region API
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="clientAddr">客户端地址，长度0-16</param>
        /// <param name="func">设置与用户界面交互的回调函数指针</param>
        /// <returns>0 初始化失败，1 初始化成功</returns>
        [DllImport(path, CharSet = CharSet.Auto)]

        static extern int InitSDK(int clientAddr, ReturnAllEventCallback func);
        /// <summary>
        /// 释放SDK
        /// </summary>
        /// <returns></returns>
        [DllImport(path, CharSet = CharSet.Auto)]
        public static extern int FreeSDK();

        /// <summary>
        /// 连接到 VBUS通讯服务器
        /// </summary>
        /// <param name="vbussrvipaddr">ＶＢＵＳ通讯服务器的ＩＰ地址</param>
        /// <param name="vbuscommport">ＶＢＵＳ通讯服务器的通讯地址</param>
        /// <returns>成功返回0（0x01:准备网络套接字失败0x02:连接服务器失败0x03:地址被占用,登录不成功0x04:被限制登录0x05:未注册的应用管理平台0x06:已经登录0x07:客户端主动断开连接）</returns>

        [DllImport(path, CharSet = CharSet.Auto)]
        private static extern int ConnectToVBUSservice(byte[] vbussrvipaddr, int vbuscommport);
      
        /// <summary>
        /// 断开与 VBUS通讯服务器的连接
        /// </summary>
        /// <returns>成功返回0,不成功返回1</returns>
        [DllImport(path, CharSet = CharSet.Auto)]
        private static extern int DisconnectFromVBUSservice();
        /// <summary>
        /// 发送控制命令
        /// </summary>
        /// <param name="ctrltype">0x03 布防 0x04 撤防 0x05 旁路 0x06 取消旁路</param>
        /// <param name="vbusaddr"> VBUS 标识地址</param>
        /// <param name="para0"> VBUS 标识地址,于vbusaddr参数相同</param>
        /// <param name="para1">主机通讯端口</param>
        /// <param name="para2">主机地址</param>
        /// <param name="para3">模块地址</param>
        /// <param name="para4">防区号</param>
        /// <param name="para5">子系统号</param>
        /// <param name="para6">控制方式 0x01 针对防区 0x02 针对模块 0x03 针对子系统 0x04 针对所有子系统</param>
        /// <param name="para7">保留固定为零</param>
        /// <returns></returns>
        [DllImport(path, CharSet = CharSet.Auto)]
        public static extern int ControlVBUSservice(int ctrltype, int vbusaddr, int para0, int para1, int para2, int para3, int para4, int para5, int para6, int para7);
        #endregion

        #region 方法

        /// <summary>
        /// 连接到 VBUS通讯服务器
        /// </summary>
        /// <param name="vbussrvipaddr">ＶＢＵＳ通讯服务器的ＩＰ地址</param>
        /// <param name="vbuscommport">ＶＢＵＳ通讯服务器的通讯地址</param>
        /// <returns>成功返回0（0x01:准备网络套接字失败0x02:连接服务器失败0x03:地址被占用,登录不成功0x04:被限制登录0x05:未注册的应用管理平台0x06:已经登录0x07:客户端主动断开连接）</returns>
        public static int ConnectToVBUS(byte[] vbussrvipaddr, int vbuscommport)
        {
            int x = ConnectToVBUSservice(vbussrvipaddr, vbuscommport);
            if (x == 0) vbusTimer.Start();
            return x;
        }


        /// <summary>
        /// 断开与 VBUS通讯服务器的连接
        /// </summary>
        /// <returns>成功返回0,不成功返回1</returns>
        public static int DisconnectVBUS()
        {
            int x=DisconnectFromVBUSservice();
            vbusTimer.Stop();
            return x;
        }

      
        /// <summary>
        /// 发送控制命令
        /// </summary>
        /// <param name="ctrltype">0x03 布防 0x04 撤防 0x05 旁路 0x06 取消旁路</param>     
        /// <param name="para2">主机地址</param>
        /// <param name="para3">模块地址</param>
        /// <param name="para4">防区号</param>
        /// <param name="para5">子系统号</param>
        /// <param name="para6">控制方式 0x01 针对防区 0x02 针对模块 0x03 针对子系统 0x04 针对所有子系统</param>
        /// <returns></returns>
        public static int ControlVBUSservice(CtrlType ctrltype, int para2, int para3, int para4, int para5, int para6)
        {
            int x = (int)ctrltype;
            return ControlVBUSservice((int)ctrltype, clientNo, clientNo, ServerPoint, para2, para3, para4, para5, para6, 0);
        }
        #endregion

        #region 控制方式
        /// <summary>
        /// 控制方式
        /// </summary>
        public enum CtrlType
        {
            布防 = 0x03,
            撤防 = 0x04,
            旁路 = 0x05,
            取消旁路 = 0x06
        }
        #endregion

        #region 从字符串获得指定长度的byte数组
        /// <summary>
        /// 从字符串获得指定长度的byte数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Length">返回长度</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static byte[] GetByteFromString(string s, int Length, Encoding encoding)
        {
            serverIp = s;
            byte[] temp = encoding.GetBytes(s);
            byte[] ret = new byte[Length];
            if (temp.Length > Length)
                Array.Copy(temp, ret, Length);
            else
                Array.Copy(temp, ret, temp.Length);
            ret[Length - 1] = 0;
            return ret;
        }
        #endregion

        #region 获取本机的局域网IP
        /// <summary>
        /// 获取本机的局域网IP
        /// </summary>        
        public static IPAddress LANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                //如果本机IP列表为空，则返回空字符串
                if (addressList.Length < 1)
                    return null;
                if (addressList.Length < 3)
                    return addressList[1];//返回本机的局域网IP
                else
                    return addressList[2];
                //ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                //ManagementObjectCollection nics = mc.GetInstances();
                //string ip = "";
                //foreach (ManagementObject nic in nics)
                //    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                //        ip = (nic["IPAddress"] as String[])[0];
                //return IPAddress.Parse(ip.ToString());
            }
        }
        #endregion

        #region Ip地址转换
        /// <summary>
        /// 将ip地址转为int型
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int GetIntIpByString(string address)
        {
            int intAddress = 0;
            if (address != "")
            {
                //将IP地址转换为字节数组
                byte[] IPArr = IPAddress.Parse(address).GetAddressBytes();
                //将字节数组转换为整型
                intAddress = BitConverter.ToInt32(IPArr, 0);
            }
            return intAddress;
        }
        /// <summary>
        /// 将整形ip转为字符ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetStrIpByIntIp(int ip)
        {
            //将整型转换为IP
            string ipAddress = new IPAddress(BitConverter.GetBytes(ip)).ToString();
            return ipAddress;
        }
        #endregion

        #region 参数类


        /// <summary>
        /// 事件信息
        /// </summary>
        public class EventInfo : EventArgs
        {
            /// <summary>
            /// ＶＢＵＳ通讯服务器的标识地址
            /// </summary>
            public int vbusaddr { get; set; }
            /// <summary>
            /// 事件发生时间-年
            /// </summary>
            public int year { get; set; }
            /// <summary>
            /// 事件发生时间-月
            /// </summary>
            public int month { get; set; }
            /// <summary>
            /// 事件发生时间-天
            /// </summary>
            public int day { get; set; }
            /// <summary>
            /// 事件发生时间-小时
            /// </summary>
            public int hour { get; set; }
            /// <summary>
            /// 事件发生时间-分钟
            /// </summary>
            public int minute { get; set; }
            /// <summary>
            /// 事件发生时间-秒
            /// </summary>
            public int second { get; set; }
            /// <summary>
            /// 事件代码
            /// </summary>
            public int events { get; set; }
            /// <summary>
            /// 设备通讯端口
            /// </summary>
            public int port { get; set; }
            /// <summary>
            /// 主机地址
            /// </summary>
            public int hostaddr { get; set; }
            /// <summary>
            /// 模块地址
            /// </summary>
            public int deviceaddr { get; set; }
            /// <summary>
            /// 子系统号
            /// </summary>
            public int subsystem { get; set; }
            /// <summary>
            /// 防区号
            /// </summary>
            public int zone { get; set; }
            /// <summary>
            /// 防区类型
            /// </summary>
            public int zonetype { get; set; }

        }
        #endregion

        #region 报警类型
        /// <summary>
        /// 报警类型
        /// </summary>
        public enum AlarmType
        {
            防区报警 = 1,
            模块故障 = 34,
            模块恢复 = 35,
            主机脱机 = 12,
            主机恢复 = 13,
            交流电源故障 = 5,
            交流电源恢复 = 6,
            电池_直流_故障 = 7,
            电池_直流_恢复 = 8,
            防区旁路 = 9,
            防区旁路恢复 = 10,
            有声劫警 = 27,
            医疗求助 = 28,
            火警 = 30,
            电话线路故障 = 15,
            电话线路恢复 = 16,
            电话报警故障 = 17,
            电话报警恢复 = 18,
            胁迫无声报警 = 29
        }
        #endregion
    }
}
