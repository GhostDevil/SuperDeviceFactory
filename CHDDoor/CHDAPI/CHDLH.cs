using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{


    /// <summary>
    /// 烟雾传感器API接口
    /// </summary>
   public static  class CHDLH
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
        /// 读取温湿度度
       /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnCurMean">返回当前温度</param>
        /// <param name="pnCurTest">返回当前湿度</param>
        /// <returns>设备返回值</returns>
       [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "ModBusThReadSensor", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
       public static extern int ModBusThReadSensor(uint nPortIndex, uint nNetID, out double pnCurMean, out double pnCurTest);
    }
}
