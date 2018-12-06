using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// 消费机与发卡器API
    /// </summary>
    public static class CHD603S
    {



        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadVersion(uint nPortIndex, uint nNetID, byte[] szVersion);





        /// <summary>
        /// 读取卡号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCardNo">返回卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadCardNo(uint nPortIndex, uint nNetID, byte[] szCardNo);





        /// <summary>
        /// 读取卡类型
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="dwType"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadCardType", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadCardType(uint nPortIndex, uint nNetID, out Int16 dwType);





        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号,4字节(分高低字节)</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="iLiftOrConsume">判断初始化的是1、８、９、１０还是１、１１、１２、１３扇区</param>
        /// 说明：
        /// 1.必须初始化的扇区号为1、8、9、10号扇区
        ///2.该函数调用的时候,只需要调用一次即可,扇区号必须从1号扇区开始进行初始化;
        ///3.函数自动进行递归调用
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_InitPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_InitPassword(uint nPortIndex, uint nNetID, string pCardNo, int iSector, int iLiftOrConsume);





        /// <summary>
        /// 写金额
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pMoney">金额数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_WriteMoney", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_WriteMoney(uint nPortIndex, uint nNetID, string pCardNo, int iSector, string pMoney);





        /// <summary>
        /// 读金额
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pMoney">返回金额数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadMoney", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadMoney(uint nPortIndex, uint nNetID, string pCardNo, int iSector, out Int16 pMoney);





        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pMoney">金额数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_Recharge", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_Recharge(uint nPortIndex, uint nNetID, String pCardNo, int iSector, string pMoney);





        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pMoney">金额数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ChargeBack", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ChargeBack(uint nPortIndex, uint nNetID, string pCardNo, int iSector, string pMoney);





        /// <summary>
        /// 写金额/次数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pMoney">金额数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_WriteCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_WriteCount(uint nPortIndex, uint nNetID, String pCardNo, int iSector, String pMoney);





        /// <summary>
        /// 读金额
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="pCount">返回金额/次数</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadCount(uint nPortIndex, uint nNetID, String pCardNo, int iSector, out Int16 pCount);





        /// <summary>
        /// 写卡块数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="iBlockNo">块号</param>
        /// <param name="btData1">块号0传递的16字节数据，必须不为NULL</param>
        /// <param name="btData2">块号1传递的16字节数据</param>
        /// <param name="btData3">块号2传递的16字节数据</param>
        /// 说明:
        /// 1.	btData1传递的值必须不为NULL；
        ///2.	btData2、btData3传递NULL时，则表明块1、块2数据保留
        ///3.	该函数被调用时，函数自动根据传递的数据递归调用
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_WriteBlockData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_WriteBlockData(uint nPortIndex, uint nNetID, string pCardNo, int iSector, int iBlockNo, byte[] btData1, byte[] btData2, byte[] btData3);





        /// <summary>
        /// 读卡块数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pCardNo">卡号</param>
        /// <param name="iSector">扇区号</param>
        /// <param name="iBlockNo">块号</param>
        /// <param name="btData1">返回块号0的16字节数据</param>
        /// <param name="btData2">返回块号1的16字节数据</param>
        /// <param name="btData3">返回块号2的16字节数据</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadBlockData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadBlockData(uint nPortIndex, uint nNetID, String pCardNo, int iSector, int iBlockNo, byte[] btData1, byte[] btData2, byte[] btData3);





    }
}
