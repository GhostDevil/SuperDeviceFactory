using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHD815T_M3
    {




        /***************************************************************
       * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
         2．	执行LinkOn校验设备密码，确认设备通讯权限；
         3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
         4．	执行LinkOff取消设备通讯权限；
         5．	当程序关闭时使用ClosePort关闭端口。
       **************************************************************/





        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_LinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_LinkOn(uint nPortIndex, uint nNetID, string szDevPwd);





        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_LinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_LinkOff(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 修改访问密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetDevPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetDevPwd(uint nPortIndex, uint nNetID, string szDevPwd);





        /// <summary>
        /// 设置设备日期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">设置日期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpCard">用户卡号（’0’~’9’、’A’~’F’）</param>
        /// <param name="UserID">用户ID（’0’~’9’、’A’~’F’）</param>
        /// <param name="lpPsw">密码（’0’~’9’、’A’~’F’）</param>
        /// <param name="pNewDateTime">时间</param>
        /// <param name="iPrivilege">权限</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_AddoneUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_AddoneUser(uint nPortIndex, uint nNetID, string lpCard, string UserID, string lpPsw, ref SYSTEMTIME pNewDateTime, int iPrivilege);





        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpCard">用户卡号（’0’~’9’、’A’~’F’）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_DeleteOneUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_DeleteOneUser(uint nPortIndex, uint nNetID, string lpCard);





        /// <summary>
        /// 删除全部用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpCard">用户卡号（’0’~’9’、’A’~’F’）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_DeleteAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_DeleteAllUser(uint nPortIndex, uint nNetID, string lpCard);





        /// <summary>
        /// 设置设备参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bCfg_sGroundSensor">是否配置地感</param>
        /// <param name="bCfg_CardMachine">是否配置卡机</param>
        /// <param name="bCfg_View">是否配置显示屏</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetParameter(uint nPortIndex, uint nNetID, int bCfg_sGroundSensor, int bCfg_CardMachine, int bCfg_View);





        /// <summary>
        /// 设置设备属性
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bChannelAttA">通道属性</param>
        /// <param name="bApplyAttA">是否收费</param>
        /// <param name="btSrcA">源区域</param>
        /// <param name="btDestnationA">目的区域</param>
        /// <param name="bConfigSensorA">是否配置地感</param>
        /// <param name="iMachineA">是否配置卡机</param>
        /// <param name="iViewA">是否配置显示屏</param>
        /// <param name="bChannelAttB">通道属性</param>
        /// <param name="bApplyAttB">是否收费</param>
        /// <param name="btSrcB">源区域</param>
        /// <param name="btDestnationB">目的区域</param>
        /// <param name="bConfigSensorB">是否配置地感</param>
        /// <param name="iMachineB">是否配置卡机</param>
        /// <param name="iViewB">是否配置显示屏</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetDeviceAtt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetDeviceAtt(uint nPortIndex, uint nNetID, int bChannelAttA, int bApplyAttA, byte btSrcA, byte btDestnationA, int bConfigSensorA, int iMachineA, int iViewA,
 int bChannelAttB, int bApplyAttB, byte btSrcB, byte btDestnationB, int bConfigSensorB, int iMachineB, int iViewB)
;





        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="PortIndex"></param>
        /// <param name="NetID"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_DeleteAllRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_DeleteAllRecord(uint PortIndex, uint NetID);





        /// <summary>
        /// 恢复记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRecord">记录数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_RecoverAllRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_RecoverAllRecord(uint nPortIndex, uint nNetID, int nRecord);





        /// <summary>
        /// 远程抬杠
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道号</param>
        /// <param name="iOperatorID">操作员ID（’0’~’9’、’A’~’F’）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_RemoteControlLevel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_RemoteControlLevel(uint nPortIndex, uint nNetID, int iChannel, string iOperatorID);





        /// <summary>
        /// 设置立即显示
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道</param>
        /// <param name="btInfo">要显示的数据</param>
        /// <param name="iInfoLength">数据长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetViewFast", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetViewFast(uint nPortIndex, uint nNetID, int iChannel, string btInfo, int iInfoLength);





        /// <summary>
        /// 设置固定显示
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道</param>
        /// <param name="btSection">段</param>
        /// <param name="btInfo">要显示的数据</param>
        /// <param name="iInfoLength">数据长度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetViewFixed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetViewFixed(uint nPortIndex, uint nNetID, int iChannel, int btSection, string btInfo, int iInfoLength);





        /// <summary>
        /// 远程播放语音
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道号</param>
        /// <param name="nItem">文件号(1~6)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_RemotePlayMedia", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_RemotePlayMedia(uint nPortIndex, uint nNetID, int iChannel, int nItem);





        /// <summary>
        /// 远程播放金额
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道号</param>
        /// <param name="iYuan">元</param>
        /// <param name="iJiao">角(0~9)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_RemoteMoney", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_RemoteMoney(uint nPortIndex, uint nNetID, int iChannel, int iYuan, int iJiao);





        /// <summary>
        /// 播放剩余有效期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iChannel">通道号</param>
        /// <param name="iCount">剩余有效期</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_RemoteRemainDate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_RemoteRemainDate(uint nPortIndex, uint nNetID, int iChannel, int iCount);





        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 读取记录参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回用户信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadRecordParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadRecordParameter(uint nPortIndex, uint nNetID, byte[] lpUserInfo);




        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetUpdateParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetUpdateParameter(uint nPortIndex,uint nNetID, int nParameter);


         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadApplyStd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadApplyStd(uint nPortIndex, uint nNetID, uint iRegion, int iCarType, StringBuilder lpUserInfo);


        


        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_SetApplyStd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_SetApplyStd(uint nPortIndex, uint nNetID,  int iCarType, int iPerDateCeiling, 
	int iFirstPeriodOfParkingFree,  int iFirstPeriodOfApply,   int iFirstPeriodOfApplyStd,   int iApply_Interval,
	int iInterval_Apply_Std, SYSTEMTIME m_PeakHour_Period1, SYSTEMTIME m_PeakHour_Period2, int iPeakHour_ParkingFree, 
	int iPeakHour_First,  int iPeakHour_ApplyStd,  int iPeakHour_Interval,  int iPeakHour_Interval_ApplyStd,  int iPeakHour_Ceiling);





        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadRecordSerial", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadRecordSerial(uint nPortIndex, uint nNetID, StringBuilder lpUserInfo);





        /// <summary>
        /// 附带顺序号读取记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadHistoryRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadHistoryRecord(uint nPortIndex, uint nNetID, StringBuilder lpUserInfo);





        /// <summary>
        /// 查询最新事件
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadNewEvent", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadNewEvent(uint nPortIndex, uint nNetID, StringBuilder lpUserInfo);





        /// <summary>
        /// 读取用户数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpCount">返回用户数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadUserCount(uint nPortIndex, uint nNetID, out int lpCount);





        /// <summary>
        /// 读取指定位置的用户资料
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iPos">指定位置</param>
        /// <param name="lpUserInfo">返回指定位置的用户信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadUserInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadUserInfo(uint nPortIndex, uint nNetID, int iPos, byte[] lpUserInfo);





        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="LPCard">返回版本号</param>
        /// <param name="lpUserInfo">返回用户信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_UserCardExistsOrNot", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_UserCardExistsOrNot(uint nPortIndex, uint nNetID, String LPCard, byte[] lpUserInfo);





        /// <summary>
        /// 读取设备参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpInfo">返回设备参数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadParameter(uint nPortIndex, uint nNetID, StringBuilder lpInfo);





        /// <summary>
        /// 读取设备属性
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回设备属性</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadDeviceAttributes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadDeviceAttributes(uint nPortIndex, uint nNetID, StringBuilder lpUserInfo);





        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD815T_M3_ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD815T_M3_ReadVersion(uint nPortIndex, uint nNetID, byte[] szVersion);







    }
}
