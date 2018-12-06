using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHDT5
    {
        /*************************************************
       调用说明
       1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
       2．	执行LinkOn校验设备密码，确认设备通讯权限；
       3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
       4．	执行LinkOff取消设备通讯权限；
       5．	当程序关闭时使用ClosePort关闭端口。
       *************************************************/


        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_LinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_LinkOn(uint  nPortIndex, uint  nNetID, String szDevPwd);



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_LinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_LinkOff(uint  nPortIndex, uint  nNetID);



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetDevPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetDevPwd(uint  nPortIndex, uint  nNetID, string szDevPwd);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">经过初始化的系统时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetDateTime(uint  nPortIndex, uint  nNetID, ref SYSTEMTIME pNewDateTime);



        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nBaudrate">波特率代码，=0 9600，=1 19200，=2 38400， =3 115200</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetBaudrate(uint  nPortIndex, uint  nNetID, uint  nBaudrate);



        /// <summary>
        /// 设置电梯参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nID">电梯编号</param>
        /// <param name="nGroundFloor">电梯地上楼层</param>
        /// <param name="nUnderGroundFloor">电梯地下楼层</param>
        /// <param name="nControlMode">楼层继电器控制方式</param>
        /// <param name="bProtocolControl">协议控制电梯</param>
        /// <param name="nFloorTime">楼层继电器时间</param>
        /// <param name="nAlarmTime">报警继电器时间</param>
        /// <param name="nSystemTime">对讲联动系统继电器延时时间</param>
        /// <param name="bAuto">紧急自动招梯</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetLiftParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetLiftParameter(uint  nPortIndex, uint  nNetID, int nID, 	int nGroundFloor, int nUnderGroundFloor, int nControlMode, int bProtocolControl,	int nFloorTime, int  nAlarmTime, int  nSystemTime, int bAuto);



        /// <summary>
        /// 设置时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nTableID">表序号</param>
        /// <param name="nHourStart1">第一段起始时</param>
        /// <param name="nMinuteStart1">第一段起始分钟</param>
        /// <param name="nHourEnd1">第一段结束时</param>
        /// <param name="nMinuteEnd1">第一段结束分钟</param>
        /// <param name="nHourStart2">第二段起始时</param>
        /// <param name="nMinuteStart2">第二段起始分钟</param>
        /// <param name="nHourEnd2">第二段起始分钟</param>
        /// <param name="nMinuteEnd2">第二段结束分钟</param>
        /// <param name="nHourStart3">第三段起始时</param>
        /// <param name="nMinuteStart3">第三段起始分钟</param>
        /// <param name="nHourEnd3">第三段结束时</param>
        /// <param name="nMinuteEnd3">第三段结束分钟</param>
        /// <param name="nHourStart4">第四段起始时</param>
        /// <param name="nMinuteStart4">第四段起始分钟</param>
        /// <param name="nHourEnd4">第四段起始时</param>
        /// <param name="nMinuteEnd4">第四段结束分钟</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetPeriod", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetPeriod(uint  nPortIndex, uint  nNetID, int  nTableID,	int  nHourStart1, int  nMinuteStart1, int  nHourEnd1, int  nMinuteEnd1, int  nHourStart2, int  nMinuteStart2, int  nHourEnd2, int  nMinuteEnd2, int  nHourStart3, int  nMinuteStart3, int  nHourEnd3, int  nMinuteEnd3, int  nHourStart4, int  nMinuteStart4, int  nHourEnd4, int  nMinuteEnd4);



        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nTableID">表序号</param>
        /// <param name="nHourStart1">返回第一段起始时</param>
        /// <param name="nMinuteStart1">返回第一段起始分钟</param>
        /// <param name="nHourEnd1">返回第一段结束时</param>
        /// <param name="nMinuteEnd1">返回第一段结束分钟</param>
        /// <param name="nHourStart2">返回第二段起始时</param>
        /// <param name="nMinuteStart2">返回第二段起始分钟</param>
        /// <param name="nHourEnd2">返回第二段起始分钟</param>
        /// <param name="nMinuteEnd2">返回第二段结束分钟</param>
        /// <param name="nHourStart3">返回第三段起始时</param>
        /// <param name="nMinuteStart3">返回第三段起始分钟</param>
        /// <param name="nHourEnd3">返回第三段结束时</param>
        /// <param name="nMinuteEnd3">返回第三段结束分钟</param>
        /// <param name="nHourStart4">返回第四段起始时</param>
        /// <param name="nMinuteStart4">返回第四段起始分钟</param>
        /// <param name="nHourEnd4">返回第四段起始分钟</param>
        /// <param name="nMinuteEnd4">返回第四段结束分钟</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadPeriod", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadPeriod(uint nPortIndex, uint nNetID, int nTable, out int nHourStart1, out int nMinuteStart1, out int nHourEnd1, out int nMinuteEnd1, out int nHourStart2, out int nMinuteStart2, out int nHourEnd2, out int nMinuteEnd2, out int nHourStart3, out int nMinuteStart3, out int nHourEnd3, out int nMinuteEnd3, out int nHourStart4, out int nMinuteStart4, out int nHourEnd4, out int nMinuteEnd4);



        /// <summary>
        /// 设置星期时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nWeek">星期几</param>
        /// <param name="nPrivilegesIndex1">第1类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex2">第2类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex3">第3类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex4">第4类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex5">第5类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex6">第6类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex7">第7类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex8">第8类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex9">第9类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex10">第10类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex11">第11类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex12">第12类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex13">第13类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex14">第14类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex15">第15类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex16">第16类权限时段索引 =0-31</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetWeek", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetWeek(uint  nPortIndex, uint  nNetID, int  nWeek,int  nPrivilegesIndex1, int  nPrivilegesIndex2, int nPrivilegesIndex3, int  nPrivilegesIndex4, int  nPrivilegesIndex5, int  nPrivilegesIndex6, int  nPrivilegesIndex7, int  nPrivilegesIndex8, int  nPrivilegesIndex9, int  nPrivilegesIndex10, int  nPrivilegesIndex11, int  nPrivilegesIndex12, int  nPrivilegesIndex13, int  nPrivilegesIndex14, int  nPrivilegesIndex15, int  nPrivilegesIndex16);



        /// <summary>
        /// 读取星期时段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nWeek">星期几</param>
        /// <param name="nPrivilegesIndex1">返回第1类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex2">返回第2类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex3">返回第3类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex4">返回第4类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex5">返回第5类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex6">返回第6类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex7">返回第7类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex8">返回第8类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex9">返回第9类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex10">返回第10类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex11">返回第11类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex12">返回第12类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex13">返回第13类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex14">返回第14类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex15">返回第15类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex16">返回第16类权限时段索引 =0-31</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadWeek", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadWeek(uint nPortIndex, uint nNetID, int nWeek, out int nPrivilegesIndex1, out int nPrivilegesIndex2, out int nPrivilegesIndex3, out int nPrivilegesIndex4, out int nPrivilegesIndex5, out int nPrivilegesIndex6, out int nPrivilegesIndex7, out int nPrivilegesIndex8, out int nPrivilegesIndex9, out int nPrivilegesIndex10, out int nPrivilegesIndex11, out int nPrivilegesIndex12, out int nPrivilegesIndex13, out int nPrivilegesIndex14, out int nPrivilegesIndex15, out int nPrivilegesIndex16);



        /// <summary>
        /// 设置特殊时段
        /// </summary>
        /// <param name="nPortIndex">设置特殊时段</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nMonth">月份</param>
        /// <param name="nDay">日</param>
        /// <param name="nPrivilegesIndex1">第1类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex2">第2类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex3">第3类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex4">第4类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex5">第5类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex6">第6类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex7">第7类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex8">第8类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex9">第9类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex10">第10类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex11">第11类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex12">第12类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex13">第13类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex14">第14类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex15">第15类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex16">第16类权限时段索引 =0-31</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetSpecial", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetSpecial(uint  nPortIndex, uint  nNetID, int  nMonth, int  nDay,int  nPrivilegesIndex1, int  nPrivilegesIndex2, int  nPrivilegesIndex3, int  nPrivilegesIndex4, int  nPrivilegesIndex5, int  nPrivilegesIndex6, int  nPrivilegesIndex7, int  nPrivilegesIndex8, int  nPrivilegesIndex9, int  nPrivilegesIndex10, int  nPrivilegesIndex11, int  nPrivilegesIndex12, int  nPrivilegesIndex13, int  nPrivilegesIndex14, int  nPrivilegesIndex15, int nPrivilegesIndex16);



        /// <summary>
        /// 读取特殊时段
        /// </summary>
        /// <param name="nPortIndex">设置特殊时段</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nMonth">月份</param>
        /// <param name="nDay">日</param>
        /// <param name="nPrivilegesIndex1">返回第1类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex2">返回第2类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex3">返回第3类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex4">返回第4类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex5">返回第5类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex6">返回第6类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex7">返回第7类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex8">返回第8类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex9">返回第9类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex10">返回第10类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex11">返回第11类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex12">返回第12类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex13">返回第13类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex14">返回第14类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex15">返回第15类权限时段索引 =0-31</param>
        /// <param name="nPrivilegesIndex16">返回第16类权限时段索引 =0-31</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadSpecial", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadSpecial(uint nPortIndex, uint nNetID,  int nMonth,   int nDay, out int nPrivilegesIndex1, out int nPrivilegesIndex2, out int nPrivilegesIndex3, out int nPrivilegesIndex4, out int nPrivilegesIndex5, out int nPrivilegesIndex6, out int nPrivilegesIndex7, out int nPrivilegesIndex8, out int nPrivilegesIndex9, out int nPrivilegesIndex10, out int nPrivilegesIndex11, out int nPrivilegesIndex12, out int nPrivilegesIndex13, out int nPrivilegesIndex14, out int nPrivilegesIndex15, out int nPrivilegesIndex16);


        /// <summary>
        /// 控制远程电梯
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="AdministratorID">管理员ID('0'-'9', 'A'-'F')</param>
        /// <param name="nLiftID">电梯编号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ControlRemoteLift", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ControlRemoteLift(uint  nPortIndex, uint  nNetID, string AdministratorID, int  nLiftID);



        /// <summary>
        /// 续费
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="pStartDateTime">起始时间</param>
        /// <param name="pEndDateTime">结束时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_Renewals", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_Renewals(uint  nPortIndex, uint  nNetID,string ResidentCode, ref SYSTEMTIME pStartDateTime, ref SYSTEMTIME pEndDateTime);



        /// <summary>
        /// 设置有效期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nDays">天数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_SetValidity", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_SetValidity(uint  nPortIndex, uint  nNetID, int  nDays);



        /// <summary>
        /// 增加住户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="pStartDateTime">住户代码</param>
        /// <param name="pEndDateTime">结束时间</param>
        /// <param name="nPrivileges">权限</param>
        /// <param name="Pwd">密码</param>
        /// <param name="UnitID1">单元号</param>
        /// <param name="StartFloor1">起始楼层</param>
        /// <param name="EndFloor1">结束楼层</param>
        /// <param name="UnitID2">单元号</param>
        /// <param name="StartFloor2">起始楼层</param>
        /// <param name="EndFloor2">结束楼层</param>
        /// <param name="UnitID3">单元号</param>
        /// <param name="StartFloor3">起始楼层</param>
        /// <param name="EndFloor3">结束楼层</param>
        /// <param name="UnitID4">单元号</param>
        /// <param name="StartFloor4">起始楼层</param>
        /// <param name="EndFloor4">结束楼层</param>
        /// <param name="UnitID5">单元号</param>
        /// <param name="StartFloor5">起始楼层</param>
        /// <param name="EndFloor5">结束楼层</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_AddResident", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_AddResident(uint nPortIndex, uint nNetID, string ResidentCode, ref SYSTEMTIME pStartDateTime, ref SYSTEMTIME pEndDateTime, int nPrivileges, string Pwd, int UnitID1, int StartFloor1, int EndFloor1, int UnitID2, int StartFloor2, int EndFloor2, int UnitID3, int StartFloor3, int EndFloor3, int UnitID4, int StartFloor4, int EndFloor4, int UnitID5, int StartFloor5, int EndFloor5);



        /// <summary>
        /// 删除住户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_DeleteResident", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_DeleteResident(uint  nPortIndex, uint  nNetID, string ResidentCode);



        /// <summary>
        /// 删除所有住户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="AdministratorID">管理员ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_DeleteALLResident", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_DeleteALLResident(uint  nPortIndex, uint  nNetID, string AdministratorID);



        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">标志1</param>
        /// <param name="ResidentCode">用户代码</param>
        /// <param name="Card">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_AddUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_AddUser(uint  nPortIndex, uint  nNetID, int nFlag, string ResidentCode, string Card);



        /// <summary>
        /// 挂失卡号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">标志2</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="Card">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_InvaildCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_InvaildCard(uint  nPortIndex, uint  nNetID, int nFlag, String ResidentCode, string Card);



        /// <summary>
        /// 解挂卡号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">标志3</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="Card">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_VaildCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_VaildCard(uint nPortIndex, uint nNetID, int nFlag, String ResidentCode, String Card);



        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFlag">标志4</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="Card">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_NullifyUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_NullifyUser(uint  nPortIndex, uint  nNetID, int nFlag, String ResidentCode,string Card);



        /// <summary>
        /// 删除所有记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_DeleteAllRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_DeleteAllRecord(uint  nPortIndex, uint  nNetID);



        /// <summary>
        /// 恢复所有记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRecord">记录数1-65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_RecoverAllRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_RecoverAllRecord(uint  nPortIndex, uint  nNetID, int nRecord);



        /// <summary>
        /// 读取系统时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">返回时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadDateTime(uint  nPortIndex, uint  nNetID, out SYSTEMTIME pNewDateTime);



        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadVersion(uint  nPortIndex, uint  nNetID, byte[] szVersion);



        /// <summary>
        /// 读取电梯信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLiftID">返回电梯编号</param>
        /// <param name="nGroundFloor">返回电梯地上楼层</param>
        /// <param name="nUnderGroundFloor">返回电梯地下楼层</param>
        /// <param name="nControlMode">返回楼层继电器控制方式</param>
        /// <param name="bProtocolControl">返回协议控制电梯</param>
        /// <param name="nFloorTime">返回楼层继电器时间</param>
        /// <param name="nAlarmTime">返回报警继电器时间</param>
        /// <param name="nRelayTime">返回对讲联动系统继电器延时时间</param>
        /// <param name="bSerious">返回紧急自动招梯</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadLiftInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadLiftInfo(uint nPortIndex, uint nNetID, out int nLiftID, out int nGroundFloor, out int nUnderGroundFloor, out int nControlMode, out int bProtocolControl, out int nFloorTime, out int nAlarmTime, out int nRelayTime, out int bSerious);



        /// <summary>
        /// 读取电梯状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFloor">返回楼层号</param>
        /// <param name="nStatus">电梯状态</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadLiftStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadLiftStatus(uint  nPortIndex, uint  nNetID, ref int nFloor, ref int nStatus);



        /// <summary>
        /// 读取未读记录数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pInfo">没有读取的记录条数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadRemaining", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadRemaining(uint  nPortIndex, uint  nNetID, out int pInfo);



        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="EventCode">事件代码</param>
        /// <param name="stTime">事件时间</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="UserCard">用户卡号</param>
        /// <param name="nFlag">刷卡楼层</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadOneRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadOneRecord(uint  nPortIndex, uint  nNetID, out int EventCode, out SYSTEMTIME stTime, byte[] ResidentCode, byte[] UserCard, out int nFlag);



        /// <summary>
        /// 读取已授权的用户数和住户数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nUserCount">返回用户数量</param>
        /// <param name="nResidentCount">返回住户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadAuthorized", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadAuthorized(uint  nPortIndex, uint  nNetID, out int nUserCount, out int nResidentCount);



        /// <summary>
        /// 增加住户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="pStartDateTime">起始时间</param>
        /// <param name="pEndDateTime">结束时间</param>
        /// <param name="nPrivileges">权限</param>
        /// <param name="Pwd">密码</param>
        /// <param name="UnitID1">单元号</param>
        /// <param name="StartFloor1">起始楼层</param>
        /// <param name="EndFloor1">结束楼层</param>
        /// <param name="UnitID2">单元号</param>
        /// <param name="StartFloor2">起始楼层</param>
        /// <param name="EndFloor2">结束楼层</param>
        /// <param name="UnitID3">单元号</param>
        /// <param name="StartFloor3">起始楼层</param>
        /// <param name="EndFloor3">结束楼层</param>
        /// <param name="UnitID4">单元号</param>
        /// <param name="StartFloor4">起始楼层</param>
        /// <param name="EndFloor4">结束楼层</param>
        /// <param name="UnitID5">单元号</param>
        /// <param name="StartFloor5">起始楼层</param>
        /// <param name="EndFloor5">结束楼层</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadResidentInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadResidentInfo(uint  nPortIndex, uint  nNetID,  string ResidentCode, out SYSTEMTIME pStartDateTime, out SYSTEMTIME pEndDateTime, out int nPrivileges, StringBuilder Pwd, out int UnitID1, out int StartFloor1, out int EndFloor1, 
out int UnitID2, out int StartFloor2, out int EndFloor2, out int UnitID3, out int StartFloor3, out int EndFloor3, out int UnitID4, out int StartFloor4, out int EndFloor4, 
out int UnitID5, out int StartFloor5, out int EndFloor5);



        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="Cardinfo">用户卡号</param>
        /// <param name="ResidentCode">住户代码</param>
        /// <param name="nStatus">状态</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "T5_ReadUserInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int T5_ReadUserInfo(uint nPortIndex, uint nNetID, string Cardinfo, byte[] ResidentCode, out int nStatus);
    }
}
