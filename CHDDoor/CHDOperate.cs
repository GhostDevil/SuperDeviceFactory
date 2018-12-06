using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SuperDeviceFactory.CHDDoorAPI;
using static SuperDeviceFactory.CHDDoor.CHDClass;

namespace SuperDeviceFactory.CHDDoor
{
    /// <summary>
    /// 说明：纽贝尔门禁代理
    /// 时间：2017-08-18
    /// 作者：痞子少爷
    /// </summary>
    public class CHDOperate
    {
        ///// <summary>
        ///// 门禁状态委托
        ///// </summary>
        ///// <param name="msg">消息对象</param>
        //public delegate void CHDDoorState(CHDMsg msg);
        ///// <summary>
        ///// 门禁状态事件
        ///// </summary>
        //public event CHDDoorState CHDState;
        /// <summary>
        /// 门禁操作日志委托
        /// </summary>
        /// <param name="msg">消息对象</param>
        public delegate void CHDLog(CHDMsg msg);
        /// <summary>
        /// 门禁操作日志事件
        /// </summary>
        public event CHDLog CHDLogMsg;
        /// <summary>
        /// 门禁用户信息委托
        /// </summary>
        /// <param name="info">用户对象</param>
        public delegate void CHDDoorUserInfo(CHDUserInfo info);
        /// <summary>
        /// 门禁用户信息事件
        /// </summary>
        public event CHDDoorUserInfo CHDUserInfo;
        /// <summary>
        /// 门禁事件委托
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public delegate void CHDDoorEventInfo(CHDEventInfo eventInfo);
        /// <summary>
        /// 门禁事件
        /// </summary>
        public event CHDDoorEventInfo CHDEventInfo;
        /// <summary>
        /// 端口标识
        /// </summary>
        public int portIndex = 0;
        /// <summary>
        /// 门禁主机ip地址
        /// </summary>
        public string deviceIp = "";
        /// <summary>
        /// 网络id
        /// </summary>
        uint nNetID = 1;
        /// <summary>
        /// 门禁主机类型
        /// </summary>
        DeviceType deviceType;
        /// <summary>
        /// 系统密码4个(0-9）字符
        /// </summary>
        string sysPwd = "0000";
        /// <summary>
        /// 键盘密码6个(0-9）字符
        /// </summary>
        string keyPwd = "000000";
        /// <summary>
        /// 是否使用中
        /// </summary>
        bool isUseing = false;
        /// <summary>
        /// 是否已认证
        /// </summary>
        public bool isLinkOn=false;
        /// <summary>
        /// 权限检查定时器
        /// </summary>
        System.Timers.Timer checkPowerTimer = null;

