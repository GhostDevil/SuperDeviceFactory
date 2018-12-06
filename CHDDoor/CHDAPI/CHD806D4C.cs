using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperDeviceFactory.CHDDoorAPI
{
    public static class CHD806D4C
    {
        /// <summary>
        /// 访问权限的确认
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szSysPwd">2字节系统密码(‘0’~’9’,’A’~’F’)</param>
        /// <param name="szKeyPwd">3字节设备设置密码(‘0’~’9’,’A’~’F’)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DLinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLinkOn(uint nPortIndex, uint nNetID, string szSysPwd, string szKeyPwd);





        /// <summary>
        /// 取消访问权限
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DLinkOff", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DLinkOff(uint nPortIndex, uint nNetID);





        /// <summary>
        /// 修改访问密码
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szNewSysPwd">2字节系统密码(‘0’~’9’,’A’~’F’)</param>
        /// <param name="szNewKeyPwd">3字节设备设置密码(‘0’~’9’,’A’~’F’)</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DNewPwd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DNewPwd(uint nPortIndex, uint nNetID, String szNewSysPwd, String szNewKeyPwd);





        /// <summary>
        /// 日期时间同步命令
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">日期。世纪,年，月，日，星期，时，分，秒</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetDateTime(uint nPortIndex, uint nNetID, ref SYSTEMTIME pTime);





        /// <summary>
        /// 门控器控制参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChildID">门号1字节. =1表示仅第1门；=2表示仅第2门；=0FFH双门同时设置。</param>
        /// <param name="nCtrlParam"></param>参考附录2
        /// <param name="nOpenDelay"></param>参考附录2
        /// <param name="nIrSureDelay"></param>参考附录2
        /// <param name="nRelayDelay"></param>参考附录2
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetOneParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetOneParam(uint nPortIndex, uint nNetID, uint nChildID, uint nCtrlParam, uint nOpenDelay, uint nIrSureDelay, uint nRelayDelay);





        /// <summary>
        /// 初始化记录区(清空记录)
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DInitRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DInitRec(uint nPortIndex, uint nNetID);






        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "FrLinkOn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int FrLinkOn(uint nPortIndex, uint nNetID, String szDevPwd);





        /// <summary>
        /// 设定读指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nLoadP">LoadP。1～65535</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetRecReadPoint", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetRecReadPoint(uint nPortIndex, uint nNetID, uint nLoadP);





        /// <summary>
        /// 设定门口机整个记录区指针
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nSaveP">SaveP。1～65535</param>
        /// <param name="nLoadP">LoadP。1～65535</param>
        /// <param name="nMF">MF。0</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetRecReadPointEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetRecReadPointEx(uint nPortIndex, uint nNetID, uint nSaveP, uint nLoadP, uint nMF);





        /// <summary>
        /// 动作继电器
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChildID">门地址</param>
        /// <param name="nRelayID">继电器地址</param>
        /// <param name="nDelayTime">闭合时间低字节在前</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4OpenRelay", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4OpenRelay(uint nPortIndex, uint nNetID, uint nChildID, uint nRelayID, uint nDelayTime);





        /// <summary>
        /// 设置特权卡
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCard1">特权卡。5个字节，分高低位，（‘0’～‘9’，‘A’～‘F’）</param>
        /// <param name="szCard2">特权卡。5个字节，分高低位，（‘0’～‘9’，‘A’～‘F’）</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4SetSuperCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4SetSuperCard(uint nPortIndex, uint nNetID, String szCard1, String szCard2);



        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DSetBaudrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DSetBaudrate(uint nPortIndex, uint  nNetID, uint  nBaudrate);
        



        /// <summary>
        /// 读取实时钟
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pTime">返回设备时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadDateTime", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadDateTime(uint nPortIndex, uint nNetID, out SYSTEMTIME pTime);



        /// <summary>
        /// 启动应答方式读记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecStart", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int  DReadRecStart(uint nPortIndex, uint nNetID);

      

            /// <summary>
        /// 停止应答方式读记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecStop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int  DReadRecStop(uint nPortIndex, uint nNetID);




        /// <summary>
        /// 应答方式读取历史记录
        /// </summary>
        /// <param name="nPortIndex"></param>
        /// <param name="nNetID"></param>
        /// <param name="nRecType">
        /// =0 SM上一次读取给上位机的记录已被正确受到, SM自动调整LOADP后读取下一条记录返回。
        /// =1 SM上一次读取给上位机SU的记录未被SU正确受到,或SU要求SM重复读取原LOADP处的记录。
        /// </param>
        /// <param name="szRecSource">返回历史记录来源 10字符的ASCII 如卡号、ID号</param>
        /// <param name="pTime">返回历史记录日期时间</param>
        /// <param name="pnRecWorkState">返回历史记录工作状态字</param>
        /// <param name="pnRecRemark">返回历史记录备注字</param>
        /// <param name="pnRecLineState">返回历史记录线路状态字</param>
        /// <param name="pnRecDoorID">返回历史记录门号 </param>
        /// <returns></returns>
         [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecAck", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int  DReadRecAck(uint nPortIndex,uint nNetID, uint nRecType, StringBuilder szRecSource,out SYSTEMTIME pTime, out int  pnRecWorkState, out int pnRecRemark,out int pnRecLineState, out int pnRecDoorID);
        

       



        /// <summary>
        /// 读取记录参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnBottom">返回Bottom</param>
        /// <param name="pnSaveP">返回SaveP</param>
        /// <param name="pnLoadP">返回LoadP</param>
        /// <param name="pnMaxLen">返回MaxLen</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadRecInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadRecInfo(uint nPortIndex, uint nNetID, out int pnBottom, out int pnSaveP, out int pnLoadP, out int pnMaxLen);





        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource"></param>参见附录1
        /// <param name="pTime"></param>参见附录1
        /// <param name="pnRecWorkState"></param>参见附录1
        /// <param name="pnRecRemark"></param>参见附录1
        /// <param name="pnRecLineState"></param>参见附录1
        /// <param name="pnRecDoorID"></param>参见附录1
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadOneRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);





        /// <summary>
        /// 查询最新事件记录
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szRecSource">参见附录1</param>
        /// <param name="pTime">参见附录1</param>
        /// <param name="pnRecWorkState">参见附录1</param>
        /// <param name="pnRecRemark">参见附录1</param>
        /// <param name="pnRecLineState">参见附录1</param>
        /// <param name="pnRecDoorID">参见附录1</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadOneNewRec", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadOneNewRec(uint nPortIndex, uint nNetID, StringBuilder szRecSource, out SYSTEMTIME pTime, out int pnRecWorkState, out int pnRecRemark, out int pnRecLineState, out int pnRecDoorID);





        /// <summary>
        /// 读门口机原始状态
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="pnLineState1">返回门1的线路状态字节</param>
        /// <param name="pnRelayState1">返回门1的继电器状态字节</param>
        /// <param name="pnLineState2">返回门2的线路状态字节</param>
        /// <param name="pnRelayState2">返回门2的继电器状态字节</param>
        /// <param name="pnLineState3">返回门3的线路状态字节</param>
        /// <param name="pnRelayState3">返回门3的继电器状态字节</param>
        /// <param name="pnLineState4">返回门4的线路状态字节</param>
        /// <param name="pnRelayState4">返回门4的继电器状态字节</param>
        /// <param name="pnLineState5">返回门5的线路状态字节</param>
        /// <param name="pnRelayState5">返回门5的继电器状态字节</param>
        /// <param name="pnWorkState">返回工作状态</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4ReadDoorState", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4ReadDoorState(uint nPortIndex, uint nNetID, out  uint pnLineState1, out  uint pnRelayState1, out  uint pnLineState2, out  uint pnRelayState2, out  uint pnLineState3, out  uint pnRelayState3, out  uint pnLineState4, out  uint pnRelayState4, out  uint pnLineState5, out  uint pnRelayState5, out  uint pnWorkState);





        /// <summary>
        /// 读取门口机工作特性参数
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="nChildID"></param>
        /// <param name="pnCtrlParam">返回控制参数</param>
        /// <param name="pnOpenDelay">返回继电器延时</param>
        /// <param name="pnIrSureDelay">返回红外报警确定时间</param>
        /// <param name="pnRelayDelay">返回开门继电器执行时间</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4ReadOneParam", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4ReadOneParam(uint nPortIndex, uint nNetID, uint nChildID, out  uint pnCtrlParam, out  uint pnOpenDelay, out  uint pnIrSureDelay, out  uint pnRelayDelay);





        /// <summary>
        /// 读取门口机名称及版本号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szVersion">返回设备版本</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "DReadVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DReadVersion(uint nPortIndex, uint nNetID, StringBuilder szVersion);





        /// <summary>
        /// 读取特权卡的卡号
        /// </summary>
        /// <param name="nPortIndex">端口标识</param>
        /// <param name="nNetID">设备网络ID</param>
        /// <param name="szCard1">返回卡号1</param>
        /// <param name="szCard2">返回卡号2</param>
        /// <returns>设备返回值</returns>
        [DllImport("DLL\\CHDDoorDLL\\CHDComm.dll", EntryPoint = "D4ReadSuperCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D4ReadSuperCard(uint nPortIndex, uint nNetID, byte[] szCard1, byte[] szCard2);




        /*****************************************************************
         * 附录1
         附录: 门控器(SM)的历史记录格式
	        ___________________________________________________________________________________________________________
            |事件来源（5字节）|日期，时间（7字节）  |工作状态（第1字节）|备注（1字节）|线路状态（1字节）|门号(1字节)  |
            |_________________|_____________________|___________________|_____________|_________________|_____________|
            |卡号或ID号等	  |世纪,年，月，日，时，|                   |REMARK	      | 0 （D7--D0)     |门1 =1门2 =2 |
            |                 |分，秒	            | 0（D7--D0）	    |             |                 |门3=3门4 =4  |
            |                 |                     |                   |             |                 | 门5 =5      |
            |_________________|_____________________|___________________|_____________|_________________|_____________|
         
          
           _____________________________________________________________________________________________
           |      REMARK           |               REMARK描述                  |      事件来源          |
           |_______________________|___________________________________________|________________________|
           |         0             |          读头0刷卡（进）                  |                        |
           |_______________________|___________________________________________|                        |
           |         1             |          读头1刷卡（出）                  |                        |
           |_______________________|___________________________________________|        5字节卡号       |
           |         2             |                                           |        高字节在前      |
           |_______________________|___________________________________________|________________________|
           |         3             |                                           |                        |
           |_______________________|___________________________________________|                        |
           |         4             |      一键锁死被按下(5门input 3)           |                        |
           |_______________________|___________________________________________|                        |
           |         5             |                                           |                        |
           |_______________________|___________________________________________|                        |
           |                       |        |工作状态bit0=0是进；=1是出        |                        |
           |         6             |手动放行|__________________________________|                        |
           |                       |        | 工作状态bit1=0远程，=1是本地     |                        |
           |_______________________| _______|__________________________________|                        |
           |         7             |        呼唤(5门输入input 2)               |                        |
           |_______________________|___________________________________________|                        |
           |         8             |        场红外报警                         |                        |
           |_______________________|___________________________________________|                        |
           |         9             |         对射报警                          |          5字节0        |
           |_______________________|___________________________________________|                        |
           |         10            |         超时未开门                        |                        |
           |_______________________|___________________________________________|                        |
           |         11            |         超时未关门                        |                        |
           |_______________________|___________________________________________|                        |
           |         12            |         合法开门                          |                        |
           |_______________________|___________________________________________|                        |
           |         13            |         门被关闭                          |                        |
           |_______________________|___________________________________________|                        |
           |         14            |         非法开门                          |                        |
           |_______________________|___________________________________________|                        |
           |         15            |         有对射                            |                        |
           |_______________________|___________________________________________|                        |
           |         16            |                                           |                        |
           |_______________________|___________________________________________|________________________|
           |         0XF0          |         读头0刷特权卡（进）               |                        |
           |_______________________|___________________________________________|                        |
           |         0XF1          |         读头1刷特权卡（出）               |       5字节卡号        |
           |_______________________|___________________________________________|       高字节在前       |
           |         0XF2          |                                           |                        |
           |_______________________|___________________________________________|                        |
           |         0XF3          |                                           |                        |
           |_______________________|___________________________________________|________________________|
           |         0X10          |         读头0胁迫（进）                   |                        |
           |_______________________|___________________________________________|________________________|
           |         OX11          |         读头1胁迫（出）                   |                        |
           |_______________________|___________________________________________|________________________|
           |         0X20          |         读头0按键（进）                   |                        |
           |_______________________|___________________________________________|                        |
           |         0X21          |         读头1按键（出）                   |                        |
           |_______________________|___________________________________________|    5字节按键值         |
           |         0X22          |                                           |                        |
           |_______________________|___________________________________________|                        |
           |         0X23          |                                           |                        |
           |_______________________|___________________________________________|________________________|
        
         * 
         * 
         *附录2
         *门1参数8字节 
         序号	 门1参数名	           意义      	                 说明
         * **************************************************************************
          1	     CTRL1	               控制字#1	                     D7—D0有不同意义
          2	     RELAY DELAY	       门锁动作延时	                 1字节，0—25.5秒
          3	     OPEN DELAY	           开门等待进入延时	             1字节, 2—255秒
          4	     IR SURE	           红外报警确认延时	             1字节, 2—255秒
          5	     IR ONDLY	           布防开启延时	                 1字节, 2—255秒
          6	     CTRL2	               控制字#2	                     D7—D0有不同意义
          7	     CTRL3	               控制字#3	                     D7—D0有不同意义
          8	     Ctrl4	               控制字#4	                     D7—D0有不同意义
        ******************************************************************************
         * 
         * 
         * 
         * 
         * ***************************************************************************
         * 第1门 CTRL1--第1控制字
         *D7	=1从=1开始就监控门状态(不论自动布防时段是否有效亦监控); =0临时不监控,仅在布防时段有效时自动开启监控,无效时结束监控
          D6	=1从=1开始就监控红外状态(不论自动布防时段是否有效亦监控); =0临时不监控, 仅在布防时段有效时自动开启监控,无效时结束监控
          D5	=1 当D1=1且在密码时段时,第2感应头要密码确认; =0不需要
          D4	=1 当D1=1且在密码时段时,第1感应头要密码确认; =0不需要
          D3	=1表示选择的门磁感应器在开门状态时其输出是开路;  =0反之
          D2	=1表示选择的红外感应器在报警状态时其输出是开路;  =0反之
          D1	=0 在密码时段内刷卡正确无需密码;  =1按密码时段确定是否要密码
          D0	=0 紧急输入时常开门;   =1紧急输入时常闭门任何卡不能开门

         * 当紧急输入时还结合CTRL2的D7,D6,D5,D4控制位动作ALARM1,当D5或D4不为0时按CTRL1的D0控制ALARM1.
         * ***************************************************************************
         * 
         * 
         * 
         * 
         * 第1门 CTRL2 --第2控制字
         * 	              ALARM1(继电器动作表)	                          RELAY1(继电器动作表)
         *****************************************************************************************
            D7	          报警(门磁或其他)	=1动作, =0 不动作		
            D6	          手动出门按钮	=1动作, =0 不动作		
            D5	          第2头刷卡合法	=1动作, =0 不动作		
            D4	          第1头刷卡合法	=1动作, =0 不动作		
            D3	          无效卡或卡失效时	=1动作,=0不动作		
            D2	       	                                                  手动按钮	=1动作, =0 NO 
            D1	       	                                                  第2感应头刷卡合法	=1动作, =0 NO
            D0	                                                           =1 只要刷卡或按键都动作，用来与监控摄像系统同步；=0（根据D7—D3定义）
         *
         * 注意1：D7，D3都是0，则表示报警继电器不做报警使用。当D3=0时表示无效卡刷卡时不产生错误状态到SU。但D7=0仍产生错误状态到SU，但不再驱动报警继电器。
         * *****************************************************************************************
         * 
         * 
         * 
         * 
         * 
         * 第1门 CTRL3--第3控制字
         * *****************************************************************************************
         * D7	 =1网络正常时由中心确认开门；=0始终本地确认；
           D6	 =1 作业时段屏蔽N+1功能， =0作业时段也不屏蔽，N+1全天有效
           D5	 =1 开启N+1功能：N卡刷卡后必须再加一特权卡确认才能开门；=0 关闭N+1功能：N刷卡后就可以开门；
           D4	 =0 门不关联；=1门关联（一个门开启，另些门不能再开）
           D3	 在第四控制字CTRL4--D0=0，第三控制字CTRL3—D3：=0时表示N=1单卡开门；=1时表示N=2双卡开门；当CTRL4--D0=1，而CTRL3—D3=0时：表示N=3三卡开门；
           D2	 =1 门锁在“驱动开始—驱动结束”后不能自动上锁，要求人工“先开门—再关门”的动作后才能上锁；=0门锁在驱动结束后自动上锁。
           D1	 =1双卡开门时要求分组；=0不分组，两张授权卡就能开门
           D0	 =1在DCU有事件发生时主动向上位机发请求，=0不主动
         * 
         * *****************************************************************************************
         * 
         * 
         * 
         * 第1门 CTRL4--第4控制字
         * *****************************************************************************************
         *  D7-d4	备份
            D3	=1 支持输入ID+密码（PIN）开门； =0屏蔽
            D2	=1 首卡后只需1张卡；=0需要N+1的N张卡（N=1，2，3）
            D1	=1 在N+1时段内必须N+1确认，在此之外首次N+1确认后就无需再次 N+1确认，直至时段结束或日期翻转第二天； =0 根据设置N+1或N确认要求；
            D0	=1：N=3卡确认；=0  （由CTRL3的D5=0/1定）单卡或双卡N=1或2
         * 
         * ******************************************************************************************
         * 
         */
    }
}
         







