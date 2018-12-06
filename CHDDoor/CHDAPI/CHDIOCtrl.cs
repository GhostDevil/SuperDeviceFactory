using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// 防区控制器API
    /// </summary>
    public static class CHDIOCtrl
    {


        /***************************************************************
      * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
        2．	执行LinkOn校验设备密码，确认设备通讯权限；
        3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
        4．	执行LinkOff取消设备通讯权限；
        5．	当程序关闭时使用ClosePort关闭端口。
        **************************************************************/



        /// <summary>
        /// 设置日期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 读取日期
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pNewDateTime">时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pNewDateTime);





        /// <summary>
        /// 设置 16通道输入信号的有效电平
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInputLevel">有效电平1</param>
        /// <param name="nInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetInputLevel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetInputLevel(uint nPortIndex, uint nNetID, byte nInputLevel, byte nInputLevel1);





        /// <summary>
        /// 读取 16通道输入信号的有效电平
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnInputLevel">有效电平1</param>
        /// <param name="pnInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadInputLevel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadInputLevel(uint nPortIndex, uint nNetID, ref byte pnInputLevel, ref byte pnInputLevel1);





        /// <summary>
        /// 设置布防或撤防
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInputLevel">有效电平1</param>
        /// <param name="nInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetAlarms", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetAlarms(uint nPortIndex, uint nNetID, byte nInputLevel, byte nInputLevel1);





        /// <summary>
        /// 读取布防或撤防
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nInputLevel">有效电平1</param>
        /// <param name="nInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadAlarms", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadAlarms(uint nPortIndex, uint nNetID, ref byte nInputLevel, ref byte nInputLevel1);





        /// <summary>
        /// 设置RLY-NO号继电器在16道报警时的响应方法
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayID"></param>
        /// <param name="nInputLevel">有效电平1</param>
        /// <param name="nInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetRelayMap", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetRelayMap(uint nPortIndex, uint nNetID, uint nRelayID, byte nInputLevel, byte nInputLevel1);





        /// <summary>
        /// 读取RLY-NO号继电器在16道报警时的响应方法
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayID">继电器ID</param>
        /// <param name="nInputLevel">有效电平1</param>
        /// <param name="nInputLevel1">有效电平2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadRelayMap", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadRelayMap(uint nPortIndex, uint nNetID, uint nRelayID, ref byte nInputLevel, ref byte nInputLevel1);





        /// <summary>
        /// 读取线路状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnAlarmState">16道报警状态，1TH.D0表示第1道输入; 2TH.D7表示第16道输入</param>
        /// <param name="pnInputState">16道线路状态，3TH.D0表示第1道输入; 4TH.D7表示第16道输入</param>
        /// <param name="pnRelayState">第5字节：D0—D7对应第1—第8继电器; =1表示动作; 6字节:D0对应第9继电器</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadState(uint nPortIndex, uint nNetID, out uint pnAlarmState, out uint pnInputState, out uint pnRelayState);





        /// <summary>
        /// 读取记录区状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnSave">返回SAVE_P</param>
        /// <param name="pnLoad">返回Load</param>
        /// <param name="pnMaxRecord">返回MaxRecord</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadRecInfo(uint nPortIndex, uint nNetID, ref uint pnSave, ref uint pnLoad, ref uint pnMaxRecord);





        /// <summary>
        /// 设置记录区状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoad">Load。0～65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IIoSetRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IIoSetRecord(uint nPortIndex, uint nNetID, uint nLoad);





        /// <summary>
        /// 设置记录区状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoad">Load。0～65535</param>
        /// <param name="nSave">Save.0～65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetRecordEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetRecordEx(uint nPortIndex, uint nNetID, uint nLoad, uint nSave);





        /// <summary>
        /// 读取记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnRecInputID"></param>参考附录1
        /// <param name="pnRecStart"></param>参考附录1
        /// <param name="pTime"></param>参考附录1
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoReadRecord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoReadRecord(uint nPortIndex, uint nNetID, ref uint pnRecInputID, ref uint pnRecStart, ref SYSTEMTIME pTime);





        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nBaudrate">=0,1,2,3,4,5,6,7对应速率:1200/2400/4800/9600/19200/38400/9600/9600BPS</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetBaudrate(uint nPortIndex, uint nNetID, uint nBaudrate);





        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayID">继电器ID</param>
        /// <param name="nFlag">D0=0开 / 1关</param>
        /// <param name="nDelay">时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "IoSetRelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int IoSetRelay(uint nPortIndex, uint nNetID, uint nRelayID, uint nFlag, uint nDelay);



        /********************************************************************************
         * 附录1
         共9字节:
            输入通道号	      开始/结束	                 日期\时间
             2字节	           =1/0	                     Y/M/D/H/M/S
		
	     输入通道号(2字节) BCD码:  RS485地址ADDR(的BCD码)开始(=0) , 到 ADDR+15 表示第1到第16通道.
         * ******************************************************************************/




    }
}
