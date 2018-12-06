using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// 发卡器API
    /// </summary>
    public static class CHDCardReader
    {
        /// <summary>
        /// 读取卡号（发卡器）
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="szCardNo">返回卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CRReadCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CRReadCardNo(uint nPortIndex, byte[] szCardNo);
        /// <summary>
        /// 清除缓冲区（发卡器）
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CRClearBuffer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CRClearBuffer(uint nPortIndex);
        /// <summary>
        /// 读取卡号(读写器)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">网络标识</param>
        /// <param name="szCardNo">返回卡号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ConsumeCard_ReadCardNo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConsumeCard_ReadCardNo(uint nPortIndex, int nNetID, ref string szCardNo);

    }
}
