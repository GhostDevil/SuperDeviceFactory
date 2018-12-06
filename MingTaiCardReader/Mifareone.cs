using System;
using System.Runtime.InteropServices;

namespace MingTaiCardReader
{
    /// <summary>
    /// <para>说明：眀泰读卡器API</para>
    /// <para>时间：2015-07-23</para>
    /// <para>作者：痞子少爷</para>
    /// </summary>
    public class Mifareone
    {
        /// <summary>
        /// 寻卡请求
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">寻卡模式,0——表示IDLE模式，一次只对一张卡操作；1——表示ALL模式，一次可对多张卡操作；2——表示指定卡模式，只对序列号等于snr的卡操作（高级函数才有）</param>
        /// <param name="tagtype">卡类型值，0x0004为M1卡，0x0010为ML卡</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_request", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_request(int icdev, int mode, out UInt16 tagtype);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode"></param>
        /// <param name="tagtype"></param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_request_std", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_request_std(int icdev, int mode, out UInt16 tagtype);

        /// <summary>
        /// 卡防冲突，返回卡的序列号(request指令之后应立即调用anticoll，除非卡的序列号已知。)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="bcnt">设为0</param>
        /// <param name="snr">返回的卡序列号地址</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_anticoll", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_anticoll(int icdev, int bcnt, out uint snr);

        /// <summary>
        /// 从多个卡中选取一个给定序列号的卡
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="snr">卡序列号</param>
        /// <param name="size">指向返回的卡容量的数据</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_select", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_select(int icdev, uint snr, out byte size);

        /// <summary>
        /// 验证某一扇区密码(卡上每个扇区有A密码和B密码，可根据实际需要确定是否使用B密码，这由该扇区的存取控制位来决定。此外，读写器中可以存放三套密码，可用rf_load_key()rf_load_key来分别装入，只有装入后才能使用验证密码函数验证。此函数也可用于验证ML卡，扇区号为0。)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">密码验证模式  0——KEYSET0的KEYA` 1——KEYSET1的KEYA 2——KEYSET2的KEYA 4——KEYSET0的KEYB 5——KEYSET1的KEYB 6——KEYSET2的KEYB</param>
        /// <param name="secnr">要验证密码的扇区号（0～15）</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_authentication", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_authentication(int icdev, int mode, int secnr);

        /// <summary>
        /// 验证某一扇区密码(卡上每个扇区有A密码和B密码，可根据实际需要确定是否使用B密码，这由该扇区的存取控制位来决定。可用rf_load_key()rf_load_key来分别装入，只有装入后才能使用验证密码函数验证。主要用于验证扇区号大于15的扇区。)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="mode">密码验证模式  0——KEYSET0的KEYA` 1——KEYSET1的KEYA 2——KEYSET2的KEYA 4——KEYSET0的KEYB 5——KEYSET1的KEYB 6——KEYSET2的KEYB</param>
        /// <param name="keynr">密码扇区号（0～15）</param>
        /// <param name="blocknr">要验证的块地址</param>
        /// <returns>成功则返回 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_authentication_2", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_authentication_2(int icdev, int mode, int keynr, int blocknr);

        /// <summary>
        /// 读取卡中数据(对于M1卡，一次读一个块的数据，为16个字节；对于ML卡，一次读出相同属性的两页（0和1，2和3，...），为8个字节)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">M1卡——块地址（0～63）； ML卡——页地址（0～11）</param>
        /// <param name="databuff">读出数据</param>
        /// <returns>成功则返回0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_read", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_read(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 读取卡中数据(对于M1卡，一次读一个块的数据，为16个字节；对于ML卡，一次读出相同属性的两页（0和1，2和3，...），为8个字节)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">M1卡——块地址（0～63）； ML卡——页地址（0～11）</param>
        /// <param name="databuff">读出数据,数据以十六进制形式表示</param>
        /// <returns>成功则返回0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_read_hex", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_read_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 向卡中写入数据(对于M1卡，一次必须写一个块，为16个字节；对于ML卡，一次必须写一页，为4个字节)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">M1卡——块地址（1～63）；ML卡——页地址（2～11）</param>
        /// <param name="databuff">要写入的数据,数据以十六进制形式表示</param>
        /// <returns>成功则返回0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_write_hex", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_write_hex(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 向卡中写入数据(对于M1卡，一次必须写一个块，为16个字节；对于ML卡，一次必须写一页，为4个字节)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">M1卡——块地址（1～63）；ML卡——页地址（2～11）</param>
        /// <param name="databuff">要写入的数据</param>
        /// <returns>成功则返回0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_write", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// 中止对该卡操作(执行该命令后如果是ALL寻卡模式则必须重新寻卡才能够对该卡操作，如果是IDLE模式则必须把卡移开感应区再进来才能寻得这张卡。)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <returns>成功则返回0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_halt", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_halt(int icdev);

        /// <summary>
        /// 初始化块值(在进行值操作时，必须先执行初始化值函数，然后才可以读、减、加的操作。)
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">块地址（1～63）</param>
        /// <param name="val">初始值</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_initval", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_initval(int icdev, int blocknr, uint val);

        /// <summary>
        /// 读块值
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">块地址（1～63）</param>
        /// <param name="val">读出值的地址</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_readval", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_readval(int icdev, int blocknr, out uint val);

        /// <summary>
        /// 块加值
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">块地址（1～63）</param>
        /// <param name="val">要增加的值</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_increment", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_increment(int icdev, int blocknr, uint val);

        /// <summary>
        /// 块减值
        /// </summary>
        /// <param name="icdev">讯设备标识符</param>
        /// <param name="blocknr">块地址1～63,4n+3除外</param>
        /// <param name="val">要减的值</param>
        /// <returns>成功则返回 0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_decrement", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_decrement(int icdev, int blocknr, uint val);

        /// <summary>
        /// 回传函数，将EEPROM中的内容传入卡的内部寄存器
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">要进行回传的块地址（1～63）</param>
        /// <returns>成功返回0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_restore", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_restore(int icdev, int blocknr);

        /// <summary>
        /// 传送，将寄存器的内容传送到EEPROM中
        /// </summary>
        /// <param name="icdev">通讯设备标识符</param>
        /// <param name="blocknr">要传送的地址（1～63）</param>
        /// <returns>成功返回0</returns>
		[DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_transfer", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern short rf_transfer(int icdev, int blocknr);
    }
}
