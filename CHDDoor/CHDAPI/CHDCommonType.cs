using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    ///// <summary>
    ///// 说明：纽贝尔门禁结构体
    ///// 时间：2017-08-10
    ///// 作者：痞子少爷
    ///// </summary>
    //public class CommonType
    //{
    /// <summary>
    /// CHD时间结构类型
    /// </summary>
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    /// <summary>
    /// 设备型号
    /// </summary>
    public enum DeviceType
    {
        CHD200G = 1,
        CHD200H,
        CHD601D_M3,
        CHD603S,
        CHD689,
        CHD805,
        CHD806D2CP,
        CHD806D2M3B,
        CHD806D4C,
        CHD806D4M3,
        CHD815T_M3,
        CHD825T,
        CHDBank,
        CHDCardReader,
        CHDIOCtrl,
        CHDLH,
        CHDT5,
        CHDTHSendor,

    }
    /// <summary>
    /// 卡类型
    /// </summary>
    public enum DoorPrivilege
    {
        /// <summary>
        /// 第一类卡
        /// </summary>
        One = 0x40,
        /// <summary>
        /// 第二类卡
        /// </summary>
        Two = 0x41,
        /// <summary>
        /// 第三类卡
        /// </summary>
        Three = 0x42,
        /// <summary>
        /// 第四类卡
        /// </summary>
        Four = 0x43,
        /// <summary>
        /// 特权卡
        /// </summary>
        Five = 0xC0,
    }
}
//}
