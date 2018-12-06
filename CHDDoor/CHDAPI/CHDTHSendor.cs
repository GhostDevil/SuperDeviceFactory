using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHDTHSendor
    {



        /***************************************************************
         * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
           2．	执行LinkOn校验设备密码，确认设备通讯权限；
           3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
           4．	执行LinkOff取消设备通讯权限；
           5．	当程序关闭时使用ClosePort关闭端口。
         **************************************************************/



        /// <summary>
        /// 读取温度湿度
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pdTempeValue">返回温度</param>
        /// <param name="pdHumidValue">返回湿度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadSensor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadSensor(uint nPortIndex, uint nNetID, out double pdTempeValue, out double pdHumidValue);


        /// <summary>
        /// 读取温湿度(MODBUS协议)
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="pdTempeValue">温度</param>
        /// <param name="pdHumidValue">湿度</param>
        /// 备  注:	不能设置重复继电器(比如:D7D6=3则其他必须是0)
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ModBusThReadSensor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ModBusThReadSensor(uint nPortIndex, uint nNetID, out double pdTempeValue, out double pdHumidValue);



        /// <summary>
        /// 设置温湿度报警输出阈值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="dTempeLow">温度低报警值(0~255)</param>
        /// <param name="dTempeHigh">温度高报警值(0~255)</param>
        /// <param name="dHumidLow">湿度低报警值(0~255)</param>
        /// <param name="dHumidHigh">湿度高报警值(0~255)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "hSetAlarmLimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int hSetAlarmLimit(uint nPortIndex, uint nNetID, double dTempeLow, double dTempeHigh, double dHumidLow, double dHumidHigh);




        /// <summary>
        /// 设置MODBUS寄存器地址
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRegAddr">寄存器地址(0~255)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThSetRegAddr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThSetRegAddr(uint nPortIndex, uint nNetID, uint nRegAddr);


        /// <summary>
        /// 读取寄存器地址
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="nRegAddr">返回的寄存器地址</param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadRegAddr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadRegAddr(uint nPortIndex, uint nNetID, out uint nRegAddr);



        /// <summary>
        /// 读取温湿度报警偏移值
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="pnTempOffset">温度偏移值(值0-255，表示可设置偏移量为0.0℃-25.5℃)</param>
        /// <param name="pnHumidOffset">湿度偏移值(值0-255，表示可设置偏移量为0.0%--25.5%)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadAlarmOffset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadAlarmOffset(uint nPortIndex, uint nNetID, out uint pnTempOffset, out uint pnHumidOffset);





        /// <summary>
        /// 设置温湿度报警偏移值
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="pnTempOffset">温度偏移值(值0-255，表示可设置偏移量为0.0℃-25.5℃)</param>
        /// <param name="pnHumidOffset">湿度偏移值(值0-255，表示可设置偏移量为0.0%--25.5%)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThSetAlarmOffset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThSetAlarmOffset(uint nPortIndex, uint nNetID, uint pnTempOffset, uint pnHumidOffset);




        /// <summary>
        /// 设置加热调整
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nHeatTime">加热H：给感应器加热去结露的时间（0—40，表示最多加热2秒就停止，=0不加热）；</param>
        /// <param name="nNotHeatTime">调整T：给感应器加热时感应器温度因加热升高，应调整（0—50表示最多调整5摄仕度）不加热时间：0-50</param>
        /// <param name="nAdjustTemp">异或X：H与T的异或值</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThSetHeatParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThSetHeatParam(uint nPortIndex, uint nNetID, uint nHeatTime, uint nNotHeatTime, uint nAdjustTemp);




        /// <summary>
        /// 设置设备地址
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNewNetGrp">组地址：变送器的分组地址（0-15）；</param>
        /// <param name="nNewNetID">地址：变送器的组内地址（1-254）；</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThNewNetAddr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThNewNetAddr(uint nPortIndex, uint nNetID, uint nNewNetGrp, uint nNewNetID);




        /// <summary>
        /// 设置通信速率
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nBaudrateIndex">速率码：=0—5（对应：1200，2400，4800，9600，19200，38400BPS）；</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThNewBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThNewBaudrate(uint nPortIndex, uint nNetID, uint nBaudrateIndex);




        /// <summary>
        /// 读取温湿度报警的阈值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pdTempeLow">返回温度低端</param>
        /// <param name="pdTempeHigh">返回温度高端</param>
        /// <param name="pdHumidLow">返回湿度低端</param>
        /// <param name="pdHumidHigh">返回湿度高端</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadAlarmLimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadAlarmLimit(uint nPortIndex, uint nNetID, out double pdTempeLow, out double pdTempeHigh, out double pdHumidLow, out double pdHumidHigh);




        /// <summary>
        /// 设置温湿度报警阈值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pdTempeLow">温度低端</param>
        /// <param name="pdTempeHigh">温度高端</param>
        /// <param name="pdHumidLow">湿度低端</param>
        /// <param name="pdHumidHigh">湿度高端</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThSetAlarmLimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThSetAlarmLimit(uint nPortIndex, uint nNetID,  double pdTempeLow,  double pdTempeHigh,  double pdHumidLow,  double pdHumidHigh);

        /// <summary>
        /// 读取加热调整参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnHeatTime">返回加热时间</param>
        /// <param name="pnNotHeatTime">返回不加热时间</param>
        /// <param name="pnAdjustTemp">返回调整温度</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadHeatParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadHeatParam(uint nPortIndex, uint nNetID, out uint pnHeatTime, out uint pnNotHeatTime, out uint pnAdjustTemp);




        /// <summary>
        /// 读取设备名称及版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回版本</param>
        /// 说明：
        /// D7	D6	      D5	D4	      D3	D2	    D1	D0
        /// 温度高报警	温度低报警	     湿度高报警	   湿度低报警
        /// S1	S0	      S1	S0	      S1	S0	   S1	S0
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);




        /// <summary>
        /// 设置继电器输出控制字
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nRelayParam">D7-D0表示四种状态，每种状态由两个位来表示对应的继电器0与继电器1输出特性</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThSetAlarmRelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThSetAlarmRelay(uint nPortIndex, uint nNetID, uint nRelayParam);




        /// <summary>
        /// 读取露点温度值
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnDewPoint">返回当前露点温度值</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadDewPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadDewPoint(uint nPortIndex, uint nNetID, out double pnDewPoint);


        /// <summary>
        /// 读取报警状态
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="pnAlarmState">
        /// 报警状态
        /// D0=湿度低报警
        /// D1=湿度高报警
        /// D2=温度低报警
        /// D3=温度高报警
        /// </param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ThReadAlarmState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ThReadAlarmState(uint nPortIndex, uint nNetID, out uint pnAlarmState);


    }
}
