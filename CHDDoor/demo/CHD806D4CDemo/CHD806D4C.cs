using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHD806D4CDemo
{
    public partial class CHD806D4C : Form
    {
        private int portId;
        private uint ctrParam = 0;

        public CHD806D4C()
        {
            InitializeComponent();
            InitCMP();
            this.Load += (o, e) =>
            {
                CHD.CommonUI.ConnectDevice conDivice = new CHD.CommonUI.ConnectDevice(1);
                conDivice.ShowDialog();
                if (conDivice.DialogResult != DialogResult.OK)
                {
                    this.Close();
                }
                else
                {
                    this.portId = conDivice.PortId;
                }
            };


        }

        public CHD806D4C(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;

        }


        #region 辅助函数
        private void InitCMP()
        {
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 3;

        }

        /// <summary>
        /// 供反射调用
        /// </summary>
        /// <param name="act"></param>
        public void ShowUI(Action act)
        {
            this.FormClosed += (e, o) => { act(); };
            InitCMP();
            this.ShowDialog();
        }




        /// <summary>
        /// 清空消息显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelInfo_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
        }



        /// <summary>
        /// 向信息台输出信息
        /// </summary>
        /// <param name="msg">要输出的信息</param>
        private void PrintMessage(String msg)
        {
            this.dataGridView1.Rows.Insert(0, new string[] { msg });
        }




        /// <summary>
        /// 网络ID
        /// </summary>
        public uint NetId
        {
            get
            {
                try
                {
                    uint netid = Convert.ToUInt32(txtNetId.Text);
                    return netid;
                }
                catch
                {
                    PrintMessage("网络ID非法，请正确设置网络ID！");
                    return (uint)0;
                }
            }
        }




        /// <summary>
        /// 端口ID
        /// </summary>
        public uint PortId
        {
            get { return (uint)this.portId; }
        }
        #endregion



        #region 通用指令
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4C.D4OpenRelay(PortId, NetId, (uint)(comboBox2.SelectedIndex + 1), (uint)(comboBox3.SelectedIndex), uint.Parse(textBox30.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("打开继电器成功! 延时:" + textBox30.Text);
                    break;
                case 0x07:		//无权限
                    PrintMessage("打开继电器失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("打开继电器失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4C.D4OpenRelay(PortId, NetId, (uint)(comboBox2.SelectedIndex), (uint)(comboBox3.SelectedIndex), 0/*=0表示立即关闭继电器*/);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("关闭继电器成功! ");
                    break;
                case 0x07:		//无权限
                    PrintMessage("关闭继电器失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("关闭继电器失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {


            int nRetValue = CHD.API.CHD806D4C.DLinkOn(PortId, NetId, textBox28.Text, textBox29.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("确认权限成功! ");
                    break;
                case 0x07:
                    PrintMessage("确认权限失败! 错误提示: 无权限");
                    break;
                default:
                    PrintMessage("确认权限失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D4C.DNewPwd(PortId, NetId, textBox28.Text, textBox29.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置密码成功! ");
                    break;
                case 0x07:
                    PrintMessage("设置密码失败! 错误提示: 无权限");
                    break;
                default:
                    PrintMessage("设置密码失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4C.DLinkOff(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("取消权限成功! ");
                    break;
                case 0x07:
                    PrintMessage("取消权限失败! 错误提示: 无权限");
                    break;
                default:
                    PrintMessage("取消权限失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取超级卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            byte[] szCard1 = new byte[12];
            byte[] szCard2 = new byte[12];

            int nRetValue = CHD.API.CHD806D4C.D4ReadSuperCard(PortId, NetId, szCard1, szCard2);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功

                        PrintMessage(String.Format("读取超级卡成功! 卡1:{0} 卡2:{1}", Encoding.Default.GetString(szCard1), Encoding.Default.GetString(szCard2)));
                        textBox26.Text = Encoding.Default.GetString(szCard1);
                        textBox27.Text = Encoding.Default.GetString(szCard2);

                        break;
                    }
                case 0x07:		//无权限
                    PrintMessage("读取超级卡失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("读取超级卡失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置超级卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4C.D4SetSuperCard(PortId, NetId, textBox26.Text, textBox27.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置超级卡成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置超级卡失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置超级卡失败! 代码:" + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4C.DSetBaudrate(PortId, NetId, (uint)(comboBox4.SelectedIndex));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置波特率成功!");
                    break;
                default:
                    PrintMessage("设置波特率失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime;
            int nRetValue = CHD.API.CHD806D4C.DReadDateTime(PortId, NetId, out stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("读取时间成功：{0} 星期{1}", CHD.Common.ParasTime(stime).ToString(), stime.wDayOfWeek));
                    break;
                default:
                    PrintMessage("读取时间失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime = CHD.Common.ParasTime(DateTime.Now);

            int nRetValue = CHD.API.CHD806D4C.DSetDateTime(PortId, NetId, ref stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("同步时间成功：{0} 星期{1}", CHD.Common.ParasTime(stime).ToString(), stime.wDayOfWeek));
                    break;
                default:
                    PrintMessage("同步时间失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 监控状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            uint nLineState1 = 0, nRelayState1 = 0;
            uint nLineState2 = 0, nRelayState2 = 0;
            uint nLineState3 = 0, nRelayState3 = 0;
            uint nLineState4 = 0, nRelayState4 = 0;
            uint nLineState5 = 0, nRelayState5 = 0;
            uint nWorkState = 0;

            int nRetValue = CHD.API.CHD806D4C.D4ReadDoorState(PortId, NetId, out nLineState1, out nRelayState1,
                out nLineState2, out nRelayState2,
                out nLineState3, out nRelayState3,
                out nLineState4, out nRelayState4,
                out nLineState5, out nRelayState5,
                out nWorkState);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("读取监控状态，第1门线路状态字：0x{0} 继电器状态字：0x{1} ", nLineState1.ToString("X2"), nRelayState1.ToString("X2")));
                    PrintMessage(String.Format("读取监控状态，第2门线路状态字：0x{0} 继电器状态字：0x{1} ", nLineState2.ToString("X2"), nRelayState2.ToString("X2")));
                    PrintMessage(String.Format("读取监控状态，第3门线路状态字：0x{0} 继电器状态字：0x{1} ", nLineState3.ToString("X2"), nRelayState3.ToString("X2")));
                    PrintMessage(String.Format("读取监控状态，第4门线路状态字：0x{0} 继电器状态字：0x{1} ", nLineState4.ToString("X2"), nRelayState4.ToString("X2")));
                    PrintMessage(String.Format("读取监控状态，第5门线路状态字：0x{0} 继电器状态字：0x{1} ", nLineState5.ToString("X2"), nRelayState5.ToString("X2")));
                    break;
                default:
                    PrintMessage("读取设备信息失败! 错误码: " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// 设备版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            StringBuilder szVersion = new StringBuilder();

            int nRetValue = CHD.API.CHD806D4C.DReadVersion(PortId, NetId, szVersion);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage("读取设备信息成功: " + szVersion.ToString());
                        break;
                    }
                default:
                    PrintMessage("读取设备信息失败! 错误码:" + nRetValue);
                    break;
            }
        }
        #endregion


        #region 记录管理
        /// <summary>
        /// 初始化记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D4C.DInitRec(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(("初始化记录成功! "));
                    break;
                case 0x07:		//Failed
                    PrintMessage(("初始化记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage("初始化记录失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设备记录区状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button34_Click(object sender, EventArgs e)
        {
            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0;

            int nRetValue = CHD.API.CHD806D4C.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("读取记录区状态成功! Bottom={0} nSaveP={1} nLoadP={2} nMaxLen={3}", nBottom, nSaveP, nLoadP, nMaxLen));
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取记录参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0, nRecordCount = 0;

            int nRetValue = CHD.API.CHD806D4C.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out  nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    if (nSaveP >= nLoadP)
                        nRecordCount = nSaveP - nLoadP;
                    else
                        nRecordCount = nMaxLen - (nLoadP - nSaveP);
                    PrintMessage(String.Format("读取成功! 设备当前未读取记录数: {0} Bottom:{1} SaveP:{2} LoadP:{3} MaxLen:{4}", nRecordCount,
                        nBottom, nSaveP, nLoadP, nMaxLen));
                    break;
                default:		//Failed
                    PrintMessage("读取失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        private string FormatLog(StringBuilder szRecSourceT, DateTime RecTime, int nRecState, int nRecRemark, int nRecLineState)
        {
            string strLog = "";
            String strTemp = "";
            switch (nRecRemark)
            {
                case 0:
                    strLog = String.Format("时间:{0} 刷卡开门!卡号:{1} ", RecTime, szRecSourceT);
                    /*strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x40)?_T("门初始态:开;") : _T("门初始态:关;");
                    strTemp += (nRecState & 0x01)?_T("开门后规定延时内未进入;") : _T("开门后规定延时内进入;"); 
                    strTemp += (nRecState & 0x20)?_T("进入后在规定时间内未关门;") : _T("进入后在规定时间内关门;"); 
                    strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                    strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                    strTemp += (nRecState & 0x04)?_T("胁迫状态;") : _T("正常刷卡;"); 
                    strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                    //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:0x{0}, 线路字节:0x%{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 1:
                    //strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strLog = string.Format("时间:{0} 键入用户ID及个人密码开门!", RecTime);
                    strTemp = string.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 2:
                    strLog = string.Format("时间:{0} 远程开门!", RecTime);
                    strTemp = string.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 3:
                    strLog = String.Format("时间:{0} 手动开门!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 4:
                    break;
                case 5:
                    strLog = String.Format("时间:{0} 告警!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 6:
                    strLog = String.Format("时间:{0} 掉电!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 7:
                    strLog = String.Format("时间:{0} 内部控制参数被修改!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 8:
                    strLog = String.Format("时间:{0} 无效刷卡开门!卡号:{1} ", RecTime, szRecSourceT);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 9:
                    strLog = String.Format("时间:{0} 用户卡已过期!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 10:
                    strLog = String.Format("时间:{0} 当前时间该用户卡无进入权限!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 11:
                    strLog = String.Format("时间:{0} 密码失败次数越限!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 15:
                    strLog = String.Format("时间:{0} 授权!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 16:
                    strLog = String.Format("时间:{0} 撤销权限!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{01}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x22:
                    if ((szRecSourceT.ToString()).Substring(4, 1) == "1")
                        strLog = String.Format("时间:{0} 紧急事件开始!", RecTime);
                    else
                        strLog = String.Format("时间:{0} 紧急事件结束!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x40:
                    strLog = String.Format("时间:{0} 合法刷卡等待中心确认开门!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x60:
                    strLog = String.Format("时间:{0} 合法刷卡本地确认开门", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState, nRecLineState);//Dialog box is not enough to show log
                    break;
                case 0x61:
                    strLog = String.Format("时间:{0} 合法本地输入两个紧急密码确认开门!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x62:
                    strLog = String.Format("时间:{0} 合法刷卡本地驱动开门继电器后，经判别门磁确认门已开!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x63:
                    strLog = String.Format("时间:{0} 合法刷卡本地开门后，在规定的延时内门被正常关闭!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x70:
                    strLog = String.Format("时间:{0} 超权限卡刷卡开门记录!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x71:
                    strLog = String.Format("时间:{0} 增加了1张超权限卡的记录!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                case 0x72:
                    strLog = String.Format("时间:{0} 删除了1张超权限卡的记录!", RecTime);
                    strTemp = String.Format("状态字节:{0}, 线路字节:{1}", nRecState.ToString("X2"), nRecLineState.ToString("X2"));//Dialog box is not enough to show log
                    break;
                default:
                    strTemp = String.Format("未知记录! 备注字节:{0} 状态字节:{1}, 线路字节:{2}", nRecRemark, nRecState.ToString("X2"), nRecLineState.ToString("X2"));
                    break;
            }
            strLog += strTemp;
            return strLog;
        }


        private bool ReadOneRecord()
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            bool nRetFlag = false;

            int nRetValue = CHD.API.CHD806D4C.DReadOneRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState));
                    nRetFlag = true;
                    break;
                case 0xE4:		//Failed
                    PrintMessage("设备内无记录!");
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    nRetFlag = false;
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码: " + nRetValue);
                    nRetFlag = false;
                    break;
            }
            return nRetFlag;
        }

        /// <summary>
        /// 顺序读取记录，并将设备读取指针移动到下一个记录。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            ReadOneRecord();
        }



        /// <summary>
        /// 指定记录序号读取一条记录，不移动设备读取指针。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int nRetValue = CHD.API.CHD806D4C.DReadOneRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success

                    PrintMessage(String.Format("读取记录序号:[{0}]成功! {1}", 0, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取最新历史事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;


            int nRetValue = CHD.API.CHD806D4C.DReadOneNewRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success

                    PrintMessage(FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState));

                    break;
                case 0xE4:		//Failed
                    PrintMessage("设备内无记录!");
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 应答方式读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            int bExitLoop = 1, bAckFlag = 1;

            int nRetvalue = CHD.API.CHD806D4C.DReadRecStart(PortId, NetId);
            if (nRetvalue != 0)
            {
                if (nRetvalue == 0xE4)
                {
                    PrintMessage(String.Format("无记录读取! 错误码: {0} 提示:设备内无记录", nRetvalue));
                }
                return;
            }
            PrintMessage("开始应答方式读取!");

            button31.Text = "正在读取...";
            button31.Enabled = false;

            while (bExitLoop == 1)
            {
                nRetvalue = CHD.API.CHD806D4C.DReadRecAck(PortId, NetId, (uint)bAckFlag, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
                bAckFlag = 1;
                switch (nRetvalue)
                {
                    case 0x00:		//Success
                        PrintMessage(FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState));
                        break;
                    case 0xE4:		//Failed
                        bExitLoop = 0;
                        PrintMessage("设备内无记录!");
                        break;
                    case 0xE5:		//Failed
                        bExitLoop = 0;
                        PrintMessage("设备所有记录已经读完!");
                        break;
                    default:		//Failed
                        bAckFlag = 0;
                        PrintMessage("读取记录失败! 错误代码:" + nRetvalue);
                        break;
                }
            }
            button31.Enabled = true;
            button31.Text = "开始读取记录";

            //关闭应答模式
            nRetvalue = CHD.API.CHD806D4C.DReadRecStop(PortId, NetId);
            if (nRetvalue == 0)
            {
                PrintMessage("关闭应答方式!");
            }
            else
            {
                PrintMessage("关闭应答方式失败! 错误码: " + nRetvalue);
            }
            button31.Enabled = true;
            button31.Text = "读取";

        }
        #endregion


        #region 控制器参数
        /// <summary>
        /// 读取控制参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAllCtrParam_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
            rightMenu.Items.Add("读取 1 门参数", null, (obj, ee) => { ReadParameter(1); });
            rightMenu.Items.Add("读取 2 门参数", null, (obj, ee) => { ReadParameter(2); });
            rightMenu.Items.Add("读取 3 门参数", null, (obj, ee) => { ReadParameter(3); });
            rightMenu.Items.Add("读取 4 门参数", null, (obj, ee) => { ReadParameter(4); });
            rightMenu.Show(Cursor.Position);
        }
        private void ReadParameter(uint doorId)
        {
            uint DoorRelayDelay; 
            uint OpenedRelayDelay;  
            uint IrSureDelay; 
            uint nCtrlParam;

            int nRetValue = CHD.API.CHD806D4C.D4ReadOneParam(PortId, NetId, doorId,
        out nCtrlParam, out DoorRelayDelay, out OpenedRelayDelay, out IrSureDelay);

            txtDoorRelayDelay.Text=DoorRelayDelay.ToString(); 
            txtOpenedRelayDelay.Text=OpenedRelayDelay.ToString(); 
            txtIrSureDelay.Text=IrSureDelay.ToString();
            switch (nRetValue)
            {
                case 0x00:
                    {
                        treeView1.Nodes[0].Text = "控制字节0   " + nCtrlParam;
                        string param = Convert.ToString(nCtrlParam, 2);
                        string pm = param;
                        for (int i = 0; i < 8 - param.Length; i++)
                        {
                            pm = ("0" + pm);
                        }

                        for (int i = 7, j = 0; i >= 0; i--, j++)
                        {
                           // treeView1.Nodes[0].Nodes[j].Checked = pm.Substring(i, 1) == "1" ? true : false;
                            treeView1.Nodes[0].Nodes[j].Checked = pm.Substring(j, 1) == "1" ? true : false;
                        }


                        PrintMessage(String.Format("读取 {0} 门控制参数成功!", doorId));
                    }
                    break;
                default:
                    PrintMessage("读取控制参数失败! 错误码:" + nRetValue);
                    break;
            }

        }

        private void SetParameter(uint doorId)
        {
            int nRetValue = CHD.API.CHD806D4C.D4SetOneParam(PortId, NetId, doorId,
        ctrParam, uint.Parse(txtDoorRelayDelay.Text), uint.Parse(txtOpenedRelayDelay.Text), uint.Parse(txtIrSureDelay.Text));
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage(string.Format("设置 {0} 门控制参数成功!", doorId));
                    }
                    break;
                default:
                    PrintMessage("设置控制参数失败! 错误码:" + nRetValue);
                    break;
            }
        }


        /// <summary>
        /// 设置控制参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOneByOne_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
            rightMenu.Items.Add("设置 1 门参数", null, (obj, ee) => { SetParameter(1); });
            rightMenu.Items.Add("设置 2 门参数", null, (obj, ee) => { SetParameter(2); });
            rightMenu.Items.Add("设置 3 门参数", null, (obj, ee) => { SetParameter(3); });
            rightMenu.Items.Add("设置 4 门参数", null, (obj, ee) => { SetParameter(4); });
            rightMenu.Show(Cursor.Position);
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Name == "ctrParam1")
            {
                e.Cancel = true;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name != "ctrParam1")
            {
                StringBuilder sb = new StringBuilder();
                foreach (TreeNode nd in e.Node.Parent.Nodes)
                {
                    if (nd.Checked)
                    {
                        sb.Append("1");
                    }
                    else
                    {
                        sb.Append("0");
                    }

                }
                int number = Convert.ToInt32(sb.ToString(), 2);
                e.Node.Parent.Text = string.Format("{0}   {1}", "控制字节0", number);
                ctrParam = (uint)number;
            }
        }
        #endregion


    }
}
