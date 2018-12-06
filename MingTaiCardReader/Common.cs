using System;
using System.Runtime.InteropServices;

namespace MingTaiCardReader
{
    /// <summary>
    /// <para>˵�����b̩������API</para>
    /// <para>ʱ�䣺2015-07-23</para>
    /// <para>���ߣ�Ʀ����ү</para>
    /// </summary>
    public class Common
    {
        /// <summary>
        /// ͨѶ�豸��ʶ��
        /// </summary>
        public int icdev; // ͨѶ�豸��ʶ��
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="port">���ںţ�ȡֵΪ0��3</param>
        /// <param name="baud">ΪͨѶ������9600��115200</param>
        /// <returns>�ɹ��򷵻ش��ڱ�ʶ��>0��ʧ�ܷ��ظ�ֵ</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_init", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵������ʼ������ͨѶ�ӿ�
        public static extern int rf_init(short port, int baud);

        /// <summary>
        /// �ͷŴ���,��WIN32������icdevΪ���ڵ��豸����������ͷź�ſ����ٴ����ӡ�
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <returns>��</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_exit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����    �ر�ͨѶ��
        public static extern short rf_exit(int icdev);

        /// <summary>
        /// ȡ�ö�д��Ӳ���汾�ţ��硰mwrf100_v3.0��
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="state">���ذ汾��Ϣ</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_get_status", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_get_status(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] state);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="msec">����ʱ�䣬��λ��10����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_beep", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_beep(int icdev, int msec);

        /// <summary>
        /// ������װ���дģ��RAM��
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">װ������ģʽ��ͬ������֤ģʽ  0����KEYSET0��KEYA` 1����KEYSET1��KEYA 2����KEYSET2��KEYA 4����KEYSET0��KEYB 5����KEYSET1��KEYB 6����KEYSET2��KEYB</param>
        /// <param name="secnr">�����ţ�M1����0��15��  ML����0��</param>
        /// <param name="keybuff">д���д���еĿ�����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_load_key", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        /// <summary>
        /// ���д����װ��ʮ����������
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">������֤ģʽ 0����KEYSET0��KEYA` 1����KEYSET1��KEYA 2����KEYSET2��KEYA 4����KEYSET0��KEYB 5����KEYSET1��KEYB 6����KEYSET2��KEYB</param>
        /// <param name="secnr">�����ţ�0��15��</param>
        /// <param name="keybuff">д���д���еĿ�����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_load_key_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_load_key_hex(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asc"></param>
        /// <param name="hex"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "a_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="asc"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "hex_a", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);

        /// <summary>
        /// ��Ƶ��дģ�鸴λ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="msec">��λʱ�䣬0��500������Ч</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_reset", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_reset(int icdev, int msec);

        /// <summary>
        /// �����Ƶģ���ڿ��ƼĴ����е�һ��������λ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="_b">Ҫ���õ�λ,0x40���RFϵͳ1������RFϵͳ0��0x02���RF�µ磨����RF�ϵ磩</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_clr_control_bit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_clr_control_bit(int icdev, int _b);

        /// <summary>
        /// ������Ƶģ����ƼĴ����е�һ��������λ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="_b">Ҫ���õ�λ,0x40���RFϵͳ1������RFϵͳ0��0x02���RF�µ磨����RF�ϵ磩</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_set_control_bit", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_set_control_bit(int icdev, int _b);

        /// <summary>
        /// �ڶ�д�����������ʾ����
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">��ʾ�ַ����ĳ��ȣ��Ϊ8</param>
        /// <param name="disp">Ҫ��ʾ������,�ܶ�д������ʱ����ʾ������/ʱ�������rf_disp_mode()rf_disp_mode�ж���ĸ�ʽ���ܼ��������ʱ����ʾ��ʽ����ʾ���ݾ�����ÿ���ֽڵ����λΪ1��ʾ��λ�����С��������Ϊ0��ʾС������</param>
        /// <returns></returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp8", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_disp8(int icdev, short mode, [MarshalAs(UnmanagedType.LPArray)]byte[] disp);

        /// <summary>
        /// �ڶ�д�������������ʾ���֣�Ϊ�Ͱ汾���ݺ�����V3.0�����ϰ汾����ʹ�ã�
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">С������ʾģʽ,0����С����Ϩ��1������λ���С����λ��2����ʮλ���С����λ��3������λ���С����λ��4����ǧλ���С����λ��</param>
        /// <param name="digit">Ҫ��ʾ����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_disp(int icdev, short mode, int digit);

