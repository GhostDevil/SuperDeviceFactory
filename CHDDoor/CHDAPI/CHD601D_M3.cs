using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHD601D_M3
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
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_LinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_LinkOn(uint nPortIndex, uint nNetID, String szDevPwd);





        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_LinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_LinkOff(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设备系统密码 10个('0'..'9')ASCII字符串
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetDevPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetDevPwd(uint nPortIndex, uint nNetID, String szDevPwd);





        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回设备版本</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadVersion(uint nPortIndex, uint nNetID, byte[] szVersion);





        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 删除所有商品条码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_DeleteAllCode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_DeleteAllCode(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 设置商品条码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCode">商品条码12个('0'..'9'、‘A’..’F’)ASCII字符串</param>
        /// <param name="pPrice">商品价格</param>
        /// <param name="pName">商品名称</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_EditCode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_EditCode(uint nPortIndex, uint nNetID, byte[] pCode, double pPrice, byte[] pName);





        /// <summary>
        /// 设置就餐时间段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iNumber">时段号</param>
        /// <param name="Period1">批次1</param>
        /// <param name="Period2">批次2</param>
        /// <param name="Period3">批次3</param>
        /// <param name="Period4">批次4</param>
        /// <param name="Period5">批次5</param>
        /// <param name="Period6">批次6</param>
        /// <param name="Period7">批次7</param>
        /// <param name="Period8">批次8</param>
        /// <param name="Period9">批次9</param>
        /// <param name="Period10">批次10</param>
        /// <param name="Period11">批次11</param>
        /// <param name="Period12">批次12</param>
        /// <param name="Period13">批次13</param>
        /// <param name="Period14">批次14</param>
        /// <param name="Period15">批次15</param>
        /// <param name="Period16">批次16</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_EatPeriod", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_EatPeriod(uint nPortIndex, uint nNetID, int iNumber, ref SYSTEMTIME Period1, ref SYSTEMTIME Period2,
                            ref SYSTEMTIME Period3, ref SYSTEMTIME Period4,
                            ref SYSTEMTIME Period5, ref SYSTEMTIME Period6,
                            ref SYSTEMTIME Period7, ref SYSTEMTIME Period8,
                            ref SYSTEMTIME Period9, ref SYSTEMTIME Period10,
                            ref SYSTEMTIME Period11, ref SYSTEMTIME Period12,
                            ref SYSTEMTIME Period13, ref SYSTEMTIME Period14,
                            ref SYSTEMTIME Period15, ref SYSTEMTIME Period16)
;





        /// <summary>
        /// 分段定值金额设定
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="Period1">批次1</param>
        /// <param name="Period2">批次2</param>
        /// <param name="pPrice1">价格1</param>
        /// <param name="Period3">批次3</param>
        /// <param name="Period4">批次4</param>
        /// <param name="pPrice2">价格2</param>
        /// <param name="Period5">批次5</param>
        /// <param name="Period6">批次6</param>
        /// <param name="pPrice3">价格3</param>
        /// <param name="Period7">批次7</param>
        /// <param name="Period8">批次8</param>
        /// <param name="pPrice4">价格4</param>
        /// <param name="Period9">批次9</param>
        /// <param name="Period10">批次10</param>
        /// <param name="pPrice5">价格5</param>
        /// <param name="Period11">批次11</param>
        /// <param name="Period12">批次12</param>
        /// <param name="pPrice6">价格6</param>
        /// <param name="Period13">批次13</param>
        /// <param name="Period14">批次14</param>
        /// <param name="pPrice7">价格7</param>
        /// <param name="Period15">批次15</param>
        /// <param name="Period16">批次16</param>
        /// <param name="pPrice8">价格8</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetMoney", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetMoney(uint nPortIndex, uint nNetID,
                ref SYSTEMTIME Period1, ref SYSTEMTIME Period2, String pPrice1, ref SYSTEMTIME Period3, ref SYSTEMTIME Period4, String pPrice2,
                ref SYSTEMTIME Period5, ref SYSTEMTIME Period6, String pPrice3,
            ref SYSTEMTIME Period7, ref SYSTEMTIME Period8, String pPrice4,
                ref SYSTEMTIME Period9, ref SYSTEMTIME Period10, String pPrice5,
                ref SYSTEMTIME Period11, ref SYSTEMTIME Period12, String pPrice6,
                ref SYSTEMTIME Period13, ref SYSTEMTIME Period14, String pPrice7,
                ref SYSTEMTIME Period15, ref SYSTEMTIME Period16, String pPrice8);






        /// <summary>
        /// 系统参数设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iSection">区位ID</param>
        /// <param name="iDevNo">设备ID</param>
        /// <param name="iBautrate">波特率</param>
        /// <param name="iConsumeMode">消费模式</param>
        /// <param name="iOperatorMode">操作员模式</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetSysParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetSysParameter(uint nPortIndex, uint nNetID, int iSection, int iDevNo, String iBautrate, int iConsumeMode, int iOperatorMode);





        /// <summary>
        /// 消费限制设定
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bCardTypeConsumeLimited">卡类消费限制</param>
        /// <param name="iLimitedCard">限制卡类</param>
        /// <param name="bLimitedPeriod">时段消费限制</param>
        /// <param name="iMealsLimited">餐限次</param>
        /// <param name="iMoneyLimited">餐限额</param>
        /// <param name="iDayCountLimited">天限次</param>
        /// <param name="iDayMoneyLimited">天限额</param>
        /// <param name="bNameList">黑白名单支持</param>
        /// <param name="bCashPayed">现金支付</param>
        /// <param name="bDisCount">打折支付</param>
        /// <param name="bNeedPsw">超额秘密支付</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_LimitedConsume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_LimitedConsume(uint nPortIndex, uint nNetID, int bCardTypeConsumeLimited, int iLimitedCard, int bLimitedPeriod, int iMealsLimited, string iMoneyLimited, int iDayCountLimited, String iDayMoneyLimited, int bNameList, int bCashPayed, int bDisCount, int bNeedPsw);





        /// <summary>
        /// 补贴参数设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="bAccount">是否补贴累加</param>
        /// <param name="bClear">是否补贴清零</param>
        /// <param name="bConfig">是否补贴确认</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetSubsides", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetSubsides(uint nPortIndex, uint nNetID, int bAccount, int bClear, int bConfig);





        /// <summary>
        /// 折扣设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iCardType">卡类型</param>
        /// <param name="iDiscount">折扣率</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetDisCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetDisCount(uint nPortIndex, uint nNetID, int iCardType, int iDiscount);





        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNumber">卡号</param>
        /// <param name="pUserID">工号</param>
        /// <param name="pPsw">秘密</param>
        /// <param name="pTime">有效期</param>
        /// <param name="iCardType">卡类</param>
        /// <param name="pName">姓名</param>
        /// <param name="iNumer">批次</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_AddOneUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_AddOneUser(uint nPortIndex, uint nNetID,
                            string pCardNumber, string pUserID,
                            string pPsw, ref SYSTEMTIME pTime,
                            int iCardType, byte[] pName, int iNumer)
