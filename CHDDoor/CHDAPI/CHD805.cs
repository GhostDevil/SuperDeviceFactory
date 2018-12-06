using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// CHD805API
    /// </summary>
    public static class CHD805
    {
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szSysPwd">设备系统密码 4个(0-9）字符</param>
        /// <param name="szKeyPwd">设备键盘密码 6个(0-9）字符</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "LinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int LinkOn(uint nPortIndex, uint nNetID, string szSysPwd, string szKeyPwd);




        /// <summary>
        /// 取消设备操作权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "LinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int LinkOff(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szNewSysPwd">新设备系统密码 4个(0-9）字符</param>
        /// <param name="szNewKeyPwd">新设备键盘密码 6个(0-9）字符</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "NewPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int NewPassword(uint nPortIndex, uint nNetID, string szNewSysPwd, string szNewKeyPwd);




        /// <summary>
        /// 设置设备日期时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpDatetime">设置的日期、时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME lpDatetime);




        /// <summary>
        /// 设备增加一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">增加的卡号(长度为10的ASCII字符串)</param>
        /// <param name="szUserID">增加的用户(长度为8的ASCII字符串)</param>
        /// <param name="szPasswd">用户的密码(长度为4的ASCII字符串)</param>
        /// <param name="lpLmtDate">用户时限(年、月、日)</param>
        /// <param name="nUserType">
        /// 用户类型
        ///D7,D6	=0,0: 一般用户（受星期准进时段限制）
        ///		=0,1: 一般用户（受星期准进时段限制） 
        ///		=1,0: 一般用户（受工作日/休息日准进时段限制）
        ///D7，D6	=1,1: 特权用户（不受任何准进时段限制），但受有效期等限制。
        ///        (1)当D7,D6=1,0时: D5,D4,D3,D2保留
        ///D1,D0: 一般用户的工作日的准进时段GROUP.No；
        ///	0,0  对应第一张表
        ///	0,1  对应第二张表
        ///	1,0  对应第三张表
        ///	1,1  对应第四张表
        ///(2)当D7,D6=0,0或0,1时， D5,D4保留
        ///	D3,D2,D1,D0: 该用户适用的星期列表的第几张(索引系统的准进列表)
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AddUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddUser(uint nPortIndex, uint nNetID, string szCardNo, string szUserID, string szPasswd, ref SYSTEMTIME lpLmtDate, uint nUserType);




        /// <summary>
        /// 设备增加一个用户(带用户名)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">加的卡号(长度为10的ASCII字符串)</param>
        /// <param name="szUserID">增加的用户(长度为8的ASCII字符串)</param>
        /// <param name="szUserName">用户姓名(长度为8的ASCII字符串)</param>
        /// <param name="szPasswd">用户的密码(长度为4的ASCII字符串)</param>
        /// <param name="lpLmtDate">用户时限</param>
        /// <param name="nUserType">
        /// 用户类型
        ///D7,D6	=0,0: 一般用户（受星期准进时段限制）
        ///		=0,1: 一般用户（受星期准进时段限制） 
        ///		=1,0: 一般用户（受工作日/休息日准进时段限制）
        ///D7，D6	=1,1: 特权用户（不受任何准进时段限制），但受有效期等限制。
        ///       (1)当D7,D6=1,0时: D5,D4,D3,D2保留
        ///D1,D0: 一般用户的工作日的准进时段GROUP.No；
        ///	0,0  对应第一张表
        ///	0,1  对应第二张表
        ///	1,0  对应第三张表
        ///	1,1  对应第四张表
        ///(2)当D7,D6=0,0或0,1时， D5,D4保留
        ///	D3,D2,D1,D0: 该用户适用的星期列表的第几张(索引系统的准进列表)
        /// </param>
        /// <returns>返回值:	设备返回值
        ///备  注:	使用设备: CHD806ACE、CHD806BCE
        ///</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AddUserWithName", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddUserWithName(uint nPortIndex, uint nNetID, StringBuilder szCardNo, StringBuilder szUserID, StringBuilder szUserName, StringBuilder szPasswd, ref SYSTEMTIME lpLmtDate, uint nUserType);




        /// <summary>
        /// 设备增加一个用户(带用户名)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">增加的卡号(长度为10的ASCII字符串)</param>
        /// <param name="szUserID">增加的用户(长度为8的ASCII字符串)</param>
        /// <param name="szUserName">用户姓名(长度为8的ASCII字符串)</param>
        /// <param name="szPasswd">用户的密码(长度为4的ASCII字符串)</param>
        /// <param name="lpLmtDate">用户时限</param>
        /// <param name="nUserType">
        /// D7,D6	=0,0: 一般用户（受星期准进时段限制）
        ///		=0,1: 一般用户（受星期准进时段限制） 
        ///		=1,0: 一般用户（受工作日/休息日准进时段限制）
        ///D7，D6	=1,1: 特权用户（不受任何准进时段限制），但受有效期等限制。
        ///       (1)当D7,D6=1,0时: D5,D4,D3,D2保留
        ///D1,D0: 一般用户的工作日的准进时段GROUP.No；
        ///	0,0  对应第一张表
        ///	0,1  对应第二张表
        ///	1,0  对应第三张表
        ///	1,1  对应第四张表
        ///(2)当D7,D6=0,0或0,1时， D5,D4保留
        ///	D3,D2,D1,D0: 该用户适用的星期列表的第几张(索引系统的准进列表)
        /// </param>
        /// <returns>
        /// 返回值:	设备返回值
        ///备  注:	使用设备: CHD806ACE、CHD806BCE
        /// </returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AddUserWithName1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddUserWithName1(uint nPortIndex, uint nNetID, StringBuilder szCardNo, StringBuilder szUserID, StringBuilder szUserName, StringBuilder szPasswd, ref SYSTEMTIME lpLmtDate, uint nUserType);




        /// <summary>
        /// 设备增加一个用户(带用户名)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">增加的卡号(长度为10的ASCII字符串)</param>
        /// <param name="szUserID">增加的用户(长度为8的ASCII字符串)</param>
        /// <param name="szUserName">用户姓名(长度为8的ASCII字符串)</param>
        /// <param name="szPasswd">用户的密码(长度为4的ASCII字符串)</param>
        /// <param name="lpLmtDate">用户时限</param>
        /// <param name="nUserType">
        /// 用户类型
        ///D7,D6	=0,0: 一般用户（受星期准进时段限制）
        ///		=0,1: 一般用户（受星期准进时段限制） 
        ///		=1,0: 一般用户（受工作日/休息日准进时段限制）
        ///D7，D6	=1,1: 特权用户（不受任何准进时段限制），但受有效期等限制。
        ///       (1)当D7,D6=1,0时: D5,D4,D3,D2保留
        ///D1,D0: 一般用户的工作日的准进时段GROUP.No；
        ///	0,0  对应第一张表
        ///	0,1  对应第二张表
        ///	1,0  对应第三张表
        ///	1,1  对应第四张表
        ///(2)当D7,D6=0,0或0,1时， D5,D4保留
        ///	D3,D2,D1,D0: 该用户适用的星期列表的第几张(索引系统的准进列表)
        /// </param>
        /// <returns>
        /// 返回值:	设备返回值
        ///备  注:	使用设备: CHD689ACE/BCE
        /// </returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AddUserWithName2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddUserWithName2(uint nPortIndex, uint nNetID, StringBuilder szCardNo, StringBuilder szUserID, StringBuilder szUserName, StringBuilder szUserNo, StringBuilder szPasswd, ref SYSTEMTIME lpLmtDate, uint nUserType);




        /// <summary>
        /// 设备按卡号删除一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">增加的卡号(长度为10的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelUserByCardNo(uint nPortIndex, uint nNetID, String szCardNo);




        /// <summary>
        /// 设备按用户ID删除一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">增加的卡号(长度为10的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelUserByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelUserByUserID(uint nPortIndex, uint nNetID, String szUserID);




        /// <summary>
        /// 设备删除所有用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelAllUser(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 将准进时段、管理时段设置成缺省值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetLmtTimeInit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetLmtTimeInit(uint nPortIndex, uint nNetID);






        // 备  注:	设置工作日准进时段: 无效
        //			设置休息日准进时段: 无效
        //			设置星期一至星期日的所有准进时段表: 无效
        //			门常开: 无效
        //			门常闭:	无效
        //			二次加密时段：无效
        //			红外、门磁监控布防时段：无效
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetLmtTimeInvalid", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetLmtTimeInvalid(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 设置门禁工作日准进时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nGroupNo">工作日组号(1-4) 表示该时段在系统内的编号</param>
        /// <param name="szTimeBCD">
        /// 时间段(长度为32的ASCII字符串)
        ///例如:	00:00->02:00,04:00->06:30,08:00->20:00,22:10->22:45
        ///表示为:	"00000200040006300800200022102245"
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWorkLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWorkLmtTime(uint nPortIndex, uint nNetID, uint nGroupNo, String szTimeBCD);




        /// <summary>
        /// 设置门禁非工作日准进时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">
        /// 时间段(长度为32的ASCII字符串)
        ///例如:	00:00->02:00,04:00->06:30,08:00->20:00,22:10->22:45
        ///表示为:	"00000200040006300800200022102245"
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetUnWorkLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetUnWorkLmtTime(uint nPortIndex, uint nNetID, String szTimeBCD);




        /// <summary>
        /// 设置门禁星期准进时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nGroupNo">工作日组号(0-15)</param>
        /// <param name="nWeek">星期(1-7)</param>
        /// <param name="szTimeBCD">
        /// 时间段(长度为48的ASCII字符串)
        ///例如00:00->02:00,04:00->06:30,08:00->12:00,13:30->17:30,18:30->22:00
        ///					表示为:	"0000020004000630080012001330173018302200"
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWeekLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWeekLmtTime(uint nPortIndex, uint nNetID, uint nGroupNo, uint nWeek, string szTimeBCD);




        /// <summary>
        /// 设置门锁继电器执行时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">继电器执行时间(1--255 单位:0.1秒)  例如: nDelayTime=50,表对门锁继电器加电5秒钟</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorRelayDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorRelayDelay(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设定开门等待进入的时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">
        /// 开门等待进入的时间(有效值20--255 单位:0.1秒)
        ///例如: nDelayTime=50,表门锁打开后5秒钟内应开门进入,然后关门;未开门进入,视门电磁锁的特性,若必须由机械 "开门--关门"动作,才能重新锁上,则报警,要求"开门--关门"。 保证门准确锁上
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorOpenDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorOpenDelay(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设定对红外报警的确认时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">红外报警的确认时间(有效值20--255 单位:0.1秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorAlarmConfirm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorAlarmConfirm(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设定安防报警驱动延时时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">安防报警的延时时间(有效值20--255 单位:0.1秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorAlarmDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorAlarmDelay(uint nPortIndex, uint nNetID, uint nDelayTime);





        // 备  注:	1,SU通过RS485联网，随时可以开启或关闭红外监控，不受该控制字的影响,也无延时;
        //			2,CHD805A/B在刷卡开门，或键盘输入用户ID号及密码开门，或远程RS485开门时，自动关闭红外监控
        //			3,CHD805A/B可由键盘开启<F4>键，或RS485远程命令开启红外监控
        /// <summary>
        /// 设定开启红外监控后的等待延时(让"开启者"有足够时间离开红外监视区，以免误报警)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">红外监控后的等待延时时间(有效值20--255 单位:0.1秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorLeaveDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorLeaveDelay(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设定红外感应器在报警时的有效电平(输出给门控器)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInfraredType">
        /// 红外感应器设置状态
        ///=0:	表示低电平有效(或继电器输出时，为吸合)
        ///=1:	表示高电平(或继电器输入时，继电器断开为有效)
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetInfraredType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetInfraredType(uint nPortIndex, uint nNetID, uint nInfraredType);




        /// <summary>
        /// 设定门开关感应器在开门状态时的有效电平(输出给门控器)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLockState">门开关感应器设置状态，
        ///=0:	表示低电平有效(或继电器输出时，为吸合)
        ///=1:	表示高电平(或继电器输入时,继电器断开为有效)
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetLockOpenType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetLockOpenType(uint nPortIndex, uint nNetID, uint nLockState);




        /// <summary>
        /// 设定门控电磁锁的(种类)特性
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLocktype">门锁设置状态，
        ///=0:	断电或加电，能自动锁上；
        ///=1:	不能自动锁上，必须由"开门再关门"的机械动作才能
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetLockType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetLockType(uint nPortIndex, uint nNetID, uint nLocktype);




        /// <summary>
        /// 设置系统支持感应卡的种类及卡片编号的获取方法
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRFDefine">
        /// 
        /// 感应卡的种类及卡片编号的获取方法
        ///D3--D0:	CHD805A/B支持维根感应头的种类
        ///	=0:	维根26
        ///	=1:	维根36
        ///	=2:	维根44
        ///	=3:	维根34
        ///D7--D4: 设定门控器从WIEGAND的BIT流中获取感应卡之编号的方式
        ///	=0: 全部BIT作为卡片编号,不足5字节,高位补0
        ///	=1: 除去校验位,取其全部BIT, 不足5字节,高位补0
        ///	=2: 除去校验位,取低位2字节, 高位补0
        ///	=3: 除去校验位,取低位3字节, 高位补0
        ///	=4: 除去校验位,取低位4字节, 高位补0
        ///	=5: 除去校验位,取低位5字节。
        ///	=其它值: 按 =0 处理
        /// </param>
        /// <returns>设备返回值</returns>
        /// // 备  注:	CHD805A版本V3.0以上已对上述维根感应头自动支持, 无需设置。
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetRFCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRFCard(uint nPortIndex, uint nNetID, uint nRFDefine);




        /// <summary>
        /// 设定系统感应卡的种类
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRFCardType">感应卡的种类   1字节(26BIT/36BIT/44BIT/64BIT 其他值无效)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetRFCardType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRFCardType(uint nPortIndex, uint nNetID, uint nRFCardType);




        /// <summary>
        /// 设定系统感应卡编号的获取方法
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRFCodeType">
        /// 感应卡的种类   1字节
        ///=0: 全部BIT作为卡片编号,不足5字节,高位补0
        ///=1: 除去校验位,取其全部BIT, 不足5字节,高位补0
        ///=2: 除去校验位,取低位2字节, 高位补0
        ///=3: 除去校验位,取低位3字节, 高位补0
        ///=4: 除去校验位,取低位4字节, 高位补0
        ///=5: 除去校验位,取低位5字节。
        ///=其它值: 按 =0 处理
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetRFCardCode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRFCardCode(uint nPortIndex, uint nNetID, uint nRFCodeType);




        /// <summary>
        /// 启用或关闭CHD805键盘 开门许可
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nEntCodeType">
        /// 是否启用键盘 开门许可
        ///=0: 不启用
        ///=1: 启用
        ///=其他值: 不启用
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetKeyEnt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetKeyEnt(uint nPortIndex, uint nNetID, uint nEntCodeType);




        /// <summary>
        /// 设置刷卡后是否需要输入密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPwdCodeType">是否启用“手动开门按钮”=0: 表示只要卡片合法,且在准进时段内就能开门，无需输入用户密码。=1: 表示必须卡片合法,且在准进时段内，要求输入用户密码，正确后，才能开门进入。</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWithPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWithPassword(uint nPortIndex, uint nNetID, uint nPwdCodeType);




        /// <summary>
        /// 启用或禁用“手动开门按钮”
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nManualCodeType">
        /// 
        /// 是否启用“手动开门按钮”
        ///=0: 禁用
        ///=1: 启用
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetManualKey", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetManualKey(uint nPortIndex, uint nNetID, uint nManualCodeType);




        /// <summary>
        /// 设置门禁每周休息日
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nWeekend1">第一个休息日
        ///=0  不休息
        ///=1-7 休息的星期
        ///值其它值，表示不休息
        ///</param>
        /// <param name="nWeekend2">第二个休息日,同上</param>
        /// <returns>设备返回值</returns>
        ///  备  注:	如星期六、星期日休息 nWeekend1=6, nWeekend2=7; 只星期日休息 nWeekend1=7，nWeekend2=0xFF
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWeekend", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWeekend(uint nPortIndex, uint nNetID, uint nWeekend1, uint nWeekend2);




        /// <summary>
        /// 增加一个节、假日到设备
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nMonth">月(1-12)</param>
        /// <param name="nDay">日(1-31)</param>
        /// 备  注:	设备内最多只能存放40个节假日
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AddHoliday", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddHoliday(uint nPortIndex, uint nNetID, uint nMonth, uint nDay);




        /// <summary>
        /// 删除一个节、假日
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nMonth">月(1-12)</param>
        /// <param name="nDay">日(1-31)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelHoliday", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelHoliday(uint nPortIndex, uint nNetID, uint nMonth, uint nDay);




        /// <summary>
        /// 删除所有节、假日
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelHoliday", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelHoliday(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szOperator">操作员 十个0-9,A-F字符.如果szOperator==NULL表示不带操作员开门</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "RemoteOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int RemoteOpenDoor(uint nPortIndex, uint nNetID, string szOperator);




        /// <summary>
        /// 远程常开门(开门后延时一段时间关门)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">延时时间(0-255 单位: 分钟)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AlwaysOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AlwaysOpenDoor(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设置常闭门
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayTime">=0:	取消常闭门; =其他值: 常闭门延时(单位:分钟)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "AlwaysCloseDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int AlwaysCloseDoor(uint nPortIndex, uint nNetID, uint nDelayTime);




        /// <summary>
        /// 设置门禁门常开时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">时间段的字符串(长度32的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetOpenLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetOpenLmtTime(uint nPortIndex, uint nNetID, String szTimeBCD);




        /// <summary>
        /// 设置门禁门常闭时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">时间段的字符串(长度32的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetCloseLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetCloseLmtTime(uint nPortIndex, uint nNetID, String szTimeBCD);




        /// <summary>
        /// 设置门磁红外监控时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">时间段的字符串(长度32的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWatchLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWatchLmtTime(uint nPortIndex, uint nNetID, string szTimeBCD);




        /// <summary>
        /// 设置门禁刷卡需要密码开门的时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">时间段的BCD码字符串32字节</param>
        /// 备  注:	如果二次密码确认时段开启，那么"星期内休息日、节假日"刷卡必须输入用户个人密码。
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetPwdLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPwdLmtTime(uint nPortIndex, uint nNetID, String szTimeBCD);




        /// <summary>
        /// 设置红外监控的布防与撤防
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInfraredCodeType">是否启用“手动开门按钮”
        ///=0: 关闭监视
        ///=1: 打开监视
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWatchInfrared", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWatchInfrared(uint nPortIndex, uint nNetID, uint nInfraredCodeType);




        /// <summary>
        /// 设定是否监视门开关感应器的状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nWatchState">开关监控状态
        ///=0:	关闭监视（门开关状态）；
        ///=1: 监视（门开关状态）。
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetWatchDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWatchDoor(uint nPortIndex, uint nNetID, uint nWatchState);




        /// <summary>
        /// 设置门禁门磁、红外等感应器的特性(第一控制字)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam">
        /// 控制字一
        ///D7:	门开关是否监控，=1是 ； =0否；
        ///D6:	红外是否监控，  =1是 ； =0否；
        ///D5:	门电磁锁特性：=0自动锁上； =1 加电开锁后，必须由机械"开门--关门"动作，才能重新锁上。
        ///D4:	允许手动按键开门 =1是 ； =0否；
        ///D3:	门开关感应器在开门状态的有效输出电平（如是继电器输出，开路为 1，闭合为0 ）
        ///D2:	红外感应器在报警状态的有效输出电平（如是继电器输出，开路为 1，闭合为0 ）
        ///D1:	=1 该门在刷卡后，需确认用户密码（才能通行）；
        ///	    =0 卡合法即可通行。
        ///D0:	ENT键开门=1:允许；=0:不允许。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorCtrl1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorCtrl1(uint nPortIndex, uint nNetID, uint nCtrlParam);




        /// <summary>
        /// 设置门禁控制器联动及报警输出特性(第二控制字)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam">
        /// 控制字二
        ///D7:	联动输入CPLD1的有效电平；
        ///D6:	联动输入CPLD2的有效电平；
        ///D5:	联动CPLD1、CPLD2输入，控制器响应的规定动作；
        ///D4:	联动CPLD1、CPLD2输入，控制器响应的规定动作；
        ///D3:	ALARM输入在报警时是=0导通=1开路
        ///D2:	在刷卡时主动将接受的维根数据输入到电脑=1允许，=0禁止；
        ///D1:	“紧急事件”输入MDIN与GND闭合时，=1闭门，任何卡不响应=0 门常开
        ///D0:	用户ID+密码开门=1允许；=0禁止。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorCtrl2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorCtrl2(uint nPortIndex, uint nNetID, uint nCtrlParam);




        /// <summary>
        /// 设置门禁控制器联动及报警输出特性(第二控制字)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam">
        /// 控制字三
        ///D7:	门常闭时段是否有效=1有效=0无效
        ///D6:	门常开时段是否有效=1有效=0无效
        ///D5:	检查准进时段=1是=0否
        ///D4:	检查有效期=1是=0否
        ///D3:	反胁迫密码输入时=1报警=0不报警
        ///D2:	密码控制时段=1有效，=0无效；
        ///D1:	分体（第二头）要输入密码=1需要=0不需要
        ///D0:	主体（第一头）要输入密码=1需要=0不需要
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetDoorCtrl3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDoorCtrl3(uint nPortIndex, uint nNetID, uint nCtrlParam);




        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "InitRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitRec(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoadP">读记录指针</param>
        /// <param name="nMF">记录标识
        ///D7=0: 记录区空间足够,正常保存记录。
        ///  =1: 记录区已满，将再次从BOTTOM开始保存记录。
        ///D0=0: 当前新记录没有覆盖了未读取的记录(即:SAVEP小于LOADP)
        ///          =1: 当前新记录覆盖了未读取的记录(即:SAVEP大于LOADP)
        ///其他BIT位保留,默认等于“0”。
        ///
        ///</param>
        ///备  注:	不推荐使用该函数，如使用该函数必须确保nLoadP、nMF取值正确。
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetRecReadPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRecReadPoint(uint nPortIndex, uint nNetID, uint nLoadP, uint nMF);




        /// <summary>
        /// 设置记录读取指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nSaveP">下一条新记录的储存位置</param>
        /// <param name="nLoadP">读记录指针</param>
        /// <param name="nMF">
        /// 记录标识
        ///D7=0: 记录区空间足够,正常保存记录。
        ///  =1: 记录区已满，将再次从BOTTOM开始保存记录。
        ///D0=0: 当前新记录没有覆盖了未读取的记录(即:SAVEP小于LOADP)
        ///          =1: 当前新记录覆盖了未读取的记录(即:SAVEP大于LOADP)
        ///其他BIT位保留,默认等于“0”。
        ///备  注:	不推荐使用该函数，如使用该函数必须确保nLoadP、nMF取值正确。
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "SetRecReadPointEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRecReadPointEx(uint nPortIndex, uint nNetID, uint nSaveP, uint nLoadP, uint nMF);




        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpTime">返回设备当前的日期时间:年、月、日、时、分、秒、星期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME lpTime);




        /// <summary>
        /// 读取SM的历史记录柜桶参数信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">桶底BOTTOM(2字节)；</param>
        /// <param name="pnSaveP">下一次新记录存放指针 SAVEP(2字节)；</param>
        /// <param name="pnLoadP">下一次读取记录位置指针 LOADP(2字节)</param>
        /// <param name="pnMaxLen">柜桶最大深度MAXLEN(2字节)；</param>
        /// **跟文档不一致
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadRecInfo(uint nPortIndex, uint nNetID, out Int16 pnBottom, out Int16 pnSaveP, out Int16 pnLoadP, out Int16 nMF, out Int16 pnMaxLen);




        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">返回历史记录状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadOneRec(uint nPortIndex, uint nNetID, byte[] szRecSource, out  SYSTEMTIME lpTime, out uint pnRecState, out uint pnRecRemark);




        /// <summary>
        /// 在指定位置读取一条历史记录(设备LoadP不移动)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nReadPoint">读取记录位置</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">返回历史记录状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadRecByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadRecByPoint(uint nPortIndex, uint nNetID, uint nReadPoint, byte[] szRecSource, out SYSTEMTIME lpTime, out uint pnRecState, out uint pnRecRemark);




        /// <summary>
        /// 带顺序号读取一条历史记录，LoadP自动下移，并返回LoadP值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnLoadP">读出记录的位置LOADP值(SM将LOADP位置的历史记录, 连同LOADP本身一并返回)</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">返回历史记录状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadRecWithPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadRecWithPoint(uint nPortIndex, uint nNetID, out uint pnLoadP, byte[] szRecSource, out  SYSTEMTIME lpTime, out uint pnRecState, out uint pnRecRemark);




        /// <summary>
        /// 读取最新记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="lpTime">返回历史记录日期时间</param>
        /// <param name="pnRecState">返回历史记录状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadNewRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadNewRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecState, ref uint pnRecRemark);




        /// <summary>
        /// 读取设备工作日准进时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nGroupNo">工作日组号(1-4)</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为32的ASCII码字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadWorkLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadWorkLmtTime(uint nPortIndex, uint nNetID, uint nGroupNo, byte[] szTimeBCD);




        /// <summary>
        /// 读取设备非工作日准进时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">读取设备非工作日准进时段</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadUnWorkLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadUnWorkLmtTime(uint nPortIndex, uint nNetID, byte[] szTimeBCD);




        /// <summary>
        /// 读取设备星期准进时间表
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nGroup">时间表序号(有效值: 0--15)</param>
        /// <param name="nWeek">星期(有效值: 1--7)</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为48的ASCII码字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadWeekLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadWeekLmtTime(uint nPortIndex, uint nNetID, uint nGroup, uint nWeek, byte[] szTimeBCD);




        /// <summary>
        /// 读取门禁门常开时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为32的ASCII码字符串)</param>
        /// 备  注:	每次只能读取某张表的某一天之准进时间段的列表
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadOpenLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadOpenLmtTime(uint nPortIndex, uint nNetID, byte[] szTimeBCD);




        /// <summary>
        /// 读取门禁门常闭时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为32的ASCII码字符串)</param>
        /// 备  注:	每次只能读取某张表的某一天之准进时间段的列表
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadCloseLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCloseLmtTime(uint nPortIndex, uint nNetID, byte[] szTimeBCD);




        /// <summary>
        /// 读取门禁(刷卡+密码)时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为32的ASCII码字符串)</param>
        /// 备  注:	每次只能读取某张表的某一天之准进时间段的列表
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadPwdLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadPwdLmtTime(uint nPortIndex, uint nNetID, byte[] szTimeBCD);




        /// <summary>
        /// 读取门磁红外监控时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeBCD">返回时间段字符串(长度为32的ASCII码字符串)</param>
        /// 备  注:	每次只能读取某张表的某一天之准进时间段的列表
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadWatchLmtTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadWatchLmtTime(uint nPortIndex, uint nNetID, byte[] szTimeBCD);




        /// <summary>
        /// 读取设备授权用户数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnUserNum">返回的用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadUserCount(uint nPortIndex, uint nNetID, out uint pnUserNum);




        /// <summary>
        /// 读取用户信息 (按指定位置)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nUserPos">读取用户位置</param>
        /// <param name="szCardNo">返回卡号 10字节('0'-'9','A'-'F')</param>
        /// <param name="szUserID">返回用户ID 8字节('0'-'9')</param>
        /// <param name="szPasswd">返回密码  4字节('0'-'9')</param>
        /// <param name="lpLmtTime">返回用户有效期</param>
        /// <param name="pnUserType">
        /// 返回用户权限(类型)
        ///1, D7,D6=0,0:一般用户（受星期准进时段限制）
        ///		=0,1:一般用户（受星期准进时段限制）
        ///   D3-D0(0-15) 的值对应星期时段的第几组
        ///
        ///2, D7,D6=1,0:一般用户（受星期准进时段限制）
        ///   D1-D0=(0-3)的值对应工作时段的第几组
        ///
        ///3, D7,D6 =1,1:特权用户（不受任何准进时段限制），但受有效期等限制。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadUserInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadUserInfo(uint nPortIndex, uint nNetID, uint nUserPos, byte[] szCardNo, byte[] szUserID, byte[] szPasswd, out SYSTEMTIME lpLmtTime, out uint pnUserType);




        /// <summary>
        /// 读取用户信息 (按指定位置)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">用户ID</param>
        /// <param name="szCardNo">返回卡号 10字节('0'-'9','A'-'F')</param>
        /// <param name="szPasswd">返回密码  4字节('0'-'9')</param>
        /// <param name="lpLmtTime">返回用户有效期</param>
        /// <param name="pnUserType">
        /// 返回用户权限(类型)
        ///1, D7,D6=0,0:一般用户（受星期准进时段限制）
        ///		=0,1:一般用户（受星期准进时段限制）
        ///   D3-D0(0-15) 的值对应星期时段的第几组
        ///
        ///2, D7,D6=1,0:一般用户（受星期准进时段限制）
        ///   D1-D0=(0-3)的值对应工作时段的第几组
        ///
        ///3, D7,D6 =1,1:特权用户（不受任何准进时段限制），但受有效期等限制。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadUserByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadUserByUserID(uint nPortIndex, uint nNetID, string szUserID, StringBuilder szCardNo, StringBuilder szPasswd, out SYSTEMTIME lpLmtTime, out uint pnUserType);




        /// <summary>
        /// 读取用户信息 (按指定位置)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">卡号 10字节('0'-'9','A'-'F')</param>
        /// <param name="szUserID">返回用户ID 8字节('0'-'9')</param>
        /// <param name="szPasswd">返回密码  4字节('0'-'9')</param>
        /// <param name="lpLmtTime">返回用户有效期</param>
        /// <param name="pnUserType">
        /// 返回用户权限(类型)
        ///1, D7,D6=0,0:一般用户（受星期准进时段限制）
        ///		=0,1:一般用户（受星期准进时段限制）
        ///   D3-D0(0-15) 的值对应星期时段的第几组
        ///
        ///2, D7,D6=1,0:一般用户（受星期准进时段限制）
        ///   D1-D0=(0-3)的值对应工作时段的第几组
        ///
        ///3, D7,D6 =1,1:特权用户（不受任何准进时段限制），但受有效期等限制。
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadUserByCardNo(uint nPortIndex, uint nNetID, string szCardNo, StringBuilder szUserID, StringBuilder szPasswd, out SYSTEMTIME lpLmtTime, out uint pnUserType);




        /// <summary>
        /// 读取设备储存的每周休息日
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// 返回第一个休息日
        ///=0  不休息
        ///=1-7 休息的星期
        ///<param name="pnWeekend1">
        /// </param>
        /// <param name="pnWeekend2">返回第二个休息日,同上</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadWeekend", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadWeekend(uint nPortIndex, uint nNetID, out uint pnWeekend1, out uint pnWeekend2);




        /// <summary>
        /// 读取设备储存的节、假日
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWeekend1"></param>
        /// <param name="pnWeekend2"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadHolidays", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadHolidays(uint nPortIndex, uint nNetID, out uint pnHolidayNum, StringBuilder szHolidays);


        /// <summary>
        /// 读取远程监控状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWorkState">
        /// 一字节 SM的工作状态：
        ///D0=0: 正常工作，=1处于报警状态；
        ///D1=0: 门控电磁继电器关，=1加电；
        ///D2=0: 不监视门开关;  =1监视;
        ///D3=0: 不监视红外;  =1监视;
        ///D4	: 保留 ；
        ///D5=0: SM的工作电源正常； =1  不正常，供电电压低而CPU被平凡复位。
        ///D6=0: SM内存储器正常；=1  不正常；
        ///D7=0: SM内实时钟IC正常;  =1 不正常。
        /// </param>
        /// <param name="pnCtrlState">
        /// SM监控线路的状态：
        ///D0=0: 联动输入无效，=1有效；
        ///D1=0: 手动开门键松开，=1按下；
        ///D2=0: 红外输入正常，=1报警状态；
        ///D3=0: 门关闭，=1 开启；
        ///D4	: 保留。
        ///D5	: INPUT1
        ///D6	: 保留。 
        ///D7	: INPUT2
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadDoorState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadDoorState(uint nPortIndex, uint nNetID, out uint pnWorkState, out uint pnCtrlState);




        /// <summary>
        /// 读取门禁设备控制参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnCtrlParam">
        /// 控制参数 1字节时
        ///D7: 门开关是否监控，=1:是 ； =0:否；
        ///D6: 红外是否监控，  =1:是 ； =0:否；
        ///D5: 门电磁锁特性：=0:自动锁上； =1:加电开锁后，必须由机械"开门--关门"动作，才能重新锁上。
        ///D4: 允许手动按键开门 =1:是 ； =0:否；
        ///D3: 门开关感应器在开门状态的有效输出电平(如是继电器输出，开路为 1，闭合为0 )
        ///D2: 红外感应器在报警状态的有效输出电平(如是继电器输出，开路为 1，闭合为0)
        ///D1: =1:该门在刷卡后，需确认用户密码(才能通行)；	=0:卡合法即可通行。
        ///D0: ENT键开门 =1:允许 =0:不允许。
        /// </param>
        /// <param name="pnRelayDelay">门锁继电器的执行时间(0.1秒单位)</param>
        /// <param name="pnOpenDelay">开门等待进入的延时时间(0.1秒单位)</param>
        /// <param name="pnAlarmConfirm">红外报警发生至确认的延时时间(0.1秒单位)</param>
        /// <param name="pnAlarmDelay">安防报警驱动延时时间</param>
        /// <param name="pnRFDefine">感应卡阅读器型号，及卡号获取控制字节</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadDoorParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadDoorParam(uint nPortIndex, uint nNetID, out uint pnCtrlParam, out uint pnRelayDelay, out uint pnOpenDelay, out uint pnAlarmConfirm, out uint pnAlarmDelay, out uint pnRFDefine);




        /// <summary>
        /// 读取门禁控制器的第二、三控制字
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnCtrlParam2">
        /// 控制字二
        ///D7:	联动输入CPLD1的有效电平；
        ///D6:	联动输入CPLD2的有效电平；
        ///D5:	联动CPLD1、CPLD2输入，控制器响应的规定动作；
        ///D4:	联动CPLD1、CPLD2输入，控制器响应的规定动作；
        ///D3:	ALARM输入在报警时是=0导通=1开路
        ///D2:	在刷卡时主动将接受的维根数据输入到电脑=1允许，=0禁止；
        ///D1:	“紧急事件”输入MDIN与GND闭合时，=1闭门，任何卡不响应=0 门常开
        ///D0:	用户ID+密码开门=1允许；=0禁止。
        /// </param>
        /// <param name="pnCtrlParam3">
        /// 控制字三
        ///D7:	门常闭时段是否有效=1有效=0无效
        ///D6:	门常开时段是否有效=1有效=0无效
        ///D5:	检查准进时段=1是=0否
        ///D4:	检查有效期=1是=0否
        ///D3:	反胁迫密码输入时=1报警=0不报警
        ///D2:	密码控制时段=1有效，=0无效；
        ///D1:	分体（第二头）要输入密码=1需要=0不需要
        ///D0:	主体（第一头）要输入密码=1需要=0不需要
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadDoorCtrl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadDoorCtrl(uint nPortIndex, uint nNetID, out uint pnCtrlParam2, out uint pnCtrlParam3);




        /// <summary>
        /// 读取设备版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">备版本字符串(长度为18的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);





        /// <summary>
        /// 删除全部节假日
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DelAllHoliday", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelAllHoliday(uint nPortIndex, uint nNetID);





        

        /*******************************************************
         附录一：设备返回值（SM 应答 或返回值含义）
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
         *******************************************************/





        /********************************************************
         * 附录二：门控器(SM)的历史记录格式 
	     门控器(SM)对如下事件的发生都有记录：
        (1)	刷卡开门 (记录卡号及开门时间, 门是否被开, 是否确认用户个人密码 等)。
        (2)	由KEYPAD 键入用户ID及个人密码开门。
        (3)	红外报警开始，红外报警结束。
        (4)	非正常开门， 关门。
        (5)	刷卡或键入ID及个人密码开门 (电磁锁动作),  但未开门进入。
        (6)	开门进入, 在规定的延时内, 未关门。
        (7)	手动开门。
        (8)	远程（由SU）开门。
        (9)	联动输入引起开门。
        (10)	SM内控制参数被修改， 红外监控被关闭， 或开启。
        (11)	SM掉电（ 开始时间  结束时间 ） 。
        每条记录用14字节表示：
        事件来源（5字节）	日期，时间（7字节）	状态（1字节）	备注（1字节）
        卡号或ID号等	年（2），月，日，时，分，秒	STATUS（D7--D0）	REMARK
 	    备注（1字节）（D7D0 ,其中D0 最低位）的D7=0 表示该条历史记录从未被SU读取过；
         D7=1表示该条历史记录已被SU读取过。
         * 1:  REMARK=0 刷卡开门记录“事件来源” = 5字节卡号
         STATUS :
                D7=0  未确认用户个人密码,  =1 确认;
				D6=0  原来门处于关状态,    =1 开状态;
				D5=0  在规定时间内开门进入,  =1 未开门进入;
				D4=0  在开门等待进入延时后,门已正常关闭, =1 仍然开的;
				D3=1  开门进入后,关闭红外监控, = 0 保持原状态;
				D2    保留
                D1  =0，外接分体刷卡；=1 内置主体刷卡；
			    D0=0, 红外监视原来是关的； =1，是开的。
                    
         * 2:  REMARK=1 键入用户ID及个人密码开门的记录“事件来源” = 1字节00, + 4字节用户ID号
	     STATUS:
			    D7=1
			    D6—D0	类似 “REMARK=0” 中描述。 	
         * 3:  REMARK=2 远程(由SU)开门记录“事件来源” = 5字节全0       
         *STATUS:
		        类似 “REMARK=0” 中描述（D1位=0）。
         * 4:  REMARK=3 手动开门记录“事件来源” = 5字节全0          
         * STATUS:
		        类似 “REMARK=0” 中描述（D1位=0）。     
         * 5:  REMARK=4 联动开门记录（无）“事件来源” = 5字节全0 
         * STATUS:
		        类似 “REMARK=0” 中描述。
         * 6:  REMARK=5 报警 (或报警取消) 记录  “事件来源” = 4字节全0  +  报警源 AS(1字节)
		                      AS=0  红外报警开始；     
                              AS=1  红外停止报警。
		                      AS=2  非正常开门；       
                              AS=3  门被关闭（非正常状态的关门记录）。
		                      AS=4  联动(I2 )有效；     
                              AS=5  联动(I2 ) 无效。
		                      AS=6  SM内部存储器发生错误, SM自动进行了初始化操作。
		                      AS=7  红外监测被关闭；    
                              AS=8  红外监测开启。
		                      AS=9  门碰开关监测被关闭； 
                             AS=10  门碰开关监测开启。
         * STATUS:
		         记录发生时的输入线状态:
			     D0=0  联动 (I2)无效,  =1有效；
			     D1=0  手动按键松开,  =1按下；
			     D2=0  红外输入无效,  =1 报警状态；
			     D3=0  门处于关状态,  =1 门处于开状态；
			     D4—D7 保留。 
                    
         * 7:  REMARK=6  SM掉电记录“事件来源”  = SM重新掉电的日期,时间：月,日,时,分,秒
         * STATUS：
                 重新上电时的输入线状态：
                 D0=0  联动 (I2)无效,  =1有效；
			     D1=0  手动按键松开,  =1按下；
			     D2=0  红外输入无效,  =1 报警状态；
			     D3=0  门处于关状态,  =1 门处于开状态；
		         D4—D7 保留。
         * 8:  REMARK=7  内部控制参数被修改的记录“事件来源” = 4字节全0  +  修改标记(1字节)修改标记(1字节)
			    D0  =1 修改了SM的密码；
			    D1  =1 修改了门的特性控制参数；
			    D2  =1 增加了新用户；
			    D3  =1 删除了用户资料；
			    D4  =1 修改了实时钟；
         	    D5	 =1 修改了控制准进的时段设置；
			    D6  =1 修改了节假日列表； 
			    D7  =1 修改了红外开启（关闭）的设置控制字。   
         * 9:  REMARK=8  无效的用户卡刷卡记录。“事件来源” = 5字节卡号
         * STATUS:
		         类似 “REMARK=5” 中描述。
         * 10：REMARK=9  用户卡的有效期已过。 “事件来源” = 5字节卡号    
         * STATUS:
		         类似 “REMARK=5” 中描述。       
         * 11：REMARK=10 当前时间该用户卡无进入权限。“事件来源” = 5字节卡号       
         * STATUS:
		         类似 “REMARK=5” 中描述。

                   
         * 12：REMARK=11 用户在个人密码确认时，三次全部不正确 。“事件来源” = 5字节卡号          
         * STATUS:
		         类似 “REMARK=5” 中描述。
                   
         * 13：REMARK=0X22 有效的消防联动输入开始时间记录 。 “事件来源” = 0，0，0，0，0
         * STATUS:
		         类似 “REMARK=5” 中描述。
                    
         * 14：REMARK=0X22  有效的消防联动输入结束时间记录。“事件来源” = 0，0，0，0，1              
         * STATUS:
		         类似 “REMARK=5” 中描述。
        注意：以下返回的REMARK 值为新增，记录门被打开和关闭时最后一次开门操作命令（包括刷卡开门、用户ID开门、远程开门和手动开门等）的记录的备注值和事件来源，这两种记录类型只在开门等待延内的开门和关门才产生，超过延时只产生非法开门和关门的报警记录，请参看REMARK=5的记录类型。
        
         * 15：REMARK=0X40  门被打开记录。
         * STATUS: 上次开门动作的REMARK。“事件来源”= 上次开门动作的“事件来源”
        16：REMARK=0X41  门被关闭记录。
        STATUS: 上次关门动作的REMARK。“事件来源”= 上次关门动作的“事件来源”
        REMARK=5 报警 (或报警取消) 记录 增加刷卡未开门进入报警
	    	    “事件来源” = 4字节全0  +  报警源 AS(1字节)
	            AS=18  刷卡未开门进入。
	        STATUS:
	    	    记录发生时的输入线状态:
	    		    D5=1 未开门进入;
        REMARK=5 报警 (或报警取消) 记录 增加刷卡开门进入未关门报警
	    	    “事件来源” = 4字节全0  +  报警源 AS(1字节)
	        AS=19  刷卡开门进入未关闭。
	    STATUS:
		    记录发生时的输入线状态:
			    D4=1 在开门等待进入延时后,仍然开的;



         *******************************************************/




    }
}
