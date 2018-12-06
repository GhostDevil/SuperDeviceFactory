using System;
using static SuperDeviceFactory.LonBonDevice.LonBonStruct;

namespace SuperDeviceFactory.LonBonDevice
{
    /// <summary>
    /// <para>说明：来邦对讲通讯委托事件</para>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-10-21</para>
    /// </summary>
    public static class LonBonDelegate
    {
        /// <summary>
        /// 对讲主机的事件委托
        /// </summary>
        /// <param name="userEvent">反馈给用户的事件</param>
        /// <param name="wParam">用户可以获知的事件信息参数，包括呼叫编号、接收编号等</param>
        /// <param name="userData">注册回调时传入的用户自定义信息</param>
        public delegate void ACTION_CALLBACK (lb_event_message_e userEvent, ref action_param wParam, IntPtr userData);
        ///// <summary>
        ///// 对讲主机的事件(包括对讲、广播、多方通话、门磁等)回调函数，返回相关事件并反馈一些基本信息。
        ///// </summary>
        //public static event ACTION_CALLBACK ActionCallBck;

        /// <summary>
        /// 操作消息委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void LonBonMsg(string msg);
        /// <summary>
        /// 操作消息事件
        /// </summary>
        public static event LonBonMsg Msg;

    }
}
