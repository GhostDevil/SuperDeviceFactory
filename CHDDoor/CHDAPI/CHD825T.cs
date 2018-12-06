using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// CHD825T
    /// </summary>
    public static class CHD825T
    {

        /***************************************************************
       * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
         2．	执行LinkOn校验设备密码，确认设备通讯权限；
         3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
         4．	执行LinkOff取消设备通讯权限；
         5．	当程序关闭时使用ClosePort关闭端口。
       **************************************************************/



        /// <summary>
        /// 设置日期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">要设置的时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);






        /// <summary>
        /// 设置LED显示命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNumber"></param>
        /// <param name="btData">包括汉字在内的16进制数据，最多30个字节</param>
        /// <param name="btDataL">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_LEDView", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_LEDView(uint nPortIndex, uint nNetID,uint nNumber, String btData, uint btDataL);





        /// <summary>
        /// 设置LED显示命令(固定显示)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNumber">值：0，1，2</param>
        /// <param name="btData">包括汉字在内的16进制数据，最多50个字节</param>
        /// <param name="nLen">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_LEDView1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_LEDView1(uint nPortIndex, uint nNetID, uint nNumber, string btData, uint nLen);





        /// <summary>
        /// 设置打印机命令(设置第一行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">包括汉字在内的16进制数据，最多24个字节</param>
        /// <param name="nLen">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd1(uint nPortIndex, uint nNetID, byte[] btData, uint nLen);





        /// <summary>
        /// 设置打印机命令(设置第二行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">包括汉字在内的16进制数据，最多48个字节</param>
        /// <param name="nLen">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd2(uint nPortIndex, uint nNetID, byte[] btData, uint nLen);





        /// <summary>
        /// 设置打印机命令(设置第三行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">包括汉字在内的16进制数据，最多48个字节</param>
        /// <param name="nLen">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd3(uint nPortIndex, uint nNetID, byte[] btData, uint nLen);





        /// <summary>
        /// 设置打印机命令(设置客户代码)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="strCode">只接受数字和大写字母以及几个特殊符号，2个字符。范围48小于等于d小于等于57  65小于等于d小于等于90和 36,37,43,45,46,47六个字符。两个字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd4(uint nPortIndex, uint nNetID, string strCode);





        /// <summary>
        /// 设置打印机命令(打印小票)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd5", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd5(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设置打印机命令(设置打印机型号)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nType">打印机型号，值：0～255</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetPrinterCmd6", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetPrinterCmd6(uint nPortIndex, uint nNetID, int nType);





        /// <summary>
        /// 设置进、出场
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bInOrOut">=0为出场； =1为进场</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetInOrOut", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetInOrOut(uint nPortIndex, uint nNetID, int bInOrOut);





        /// <summary>
        /// 放行命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bInOrOut">值：1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ControlDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ControlDoor(uint nPortIndex, uint nNetID, int bInOrOut);





        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">用户卡号。5个字节（’0’~’9’,’A’~’F’）,分高低字节</param>
        /// <param name="szUserID">用户ID。4个字节（’0’~’9’,’A’~’F’）,分高低字节</param>
        /// <param name="szUserPwd">用户密码。2个字节（’0’~’9’,’A’~’F’）,分高低字节</param>
        /// <param name="pTime">时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_AddUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_AddUser(uint nPortIndex, uint nNetID, String szCardNo, string szUserID, string szUserPwd, ref SYSTEMTIME pTime);





        /// <summary>
        /// 删除用户卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">用户卡号。5个字节（’0’~’9’,’A’~’F’）,分高低字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_DeleteCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_DeleteCard(uint nPortIndex, uint nNetID, string szCardNo);





        /// <summary>
        /// 全部删除用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_DeleteAllCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_DeleteAllCard(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_Init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_Init(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设定门口机记录读指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPointer">设定LoadP。0～65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetReadPointer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetReadPointer(uint nPortIndex, uint nNetID, uint nPointer);





        /// <summary>
        /// 设定门口机整个记录区指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPointer1">设定SAVEP。0～65535</param>
        /// <param name="nPointer2">设定LOADP。0～65535</param>
        /// <param name="bFlag">MF，0～255</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetRecPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetRecPoint(uint nPortIndex, uint nNetID, uint nPointer1, uint nPointer2, int bFlag);





        /// <summary>
        /// 读取系统时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 读取打印机内容命令(读取第一行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">返回第一行的信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadPrinterCmd1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadPrinterCmd1(uint nPortIndex, uint nNetID, StringBuilder btData);





        /// <summary>
        /// 读取打印机内容命令(读取第二行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">返回第二行的信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadPrinterCmd2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadPrinterCmd2(uint nPortIndex, uint nNetID, StringBuilder btData);





        /// <summary>
        /// 读取打印机内容命令(读取第三行)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">返回第三行的信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadPrinterCmd3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadPrinterCmd3(uint nPortIndex, uint nNetID, byte[] btData);





        /// <summary>
        /// 读取打印机内容命令(读取客户代码)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="strCode">返回客户代码</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadPrinterCmd4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadPrinterCmd4(uint nPortIndex, uint nNetID, StringBuilder strCode);





        /// <summary>
        /// 读取门口机历史记录柜桶参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nButtom">返回桶底BOTTOM；</param>
        /// <param name="nSAVEP">返回下一次新记录存放指针 SAVEP（2字节）；</param>
        /// <param name="nLOADP">返回下一次读取记录位置指针 LOADP（2字节）</param>
        /// <param name="nMF">返回SM已修改LOADP标志MF （1字节）</param>
        /// <param name="nMAXLEN">返回柜桶最大深度MAXLEN（2字节）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadBottom", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadBottom(uint nPortIndex, uint nNetID, out int nButtom, out int nSAVEP, out int nLOADP, out int nMF, out int nMAXLEN);





        /// <summary>
        /// 顺序读取门口机一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCard">返回卡号</param>
        /// <param name="pTime">返回时间</param>
        /// <param name="pnRecWorkState">返回状态</param>
        /// <param name="pnRecRemark">返回Remark</param>
        /// <param name="Flag">返回停车场标识</param>
        /// <param name="pAddr">返回地址码</param>
        /// <param name="pNo">返回进场编号</param>
        /// <param name="pLen">返回数据长度</param>
        /// 说明：
        ///pLen=14,只读取pCard、pTime、pnRecWorkState、pnRecRemark的值；
        ///pnRecRemark为0表示出场，为1表示进场
        ///pnRecWorkState为0时，表示摩托车;pnRecWorkState=1时，表示小汽车
        ///pLen=9,只读取Flag、pAddr、pTime、pNo的值。
        ///Flag>>8 为 1时表示缺纸，为0时表示不缺纸
        ///Flag and 0xff为1时表示小汽车，为0时表示摩托车
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadOneRec(uint nPortIndex, uint nNetID, StringBuilder pCard, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int Flag, out int pAddr, out int pNo, out int pLen);





        /// <summary>
        /// 查询门口机的最新事件记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCard">返回卡号</param>
        /// <param name="pTime">返回时间</param>
        /// <param name="pnRecWorkState">返回状态</param>
        /// <param name="pnRecRemark">返回Remark</param>
        /// <param name="Flag">返回停车场标识</param>
        /// <param name="pAddr">返回地址码</param>
        /// <param name="pNo">返回进场编号</param>
        /// <param name="pLen">返回数据长度</param>
        /// 说明：
        ///pLen=14,只读取pCard、pTime、pnRecWorkState、pnRecRemark的值；
        ///pnRecRemark为0表示出场，为1表示进场
        ///pnRecWorkState为0时，表示摩托车;pnRecWorkState=1时，表示小汽车
        ///pLen=9,只读取Flag、pAddr、pTime、pNo的值。
        ///Flag>>8 为 1时表示缺纸，为0时表示不缺纸
        ///Flag and 0xff为1时表示小汽车，为0时表示摩托车
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadNewEvent", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadNewEvent(uint nPortIndex, uint nNetID, StringBuilder pCard, out  SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int Flag, out int pAddr, out int pNo, out int pLen);





        /// <summary>
        /// 读取已存储的用户数目
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCount">返回用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadUserCount(uint nPortIndex, uint nNetID, out int nCount);





        /// <summary>
        /// 查询指定卡号的用户卡是否存在
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">用户卡号。5个字节（’0’~’9’,’A’~’F’）,分高低字节</param>
        /// <param name="sCardNo">返回用户卡号</param>
        /// <param name="sUserID">返回用户ID</param>
        /// <param name="sPwd">返回用户密码</param>
        /// <param name="pTime">返回日期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadUser(uint nPortIndex, uint nNetID, string szCardNo, StringBuilder sCardNo, StringBuilder sUserID, StringBuilder sPwd, out  SYSTEMTIME pTime);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="nLoadp"></param>
        /// <param name="bRet"></param>
        /// <param name="szRecSource"></param>
        /// <param name="pTime"></param>
        /// <param name="pnRecWorkState"></param>
        /// <param name="pnRecRemark"></param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadRecByRandom", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadRecByRandom(uint nPortIndex, uint nNetID, int nLoadp,ref int bRet,  StringBuilder szRecSource, ref SYSTEMTIME pTime, out uint pnRecWorkState, out uint pnRecRemark);
                                 
        


        /// <summary>
        /// 读取指定存储位置的用户登记资料
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iPos">用户存储在设备内存里面的位置。0～65535</param>
        /// <param name="sCardNo">返回用户卡号</param>
        /// <param name="sUserID">返回用户ID</param>
        /// <param name="sPwd">返回用户密码</param>
        /// <param name="pTime">返回日期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadUserByPos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadUserByPos(uint nPortIndex, uint nNetID, int iPos, StringBuilder sCardNo, StringBuilder sUserID, StringBuilder sPwd, out SYSTEMTIME pTime);





        /// <summary>
        /// 读取设备名称及版本号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_ReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);





        /// <summary>
        /// 设置停车场车位状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bState">=0为车位空余； =1为车位已满</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_SetParkState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_SetParkState(uint nPortIndex, uint nNetID, int bState);





        /// <summary>
        /// 远程控制继电器
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRealy">继电器编号。1~4</param>
        /// <param name="btSeconds">继电器动作时间.1~255。转换为实际秒数为0.1～25.5</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_RemoteControlRelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_RemoteControlRelay(uint nPortIndex, uint nNetID, uint nRealy, byte btSeconds);





        /// <summary>
        /// logo 添加命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="btData">信息体。BYTE数组</param>
        /// <param name="nLen">btData的长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD825T_WriteLogoInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD825T_WriteLogoInfo(uint nPortIndex, uint nNetID, IntPtr btData, uint nLen);






        /*****************************************************************************
         * 附录1 刷卡进出场记录格式
         每条记录用14字节表示：
         事件来源（5字节）	日期，时间（7字节）	       状态（1字节）	备注（1字节）
          卡号或ID号等	 年（2），月，日，时，分，秒	 STATUS	          REMARK
         REMARK : = 1 表示进场
                  = 0 表示出场
         注：新解释，
         状态字节，表示车辆类型。
         当STATUS=0时，表示摩托车;STATUS=1时，表示小汽车。
         * ***************************************************************************/




        /****************************************************************************
         * 附录2 小票进场记录格式(小票由控制板自动开门，电脑不需要开门);
           每条记录9个字节表示：
           停车场标识	 地址码(HEX)	入场时间（月-日-时-分）	    进场编号（HEX）
           2字节（BCD）	   1字节	           4字节	           2字节（高位在前）
          注：新解释，
          停车场标识的第一字节，标识纸张状态。
          当Data[0]=0时，指在联网并正常情况;Data[0]=1时，表示联网但是小票机缺纸；Data[0]=2时，表示脱机下正常使用；Data[0]=3时，表示脱机但是小票机缺纸或少纸。
          停车场标识的第二字节，标识车辆类型。
          当Data[1]=0时，表示摩托车;Data[1]=1时，表示小汽车。
         * *************************************************************************/









    }
}
