using System;
using System.Runtime.InteropServices;
using static SuperDeviceFactory.LonBonDevice.LonBonDelegate;
using static SuperDeviceFactory.LonBonDevice.LonBonStruct;

namespace SuperDeviceFactory.LonBonDevice
{
    /// <summary>
    /// <para>说明：来邦对讲API。实现应用程序与对讲主机之间进行交互通信，具有控制对讲主机接听分机呼叫，与其他终端对讲、广播、多方通话等基本功能。</para>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-10-21</para>
    /// </summary>
    public static class LonBonAPI
    {
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="serverIp">对讲服务器IP地址(地址盒地址)</param>
        /// <param name="svrPort">对讲服务器监听端口（默认5160端口）</param>
        /// <returns>返回0成功，-1或者其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_initialServer(string serverIp, int svrPort);

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_releaseServer();

        /// <summary>
        /// 发起呼叫
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">对讲主机编号</param>
        /// <param name="terNum">对讲终端编号</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_call(string svrIp, int hostNum, int terNum);

        /// <summary>
        /// 接听来自主机或分机的呼叫
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">对讲主机编号</param>
        /// <param name="terNum">对讲终端编号</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_answer(string svrIp, int hostNum, int terNum);

        /// <summary>
        /// 挂断当前的通话
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">对讲主机编号</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_hangUp(string svrIp, int hostNum);

        /// <summary>
        /// 指定由某个主机发起广播，与多个终端进行通话
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">广播主机编号</param>
        /// <param name="groupNum">终端编号列表（整型数组）</param>
        /// <param name="count">终端编号数量</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_start_broadcast(string svrIp, int hostNum, int[] groupNum, int count);

        /// <summary>
        /// 断开之前发起的广播
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">发起广播的主机编号</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_stop_broadcast(string svrIp, int hostNum);

        /// <summary>
        /// 发起多方通话（会议模式——与会成员发言，互相之间都可以收到）
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">发起多方通话主机编号</param>
        /// <param name="groupNum">终端编号列表（整型数组）</param>
        /// <param name="count">终端编号数量</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_start_multiTalk_conference(string svrIp, int hostNum, int[] groupNum, int count);

        /// <summary>
        /// 发起多方通话（指挥模式——与会成员发言只有主席端能收到，其他成员收不到）
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">发起多方通话主机编号</param>
        /// <param name="groupNum">终端编号列表（整型数组）</param>
        /// <param name="count">终端编号数量</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_start_multiTalk_conduct(string svrIp, int hostNum, int[] groupNum, int count);

        /// <summary>
        /// 停止之前发起的多方通话
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="hostNum">发起多方通话主机编号</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_stop_multiTalk(string svrIp, int hostNum);

        /// <summary>
        /// 获取对讲服务器下所有终端的基本信息
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="terminalInfo">返回的对讲终端信息</param>
        /// <param name="count">对讲终端的数量，调用后返回实际的数量</param>
        /// <returns>返回0成功，其他失败。（如果对讲系统中对讲数量比count多，那么函数返回失败，且count返回实际的数量。）</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_getTerminalInfos(string svrIp, TerminalInfo terminalInfo, int[] count);

        /// <summary>
        /// 获取指定终端的基本信息
        /// </summary>
        /// <param name="svrIp">对讲服务器IP地址</param>
        /// <param name="terNum">指定终端的编号</param>
        /// <param name="terminalInfo">返回的指定的对讲终端信息</param>
        /// <returns>返回0成功，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll", EntryPoint = "lb_getTerminalInfo")]
        public static extern int lb_getTerminalInfo(string svrIp, int terNum, IntPtr terminalInfo);

        /// <summary>
        /// 获取对讲服务器中所有主机终端的数量
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <returns>大于等于0表示返回的主机终端数量，否则失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_all_master_count(string svrIp);

        /// <summary>
        /// 获取对讲服务器中所有主机终端的编号列表
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <param name="mstList">整型数组，用于存编号列表，需预先分配空间</param>
        /// <param name="n_size">编号列表mstList的数组长度</param>
        /// <returns>大于等于0表示返回的编号列表中实际的终端数量，否则失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_all_master(string svrIp, int[] mstList, int n_size);

        /// <summary>
        /// 获取对讲服务器中在线主机终端的数量
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <returns>大于等于0表示返回在线主机终端数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_online_master_count(string svrIp);

        /// <summary>
        /// 获取对讲服务器中所有在线主机终端的编号列表
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <param name="mstList">整型数组，用于缓存在线主机编号列表，需预先分配空间</param>
        /// <param name="n_size">编号列表mstList的数组长度</param>
        /// <returns>大于等于0表示返回的编号列表中实际的在线主机终端数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_online_master(string svrIp, int[] mstList, int n_size);

        /// <summary>
        /// 获取指定主机终端下所有分机终端的数量
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <param name="hostNum">指定主机终端编号</param>
        /// <returns>大于等于0表示返回该主机下所有分机终端的数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_terminal_from_master_count(string svrIp, int hostNum);

        /// <summary>
        /// 获取指定主机下所有分机终端的列表
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <param name="hostNum">指定主机终端编号</param>
        /// <param name="terList">整型数组，用于缓存所有分机终端列表，需预先分配空间</param>
        /// <param name="n_size">编号列表terList的数组长度</param>
        /// <returns>大于等于0表示返回的编号列表中实际的分机终端的数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_terminal_from_master(string svrIp, int hostNum, int[] terList, int n_size);

        /// <summary>
        /// 获取指定主机终端下所有的在线分机的数量
        /// </summary>
        /// <param name="svrIp">对讲服务器的IP地址</param>
        /// <param name="hostNum">指定主机终端编号</param>
        /// <returns>大于等于0表示返回该主机下所有在线分机终端的数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_terminal_online_from_master_count(string svrIp, int hostNum);

        /// <summary>
        /// 获取指定主机终端下所有在线分机终端列表
        /// </summary>
        /// <param name="svrIp">对讲服务器的网络地址</param>
        /// <param name="hostNum">指定主机终端编号</param>
        /// <param name="terList">整型数组，用于缓存所有在线分机终端列表，需预先分配空间</param>
        /// <param name="n_size">编号列表terList的数组长度</param>
        /// <returns>大于等于0表示返回的终端列表中实际的在线分机终端数量，其他失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_terminal_online_from_master(string svrIp, int hostNum, int[] terList, int n_size);

        /// <summary>
        /// 获取指定设备终端的在线状态
        /// </summary>
        /// <param name="svrIp">对讲服务器的网络地址</param>
        /// <param name="displayNum">指定设备终端的编号</param>
        /// <returns>返回非0表示在线，0表示不在线。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_state_from_terminal(string svrIp, int displayNum);

        /// <summary>
        /// 获取失败的信息
        /// </summary>
        /// <param name="errorId">错误Id，由以上接口返回</param>
        /// <param name="strErr">错误输出缓冲区</param>
        /// <param name="errlen">缓冲区大小</param>
        /// <returns></returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll")]
        public static extern int lb_get_error_info(int errorId,string strErr, int errlen);

        /// <summary>
        /// 注册对讲主机事件回调函数
        /// </summary>
        /// <param name="callback">回调函数地址</param>
        /// <param name="userData">用户自定义的信息</param>
        /// <returns>返回0成功、-1失败。</returns>
        [DllImport(@"DLL\LONBONDLL\lb_sdk_universal.dll", BestFitMapping =true,CharSet =CharSet.Ansi, EntryPoint = "lb_CallActionNotify")]
        public static extern int lb_CallActionNotify(ACTION_CALLBACK callback, IntPtr userData);
    }
}
