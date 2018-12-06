using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// CHD产品公共API
    /// </summary>
    public static class CHDCommon
    {
        #region 公共API
        /// <summary>
        /// 打开并初始化一个串口
        /// </summary>
        /// <param name="szComPort">串口号(如:"\\\\.\\COM1"、"\\\\.\\COM10"...)</param>
        /// <param name="nBaudRate">波特率</param>
        /// <returns>成功: 返回一个大于0的端口标识
        ///失败: 返回一个小于零的值
        ///-1: 打开端口失败,比如该串口已经被占用，或者不存在
        ///-2: 初始化端口失败
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "OpenCom", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenCom(String szComPort, uint nBaudRate);

        /// <summary>
        /// 打开一个TCP端口
        /// </summary>
        /// <param name="szIpAddr">IP地址</param>
        /// <param name="nTcpPort">TCP端口号</param>
        /// <returns>成功: 返回一个大于0的端口标识
        ///失败: 返回一个小于零的值
        ///-1:TCP连接失败 目标IP不存在、端口错误、设备已经被其它程序连接
        ///-2:加载socket失败
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "OpenTcp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenTcp(String szIpAddr, uint nTcpPort);

        /// <summary>
        /// 重新连接端口
        /// </summary>
        /// <param name="nPortIndex">端口标识(OpenCom或者OpenTcp的成功返回值</param>
        /// <returns>成功: 返回0
        ///失败: 返回一个小于零的值
        ///备  注:	当发现连接失效的时候调用该函数
        ///调用该函数之前建议先调用一次ClosePort
        ///如果为TCP方式的连接,ClosePort后需要适当的延时(推荐值500毫秒)后再调用该函数
        /// </returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReConectPort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReConectPort(uint nPortIndex);

        /// <summary>
        /// 关闭一个端口
        /// </summary>
        /// <param name="nPortIndex">端口标识(OpenCom或者OpenTcp的成功返回值</param>
        /// <returns>
        ///成功: 返回 0
        ///失败: 返回 一个负数
        ///-1: 端口标识无效
        ///-2: 操作超时
        /// </returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ClosePort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClosePort(uint nPortIndex);

        /// <summary>
        /// 获取指定的通讯端口状态
        /// </summary>
        /// <param name="nPortIndex">端口标识(OpenCom或者OpenTcp的成功返回值)</param>
        /// <returns>
        /// = 0:	正常、该端口可用
        ///失败: 返回 一个负数
        ///-1:	端口标识无效
        ///-2:	操作超时
        ///-3:	该端口的连接不可用
        ///备  注:	该函数返回的结果只供参考
        /// </returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "GetPortState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPortState(uint nPortIndex);

        /// <summary>
        /// 设置超时
        /// </summary>
        /// <param name="nPortIndex">端口标识(OpenCom或者OpenTcp的成功返回值)</param>
        /// <param name="nTimeOut">超时值，单位：毫秒</param>
        /// <returns>
        /// 成功 返回设置前的超时值(单位：毫秒)
        ///-1: 端口标识无效
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetTimeOuts", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetTimeOuts(uint nPortIndex, uint nTimeOut);


        /// <summary>
        /// 读取指定端口上的设备请求消息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="pnNetID">发出请求包的NETID</param>
        /// <returns>返回值:	设备返回值
        ///备  注:	该函数不在支持 只返回 -1
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadReQuest", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadReQuest(uint nPortIndex, ref uint pnNetID);


        /// <summary>
        /// 中断读取设备请求消息
        /// </summary>
        /// <param name="nPortIndex">端口索引号</param>
        /// <returns>返回值:	0 成功； 非零为失败.
        ///备  注:	该函数不在支持 只返回 -1
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BreakReQuest", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BreakReQuest(uint nPortIndex);

        #endregion

        #region 公共方法
        public static DateTime CombinDateTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

        }

        /// <summary>
        /// 获取SYSTEMTIME日期
        /// </summary>
        /// <param name="dt">DateTime格式日期</param>
        /// <returns>返回SYSTEMTIME日期</returns>
        public static SYSTEMTIME ParasTime(DateTime dt)
        {
            if (dt == null)
                dt = DateTime.Now;
            SYSTEMTIME time = new SYSTEMTIME();
            time.wYear = (ushort)dt.Year;
            time.wMilliseconds = (ushort)dt.Millisecond;
            time.wDay = (ushort)dt.Day;
            time.wHour = (ushort)dt.Hour;
            time.wMonth = (ushort)dt.Month;
            time.wSecond = (ushort)dt.Second;
            time.wMinute = (ushort)dt.Minute;
            time.wDayOfWeek = (ushort)dt.DayOfWeek;
            return time;

        }

        /// <summary>
        /// 转换日期
        /// </summary>
        /// <param name="tm">SYSTEMTIME 时间</param>
        /// <returns>返回DateTime格式时间</returns>
        public static DateTime ParasTime(SYSTEMTIME tm)
        {
            //return DateTime.Now;
            try
            {
                return new DateTime(tm.wYear, tm.wMonth, tm.wDay, tm.wHour, tm.wMinute, tm.wSecond, tm.wMilliseconds);
            }
            catch(Exception)
            {
                //System.Windows.Forms.MessageBox.Show("返回的时间不是正确的时间表达式");
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 将字节数据转换为BCD编码
        /// </summary>
        /// <param name="rawByte">字节数据</param>
        /// <param name="isHight">是否转换为高位字节，默认为false</param>
        /// <returns></returns>
        public static byte ByteToBCD(byte rawByte, bool isHight = false)
        {
            byte data = isHight ? (byte)(rawByte >> 4) : (byte)(rawByte & 0x0f);
            data = (data >= 0 && data <= 9) ? (byte)(48 + data) : (byte)(55 + data);
            return data;
        }

        /// <summary>
        /// 将两字节BCD编码转换为单字节数据
        /// </summary>
        /// <param name="hByte">高位BCD编码</param>
        /// <param name="lbyte">低位BCD编码</param>
        /// <returns></returns>
        public static byte BCDToByte(byte hByte, byte lbyte)
        {
            byte[] data = new byte[] { hByte, lbyte };
            for (int i = 0; i < data.Length; i++)
            {
                if ((data[i] >= 48) && (data[i] <= 57))//数字
                {
                    data[i] = (byte)(data[i] - 48);
                }
                else
                {
                    if ((data[i] >= 97) && (data[i] <= 102))//小写字母
                    {
                        data[i] = (byte)(data[i] - 87);
                    }
                    else data[i] = (byte)(data[i] - 55);//大写字母
                }
            }
            return (byte)((data[0] << 4) + data[1]);
        }



        /// <summary>
        /// 将字符串转换为BCD字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] StringToBCDByteArray(String str)
        {
            byte[] strByte = Encoding.Default.GetBytes(str);
            byte[] bcdByte = new byte[strByte.Length * 2];
            for (int i = 0; i < strByte.Length; i++)
            {
                bcdByte[2 * i] = ByteToBCD(strByte[i], true);
                bcdByte[2 * i + 1] = ByteToBCD(strByte[i]);
            }
            return bcdByte;
        }

        /// <summary>
        /// 将BCD字节数字转换为字符串
        /// </summary>
        /// <param name="bcdArray">BCD字节数组</param>
        /// <returns></returns>
        public static string BCDByteArrayToString(byte[] bcdArray)
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < bcdArray.Length; i++, i++)
            {
                data.Add(BCDToByte(bcdArray[i], bcdArray[i + 1]));
            }
            string str = Encoding.Default.GetString(data.ToArray());
            return str;

        }
        #endregion
    }
}