        /// <summary>
        /// DES�㷨���ܺ���
        /// </summary>
        /// <param name="key">��Կ</param>
        /// <param name="ptrsource">Ҫ�������ԭ��</param>
        /// <param name="len">ԭ�ĳ��ȣ�����Ϊ8�ı���</param>
        /// <param name="ptrdest">���ܺ������</param>
        /// <returns>�ɹ�����0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_encrypt", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);

        /// <summary>
        /// DES�㷨���ܺ���(�˺���������д��ͨ�ţ�ֻ��PC�����㡣)
        /// </summary>
        /// <param name="key">��Կ</param>
        /// <param name="ptrsource">Ҫ���ܵ�����</param>
        /// <param name="len">ԭ�ĳ��ȱ���Ϊ8�ı���</param>
        /// <param name="ptrdest">���ܺ��ԭ��</param>
        /// <returns>�ɹ�����0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_decrypt", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] key, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrsource, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] ptrdest);

        /// <summary>
        /// ��ȡ��д����ע��Ϣ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="offset">ƫ�Ƶ�ַ(0��383)</param>
        /// <param name="len">��ȡ��Ϣ���ȣ�1��384��</param>
        /// <param name="databuff">��ȡ������Ϣ</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_srd_eeprom", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_srd_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// ���д����ע����д����Ϣ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="offset">ƫ�Ƶ�ַ��0��383��</param>
        /// <param name="len">��ȡ��Ϣ���ȣ�1��384��</param>
        /// <param name="databuff">Ҫд�����Ϣ</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_swr_eeprom", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_swr_eeprom(int icdev, int offset, int len, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        /// <summary>
        /// ���д���˿���������֣����źſ����ڿ����û������衣
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="_byte">�����֣����ֽڵ�5λÿһλ����һ�����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_setport", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_setport(int icdev, byte _byte);

        /// <summary>
        /// ��ȡ��д���˿������ֵ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="_byte">�˿�����ֵ��1���ֽ�,��5λ��Ч��</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_getport", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_getport(int icdev, out byte _byte);

        /// <summary>
        /// ��ȡ��д�����ڡ����ڡ�ʱ��
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="time">�������ݣ�����Ϊ7���ֽڣ���ʽΪ���ꡢ���ڡ��¡��ա�ʱ���֡��롱</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_gettime", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_gettime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// ͬrf_gettime(),��ʮ�����Ʊ�ʾ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="time">����Ϊ14���ֽ�,��Ϊ����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_gettime_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_gettime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// ͬrf_settime()����ʮ�����Ʊ�ʾ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="time">����Ϊ14���ֽ�,��Ϊ����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_settime_hex", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_settime_hex(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// ���ö�д�����ڡ����ڡ�ʱ��
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="time">����Ϊ7���ֽڣ���ʽΪ���ꡢ���ڡ��¡��ա�ʱ���֡��롱</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_settime", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_settime(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] time);

        /// <summary>
        /// �����������ʾ����
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="bright">����ֵ��0��15��Ч��0��ʾ���15��ʾ����</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = " rf_setbright", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_setbright(int icdev, byte bright);

        /// <summary>
        /// ���ö�д��������ܿط�ʽ���ػ���ɱ�������ֵ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">�ܿط�ʽ0�����������ʾ�ܼ��������1�����������ʾ�ܶ�д�����ƣ��������ã�</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_ctl_mode", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_ctl_mode(int icdev, int mode);

        /// <summary>
        /// ���ö�д���������ʾģʽ���ػ���ɱ�������ֵ
        /// </summary>
        /// <param name="icdev">ͨѶ�豸��ʶ��</param>
        /// <param name="mode">��ʾģʽ 0�������ڣ���ʽΪ����-��-�գ�yy-mm-dd������BCD�� 1����ʱ�䣬��ʽΪ��ʱ-��-�루hh-nn-ss���� ��BCD��</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "rf_disp_mode", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short rf_disp_mode(int icdev, int mode);

        /// <summary>
        /// ��ȡ����汾��
        /// </summary>
        /// <param name="ver">��Ű汾�ŵĻ�����������18�ֽ�(���������ַ���\0��)��</param>
        /// <returns>�ɹ��򷵻� 0</returns>
        [DllImport(@"DLL\MTDLL\mwrf32.DLL", EntryPoint = "lib_ver", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //˵����     �����豸��ǰ״̬
        public static extern short lib_ver([MarshalAs(UnmanagedType.LPArray)]byte[] ver);
    }
}
