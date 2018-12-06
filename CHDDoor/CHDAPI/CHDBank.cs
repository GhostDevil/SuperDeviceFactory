using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    ///CHDBank接口类
    /// </summary>
   public static class CHDBank
    {

        /**附录1
         * 银行卡卡号或ID（10 字节）	日期，时间（6 字节BCD 码）	备注（1 字节）
         * 卡号或ID 号	年，月，日，时，分，秒	REMARK 
         * 
         **/



        /**调用说明
         * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
         * 2．	执行LinkOn校验设备密码，确认设备通讯权限；
         * 3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
         * 4．	执行LinkOff取消设备通讯权限；
         * 5．	当程序关闭时使用ClosePort关闭端口。
         * 
         **/




        /// <summary>
        /// 日期时间同步命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">设置给设备的日期时间:年、月、日、时、分、秒、星期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);




        /// <summary>
        /// 开门继电器执行时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayDelay">（有效值1--255），单位： 0.1 秒。</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetOpenRelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetOpenRelay(uint nPortIndex, uint nNetID, uint nRelayDelay);




        /// <summary>
        /// 设定门口机本地报警继电器的闭合时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayDelay">（有效值1--255），单位： 0.1 秒。</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetAlarmDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetAlarmDelay(uint nPortIndex, uint nNetID, uint nRelayDelay);




        /// <summary>
        /// 门口机开启红外监控后的等待延时（布防延时）
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">开启延时DELAY TIME（0.1 秒为单位）,有效值20--255 秒</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetOpenIrDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetOpenIrDelay(uint nPortIndex, uint nNetID, uint nOpenIrDelay);




        /// <summary>
        /// 门口机门磁、红外等感应器的特性（单独设定）
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">控制参数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetCtrlParam1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetCtrlParam1(uint nPortIndex, uint nNetID, uint nCtrlParam1);




        /// <summary>
        ///设定门口机所有参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam1">控制参数</param>
        /// <param name="nDoorRelayDelay">门锁继电器执行时间</param>
        /// <param name="nOpenedRelayDelay">开门等待进入的时间（红外无效确认时间）</param>
        /// <param name="nIrSureDelay">红外报警的确认时间</param>
        /// <param name="nIcDelay">安防报警的驱动延时</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetAllCtrlParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetAllCtrlParam(uint nPortIndex, uint nNetID, uint nCtrlParam1, uint nDoorRelayDelay, uint nOpenedRelayDelay, uint nIrSureDelay, uint nIcDelay);



        /// <summary>
        ///门口机联动及报警输出特性
        /// </summary>
        /// <param name="nPortIndex">nPortIndex</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam2">控制参数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetCtrlParam2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetCtrlParam2(uint nPortIndex, uint nNetID, uint nCtrlParam2);




        /// <summary>
        /// 远程开门(带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserNo">用户信息。5个字节，分高低位。</param>
        /// <param name="nFlag">=1 开门,=0 不操作</param>
        /// <returns>0开门成功否则开门失败</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkRemoteOpenDoorWithUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkRemoteOpenDoorWithUser(uint nPortIndex, uint nNetID, String szUserNo, uint nFlag);




        /// <summary>
        /// 单一放行(不带系统操作员信息)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">=1 开门,=0 不操作</param>
        /// <returns>0开门成功否则开门失败</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkRemoteOpenDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkRemoteOpenDoor(uint nPortIndex, uint nNetID, uint nFlag);




        /// <summary>
        /// 初驶化门口机的记录区(清空记录)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkInitRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkInitRec(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 设定门口机记录读指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoadP">LOADP.0~65535</param>
        /// <param name="nMF">0</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetRecReadPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetRecReadPoint(uint nPortIndex, uint nNetID, uint nLoadP, uint nMF);




        /// <summary>
        /// 设定门口机整个记录区指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nSaveP">SAVEP：（2 字节）</param>
        /// <param name="nLoadP">LOADP.0~65535</param>
        /// <param name="nMF">0</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetRecPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetRecPoint(uint nPortIndex, uint nNetID, uint nSaveP, uint nLoadP, uint nMF);



        /// <summary>
        ///按语音段号播音
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNumber">1 字节语音段号（0-255）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkPlayMedio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkPlayMedio(uint nPortIndex, uint nNetID, uint nNumber);




        /// <summary>
        ///设置门口机自动开启对红外、门开关的监控时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetWatchTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetWatchTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 设置门口机一天内允许手动开门的时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetManualTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetManualTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 设置室内灯光常开时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetIndoorLightTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetIndoorLightTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 设置室外灯光常开时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetOutdoorLightTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetOutdoorLightTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        ///恢复出厂设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetRestore", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetRestore(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 人数限制设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPeople">设置门控制器限制人数范围1-255</param>
        /// <param name="nLmtTime">延迟时间。例如，设置30 分钟延时，DATA1=0x50,DATA2=0x46.</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetEnterLmt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetEnterLmt(uint nPortIndex, uint nNetID, uint nPeople, uint nLmtTime);




        /// <summary>
        /// 远程关门
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkRemoteCloseDoor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkRemoteCloseDoor(uint nPortIndex, uint nNetID);




        /// <summary>
        ///读取门控制器的实时钟
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="EpDateTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME EpDateTime);




        /// <summary>
        /// 读取门口机历史记录柜桶参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">返回桶参数</param>
        /// <param name="pnSaveP">返回SaveP</param>
        /// <param name="pnLoadP">返回LoadP</param>
        /// <param name="pnMaxLen">返回MaxLen</param>
        /// <param name="pnMF">返回MF</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadRecInfo(uint nPortIndex, uint nNetID, ref uint pnBottom, ref uint pnSaveP, ref uint pnLoadP, ref uint pnMaxLen, ref uint pnMF);




        /// <summary>
        /// 顺序读取门口机一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nPos">存储位置</param>
        /// <param name="szRecSource">查看附录1</param>
        /// <param name="ElpTime">查看附录1</param>
        /// <param name="pnRecRemark">查看附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadRecByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadRecByPoint(uint nPortIndex, uint nNetID, uint nPos, StringBuilder szRecSource, ref SYSTEMTIME ElpTime, ref uint pnRecRemark);




        /// <summary>
        /// 附带顺序号读取门口机记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnLoadP">返回LoadP</param>
        /// <param name="szRecSource">查看附录1</param>
        /// <param name="ElpTime">查看附录1</param>
        /// <param name="pnRecRemark">查看附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadRecWithPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadRecWithPoint(uint nPortIndex, uint nNetID, ref uint pnLoadP, String szRecSource, ref SYSTEMTIME ElpTime, ref uint pnRecRemark);




        /// <summary>
        /// 查询门口机的最新事件记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">查看附录1</param>
        /// <param name="ElpTime">查看附录1</param>
        /// <param name="pnRecRemark">查看附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadNewRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadNewRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, ref SYSTEMTIME lpTime, ref uint pnRecRemark);





        /// <summary>
        /// 读取门口机的手动开门允许时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadManualTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadManualTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 读取门口机（区域）红外、门磁布防时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadWatchTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadWatchTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 读取室内灯光长开时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadIndoorLightTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadIndoorLightTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);





        /// <summary>
        /// 读取室外灯光时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szTimeSlot">>HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)
        ///HH：MM (起始)→HH:MM (结束)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadOutdoorLightTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadOutdoorLightTime(uint nPortIndex, uint nNetID, StringBuilder szTimeSlot);




        /// <summary>
        /// 远程监控门口机
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnWorkState">返回SM 的工作状态</param>
        /// <param name="pnLineState">返回SM 监控线路的状态</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadCtrlState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadCtrlState(uint nPortIndex, uint nNetID, ref uint pnWorkState, ref uint pnLineState);





        /// <summary>
        /// 读取门口机工作特性参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnCtrlParam1">返回门碰开关，红外状态控制字</param>
        /// <param name="pnCtrlParam2">返回门锁继电器的执行时间（0.1 秒为单位）；</param>
        /// <param name="pnDoorRelayDelay">返回开门等待进入的延时时间（0.1 秒为单位）；</param>
        /// <param name="pnOpenedRelayDelay">返回红外报警发生至确认的延时时间（0.1 秒为单位）；</param>
        /// <param name="pnIrSureDelay">返回安防报警驱动延时（0.1 秒为单位）；</param>
        /// <param name="pnIcDelay">返回第二控制字</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadCtrlParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadCtrlParam(uint nPortIndex, uint nNetID, ref uint pnCtrlParam1, ref uint pnCtrlParam2, ref uint pnDoorRelayDelay, ref uint pnOpenedRelayDelay, ref uint pnIrSureDelay, ref uint pnIcDelay);




       /// <summary>
        /// 读取授权数量
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nUserCount">返回门授权数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadUserCount(uint nPortIndex, uint nNetID, out uint nUserCount);



       /// <summary>
       /// 根据卡号读取用户
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="cInfo">用户信息</param>
       /// <returns>0卡号已存在，229卡号不存在，其他为操作失败</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadUserByCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadUserByCardNo(uint nPortIndex, uint nNetID, string cInfo);




        /// <summary>
        /// 通过用户位置查询用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="m_nReadPoint">位置</param>
        /// <param name="szCardNo">卡号</param>
        /// <returns>=0读取成功，卡号为：szCardNo。=229卡号不存在</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadUserByPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadUserByPoint(uint nPortIndex, uint nNetID, uint m_nReadPoint, StringBuilder szCardNo);




       
       /// <summary>
       /// 布防设置
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
       /// <param name="m_nArming">布防时间，0为解除布防</param>
       /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkRemoteArming", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkRemoteArming(uint nPortIndex, uint nNetID, uint m_nArming);





       /// <summary>
       /// 设置备用继电器
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
       /// <param name="m_nRelayInfo"></param>
       /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetExRelayDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetExRelayDelay(uint nPortIndex, uint nNetID, out IntPtr dd);




       /// <summary>
       /// 读取一条记录
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
       /// <param name="szRecSource">卡号</param>
       /// <param name="ElpTime">时间</param>
       /// <param name="pnRecRemark">标识</param>
       /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkReadOneRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, ref SYSTEMTIME ElpTime, ref uint pnRecRemark);


       


       /// <summary>
       /// 设置等待进入的延时时间
       /// </summary>
       /// <param name="nPortIndex">端口标识</param>
       /// <param name="nNetID">设备网络ID</param>
       /// <param name="nWaitDelay">延时时间</param>
       /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetWaitDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetWaitDelay(uint nPortIndex, uint nNetID, uint nWaitDelay);






       /// <summary>
       /// 设置红外报警延时时间
       /// </summary>
       /// <param name="nPortIndex">端口标识</param>
       /// <param name="nNetID">设备网络ID</param>
       /// <param name="nWaitDelay"></param>
       /// <returns>设备返回值</returns>
       [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "BkSetIrSureDelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BkSetIrSureDelay(uint nPortIndex, uint nNetID, uint nIrSureDelay);





    }
}
