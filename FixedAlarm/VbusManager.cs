using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperDeviceFactory.FixedAlarm
{
    /// <summary>
    /// 说明：英安特报警主机代理
    /// 时间：2014-12-12
    /// 作者：痞子少爷
    /// </summary>
    public class VbusManager
    {
        /// <summary>
        /// 报警回调事件
        /// </summary>
        /// <param name="EventInfo">报警事件信息</param>
        /// <param name="eventType">事件类型说明</param>
        public delegate void ReturnAllEventCallback(VbusAPI.EventInfo info, string eventType);
        /// <summary>
        /// 报警回调事件
        /// </summary>
        public static event ReturnAllEventCallback Alarm;

        static string ip = "";
        static int port = 0;
        /// <summary>
        /// 连接到vbus服务器
        /// </summary>
        /// <param name="serverIp">服务ip</param>
        /// <param name="serverPoint">服务端地址</param>
        ///  <param name="clientPoint">客户端地址</param>
        public static void ConnectionVbus(string serverIp, int serverPoint, int clientCommPoint)
        {
            ip = serverIp;
            port = serverPoint;
            bool b = VbusAlarm(clientCommPoint);
            while (!b)
            { b = VbusAlarm(clientCommPoint); }
        }
        /// <summary>
        /// 断开vbus服务器
        /// </summary>
        /// <returns>成功返回0,不成功返回1</returns>
        public static bool DisconnectVBUS()
        {
            return VbusAPI.DisconnectVBUS() == 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientCommPoint"></param>
        /// <returns></returns>
        private static bool VbusAlarm(int clientCommPoint = 9400)
        {
            string s = VbusAPI.LANIP.ToString();
            VbusAPI.Alarm += Vbus_Alarm;
            int x = VbusAPI.InitSDK(13, port, clientCommPoint);
            if (x != 1)
                return false;
            int y = VbusAPI.ConnectToVBUS(VbusAPI.GetByteFromString(ip, 18, Encoding.ASCII), clientCommPoint);
            if (y != 0)
                return false;
            return true;
        }
        static void Vbus_Alarm(int vbusaddr, int year, int month, int day, int hour, int minute, int second, int events, int port, int hostaddr, int deviceaddr, int subsystem, int zone, int zonetype)
        {
            string eventStr = "";
            eventStr = SuperFramework.EnumHelper.GetMemberName<VbusAPI.AlarmType>(events);
            Alarm?.Invoke(new VbusAPI.EventInfo() { day = day, deviceaddr = deviceaddr, events = events, hostaddr = hostaddr, hour = hour, minute = minute, month = month, port = port, second = second, subsystem = subsystem, vbusaddr = vbusaddr, year = year, zone = zone, zonetype = zonetype }, eventStr);

            //switch (events)
            //{
            //    case 1:
            //        eventStr = "防区报警";
            //        break;
            //    case 34:
            //        eventStr = "模块故障";
            //        break;
            //    case 35:
            //        eventStr = "模块恢复";
            //        break;
            //    case 12:
            //        eventStr = "主机脱机";
            //        break;
            //    case 13:
            //        eventStr = "主机恢复";
            //        break;
            //    case 5:
            //        eventStr = "交流电源故障";
            //        break;
            //    case 6:
            //        eventStr = "交流电源恢复";
            //        break;
            //    case 7:
            //        eventStr = "电池(直流)故障";
            //        break;
            //    case 8:
            //        eventStr = "电池(直流)恢复";
            //        break;
            //    case 9:
            //        eventStr = "防区旁路";
            //        break;
            //    case 10:
            //        eventStr = "防区旁路恢复";
            //        break;
            //    case 27:
            //        eventStr = "有声劫警";
            //        break;
            //    case 28:
            //        eventStr = "医疗求助";
            //        break;
            //    case 30:
            //        eventStr = "火警";
            //        break;
            //    case 15:
            //        eventStr = "电话线路故障";
            //        break;
            //    case 16:
            //        eventStr = "电话线路恢复";
            //        break;
            //    case 17:
            //        eventStr = "电话报警故障";
            //        break;
            //    case 18:
            //        eventStr = "电话报警恢复";
            //        break;
            //    case 29:
            //        eventStr = "胁迫无声报警";
            //        break;
            //    //default:
            //    //    eventStr = "其他报警";
            //    //    break;
            //}
            //if (eventStr == "主机脱机")
            //{
            //    int x = Vbus.DisconnectFromVBUSservice();
            //    //while (x != 0)
            //    //{ x = Vbus.DisconnectFromVBUSservice(); }
            //    Vbus.FreeSDK();
            //    bool b = VbusAlarm();
            //    while (!b)
            //    { b = VbusAlarm(); }
            //}

        }
        /// <summary>
        /// 获取报警类型
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns>事件类型字符串</returns>
        public static string GetAlarm(int eventType)
        {
            string m_Alarm = "";
            int alarmAction = -1;
            switch (eventType)
            {
                case 0:
                    m_Alarm = "报警防区恢复";
                    alarmAction = 2;
                    break;
                case 1:
                    m_Alarm = "防区报警";
                    alarmAction = 1;
                    break;
                case 2:
                    m_Alarm = "防区未准备";
                    break;
                case 3:
                    m_Alarm = "用户撤防";
                    break;
                case 4:
                    m_Alarm = "用户布防";
                    break;
                case 5:
                    m_Alarm = "交流电源故障";
                    break;
                case 6:
                    m_Alarm = "交流电源恢复";
                    break;
                case 7:
                    m_Alarm = "电池(直流)故障";
                    break;
                case 8:
                    m_Alarm = "电池(直流)恢复";
                    break;
                case 9:
                    m_Alarm = "防区旁路";
                    break;
                case 10:
                    m_Alarm = "防区旁路恢复";
                    break;
                case 11:
                    m_Alarm = "防区状态";
                    break;
                case 12:
                    m_Alarm = "主机脱机";
                    break;
                case 13:
                    m_Alarm = "主机恢复";
                    break;
                case 14:
                    m_Alarm = "开门";
                    break;
                case 15:
                    m_Alarm = "电话线路故障";
                    break;
                case 16:
                    m_Alarm = "电话线路恢复";
                    break;
                case 17:
                    m_Alarm = "电话报警故障";
                    break;
                case 18:
                    m_Alarm = "电话报警恢复";
                    break;
                case 19:
                    m_Alarm = "获取开门密码";
                    break;
                case 20:
                    m_Alarm = "门禁开门";
                    break;
                case 21:
                    m_Alarm = "门禁关门";
                    break;
                case 22:
                    m_Alarm = "提示信息";
                    break;
                case 23:
                    m_Alarm = "正常巡更";
                    break;
                case 24:
                    m_Alarm = "提前巡更";
                    break;
                case 25:
                    m_Alarm = "滞后巡更";
                    break;
                case 26:
                    m_Alarm = "巡更员未到巡更点";
                    break;
                case 27:
                    m_Alarm = "有声劫警";
                    alarmAction = 1;
                    break;
                case 28:
                    m_Alarm = "医疗求助";
                    alarmAction = 1;
                    break;
                case 29:
                    m_Alarm = "胁迫无声报警";
                    alarmAction = 1;
                    break;
                case 30:
                    m_Alarm = "火警";
                    alarmAction = 1;
                    break;
                case 31:
                    m_Alarm = "无效巡更";
                    break;
                case 32:
                    m_Alarm = "子系统布防";
                    break;
                case 33:
                    m_Alarm = "子系统撤防";
                    break;
                case 34:
                    m_Alarm = "模块故障";
                    break;
                case 35:
                    m_Alarm = "模块恢复";
                    break;
                case 36:
                    m_Alarm = "防区布防";
                    break;
                case 37:
                    m_Alarm = "防区撤防";
                    break;
                case 38:
                    m_Alarm = "通道开启";
                    break;
                case 39:
                    m_Alarm = "通道关闭";
                    break;
                case 40:
                    m_Alarm = "所有通道开启";
                    break;
                case 41:
                    m_Alarm = "所有通道关闭";
                    break;
                case 42:
                    m_Alarm = "门禁正常巡更";
                    break;
                case 43:
                    m_Alarm = "门禁滞后巡更";
                    break;
                case 44:
                    m_Alarm = "门禁提前巡更";
                    break;
                case 45:
                    m_Alarm = "门禁未到巡更点";
                    break;
                case 46:
                    m_Alarm = "门禁故障";
                    break;
                case 47:
                    m_Alarm = "演示软件到期";
                    break;
                case 48:
                    m_Alarm = "防区未准备恢复";
                    break;
                //case 48:
                //    m_Alarm = "读IC 卡信息";
                //    break;
                //case 49:
                //    m_Alarm = "GSM 模块故障";
                //    break;
                case 50:
                    m_Alarm = "GSM 模块故障";
                    break;
                case 51:
                    m_Alarm = "GSM 报告故障";
                    break;
                case 52:
                    m_Alarm = "GSM SIM 卡故障";
                    break;
                case 53:
                    m_Alarm = "GSM 天线或无信号";
                    break;
                case 54:
                    m_Alarm = "清除事件记录";
                    break;
                case 55:
                    m_Alarm = "参数复位";
                    break;
                case 56:
                    m_Alarm = "管理员键盘改变参数";
                    break;
                case 57:
                    m_Alarm = "PC 改变参数";
                    break;
                case 58:
                    m_Alarm = "GSM 定期测试";
                    break;
                case 59:
                    m_Alarm = "电话线路定期测试";
                    break;
            }
            return m_Alarm +"_"+ alarmAction;
        }
    }
}
