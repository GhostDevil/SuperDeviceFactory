using SuperDeviceFactory.CHDDoorAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using static SuperDeviceFactory.CHDDoor.CHDClass;

namespace SuperDeviceFactory.CHDDoor
{
    /// <summary>
    /// 说明：纽贝尔门禁管理
    /// 时间：2017-08-18
    /// 作者：痞子少爷
    /// </summary>
    public class CHDManager
    {
        #region 事件变量
        /// <summary>
        /// 操作信息委托
        /// </summary>
        /// <param name="msg">消息</param>
        public delegate void ReturnMsg(string msg);
        /// <summary>
        /// 操作信息
        /// </summary>
        public event ReturnMsg TextMsg;
        /// <summary>
        /// 事件信息委托
        /// </summary>
        /// <param name="msg">事件消息</param>
        public delegate void EventInfo(CHDEventInfo msg);
        /// <summary>
        /// 事件信息
        /// </summary>
        public event EventInfo EventMessage;
        /// <summary>
        /// 门禁操作集合
        /// </summary>
        List<CHDOperate> opera = new List<CHDOperate>();
        /// <summary>
        /// 设备对象集合
        /// </summary>
        List<CHDDoorUseInfo> useInfo = null;
        /// <summary>
        /// 服务运行状态
        /// </summary>
        bool serverIsStart = false;
        #endregion

        #region 获取对象
        /// <summary>
        /// 获取对应CHDOperate
        /// </summary>
        /// <param name="ip">门禁主机ip地址</param>
        /// <returns>返回CHDOperate对象</returns>
        CHDOperate GetOPByIP(string ip)
        {
            try
            {
                if (opera == null)
                    return null;
                for (int i = 0; i < opera.Count; i++)
                {
                    if (ip == opera[i].deviceIp)
                        return opera[i];
                    else
                        Thread.Sleep(10);
                }
            }
            catch (Exception) { }
            return null;
        }
        /// <summary>
        /// 获取门禁主机对象
        /// </summary>
        /// <param name="ip">门禁主机地址</param>
        /// <returns>返回门禁主机信息对象</returns>
        CHDDoorUseInfo GetUseInfo(string ip)
        {
            if (useInfo == null)
                return null;
            for (int i = 0; i < useInfo.Count; i++)
            {
                if (useInfo[i].DeviceIp != "" && useInfo[i].DevicePoint > 0)
                {
                    if(useInfo[i].DeviceIp==ip)
                        return useInfo[i];
                }
            }
            return null;
        }
        #endregion