;





        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iIndex">索引</param>
        /// <param name="pInfo">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_DeleteUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_DeleteUser(uint nPortIndex, uint nNetID,
                            int iIndex, string pInfo)
;





        /// <summary>
        /// 挂失/解挂
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iIndex">标识</param>
        /// <param name="iCardNo">卡片数</param>
        /// <param name="pInfo">卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetLoss", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetLoss(uint nPortIndex, uint nNetID,
                        int iIndex, int iCardNo, String pInfo)
;





        /// <summary>
        /// 管理员设置
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iIndex">权限索引</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="pAdmin">管理员代码</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetAdmin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetAdmin(uint nPortIndex, uint nNetID, int iIndex, String pCardNo, string pAdmin);





        /// <summary>
        /// 补贴金额写入
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pUserID">用户ID</param>
        /// <param name="index">1</param>
        /// <param name="pMoney">补贴金额</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_SetMoneyOperat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_SetMoneyOperat(uint nPortIndex, uint nNetID, String pUserID, int index, String pMoney);






        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_InitRecorde", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_InitRecorde(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpCount">返回用户数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadUserCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadUserCount(uint nPortIndex, uint nNetID, out int lpCount);







        /// <summary>
        /// 读取商品条码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pConsumeCard">商品条码</param> 
        /// <param name="lpInfo">返回21字节信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadConsumeCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadConsumeCard(uint nPortIndex, uint nNetID, String pConsumeCard, byte[] lpInfo);





        /// <summary>
        /// 读取批次时间段
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iBatch">批次</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadBatch", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadBatch(uint nPortIndex, uint nNetID, int iBatch, byte[] lpInfo);





        /// <summary>
        /// 读取消费限制信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadConsumeLimited", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadConsumeLimited(uint nPortIndex, uint nNetID, StringBuilder lpInfo);





        /// <summary>
        /// 读取定值模式金额
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadFixed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadFixed(uint nPortIndex, uint nNetID, byte[] lpInfo);





        /// <summary>
        /// 读取管理员信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pAdmin">管理员号</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadAdmin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadAdmin(uint nPortIndex, uint nNetID, String pAdmin, StringBuilder lpInfo);





        /// <summary>
        /// 读记录参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpUserInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadRecordParameter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadRecordParameter(uint nPortIndex, uint nNetID, StringBuilder lpUserInfo);





        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadOneRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadOneRecord(uint nPortIndex, uint nNetID, byte[] lpInfo);





        /// <summary>
        /// 读记录应答
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iRecord">记录号</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadRecordAck", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadRecordAck(uint nPortIndex, uint nNetID, int iRecord, StringBuilder lpInfo);





        /// <summary>
        /// 按记录指针读取
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iRecord">记录指针</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadRecordByPointer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadRecordByPointer(uint nPortIndex, uint nNetID, int iRecord, byte[] lpInfo);





        /// <summary>
        /// 按ID读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pUserID">用户ID</param>
        /// <param name="lpInfo">返回信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadUserID(uint nPortIndex, uint nNetID, string pUserID, byte[] lpInfo);





        /// <summary>
        /// 按卡号返回信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="lpInfo">返回用户信息</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_ReadCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_ReadCardNo(uint nPortIndex, uint nNetID, string pCardNo, StringBuilder lpInfo);





        /// <summary>
        /// 移动记录指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iPointer">记录指针号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "Consume_MovePointer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Consume_MovePointer(uint nPortIndex, uint nNetID, int iPointer)
;





    }
}
