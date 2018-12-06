using System;
using System.Collections.Generic;

namespace SuperDeviceFactory.CHDDoor
{
    /// <summary>
    /// 纽贝尔门禁类
    /// </summary>
    public class CHDClass
    {
        /// <summary>
        /// 设备状态
        /// </summary>
        public class CHDDeviceState
        {
            /// <summary>
            /// 设备ip
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 工作状态
            /// </summary>
            public CHDDoorWorkState[] WorkStates { get; set; }
            /// <summary>
            /// 线路状态
            /// </summary>
            public CHDDoorLineState[] LineStates { get; set; }
        }
        /// <summary>
        /// 门禁工作状态
        /// </summary>
        public class CHDDoorWorkState
        {
            /// <summary>
            /// 门禁编号
            /// </summary>
            public byte doorNo;
            /// <summary>
            /// 实时钟  0正常 1不正常
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// DCU事件 1需要SU处理事件 0无事件
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// 工作电源 1不正常,电压低而CPU被平凡复位 0正常
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 保留（防拆开关）
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 红外入侵 1监视 0不监视
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 门开关状态 1监视 0不监视
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 门控电磁继电器 1家电驱动 0关闭
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// 状态 1报警 0正常工作
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 门禁线路状态
        /// </summary>
        public class CHDDoorLineState
        {
            /// <summary>
            /// 门禁编号
            /// </summary>
            public byte doorNo;
            /// <summary>
            /// 紧急驱动 1输入 0正常
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// 门控 1常闭 0正常
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// 门控 1常开 0正常
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 胁迫 1是 0否
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 门状态 1门开 0闭合
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 红外报警 1报警 0正常
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 出门放行键 1按下 0松开
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// 预留
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 门禁远程控制动作
        /// </summary>
        public enum CHDControlDoorType
        {
            /// <summary>
            /// 远程开门
            /// </summary>
            Open = 0,
            /// <summary>
            /// 开始常开
            /// </summary>
            StartOpen,
            /// <summary>
            /// 开始常关
            /// </summary>
            StartClose,
            /// <summary>
            /// 结束常开
            /// </summary>
            EndOpen,
            /// <summary>
            /// 结束常关
            /// </summary>
            EndClose
        }/// <summary>
         /// 事件类型
         /// </summary>
        public enum CHDEventType
        {
            /// <summary>
            /// 日常信息
            /// </summary>
            Info = 0,
            /// <summary>
            /// 报警信息
            /// </summary>
            Alarm,
            /// <summary>
            /// 设置信息
            /// </summary>
            SetInfo,
        }
        /// <summary>
        /// 门禁四字节控制参数 字节1参数 (806D2CP)
        /// </summary>
        public class CHDByteParameter1
        {
            /// <summary>
            /// 门状态监控 1启用 0禁用
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// 红外监控 1启用 0禁用
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// 第二感应头密码 1启用 0禁用
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 第一感应头密码 1启用 0禁用
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 开门状态时门磁开路 1启用 0禁用
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 报警状态时红外开路 1启用 0禁用
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 刷卡加密时段 1启用 0禁用
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// 紧急输入状态时门常关 1启用 0禁用
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 门禁四字节控制参数 字节2参数 (806D2CP)
        /// </summary>
        public class CHDByteParameter2
        {
            /// <summary>
            /// 报警时报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// 手动按钮时报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// 第二头刷卡合法报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 第一头刷卡合法报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 无效卡刷卡报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 手动按钮时开门继电器 1启用 0禁用
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 第二头刷卡开门继电器 1启用 0禁用
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// 刷卡或按键报警继电器 1启用 0禁用
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 门禁四字节控制参数 字节3参数 (806D2CP)
        /// </summary>
        public class CHDByteParameter3
        {
            /// <summary>
            /// 网络正常由中心开门 1启用 0禁用
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// N+1功能时段屏蔽 1启用 0禁用
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// N+1功能[加特权卡确认] 1启用 0禁用
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 双门不能同时开[互锁] 1启用 0禁用
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 双卡确认开门 1启用 0禁用
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 双卡确认开门 1启用 0禁用
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 多卡开门分组 1启用 0禁用
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// DCU事件主动上报 1启用 0禁用
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 门禁四字节控制参数 字节4参数 (806D2CP)
        /// </summary>
        public class CHDByteParameter4
        {
            /// <summary>
            /// 多卡开门区分银行代码 1启用 0禁用
            /// </summary>
            public byte ByteD7;
            /// <summary>
            /// N+保留 1启用 0禁用
            /// </summary>
            public byte ByteD6;
            /// <summary>
            /// 保留 1启用 0禁用
            /// </summary>
            public byte ByteD5;
            /// <summary>
            /// 保留 1启用 0禁用
            /// </summary>
            public byte ByteD4;
            /// <summary>
            /// 保留 1启用 0禁用
            /// </summary>
            public byte ByteD3;
            /// <summary>
            /// 首次N+1确认后单卡开门 1启用 0禁用
            /// </summary>
            public byte ByteD2;
            /// <summary>
            /// 首次N+1确认 1启用 0禁用
            /// </summary>
            public byte ByteD1;
            /// <summary>
            /// 三卡确认方式 1启用 0禁用
            /// </summary>
            public byte ByteD0;
        }
        /// <summary>
        /// 事件信息
        /// </summary>
        [Serializable]
        public class CHDEventInfo
        {
            /// <summary>
            /// 设备id
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 门禁编号
            /// </summary>
            public int DoorNo { get; set; }
            /// <summary>
            /// 用户卡号
            /// </summary>
            public string UserCard { get; set; }
            /// <summary>
            /// 事件名称
            /// </summary>
            public string EventName { get; set; }
            /// <summary>
            /// 事件事件
            /// </summary>
            public DateTime EventTime { get; set; }
            /// <summary>
            /// 事件类型
            /// </summary>
            public CHDEventType EventType { get; set; }
            /// <summary>
            /// 门禁状态 0 关 1 开
            /// </summary>
            public int DoorState { get; set; }
            /// <summary>
            /// 进出 1进2出
            /// </summary>
            public int InOut { get; set; }
        }
        /// <summary>
        /// 设备操作参数结构
        /// </summary>
        [Serializable]
        public class CHDDoorUseInfo
        {
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            /// <remarks>登陆事件注册返回的ID</remarks>
            private int userId = -1;
            /// <summary>
            /// 报警事件返回ID
            /// </summary>
            /// <remarks>报警事件注册返回的ID</remarks>
            public int AlarmId { get; set; }
            /// <summary>
            /// 远程配置id
            /// </summary>
            /// <remarks>远程事件注册返回的ID</remarks>
            public int RemoteId { get; set; }
            /// <summary>
            /// 登录用户名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 登录用户密码
            /// </summary>
            public string UserPwd { get; set; }
            /// <summary>
            /// 登录设备地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 登录设备端口
            /// </summary>
            public decimal DevicePoint { get; set; }
            /// <summary>
            /// 设备名称
            /// </summary>
            public string DeviceName { get; set; }
            /// <summary>
            /// 设备地址
            /// </summary>
            public string DevicePosition { get; set; }
            /// <summary>
            /// 超时时间，单位毫秒.
            /// </summary>
            /// <remarks></remarks>
            public uint WaitTime { get; set; }
            /// <summary>
            /// 连接尝试次数（保留）
            /// </summary>
            public uint TryTimes { get; set; }
            /// <summary>
            /// 重连间隔，单位:毫秒
            /// </summary>
            public uint Interval { get; set; }
            /// <summary>
            /// 是否重连 0-不重连，1-重连
            /// </summary> 
            /// <remarks> 0-不重连，1-重连</remarks>
            public int EnableRecon { get; set; }
            /// <summary>
            /// 用户登录返回ID
            /// </summary>
            public int UserId
            {
                get
                {
                    return userId;
                }

                set
                {
                    userId = value;
                }
            }
        }
        /// <summary>
        /// 门禁参数
        /// </summary>
        public class CHDDoorParam
        {
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 门禁通道号
            /// </summary>
            public int DoorNo { get; set; }
            /// <summary>
            /// 门锁继电器执行时间0-25.5秒 单位0.1秒
            /// </summary>
            public int RelayDelay { get; set; }
            /// <summary>
            /// 开门持续时间0-255秒
            /// </summary>
            public int OpenDelay { get; set; }
            /// <summary>
            /// 红外报警确认时间0-255秒
            /// </summary>
            public int IrSureDelay { get; set; }
            /// <summary>
            /// 安防报警驱动时间0-255秒
            /// </summary>
            public int IrOnDelay { get; set; }
            /// <summary>
            /// 四字节控制参数
            /// </summary>
            public CHDByteParameter BytePar { get; set; }

        }
        /// <summary>
        /// 四字节控制参数
        /// </summary>
        public class CHDByteParameter
        {
            /// <summary>
            /// 门禁四字节控制参数 字节4参数
            /// </summary>
            public CHDByteParameter1 Parm1 { get; set; }
            /// <summary>
            /// 门禁四字节控制参数 字节2参数
            /// </summary>
            public CHDByteParameter2 Parm2 { get; set; }
            /// <summary>
            /// 门禁四字节控制参数 字节3参数
            /// </summary>
            public CHDByteParameter3 Parm3 { get; set; }
            /// <summary>
            /// 门禁四字节控制参数 字节4参数
            /// </summary>
            public CHDByteParameter4 Parm4 { get; set; }
        }
        /// <summary>
        /// 门禁消息
        /// </summary>
        public class CHDMsg
        {
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 消息
            /// </summary>
            public string Msg { get; set; }
            /// <summary>
            /// 返回值
            /// </summary>
            public int ReturnCode { get; set; }
            /// <summary>
            /// 执行是否成功
            /// </summary>
            public bool IsSuccessful { get; set; }
            /// <summary>
            /// 操作时间
            /// </summary>
            public DateTime Time { get; set; }
        }
        /// <summary>
        /// CHD门禁用户信息
        /// </summary>
        public class CHDUserInfo
        {
            /// <summary>
            /// 设备ip地址
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 卡号
            /// </summary>
            public string CardNo { get; set; }
            /// <summary>
            /// 用户编号 八位不重复编号
            /// </summary>
            public string UserNo { get; set; }
            /// <summary>
            /// 开门密码
            /// </summary>
            public string DoorPwd { get; set; }
            /// <summary>
            /// 有效期
            /// </summary>
            public DateTime LmtTime { get; set; }
            /// <summary>
            /// 门禁权限 1门1有权限 2门2有权限 3都有权限
            /// </summary>
            public int DoorRight { get; set; }
            /// <summary>
            /// 卡类型 
            /// </summary>
            public int CardPrivilege { get; set; }

        }
        /// <summary>
        /// 周计划
        /// </summary>
        public class WeekPlan
        {
            /// <summary>
            /// 门禁ip
            /// </summary>
            public string DoorIp{ get; set; }
            /// <summary>
            /// 门编号
            /// </summary>
            public int DoorId { get; set; }
            /// <summary>
            /// 周计划 长度7
            /// </summary>
            public List<DoorPlan> WPlan { get; set; }

        }
        /// <summary>
        /// 假日计划
        /// </summary>
        public class HolidayList
        {
            /// <summary>
            /// 门禁设备ip
            /// </summary>
            public string DeviceIp { get; set; }
            /// <summary>
            /// 门编号 3全部
            /// </summary>
            public int DoorId { get; set; }
            /// <summary>
            /// 开始月份
            /// </summary>
            public int LmtMonth { get; set; }
            /// <summary>
            /// 开始日期
            /// </summary>
            public int LmtDay { get; set; }
            /// <summary>
            /// 持续天数
            /// </summary>
            public int DayCount { get; set; }
            /// <summary>
            /// DayCount个计划列表
            /// </summary>
            public List<DoorPlan> HPlan { get; set; }
        }
        /// <summary>
        /// 时段计划
        /// </summary>
        public class DoorPlan
        {
            /// <summary>
            /// 天
            /// </summary>
            public int DayNum { get; set; }
            /// <summary>
            /// 第1类卡当天准进时段索引 0--31
            /// </summary>
            public int CardType1TimeIndex { get; set; }
            /// <summary>
            /// 第2类卡当天准进时段索引 0--31
            /// </summary>
            public int CardType2TimeIndex { get; set; }
            /// <summary>
            /// 第3类卡当天准进时段索引 0--31
            /// </summary>
            public int CardType3TimeIndex { get; set; }
            /// <summary>
            /// 第4类卡当天准进时段索引 0--31
            /// </summary>
            public int CardType4TimeIndex { get; set; }
            /// <summary>
            /// 当天门常开时段索引 0--31
            /// </summary>
            public int TodayOpenTimeIndex { get; set; }
            /// <summary>
            /// 当天刷卡加密码时段索引 0--31
            /// </summary>
            public int CardAndPWDTimeIndex { get; set; }
            /// <summary>
            /// 当天自动布防的时段索引 0--31
            /// </summary>
            public int AutoAalmingTimeIndex { get; set; }
            /// <summary>
            /// N+1屏蔽时段 0--31
            /// </summary>
            public int N1TimeIndex { get; set; }
        }
        /// <summary>
        /// 每天时间段
        /// </summary>
        public class DayTimeSplit
        {
            ///// <summary>
            ///// 设备ip
            ///// </summary>
            //public string DeviceIp { get;set; }
            /// <summary>
            /// 序号 0-31
            /// </summary>
            public int Index { get; set; }
            /// <summary>
            ///  开始时间1 HH：MM
            /// </summary>
            public string StartTimes1 { get; set; }
            /// <summary>
            ///  结束时间1 HH：MM
            /// </summary>
            public string EndTimes1 { get; set; }
            /// <summary>
            ///  开始时间2 HH：MM
            /// </summary>
            public string StartTimes2 { get; set; }
            /// <summary>
            ///  结束时间2 HH：MM
            /// </summary>
            public string EndTimes2 { get; set; }
            /// <summary>
            ///  开始时间3 HH：MM
            /// </summary>
            public string StartTimes3 { get; set; }
            /// <summary>
            ///  结束时间3 HH：MM
            /// </summary>
            public string EndTimes3 { get; set; }
            /// <summary>
            ///  开始时间4 HH：MM
            /// </summary>
            public string StartTimes4 { get; set; }
            /// <summary>
            ///  结束时间4 HH：MM
            /// </summary>
            public string EndTimes4 { get; set; }
            ///// <summary>
            ///// 时段列表
            ///// </summary>
            //public List<TimeSplit> list { get; set; }
        }
        /// <summary>
        /// 时间段
        /// </summary>
        public class TimeSplit
        {
            /// <summary>
            ///  开始时间 HH：MM
            /// </summary>
            public string StartTimes { get; set; }
            /// <summary>
            ///  结束时间 HH：MM
            /// </summary>
            public string EndTimes { get; set; }
         
        }

    }
}
