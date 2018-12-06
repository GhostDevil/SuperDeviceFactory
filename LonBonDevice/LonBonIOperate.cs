using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SuperDeviceFactory.LonBonDevice.LonBonStruct;

namespace SuperDeviceFactory.LonBonDevice
{
    /// <summary>
    /// <para>说明：实现应用程序与对讲主机之间进行交互通信，具有控制对讲主机接听分机呼叫，与其他终端对讲、广播、多方通话等基本功能。</para>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-11-01</para>
    /// <para>
    /// 注意：
    /// <para>1）主机编号：统一采用6位整数编号，前3位为主机号，后3位为0</para>
    /// <para>2）终端编号：可以是主机编号，也可以是分机编号。分机编号采用6位或9位编号，采用6位编号时，前3位为该分机所属主机号码，后3位为分机编号。采用9位编号时，前3位为该分机所属主机编号，中间3位为音视频接线箱编号，后3位为该接线箱下ATM音视频终端编号。</para>
    /// <para>例：105000表示105号主机。
	/// 105006表示105号主机下的6号分机。
	/// 105007008表示105号主机下7号接线箱下8号终端。</para>
    /// </para>
    /// </summary>
    public class LonBonIOperate
    {
        #region 获取事件名称
        /// <summary>
        /// 获取事件名称
        /// </summary>
        /// <param name="userEvent">事件类型</param>
        public static string GetEventName(LonBonStruct.lb_event_message_e userEvent)
        {
            switch (userEvent)
            {
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_NONE:
                    return "";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_PROCESSING:
                    return "呼出处理中";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_RINGBACK:
                    return "呼出振铃";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_CALLIN:
                    return "普通呼入";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_EXTNEMGY:
                    return "紧急报警";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_EXTNNSALM:
                    return "喧哗报警";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_EXTNRMALARM:
                    return "防拆报警";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_EXTNWARDALARM:
                    return "病区门口机报警";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_EXTNINFUSALM:
                    return "输液报警";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_LSTN_CONNECT:
                    return "监听接通";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_TALK_CONNECT:
                    return "对讲接通";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_TALK_DISCONNECT:
                    return "通话挂断";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_CALLIN_DISCONNECT:
                    return "呼入/呼出挂断";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_CALLOUT_FAIL:
                    return "呼出失败";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_DR1_OPEN:
                    return "门磁断开提示";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_DR1_CLOSE:
                    return "门磁闭合提示";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_DR2_OPEN:
                    return "门磁断开提示";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_DR2_CLOSE:
                    return "门磁闭合提示";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_BC2MST_START:
                    return "对主机喊话广播开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_BC2EXTN_START:
                    return "对分机喊话广播开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_BC2EXTNFILE_START:
                    return "文件广播开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_BC2EXTNEXAD_START:
                    return "外接音源广播开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_BC_STOP:
                    return "广播结束";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_MLTK_CONFERENCE_START:
                    return "会议模式开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_MLTK_CONDUCT_START:
                    return "指挥模式开始";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_MLTK_STOP:
                    return "多方通话结束";
                case LonBonStruct.lb_event_message_e.LBTCP_EVENT_MLTK_FAIL:
                    return "多方通话失败";
                default:
                    return "未知事件";
            }
        }
        #endregion

        #region 检查来邦设备状态
        /// <summary>
        /// 检查来邦设备状态
        /// </summary>
        /// <param name="svrIp">地址盒ip</param>
        /// <returns>返回设备状态列表</returns>
        public LBTalkDeviceState CheckLBDeviceState(string svrIp)
        {
            try
            {

                int count = LonBonAPI.lb_get_all_master_count(svrIp);
                if (count > 0)
                {
                    LBTalkDeviceState states = new LBTalkDeviceState();
                    List<LBTalkState> deState = new List<LBTalkState>();
                    int[] talk = new int[count];
                    if (LonBonAPI.lb_get_all_master(svrIp, talk, count) > 0)
                    {
                        states.masterCount = talk.Length;
                        int state = -1;
                        for (int i = 0; i < talk.Length; i++)
                        {
                            state = LonBonAPI.lb_get_state_from_terminal(svrIp, talk[i]);

                            deState.Add(new LBTalkState() { deviceId = talk[i], state = state, deviceType = 1 });

                            count = LonBonAPI.lb_get_terminal_from_master_count(svrIp, talk[i]);
                            states.terminalCount = count;
                            int[] fTalk = new int[count];
                            if (count > 0)
                            {
                                if (LonBonAPI.lb_get_terminal_from_master(svrIp, talk[i], fTalk, count) > 0)
                                {
                                    state = LonBonAPI.lb_get_state_from_terminal(svrIp, fTalk[i]);

                                    deState.Add(new LBTalkState() { deviceId = fTalk[i], state = state, deviceType = 2 });

                                }
                            }
                        }
                    }
                    states.states = deState;
                    return states;
                }
            }
            catch (Exception) { }
            return new LBTalkDeviceState();
        }
        #endregion
    }
}
