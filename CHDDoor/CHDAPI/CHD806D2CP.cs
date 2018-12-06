using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHD806D2CP
    {



        /***************************************************************
       * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
         2．	执行LinkOn校验设备密码，确认设备通讯权限；
         3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
         4．	执行LinkOff取消设备通讯权限；
         5．	当程序关闭时使用ClosePort关闭端口。
       **************************************************************/





        /// <summary>
        /// 访问权限的确认
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szSysPwd">2字节系统密码(‘0’~’9’,’A’~’F’)</param>
        /// <param name="szKeyPwd">3字节设备设置密码(‘0’~’9’,’A’~’F’)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DLinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLinkOn(uint nPortIndex, uint nNetID, String szSysPwd, String szKeyPwd);





        /// <summary>
        /// 取消访问权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DLinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLinkOff(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 修改访问密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szNewSysPwd">2字节系统密码 4位(‘0’~’9’,’A’~’F’)</param>
        /// <param name="szNewKeyPwd">3字节设备设置密码 6位(‘0’~’9’,’A’~’F’)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DNewPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DNewPwd(uint nPortIndex, uint nNetID, String szNewSysPwd, String szNewKeyPwd);





        /// <summary>
        /// 日期时间同步命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">日期。世纪,年，月，日，星期，时，分，秒</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pTime);





        /// <summary>
        /// 时段列表
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">表序号0--31</param>
        /// <param name="szTime">时间。HH：MM-HH：MM，HH：MM-HH：MM，HH：MM-HH：MM，HH：MM-HH：MM</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetListTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetListTime(uint nPortIndex, uint nNetID, uint nIndex, StringBuilder szTime);



        /// <summary>
        /// 按卡号读取用户信息
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="szReadCardNo">需要读取的卡号的字符串</param>
        /// <param name="szCardNum">返回卡号   10字节('0'-'9','A'-'F')字符串</param>
        /// <param name="szUserID">返回用户ID 8字节('0'-'9')字符串</param>
        /// <param name="szPasswd">返回密码   4字节('0'-'9')字符串</param>
        /// <param name="pTime">返回用户有效期</param>
        /// <param name="pnDoorRight">
        /// 返回第一门用户权限 =0 无权限
        /// D7,D6 =0,1:一般用户（受星期准进时段限制）
        /// D1-D0(0-3) 的值对应一般用户的第类卡
        /// D7,D6 =1,1:特权用户（不受任何准进时段限制），但受有效期等限制。
        /// </param>
        /// <param name="nCount">门的数量(如CHD806D2CP是双门控制器,该值就是"2")</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserInfoByCardNoEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserInfoByCardNoEx(uint nPortIndex, uint nNetID, string szReadCardNo, StringBuilder szCardNum, StringBuilder szUserID, StringBuilder szPasswd, out SYSTEMTIME pTime, out uint pnDoorRight, uint nCount);





        /// <summary>
        /// 按星期管理的一般时间段表索引设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号。1,2,0FFH</param>
        /// <param name="szTime">一星期7天共56字节的索引表.查看附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetWeekTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetWeekTime(uint nPortIndex, uint nNetID, uint nDoorID, byte[] szTime);





        /// <summary>
        /// 设置特殊日期（如节假日）时间段管理表索引
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号=1,2,0FFH</param>
        /// <param name="nLmtMonth">月.1～12</param>
        /// <param name="nLmtDay">日期.1～31</param>
        /// <param name="szTime">8字节的管理时段索引表。查看附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetHolidayTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetHolidayTime(uint nPortIndex, uint nNetID, uint nDoorID, int nLmtMonth, int nLmtDay, byte[] szTime);





        /// <summary>
        /// 授权一个用户卡（至门控制器）
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">5字节卡号 10位。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="szUserID">4字节编号 8位。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="szUserPwd">2字节密码 4位 。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="pTime">日期。4字节。格式：YYYY，MM，DD</param>
        /// <param name="pnDoorRight">门1权限</param>
        /// <param name="nCount">门2权限</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DAddUserEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DAddUserEx(uint nPortIndex, uint nNetID, String szCardNo, String szUserID, String szUserPwd, ref SYSTEMTIME pTime, ref uint pnDoorRight, uint nCount);





        /// <summary>
        /// 取消用户卡权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDelUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDelUserByCardNo(uint nPortIndex, uint nNetID, string szCardNo);





        /// <summary>
        /// 全部删除用户（从门控制器内）
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDelAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDelAllUser(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 门控器控制参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChildID">门号1字节. =1表示仅第1门；=2表示仅第2门；=0FFH双门同时设置。</param>
        /// <param name="nCtrlParam">控制参数，参考附录2</param>
        /// <param name="nOpenDelay">控制参数，参考附录2</param>
        /// <param name="nIrSureDelay">控制参数，参考附录2</param>
        /// <param name="nRelayDelay">控制参数，参考附录2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetOneParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetOneParam(uint nPortIndex, uint nNetID, uint nChildID, uint nCtrlParam, uint nOpenDelay, uint nIrSureDelay, uint nRelayDelay);





        /// <summary>
        /// 报警延时时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelay">报警源结束后继续报警的延时1字节（秒）. 1—255秒</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetAlarmDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetAlarmDelay(uint nPortIndex, uint nNetID, uint nDelay);





        /// <summary>
        /// 初始化记录区(清空记录)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DInitRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DInitRec(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设定读指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoadP">LoadP。1～65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetRecReadPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetRecReadPoint(uint nPortIndex, uint nNetID, uint nLoadP);





        /// <summary>
        /// 远程放行(不带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">=1开门1;=2开门2;=0FFH 两门全开;=0不动作</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DRemoteOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DRemoteOpenDoor(uint nPortIndex, uint nNetID, uint nDoorID);





        /// <summary>
        /// 远程放行(带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">=1开门1;=2开门2;=0FFH 两门全开;=0不动作</param>
        /// <param name="szUser">操作员编号信息.(‘0’~’9’, ‘A’~ ‘F’).5个字节，分高低位</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DRemoteOpenDoorWithUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DRemoteOpenDoorWithUser(uint nPortIndex, uint nNetID, uint nDoorID, string szUser);





        /// <summary>
        /// 远程驱动报警(继电器)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayID">  =1驱动第1报警继电器 =2	驱动第2报警继电器 =0FFH 	驱动第1和第2报警继电器 =0 	不操作</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DOpenAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DOpenAlarm(uint nPortIndex, uint nNetID, uint nDelayID);





        /// <summary>
        /// 远程关闭报警(继电器)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayID">=1	关闭第1报警继电器=2	关闭第2报警继电器=0FFH 	关闭第1和第2报警继电器=0 	不操作</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DCloseAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DCloseAlarm(uint nPortIndex, uint nNetID, uint nDelayID);





        /// <summary>
        /// 通信速率设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nBaudrate">=0---7/依次对应:1200/2400/4800/9600/19200/38400/76800/153600</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetBaudrate(uint nPortIndex, uint nNetID, uint nBaudrate);





        /// <summary>
        /// 胁迫状态复位
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DMenaceClose", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DMenaceClose(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设置紧急开门密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDoor1Pwd1">第1门第1密码，8个数字（4字节）</param>
        /// <param name="szDoor1Pwd2">第1门第2密码，8个数字（4字节）</param>
        /// <param name="szDoor2Pwd1">第2门第1密码，8个数字（4字节）</param>
        /// <param name="szDoor2Pwd2">第2门第2密码，8个数字（4字节）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetSuperPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetSuperPwd(uint nPortIndex, uint nNetID, String szDoor1Pwd1, String szDoor1Pwd2, String szDoor2Pwd1, String szDoor2Pwd2);





        /// <summary>
        /// 远程常闭门与解除（带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">=1操作门1 =2操作门2 =0FFH 操作门1和2  =0不操作</param>
        /// <param name="nDelay">该2字节不等于0时：常闭门的延时时间（单位分钟） 该2字节全=0时：解除门的常闭</param>
        /// <param name="szUser">操作员编号信息。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DAlwaysCloseDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DAlwaysCloseDoor(uint nPortIndex, uint nNetID, uint nDoorID, uint nDelay, string szUser);





        /// <summary>
        /// 远程常开门与解除(带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">=1操作门1  =2操作门2  =0FFH 操作门1和2  =0不操作</param>
        /// <param name="nDelay">该2字节不等于0时，常开门的延时时间（单位分钟），该2字节全=0时：解除门的常开</param>
        /// <param name="szUser">操作员编号信息。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DAlwaysOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DAlwaysOpenDoor(uint nPortIndex, uint nNetID, uint nDoorID, uint nDelay, string szUser);





        /// <summary>
        /// 产生超级权限卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCard1">第1张卡号。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="szCard2">第2张卡号。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="szCard3">第3张卡号。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <param name="szCard4">第4张卡号。5字节卡号。（‘0’～‘9’，‘A’～‘F’）。分高低字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetSuperCardEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetSuperCardEx(uint nPortIndex, uint nNetID, string szCard1, string szCard2, string szCard3, string szCard4);





        /// <summary>
        /// 删除超级权限卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">必须为0</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDeleteSuperCardEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDeleteSuperCardEx(uint nPortIndex, uint nNetID, uint nDoorID);





        /// <summary>
        /// 设置DCU主动上传的IP通道
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChannel">通道号。0，1，2，3…</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetTransmitChannel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetTransmitChannel(uint nPortIndex, uint nNetID, uint nChannel);





        /// <summary>
        /// 读取实时钟
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pTime);





        /// <summary>
        /// 读取记录参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">返回BOTTOM</param>
        /// <param name="pnSaveP">返回SAVEP</param>
        /// <param name="pnLoadP">返回LOADP</param>
        /// <param name="pnMaxLen">返回MAXLEN</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecInfo(uint nPortIndex, uint nNetID, out int pnBottom, out int pnSaveP, out int pnLoadP, out int pnMaxLen);





        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">事件来源</param>
        /// <param name="pTime">事件时间</param>
        /// <param name="pnRecWorkState">工作状态</param>
        /// <param name="pnRecRemark">备注</param>
        /// <param name="pnRecLineState">线路状态</param>
        /// <param name="pnRecDoorID">门号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadOneRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, ref SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);





        /// <summary>
        /// 顺序读取一条历史记录(带存储位置)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnRecPoint">返回LOADP</param>
        /// <param name="szRecSource">事件来源</param>
        /// <param name="pTime">事件时间</param>
        /// <param name="pnRecWorkState">工作状态</param>
        /// <param name="pnRecRemark">备注</param>
        /// <param name="pnRecLineState">线路状态</param>
        /// <param name="pnRecDoorID">门号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecWithPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecWithPoint(uint nPortIndex, uint nNetID, out int pnRecPoint, StringBuilder szRecSource, ref SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);





        /// <summary>
        /// 随机读取记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nReadPoint">信息存储位置。0～65535</param>
        /// <param name="szRecSource">参考附录3</param>
        /// <param name="pTime">参考附录3</param>
        /// <param name="pnRecWorkState">参考附录3</param>
        /// <param name="pnRecRemark">参考附录3</param>
        /// <param name="pnRecLineState">参考附录3</param>
        /// <param name="pnRecDoorID">参考附录3</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecByPoint(uint nPortIndex, uint nNetID, Int16 nReadPoint, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);





        /// <summary>
        /// 查询最新事件记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">参考附录3</param>
        /// <param name="pTime">参考附录3</param>
        /// <param name="pnRecWorkState">参考附录3</param>
        /// <param name="pnRecRemark">参考附录3</param>
        /// <param name="pnRecLineState">参考附录3</param>
        /// <param name="pnRecDoorID">参考附录3</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneNewRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadOneNewRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);






        /// <summary>
        /// 读取一张时段表
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">表序号，1字节=0--31</param>
        /// <param name="szTime"> HH：MM (起始)   HH:MM(结束)，HH：MM (起始)   HH:MM(结束) HH：MM (起始)   HH:MM(结束)，HH：MM (起始)   HH:MM(结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadListTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadListTime(uint nPortIndex, uint nNetID, uint nIndex, StringBuilder szTime);





        /// <summary>
        /// 读取星期一至星期日的时段表(一般正常时段)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">= 1读取第1门；=2读取第2门；其他值无效；</param>
        /// <param name="nWeekID">= 1—7：指定读取星期一（=1）、或星期二（=2）。。。星期天（=7）；</param>
        /// <param name="szTime">返回8字节时段索引表</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadWeekTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadWeekTime(uint nPortIndex, uint nNetID, uint nDoorID, uint nWeekID, byte[] szTime);





        /// <summary>
        /// 读取特殊日期的时段表(例如节假日)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">= 1读取第1门；=2读取第2门；其他值无效；</param>
        /// <param name="nIndex">1—44组</param>
        /// <param name="pnLmtMonth">返回月</param>
        /// <param name="pnLmtDay">返回日期</param>
        /// <param name="szTime">返回8字节时段索引表</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadHolidayTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadHolidayTime(uint nPortIndex, uint nNetID, uint nDoorID, uint nIndex, out int pnLmtMonth, out int pnLmtDay, byte[] szTime);





        /// <summary>
        /// 读取已授权的用户数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="pnUserNum">返回用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserCount(uint nPortIndex, uint nNetID, out int pnUserNum);





        /// <summary>
        /// 读取指定存储位置的用户登记资料
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nUserPos">用户存储在设备内存中的位置</param>
        /// <param name="szCardNum">返回用户卡号</param>
        /// <param name="szUserID">返回用户ID</param>
        /// <param name="szPasswd">返回用户密码</param>
        /// <param name="pTime">返回时间</param>
        /// <param name="pnDoor1Right">返回权限1</param>
        /// <param name="pnDoor2Right">返回权限2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserInfoByPos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserInfoByPos(uint nPortIndex, uint nNetID, uint nUserPos, byte[] szCardNum, byte[] szUserID, byte[]  szPasswd, out SYSTEMTIME pTime, out int pnDoor1Right, out int pnDoor2Right);




        /// <summary>
        /// 读取工作参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号。1，2，3，4，0xFF</param>
        /// <param name="pnCtrl1Param">返回控制字#1</param>
        /// <param name="pnRelayDelay">返回门锁动作延时</param>
        /// <param name="pnOpenDelay">返回开门等待进入延时</param>
        /// <param name="pnIrSureDelay">返回红外报警确认延时</param>
        /// <param name="pnIrOnDelay">返回布防开启延时</param>
        /// <param name="pnCtrl2Param">返回控制字#2</param>
        /// <param name="pnCtrl3Param">返回控制字#3</param>
        /// <param name="pnCtrl4Param">返回控制字#4</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadCtrlParamEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadCtrlParamEx(uint nPortIndex, uint nNetID, int nDoorID, out int pnCtrl1Param, out int pnRelayDelay, out int pnOpenDelay, out int pnIrSureDelay, out int pnIrOnDelay, out int pnCtrl2Param, out int pnCtrl3Param, out int pnCtrl4Param);




        /// <summary>
        /// 设置门禁控制器参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号。1，2，3，4，0xFF</param>
        /// <param name="nCtrl1Param">参看附录2</param>
        /// <param name="nRelayDelay">参看附录2</param>
        /// <param name="nOpenDelay">参看附录2</param>
        /// <param name="nIrSureDelay">参看附录2</param>
        /// <param name="nIrOnDelay">参看附录2</param>
        /// <param name="nCtrl2Param">参看附录2</param>
        /// <param name="nCtrl3Param">参看附录2</param>
        /// <param name="nCtrl4Param">参看附录2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetCtrlParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetCtrlParam(uint nPortIndex, uint nNetID, uint nDoorID, uint nCtrl1Param, uint nRelayDelay, uint nOpenDelay, uint nIrSureDelay, uint nIrOnDelay, uint nCtrl2Param, uint nCtrl3Param, uint nCtrl4Param);





        /// <summary>
        /// 查询指定ID的用户是否存在
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szReadCardNo">用户卡号</param>
        /// <param name="szCardNum">返回用户卡号</param>
        /// <param name="szUserID">返回用户ID</param>
        /// <param name="szPasswd">返回用户密码</param>
        /// <param name="pTime">返回时间</param>
        /// <param name="pnDoor1Right">返回权限1</param>
        /// <param name="pnDoor2Right">返回权限2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserInfoByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserInfoByUserID(uint nPortIndex, uint nNetID, string szReadUserID, byte[] szCardNum, byte[] szUserID, byte[] szPasswd, out SYSTEMTIME pTime, out int pnDoor1Right, out int pnDoor2Right);





        /// <summary>
        /// 查询指定卡号的用户是否存在
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szReadCardNo">用户卡号</param>
        /// <param name="szCardNum">返回用户卡号</param>
        /// <param name="szUserID">返回用户ID</param>
        /// <param name="szPasswd">返回用户密码</param>
        /// <param name="Time">返回时间</param>
        /// <param name="pnDoor1Right">返回权限1</param>
        /// <param name="pnDoor2Right">返回权限2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserInfoByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserInfoByCardNo(uint nPortIndex, uint nNetID, string szReadCardNo, byte[] szCardNum, byte[] szUserID, byte[] szPasswd, out SYSTEMTIME Time, out int pnDoor1Right, out int pnDoor2Right);





        /// <summary>
        /// 读取工作参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWorkState1">返回SM的门1工作状态</param>
        /// <param name="pnLineState1">返回门1的线路状态</param>
        /// <param name="pnWorkState2">返回SM的门2工作状态</param>
        /// <param name="pnLineState2">返回门2的线路状态</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDoorState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDoorState(uint nPortIndex, uint nNetID, out int pnWorkState1, out int pnLineState1, out int pnWorkState2, out int pnLineState2);





        /// <summary>
        /// 读取设备名称及版本号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);





        /// <summary>
        /// 读取紧急开门的密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="RszDoor1Pwd1">第1门第1密码。8个数字（4字节）</param>
        /// <param name="szDoor1Pwd2">第1门第2密码。8个数字（4字节）</param>
        /// <param name="szDoor2Pwd1">第2门第1密码。8个数字（4字节）</param>
        /// <param name="szDoor2Pwd2">第2门第2密码。8个数字（4字节）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadSuperPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadSuperPwd(uint nPortIndex, uint nNetID, byte[] RszDoor1Pwd1, byte[] szDoor1Pwd2, byte[] szDoor2Pwd1, byte[] szDoor2Pwd2);





        /// <summary>
        /// 读取常闭门/常开门的剩余时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID"></param>
        /// <param name="pnState1">返回第1门的状态，=0正常</param>
        /// <param name="pnDelay1">返回分钟</param>
        /// <param name="pnState2">返回第2门的状态，=0正常</param>
        /// <param name="pnDelay2">返回分钟</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadAlwaysState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadAlwaysState(uint nPortIndex, uint nNetID, uint nDoorID, out uint pnState1, out uint pnDelay1, out uint pnState2, out uint pnDelay2);





        /// <summary>
        /// 读取DCU的超级权限卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCard1">返回超级权限卡</param>
        /// <param name="szCard2">返回超级权限卡</param>
        /// <param name="szCard3">返回超级权限卡</param>
        /// <param name="szCard4">返回超级权限卡</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadSuperCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadSuperCard(uint nPortIndex, uint nNetID, StringBuilder szCard1, StringBuilder szCard2, StringBuilder szCard3, StringBuilder szCard4);





        /// <summary>
        /// 读取DCU的主动上传通道配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnChannel">返回信息，第1字节是网络IP的子通道号</param>
        /// <param name="pnHost">返回信息，第2字节是请求服务的HOST标识</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadTransmitChannel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadTransmitChannel(uint nPortIndex, uint nNetID, out int pnChannel, out int pnHost);




        /**********************************************************************************
         * 附录1
 
         * 第1类卡当天准进时段索引	  第2类卡当天准进时段索引	  第3类卡当天准进时段索引	  第4类卡当天准进时段索引	  当天门常开时段索引	 当天刷卡加密码时段索引	   当天自动布防的时段索引	   N+1屏蔽时段
               =0--31	                    =0--31	                    =0--31	                      =0--31	                =0--31	                 -0--31	                  =0--31	             =0--31
         * 
         * *********************************************************************************
         * 
         * 
         * 
         * 附录2：
        
         * 门1参数8字节
             序号	   门1参数名	          意义	                    说明
             *******************************************************************************
             1	       CTRL1	              控制字#1	                D7—D0有不同意义
             2	       RELAY DELAY	          门锁动作延时	            1字节，0—25.5秒
             3	       OPEN DELAY	          开门等待进入延时	        1字节,2—255秒
             4	       IR SURE	              红外报警确认延时	        1字节,  2—255秒
             5	       IR ONDLY	              布防开启延时	            1字节, 2—255秒
             6	       CTRL2	              控制字#2	                D7—D0有不同意义
             7	       CTRL3	              控制字#3	                D7—D0有不同意义
             8	       Ctrl4	              控制字#4	                D7—D0有不同意义
         * 
         * ***********************************************************************************
         * 
         第1门 CTRL1--第1控制字
         * D7	=1从=1开始就监控门状态(不论自动布防时段是否有效亦监控); =0临时不监控,仅在布防时段有效时自动开启监控,无效时结束监控
           D6	=1从=1开始就监控红外状态(不论自动布防时段是否有效亦监控); =0临时不监控, 仅在布防时段有效时自动开启监控,无效时结束监控
           D5	=1 当D1=1且在密码时段时,第2感应头要密码确认; =0不需要
           D4	=1 当D1=1且在密码时段时,第1感应头要密码确认; =0不需要
           D3	=1表示选择的门磁感应器在开门状态时其输出是开路;  =0反之
           D2	=1表示选择的红外感应器在报警状态时其输出是开路;  =0反之
           D1	=0 在密码时段内刷卡正确无需密码;  =1按密码时段确定是否要密码
           D0	=0 紧急输入时常开门;   =1紧急输入时常闭门任何卡不能开门
         * 
         * 当紧急输入时还结合CTRL2的D7,D6,D5,D4控制位动作ALARM1,当D5或D4不为0时按CTRL1的D0控制ALARM1.
         * ***************************************************************************************

         * 第1门 CTRL2 --第2控制字
         * 	      ALARM1(继电器动作表)	                        RELAY1(继电器动作表)
             D7	  报警(门磁或其他)	=1动作, =0 不动作		
             D6	  手动出门按钮	=1动作, =0 不动作		
             D5	  第2头刷卡合法	=1动作, =0 不动作		
             D4	  第1头刷卡合法	=1动作, =0 不动作		
             D3	  无效卡或卡失效时	=1动作,=0不动作		
             D2		                                           手动按钮	=1动作, =0 NO 
             D1		                                           第2感应头刷卡合法	=1动作, =0 NO
             D0	=1 只要刷卡或按键都动作，用来与监控摄像系统同步；=0（根据D7—D3定义）
         * 
         * 注意1：D7，D3都是0，则表示报警继电器不做报警使用。当D3=0时表示无效卡刷卡时不产生错误状态到SU。但D7=0仍产生错误状态到SU，但不再驱动报警继电器。
         * *****************************************************************************************
         * 
         * 
         * 第1门 CTRL3--第3控制字
           D7	=1网络正常时由中心确认开门；=0始终本地确认；
           D6	=1 作业时段屏蔽N+1功能， =0作业时段也不屏蔽，N+1全天有效
           D5	=1 开启N+1功能：N卡刷卡后必须再加一特权卡确认才能开门；
           =0 关闭N+1功能：N刷卡后就可以开门；
           D4	=0 门不关联；=1门关联（一个门开启，另些门不能再开）
           D3	在第四控制字CTRL4--D0=0，第三控制字CTRL3—D3：=0时表示N=1单卡开门；=1时表示N=2双卡开门；当CTRL4--D0=1，而CTRL3—D3=0时：表示N=3三卡开门；
           D2	=1 门锁在“驱动开始—驱动结束”后不能自动上锁，要求人工“先开门—再关门”的动作后才能上锁；=0门锁在驱动结束后自动上锁。
           D1	=1双卡开门时要求分组；=0不分组，两张授权卡就能开门
           D0	=1在DCU有事件发生时主动向上位机发请求，=0不主动
         * 
         * *****************************************************************************************
         * 
         * 
         * D7-d4	备份
           D3	=1 支持输入ID+密码（PIN）开门； =0屏蔽
           D2	=1 首卡后只需1张卡；=0需要N+1的N张卡（N=1，2，3）
           D1	=1 在N+1时段内必须N+1确认，在此之外首次N+1确认后就无需再次 N+1确认，直至时段结束或日期翻转第二天；=0 根据设置N+1或N确认要求；
           D0	=1：N=3卡确认；=0  （由CTRL3的D5=0/1定）单卡或双卡N=1或2
         * 
         * ******************************************************************************************
         * 
         * 
         * 附录3: 门控器(SM)的历史记录格式
         * 门控器(SM)对如下事件的发生都有记录：每条记录用16字节表示：
         * 
         * 事件来源（5字节）	     日期，时间（7字节）	   工作状态（第1字节）	      备注（1字节）	   线路状态（第2字节）   	门号(1字节)
         * ***********************************************************************************************************************************
           卡号或ID号等	    世纪,年，月，日，时，分，秒	   WORK-STATUS（D7--D0）	   REMARK	        LINE-STATUS（D7--D0）	门1 =0门2 =1
         * 
         * *LINE-STATUS定义如下:
         * 
         * *D7	=0防拆开关关闭；  =1 拆开;
            D6	=0 无错误报警; =1当前正在报警状态
            D5	指纹输入
            D4	密码键盘输入
            D3	=1 当时门是开的； = 0闭合的 ;
            D2	=1 红外处于告警状态; =0正常;
            D1	=1 出门手动按钮按下;  =0 松开的
            D0	=1 紧急输入处于有效;  =0 无效的
         * 
         * 备注REMARK（1字节）：
         * 1:  REMARK=0 刷卡开门记录 
              “事件来源” = 5字节卡号
              * WORK-STATUS :
              * 
              * D7	=0 刷卡开门未确认用户个人密码；  =1 确认;
                D6	=0 原来门处于关状态；=1 开状态;
                D5	=0 在规定延时内开门进入；=1未开门进入
                D4	=0 在规定延时内进入后又关好门； =1门一直开的
                D3	=1 开门进入后,关闭了红外监控； = 0 保持原状态;
                D2	=0 正常刷卡; =1胁迫状态
                D1	=0进入刷卡（第1头）；=1 出门刷卡（第2头）；
                D0	=0刷卡时入侵监视原来是关的； =1，原来是开的。
               *     
               * 由LiNE-STATE字节的D4，D5两BIT区分指纹输入与ID+密码输入。
         * 
         * 
         * 2:  REMARK=1 键入用户ID及个人密码开门的记录
         *     “事件来源” = 1字节00, + 4字节用户ID号
              * WORK-STATUS :
              * D7	=1 确认用户个人密码;
                D6	=0 原来门处于关状态；   =1 开状态;
                D5	=0 在规定延时内开门进入；=1未开门进入
                D4	=0 在规定延时内进入后又关好门； =1门一直开的
                D3	=1 开门进入后,关闭了红外监控； = 0 保持原状态;
                D2	=0 正常状态; =1胁迫状态
                D1	=0进入第1头密码输入；=1第2头密码输入；
                D0	=0开门时入侵监视原来是关的； =1，原来是开的。
         * 
         * 
         * 3:  REMARK=2 远程(由SU)开门记录
         *     “事件来源” = 5字节全0，或上位机带来的操作员标识信息。
         *     WORK-STATUS :
         *     D7	=0;
               D6	=0 执行开门命令时原来门处于关状态；   =1 开状态;
               D5	=0 在规定延时内开门进入；=1未开门进入
               D4	=0 在规定延时内进入后又关好门； =1门一直开的
               D3	=1 开门进入后,关闭了红外监控； = 0 保持原状态;
               D2	=0 正常状态; =1胁迫状态
               D1	=0 
               D0	=0执行开门时入侵监视原来是关的； =1，原来是开的。
         * 
         * 
         * 4:  REMARK=3 手动出门记录
         *     “事件来源” = 5字节全0
         *     WORK-STATUS :
         *     D7	=0
               D6	=0 原来门处于关状态；   =1 开状态;
               D5	=0 在规定延时内开门进入；=1未开门进入
               D4	=0 在规定延时内进入后又关好门； =1门一直开的
               D3	=0 
               D2	=0
               D1	=0 
               D0	=0 
         * 
         * 
         * 5:  REMARK=5 事件(报警、或报警取消、线路状态、工作状态变化等) 记录
         *     “事件来源” = 3字节全0  +  报警源AS2（1字节）+报警源1  AS(1字节)报警源 AS2，AS1:
         *             AS2	AS1	            报警来源说明
                       =0	=0	            红外报警开始
                       =0	=1	            红外停止报警
                       =0	=2	            非正常开门
                       =0	=3	            门被关闭（非正常开门对应的关门记录）
                       =0	=7	            入侵（红外）监测被关闭
                       =0	=8	            入侵（红外）监测开启
                       =0	=9	            门碰开关监测被关闭
                       =0	=10	            门碰开关监测开启
                       =0	=30H/82H	    合法刷卡在规定延时内未开门进入
                       =0	=32H/84H	    合法进入后但在规定延时内未关好门，一直开着
                       =0	=22H	        紧急输入有效开始记录
                       =1	=22H	        紧急输入结束记录
         * 
         *       WORK-STATUS: 记录发生时的工作（或线路）状态: 
         *    D7	   保留
              D6	   保留
              D5	   保留
              D4	   保留
              D3	   门磁感应器线路状态=0表示门闭；=1门开
              D2	   入侵探测器状态=0正常；=1表示当前有入侵报警；
              D1	   出门按钮连线状态，=0按键松开；=1按键闭合；
              D0	   保留

         * 
         * 
         * 6:  REMARK=6  SM掉电再上电记录
         *     “事件来源”  = SM掉电的日期,时间：月,日,时,分,秒；后续时间信息为重新上电的日期时间。
         *     WORK-STATUS：记录发生时的输入线状态: 
         *     D7	=0 掉电；
               D6	保留
               D5	保留
               D4	保留
               D3	门磁感应器线路状态=0表示门闭；=1门开
               D2	入侵探测器状态=0正常；=1表示当前有入侵报警；
               D1	出门按钮连线状态，=0按键松开；=1按键闭合；
               D0	保留

         * 
         * 
         * 7:  REMARK=7  内部控制参数被修改的记录
         *     “事件来源” = 3字节全0  +  修改标记2(1字节)+ 修改标记1(1字节)
         *     
         *           修改标记2(1字节)	                 修改标记1(1字节)
                     D7		                              D7	=1修改了入侵探测控制
                     D6		                              D6	=0
                     D5		                              D5	=1 修改了时段设置；
                     D4		                              D4	=1 修改了实时钟；
                     D3		                              D3	=1 删除了用户资料；
                     D2	=1仅修改读取指针                  D2	=1 进行了授权新用户操作；
                     D1	=1修改了记录指针                  D1	=1 修改了门控制参数；
                     D0	=1修改报警延时	                  D0	=1 修改了SM的密码；
         * 
         *     WORK-STATUS: 记录发生时的工作状态: 
         *     D7	保留
               D6	保留
               D5	保留
               D4	保留
               D3	门磁感应器线路状态=0表示门闭；=1门开
               D2	入侵探测器状态=0正常；=1表示当前有入侵报警；
               D1	出门按钮连线状态，=0按键松开；=1按键闭合；
               D0	保留
         * 
         * 
         *8:  REMARK=8无效的用户卡刷卡记录。
         *    “事件来源” = 5字节卡号
         *    WORK_STATUS:	记录发生时的工作状态:
         *    D7-D2	保留
              D1    	=0第1头；=1第2头；
              D0    	保留
         * 
         * 
         * 9：REMARK=9  用户卡的有效期已过。
         *    “事件来源” = 5字节卡号
         *    WORK-STATUS: 记录发生时的工作状态: 
         *    D7-D2	保留
              D1  	=0第1头；=1第2头；
              D0	    保留
         * 
         * 10：REMARK=10 当前时间该用户卡无进入权限。
         *     “事件来源” = 5字节卡号
         *     WORK-STATUS:	记录发生时的工作状态:
         *     D7-D2	保留
               D1	=0第1头；=1第2头；
               D0	保留
         * 
         * 11：REMARK=15 本地加卡(授权记录)。
         *     Work_status,line-status 为0
         *     
         * 12：REMARK=16 本地删卡(撤权记录)。
         *     Work_status,line-status 为0
         *     
         * 13：REMARK=0X22 紧急输入开始记录。
         *     “事件来源” = 0，0，0，0，0
         *     WORK-STATUS:	记录发生时的工作状态:
         *     D7	保留
               D6	保留
               D5	保留
               D4	保留
               D3	门磁感应器线路状态=0表示门闭；=1门开
               D2	入侵探测器状态=0正常；=1表示当前有入侵报警；
               D1	出门按钮连线状态，=0按键松开；=1按键闭合；
               D0	=1紧急输入有效时做常开门；=0做常闭门；
         * 
         * 14：REMARK=0X22  紧急输入结束记录。
         *     “事件来源” = 0，0，0，0，1
         *     WORK-STATUS:	记录发生时的工作状态:
         *     D7	保留
               D6	保留
               D5	保留
               D4	保留
               D3	门磁感应器线路状态=0表示门闭；=1门开
               D2	入侵探测器状态=0正常；=1表示当前有入侵报警；
               D1	出门按钮连线状态，=0按键松开；=1按键闭合；
               D0	=1紧急输入有效时做常开门；=0做常闭门；

         * 
         * 15：REMARK=0X40  合法刷卡等待中心确认开门。
         *     “事件来源” = 0，结合以往记录
         *     WORK-STATUS:	记录发生时的工作状态: 保留
         *     
         * 
         * 16：REMARK=0X60  合法刷卡本地确认开门。
         *     “事件来源” =0，结合以往记录
         *     WORK-STATUS:	记录发生时的工作状态: 保留
         *     
         * 17：REMARK=0X61  合法本地输入两个紧急密码(每个密码8位数)确认开门。
         *     “事件来源” =0，结合以往记录
         *     WORK-STATUS:	记录发生时的工作状态: 保留
         *     
         * 18：REMARK=0X62  合法刷卡本地驱动开门继电器后，经判别门磁确认门已开。
         *     “事件来源” =0，结合以往记录
         *     WORK-STATUS:	记录发生时的工作状态: 保留
         *     
         * 19：REMARK=0X63  合法刷卡本地开门后，在规定的延时内门被正常关闭。
         *     “事件来源” =0，结合以往记录
         *     WORK-STATUS:	记录发生时的工作状态: 保留
         *     
         * 20:  REMARK=70H 超权限卡刷卡开门记录
         *      “事件来源” = 卡号
         *      WORK-STATUS :
         *      D7	=0 刷卡开门未确认用户个人密码；  =1 确认;
                D6	=0 原来门处于关状态；=1 开状态;
                D5	=0 在规定延时内开门进入；=1未开门进入
                D4	=0 在规定延时内进入后又关好门； =1门一直开的
                D3	=1 开门进入后,关闭了红外监控； = 0 保持原状态;
                D2	=0 正常刷卡; =1胁迫状态
                D1	=0进入刷卡（第1头）；=1 出门刷卡（第2头）；
                D0	=0刷卡时入侵监视原来是关的； =1，原来是开的。
         *     超权限卡由用户保管，特殊时候，紧急开门。
         * 
         * 21:  REMARK=71H HOST增加了1张超权限卡的记录
         *      “事件来源” =新增加的超权卡的卡号（4字节）
         *      超权限卡由用户保管，特殊时候，紧急开门。
         *      
         * 22:  REMARK=72H HOST删除了1张超权限卡的记录
         *      “事件来源” =新删除的超权卡的卡号（4字节）
         *      
         * 
         * 
         * 
         * 
         * 2009-10-10增加：
         * 20：REMARK=0X41远程常闭门或解除。
         *     “事件来源” =监控中心操作员ID
         *     ORK-STATUS: 常闭门的延时时间（单位分钟），=0表示解除
         *     
         * 21：REMARK=0X42延时时间到解除常闭门。
         *     “事件来源” =0
         *     WORK-STATUS: 记录发生时的工作状态: 保留
         *     
         * 22：REMARK=0X43远程常开门或解除。
         *     “事件来源” =监控中心操作员ID
         *     WORK-STATUS: 常开门的延时时间（单位分钟），=0表示解除
         *     
         * 23：REMARK=0X44延时时间到解除常开门。
         *     “事件来源” =0
         *     WORK-STATUS: 记录发生时的工作状态: 保留
         *     
         * *************************************************************************
         * 
         * 
         * 
         * 
         * 
         * 附录4
         * 一字节： SM的门1工作状态：
         *          D7	=0实时钟IC正常;  =1 不正常需要重新设置时间;
                    D6	=0正常，无事件请求；=1 DCU有事件要SU处理;
                    D5	=0工作电源正常； =1 不正常,电压低而CPU被平凡复位;
                    D4	保留（防拆开关）;
                    D3	=0不监视红外入侵;  =1监视;
                    D2	=0不监视门开关状态;  =1监视;
                    D1	=0门控电磁继电器关闭，=1加电驱动；
                    D0	=0正常工作，=1处于报警状态；

         * 
         * 第二字节：门1的线路状态：
         *           D7	=1紧急驱动输入； =0正常
                     D6	=0 门控正常；=1常闭门；（2009-10-10改）
                     D5	=0 门控正常；=1常开门；（2009-10-10改）
                     D4	=1胁迫；
                     D3	=1门开的; =0门闭合;  
                     D2	=1窃入红外报警；=0正常；
                     D1	=1出门放行键按下；=0松开；
                     D0	=0 
         * 
         * 第三字节： SM的门2工作状态
         *            D7	=0实时钟IC正常;  =1 不正常需要重新设置时间
                      D6	=0正常，无事件请求；=1 DCU有事件要SU处理
                      D5	=0工作电源正常； =1 不正常,电压低而CPU被平凡复位
                      D4	保留（防拆开关）
                      D3	=0不监视红外入侵;  =1监视;
                      D2	=0不监视门开关状态;  =1监视;
                      D1	=0门控电磁继电器关闭，=1加电驱动；
                      D0	=0正常工作，=1处于报警状态；
         * 
         * 第四字节：门2的线路状态：
         *           D7	=1紧急驱动输入；=0正常;
                     D6	=0 门控正常；=1常闭门；（2009-10-10改）
                     D5	=0 门控正常；=1常开门；（2009-10-10改）
                     D4	=1胁迫
                     D3	=1门开的; =0门闭合;
                     D2	=1窃入红外报警状态；=0正常；
                     D1	=1出门放行键按下；=0松开；
                     D0	=0 

         * 
         * ****************************************************************************
         * ***************************************************************************/




    }
}
