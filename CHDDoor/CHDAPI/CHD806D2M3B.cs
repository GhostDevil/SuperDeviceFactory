using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHD806D2M3B
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
        /// 读取远程监控状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWorkState1">
        /// 第一门的工作状态
        /// D7 =0实时钟IC正常;  =1 不正常需要重新设置时间;
        /// D6 =0正常，无事件请求；=1 DCU有事件要SU处理;
        /// D5 =0工作电源正常； =1 不正常,电压低而CPU被平凡复位;
        /// D4 保留（防拆开关）;
        /// D3 =0不监视红外入侵;  =1监视;
        /// D2 =0不监视门开关状态;  =1监视;
        /// D1 =0门控电磁继电器关闭，=1加电驱动；
        /// D0 =0正常工作，=1处于报警状态；
        /// </param>
        /// <param name="pnLineState1">
        /// 第一门的线路状态
        /// D7 =1紧急驱动输入； =0正常
        /// D6 =0 
        /// D5 =0 
        /// D4 保留
        /// D3 =1门开的; =0门闭合;  
        /// D2 =1窃入红外报警；=0正常；
        /// D1 =1出门放行键按下；=0松开；
        /// D0 =0 
        /// </param>
        /// <param name="pnWorkState2">
        /// 第二门的工作状态
        /// BIT位同第一门 
        /// </param>
        /// <param name="pnLineState2">
        /// 第二门的线路状态
        /// BIT位同第一门 
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDoorState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDoorState(uint nPortIndex, uint nNetID, out int pnWorkState1, out int pnLineState1, out int pnWorkState2, out int pnLineState2);



        /// <summary>
        /// 删除超级卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门ID</param>
        /// 备  注:	门ID的值不同设备不同的传值，请查看文档相关部分
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDeleteSuperCardEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int   DDeleteSuperCardEx(uint nPortIndex, uint nNetID, uint nDoorID);

       
        

        /// <summary>
        /// 设置超级卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCard1">第一张卡( ASCII的字符串 )</param>
        /// <param name="szCard2">第二张卡( ASCII的字符串 )</param>
        /// <param name="szCard3">第三张卡( ASCII的字符串 )</param>
        /// <param name="szCard4">第四张卡( ASCII的字符串 )</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetSuperCardEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int   DSetSuperCardEx(uint nPortIndex,uint nNetID, String szCard1, String szCard2, String szCard3, String szCard4);
        



        /// <summary>
        /// 取消访问权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DLinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLinkOff(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWorkState">
        /// 工作状态字节 pnWorkState[0]:1门; pnWorkState[1]:2门; ...;
        ///D7 =0实时钟IC正常;  =1 不正常需要重新设置时间;
        ///D6 =0正常，无事件请求；=1 DCU有事件要SU处理;
        ///D5 =0工作电源正常； =1 不正常,电压低而CPU被平凡复位;
        ///D4 保留（防拆开关）;
        ///D3 =0不监视红外入侵;  =1监视;
        ///D2 =0不监视门开关状态;  =1监视;
        ///D1 =0门控电磁继电器关闭，=1加电驱动；
        ///D0 =0正常工作，=1处于报警状态；
        /// </param>
        /// <param name="pnLineState">
        /// 线路状态 pnLineState[0]:1门; pnLineState[1]:2门; ...;
        ///D7 =1紧急驱动输入； =0正常
        ///D6 =0 门正常；=1常闭门； 
        ///D5 =0 门正常；=1常开门；  
        ///D4 保留
        ///D3 =1门开的; =0门闭合;  
        ///D2 =1窃入红外报警；=0正常；
        ///D1 =1出门放行键按下；=0松开；
        ///D0 =0 
        /// </param>
        /// <param name="nDoorCount">门的数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDoorStateEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDoorStateEx(uint nPortIndex, uint nNetID, out uint pnWorkState, out uint pnLineState, uint nDoorCount);





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
        /// 读取常开/闭门的状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门ID  =1:门1; =2:门2; ...;</param>
        /// <param name="pnState">状态 D5=1表示常开  D6=1表示常闭  =0正常</param>
        /// <param name="pnDelay">剩余时间（分钟） =0时解除常开门</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadAlwaysStateEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadAlwaysStateEx(uint nPortIndex, uint nNetID, uint nDoorID, out  uint pnState, out  uint pnDelay);







        /// <summary>
        /// 远程常开门
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">门ID</param>
        /// <param name="nDoorID">=1操作门1  =2操作门2  =0FFH 操作门1和2  =0不操作</param>
        /// <param name="nDelay">闭门延时时间（分钟） =0时解除常开门</param>
        /// <param name="szUser">操作员(长度为10的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DAlwaysOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DAlwaysOpenDoor(uint nPortIndex, uint nNetID, uint nDoorID, uint nDelay, string szUser);






        /// <summary>
        /// 远程常闭门
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门ID</param>
        /// <param name="nDelay">闭门延时时间（分钟） =0时解除常闭门</param>
        /// <param name="szUser">操作员(长度为10的ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DAlwaysCloseDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DAlwaysCloseDoor(uint nPortIndex, uint nNetID, uint nDoorID, uint nDelay, string szUser);






        /// <summary>
        /// 设置设备新密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szNewSysPwd">新设备系统密码 4个('0'..'9','A'..'F'）字符</param>
        /// <param name="szNewKeyPwd">新设备键盘密码 6个('0'..'9','A'..'F'）字符</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DNewPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DNewPwd(uint nPortIndex, uint nNetID, String szNewSysPwd, String szNewKeyPwd);





        /// <summary>
        /// 设置控制器网络地址
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNewGrp">组号(0-254)</param>
        /// 备  注：nNetID = nID + Grp*256;
        /// <param name="nNewID">设备ID(1-254)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DNewNetID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DNewNetID(uint nPortIndex, uint nNetID, uint nNewGrp, uint nNewID);







        /// <summary>
        /// 打开报警继电器
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayID"> 继电器ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DOpenAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DOpenAlarm(uint nPortIndex, uint nNetID, uint nDelayID);






        /// <summary>
        /// 关闭报警继电器
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDelayID">继电器ID  =1:1号报警继电器; =2:2号报警继电器; ….; =0xFF:所有报警继电器 =0:不操作</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DCloseAlarm", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DCloseAlarm(uint nPortIndex, uint nNetID, uint nDelayID);






        /// <summary>
        /// 设置主动上传通道
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChannel">传通道 0，1，2，3…</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetTransmitChannel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetTransmitChannel(uint nPortIndex, uint nNetID, uint nChannel);






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







        /// <summary>
        /// 关闭胁迫报警
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DMenaceClose", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DMenaceClose(uint nPortIndex, uint nNetID);






        /// <summary>
        /// 读取设备版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回的设备描述及版本(以'\0'结尾ASCII字符串)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);






        /// <summary>
        /// 设置设备日期时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">日期。世纪,年，月，日，星期，时，分，秒</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pTime);






        /// <summary>
        /// 读取实时钟
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">返回设备当前的日期时间:年、月、日、时、分、秒、星期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pTime);






        /// <summary>
        /// 初始化记录区(清空记录)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrLinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DInitRec(uint nPortIndex, uint nNetID);






        /// <summary>
        /// 读取SM的历史记录柜桶参数信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">桶底BOTTOM(2字节)；</param>
        /// <param name="pnSaveP">下一次新记录存放指针 SAVEP(2字节)；</param>
        /// <param name="pnLoadP">下一次读取记录位置指针 LOADP(2字节)</param>
        /// <param name="pnMaxLen">柜桶最大深度MAXLEN(2字节)；</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecInfo(uint nPortIndex, uint nNetID, out int pnBottom, out int pnSaveP, out int pnLoadP, out int pnMaxLen);







        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="pTime">返回历史记录日期时间</param>
        /// <param name="pnRecWorkState">返回历史记录工作状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注字</param>
        /// <param name="pnRecLineState">返回历史记录线路状态字</param>
        /// <param name="pnRecDoorID">返回历史记录门号(比如1,2,3,4,5,8)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadOneRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);






        /// <summary>
        /// 顺序读取一条历史记录(带存储位置)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnRecPoint">返回记录的指针</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="pTime">返回历史记录日期时间</param>
        /// <param name="pnRecWorkState">返回历史记录工作状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注字</param>
        /// <param name="pnRecLineState">返回历史记录线路状态字</param>
        /// <param name="pnRecDoorID">返回历史记录门号(比如1,2,3,4,5,8)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecWithPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecWithPoint(uint nPortIndex, uint nNetID, out int pnRecPoint, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);






        /// <summary>
        /// 随机读取记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nReadPoint">信息存储位置。0～65535</param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="pTime">返回历史记录日期时间</param>
        /// <param name="pnRecWorkState">返回历史记录工作状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注字</param>
        /// <param name="pnRecLineState">返回历史记录线路状态字</param>
        /// <param name="pnRecDoorID">返回历史记录门号(比如1,2,3,4,5,8)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecByPoint(uint nPortIndex, uint nNetID, Int16 nReadPoint, StringBuilder szRecSource, out  SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);



        /// <summary>
        /// 顺序读取一条历史记录，LoadP自动下移
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">返回历史记录来源10字符的ASCII字符串</param>
        /// <param name="pTime">返回历史记录日期时间</param>
        /// <param name="pnRecWorkState">返回历史记录工作状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注字</param>
        /// <param name="pnRecLineState">返回历史记录线路状态字</param>
        /// <param name="pnRecDoorID">返回历史记录门号 </param>
        /// <returns>设备返回值</returns>

        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneNewRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int  DReadOneNewRec(uint nPortIndex, uint nNetID,StringBuilder szRecSource,out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int  pnRecDoorID);

       



        /// <summary>
        /// 设备按卡号删除一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">卡号 10个('0'..'9','A'..'F')的ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDelUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDelUserByCardNo(uint nPortIndex, uint nNetID, string szCardNo);






        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="pnUserNum">返回用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadUserCount(uint nPortIndex, uint nNetID, out int pnUserNum);






        /// <summary>
        /// 设备删除所有用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DDelAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DDelAllUser(uint nPortIndex, uint nNetID);






        /// <summary>
        /// 设置远程超级开门密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号。1，2，3，4</param>
        /// <param name="szPwd1">第一个密码( 长度为8的ASCII的字符串 )</param>
        /// <param name="szPwd2">第二个密码( 长度为8的ASCII的字符串 )</param>
        /// 备  注:	本函数不适用于CHD806D2CP设备
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetSuperPwdEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetSuperPwdEx(uint nPortIndex, uint nNetID, uint nDoorID, string szPwd1, string szPwd2, string szPwd3, string szPwd4);






        /// <summary>
        /// 读取超级开门密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号（0--254）</param>
        /// <param name="szPwd1">第一个密码( 长度为8的ASCII的字符串 )</param>
        /// <param name="szPwd2">第二个密码( 长度为8的ASCII的字符串 )</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadSuperPwdEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadSuperPwdEx(uint nPortIndex, uint nNetID, uint nDoorID, StringBuilder szPwd1, StringBuilder szPwd2);






        /// <summary>
        /// 读取超级卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号（0--254）</param>
        /// <param name="szCard1">第一张卡( ASCII的字符串 )</param>
        /// <param name="szCard2">第二张卡( ASCII的字符串 )</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadSuperCardEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadSuperCardEx(uint nPortIndex, uint nNetID, uint nDoorID, StringBuilder szCard1, StringBuilder szCard2);






        /// <summary>
        /// 设置超级卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号。0～255</param>
        /// <param name="szCard1">第一张卡( ASCII的字符串 )</param>
        /// <param name="szCard2">第二张卡( ASCII的字符串 )</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetSuperCardEx1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetSuperCardEx1(uint nPortIndex, uint nNetID, uint nDoorID, String szCard1, String szCard2);





        /// <summary>
        /// 设置星期时段序号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">门ID</param>
        /// <param name="nDoorID">门ID</param>
        /// <param name="nWeek">星期(取值:1-7)</param>
        /// <param name="szTime">该天对应的时段序号列表(1类卡准进时段+2类卡准进时段+...+32类卡准进时段+常开时段+刷卡加密码时段+自动布防时段+N+1屏蔽时段)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetWeekTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetWeekTime(uint nPortIndex, uint nNetID, uint nDoorID, uint nWeek, String szTime);






        /// <summary>
        /// 设置星期时段序号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门ID</param>
        /// <param name="nLmtMonth">月</param>
        /// <param name="nLmtDay">日</param>
        /// <param name="szTime">该天对应的时段序号列表(1类卡准进时段+2类卡准进时段+...+32类卡准进时段+常开时段+刷卡加密码时段+自动布防时段+N+1屏蔽时段)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetHolidayTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetHolidayTime(uint nPortIndex, uint nNetID, uint nDoorID, uint nLmtMonth, uint nLmtDay, string szTime);






        /// <summary>
        /// 设备增加一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">增加的卡号   10个('0'..'9','A'..'F')的ASCII字符串</param>
        /// <param name="szUserID">增加的用户ID  8个('0'..'9')的ASCII字符串</param>
        /// <param name="szUserPwd">用户的密码   4个('0'..'9','A'..'F')的ASCII字符串</param>
        /// <param name="pTime">用户使用时间期限:年、月、日</param>
        /// <param name="nLevel">用户级别</param>
        /// <param name="nBCode">银行代码</param>
        /// <param name="nGroup1">组别(门1)
        ///=0:无权限
        ///=1:超级权限卡
        ///=2:VIP卡
        ///=3:库管员
        ///=4:普通职员
        ///=255:表示该权限不改变
        ///在"N+1"中的1为VIP卡，N为普通职员(组别小于4的人员)。
        ///</param>
        /// <param name="nCardType1">
        /// 卡类型(门1)
        ///取值:0-31;
        ///在N卡开门中，如果需要检查分组，则要求卡片的N卡属于同一卡片类
        /// </param>
        /// <param name="nGroup2">组别(门2)</param>
        /// <param name="nCardType2">卡类型(门2)</param>
        /// 备  注:	关于各种类型卡的分辨:
        ///1:如果D7,D6为1,1就是特权卡,不管其他位为任何值
        ///2:如果D7,D6不为1,1,只需要判断D1,D0的值对应卡的类型
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBAddUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBAddUser(uint nPortIndex, uint nNetID, String szCardNo, String szUserID, String szUserPwd, ref SYSTEMTIME pTime, uint nLevel, uint nBCode, uint nGroup1, uint nCardType1, uint nGroup2, uint nCardType2);






        /// <summary>
        /// 设置控制器输入/输出端口参数配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPortID">控制器IO端口号</param>
        /// <param name="nMode">模式 D0=0:非使能; D0=1:使能;</param>
        /// <param name="nConfig">配置
        ///D0=0:正常情况常闭; D0=1:正常情况常开;
        ///D1=0:紧急情况常闭; D1=1:紧急情况常开;
        ///</param>
        /// <param name="nIdelay">输入延时(0-255毫秒)</param>
        /// <param name="nOdelay">输出延时(0-65535秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetIOConfig", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetIOConfig(uint nPortIndex, uint nNetID, uint nPortID, uint nMode, uint nConfig, uint nIdelay, uint nOdelay);






        /// <summary>
        /// 设置控制器输出端口逻辑配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPortID">控制器IO端口号</param>
        /// <param name="szConfig">长为128配置字节数组(很复杂...囧...)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetOLogicCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetOLogicCfg(uint nPortIndex, uint nNetID, uint nPortID, String szConfig);






        /// <summary>
        /// 设置读卡器的配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCardReaderID">读卡器ID号</param>
        /// <param name="nDoorID">门号</param>
        /// <param name="nMode">
        /// 模式
        ///D0=0:非使能;	=1:使能;
        ///D1=0:进场;		=1:出场;
        ///D2=0:仓库区;	=1:非仓库区;
        ///D3=0:检测库管员;=1:不检测库管员
        ///D4=0:联网时中心确认开门，非联网时本地确认开门;	=1:本地确认开门
        ///</param>
        /// <param name="nConfig">
        /// 配置
        ///D0=0:不校验权限;	=1:校验权限;
        ///D1=0:不分组;		=1:分组;
        ///D2=0:不分类;		=1:分类;
        ///D3=0:刷卡不存记录;	=1:刷卡保存记录
        ///D4=0:不支持刷卡+密码;	=1:支持刷卡+密码
        ///D5=0:不支持区域管理	=1:支持区域管理
        ///D6=0:不支持指纹+密码;=1:支持指纹+密码
        /// </param>
        /// <param name="nCurArea">当前区域(0-255)</param>
        /// <param name="nInArea">进入区域(0-255)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetCardReaderCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetCardReaderCfg(uint nPortIndex, uint nNetID, uint nCardReaderID, uint nDoorID, uint nMode, uint nConfig, uint nCurArea, uint nInArea);






        /// <summary>
        /// 设置控制器固定关联配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号</param>
        /// <param name="nIndex">索引</param>
        /// <param name="nTrigger">触发点</param>
        /// <param name="nMode">模式</param>
        /// <param name="nDelay">延时触发时间</param>
        /// <param name="nPeriodIndex">有效时段索引</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetFixedAssCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetFixedAssCfg(uint nPortIndex, uint nNetID, uint nDoorID, uint nIndex, uint nTrigger, uint nMode, uint nDelay, uint nPeriodIndex);






        /// <summary>
        /// 设置自建关联联配置(创建虚拟端口号，最大20)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">自建关联编号</param>
        /// <param name="nTrigger">触发点</param>
        /// <param name="nMode">模式</param>
        /// <param name="nDelay">延时触发时间(该参数无效)</param>
        /// <param name="nPeriodIndex">有效时段索引</param>
        /// <param name="nReply">响应点( nReply = nReply and 0x40FF )</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetCustomAssCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetCustomAssCfg(uint nPortIndex, uint nNetID, uint nIndex, uint nTrigger, uint nMode, uint nDelay, uint nPeriodIndex, uint nReply);






        /// <summary>
        /// 设置仓库管理员模式
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nMode1">
        /// 模式(门1)
        ///=0:无需支持仓库管理员先进入条件
        ///=1:仓库管理员先进入.仓库管理员没有进入,其他用户刷卡时不能开门.
        ///=3:支持2个仓官员模式
        ///</param>
        /// <param name="nMode2">模式(门2)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetDepositoryMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetDepositoryMode(uint nPortIndex, uint nNetID, uint nMode1, uint nMode2);






        /// <summary>
        /// 设置门内组地址
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInternalID">门内组地址</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetInternalID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetInternalID(uint nPortIndex, uint nNetID, uint nInternalID);






        /// <summary>
        /// 清除设备状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">卡号</param>
        /// <param name="nCurArea">当前区域</param>
        /// <param name="nFlag">
        /// 清除标识
        ///=0:清除所有信息。包括所有卡片所在的区域与库管员的状态
        ///   此时将忽略szCardNo、nCurArea参数
        ///=1:仓库管理员先进入.仓库管理员没有进入,其他用户刷卡时不能开门.
        ///=2:支持2个仓官员模式
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBSetStatusInit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBSetStatusInit(uint nPortIndex, uint nNetID, String szCardNo, uint nCurArea, uint nFlag);






        /// <summary>
        /// 读取门内组地址
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnInternalID">门内组地址</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadInternalID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadInternalID(uint nPortIndex, uint nNetID, out uint pnInternalID);






        /// <summary>
        /// 按卡号读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szReadCardNo">需要读取的卡号的字符串</param>
        /// <param name="szUserID">返回卡号   10字节('0'-'9','A'-'F')字符串</param>
        /// <param name="szUserPwd">返回密码   4字节('0'-'9')字符串</param>
        /// <param name="pTime">返回用户有效期</param>
        /// <param name="pnLevel">用户级别</param>
        /// <param name="pnBCode">银行代码</param>
        /// <param name="pnGroup1">
        /// 组别(门1)
        ///=0:无权限
        ///=1:超级权限卡
        ///=2:VIP卡
        ///=3:库管员
        ///=4:普通职员
        ///=255:表示该权限不改变
        ///在"N+1"中的1为VIP卡，N为普通职员(组别小于4的人员)。
        ///</param>
        /// <param name="pnCardType1">
        /// 卡类型(门1)
        ///取值:0-31;
        ///在N卡开门中，如果需要检查分组，则要求卡片的N卡属于同一卡片类
        ///</param>
        /// <param name="pnGroup2">组别(门2)</param>
        /// <param name="pnCardType2">卡类型(门2)</param>
        /// 备  注:	关于各种类型卡的分辨:
        ///1:如果D7,D6为1,1就是特权卡,不管其他位为任何值
        ///2:如果D7,D6不为1,1,只需要判断D1,D0的值对应卡的类型
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadUserInfoByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadUserInfoByCardNo(uint nPortIndex, uint nNetID, String szReadCardNo, StringBuilder szUserID, StringBuilder szUserPwd, out SYSTEMTIME pTime, out uint pnLevel, out uint pnBCode, out uint pnGroup1, out uint pnCardType1, out uint pnGroup2, out uint pnCardType2);






        /// <summary>
        /// 按用户ID读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">返回用户ID 8字节('0'-'9')字符串</param>
        /// <param name="szCardNo">需要读取的卡号的字符串</param>
        /// <param name="szUserPwd">返回密码   4字节('0'-'9')字符串</param>
        /// <param name="pTime">返回用户有效期</param>
        /// <param name="pnLevel">用户级别</param>
        /// <param name="pnBCode">银行代码</param>
        /// <param name="pnGroup1">
        /// 组别(门1)
        ///=0:无权限
        ///=1:超级权限卡
        ///=2:VIP卡
        ///=3:库管员
        ///=4:普通职员
        ///=255:表示该权限不改变
        ///在"N+1"中的1为VIP卡，N为普通职员(组别小于4的人员)。
        ///</param>
        /// <param name="pnCardType1">
        /// 卡类型(门1)
        ///取值:0-31;
        ///在N卡开门中，如果需要检查分组，则要求卡片的N卡属于同一卡片类
        ///</param>
        /// <param name="pnGroup2">组别(门2)</param>
        /// <param name="pnCardType2">卡类型(门2)</param>
        /// 备  注:	关于各种类型卡的分辨:
        ///1:如果D7,D6为1,1就是特权卡,不管其他位为任何值
        ///2:如果D7,D6不为1,1,只需要判断D1,D0的值对应卡的类型
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadUserInfoByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadUserInfoByUserID(uint nPortIndex, uint nNetID, String szUserID, StringBuilder szCardNo, StringBuilder szUserPwd, out SYSTEMTIME pTime, out uint pnLevel, out uint pnBCode, out uint pnGroup1, out uint pnCardType1, out uint pnGroup2, out uint pnCardType2);






        /// <summary>
        /// 按存储位置读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nUserPos"></param>
        /// <param name="szCardNo">需要读取的卡号的字符串</param>
        /// <param name="szUserID">返回用户ID 8字节('0'-'9')字符串</param>
        /// <param name="szUserPwd">返回密码   4字节('0'-'9')字符串</param>
        /// <param name="pTime">返回用户有效期</param>
        /// <param name="pnLevel">用户级别</param>
        /// <param name="pnBCode">银行代码</param>
        /// <param name="pnGroup1">
        /// 组别(门1)
        ///=0:无权限
        ///=1:超级权限卡
        ///=2:VIP卡
        ///=3:库管员
        ///=4:普通职员
        ///=255:表示该权限不改变
        ///在"N+1"中的1为VIP卡，N为普通职员(组别小于4的人员)。
        ///</param>
        /// <param name="pnCardType1">
        /// 卡类型(门1)
        ///取值:0-31;
        ///在N卡开门中，如果需要检查分组，则要求卡片的N卡属于同一卡片类
        ///</param>
        /// <param name="pnGroup2">组别(门2)</param>
        /// <param name="pnCardType2">卡类型(门2)</param>
        /// 备  注:	关于各种类型卡的分辨:
        ///1:如果D7,D6为1,1就是特权卡,不管其他位为任何值
        ///2:如果D7,D6不为1,1,只需要判断D1,D0的值对应卡的类型
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadUserInfoByUserPos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadUserInfoByUserPos(uint nPortIndex, uint nNetID, uint nUserPos, StringBuilder szCardNo, StringBuilder szUserID, StringBuilder szUserPwd, out SYSTEMTIME pTime, out uint pnLevel, out uint pnBCode, out uint pnGroup1, out uint pnCardType1, out uint pnGroup2, out uint pnCardType2);






        /// <summary>
        /// 读取控制器端口数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnPortCount">返回端口数量</param>
        /// <param name="pnReaderCount">返回读卡器数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadPortCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadPortCount(uint nPortIndex, uint nNetID, out uint pnPortCount, out uint pnReaderCount);






        /// <summary>
        /// 读取控制器输入/输出端口参数配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPortID">控制器IO端口号</param>
        /// <param name="szPortName">端口名(8字节)</param>
        /// <param name="pnMode">模式 D0=0:非使能; D0=1:使能;</param>
        /// <param name="pnConfig">
        /// 配置
        ///D0=0:正常情况常闭; D0=1:正常情况常开;
        ///D1=0:紧急情况常闭; D1=1:紧急情况常开;
        ///</param>
        /// <param name="pnIdelay">输入延时(0-255毫秒)</param>
        /// <param name="pnOdelay">输出延时(0-65535秒)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadIOConfig", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadIOConfig(uint nPortIndex, uint nNetID, uint nPortID, StringBuilder szPortName, out uint pnMode, out uint pnConfig, out uint pnIdelay, out uint pnOdelay);






        /// <summary>
        /// 读取控制器读取IO逻辑输出表
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPortID">控制器IO端口号</param>
        /// <param name="szConfig">逻辑配置数据(128字节)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadOLogicCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadOLogicCfg(uint nPortIndex, uint nNetID, uint nPortID, StringBuilder szConfig);






        /// <summary>
        /// 读取读卡器的配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCardReaderID">读卡器ID号</param>
        /// <param name="szPortName">端口名(8字节)</param>
        /// <param name="pnDoorID">门号</param>
        /// <param name="pnMode">
        /// 模式
        ///D0=0:非使能;	=1:使能;
        ///D1=0:进场;		=1:出场;
        ///D2=0:仓库区;	=1:非仓库区;
        ///D3=0:检测库管员;=1:不检测库管员
        ///D4=0:联网时中心确认开门，非联网时本地确认开门;	=1:本地确认开门
        ///</param>
        /// 配置
        ///D0=0:不校验权限;	=1:校验权限;
        ///D1=0:不分组;		=1:分组;
        ///D2=0:不分类;		=1:分类;
        ///D3=0:刷卡不存记录;	=1:刷卡保存记录
        ///D4=0:不支持刷卡+密码;	=1:支持刷卡+密码
        ///D5=0:不支持区域管理	=1:支持区域管理
        ///D6=0:不支持指纹+密码;=1:支持指纹+密码
        ///<param name="pnConfig">
        /// </param>
        /// <param name="pnCurArea">当前区域(0-255)</param>
        /// <param name="pnInArea">进入区域(0-255)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadCardReaderCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadCardReaderCfg(uint nPortIndex, uint nNetID, uint nCardReaderID, StringBuilder szPortName, out uint pnDoorID, out uint pnMode, out uint pnConfig, out uint pnCurArea, out uint pnInArea);






        /// <summary>
        /// 读取控制器固定关联配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDoorID">门号</param>
        /// <param name="nIndex">索引</param>
        /// <param name="pnTrigger">触发点</param>
        /// <param name="pnMode">模式</param>
        /// <param name="pnDelay">延时触发时间</param>
        /// <param name="pnPeriodIndex">有效时段索引</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadFixedAssCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadFixedAssCfg(uint nPortIndex, uint nNetID, uint nDoorID, uint nIndex, out uint pnTrigger, out uint pnMode, out uint pnDelay, out uint pnPeriodIndex);






        /// <summary>
        /// 读取自建关联联配置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nIndex">自建关联编号</param>
        /// <param name="pnTrigger">触发点</param>
        /// <param name="pnMode">模式(该参数无效)</param>
        /// <param name="pnDelay">延时触发时间</param>
        /// <param name="pnPeriodIndex">有效时段索引</param>
        /// <param name="pnReply">响应点</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadCustomAssCfg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadCustomAssCfg(uint nPortIndex, uint nNetID, uint nIndex, out   uint pnTrigger, out   uint pnMode, out   uint pnDelay, out   uint pnPeriodIndex, out   uint pnReply);






        /// <summary>
        /// 设置仓库管理员模式
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnMode">
        /// 模式(门1)
        ///=0:无需支持仓库管理员先进入条件
        ///=1:仓库管理员先进入.仓库管理员没有进入,其他用户刷卡时不能开门.
        ///=3:支持2个仓官员模式
        ///</param>
        /// <param name="pnMode2">模式(门2)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DBReadDepositoryMode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DBReadDepositoryMode(uint nPortIndex, uint nNetID, out   uint pnMode, out   uint pnMode2);







        /*****************************************************************************************
         * 门ID号的值说明
         * 1．	CHD806D1CP、CHD806D2CP、CHD806D4类型的设备门ID号用bit为表示；
                D0:1门; D2:2门; D2:3门; D3:4门; D4:5门; D5:6门; D6:7门; D7:8门;
                例如：门ID等于0x04(0000, 0100) 表示第4门

         2．	CHD806D4M3、CHD806D8M3类型的设备门ID号直接用数字表示
                例如：门ID等于1 表示第1门、门ID等于2 表示第2门、门ID等于3 表示第3门
         * 
         * ****************************************************************************************
         * 
         * 
         * 附录一：设备返回值（SM 应答 或返回值含义）
         *  RTN=00H  SM 正常执行 SU 发来的命令。
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
         * 
         * 
         ******************************************************************************************
         *
         * 
         * 
         *  附录二： 门控器(SM)的历史记录格式
         * 门控器(SM)对如下事件的发生都有记录：每条记录用16字节表示：
         * 
         * 事件来源（5字节）	     日期，时间（7字节）	   工作状态（第1字节）	      备注（1字节）	   线路状态（第2字节）   	门号(1字节)
         * ***********************************************************************************************************************************
           卡号或ID号等	    世纪,年，月，日，时，分，秒	    WORK-STATUS（D7--D0）	      REMARK	        LINE-STATUS（D7--D0）	门1 =0门2 =1
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
         * *************************************************************************/










    }
}
