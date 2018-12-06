using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SuperDeviceFactory.LonBonDevice
{
    /// <summary>
    /// <para>说明：来邦对讲数据结构</para>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-11-10</para>
    /// </summary>
    public static class LonBonStruct
    {
        #region 控制类型
        /// <summary>
        /// 控制类型
        /// </summary>
        public enum LBControlCmd
        {
            /// <summary>
            /// 发起呼叫
            /// </summary>
            呼叫 = 1,
            /// <summary>
            /// 挂断当前的通话
            /// </summary>
            挂断 = 2,
            /// <summary>
            /// 接听来自主机或分机的呼叫
            /// </summary>
            接听 = 3,
            /// <summary>
            /// 发起多方通话（会议模式——与会成员发言，互相之间都可以收到）
            /// </summary>
            发起多方通话_会议模式,
            /// <summary>
            /// 发起多方通话（指挥模式——与会成员发言只有主席端能收到，其他成员收不到）
            /// </summary>
            发起多方通话_指挥模式,
            /// <summary>
            /// 停止之前发起的多方通话
            /// </summary>
            停止多方通话,
            /// <summary>
            /// 指定由某个主机发起广播，与多个终端进行通话
            /// </summary>
            发起广播,
            /// <summary>
            /// 断开之前发起的广播
            /// </summary>
            挂断广播
        }
        #endregion

        #region 设备信息结构体
        /// <summary>
        /// 来邦设备在线状态
        /// </summary>
        public struct LBTalkDeviceState
        {
            /// <summary>
            /// /主机数量
            /// </summary>
            public int masterCount;
            /// <summary>
            /// 分机数量
            /// </summary>
            public int terminalCount;
            /// <summary>
            /// 设备状态列表
            /// </summary>
            public List<LBTalkState> states;
        }
        /// <summary>
        /// 来邦设备状态
        /// </summary>
        public struct LBTalkState
        {
            /// <summary>
            /// 设备编号
            /// </summary>
            public int deviceId;
            /// <summary>
            /// 在线状态 非0表示在线，0表示不在线
            /// </summary>
            public int state;
            /// <summary>
            /// 设备类型 1-主机 2-分机
            /// </summary>
            public int deviceType;
        }
        /// <summary>
        /// 对讲设备信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TerminalInfo
        {
            /// <summary>
            /// 终端类型：-主机，-分机
            /// </summary>
            public int terminalType;
            /// <summary>
            /// 终端编号
            /// </summary>
            public int displayNum;
            /// <summary>
            /// 终端IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string netAddr;
            /// <summary>
            /// 终端名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string name;
            /// <summary>
            /// 终端型号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string model;
        }

        /// <summary>
        /// 会话设备信息
        /// </summary>
       [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct action_param
        {
            /// <summary>
            /// 发送端
            /// </summary>
            public int sender;
            /// <summary>
            /// 接收端
            /// </summary>
            public int receiver;
            /// <summary>
            /// 广播接收端
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string acceptBc;
            /// <summary>
            /// 会话标识
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sessionId;
            /// <summary>
            /// 广播组序(标识)/门磁编号 
            /// </summary>
            public int broadId;
            /// <summary>
            /// 录音文件名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string rdFile;
            /// <summary>
            /// Atm编号
            /// </summary>
            public int atmTerNum;
        }
        #endregion

        #region 事件类型
        /// <summary>
        /// 事件类型
        /// </summary>
        public enum lb_event_message_e
        {
            LBTCP_EVENT_NONE = 0,
            /// <summary>
            /// 呼出处理中
            /// </summary>
            LBTCP_EVENT_PROCESSING,
            /// <summary>
            /// 呼出振铃
            /// </summary>
            LBTCP_EVENT_RINGBACK,
            /// <summary>
            /// 普通呼入
            /// </summary>
            LBTCP_EVENT_CALLIN,
            /// <summary>
            /// 紧急报警
            /// </summary>
            LBTCP_EVENT_EXTNEMGY,
            /// <summary>
            /// 喧哗报警
            /// </summary>
            LBTCP_EVENT_EXTNNSALM,
            /// <summary>
            /// 防拆报警
            /// </summary>
            LBTCP_EVENT_EXTNRMALARM,
            /// <summary>
            /// 病区门口机报警
            /// </summary>
            LBTCP_EVENT_EXTNWARDALARM,
            /// <summary>
            /// 输液报警
            /// </summary>
            LBTCP_EVENT_EXTNINFUSALM,
            /// <summary>
            /// 监听接通
            /// </summary>
            LBTCP_EVENT_LSTN_CONNECT,
            /// <summary>
            /// 对讲接通
            /// </summary>
            LBTCP_EVENT_TALK_CONNECT,
            /// <summary>
            /// 通话挂断
            /// </summary>
            LBTCP_EVENT_TALK_DISCONNECT,
            /// <summary>
            /// 呼入/呼出挂断
            /// </summary>
            LBTCP_EVENT_CALLIN_DISCONNECT,
            /// <summary>
            /// 呼出失败
            /// </summary>
            LBTCP_EVENT_CALLOUT_FAIL,
            /// <summary>
            /// 门磁断开提示
            /// </summary>
            LBTCP_EVENT_DR1_OPEN,
            /// <summary>
            /// 门磁闭合提示
            /// </summary>
            LBTCP_EVENT_DR1_CLOSE,
            /// <summary>
            /// 门磁断开提示
            /// </summary>
            LBTCP_EVENT_DR2_OPEN,
            /// <summary>
            /// 门磁闭合提示
            /// </summary>
            LBTCP_EVENT_DR2_CLOSE,
            /// <summary>
            /// 对主机喊话广播开始
            /// </summary>
            LBTCP_EVENT_BC2MST_START,
            /// <summary>
            /// 对分机喊话广播开始
            /// </summary>
            LBTCP_EVENT_BC2EXTN_START,
            /// <summary>
            /// 文件广播开始
            /// </summary>
            LBTCP_EVENT_BC2EXTNFILE_START,
            /// <summary>
            /// 外接音源广播开始
            /// </summary>
            LBTCP_EVENT_BC2EXTNEXAD_START,
            /// <summary>
            /// 广播结束
            /// </summary>
            LBTCP_EVENT_BC_STOP,
            /// <summary>
            /// 会议模式开始
            /// </summary>
            LBTCP_EVENT_MLTK_CONFERENCE_START,
            /// <summary>
            /// 指挥模式开始
            /// </summary>
            LBTCP_EVENT_MLTK_CONDUCT_START,
            /// <summary>
            /// 多方通话结束
            /// </summary>
            LBTCP_EVENT_MLTK_STOP,
            /// <summary>
            /// 多方通话失败
            /// </summary>
            LBTCP_EVENT_MLTK_FAIL,
        }
        #endregion

        
    }
}