        #region 连接门禁主机
        /// <summary>
        /// 连接门禁主机
        /// </summary>
        /// <param name="ipOrCom">ip地址或者com口</param>
        /// <param name="type">门禁主机类型</param>
        /// <param name="netID">网络id</param>
        /// <param name="portOrBaud">端口号或者波特率</param>
        /// <param name="isTcp">是否tcp连接</param>
        /// <param name="isAutoGetEventMsg">自动获取动态事件</param>
        /// <param name="szSysPwd">门禁主机系统密码 4个(0-9）字符</param>
        /// <param name="szKeyPwd">门禁主机键盘密码 6个(0-9）字符</param>
        /// <param name="nTimeOut">超时值，单位：毫秒</param>
        /// <returns>true：成功 false：失败</returns>
        public bool ConnectionDevice(string ipOrCom, DeviceType type, uint netID = 1, int portOrBaud = 4001, bool isTcp = true,bool isAutoGetEventMsg=true, string szSysPwd = "0000", string szKeyPwd = "000000", uint nTimeOut = 1000 * 60*60)
        {
            deviceIp = ipOrCom;
            deviceType = type;
            nNetID = netID;
            sysPwd = szSysPwd;
            keyPwd = szKeyPwd;
            bool b = false;
            string str = "";
            if (isTcp)
                portIndex = CHDCommon.OpenTcp(ipOrCom, (uint)portOrBaud);
            else
                portIndex = CHDCommon.OpenCom(ipOrCom, (uint)portOrBaud);
            if (portIndex >= 0)
            {
                if (type != DeviceType.CHDCardReader)
                {
                    SetTimeOut((uint)portIndex, nTimeOut);
                    str = string.Format("门禁主机 {0} 登录门禁主机成功！", deviceIp);
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = portIndex, Time = DateTime.Now });
                    DLinke();
                    if (checkPowerTimer == null)
                    {
                        checkPowerTimer = new System.Timers.Timer() { Interval = 1000 * 60 * 3.5 };
                        checkPowerTimer.Elapsed += (o, e) => {
                            //ReConnection();
                                SetDeviceTime();
                            //DLinke();
                            };
                    }
                    checkPowerTimer.Start();
                    if (msgThread == null && isAutoGetEventMsg)
                    {
                        msgThread = new Thread(new ThreadStart(StartReadNewMsg))
                        {
                            IsBackground = true
                        };
                        isReadMsg = true;
                        msgThread.Start();
                    }
                }
                b = true;
            }
            else
            {
                str = string.Format("门禁主机 {0} 登录门禁主机失败！ 错误码:{1}", deviceIp, portIndex);
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = portIndex, Time = DateTime.Now });
            }
            return b;
        }
        /// <summary>
        /// 设备认证
        /// </summary>
        /// <param name="showMsg">是否显示日志</param>
        private void DLinke(bool showMsg=true)
        {
            //isLinkOn = DLinkOff(false);
            for (int i = 0; i < 5; i++)
            {
                //if (checkPowerTimer == null)
                //    return;
                if (!isLinkOn)
                {
                    isLinkOn = LinkOn(showMsg);
                    Thread.Sleep(10);
                }
                else
                    break;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public bool DisConnectionDevice()
        {
            bool b = false;
            try
            {
                isReadMsg = false;
                Thread.Sleep(10);
                if (msgThread != null)
                    msgThread.Abort();
                msgThread = null;
            }
            catch (Exception) { }
            if (checkPowerTimer != null)
            {
                checkPowerTimer.Stop();
                checkPowerTimer = null;
            }
            string str = "";
            isLinkOn = DLinkOff();
            portIndex = CHDCommon.ClosePort((uint)portIndex);
            if (portIndex == 0)
            {
                str = string.Format("门禁主机 {0} 登出门禁主机成功！", deviceIp);
                b = true;
            }
            else
                str = string.Format("门禁主机 {0} 登出门禁主机失败！ 错误码:{1}", deviceIp, portIndex);
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = portIndex, Time = DateTime.Now });
            return b;
        }
        /// <summary>
        /// 设置超时
        /// </summary>
        /// <param name="nPortIndex">端口标识(OpenCom或者OpenTcp的成功返回值)</param>
        /// <param name="nTimeOut">超时值，单位：毫秒</param>
        /// <returns>true：成功 false：失败</returns>
        public bool SetTimeOut(uint nPortIndex, uint nTimeOut=1000*60)
        {
            int nRetValue = -1;
            nRetValue=CHDCommon.SetTimeOuts(nPortIndex, nTimeOut);
            if (nRetValue < 0)
                return false;
            return true; 
        }
        /// <summary>
        ///重连门禁主机
        /// </summary>
        /// <returns>true：成功 false：失败</returns>
        public bool ReConnection()
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHDCommon.ReConectPort((uint)portIndex);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            //int x = CHDCommon.ReConectPort((uint)userId);
            if (nRetValue == 0)
            {
                LinkOn();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 访问权限确认
        /// <para>四分钟后无请求设置操作则需要从新确认</para>
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool LinkOn(bool isShowLog=true)//string szSysPwd = "0000", string szKeyPwd = "000000"
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    nRetValue = CHD805.LinkOn((uint)portIndex, nNetID, sysPwd, keyPwd);
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DLinkOn((uint)portIndex, nNetID, sysPwd, keyPwd);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    nRetValue = CHD806D4C.DLinkOn((uint)portIndex, nNetID, sysPwd, keyPwd);
                    break;
                case DeviceType.CHD806D4M3:
                    nRetValue = CHD806D4M3.DLinkOn((uint)portIndex, nNetID, sysPwd, keyPwd);
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:      //成功
                    if(isShowLog)
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 确认权限成功！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    isLinkOn = true;
                    return isLinkOn;
                case 0x07:      //无权限
                    if (isShowLog)
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 确认权限失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    isLinkOn = false;
                    return isLinkOn;
                default:        //其他值表示失败
                    if (isShowLog)
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 确认权限失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    isLinkOn = false;
                    return isLinkOn;
            }
        }
        /// <summary>
        /// 访问取消
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool DLinkOff(bool isShowLog=true)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    nRetValue = CHD805.LinkOff((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DLinkOff((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    nRetValue = CHD806D4C.DLinkOff((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D4M3:
                    nRetValue = CHD806D4M3.DLinkOff((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:      //成功
                    if(isShowLog)
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 取消权限成功！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:      //无权限
                    //if (isShowLog)
                    //    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 取消权限失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    //return false;
                default:        //其他值表示失败
                    if (isShowLog)
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 取消权限失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        #endregion

        #region 特权设置
        /// <summary>
        /// 读取特权卡
        /// </summary>
        /// <param name="superCard1">输出特权卡1</param>
        /// <param name="superCard2">输出特权卡2</param>
        /// <param name="superCard3">输出特权卡3</param>
        /// <param name="superCard4">输出特权卡4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetSuperCard(ref string superCard1, ref string superCard2, ref string superCard3, ref string superCard4)
        {
            StringBuilder szCard1 = new StringBuilder();
            StringBuilder szCard2 = new StringBuilder();
            StringBuilder szCard3 = new StringBuilder();
            StringBuilder szCard4 = new StringBuilder();
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadSuperCard((uint)portIndex, nNetID, szCard1, szCard2, szCard3, szCard4);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }

            switch (nRetValue)
            {
                case 0x00:
                    superCard1 = szCard1.ToString().Replace("\0", "");
                    superCard2 = szCard2.ToString().Replace("\0", "");
                    superCard3 = szCard3.ToString().Replace("\0", "");
                    superCard4 = szCard4.ToString().Replace("\0", "");
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 获取超级卡成功! 卡1:{1} 卡2:{2}  卡3：{3}  卡4：{4}", deviceIp, superCard1, superCard2, superCard3, superCard4), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 获取超级卡成功失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }

        }
        /// <summary>
        /// 设置特权卡
        /// </summary>
        /// <param name="superCard1">特权卡1</param>
        /// <param name="superCard2">特权卡2</param>
        /// <param name="superCard3">特权卡3</param>
        /// <param name="superCard4">特权卡4</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetSuperCard(string superCard1, string superCard2 = "", string superCard3 = "", string superCard4 = "")
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DSetSuperCardEx((uint)portIndex, nNetID, superCard1, superCard2, superCard3, superCard4);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }

            switch (nRetValue)
            {
                case 0x00:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置超级卡成功", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置超级卡失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 设置超级卡成功失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 删除特权卡
        /// </summary>
        /// <param name="doorNo">门编号 1门1 2门2 ,暂为0</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteSuperCard(uint doorNo=0)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DDeleteSuperCardEx((uint)portIndex, nNetID, doorNo);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 删除超级卡成功", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 删除超级卡成功！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 删除超级卡成功！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 读取超级密码
        /// </summary>
        /// <param name="door1Pwd1">输出门禁1密码1</param>
        /// <param name="door1Pwd2">输出门禁1密码2</param>
        /// <param name="door2Pwd1">输出门禁2密码1</param>
        /// <param name="door2Pwd2">输出门禁2密码2</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetSuperPwd(ref string door1Pwd1, ref string door1Pwd2, ref string door2Pwd1, ref string door2Pwd2)
        {

            byte[] szPwd1 = new byte[10];
            byte[] szPwd2 = new byte[10];
            byte[] szPwd3 = new byte[10];
            byte[] szPwd4 = new byte[10];
            int nRetValue = 0;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadSuperPwd((uint)portIndex, nNetID, szPwd1, szPwd2, szPwd3, szPwd4);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            if (0x00 == nRetValue)
            {
                door1Pwd1 = Encoding.Default.GetString(szPwd1).Replace("\0", "");
                door1Pwd2 = Encoding.Default.GetString(szPwd2).Replace("\0", "");
                door2Pwd1 = Encoding.Default.GetString(szPwd3).Replace("\0", "");
                door2Pwd2 = Encoding.Default.GetString(szPwd4).Replace("\0", "");
                string str = string.Format("门禁主机 {0} 操作成功！ 第一门密码1:{1} 第一门密码2:{2} 第二门密码1:{3} 第二门密码2:{4} ", deviceIp,
                    door1Pwd1, door1Pwd2, door2Pwd1, door2Pwd2);
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
                return true;
            }
            else
            {
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 操作失败！ 错误码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                return false;
            }
        }

        /// <summary>
        /// 设置超级密码
        /// </summary>
        /// <param name="door1Pwd1">门禁1密码1</param>
        /// <param name="door1Pwd2">门禁1密码2</param>
        /// <param name="door2Pwd1">门禁2密码1</param>
        /// <param name="door2Pwd2">门禁2密码2</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetSuperPwd(string door1Pwd1, string door1Pwd2 = "FFFFFFFF", string door2Pwd1 = "FFFFFFFF", string door2Pwd2 = "FFFFFFFF")
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DSetSuperPwd((uint)portIndex, nNetID, door1Pwd1, door1Pwd2, door2Pwd1, door2Pwd2);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }

            switch (nRetValue)
            {
                case 0x00:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置密码成功", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置密码失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 设置密码失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }

        #endregion

        #region 记录读取
        /// <summary>
        /// 初始化记录区
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool InitRec()
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    nRetValue = CHD805.InitRec((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DInitRec((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    // nRetValue = CHD806D4C.((uint)PortIndex, nNetID);
                    break;
                case DeviceType.CHD806D4M3:
                    // nRetValue = CHD806D4M3.InitRec((uint)PortIndex, nNetID);
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//Success
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 初始化记录成功！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0xE4:		//Failed
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 门禁主机内无记录！！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE5:		//Failed
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 初始化记录失败！错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                default:		//Failed
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 初始化记录失败！ 错误代码: {0}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 设定读指针位置
        /// </summary>
        /// <param name="nLoadP">读指针位置</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetnLoadP(uint nLoadP)
        {
            int nRetValue = 0;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.D4SetRecReadPoint((uint)portIndex, nNetID, nLoadP);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//Success
                    //CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置读指针位置 {1} 成功！", deviceIp, nLoadP), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//Failed
                    //CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 设置读指针位置 {1} ！ 错误代码: {1}", deviceIp, nLoadP, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }

        }
        /// <summary>
        /// 门禁主机记录区状态
        /// </summary>
        /// <param name="nBottom">返回开始位置BOTTOM</param>
        /// <param name="nSaveP">返回存储位置SAVEP</param>
        /// <param name="nLoadP">返回未读数量LOADP</param>
        /// <param name="nMaxLen">返回最大容量MAXLEN</param>
        /// <returns>true:成功 false:失败</returns>
        public bool QueryRecStatu(ref int nBottom, ref int nSaveP, ref int nLoadP, ref int nMaxLen)
        {
            int nRetValue = 0;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadRecInfo((uint)portIndex, nNetID, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
                    
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//Success
                    string s = string.Format("读取记录区状态成功! 开始位置Bottom={0} 存储数量nSaveP={1} 未读数量nLoadP={2} 最大容量nMaxLen={3}", nBottom, nSaveP, nLoadP, nMaxLen);
                    //CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = s, ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:		//Failed
                    //CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取记录状态失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 读取记录线程
        /// </summary>
        Thread msgThread = null;
        /// <summary>
        /// 读取记录线程状态
        /// </summary>
        bool isReadMsg = true;
        int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0;
        /// <summary>
        /// 读取新纪录
        /// </summary>
        public void StartReadNewMsg()
        {
            while (isReadMsg)
            {
                if (!isLinkOn)
                    isLinkOn = LinkOn(false);
                if (isLinkOn)
                    ReadOneRec();
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 读取新纪录依据指针位置
        /// </summary>
        public void StartReadMsgByPoint()
        {
            while (isReadMsg)
            {
                if (QueryRecStatu(ref nBottom, ref nSaveP, ref nLoadP, ref nMaxLen))
                {
                    for (int i = nLoadP + 1; i <= nSaveP; i++)
                    {
                        ReadRecByPoint((short)i);
                        Thread.Sleep(10);
                    }
                    SetnLoadP((uint)nSaveP);
                    if (nSaveP == nMaxLen)
                        InitRec();
                    Thread.Sleep(10);
                }
                Thread.Sleep(10);
            }
        }
        /// <summary>
        /// 读取新纪录
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool ReadOneRec()
        {
            try
            {
                SYSTEMTIME RecTime = new SYSTEMTIME();
                int nRecWorkState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
                StringBuilder szRecSource = new StringBuilder();
                //byte[] szRecSource = new byte[] { };
                int nRetValue = 0;
                switch (deviceType)
                {
                    case DeviceType.CHD200G:
                        break;
                    case DeviceType.CHD200H:
                        break;
                    case DeviceType.CHD601D_M3:
                        break;
                    case DeviceType.CHD603S:
                        break;
                    case DeviceType.CHD689:
                        break;
                    case DeviceType.CHD805:
                        //CHD805.ReadNewRec((uint)portIndex, nNetID, szRecSource, ref RecTime, ref nRecState, ref nRecRemark);
                        break;
                    case DeviceType.CHD806D2CP://DReadRecWithPoint
                        CHD806D2CP.DReadOneRec((uint)portIndex, nNetID, szRecSource, ref RecTime, out nRecWorkState, out nRecRemark, out nRecLineState, out nDoorID);
                        break;
                    case DeviceType.CHD806D2M3B:
                        break;
                    case DeviceType.CHD806D4C:
                        // nRetValue = CHD806D4C.((uint)PortIndex, nNetID);
                        break;
                    case DeviceType.CHD806D4M3:
                        // nRetValue = CHD806D4M3.InitRec((uint)PortIndex, nNetID);
                        break;
                    case DeviceType.CHD815T_M3:
                        break;
                    case DeviceType.CHD825T:
                        break;
                    case DeviceType.CHDBank:
                        break;
                    case DeviceType.CHDCardReader:
                        break;
                    case DeviceType.CHDIOCtrl:
                        break;
                    case DeviceType.CHDLH:
                        break;
                    case DeviceType.CHDT5:
                        break;
                    case DeviceType.CHDTHSendor:
                        break;
                    default:
                        break;
                }
                if (nRecWorkState == 0 && nRecRemark == 0 && nRecLineState == 0 && nDoorID == 0)
                    return true;
                switch (nRetValue)
                {
                    case 0x00:      //Success
                        string s = FormatLog(deviceIp, nDoorID, szRecSource.ToString().Substring(0, 10), CHDCommon.ParasTime(RecTime), nRecWorkState, nRecRemark, nRecLineState, nDoorID);
                        if (!s.Contains("未知记录") && !s.Contains("控制参数修改"))
                            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = s, ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    case 0xE4:      //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 内无记录！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    case 0xE5:      //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 所有记录已经读完！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    default:        //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取记录失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                        return false;
                }
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 读取新纪录
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool ReadRecByPoint(int point)
        {
            try
            {
                SYSTEMTIME RecTime = new SYSTEMTIME();
                int nRecWorkState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
                 StringBuilder szRecSource = new StringBuilder();
                //byte[] szRecSource = new byte[] { };
                int nRetValue = 0;
                switch (deviceType)
                {
                    case DeviceType.CHD200G:
                        break;
                    case DeviceType.CHD200H:
                        break;
                    case DeviceType.CHD601D_M3:
                        break;
                    case DeviceType.CHD603S:
                        break;
                    case DeviceType.CHD689:
                        break;
                    case DeviceType.CHD805:
                        //CHD805.ReadNewRec((uint)PortIndex, nNetID, sb, ref time, ref nRecState, ref nRecRemark);
                        break;
                    case DeviceType.CHD806D2CP://DReadRecWithPoint
                        //CHD806D2CP.DReadRecByPoint((uint)portIndex, nNetID, (short)point, szRecSource, out RecTime, out nRecWorkState, out nRecRemark, out nRecLineState, out nDoorID);
                        //CHD806D2CP.DReadRecWithPoint((uint)portIndex, nNetID, out point, szRecSource, ref RecTime, out nRecWorkState, out nRecRemark, out nRecLineState, out nDoorID);
                        CHD806D2CP.DReadOneNewRec((uint)portIndex, nNetID, szRecSource, out RecTime, out nRecWorkState, out nRecRemark, out nRecLineState, out nDoorID);
                        break;
                    case DeviceType.CHD806D2M3B:
                        break;
                    case DeviceType.CHD806D4C:
                        // nRetValue = CHD806D4C.((uint)PortIndex, nNetID);
                        break;
                    case DeviceType.CHD806D4M3:
                        // nRetValue = CHD806D4M3.InitRec((uint)PortIndex, nNetID);
                        break;
                    case DeviceType.CHD815T_M3:
                        break;
                    case DeviceType.CHD825T:
                        break;
                    case DeviceType.CHDBank:
                        break;
                    case DeviceType.CHDCardReader:
                        break;
                    case DeviceType.CHDIOCtrl:
                        break;
                    case DeviceType.CHDLH:
                        break;
                    case DeviceType.CHDT5:
                        break;
                    case DeviceType.CHDTHSendor:
                        break;
                    default:
                        break;
                }
                if (nRecWorkState == 0 && nRecRemark == 0 && nRecLineState == 0 && nDoorID == 0)
                    return true;
                switch (nRetValue)
                {
                    case 0x00:      //Success
                        string s = FormatLog(deviceIp, nDoorID, szRecSource.ToString().Substring(0, 10), CHDCommon.ParasTime(RecTime), nRecWorkState, nRecRemark, nRecLineState,nDoorID);
                        if (!s.Contains("未知记录") && !s.Contains("控制参数修改"))
                            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = s, ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    case 0xE4:      //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 内无记录！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    case 0xE5:      //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 所有记录已经读完！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                        return true;
                    default:        //Failed
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取记录失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                        return false;
                }
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// 消息解析
        /// </summary>
        /// <param name="deviceIp">门禁主机ip</param>
        /// <param name="doorId">门禁编号</param>
        /// <param name="szRecSourceT">卡号id号等</param>
        /// <param name="RecTime">事件时间</param>
        /// <param name="nRecWorkState">工作状态</param>
        /// <param name="nRecRemark">备注</param>
        /// <param name="nRecLineState">线路状态</param>
        /// <param name="doorNo">门编号</param>
        /// <returns>消息字符串</returns>
        public string FormatLog(string deviceIp, int doorId, string szRecSourceT, DateTime RecTime, int nRecWorkState, int nRecRemark, int nRecLineState, int doorNo)
        {
            string strLog = "";
            string strTemp = "";
            string eventStr = "";
            string work = GetWorkStateNo(nRecWorkState);
            string line= GetWorkStateNo(nRecLineState);
            int lstate = -1;
            if (line.Length >= 8)
                lstate = int.Parse(line.Substring(4, 1));
            CHDEventType type=  CHDEventType.Info;
            switch (nRecRemark)
            {
                case 0:
                    strLog = string.Format("门禁主机{4} 时间:{0} 刷卡开门 卡号:{1} 方向：{2} 通道：{3} 门状态：{5}", RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp,lstate);
                    eventStr = "刷卡开门";
                    type = CHDEventType.Info;
                    
                    //strTemp += (nRecWorkState & 0x80) ?"密码验证成功;" :"密码验证失败;";
                    //strTemp += (nRecWorkState & 0x40) ? _T("门初始态:开;") : _T("门初始态:关;");
                    //strTemp += (nRecWorkState & 0x01) ? _T("开门后规定延时内未进入;") : _T("开门后规定延时内进入;");
                    //strTemp += (nRecWorkState & 0x20) ? _T("进入后在规定时间内未关门;") : _T("进入后在规定时间内关门;");
                    //strTemp += (nRecState & 0x10) ? _T("等待时间后门未关闭;") : _T("等待时间后门关闭;");
                    //strTemp += (nRecState & 0x08) ? _T("关闭红外监控;") : _T("");
                    //strTemp += (nRecState & 0x04) ? _T("胁迫状态;") : _T("正常刷卡;");
                    //strTemp += (nRecState & 0x02) ? _T("分体刷卡;") : _T("主体刷卡;");
                    //strTemp += (nRecState & 0x01) ? _T("红外监控初始态:开;") : _T("红外监控初始态:关;");
                    strTemp = string.Format("  状态字节:0x{0}, 线路字节:0x%{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 1:
                    //strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    //if()
                    strLog = string.Format("门禁主机{0} 时间:{1} 键入用户ID及个人密码开门 方向：{2} 通道：{3} 门状态：{4}", deviceIp, RecTime, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo,lstate);//键入用户ID及个人密码开门
                    type = CHDEventType.Info;
                    eventStr = "ID及密码开门";
                    strTemp = string.Format("状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 2:
                    strLog = string.Format("门禁主机{0} 时间:{1} 远程开门 用户：{2} 通道：{3} 门状态：{4}", deviceIp ,RecTime,szRecSourceT, doorNo,lstate);
                    type = CHDEventType.Info;
                    eventStr = "远程开门";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 3:
                    strLog = string.Format("门禁主机{0} 时间:{1} 手动开门 通道：{2} 门状态：{3}", deviceIp, RecTime, doorNo,lstate);
                    type = CHDEventType.Info;
                    eventStr = "手动开门";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 4:
                    break;
                case 5:
                    switch (int.Parse(szRecSourceT))
                    {
                        case 0:
                            eventStr = "红外报警开始";
                            type = CHDEventType.Alarm;
                            break;
                        case 1:
                            eventStr = "红外报警停止";
                            type = CHDEventType.Info;
                            break;
                        case 2:
                            eventStr = "异常开门";
                            type = CHDEventType.Alarm;
                            break;
                        case 3:
                            eventStr = "门被关闭（非正常开门）";
                            type = CHDEventType.Info;
                            break;
                        case 7:
                            eventStr = "入侵(红外)监测关闭";
                            type = CHDEventType.SetInfo;
                            break;
                        case 8:
                            eventStr = "入侵(红外)监测开启";
                            type = CHDEventType.SetInfo;
                            break;
                        case 9:
                            eventStr = "门碰开关监测关闭";
                            type = CHDEventType.SetInfo;
                            break;
                        case 10:
                            eventStr = "门碰开关监测开启";
                            type = CHDEventType.SetInfo;
                            break;
                        case 30:
                        case 82:
                            eventStr = "门磁打开超时(未进入)";//合法刷卡在规定延时内未开门进入
                            type = CHDEventType.Alarm;
                            break;
                        case 32:
                        case 84:
                            eventStr = "门磁超时未关(进入后)";//合法进入后但在规定延时内未关好门，一直开着
                            type = CHDEventType.Alarm;
                            break;
                        case 22:
                            if (szRecSourceT.Substring(4, 1) == "0")
                                eventStr = "紧急输入有效开始记录";
                            else
                                eventStr = "紧急输入结束记录";
                            type = CHDEventType.Info;
                            break;
                        default :
                            eventStr = "其他告警事件";
                            type = CHDEventType.Alarm;
                            break;
                    }
                    strLog = string.Format("门禁主机{0} 时间:{1} {3} 门状态：{2}", deviceIp, RecTime,lstate, eventStr);//告警
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 6:
                    strLog = string.Format("门禁主机{0} 时间:{1} 门禁主机掉电 门状态：{2}", deviceIp, RecTime,lstate);//掉电
                    type = CHDEventType.Alarm;
                    eventStr = "门禁主机掉电";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 7:
                    strLog = string.Format("门禁主机{0} 时间:{1} 内部控制参数修改 门状态：{2}", deviceIp, RecTime,lstate);//内部控制参数被修改
                    type = CHDEventType.SetInfo;
                    eventStr = "内部控制参数修改";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 8:
                    strLog = string.Format("门禁主机{4} 时间:{0} 无效刷卡 卡号:{1} 方向：{2} 通道：{3} 门状态：{5}", RecTime, szRecSourceT.ToString().Substring(0, 10), work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp,lstate);//无效刷卡开门//RecTime, szRecSourceT.ToString().Substring(0, 10)
                    eventStr = "无效刷卡";
                    type = CHDEventType.Alarm;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 9:
                    strLog = string.Format("门禁主机{2} 时间:{0} 用户卡过期 卡号:{1}", RecTime, szRecSourceT, deviceIp);//用户卡已过期
                    eventStr = "用户卡过期";
                    type = CHDEventType.Alarm;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 10:
                    strLog = string.Format("门禁主机{4} 时间:{0} 不在允许时间内 卡号:{1} 方向：{2} 通道：{3} 门状态：{5}", RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp,lstate);//当前时间该用户卡无进入权限
                    eventStr = "无效时段";
                    type = CHDEventType.Alarm;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 11:
                    strLog = string.Format("门禁主机{4} 时间:{0} 密码认证失败超次 卡号:{1} 方向：{2} 通道：{3} 门状态：{5}", RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp,lstate);//密码失败次数越限
                    eventStr = "密码认证失败超次";
                    type = CHDEventType.Alarm;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 15:
                    strLog = string.Format("门禁主机{0} 时间:{1} 门禁授权", deviceIp, RecTime);
                    eventStr = "门禁授权";
                    type = CHDEventType.SetInfo;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 16:
                    strLog = string.Format("门禁主机{0} 时间:{1} 撤销权限", deviceIp, RecTime);
                    eventStr = "门禁撤权";
                    type = CHDEventType.SetInfo;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{01}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x22:
                    if ((szRecSourceT.ToString()).Substring(4, 1) == "1")
                    {
                        strLog = string.Format("门禁主机{0} 时间:{1} 紧急事件开始", deviceIp, RecTime);
                        eventStr = "紧急事件开始";
                        type = CHDEventType.Alarm;
                    }
                    else
                    {
                        strLog = string.Format("门禁主机{0} 时间:{1} 紧急事件结束", deviceIp, RecTime);
                        eventStr = "紧急事件结束";
                        type = CHDEventType.Info;
                    }
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x40:
                    strLog = string.Format("门禁主机{0} 时间:{1} 合法刷卡等待中心确认开门 卡号:{2} 方向：{3} 通道：{4} 门状态：{5}", deviceIp, RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo,lstate);// 合法刷卡等待中心确认开门
                    eventStr = "合法刷卡待中心开门";
                    type = CHDEventType.Info;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x60:
                    strLog = string.Format("门禁主机{4} 时间:{0} 身份验证通过 卡号:{1} 方向：{2} 通道：{3} 门状态：{5}", RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp,lstate);//合法刷卡本地确认开门
                    eventStr = "身份验证通过";
                    type = CHDEventType.Info;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState, nRecLineState);//Dialog box is not enough to show log
                    break;
                case 0x61:
                    strLog = string.Format("门禁主机{0} 时间:{1} 本地双紧急密码认证通过 门状态：{2}", deviceIp, RecTime,lstate);//合法本地输入两个紧急密码确认开门
                    eventStr = "本地双紧急密码认证通过";
                    type = CHDEventType.Info;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x62:
                    strLog = string.Format("门禁主机{0} 时间:{1} 门磁打开 门状态：{2}", deviceIp, RecTime,lstate);//合法刷卡本地驱动开门继电器后，经判别门磁确认门已开
                    eventStr = "门磁开启";
                    type = CHDEventType.Info;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x63:
                    strLog = string.Format("门禁主机{0} 时间:{1} 门磁关闭 门状态：{2}", deviceIp, RecTime,lstate);//合法刷卡本地开门后，在规定的延时内门被正常关闭
                    eventStr = "门磁关闭";
                    type = CHDEventType.Info;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x70:
                    
                    if (work.Substring(5, 1) == "1")
                    {
                        eventStr = "超级卡胁迫开门";
                        type = CHDEventType.Alarm;
                    }
                    else
                    {
                        eventStr = "超级卡开门";
                        type = CHDEventType.Info;
                    }
                    strLog = string.Format("门禁主机{4} 时间:{0} {5} 卡号:{1} 方向：{2} 通道：{3} 门状态：{6}", RecTime, szRecSourceT, work.Substring(6, 1) == "1" ? "出门" : "进门", doorNo, deviceIp, eventStr,lstate);//时间:{0} 超权限卡刷卡开门记录
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x71:
                    strLog = string.Format("门禁主机{2} 时间:{0} 增加了1张超级卡 卡号:{1}", RecTime, szRecSourceT, deviceIp);//增加了1张超权限卡的记录
                    eventStr = "增加超级卡";
                    type = CHDEventType.SetInfo;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x72:
                    strLog = string.Format("门禁主机{2} 时间:{0} 删除了1张超级卡 卡号:{1}", RecTime, szRecSourceT, deviceIp);//删除了1张超权限卡的记录
                    eventStr = "删除超级卡";
                    type = CHDEventType.SetInfo;
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0X41:
                    strLog = string.Format("门禁主机{0} 时间:{1} 远程常闭门或解除 用户：{2} 通道：{3} 门状态：{4}", deviceIp, RecTime, szRecSourceT, doorNo,lstate);
                    type = CHDEventType.Info;
                    if (work == "0")
                        eventStr = "远程常闭门解除";
                    else
                        eventStr = "远程常闭门开启";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0X42:
                    strLog = string.Format("门禁主机{0} 时间:{1} 延时时间到解除常闭门 用户：{2} 通道：{3} 门状态：{4}", deviceIp, RecTime, szRecSourceT, doorNo,lstate);
                    type = CHDEventType.Info;
                    eventStr = "延时时间到解除常闭门";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0X43:
                    strLog = string.Format("门禁主机{0} 时间:{1} 远程常开门或解除 用户：{2} 通道：{3} 门状态：{4}", deviceIp, RecTime, szRecSourceT, doorNo,lstate);
                    type = CHDEventType.Info;
                    if (work == "0")
                        eventStr = "远程常开门解除";
                    else
                        eventStr = "远程常开门开启";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0X44:
                    strLog = string.Format("门禁主机{0} 时间:{1} 延时时间到解除常开门 用户：{2} 通道：{3} 门状态：{4}", deviceIp, RecTime, szRecSourceT, doorNo,lstate);
                    type = CHDEventType.Info;
                    eventStr = "延时时间到解除常开门";
                    strTemp = string.Format("  状态字节:{0}, 线路字节:{1}", nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                default:
                    strTemp = string.Format("门禁主机{3} 未知记录！ 备注字节:{0} 状态字节:{1}, 线路字节:{2}", nRecRemark, nRecWorkState.ToString("X2"), nRecLineState.ToString("X2"), deviceIp);
                    eventStr = "未知记录";
                    type = CHDEventType.Info;
                    break;
            }
            if (!strTemp.Contains("未知记录") && !strTemp.Contains("控制参数修改") )
                CHDEventInfo?.Invoke(new CHDClass.CHDEventInfo() { DeviceIp = deviceIp, DoorNo = doorId, EventName = eventStr, EventTime = RecTime, EventType = type, UserCard = szRecSourceT.ToString(), DoorState= lstate, InOut= int.Parse(work.Substring(6, 1)==""?"1": work.Substring(6, 1)) });
            strLog += strTemp;
            return strLog;
        }
        string GetWorkStateNo(int pnRecWorkState)
        {
            string param = Convert.ToString(pnRecWorkState, 2);
            string pm = param;
            for (int k = 0; k < 8 - param.Length; k++)
            {
                pm = ("0" + pm);
            }
            return pm;
        }

        ///// <summary>
        ///// 当前门禁主机记录总数
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public int GetMsgCount(object sender, EventArgs e)
        //{

        //    int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0, nRecordCount = 0;
        //    int nRetValue = -1;
        //    switch (deviceType)
        //    {
        //        case DeviceType.CHD200G:
        //            break;
        //        case DeviceType.CHD200H:
        //            break;
        //        case DeviceType.CHD601D_M3:
        //            break;
        //        case DeviceType.CHD603S:
        //            break;
        //        case DeviceType.CHD689:
        //            break;
        //        case DeviceType.CHD805:
        //            break;
        //        case DeviceType.CHD806D2CP:
        //            nRetValue = CHD806D2CP.DReadRecInfo((uint)PortIndex, nNetID, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
        //            break;
        //        case DeviceType.CHD806D2M3B:
        //            break;
        //        case DeviceType.CHD806D4C:
        //            break;
        //        case DeviceType.CHD806D4M3:
        //            break;
        //        case DeviceType.CHD815T_M3:
        //            break;
        //        case DeviceType.CHD825T:
        //            break;
        //        case DeviceType.CHDBank:
        //            break;
        //        case DeviceType.CHDCardReader:
        //            break;
        //        case DeviceType.CHDIOCtrl:
        //            break;
        //        case DeviceType.CHDLH:
        //            break;
        //        case DeviceType.CHDT5:
        //            break;
        //        case DeviceType.CHDTHSendor:
        //            break;
        //        default:
        //            break;
        //    }
        //    switch (nRetValue)
        //    {
        //        case 0x00:		//Success
        //            if (nSaveP >= nLoadP)
        //                nRecordCount = nSaveP - nLoadP;
        //            else
        //                nRecordCount = nMaxLen - (nLoadP - nSaveP);
        //            //PrintMessage("读取成功! 门禁主机当前未读取记录数: " + nRecordCount);
        //            break;
        //        default:		//Failed
        //            //PrintMessage("读取失败! 错误代码: " + nRetValue);
        //            break;
        //    }
        //}
        #endregion

        #region 卡用户管理
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddUser(CHDUserInfo info)
        {
            SYSTEMTIME LmtTime = CHDCommon.ParasTime(info.LmtTime);
            uint nDoor1Privilege = (uint)info.CardPrivilege;
            uint nDoor2Privilege = 0;//(uint)info.CardPrivilege[1];
            int nRetValue = CHD806D2CP.DAddUserEx((uint)portIndex, nNetID,
                info.CardNo,			//卡号
                info.UserNo,			//用户ID
                info.DoorPwd,		//开门密码
                ref LmtTime,				//有效期
                ref nDoor1Privilege, nDoor2Privilege);		//门权限
            switch (nRetValue)
            {
                case 0x00:      //成功
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 增加用户成功！ 卡号:{1} 用户ID:{2} ", deviceIp, info.CardNo, info.UserNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//SM内已满
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 增加用户失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE2:		//SM内已满
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} SM内已满！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE6:		//用户ID号重复
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 用户ID:{1} 重复！", deviceIp, info.UserNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE7:		//卡号重复
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 卡号:" + info.CardNo + "重复！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE8:		//用户信息项全部重复设置
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 用户信息项全部重复设置！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 增加用户失败！ 错误代码： {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }

        /// <summary>
        /// 根据卡号读取用户信息
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>true:成功 false:失败</returns>
        public CHDUserInfo GetUserInfoByCard(string cardNo)
        {
            byte[] szRetCardNo = new byte[11];
            byte[] szUserID = new byte[11];
            byte[] szPasswd = new byte[11];
            int nDoorRight1 = -1, nDoorRight2 = -1;
            SYSTEMTIME LmtTime = CHDCommon.ParasTime(DateTime.Now);
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadUserInfoByCardNo((uint)portIndex, nNetID, cardNo, szRetCardNo, szUserID, szPasswd, out LmtTime, out nDoorRight1, out nDoorRight2);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {
                        CHDUserInfo info = new CHDUserInfo()
                        {
                            CardNo = Encoding.Default.GetString(szRetCardNo),
                            DoorPwd = Encoding.Default.GetString(szPasswd),
                            DoorRight = int.Parse(nDoorRight1.ToString("X2")) + int.Parse(nDoorRight2.ToString("X2")),
                            LmtTime = CHDCommon.ParasTime(LmtTime),
                            UserNo = Encoding.Default.GetString(szUserID)
                        };
                        string msg = string.Format("门禁主机 {0} 读取用户信息成功！ 卡号:{1} 用户ID:{2} 开门密码:{3} 有效期:{4}  第1门权限：{5},第二门权限：{6}",
                           deviceIp,
                           info.CardNo.Replace("\0",""),							//卡号
                           info.UserNo.Replace("\0", ""),						    //用户
                           info.DoorPwd.Replace("\0", ""),							//密码
                           info.LmtTime, //有效期
                           nDoorRight1.ToString("X2"), nDoorRight2.ToString("X2"));
                        CHDUserInfo?.Invoke(info);
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = msg, ReturnCode = nRetValue, Time = DateTime.Now });
                        return info;
                    }
                case 0x07:		//SM内已满
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                case 0xE4:      //无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部相应设置项的存储空间已空", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                case 0xE5:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部无相应信息项", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
            }
        }
        /// <summary>
        /// 根据编号读取用户信息
        /// </summary>
        /// <param name="userNo">卡号</param>
        /// <returns>true:成功 false:失败</returns>
        public CHDUserInfo GetUserInfoByNo(string userNo)
        {
            byte[] szRetCardNo = new byte[11];
            byte[] szUserID = new byte[11];
            byte[] szPasswd = new byte[11];
            int nDoorRight1 = -1, nDoorRight2 = -1;
            SYSTEMTIME LmtTime = CHDCommon.ParasTime(DateTime.Now);
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadUserInfoByUserID((uint)portIndex, nNetID, userNo, szRetCardNo, szUserID, szPasswd, out LmtTime, out nDoorRight1, out nDoorRight2);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {
                        CHDUserInfo info = new CHDUserInfo()
                        {
                            CardNo = Encoding.Default.GetString(szRetCardNo),
                            DoorPwd = Encoding.Default.GetString(szPasswd),
                            DoorRight = int.Parse(nDoorRight1.ToString("X2")) + int.Parse(nDoorRight2.ToString("X2")),
                            LmtTime = CHDCommon.ParasTime(LmtTime),
                            UserNo = Encoding.Default.GetString(szUserID)
                        };
                        string msg = string.Format("门禁主机 {0} 读取用户信息成功！ 卡号:{1} 用户ID:{2} 开门密码:{3} 有效期:{4}  第1门权限：{5},第二门权限：{6}",
                           deviceIp,
                           info.CardNo,							//卡号
                           info.UserNo,						    //用户
                           info.DoorPwd,							//密码
                           info.LmtTime, //有效期
                           nDoorRight1.ToString("X2"), nDoorRight2.ToString("X2"));
                        CHDUserInfo?.Invoke(info);
                        CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = msg, ReturnCode = nRetValue, Time = DateTime.Now });
                        return info;
                    }
                case 0x07:		//SM内已满
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                case 0xE4:      //无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部相应设置项的存储空间已空", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                case 0xE5:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部无相应信息项", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return null;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteUserByCard(string cardNo)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DDelUserByCardNo((uint)portIndex, nNetID, cardNo);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }

            switch (nRetValue)
            {
                case 0x00:		//成功
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 删除卡号:{1} 成功！ ", deviceIp, cardNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE4:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部相应设置项的存储空间已空", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE5:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部无相应信息项", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }

        /// <summary>
        /// 清除用户
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool CleraUsers(string cardNo)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DDelAllUser((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//成功
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 删除门禁主机所有用户成功！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: 无权限！", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE4:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部相应设置项的存储空间已空", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                case 0xE5:		//无相应信息
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误提示: SM内部无相应信息项", deviceIp), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                default:		//其他值表示失败
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取用户信息失败！ 错误代码: {1}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }

        }
        
        #endregion

        #region 设置门禁参数

        /// <summary>
        /// 读取门禁参数设置
        /// </summary>
        /// <param name="doorNo">门编号 1门1 2门2 255全部</param>
        /// <param name="parm">输出门禁参数</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetDoorParam(int doorNo, out CHDDoorParam parm)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            //四字节参数
            int[] ctrParam = new int[4];
            int doorID = doorNo;
            int nRetValue = CHD806D2CP.DReadCtrlParamEx((uint)portIndex, nNetID, doorID,//Read one door
                out ctrParam[0], out int RelayDelay, out int OpenDelay, out int IrSureDelay, out int IrOnDelay, out ctrParam[1], out ctrParam[2], out ctrParam[3]);
            isUseing = false;
            switch (nRetValue)
            {
                case 0x00:
                    CHDByteParameter bytePar=GetByteParameter(ref ctrParam);
                    CHDDoorParam par = new CHDDoorParam()
                    {
                        DeviceIp = deviceIp,
                        DoorNo = doorNo,
                        IrOnDelay = IrOnDelay,
                        IrSureDelay = IrSureDelay,
                        OpenDelay = OpenDelay,
                        RelayDelay = RelayDelay,
                        BytePar = bytePar
                    };
                    parm = par;
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 读取门控制参数成功！", deviceIp + " " + doorNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:
                    parm = null;
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取门控制参数失败！ 错误码: {1}", deviceIp + " " + doorNo, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
           
        }
        /// <summary>
        /// 获取四字节参数对象
        /// </summary>
        /// <param name="ctrParam">四字节参数</param>
        /// <returns>返回四字节参数对象</returns>
        public CHDByteParameter GetByteParameter(ref int[] ctrParam)
        {
            CHDByteParameter bytePar = new CHDByteParameter();
            for (int i = 0; i < ctrParam.Length; i++)
            {
                string param = Convert.ToString(ctrParam[i], 2);
                string pm = param;
                for (int k = 0; k < 8 - param.Length; k++)
                {
                    pm = ("0" + pm);
                }

                for (int j = 0; j < 8; j++)
                {
                    ctrParam[i] = pm.Substring(j, 1) == "1" ? 1 : 0;
                }
                switch (i)
                {
                    case 0:
                        bytePar.Parm1 = new CHDByteParameter1
                        {
                            ByteD7 = byte.Parse(pm.Substring(0, 1) == "1" ? "1" : "0"),
                            ByteD6 = byte.Parse(pm.Substring(1, 1) == "1" ? "1" : "0"),
                            ByteD5 = byte.Parse(pm.Substring(2, 1) == "1" ? "1" : "0"),
                            ByteD4 = byte.Parse(pm.Substring(3, 1) == "1" ? "1" : "0"),
                            ByteD3 = byte.Parse(pm.Substring(4, 1) == "1" ? "1" : "0"),
                            ByteD2 = byte.Parse(pm.Substring(5, 1) == "1" ? "1" : "0"),
                            ByteD1 = byte.Parse(pm.Substring(6, 1) == "1" ? "1" : "0"),
                            ByteD0 = byte.Parse(pm.Substring(7, 1) == "1" ? "1" : "0")
                        };
                        break;
                    case 1:
                        bytePar.Parm2 = new CHDByteParameter2
                        {
                            ByteD7 = byte.Parse(pm.Substring(0, 1) == "1" ? "1" : "0"),
                            ByteD6 = byte.Parse(pm.Substring(1, 1) == "1" ? "1" : "0"),
                            ByteD5 = byte.Parse(pm.Substring(2, 1) == "1" ? "1" : "0"),
                            ByteD4 = byte.Parse(pm.Substring(3, 1) == "1" ? "1" : "0"),
                            ByteD3 = byte.Parse(pm.Substring(4, 1) == "1" ? "1" : "0"),
                            ByteD2 = byte.Parse(pm.Substring(5, 1) == "1" ? "1" : "0"),
                            ByteD1 = byte.Parse(pm.Substring(6, 1) == "1" ? "1" : "0"),
                            ByteD0 = byte.Parse(pm.Substring(7, 1) == "1" ? "1" : "0")
                        };
                        break;
                    case 2:
                        bytePar.Parm3 = new CHDByteParameter3
                        {
                            ByteD7 = byte.Parse(pm.Substring(0, 1) == "1" ? "1" : "0"),
                            ByteD6 = byte.Parse(pm.Substring(1, 1) == "1" ? "1" : "0"),
                            ByteD5 = byte.Parse(pm.Substring(2, 1) == "1" ? "1" : "0"),
                            ByteD4 = byte.Parse(pm.Substring(3, 1) == "1" ? "1" : "0"),
                            ByteD3 = byte.Parse(pm.Substring(4, 1) == "1" ? "1" : "0"),
                            ByteD2 = byte.Parse(pm.Substring(5, 1) == "1" ? "1" : "0"),
                            ByteD1 = byte.Parse(pm.Substring(6, 1) == "1" ? "1" : "0"),
                            ByteD0 = byte.Parse(pm.Substring(7, 1) == "1" ? "1" : "0")
                        };
                        break;
                    case 3:
                        bytePar.Parm4 = new CHDByteParameter4
                        {
                            ByteD7 = byte.Parse(pm.Substring(0, 1) == "1" ? "1" : "0"),
                            ByteD6 = byte.Parse(pm.Substring(1, 1) == "1" ? "1" : "0"),
                            ByteD5 = byte.Parse(pm.Substring(2, 1) == "1" ? "1" : "0"),
                            ByteD4 = byte.Parse(pm.Substring(3, 1) == "1" ? "1" : "0"),
                            ByteD3 = byte.Parse(pm.Substring(4, 1) == "1" ? "1" : "0"),
                            ByteD2 = byte.Parse(pm.Substring(5, 1) == "1" ? "1" : "0"),
                            ByteD1 = byte.Parse(pm.Substring(6, 1) == "1" ? "1" : "0"),
                            ByteD0 = byte.Parse(pm.Substring(7, 1) == "1" ? "1" : "0")
                        };
                        break;
                    default:
                        break;
                }
            }
            return bytePar;
        }
        /// <summary>
        /// 获取四字节参数对象
        /// </summary>
        /// <param name="par">四字节参数对象</param>
        /// <returns>返回四字节参数数组</returns>
        int[] GetByteParameter(CHDByteParameter par)
        {
            int[] parms = new int[4];
            StringBuilder sb1 = new StringBuilder();
            sb1.Append(par.Parm1.ByteD7);
            sb1.Append(par.Parm1.ByteD6);
            sb1.Append(par.Parm1.ByteD5);
            sb1.Append(par.Parm1.ByteD4);
            sb1.Append(par.Parm1.ByteD3);
            sb1.Append(par.Parm1.ByteD2);
            sb1.Append(par.Parm1.ByteD1);
            sb1.Append(par.Parm1.ByteD0);
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(par.Parm2.ByteD7);
            sb2.Append(par.Parm2.ByteD6);
            sb2.Append(par.Parm2.ByteD5);
            sb2.Append(par.Parm2.ByteD4);
            sb2.Append(par.Parm2.ByteD3);
            sb2.Append(par.Parm2.ByteD2);
            sb2.Append(par.Parm2.ByteD1);
            sb2.Append(par.Parm2.ByteD0);
            StringBuilder sb3 = new StringBuilder();
            sb3.Append(par.Parm3.ByteD7);
            sb3.Append(par.Parm3.ByteD6);
            sb3.Append(par.Parm3.ByteD5);
            sb3.Append(par.Parm3.ByteD4);
            sb3.Append(par.Parm3.ByteD3);
            sb3.Append(par.Parm3.ByteD2);
            sb3.Append(par.Parm3.ByteD1);
            sb3.Append(par.Parm3.ByteD0);
            StringBuilder sb4 = new StringBuilder();
            sb4.Append(par.Parm4.ByteD7);
            sb4.Append(par.Parm4.ByteD6);
            sb4.Append(par.Parm4.ByteD5);
            sb4.Append(par.Parm4.ByteD4);
            sb4.Append(par.Parm4.ByteD3);
            sb4.Append(par.Parm4.ByteD2);
            sb4.Append(par.Parm4.ByteD1);
            sb4.Append(par.Parm4.ByteD0);
            int number1 = Convert.ToInt32(sb1.ToString(), 2);
            parms[0] = number1;
            int number2 = Convert.ToInt32(sb2.ToString(), 2);
            parms[1] = number2;
            int number3 = Convert.ToInt32(sb3.ToString(), 2);
            parms[2] = number3;
            int number4 = Convert.ToInt32(sb4.ToString(), 2);
            parms[3] = number4;
            return parms;
        }

        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="parm">门禁参数</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetDoorParam(CHDDoorParam parm)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            uint doorID = (uint)parm.DoorNo;
            int nRetValue = -1;
            int[] par = GetByteParameter(parm.BytePar);
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DSetCtrlParam((uint)portIndex, nNetID, doorID, (uint)(par[0]), uint.Parse(parm.RelayDelay.ToString()), uint.Parse(parm.OpenDelay.ToString()), uint.Parse(parm.IrSureDelay.ToString()), uint.Parse(parm.IrOnDelay.ToString()), (uint)(par[1]), (uint)(par[2]), (uint)(par[3]));
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            isUseing = false;
            switch (nRetValue)
            {
                case 0x00:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置门控制参数成功！", parm.DeviceIp + " " + parm.DoorNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                case 0x07:		//无权限
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 设置门控制参数失败！ 错误提示: 无权限！", parm.DeviceIp + " " + parm.DoorNo), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
                default:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 设置门控制参数失败！ 错误码: {1}", parm.DeviceIp + " " + parm.DoorNo, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
            
        }
        #endregion

        #region 门状态操作
        /// <summary>
        /// 开启常开门
        /// </summary>
        /// <param name="doorId">门编号 1门1 2门2 255全部</param>
        /// <param name="delay">持续时间 分钟</param>
        /// <param name="user">操作员</param>
        /// <returns>true:成功 false:失败</returns>
        public bool OpenAlwaysOpenDoor(uint doorId, uint delay, string user = "")
        {

            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DAlwaysOpenDoor((uint)portIndex, nNetID, doorId, delay, user);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开启常开门成功！", deviceIp + " " + doorId), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 开启常开门失败！ 错误码: {1}", deviceIp + " " + doorId, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 取消常开门
        /// </summary>
        /// <param name="doorId">门编号 1门1 2门2 255全部</param>
        /// <param name="user">操作员</param>
        /// <returns>true:成功 false:失败</returns>
        public bool CloseAlwaysOpenDoor(uint doorId, string user = "")
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DAlwaysOpenDoor((uint)portIndex, nNetID, doorId, 0, user);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 取消常开门成功！", deviceIp + " " + doorId), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 取消常开门失败！ 错误码: {1}", deviceIp + " " + doorId, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 开启常闭门
        /// </summary>
        /// <param name="doorId">门编号 1门1 2门2 255全部</param>
        /// <param name="delay">持续时间 分钟</param>
        /// <param name="user">操作员</param>
        /// <returns>true:成功 false:失败</returns>
        public bool OpenAlwaysCloseDoor(uint doorId, uint delay, string user = "")
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DAlwaysCloseDoor((uint)portIndex, nNetID, doorId, delay, user);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开启常闭门成功！", deviceIp + " " + doorId), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 开启常闭门失败！ 错误码: {1}", deviceIp + " " + doorId, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 取消常闭门
        /// </summary>
        /// <param name="doorId">门编号 1门1 2门2 255全部</param>
        /// <param name="user">操作员</param>
        /// <returns>true:成功 false:失败</returns>
        public bool CloseAlwaysCloseDoor(uint doorId, string user = "")
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DAlwaysCloseDoor((uint)portIndex, nNetID, doorId, 0, user);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 关闭常闭门成功！", deviceIp + " " + doorId), ReturnCode = nRetValue, Time = DateTime.Now });
                    return true;
                default:
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 关闭常闭门失败！ 错误码: {1}", deviceIp + " " + doorId, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="doorId">门编号 1门1 2门2 255全部 </param>
        /// <param name="delay">持续时间 分钟</param>
        /// <param name="user">操作员</param>
        /// <returns>true:成功 false:失败</returns>
        public bool OpenDoor(uint doorId, uint delay, string user = "")
        {
            int nRerValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    if (string.IsNullOrWhiteSpace(user))
                        nRerValue = CHD806D2CP.DRemoteOpenDoor((uint)portIndex, nNetID, doorId);
                    else
                        nRerValue = CHD806D2CP.DRemoteOpenDoorWithUser((uint)portIndex, nNetID, doorId, user);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            if (nRerValue == 0x00)
            {
                if (!string.IsNullOrWhiteSpace(user))
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 远程开 {1} 门成功！ 操作员:{2}", deviceIp, doorId, user), ReturnCode = nRerValue, Time = DateTime.Now });
                else
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 远程开 {1} 门成功！", deviceIp, doorId), ReturnCode = nRerValue, Time = DateTime.Now });
                return true;
            }
            else if (nRerValue == 0x07)
            {
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 远程开 {1} 门失败！ 错误提示: 无权限！", deviceIp, doorId), ReturnCode = nRerValue, Time = DateTime.Now });
            }
            else
            {
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 远程开 {1} 门失败！ 错误码: {2}", deviceIp, doorId, nRerValue), ReturnCode = nRerValue, Time = DateTime.Now });
            }
            return false;
        }

        #endregion

        #region 报警
        /// <summary>
        /// 解除胁迫报警
        /// </summary>
        /// <returns>true:成功 false:失败</returns>
        public bool CloseMenace()
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DMenaceClose((uint)portIndex, nNetID);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            bool b = false;
            string str = "";
            switch (nRetValue)
            {
                case 0x00:
                    b = true;
                    str = string.Format("门禁主机 {0} 关闭胁迫报警成功！", deviceIp);
                    break;
                default:
                    str = string.Format("门禁主机 {0} 关闭胁迫报警失败！ 错误码: {1}", deviceIp, nRetValue);
                    break;
            }
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            return b;
        }

        /// <summary>
        /// 开启报警继电器
        /// </summary>
        ///<param name="alarmNo">=1驱动第1报警继电器 =2	驱动第2报警继电器 =0FFH 	驱动第1和第2报警继电器 =0 	不操作</param>
        /// <returns>true:成功 false:失败</returns>
        public bool OpenDeviceAlarm(uint alarmNo)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DOpenAlarm((uint)portIndex, nNetID, alarmNo);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            bool b = false;
            string str = "";
            switch (nRetValue)
            {
                case 0x00:
                    b = true;
                    str = string.Format("门禁主机 {0} 开启 {1} 号报警继电器成功！", deviceIp, alarmNo);
                    break;
                default:
                    str = string.Format("门禁主机 {0} 开启 {1} 号报警继电器失败！ 错误码: {2}", deviceIp, alarmNo, nRetValue);
                    break;
            }
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            return b;
        }

        /// <summary>
        /// 关闭报警继电器
        /// </summary>
        ///<param name="alarmNo">=1驱动第1报警继电器 =2	驱动第2报警继电器 =0FFH 	驱动第1和第2报警继电器 =0 	不操作</param>
        /// <returns>true:成功 false:失败</returns>
        public bool CloseDeviceAlarm(uint alarmNo)
        {
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DCloseAlarm((uint)portIndex, nNetID, alarmNo);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            bool b = false;
            string str = "";
            switch (nRetValue)
            {
                case 0x00:
                    b = true;
                    str = string.Format("门禁主机 {0} 关闭继电器 {1} 报警成功！", deviceIp, alarmNo);
                    break;
                default:
                    str = string.Format("门禁主机 {0} 关闭继电器 {0} 报警失败！ 错误码: {2}", deviceIp, alarmNo, nRetValue);
                    break;
            }
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            return b;
        }

        #endregion

        #region 时间段
        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="times">返回时段列表</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetListTime(ref List<DayTimeSplit>  times)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开始读取时间段列表···", deviceIp), ReturnCode = 0, Time = DateTime.Now });
            StringBuilder szTimeSlot = new StringBuilder();
            String strTemp;
            bool b = false;
            string str = "";
            for (int i = 0; i < 32; ++i)
            {
                b = false;
                DayTimeSplit ts = new DayTimeSplit();
                int nRetValue = CHD806D2CP.DReadListTime((uint)portIndex, nNetID, (uint)i, szTimeSlot);
                if (nRetValue == 0x00)
                {
                    strTemp = szTimeSlot.ToString();
                    
                    //for (int j = 1; j < 9; ++j)
                    //{
                    //    DayTimeSplit tsp = new DayTimeSplit();
                    //    strItemText = strTemp.Substring(j * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                    //    strItemText += ":";
                    //    strItemText += strTemp.Substring(j * 4 - 4 + 2, 2);
                    //    //tsp.Times.Add(strItemText);
                    //    ts.Split.Add(tsp);
                    //}
                    ts.StartTimes1= strTemp.Substring(1 * 4 - 4, 2)+":"+ strTemp.Substring(1 * 4 - 4+2, 2);
                    ts.EndTimes1 = strTemp.Substring(2 * 4 - 4, 2) + ":" + strTemp.Substring(2 * 4 - 4 + 2, 2);
                    ts.StartTimes2 = strTemp.Substring(3 * 4 - 4, 2) + ":" + strTemp.Substring(3 * 4 - 4 + 2, 2);
                    ts.EndTimes2 = strTemp.Substring(4 * 4 - 4, 2) + ":" + strTemp.Substring(4 * 4 - 4 + 2, 2);
                    ts.StartTimes3 = strTemp.Substring(5 * 4 - 4, 2) + ":" + strTemp.Substring(5 * 4 - 4 + 2, 2);
                    ts.EndTimes3 = strTemp.Substring(6 * 4 - 4, 2) + ":" + strTemp.Substring(6 * 4 - 4 + 2, 2);
                    ts.StartTimes4 = strTemp.Substring(7 * 4 - 4, 2) + ":" + strTemp.Substring(7 * 4 - 4 + 2, 2);
                    ts.EndTimes4 = strTemp.Substring(8 * 4 - 4, 2) + ":" + strTemp.Substring(8 * 4 - 4 + 2, 2);
                   
                    b = true;
                    str = string.Format("门禁主机 {0} 读取第{1}张表成功:[ {2} ]", deviceIp, i, strTemp);
                }
                else
                {
                    str = string.Format("门禁主机 {0} 读取第{1}张表失败！ 错误码: {2}", deviceIp, i, nRetValue);
                }
                ts.Index = i;
                times[i]=ts;
                CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
                Thread.Sleep(20);
            }
            isUseing = false;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 时间段读取完毕！", deviceIp), ReturnCode = 0, Time = DateTime.Now });
            return b;
        }

        /// <summary>
        /// 设置时段
        /// </summary>
        ///<param name="times">时间段列表 31组</param>
        public bool SetListTime(List<DayTimeSplit> times)
        {
            while (isUseing)
            {
                Thread.Sleep(10);
            }
            isUseing = true;
            bool b = false;
            string str = "";
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开始设置时间段列表···", deviceIp), ReturnCode = 0, Time = DateTime.Now });
            try
            {
                for (int i = 0; i < times.Count; ++i)//31
                {
                    b = false;
                    String strText = string.Empty;
                    //for (int j = 1; j < 9; ++j)
                    //{
                    strText += times[i].StartTimes1.ToString();
                    strText += times[i].EndTimes1.ToString();
                    strText += times[i].StartTimes2.ToString();
                    strText += times[i].EndTimes2.ToString();
                    strText += times[i].StartTimes3.ToString();
                    strText += times[i].EndTimes3.ToString();
                    strText += times[i].StartTimes4.ToString();
                    strText += times[i].EndTimes4.ToString();
                    //}
                    strText = strText.Replace(":", "");
                    int nRetValue = CHD806D4M3.DSetListTime((uint)portIndex, nNetID, (uint)i, new StringBuilder(strText));
                    if (nRetValue == 0)
                    {
                        b = true;
                        str = string.Format("门禁主机 {0} 设置第{1}张表: {2} 成功！", deviceIp, i, strText);
                    }
                    else if (nRetValue == 0x07)
                    {
                        str = string.Format("门禁主机 {0} 设置第{1}张表: {2} 失败！ 错误码: 无权限", deviceIp, i, strText);
                    }
                    else
                    {
                        str = string.Format("门禁主机 {0} 设置第{1}张表: {2} 失败！ 错误码: {3}", deviceIp, i, strText, nRetValue);
                    }
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
                    Thread.Sleep(10);
                }
            }
            catch (Exception) { }
            isUseing = false;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 设置时间段完毕！", deviceIp), ReturnCode = 0, Time = DateTime.Now });
            return b;
        }
        /// <summary>
        /// 获取周计划时间列表
        /// </summary>
        /// <param name="weekPlan">周计划对象</param>
        /// <returns>返回周计划时间列表</returns>
        public bool GetWeePlan(ref WeekPlan weekPlan)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            byte[] Time = new byte[64];
            //List<TimeList> times = new List<TimeList>();
            bool b = false;
            string str = "";
            uint doorId = (uint)(weekPlan.DoorId > 2 ? 255 : weekPlan.DoorId);
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开始获取周计划列表···", deviceIp + " " + doorId), ReturnCode = 0, Time = DateTime.Now });
            try
            {
                for (int nWeek = 1; nWeek <= 7; ++nWeek)
                {
                    b = false;
                    int nRetValue = CHD806D2CP.DReadWeekTime((uint)portIndex, nNetID, doorId/*转换后的门号*/, (uint)nWeek/*星期*/, Time);
                    if (nRetValue == 0)
                    {
                        weekPlan.WPlan[nWeek - 1].CardType1TimeIndex = Time[0];
                        weekPlan.WPlan[nWeek - 1].CardType2TimeIndex = Time[1];
                        weekPlan.WPlan[nWeek - 1].CardType3TimeIndex = Time[2];
                        weekPlan.WPlan[nWeek - 1].CardType4TimeIndex = Time[3];
                        weekPlan.WPlan[nWeek - 1].TodayOpenTimeIndex = Time[4];
                        weekPlan.WPlan[nWeek - 1].CardAndPWDTimeIndex = Time[5];
                        weekPlan.WPlan[nWeek - 1].AutoAalmingTimeIndex = Time[6];
                        weekPlan.WPlan[nWeek - 1].N1TimeIndex = Time[7];
                        b = true;
                        str = string.Format("门禁主机 {10} 读取 {0} 号门 星期{1} 的时段:[ {2} {3} {4} {5} {6} {7} {8} {9} ]", doorId, nWeek,
                            Time[0], Time[1], Time[2], Time[3],
                            Time[4], Time[5], Time[6], Time[8], deviceIp);
                        Thread.Sleep(5);
                    }
                    else
                    {
                        b = false;
                        str = string.Format("门禁主机 {3} 读取 {0} 号门 星期{1} 的时段失败！错误码: {2}", doorId, nWeek, nRetValue, deviceIp);
                    }
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = 0, Time = DateTime.Now });
                    Thread.Sleep(20);
                }
            }
            catch (Exception) { }
            isUseing = false;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 周计划读取完毕！", deviceIp), ReturnCode = 0, Time = DateTime.Now });
            return b;
        }
        /// <summary>
        /// 设置周计划
        /// </summary>
        /// <param name="wPlan">周计划</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetWeekPlan(WeekPlan wPlan)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;

            int doorId = wPlan.DoorId > 2 ? 255 : wPlan.DoorId;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 开始设置周计划···", deviceIp + " " + doorId), ReturnCode = 0, Time = DateTime.Now });
            string strTmp = "";
            string strTimeList = "";
            bool b = false;
            string str = "";
            int nRetValue = -1;
            try
            {
                for (int i = 0; i < 7; ++i)
                {
                    //strTmp  = wPlan.WPlan[i].DayNum.ToString();
                    //strTmp = wPlan.WPlan[i].CardType1TimeIndex.ToString();
                    //strTmp = wPlan.WPlan[i].CardType2TimeIndex.ToString();
                    //strTmp = wPlan.WPlan[i].CardType3TimeIndex.ToString();
                    //strTmp += wPlan.WPlan[i].CardType4TimeIndex.ToString();
                    //strTmp += wPlan.WPlan[i].TodayOpenTimeIndex.ToString();
                    //strTmp += wPlan.WPlan[i].CardAndPWDTimeIndex.ToString();
                    //strTmp += wPlan.WPlan[i].AutoAalmingTimeIndex.ToString();
                    //strTmp += wPlan.WPlan[i].N1TimeIndex.ToString();
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].CardType1TimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].CardType2TimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].CardType3TimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].CardType4TimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].TodayOpenTimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].CardAndPWDTimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].AutoAalmingTimeIndex.ToString()).ToString("X2"));
                    strTimeList += (Convert.ToInt16(wPlan.WPlan[i].N1TimeIndex.ToString()).ToString("X2"));
                }
                byte[] cInfo = Encoding.Default.GetBytes(strTimeList);
                nRetValue = CHD806D2CP.DSetWeekTime((uint)portIndex, nNetID, (uint)doorId/*转换后的门号*/, cInfo);
                if (nRetValue == 0)
                {
                    b = true;
                    str = string.Format("门禁主机 {0} 周计划设置成功！\n\n [ " + strTimeList + "]", deviceIp + " " + doorId);
                }
                else if (nRetValue == 0x07)
                {
                    str = string.Format("门禁主机 {0} 周计划设置失败！ 无权限", deviceIp + " " + doorId);
                }
                else
                    str = string.Format("门禁主机 {0}周计划设置失败！ 错误码: {1}", deviceIp + " " + doorId, nRetValue);
            }
            catch (Exception) { }
            isUseing = false;
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            return b;
        }
        /// <summary>
        /// 设置假日计划
        /// </summary>
        /// <param name="hList">假日计划参数</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetHolidayListTime(HolidayList hList)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            bool b = false;
            //string str = "";
            //int nLmtMonth = 5;
            //int nLmtDay = 1;
            //String strTimeList = "";
            //String strTmp = "";
            //int nMax = hList.DayCount;
            //uint doorId = hList.DoorId;
            //if (nMax == 0) return false;
            //for (int i = 0; i < nMax; ++i)
            //{
            //    strTimeList = "";
            //    for (int j = 0; j < 8; ++j)
            //    {
            //        strTmp = hList.HPlan[i].Split[j].ToString();
            //        //strTmp.Format(_T("%02x"), _ttoi(strTmp)); //转换成16进制
            //        strTimeList += (Convert.ToInt16(strTmp).ToString("X2"));
            //    }
            //    nLmtMonth = hList.LmtMonth;
            //    nLmtMonth = (0 < nLmtMonth && nLmtMonth < 13) ? nLmtMonth : 5;

            //    nLmtDay = hList.LmtDay + i;
            //    nLmtDay = (0 < nLmtDay && nLmtDay < 32) ? nLmtDay : 1;

            //    byte[] cInfo = Encoding.Default.GetBytes(strTimeList);
            //    int nRetValue = CHD806D2CP.DSetHolidayTime((uint)PortIndex, nNetID, doorId/*转换后的门号*/,
            //        nLmtMonth/*月*/, nLmtDay/*日*/, cInfo);
            //    if (nRetValue == 0)
            //    {
            //        b = true;
            //        str = string.Format("门禁主机 {0} 设置节假日: {1} 月 {2} 日时段成功！ [ {3} ]", deviceIp, nLmtMonth, nLmtDay, strTimeList);
            //    }
            //    else if (nRetValue == 0x07)
            //    {
            //        str = string.Format("门禁主机 {0} 二门星期时间段列表设置失败！ 无权限", deviceIp);
            //    }
            //    else
            //        str = string.Format("门禁主机 {0} 设置节假日: {1} 月 {2} 日时段失败！ 错误码: {3}", deviceIp, nLmtMonth, nLmtDay, nRetValue);
            //    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = b, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            //}
            //str = string.Format("门禁主机 {0} 设置二门节、假日时段完毕！", deviceIp);
            //CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = str, ReturnCode = 0, Time = DateTime.Now });
            isUseing = false;
            return b;
        }



        //private void GetHolidayListTime()
        //{

        //    int nRetValue = 0, nLmtMonth = 0, nLmtDay = 0;
        //    byte[] Time = new byte[64];
        //    uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);


        //    PrintMessage("开始读取节、假日时间段列表");
        //    m_HolidayList.Rows.Clear();
        //    uint i = 0;
        //    do
        //    {
        //        nRetValue = CHD.API.CHD806D2CP.DReadHolidayTime(PortId, NetId, doorId/*转换后的门号*/,
        //            ++i, out nLmtMonth, out nLmtDay, Time);
        //        if (nRetValue == 0)
        //        {
        //            m_HolidayList.Rows.Add(new object[] { nLmtMonth, nLmtDay, Time[0], Time[1], Time[2], Time[3], Time[4], Time[5], Time[6], Time[7] });
        //            PrintMessage(string.Format(" 读取 {0}门 节假日:序号{1} {2} 月 {3} 日[ {4} {5} {6} {7} {8} {9} {10} {11} ]时段成功！", doorId, i, nLmtMonth, nLmtDay,
        //               Time[0], Time[1], Time[2], Time[3], Time[4], Time[5], Time[6], Time[7]));
        //        }
        //        else if (nRetValue == 0xE5)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            PrintMessage(string.Format("读取 {0}门 节假日:序号{1} {2} 月 {3} 日 时段失败！ 错误码: {4}", doorId, i - 1, nLmtMonth, nLmtDay, nRetValue));
        //        }
        //    } while (nRetValue == 0x00);
        //    PrintMessage("节、假日时间段列表读取完毕！");
        //}

        #endregion

        #region  门禁主机参数
        /// <summary>
        /// 同步时间
        /// </summary>
        public bool SetDeviceTime()
        {
            bool b = false;
            string str = "";
            SYSTEMTIME stime = CHDCommon.ParasTime(DateTime.Now);
            int nRetValue = CHD806D2CP.DSetDateTime((uint)portIndex, nNetID, ref stime);
            switch (nRetValue)
            {
                case 0x00:
                    b = true;
                    str= String.Format("门禁主机 {0} 同步时间成功：{1} 星期{2}",deviceIp, CHDCommon.ParasTime(stime).ToString(), stime.wDayOfWeek);
                    break;
                default:
                    str=string.Format("门禁主机 {0} 同步时间失败！ 错误码: {1}", deviceIp, nRetValue);
                    break;
            }
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = str, ReturnCode = nRetValue, Time = DateTime.Now });
            return b;
        }
        #endregion

        #region 发卡器
        /// <summary>
        /// 读取卡号
        /// </summary>
        /// <returns>返回卡号</returns>
        public string GetCardNum()
        {
            byte[] str = new byte[16];
            string msg="";
            int nRetValue = CHDCardReader.CRReadCardNo((uint)portIndex,  str);
            CHDCardReader.CRClearBuffer((uint)portIndex);
            switch (nRetValue)
            {
                case 0x00:
                    msg = string.Format("门禁主机 {0} 读取卡号成功：{1} ", deviceIp, Encoding.UTF8.GetString(str).Trim('\0'));
                    break;
                default:
                    msg = string.Format("门禁主机 {0} 读取卡号失败！ 错误码: {1}", deviceIp, nRetValue);
                    break;
            }
            CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = msg, ReturnCode = nRetValue, Time = DateTime.Now });
            return Encoding.UTF8.GetString(str).Trim('\0');
        }
        #endregion

        #region 工作状态
        /// <summary>
        /// 获取工作状态
        /// </summary>
        /// <param name="state">返回状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool GetWorkState(ref CHDDeviceState state)
        {
            while (isUseing)
            {
                Thread.Sleep(20);
            }
            isUseing = true;
            int pnWorkState1=0, pnLineState1=0;
            int pnWorkState2=0, pnLineState2=0;
            int nRetValue = -1;
            switch (deviceType)
            {
                case DeviceType.CHD200G:
                    break;
                case DeviceType.CHD200H:
                    break;
                case DeviceType.CHD601D_M3:
                    break;
                case DeviceType.CHD603S:
                    break;
                case DeviceType.CHD689:
                    break;
                case DeviceType.CHD805:
                    break;
                case DeviceType.CHD806D2CP:
                    nRetValue = CHD806D2CP.DReadDoorState((uint)portIndex, nNetID, out pnWorkState1, out pnLineState1, out pnWorkState2, out pnLineState2);
                    break;
                case DeviceType.CHD806D2M3B:
                    break;
                case DeviceType.CHD806D4C:
                    break;
                case DeviceType.CHD806D4M3:
                    break;
                case DeviceType.CHD815T_M3:
                    break;
                case DeviceType.CHD825T:
                    break;
                case DeviceType.CHDBank:
                    break;
                case DeviceType.CHDCardReader:
                    break;
                case DeviceType.CHDIOCtrl:
                    break;
                case DeviceType.CHDLH:
                    break;
                case DeviceType.CHDT5:
                    break;
                case DeviceType.CHDTHSendor:
                    break;
                default:
                    break;
            }
            isUseing = false;
            string str = GetWorkFormtStr(pnWorkState1, pnLineState1,pnWorkState2, pnLineState2,ref state);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = true, Msg = string.Format("门禁主机 {0} 获取工作状态成功！{1}", deviceIp,str), ReturnCode = nRetValue, Time = DateTime.Now });
                   
                    return true;
                default:		//Failed
                    CHDLogMsg?.Invoke(new CHDMsg() { DeviceIp = deviceIp, IsSuccessful = false, Msg = string.Format("门禁主机 {0} 读取工作状态失败··· 错误代码: {0}", deviceIp, nRetValue), ReturnCode = nRetValue, Time = DateTime.Now });
                    return false;
            }
        }
        /// <summary>
        /// 工作状态格式化
        /// </summary>
        /// <param name="pnWorkState1">门1工作状态</param>
        /// <param name="pnLineState1">门1线路状态</param>
        /// <param name="pnWorkState2">门2工作状态</param>
        /// <param name="pnLineState2">门2线路状态</param>
        /// <param name="state">状态对象</param>
        /// <returns>返回字符串</returns>
        private string GetWorkFormtStr(int pnWorkState1, int pnLineState1, int pnWorkState2, int pnLineState2,ref CHDDeviceState state)
        {
            state.LineStates = new CHDDoorLineState[2] { new CHDDoorLineState(), new CHDDoorLineState() };
            state.WorkStates = new CHDDoorWorkState[2] { new CHDDoorWorkState(), new CHDDoorWorkState() };
            string str = "";
            string paramWork1 = Convert.ToString(pnWorkState1, 2);
            string pmWork1 = paramWork1;
            GetDoorWork(ref str, paramWork1,1, ref pmWork1,ref state.WorkStates[0]);
            string paramWork2 = Convert.ToString(pnLineState1, 2);
            string pmLine2 = paramWork2;
            GetDoorLineState(ref str, paramWork2,1, ref pmLine2, ref state.LineStates[0]);
            string paramWork3 = Convert.ToString(pnWorkState2, 2);
            string pmWork3 = paramWork3;
            str += Environment.NewLine;
            GetDoorWork(ref str, paramWork3, 2,ref pmWork3, ref state.WorkStates[1]);
            string paramWork4 = Convert.ToString(pnLineState2, 2);
            string pmLine4 = paramWork4;
            GetDoorLineState(ref str, paramWork4,2, ref pmLine4, ref state.LineStates[1]);
            return str;
        }
        /// <summary>
        /// 线路状态
        /// </summary>
        /// <param name="str"></param>
        /// <param name="paramWork"></param>
        /// <param name="doorId"></param>
        /// <param name="pm"></param>
        /// <param name="lineStates"></param>
        private void GetDoorLineState(ref string str, string paramWork, int doorId, ref string pm, ref CHDDoorLineState lineStates)
        {
            for (int k = 0; k < 8 - paramWork.Length; k++)
            {
                pm = ("0" + pm);
            }
            str += Environment.NewLine;
            str += "  门"+ doorId + "线路状态：";
            lineStates.ByteD7 = byte.Parse(pm.Substring(0, 1));
            if (pm.Substring(0, 1) == "0")
                str += "  紧急驱动正常;";
            else
                str += "  紧急驱动输入;";
            lineStates.ByteD6 = byte.Parse(pm.Substring(1, 1));
            if (pm.Substring(1, 1) == "0")
                str += "  门控正常;";
            else
                str += "  常闭门;";
            lineStates.ByteD5 = byte.Parse(pm.Substring(2, 1));
            if (pm.Substring(2, 1) == "0")
                str += "  门控正常;";
            else
                str += "  常开门;";
            lineStates.ByteD4 = byte.Parse(pm.Substring(3, 1));
            if (pm.Substring(3, 1) == "0")
                str += "  ";
            else
                str += "  胁迫;";
            lineStates.ByteD3 = byte.Parse(pm.Substring(4, 1));
            if (pm.Substring(4, 1) == "0")
                str += "  门闭合;";
            else
                str += "  门开的;";
            lineStates.ByteD2 = byte.Parse(pm.Substring(5, 1));
            if (pm.Substring(5, 1) == "0")
                str += "  窃入红外正常;";
            else
                str += "  窃入红外报警;";
            lineStates.ByteD1 = byte.Parse(pm.Substring(6, 1));
            if (pm.Substring(6, 1) == "0")
                str += "  出门放行键松开;";
            else
                str += "  出门放行键按下;";
            lineStates.ByteD0 = byte.Parse(pm.Substring(7, 1));
            if (pm.Substring(7, 1) == "0")
                str += "  ";
            else
                str += "  ";
        }
        /// <summary>
        /// 工作状态
        /// </summary>
        /// <param name="str"></param>
        /// <param name="param"></param>
        /// <param name="doorId"></param>
        /// <param name="pm"></param>
        /// <param name="state"></param>
        private void GetDoorWork(ref string str, string param,int doorId, ref string pm,ref CHDDoorWorkState state)
        {
            for (int k = 0; k < 8 - param.Length; k++)
            {
                pm = ("0" + pm);
            }
            str += Environment.NewLine;
            str += "  门"+ doorId + "工作状态：";
            state.ByteD7 = byte.Parse(pm.Substring(0, 1));
            if (pm.Substring(0, 1) == "0")
                str += "  实时钟IC正常;";
            else
                str += "  不正常需要重新设置时间;";
            state.ByteD6 = byte.Parse(pm.Substring(1, 1));
            if (pm.Substring(1, 1) == "0")
                str += "  正常，无事件请求;";
            else
                str += "  DCU有事件要SU处理;";
            state.ByteD5 = byte.Parse(pm.Substring(2, 1));
            if (pm.Substring(2, 1) == "0")
                str += "  工作电源正常;";
            else
                str += "  不正常,电压低而CPU被平凡复位;";
            state.ByteD4 = byte.Parse(pm.Substring(3, 1));
            if (pm.Substring(3, 1) == "0")
                str += "  保留（防拆开关）;";
            else
                str += "  保留（防拆开关）;";
            state.ByteD3 = byte.Parse(pm.Substring(4, 1));
            if (pm.Substring(4, 1) == "0")
                str += "  不监视红外入侵;";
            else
                str += "  监视红外入侵;";
            state.ByteD2 = byte.Parse(pm.Substring(5, 1));
            if (pm.Substring(5, 1) == "0")
                str += "  不监视门开关状态;";
            else
                str += "  监视门开关状态;";
            state.ByteD1 = byte.Parse(pm.Substring(6, 1));
            if (pm.Substring(6, 1) == "0")
                str += "  门控电磁继电器关闭;";
            else
                str += "  门控电磁继电器加电驱动;";
            state.ByteD0 = byte.Parse(pm.Substring(7, 1));
            if (pm.Substring(7, 1) == "0")
                str += "  正常工作;";
            else
                str += "  处于报警状态;";
        }
        
        #endregion
    }
}
