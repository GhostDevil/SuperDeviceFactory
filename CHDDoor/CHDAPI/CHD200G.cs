using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    /// <summary>
    /// HCD200G指纹机API
    /// </summary>
    public static class CHD200G
    {



        /***************************************************************
         * 1．	使用OpenCom/OpenTcp打开对应序号的端口；(当发现连接失效的时:调用ReConectPort重新连接)
           2．	执行LinkOn校验设备密码，确认设备通讯权限；
           3．	执行相应的指令进行与设备通讯的一系列操作，特别声明如果在4分钟内没与设备进行通讯的操作，设备通讯权限自动取消；
           4．	执行LinkOff取消设备通讯权限；
           5．	当程序关闭时使用ClosePort关闭端口。
         **************************************************************/
          





        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrLinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrLinkOn(uint nPortIndex, uint nNetID, String szDevPwd);




        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrLinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrLinkOff(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szDevPwd">设备系统密码 10个('0'..'9')ASCII字符串</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetDevPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetDevPwd(uint nPortIndex, uint nNetID, StringBuilder szDevPwd);




        /// <summary>
        /// 更改网络ID
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nNewGrp">组号(0-15)</param>
        /// <param name="nNewID">设备ID(1-254)</param>
        /// 备注nNetID = nID + Grp*256;
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrNewNetAddr1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrNewNetAddr1(uint nPortIndex, uint nNetID, uint nNewGrp, uint nNewID);




        /// <summary>
        /// 更改波特率
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nBaudrate">波特率代码，=0 9600，=1 19200，=2 38400， =3 115200</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetBaudrate(uint nPortIndex, uint nNetID, uint nBaudrate);




        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回设备版本</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrReadVersion(uint nPortIndex, uint nNetID, byte[] szVersion);




        /// <summary>
        /// 删除指纹
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFingerIndex">指纹编号必须大于9
        ///例：指纹编号12
        ///其中十位数表示个人ID，个位数表示这个人的指纹编号；
        ///删除这个人的指纹编号只需要删除这个人的10这个指纹编号即可，硬件会自动删除与这个人有关的11、12指纹编号
        ///</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrDelOneFinger", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrDelOneFinger(uint nPortIndex, uint nNetID, uint nFingerIndex);




        /// <summary>
        /// 设置指纹头管理员
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nAdminType1">第一管理员类型=0超级管理员，其它一般管理员</param>
        /// <param name="szAdminID1">第一管理员ID(长度为:8的ASCII码字符串)</param>
        /// <param name="nAdminType2">第二管理员类型=0超级管理员，其它一般管理员</param>
        /// <param name="szAdminID2">第二管理员ID(长度为:8的ASCII码字符串)</param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetAdmin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetAdmin(uint nPortIndex, uint nNetID, uint  nAdminType1, string szAdminID1, uint  nAdminType2, string szAdminID2);



        /// <summary>
        /// 读取指纹头管理员
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="pnAdminType1">第一管理员类型=0超级管理员，其它一般管理员</param>
        /// <param name="szAdminID1">第一管理员ID(长度为:8的ASCII码字符串)</param>
        /// <param name="pnAdminType2">第二管理员类型=0超级管理员，其它一般管理员</param>
        /// <param name="szAdminID2">第二管理员ID(长度为:8的ASCII码字符串</param>
        /// <returns></returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrReadAdmin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrReadAdmin(uint nPortIndex, uint nNetID, out int pnAdminType1, StringBuilder szAdminID1,out int pnAdminType2, StringBuilder szAdminID2);




        /// <summary>
        /// 删除所有指纹
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrDelAllFinger", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrDelAllFinger(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">用户ID号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szCardNo">用户卡号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserName">用户名(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserNo">用户编号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserPwd">用户密码(长度为4的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="nFingerIndex1">
        ///指纹编号必须大于9
        ///例：指纹编号是10
        ///则表示这个人的ID是1，指纹编号为0
        ///那么可知，这个人可有10个指纹编号：10～19
        ///第二指纹编号、第三指纹编号可以为10～19其中的数据，不能重复
        /// </param>
        /// <param name="nFingerIndex2"></param>
        /// <param name="nFingerIndex3"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrAddOneUserEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrAddOneUserEx(uint nPortIndex, uint nNetID, string szUserID, string szCardNo, string szUserName, string szUserNo, string szUserPwd, uint nFingerIndex1, uint nFingerIndex2, uint nFingerIndex3);




        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">用户ID号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szCardNo">用户卡号(长度为10的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserName">用户名(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserNo">用户编号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="szUserPwd">用户密码(长度为4的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <param name="nFingerIndex1">
        ///指纹编号必须大于9
        ///例：指纹编号是10
        ///则表示这个人的ID是1，指纹编号为0
        ///那么可知，这个人可有10个指纹编号：10～19
        ///第二指纹编号、第三指纹编号可以为10～19其中的数据，不能重复
        /// </param>
        /// <param name="nFingerIndex2"></param>
        /// <param name="nFingerIndex3"></param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrAddOneUserEx1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrAddOneUserEx1(uint nPortIndex, uint nNetID, StringBuilder szUserID, StringBuilder szCardNo, StringBuilder szUserName, StringBuilder szUserNo, StringBuilder szUserPwd, int nFingerIndex1, int nFingerIndex2, int nFingerIndex3);




        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">需要读取用户ID,长度为8的ASCII码字符串</param>
        /// <param name="szCardNo">返回的卡号,长度为4的ASCII码字符串</param>
        /// <param name="szUserPwd">返回的用户密码,长度为4的ASCII码字符串,"FFFF"表示无效/没有密码</param>
        /// <param name="pnFingerIndex1">返回第一指纹编号</param>
        /// <param name="pnFingerIndex2">返回第二指纹编号</param>
        /// <param name="pnFingerIndex3">返回第三指纹编号</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrReadUserInfoByUserIDEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrReadUserInfoByUserIDEx(uint nPortIndex, uint nNetID, string szUserID, StringBuilder szCardNo, StringBuilder szUserPwd, out uint pnFingerIndex1, out uint pnFingerIndex2, out uint pnFingerIndex3);




        /// <summary>
        /// 删除指定用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szUserID">用户ID号(长度为8的ASCII码字符串('0'-'9', 'A'-'F'))</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrDelOneUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrDelOneUser(uint nPortIndex, uint nNetID, string szUserID);




        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnUserCnt">返回的用户数量</param>
        /// <param name="pnFingerCnt">返回的指纹数量</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrReadUserNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrReadUserNum(uint nPortIndex, uint nNetID, out uint pnUserCnt, out uint pnFingerCnt);




        /// <summary>
        /// 删除所有用户
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrDelAllUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrDelAllUser(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 设置控制参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nCtrlParam">
        /// 控制参数字节
        ///D1,D0	维根输出格式
        ///		=0 28BIT
        ///		=1 26BIT
        ///		=2 44BIT
        ///D3,D2	工作模式
        ///		=0 独立工作
        ///		=1 刷卡加指纹
        ///		=2 ID加指纹
        ///D4		编号转换方法
        ///		=0 十六进制
        ///		=1 BCD码
        ///D5		安装位置
        ///		=0 进
        ///		=1 出
        ///D7		安装位置/ 语言类型(见备注)
        ///		=0 4BIT
        ///		=1 8BIT
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetCtrl1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetCtrl1(uint nPortIndex, uint nNetID, uint nCtrlParam);




        /// <summary>
        /// 读取控制参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnCtrlParam">
        /// 返回控制参数
        ///控制参数字节
        ///D1,D0	维根输出格式
        ///		=0 28BIT
        ///		=1 26BIT
        ///		=2 44BIT
        ///D3,D2	工作模式
        ///		=0 独立工作
        ///		=1 刷卡加指纹
        ///		=2 ID加指纹
        ///D4		编号转换方法
        ///		=0 十六进制
        ///		=1 BCD码
        ///D5		安装位置
        ///		=0 进
        ///		=1 出
        ///D7		安装位置/ 语言类型(见备注)
        ///		=0 4BIT
        ///		=1 8BIT
        /// </param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrGetCtrl1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrGetCtrl1(uint nPortIndex, uint nNetID, ref uint pnCtrlParam);




        /// <summary>
        /// 启动注册指纹
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="iSize">注册指纹块大小</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrRegFingerStart1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrRegFingerStart1(uint nPortIndex, uint nNetID, int iSize);


        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrRegFingerStart", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrRegFingerStart(uint nPortIndex, uint nNetID);


         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrRefreshData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrRefreshData(uint nPortIndex, uint nNetID);


         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD200H_FrGetCtrl1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
         public static extern int CHD200H_FrGetCtrl1(uint nPortIndex, uint nNetID, out uint pnCtrlParam);
       

        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrAddOneUser", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
         public static extern int FrAddOneUser(uint nPortIndex, uint nNetID, String szUserID, String szUserName, String szUserNo, String szUserPwd, uint nFingerIndex1, uint nFingerIndex2, uint nFingerIndex3);




         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrReadUserInfoByUserID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
         public static extern int FrReadUserInfoByUserID(uint nPortIndex, uint nNetID, String szUserID, StringBuilder szUserName, StringBuilder szUserNo, StringBuilder szUserPwd, out uint pnFingerIndex1, out uint pnFingerIndex2,out uint pnFingerIndex3);


        
        

        /// <summary>
        /// 停止注册指纹
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrRegFingerStop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrRegFingerStop(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnState">返回注册状态 =0:不在注册状态 =1:正在注册中 =2:注册被取消</param>
        /// <param name="pnFingerSize">返回指纹大小</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrRegGetState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrRegGetState(uint nPortIndex, uint nNetID, out uint pnState, out uint pnFingerSize);




        /// <summary>
        /// 读取指纹数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nGetIndex">读取指纹数据包序号：0-18</param>
        /// <param name="pnGetSize">返回指纹数据长度</param>
        /// <param name="lpData">返回指纹数据</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrGetFingerData1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrGetFingerData1(uint nPortIndex, uint nNetID, uint nGetIndex, out uint pnGetSize, Byte[] lpData);

        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrGetFingerData1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrGetFingerData(uint nPortIndex, uint nNetID, uint nGetIndex, out uint pnGetSize, Byte[] lpData);





        /// <summary>
        /// 开始设置指纹数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFingerIndex">
        /// 指纹编号大于9
        ///例：指纹编号是10
        ///则表示这个人的ID是1，指纹编号为0.
        ///那么可知，这个人可有10个指纹编号：10～19
        ///从0号开始设置
        /// </param>
        /// <param name="nFingerSize">指纹数据总长度</param>
        /// <param name="iSize">设置指纹数据大小</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetFingerBegin1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetFingerBegin1(uint nPortIndex, uint nNetID, uint nFingerIndex, uint nFingerSize, int iSize);




        /// <summary>
        /// 设置指纹数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nSetIndex">指纹数据包编号：0-18</param>
        /// <param name="tnSetSize">指纹数据包长度</param>
        /// <param name="lpSetData">指纹数据</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetFingerData1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetFingerData1(uint nPortIndex, uint nNetID, uint nSetIndex, int tnSetSize, byte[] lpSetData);




        /// <summary>
        /// 停止设置指纹数据
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrSetFingerEnd1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrSetFingerEnd1(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 开始读取指纹
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nFingerIndex"></param>
        /// <param name="pnFingerSize">返回的指纹包大小</param>
        /// <param name="iSize">指纹读取大小</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrGetFingerBegin1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrGetFingerBegin1(uint nPortIndex, uint nNetID, uint nFingerIndex, ref uint pnFingerSize, int iSize);




        /// <summary>
        /// 读取注册指纹状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnState">注册状态 =0:不在注册状态 =1:正在注册中 =2:注册被取消</param>
        /// <param name="pnFingerSize">指纹大小</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD200H_FrRegGetState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD200H_FrRegGetState(uint nPortIndex, uint nNetID, out uint pnState, out uint pnFingerSize);




        /// <summary>
        /// 读取指纹数据
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="nGetIndex"></param>
        /// <param name="pnGetSize"></param>
        /// <param name="lpData"></param>
        /// <returns></returns>
         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "CHD200H_FrGetFingerData1", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CHD200H_FrGetFingerData1(uint nPortIndex,uint nNetID, uint nGetIndex, out uint pnGetSize,  byte[] lpData);
        
    }
}
