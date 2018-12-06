using System;
using System.Runtime.InteropServices;

namespace MingTaiCardReader
{
    /// <summary>
    /// <para>说明：眀泰读卡器API</para>
    /// <para>时间：2015-07-23</para>
    /// <para>作者：痞子少爷</para>
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 通讯设备标识符
        /// </summary>
        public int icdev; // 通讯设备标识符
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="port">串口号，取值为0～3</param>
        /// <param name="baud">为通讯波特率9600～115200</param>
        /// <returns>成功则返回串口标识符>0，失败返回负值</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_init", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：初始化串口通讯接口
        public static extern int rf_init(short port, int baud);

        /// <summary>
        /// 释放串口,在WIN32环境下icdev为串口的设备句柄，必须释放后才可以再次连接。
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <returns>无</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_exit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：    关闭通讯口
        public static extern short rf_exit(int icdev);

        /// <summary>
        /// 取得读写器硬件版本号，如“mwrf100_v3.0”
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="state">返回版本信息</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_get_status", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_get_status(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] state);

        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="msec">蜂鸣时间，单位是10毫秒</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_beep", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_beep(int icdev, int msec);

        /// <summary>
        /// 将密码装入读写模块RAM中
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">装入密码模式，同密码验证模式  0——KEYSET0的KEYA` 1——KEYSET1的KEYA 2——KEYSET2的KEYA 4——KEYSET0的KEYB 5——KEYSET1的KEYB 6——KEYSET2的KEYB</param>
        /// <param name="secnr">扇区号（M1卡：0～15；  ML卡：0）</param>
        /// <param name="keybuff">写入读写器中的卡密码</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_load_key", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        /// <summary>
        /// 向读写器中装入十六进制密码
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">密码验证模式 0——KEYSET0的KEYA` 1——KEYSET1的KEYA 2——KEYSET2的KEYA 4——KEYSET0的KEYB 5——KEYSET1的KEYB 6——KEYSET2的KEYB</param>
        /// <param name="secnr">扇区号（0～15）</param>
        /// <param name="keybuff">写入读写器中的卡密码</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_load_key_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_load_key_hex(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asc"></param>
        /// <param name="hex"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "a_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="asc"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "hex_a", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);

        /// <summary>
        /// 射频读写模块复位
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="msec">复位时间，0～500毫秒有效</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_reset", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_reset(int icdev, int msec);

        /// <summary>
        /// 清除射频模块内控制寄存器中的一个二进制位
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="_b">要设置的位,0x40清除RF系统1（设置RF系统0）0x02清除RF下电（设置RF上电）</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_clr_control_bit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_clr_control_bit(int icdev, int _b);

        /// <summary>
        /// 设置射频模块控制寄存器中的一个二进制位
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="_b">要设置的位,0x40清除RF系统1（设置RF系统0）0x02清除RF下电（设置RF上电）</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_set_control_bit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_set_control_bit(int icdev, int _b);

        /// <summary>
        /// 在读写器数码管上显示数字
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">显示字符串的长度，最长为8</param>
        /// <param name="disp">要显示的数据,受读写器控制时，显示的日期/时间请参照rf_disp_mode()rf_disp_mode中定义的格式；受计算机控制时，显示方式由显示数据决定；每个字节的最高位为1表示本位数后的小数点亮，为0表示小数点灭。</param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp8", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_disp8(int icdev, short mode, [MarshalAs(UnmanagedType.LPArray)]byte[] disp);

        /// <summary>
        /// 在读写器的数码管上显示数字（为低版本兼容函数，V3.0及以上版本不能使用）
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">小数点显示模式,0——小数点熄灭1——个位后的小数点位亮2——十位后的小数点位亮3——百位后的小数点位亮4——千位后的小数点位亮</param>
        /// <param name="digit">要显示的数</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_disp(int icdev, short mode, int digit);

        /// <summary>
        /// DES算法加密函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="ptrsource">要加密码的原文</param>
        /// <param name="len">原文长度，必需为8的倍数</param>
        /// <param name="ptrdest">加密后的密文</param>
        /// <returns>成功返回0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_encrypt", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);

        /// <summary>
        /// DES算法解密函数(此函数不跟读写器通信，只在PC机运算。)
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="ptrsource">要解密的密文</param>
        /// <param name="len">原文长度必需为8的倍数</param>
        /// <param name="ptrdest">解密后的原文</param>
        /// <returns>成功返回0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_decrypt", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);

        /// <summary>
        /// 读取读写器备注信息
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="offset">偏移地址(0～383)</param>
        /// <param name="len">读取信息长度（1～384）</param>
        /// <param name="databuff">读取到的信息</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_srd_eeprom", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_srd_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 向读写器备注区中写入信息
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="offset">偏移地址（0～383）</param>
        /// <param name="len">读取信息长度（1～384）</param>
        /// <param name="databuff">要写入的信息</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_swr_eeprom", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_swr_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 向读写器端口输出控制字，此信号可用于控制用户的外设。
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="_byte">控制字，该字节低5位每一位控制一个输出</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_setport", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_setport(int icdev, byte _byte);

        /// <summary>
        /// 读取读写器端口输入的值
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="_byte">端口输入值，1个字节,低5位有效。</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_getport", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_getport(int icdev, out byte _byte);

        /// <summary>
        /// 读取读写器日期、星期、时间
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="time">返回数据，长度为7个字节，格式为“年、星期、月、日、时、分、秒”</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_gettime", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_gettime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// 同rf_gettime(),用十六进制表示
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="time">长度为14个字节,均为数字</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_gettime_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_gettime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// 同rf_settime()，用十六进制表示
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="time">长度为14个字节,均为数字</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_settime_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_settime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// 设置读写器日期、星期、时间
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="time">长度为7个字节，格式为“年、星期、月、日、时、分、秒”</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_settime", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_settime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// 设置数码管显示亮度
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="bright">亮度值，0～15有效，0表示最暗，15表示最亮</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = " rf_setbright", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_setbright(int icdev, byte bright);

        /// <summary>
        /// 设置读写器数码管受控方式，关机后可保存设置值
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">受控方式0——数码管显示受计算机控制1——数码管显示受读写器控制（出厂设置）</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_ctl_mode", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_ctl_mode(int icdev, int mode);

        /// <summary>
        /// 设置读写器数码管显示模式，关机后可保存设置值
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">显示模式 0——日期，格式为“年-月-日（yy-mm-dd）”，BCD码 1——时间，格式为“时-分-秒（hh-nn-ss）” ，BCD码</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp_mode", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_disp_mode(int icdev, int mode);

        /// <summary>
        /// 读取软件版本号
        /// </summary>
        /// <param name="ver">存放版本号的缓冲区，长度18字节(包括结束字符’\0’)。</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "lib_ver", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short lib_ver([MarshalAs(UnmanagedType.LPArray)]byte[] ver);
    }
}
