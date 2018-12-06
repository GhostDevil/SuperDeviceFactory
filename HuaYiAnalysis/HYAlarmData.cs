using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperDeviceFactory.HuaYiAnalysis
{
    /// <summary>
    /// 华亿报警数据
    /// </summary>
    [Serializable]
    public class HYAlarmData
    {
        /// <summary>
        /// 接收数据标识
        /// </summary>
        public static string DataId = "0x02,0x6f,0x01,0x01";
        /// <summary>
        /// 报警数据结构
        /// </summary>
        public struct MsgStrut
        {
            /// <summary>
            /// 数据标识
            /// </summary>
            public string DataId { get; set; }
            /// <summary>
            /// 数据总长度
            /// </summary>
            public int Length { get; set; }
            /// <summary>
            /// 设备id
            /// </summary>
            public int DeviceId { get; set; }
            /// <summary>
            /// 报警类型
            /// </summary>
            public XmlType AlarmType { get; set; }
            /// <summary>
            /// xml数据
            /// </summary>
            public string XmlData { get; set; }
        }
        /// <summary>
        /// xml数据类型
        /// </summary>
        public enum XmlType
        {
            /// <summary>
            /// 报警xml数据
            /// </summary>
            AlarmEventMsg = 0,
            /// <summary>
            /// 轨迹xml数据
            /// </summary>
            XMLLayoutMessage = 1,
            /// <summary>
            /// 人流xml数据
            /// </summary>
            CountingEventMsg = 2
        }
        public enum AlarmType
        {
            ALARM_NOREFERENCEFOUND,

            ALARM_VIDEOLOSSE,

            ALARM_VMD,

            ALARM_VMD_HUMAN,

            ALARM_VMD_VEHICLE,

            ALARM_VMD_OTHER,

            ALARM_STATICOBJECT,

            ALARM_STATICOBJECT_HUMAN,

            ALARM_STATICOBJECT_VEHICLE,

            ALARM_STATICOBJECT_OTHER,

            ALARM_PRESENCE,

            ALARM_DIRECTIONALMOTION,

            ALARM_OBJECTSTARTED,

            ALARM_PATHDETECTION,

            ALARM_PATHDETECTION_HUMAN,

            ALARM_PATHDETECTION_VEHICLE,

            ALARM_PATHDETECTION_OTHER,

            ALARM_OBJECTREMOVAL,

            ALARM_SPEED,

            ALARM_LOITERING,

            ALARM_AREACOVERAGE,

            ALARM_MOTIONACTIVITY,

            ALARM_FLOWCOUNTING,

            ALARM_CARCOUNTING,

            ALARM_TRIP,

            ALARM_TRIP_HUMAN,

            ALARM_TRIP_VEHICLE,

            ALARM_TRIP_OTHER,

            ALARM_DOUBLETRIP,

            ALARM_DOUBLETRIP_HUMAN,

            ALARM_DOUBLETRIP_VEHICLE,

            ALARM_DOUBLETRIP_OTHER,

            ALARM_DEPENDENCY,

        }
    }
}