        #region 登录登出
        /// <summary>
        /// 登录门禁主机
        /// </summary>
        /// <param name="infos">门禁主机信息</param>
        public void Login(List<CHDDoorUseInfo> infos)
        {
            useInfo = infos;
            for (int i = 0; i < useInfo.Count; i++)
            {
                if (useInfo[i].DeviceIp != "" && useInfo[i].DevicePoint > 0)
                {
                    (new Thread(new ParameterizedThreadStart(LoginDevice)) { IsBackground = true }).Start(useInfo[i]);
                    Thread.Sleep(10);
                }
            }
        }
        /// <summary>
        /// 重新登录门禁主机
        /// </summary>
        /// <param name="infos">门禁主机信息</param>
        public void ReLogin(CHDDoorUseInfo infos)
        {
            LoginOut(infos);
            CHDOperate op = GetOPByIP(infos.DeviceIp);
            if(null!=op)
                opera.Remove(op);
            (new Thread(new ParameterizedThreadStart(LoginDevice)) { IsBackground = true }).Start(infos);
        }
        private void LoginDevice(object obj)
        {
            CHDDoorUseInfo item = obj as CHDDoorUseInfo;
            CHDOperate op = new CHDOperate();
            op.CHDLogMsg += Op_CHDLogMsg;
            op.CHDEventInfo += Op_CHDEventInfo;
            serverIsStart = true;
            bool b = false;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    if(!serverIsStart)
                        return;
                    b = op.ConnectionDevice(item.DeviceIp, DeviceType.CHD806D2CP, portOrBaud: (int)item.DevicePoint, szSysPwd: item.UserName, szKeyPwd: item.UserPwd);
                    if (b)
                    {
                        opera.Add(op);
                        (new Thread(new ParameterizedThreadStart(LinkOn)) { IsBackground = true }).Start(op);
                        break;
                    }
                    else
                        Thread.Sleep(20);
                }
                catch (Exception) { }
            }
        }
        void LinkOn(object obj)
        {
            CHDOperate op= obj as CHDOperate;
            for (int j = 0; j < 5; j++)
            {
                if (op.isLinkOn)
                {
                    op.SetDeviceTime();
                    break;
                }
                else
                {
                    if (j == 4)
                        return;
                    Thread.Sleep(500);
                    continue;
                }
            }
        }
        /// <summary>
        /// 事件信息
        /// </summary>
        /// <param name="eventInfo"></param>
        private void Op_CHDEventInfo(CHDEventInfo eventInfo)
        {
            EventMessage?.Invoke(eventInfo);
        }

        /// <summary>
        /// 登出门禁主机
        /// </summary>
        public void LoginOut()
        {
            serverIsStart = false;
            if (opera.Count > 0)
            {
                for (int i = 0; i < opera.Count; i++)
                {
                    opera[i].DisConnectionDevice();
                    opera[i].CHDLogMsg -= Op_CHDLogMsg;
                    opera[i].CHDEventInfo -= Op_CHDEventInfo;
                    Thread.Sleep(10);
                }
                opera.Clear();
            }
        }
        /// <summary>
        /// 登出门禁主机
        /// </summary>
        /// <param name="userInfo">门禁主机信息</param>
        public bool LoginOut(CHDDoorUseInfo userInfo)
        {
            CHDOperate op = GetOPByIP(userInfo.DeviceIp);
            if (null != op)
            {
                return op.DisConnectionDevice();
            }
            return false;
        }
        private void Op_CHDLogMsg(CHDClass.CHDMsg msg)
        {
            TextMsg?.Invoke(msg.Msg);
            if (msg.Msg.Contains("无权限"))
                (new Thread(new ParameterizedThreadStart(ReDlink)) { IsBackground = true }).Start(msg);
        }
        void ReDlink(object obj)
        {
            CHDMsg msg = obj as CHDMsg;
            CHDOperate op = GetOPByIP(msg.DeviceIp);
            if (null != op)
            {
                op.DLinkOff(false);
                op.isLinkOn = op.LinkOn(true);
                for (int i = 0; i < 2; i++)
                {
                    if (!serverIsStart)
                        return;
                    if (!op.isLinkOn)
                    {
                        op.isLinkOn = op.LinkOn(true);
                        Thread.Sleep(500);
                    }
                    else
                        break;
                }
            }
        }
        #endregion

        #region 用户操作
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool AddCardUser(CHDUserInfo userInfo)
        {
            bool b = false;
            if (userInfo != null)
            {
                CHDOperate op = GetOPByIP(userInfo.DeviceIp);
                //CHDDoorUseInfo info = GetUseInfo(userInfo.DeviceIp);
                if (null != op)
                {
                    if (op.isLinkOn)
                    {
                        b = op.AddUser(userInfo);
                        return b;
                    }
                    else
                    {
                        TextMsg?.Invoke(string.Format("门禁主机 {0} 添加用户失败,无权限,请稍后再试···", userInfo.DeviceIp));
                        op.LinkOn();
                    }
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 添加用户失败,设备无法通讯,请稍后再试···", userInfo.DeviceIp));
            return b;
        }
        /// <summary>
        /// 删除用户信息根据卡号
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteUserByCard(string cardNo, string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.DeleteUserByCard(cardNo);
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 删除卡 {1} 用户失败,无权限,请稍后再试···", deviceIp,cardNo));
            }
                TextMsg?.Invoke(string.Format("门禁主机 {0} 删除卡 {1} 用户失败,设备无法通讯,请稍后再试···", deviceIp, cardNo));
            return b;
        }
        /// <summary>
        /// 删除超级权限卡
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteSuperCard(string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.DeleteSuperCard();
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 删除超级卡失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 删除超级卡失败···", deviceIp));
            return b;
        }
        /// <summary>
        /// 获取超级卡
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="superCard1">特权卡1</param>
        /// <param name="superCard2">特权卡2</param>
        /// <param name="superCard3">特权卡3</param>
        /// <param name="superCard4">特权卡4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetSuperCard(string deviceIp, ref string superCard1, ref string superCard2, ref string superCard3, ref string superCard4)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetSuperCard(ref superCard1, ref superCard2, ref superCard3, ref superCard4);
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 获取超级卡失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取超级卡失败···", deviceIp));
            return b;
        }
        /// <summary>
        /// 设置超级卡
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="superCard1">特权卡1</param>
        /// <param name="superCard2">特权卡2</param>
        /// <param name="superCard3">特权卡3</param>
        /// <param name="superCard4">特权卡4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetSuperCard(string deviceIp, string superCard1, string superCard2 = "", string superCard3 = "", string superCard4 = "")
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.SetSuperCard(superCard1, superCard2, superCard3, superCard4);
                    
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 设置超级卡失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 设置超级卡失败,设备无法通讯,请稍后再试···", deviceIp));
            return b;
        }
        ///// <summary>
        ///// 设置超级卡
        ///// </summary>
        ///// <param name="deviceIp">门禁主机ip</param>
        ///// <returns>true:成功 false:失败</returns>
        //public bool DelSuperCard(string deviceIp)
        //{
        //    CHDOperate op = GetOPByIP(deviceIp);
        //    bool b = false;
        //    if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
        //    {
        //        if (op.isLinkOn)
        //        {
        //            b = op.DeleteSuperCard();
        //            
        //            return b;
        //        }
        //    }
        //    TextMsg?.Invoke(string.Format("门禁主机 {0} 删除超级卡 {1} 用户失败···", deviceIp));
        //    return b;
        //}
        /// <summary>
        /// 设置超级密码
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="superPwd1">超级密码1</param>
        /// <param name="superPwd2">超级密码2</param>
        /// <param name="superPwd3">超级密码3</param>
        /// <param name="superPwd4">超级密码4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetSuperPwd(string deviceIp, string superPwd1 = "FFFFFFFF", string superPwd2 = "FFFFFFFF", string superPwd3 = "FFFFFFFF", string superPwd4 = "FFFFFFFF")
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.SetSuperPwd(superPwd1, superPwd2, superPwd3, superPwd4);
                    
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 设置超密码失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 设置超密码失败···", deviceIp));
            return b;
        }
        /// <summary>
        /// 删除超级密码
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteSuperPwd(string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.SetSuperPwd("FFFFFFFF", "FFFFFFFF", "FFFFFFFF", "FFFFFFFF");
                    
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 删除超密码失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 删除超密码失败···", deviceIp));
            return b;
        }
        /// <summary>
        /// 获取超级密码
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="superPwd1">超级密码1</param>
        /// <param name="superPwd2">超级密码2</param>
        /// <param name="superPwd3">超级密码3</param>
        /// <param name="superPwd4">超级密码4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetSuperPwd(string deviceIp, ref string superPwd1, ref string superPwd2, ref string superPwd3, ref string superPwd4)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetSuperPwd(ref superPwd1, ref superPwd2, ref superPwd3, ref superPwd4);
                    
                    return b;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 获取超密码失败,无权限,请稍后再试···", deviceIp));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取超密码失败···", deviceIp));
            return b;
        }
        /// <summary>
        /// 获取用户信息根据卡号
        /// </summary>
        /// <param name="cardNo">用户卡号</param>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>返回卡用户信息</returns>
        public CHDUserInfo GetUserInfoByCard(string cardNo, string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            CHDUserInfo info = new CHDUserInfo();
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    info = op.GetUserInfoByCard(cardNo);
                    return info;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 获取卡 {1} 用户失败,无权限,请稍后再试···", deviceIp, cardNo));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取卡 {1} 用户失败,设备无法通讯,请稍后再试···", deviceIp, cardNo));
            return null;
        }
        /// <summary>
        /// 获取用户信息根据编号
        /// </summary>
        /// <param name="userNo">用户变号</param>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>返回卡用户信息</returns>
        public CHDUserInfo GetUserInfoByNo(string userNo, string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            CHDUserInfo info = new CHDUserInfo();
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    info = op.GetUserInfoByNo(userNo);
                    
                    return info;
                }
                else
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 获取 {1} 无权限,请稍后再试···", deviceIp, userNo));
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取 {1} 用户失败,设备无法通讯,请稍后再试···", deviceIp,userNo ));
            return null;
        }

        #endregion

        #region 门禁操作
        /// <summary>
        /// 门禁远程操作
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="type">操作类型</param>
        /// <param name="doorId">门禁编号</param>
        /// <param name="delay">持续时间</param>
        /// <param name="userCard">用户门禁卡</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ControlDoor(string deviceIp, CHDControlDoorType type, uint doorId, uint delay, string userCard)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            if (null != op)
            {
               // CHDDoorUseInfo info = GetUseInfo(deviceIp);
                bool b = false;
                if (op.isLinkOn)
                {
                    switch (type)
                    {
                        case CHDControlDoorType.Open:
                            b = op.OpenDoor(doorId, delay, userCard);
                            break;
                        case CHDControlDoorType.StartOpen:
                            b = op.OpenAlwaysOpenDoor(doorId, delay, userCard);
                            break;
                        case CHDControlDoorType.StartClose:
                            b = op.OpenAlwaysCloseDoor(doorId, delay, userCard);
                            break;
                        case CHDControlDoorType.EndOpen:
                            b = op.CloseAlwaysOpenDoor(doorId, userCard);
                            break;
                        case CHDControlDoorType.EndClose:
                            b = op.CloseAlwaysCloseDoor(doorId, userCard);
                            break;
                    }
                    if (!b)
                    {
                        if (!op.ReConnection())
                        {
                            CHDDoorUseInfo info = GetUseInfo(deviceIp);
                            if (null != info)
                                ReLogin(info);
                        }
                        op.isLinkOn = op.LinkOn(true);
                    }
                    return b;
                }
                else
                {
                    TextMsg?.Invoke(string.Format("门禁主机 {0} 远程 {1} 操作失败,身份确认失败···", deviceIp + " " + doorId, type.ToString()));
                    if (!op.ReConnection())
                    {
                        CHDDoorUseInfo info= GetUseInfo(deviceIp);
                        if(null!=info)
                            ReLogin(info);
                    }
                    op.isLinkOn = op.LinkOn(true);
                    return false;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 远程 {1} 操作失败···", deviceIp + " " + doorId, type.ToString()));
            return false;
        }
        #endregion

        #region 门禁主机操作
        /// <summary>
        /// 获取门禁主机参数
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="doorId">门禁编号</param>
        /// <param name="param">参数信息</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetDeviceConfig(string deviceIp,int doorId,out CHDDoorParam param)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            string str = "";
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetDoorParam(doorId, out param);
                    if (!b)
                    {
                        op.LinkOn(true);
                        b = op.GetDoorParam(doorId, out param);
                    }
                    return b;
                }
                str = "无权限";
            }
            else
            {
                str = "设备无法通讯";
            }
            param = null;
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取参数失败,{1},请稍后再试···{2}", deviceIp, str,DateTime.Now));
            return b;
        }
        /// <summary>
        /// 设置门禁主机参数
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="param">参数信息</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetDeviceConfig(string deviceIp,CHDDoorParam param)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            string str = "";
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (!op.isLinkOn)
                    op.LinkOn(true);
                if (op.isLinkOn)
                {
                    b = op.SetDoorParam(param);
                    if (!b)
                    {
                        op.LinkOn(true);
                        b = op.SetDoorParam(param);
                    }
                    return b;
                }
                str = "无权限";
            }
            else
            {
                str = "设备无法通讯";
            }
            param = null;
            TextMsg?.Invoke(string.Format("门禁主机 {0} 设置参数失败,{1}，请稍后再试···{2}", deviceIp, str, DateTime.Now));
            return b;
        }
        /// <summary>
        /// 设置门禁主机时间
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetDeviceTime(string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (!op.isLinkOn)
                    op.isLinkOn = op.LinkOn(false);
                if (op.isLinkOn)
                {
                    b = op.SetDeviceTime();
                    if (!b)
                    {
                        CHDDoorUseInfo info = GetUseInfo(deviceIp);
                        if (null != info)
                            ReLogin(info);
                    }
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 同步时间 {1} 失败···", deviceIp, DateTime.Now));
            return b;
        }
        /// <summary>
        /// 寻卡号
        /// </summary>
        /// <param name="com">通讯端口</param>
        /// <returns>返回卡号</returns>
        public string ReaderCardNum(string com)
        {
            CHDOperate op = new CHDOperate();
            op.CHDLogMsg += Op_CHDLogMsg;
            if (op.ConnectionDevice(com, DeviceType.CHDCardReader, portOrBaud: 9600, isTcp: false))
            {
                string no = "";
                if (null != op && !string.IsNullOrWhiteSpace(com))
                    no = op.GetCardNum();
                op.DisConnectionDevice();
                return no;
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取卡号失败···", com, DateTime.Now));
            return "";
        }
        /// <summary>
        /// 寻卡号
        /// </summary>
        /// <param name="op">门禁主机操作对象</param>
        /// <returns>返回卡号</returns>
        public string ReaderCardNum(CHDOperate op)
        {
            string no = "";
            if (null != op)
                no = op.GetCardNum();
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取卡号失败···", op.deviceIp, DateTime.Now));
            return no;
        }
        #endregion

        #region 计划设置
        /// <summary>
        /// 设置周计划
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="wPlan">周计划</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetWeekPlan(string deviceIp, WeekPlan wPlan)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.SetWeekPlan(wPlan);
                    
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 设置周计划失败···", deviceIp, DateTime.Now));
            return false;
        }
        /// <summary>
        /// 获取周计划
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="wPlan">周计划</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetWeekPlan(string deviceIp, ref WeekPlan wPlan)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetWeePlan(ref wPlan);
                    
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 设置周计划失败···", deviceIp, DateTime.Now));
            return false;
        }
        /// <summary>
        /// 获取时间段
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="times">时间段</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetDayTimes(string deviceIp, ref List<DayTimeSplit> times)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetListTime(ref times);
                    
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取时间段失败···", deviceIp, DateTime.Now));
            return false;
        }
        /// <summary>
        /// 设置时间段
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="times">时间段</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetDayTimes(string deviceIp, List<DayTimeSplit> times)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.SetListTime(times);
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 获取时间段失败···", deviceIp, DateTime.Now));
            return false;
        }
        #endregion

        #region 记录读取
        /// <summary>
        /// 读取新纪录
        /// </summary>
        /// <param name="deviceIp">门禁主机ip地址</param>
        public void ReadOneRe(string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    if (op.ReadOneRec())
                        return;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 读取新纪录失败···", deviceIp, DateTime.Now));

        }

        /// <summary>
        /// 读取新纪录
        /// </summary>
        /// <param name="deviceIp">门禁主机ip地址</param>
        public void ReadOneReByPoint(string deviceIp)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0;
                    if (op.QueryRecStatu(ref nBottom, ref nSaveP, ref nLoadP, ref nMaxLen))
                    {
                        op.SetnLoadP((uint)nSaveP);
                        for (int i = nLoadP + 1; i <= nSaveP; i++)
                        {
                            op.ReadRecByPoint((short)0);
                            Thread.Sleep(5);
                        }
                    }

                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 读取新纪录失败···", deviceIp, DateTime.Now));

        }

        #endregion

        #region 门禁主机状态
        /// <summary>
        /// 获取门禁主机工作状态
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="state">门禁主机状态对象</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetWorkState(string deviceIp, ref CHDDeviceState state)
        {
            CHDOperate op = GetOPByIP(deviceIp);
            bool b = false;
            if (null != op && !string.IsNullOrWhiteSpace(deviceIp))
            {
                if (op.isLinkOn)
                {
                    b = op.GetWorkState(ref state);
                    
                    return b;
                }
            }
            TextMsg?.Invoke(string.Format("门禁主机 {0} 工作状态获取失败···", deviceIp));
            return b;
        }
        #endregion

    }
}
