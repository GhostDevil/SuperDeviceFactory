using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// CHD689考勤机API
    /// </summary>
    public static class CHD689
    {


        /******************************************************************************************
         * 调用说明
            1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
            2．	执行LinkOn校验设备密码，确认设备通讯权限；
            3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
            4．	执行LinkOff取消设备通讯权限；
            5．	当程序关闭时使用ClosePort关闭端口。

         * ***************************************************************************************/


        /// <summary>
        /// 设备验证密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szSysPwd">设备系统密码 4个('0'..'9','A'..'F')字符</param>
        /// <param name="szKeyPwd">设备键盘密码 6个('0'..'9','A'..'F')字符</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "LinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int LinkOn(uint nPortIndex, uint nNetID, String szSysPwd, String szKeyPwd);

        



        /// <summary>
        /// 关闭设备密码验证
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "LinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int LinkOff(uint nPortIndex, uint nNetID);



        /// <summary>
        /// 设置设备新密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szNewSysPwd">新设备系统密码 4个(0-9）字符</param>
        /// <param name="szNewKeyPwd">新设备键盘密码 6个(0-9）字符</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "NewPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NewPassword(uint nPortIndex, uint nNetID, StringBuilder szNewSysPwd, StringBuilder szNewKeyPwd);


        




        /// <summary>
        /// 设置考勤机日期时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">设置给设备的日期时间:年、月、日、时、分、秒、星期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 设置考勤时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">时段表索引值(1->8)</param>
        ///       /// 室外灯光常开时间段(长度为48的ANSI字符串);
        ///共6个时段;每一段为开始时间到结束时间 HHmmhhmm
        ///例如:08:30->11:20,12:00->14:30,15:30->19:00,19:30->21:30,22:00->23:00,23:30->23:59
        ///字符串:"083011201200143015301900193021302200230023302359"( ASCII的字符串 )
        ///<param name="szPeriod">
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetPeriod", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetPeriod(uint nPortIndex, uint nNetID, uint nIndex, string szPeriod);





        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">卡号</param>
        /// <param name="szUserID">ID号</param>
        /// <param name="szUserName">用户名</param>
        /// <param name="szUserNo">用户编号(工号)</param>
        /// <param name="szPassword">密码</param>
        /// <param name="nShift">班次</param>
        /// <param name="lpLmtDate">有效期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtAddUserWithName", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtAddUserWithName(uint nPortIndex, uint nNetID, string szCardNo, string szUserID, string szUserName, string szUserNo, string szPassword, uint nShift, ref SYSTEMTIME lpLmtDate);






        /// <summary>
        /// 读取指定位置的用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPoint">用户储存位置</param>
        /// <param name="szCardNo">返回的用户卡号</param>
        /// <param name="szUserID">返回的ID号</param>
        /// <param name="szPassword">返回的密码</param>
        /// <param name="pnShift">返回的班次</param>
        /// <param name="lpLmtDate">返回的有效期</param>
        /// <returns>返回值:	设备返回值(0XE5表示没有用户)</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadUserByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadUserByPoint(uint nPortIndex, uint nNetID, uint nPoint, byte[] szCardNo, byte[] szUserID, StringBuilder szPassword, out uint pnShift, out SYSTEMTIME lpLmtDate);







        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">卡号	</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtDelUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtDelUserByCardNo(uint nPortIndex, uint nNetID, string szCardNo);





        /// <summary>
        /// 删除所有用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtDelAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtDelAllUser(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设置设备参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nConfig">
        /// 配置字节
        ///D7: =1:取原始卡号16进制4字节做为卡号
        ///    =0:D6位有效
        ///D6: =0:用原始卡号16进制4字节换算成十进制取8位数做新卡号
        ///	   =1:用原始卡号16进制4字节换算成十进制取8位数做新卡号
        ///D5: =0:在考勤机上显示卡号
        ///	   =1:显示员工工号
        ///D4: =0:考勤员工卡号用3字节表示
        ///	   =1:用4字节表示
        ///D3: 保留
        ///D2: =0:检查考勤员工表
        ///	   =1:不检查
        ///D1: 保留
        ///D0: =0:不检查考勤班组
        ///	   =1:检查考勤班组
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetCtrlParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetCtrlParam(uint nPortIndex, uint nNetID, uint nConfig);





        /// <summary>
        /// 设置考勤钟确认继电器执行时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelay">继电器执行时间(单位:0.1秒)	</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetClockOpenTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetClockOpenTime(uint nPortIndex, uint nNetID, uint nDelay);





        /// <summary>
        /// 设置考勤钟允许重复打卡时间间隔
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelay">继电器执行时间(单位:分钟)例如:nDelay=10,表示在10分钟内重复刷卡被拒绝	</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetClockRepeatDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetClockRepeatDelay(uint nPortIndex, uint nNetID, uint nDelay);





        /// <summary>
        /// 设置考勤钟报时继电器输出时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelay">继电器执行时间(单位:0.1秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetClockAlarmDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetClockAlarmDelay(uint nPortIndex, uint nNetID, uint nDelay);





        /// <summary>
        /// 设置考勤钟报警(报时)时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">报警时间序号(1->32)考勤机允许设置32个报警时间</param>
        /// <param name="nAlarmHour">报警时间(时)</param>
        /// <param name="nAlarmMin">报警时间(分)</param>
        /// 备  注:  更改设置时应该先清除设置, nIndex=0x55时为清除设置
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetClockAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetClockAlarm(uint nPortIndex, uint nNetID, uint nIndex, uint nAlarmHour, uint nAlarmMin);






        /// <summary>
        /// 控制开门继电器
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">=1:打开开门继电器=0:关闭开门继电器</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetOpenDoor(uint nPortIndex, uint nNetID, uint nFlag);





        /// <summary>
        /// 读取考勤机的实时钟
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pDateTime">返回设置当前日期和时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pDateTime);





        /// <summary>
        /// 读取考勤时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">时段表索引值(1->8)</param>
        /// <param name="szPeriod">
        ///       /// 室外灯光常开时间段(长度为48的ANSI字符串);
        ///共6个时段;每一段为开始时间到结束时间 HHmmhhmm
        ///例如:08:30->11:20,12:00->14:30,15:30->19:00,19:30->21:30,22:00->23:00,23:30->23:59
        ///字符串:"083011201200143015301900193021302200230023302359"( ASCII的字符串 )
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadPeriod", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadPeriod(uint nPortIndex, uint nNetID, uint nIndex, StringBuilder szPeriod);





        /// <summary>
        /// 读取考勤钟报警(报时)时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">报警时间序号(1->32)</param>
        /// <param name="pnAlarmHour">报警时间(时)</param>
        /// <param name="pnAlarmMin">报警时间(分)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadClockAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadClockAlarm(uint nPortIndex, uint nNetID, uint nIndex, ref uint pnAlarmHour, ref uint pnAlarmMin);






        /// <summary>
        /// 读取已存在的用户数目
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnUserCount">用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadUserCount(uint nPortIndex, uint nNetID, out uint pnUserCount);





        /// <summary>
        /// 读取指定卡的用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">用户卡号</param>
        /// <param name="szUserID">返回的ID号</param>
        /// <param name="szPassword">返回的密码</param>
        /// <param name="pnShift">返回的班次</param>
        /// <param name="lpLmtDate">返回的有效期</param>
        /// <returns>返回值:	设备返回值(0XE5表示没有用户)</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadUserByCardNo(uint nPortIndex, uint nNetID, String szCardNo, StringBuilder szUserID, StringBuilder szPassword, out uint pnShift, out SYSTEMTIME lpLmtDate);






        /// <summary>
        /// 读取指定ID的用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">用户ID号</param>
        /// <param name="szCardNo">返回的用户卡号</param>
        /// <param name="szPassword">返回的密码</param>
        /// <param name="pnShift">返回的班次</param>
        /// <param name="lpLmtDate">返回的有效期</param>
        /// <returns>返回值:	设备返回值(0XE5表示没有用户)</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadUserByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadUserByUserID(uint nPortIndex, uint nNetID, String szUserID, StringBuilder szCardNo, StringBuilder szPassword, out uint pnShift, out SYSTEMTIME lpLmtDate);






        /// <summary>
        /// 读取设备参数配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnConfig">
        /// 配置字节
        ///D7: =1:取原始卡号16进制4字节做为卡号
        ///	=0:D6位有效
        ///D6: =0:用原始卡号16进制4字节换算成十进制取8位数做新卡号
        ///	=1:用原始卡号16进制4字节换算成十进制取8位数做新卡号
        ///D5: =0:在考勤机上显示卡号
        ///	=1:显示员工工号
        ///D4: =0:考勤员工卡号用3字节表示
        ///	=1:用4字节表示
        ///D3: 保留
        ///D2: =0:检查考勤员工表
        ///	=1:不检查
        ///D1: 保留
        ///D0: =0:不检查考勤班组
        ///	=1:检查考勤班组
        /// </param>
        /// <param name="pnRelayDelay">继电器执行时间(单位:0.1秒)</param>
        /// <param name="pnRepeatDelay">允许重复打卡时间(单位:分钟)</param>
        /// <param name="pnAlarmDelay">报警继电器输出时间 (单位:0.1秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadCtrlParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadCtrlParam(uint nPortIndex, uint nNetID, ref uint pnConfig, ref uint pnRelayDelay, ref uint pnRepeatDelay, ref uint pnAlarmDelay);






        /// <summary>
        /// 读取设备版本信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回的设置版本字符串(18个字节)	</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadVersion(uint nPortIndex, uint nNetID, byte[] szVersion);





        /// <summary>
        /// 读取最后一次刷卡卡号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnResult">查询结果pnResult指向的值为零:设备没有记录到卡号pnResult指向的值不为零时:szCardNo返回卡号</param>
        /// <param name="szCardNo">返回的卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadCardNo(uint nPortIndex, uint nNetID, out uint pnResult, byte[] szCardNo);




        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadCardNo1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadCardNo1(uint nPortIndex, uint nNetID,  byte[] szCardNo,out SYSTEMTIME tm);




        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtInitRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtInitRec(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 读取SM的历史记录柜桶参数信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">桶底BOTTOM(2字节)；</param>
        /// <param name="pnSaveP">下一次新记录存放指针 SAVEP(2字节)；</param>
        /// <param name="pnLoadP">下一次读取记录位置指针 LOADP(2字节)</param>
        /// <param name="pnMF">SM已修改LOADP标志MF (1字节;D0=0 未修改)</param>
        /// <param name="pnMaxLen">柜桶最大深度MAXLEN(2字节);</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecInfo(uint nPortIndex, uint nNetID, out uint pnBottom, out uint pnSaveP, out uint pnLoadP, out uint pnMF, out uint pnMaxLen);






        /// <summary>
        /// 设置记录读取指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoadP">读记录指针</param>
        /// <param name="nMF">
        /// 记录标识
        ///D7=0: 记录区空间足够,正常保存记录。
        ///  =1: 记录区已满，将再次从BOTTOM开始保存记录。
        ///D0=0: 当前新记录没有覆盖了未读取的记录(即:SAVEP小于LOADP)
        ///  =1: 当前新记录覆盖了未读取的记录(即:SAVEP大于LOADP)
        ///其他BIT位保留,默认等于“0”。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetRecReadPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetRecReadPoint(uint nPortIndex, uint nNetID, uint nLoadP, uint nMF);





        /// <summary>
        /// 读取设备记录区信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如银行卡号或者ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">保留(NULL)</param>
        /// <param name="pnRecRemark">保留(NULL)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadOneRec(uint nPortIndex, uint nNetID, byte[] szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecState, ref uint pnRecRemark);






        /// <summary>
        /// 随机读取设备记录区信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPos">记录位置</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如银行卡号或者ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">保留(NULL)</param>
        /// <param name="pnRecRemark">保留(NULL)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecByPoint(uint nPortIndex, uint nNetID, uint nPos, StringBuilder szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecState, ref uint pnRecRemark);






        /// <summary>
        /// 读取设备记录并且返回记录位置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPos">返回记录位置</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如银行卡号或者ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">保留(NULL)</param>
        /// <param name="pnRecRemark">保留(NULL)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecWithPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecWithPoint(uint nPortIndex, uint nNetID, ref uint nPos, byte[] szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecState, ref uint pnRecRemark);






        /// <summary>
        /// 启动应答方式读记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecStart", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecStart(uint nPortIndex, uint nNetID);





        /// <summary>
        ///  应答方式读取历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRecType">
        /// =0 SM上一次读取给上位机的记录已被正确受到, SM自动调整LOADP后读取下一条记录返回。
        /// =1 SM上一次读取给上位机SU的记录未被SU正确受到,或SU要求SM重复读取原LOADP处的记录。
        /// </param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="lpTime">保留(NULL)</param>
        /// <param name="pnRecState">保留(NULL)</param>
        /// <param name="pnRecRemark"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecAck", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecAck(uint nPortIndex, uint nNetID, uint nRecType, StringBuilder szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecState, ref uint pnRecRemark);






        /// <summary>
        /// 停止应答方式读记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtReadRecStop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtReadRecStop(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设置的提示信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelay">显示的延时时间(单位:秒)</param>
        /// <param name="szHintText">设置的提示信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AtSetHintNow", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AtSetHintNow(uint nPortIndex, uint nNetID, uint nDelay, String szHintText);







        /***********************************************************************************
         *附录一：设备返回值（SM 应答 或返回值含义）
             RTN=00H  SM 正常执行 SU 发来的命令。
             RTN=01H  VER 不符，命令未执行。
             RTN=02H  SM接收的命令帧，累加和检查不对。
             RTN=03H  SM接收的命令帧，参数部份的累加和检查不对。
             RTN=04H  无效的CID2 。
             RTN=05H  不能识别的命令格式。
             RTN=06H  SM接收的命令帧，数据信息部份有无效数据。
             RTN=07H  SU无设置SM（或访问SM的重要信息）之权限。
             RTN=E0H  SU 与SM 之间的密码确认不正确。
             RTN=E1H,  SU对SM 内部密码的修改不成功。	
             RTN=E2H，SM内部相应设置项存储空间已满。	 
             RTN=E3H，SU对SM内部参数的修改（或删除）不成功。 
             RTN=E4H，SM内部相应设置项的存储空间已空。
             RTN=E5H，SM内部无相应信息项。
             RTN=E6H，SU重复设入SM相同ID的用户，SM保持原用户不变。
             RTN=E7H，SU重复设入相同卡号的RF卡，给不同的用户。
             RTN=E8H，SU重复设入完全相同的用户。
             RTN=-1， 参数错误。
             RTN=-2， 未收到设备返回数据，比如：超时。
             RTN=-3， 设备返回的参数中有无效数据或参数长度错误(校验不通过)
             RTN=-4， 读取数据过程中发生其他错误(需要关闭端口，在重新连接)
         ***********************************************************************************/

    }
}
