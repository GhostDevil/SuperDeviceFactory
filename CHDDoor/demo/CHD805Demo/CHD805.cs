using CHD.CommonUI;
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

namespace CHD805Demo
{
    public partial class CHD805 : Form
    {
        private int portId;


        public CHD805()
        {
            InitializeComponent();
            this.Load += (o, e) =>
            {
                ConnectDevice conDivice = new ConnectDevice();
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

        public CHD805(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitDataGrid();
            InitComponent();
        }




        #region 辅助函数

        private void InitComponent()
        {
            cmbGetCardMode.SelectedIndex = 0;
            cmbCardNoMethod.SelectedIndex = 0;
            cmbCardType.SelectedIndex = 0;
            cmbSetWeek1.SelectedIndex = 5;
            cmbSetWeek2.SelectedIndex = 6;
            cmbWeekTime.SelectedIndex = 0;
        }



        /// <summary>
        /// 供反射调用
        /// </summary>
        /// <param name="act"></param>
        public void ShowUI(Action act)
        {
            this.FormClosed += (e, o) => { act(); };
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




        private void PrintExcuteResult(int result, string sucMsg, string failMsg)
        {
            if (result == 0)
            {
                PrintMessage(sucMsg);
            }
            else
            {
                PrintMessage(String.Format("{0}错误码: {1}", failMsg, result));
            }
        }




        /// <summary>
        /// 获取时间段的字符表达串
        /// </summary>
        /// <param name="dtPickers"></param>
        /// <returns></returns>
        private StringBuilder GetTimeString(DateTimePicker[] dtPickers)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dtPickers.Length; i++)
            {
                sb.Append(dtPickers[i].Value.Hour.ToString("D2"));
                sb.Append(dtPickers[i].Value.Minute.ToString("D2"));
            }
            return sb;
        }



        /// <summary>
        /// 将时间段字符串设置到DtatePicer
        /// </summary>
        /// <param name="dtPickers"></param>
        /// <param name="timeStr"></param>
        private void SetDateTimePicer(DateTimePicker[] dtPickers, string timeStr)
        {
            if (timeStr.Length > 32)
            {
                for (int i = 0; i < dtPickers.Length; i++)
                {
                    dtPickers[i].Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(timeStr.Substring(i * 4, 2)), Convert.ToInt32(timeStr.Substring(i * 4 + 2, 2)), 0);
                }
            }
        }



        private uint GetParam(Object sender)
        {
            Button btn = sender as Button;
            StringBuilder sb = new StringBuilder();
            var controls = from Control c in btn.Parent.Controls
                           where c is GroupBox
                           orderby c.Tag ascending
                           select c;
            foreach (var ctr in controls)
            {
                foreach (Control rad in ctr.Controls)
                {
                    if (rad is RadioButton)
                    {
                        RadioButton radioBtn = rad as RadioButton;
                        if (radioBtn.Text == "启用")
                        {
                            if (radioBtn.Checked)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                    }
                }
            }
            uint ctrParam = Convert.ToUInt16(sb.ToString(), 2);
            return ctrParam;
        }




        private String FormatLog(String szRecSource, DateTime lpRecTime, uint nRecState, uint nRecRemark)
        {
            String strLog = "";
            String strTemp = "";
            switch (nRecState)
            {
                case 0:
                    strLog = String.Format("时间:{0} 刷卡开门!卡号:{1} ", lpRecTime, szRecSource);
                    /*strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                        strTemp += (nRecState & 0x40)?_T("门初始态:开;") : _T("门初始态:关;");
                        strTemp += (nRecState & 0x01)?_T("红外初始态:开;") : _T("红外初始态:关;"); 
                        strTemp += (nRecState & 0x20)?_T("开门未进入;") : _T("开门进入;"); 
                        strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                        strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                        strTemp += (nRecState & 0x04)?_T("密码验证成功;") : _T("密码验证失败;"); 
                        strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                        //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 1:
                    //strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strLog = String.Format("时间:{0} 键入用户ID及个人密码开门!", lpRecTime);
                    /*strTemp += (nRecState & 0x40)?_T("门初始状态:开;") : _T("门初始状态:关;");
                    strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); 
                    strTemp += (nRecState & 0x20)?_T("开门未进入;") : _T("开门进入;"); 
                    strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                    strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                    strTemp += (nRecState & 0x04)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                    //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 2:
                    strLog = String.Format("时间:{0} 远程开门!", lpRecTime);
                    /*strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x40)?_T("门初始状态:开;") : _T("门初始状态:关;");
                    strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); 
                    strTemp += (nRecState & 0x20)?_T("开门未进入;") : _T("开门进入;"); 
                    strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                    strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                    strTemp += (nRecState & 0x04)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                    //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 3:
                    strLog = String.Format("时间:{0} 手动开门!", lpRecTime);
                    /*strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x40)?_T("门初始状态:开;") : _T("门初始状态:关;");
                    strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); 
                    strTemp += (nRecState & 0x20)?_T("开门未进入;") : _T("开门进入;"); 
                    strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                    strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                    strTemp += (nRecState & 0x04)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                    //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 4:
                    strLog = String.Format("时间:{0} 联动开门!", lpRecTime);
                    /*strTemp += (nRecState & 0x80)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x40)?_T("门初始状态:开;") : _T("门初始状态:关;");
                    strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); 
                    strTemp += (nRecState & 0x20)?_T("开门未进入;") : _T("开门进入;"); 
                    strTemp += (nRecState & 0x10)?_T("等待时间后门未关闭;") : _T("等待时间后门关闭;"); 
                    strTemp += (nRecState & 0x08)?_T("关闭红外监控;") : _T(""); 
                    strTemp += (nRecState & 0x04)?_T("密码验证成功;") : _T("密码验证失败;"); 
                    strTemp += (nRecState & 0x02)?_T("分体刷卡;") : _T("主体刷卡;"); 
                    //strTemp += (nRecState & 0x01)?_T("红外监控初始态:开;") : _T("红外监控初始态:关;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 5:
                    strLog = String.Format("时间:{0} 告警!", lpRecTime);
                    /*{
                        switch ( *(szRecSource+4) )
                        {
                        case 0:
                            strTemp += _T("红外报警开始;");
                            break;
                        case 1:
                            strTemp += _T("红外报警结束;");
                            break;
                        case 2:
                            strTemp += _T("非正常开门;");
                            break;
                        case 3:
                            strTemp += _T("门被关闭;");
                            break;
                        case 4:
                            strTemp += _T("联动(I2 )有效;");
                            break;
                        case 5:
                            strTemp += _T("联动(I2 )无效;");
                            break;
                        case 6:
                            strTemp += _T("设备内部存储器发生错误,设备自动进行了初始化操作;");
                            break;
                        case 7:
                            strTemp += _T("红外监测被关闭;");
                            break;
                        case 8:
                            strTemp += _T("红外监测开启;");
                            break;
                        case 9:
                            strTemp += _T("门碰开关监测被关闭;");
                            break;
                        case 10:
                            strTemp += _T("门碰开关监测开启;");
                            break;
                        default:
                            strTemp += _T("");
                            break;
                        }
                    }
                    strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 6:
                    strLog = String.Format("时间:{0} 掉电!", lpRecTime);
                    /*strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16)); ;//Dialog box is not enough to show log
                    break;
                case 7:
                    strLog = String.Format("时间:{0} 内部控制参数被修改!", lpRecTime);
                    /*strTemp += ( *(szRecSource+4) & 0x80 )?_T("红外设置被修改;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x40 )?_T("特殊日期被修改;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x20 )?_T("准进时段被修改;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x10 )?_T("时间被修改;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x08 )?_T("用户资料被删除;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x04 )?_T("新增用户;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x02 )?_T("门禁参数被修改;") : _T(""); 
                    strTemp += ( *(szRecSource+4) & 0x01 )?_T("密码被修改;") : _T(""); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 8:
                    strLog = String.Format("时间:{0} 无效刷卡开门!卡号:{1}", lpRecTime, szRecSource);
                    /*strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 9:
                    strLog = String.Format("时间:{0} 用户卡已过期!", lpRecTime);
                    /*strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 10:
                    strLog = String.Format("时间:{0} 非准进时段刷卡!", lpRecTime);
                    /*strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 11:
                    strLog = String.Format("时间:{0} 密码失败次数越限!", lpRecTime);
                    /*strTemp += (nRecState & 0x08)?_T("门状态:开;") : _T("门状态:关"); 
                    strTemp += (nRecState & 0x04)?_T("红外输入状态:报警;") : _T("红外输入状态:无效;"); 
                    strTemp += (nRecState & 0x02)?_T("手动按键:松;") : _T("手动按键:按下"); 
                    strTemp += (nRecState & 0x01)?_T("I2:有效;") : _T("I2:无效;"); */
                    strTemp = String.Format("状态字节:{0}", Convert.ToString(nRecRemark, 16));//Dialog box is not enough to show log
                    break;
                case 0x22:
                //if (*(szRecSource + 4) == 1)
                //    strLog.Format(_T("时间:{0} 有效的消防联动输入开始时间!"),
                //        lpRecTime);
                //else
                //    strLog.Format(_T("时间:{0} 有效的消防联动输入结束时间!"),
                //        lpRecTime);
                //strTemp.Format(_T("状态字节:0x%02x"), nRecRemark);//Dialog box is not enough to show log
                //break;
                //待解决
                default:
                    strTemp = String.Format("未知记录! 备注字节:{0}", Convert.ToString(nRecRemark, 16));
                    break;
            }
            strLog += strTemp;
            return strLog;
        }

        #endregion



        #region 卡/用户管理

        /// <summary>
        /// 设置设备时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDevTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME time = CHD.Common.ParasTime(DateTime.Now);
            int result = CHD.API.CHD805.SetDateTime(PortId, NetId, ref time);
            string msg = "";
            switch (result)
            {
                case 0:
                    msg = "设置设备时间成功!";
                    break;
                case 7:
                    msg = "设置设备时间失败! 错误提示: 无权限!";
                    break;
                default:
                    msg = String.Format("设置设备时间失败! 错误代码: {0}", result);
                    break;
            }
            PrintMessage(msg);
        }



        /// <summary>
        /// 设置准进时段缺省值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLmtInit_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetLmtTimeInit(PortId, NetId);
            string msg = "";
            switch (nRetValue)
            {
                case 0x00:		//成功
                    msg = "将准进时段、管理时段设置成缺省值成功!";
                    break;
                case 0x07:		//无权限
                    msg = "将准进时段、管理时段设置成缺省值失败! 错误提示: 无权限!";
                    break;
                default:		//其他值表示失败
                    msg = string.Format("将准进时段、管理时段设置成缺省值失败! 错误代码: {0}", nRetValue);
                    break;
            }
            PrintMessage(msg);
        }



        /// <summary>
        /// 取消准进时段缺省值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelLmtInit_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetLmtTimeInvalid(PortId, NetId);
            string msg = "";
            switch (nRetValue)
            {
                case 0x00:		//成功
                    msg = ("取消准进时段、管理时段设置缺省值成功!");
                    break;
                case 0x07:		//无权限
                    msg = ("取消准进时段、管理时段设置缺省值失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    msg = String.Format("取消准进时段、管理时段设置缺省值失败! 错误代码: {0}", nRetValue);
                    break;
            }
            PrintMessage(msg);
        }



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOn_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.LinkOn(PortId, NetId, txtSysPwd.Text, txtKeyPwd.Text);
            string msg = "";
            switch (nRetValue)
            {
                case 0x00:		//成功
                    msg = ("确认权限成功!");
                    break;
                case 0x07:		//无权限
                    msg = ("确认权限失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    msg = String.Format("确认权限失败! 错误代码: {0}", nRetValue);
                    break;
            }
            PrintMessage(msg);
        }



        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPwd_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.NewPassword(PortId, NetId, txtSysPwd.Text, txtKeyPwd.Text);
            string msg;
            switch (nRetValue)
            {
                case 0x00:		//成功
                    msg = ("设置设备密码成功!");
                    break;
                case 0x07:		//无权限
                    msg = ("设置设备密码失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    msg = String.Format("设置设备密码失败! 错误代码: {0}", nRetValue);
                    break;
            }
            PrintMessage(msg);

        }



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOff_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.LinkOff(PortId, NetId);
            string msg = "";
            switch (nRetValue)
            {
                case 0x00:		//成功
                    msg = ("取消权限成功!");

                    break;
                case 0x07:		//无权限
                    msg = ("取消权限失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    msg = String.Format("取消权限失败! 错误代码: {0}", nRetValue);
                    break;
            }
            PrintMessage(msg);
        }



        /// <summary>
        /// 设置取卡模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetGetCardMode_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetRFCardCode(PortId, NetId, (uint)cmbGetCardMode.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置取卡模式成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置取卡模式失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置取卡模式失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME lmtTime = CHD.Common.ParasTime(dpAddULmtTime.Value);
            int nRetValue = CHD.API.CHD805.AddUser(PortId, NetId, txtCardNo.Text, txtUserId.Text, txtOpenDoorPwd.Text, ref lmtTime, 0);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("增加用户成功! 卡号:{0} 用户ID:{1} ", txtCardNo.Text, txtUserId.Text));
                    break;
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE2:		//SM内已满
                    PrintMessage("SM内已满!");
                    break;
                case 0xE6:		//用户ID号重复
                    PrintMessage(String.Format("用户ID:{0} 重复!", txtUserId.Text));
                    break;
                case 0xE7:		//卡号重复
                    PrintMessage(string.Format("卡号:{0} 重复!", txtCardNo.Text));
                    break;
                case 0xE8:		//用户信息项全部重复设置
                    PrintMessage("用户信息项全部重复设置!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("增加用户失败! {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserCount_Click(object sender, EventArgs e)
        {
            uint userCount = 0;
            int result = CHD.API.CHD805.ReadUserCount(PortId, NetId, out userCount);
            PrintExcuteResult(result, String.Format("读取用户数量成功! 用户数量: {0}", userCount), string.Format("读取用户数量失败! 错误代码: {0}", result));

        }



        /// <summary>
        /// 清空用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearUser_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.DelAllUser(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("删除设备所有用户成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("删除用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("删除用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 通过存储位置读取用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByPos_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToUInt16(txtInfoPosSave.Text);
            }
            catch
            {
                MessageBox.Show("请正确输入存储位置！");
                return;
            }
            CHD.API.SYSTEMTIME LmtTime;
            byte[] cardNo = new byte[12];
            byte[] userID = new byte[12];
            byte[] passwor = new byte[12];
            uint cardType = 0;
            int nRetValue = CHD.API.CHD805.ReadUserInfo(PortId, NetId, uint.Parse(txtInfoPosSave.Text), cardNo, userID, passwor, out LmtTime, out cardType);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功

                        PrintMessage(string.Format("查询用户信息成功! 卡号:{0} 用户ID:{1} 密码:{2} 有效期:{3}-{4}-{5} 卡类型:{6}", Encoding.Default.GetString(cardNo), Encoding.Default.GetString(userID),
                           Encoding.Default.GetString(passwor), LmtTime.wYear, LmtTime.wMonth, LmtTime.wDay, cardType));
                        break;
                    }
                case 0x07:		//无权限
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 通过用户ID读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByUId_Click(object sender, EventArgs e)
        {
            if (txtInfoUserId.Text == null || txtInfoUserId.Text.Length <= 0)
            {
                MessageBox.Show("请输入用户ID");
                return;
            }
            CHD.API.SYSTEMTIME LmtTime;
            StringBuilder cardNo = new StringBuilder();
            StringBuilder passwor = new StringBuilder();
            uint cardType = 0;
            int nRetValue = CHD.API.CHD805.ReadUserByUserID(PortId, NetId, txtInfoUserId.Text, cardNo, passwor, out LmtTime, out cardType);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功

                        PrintMessage(string.Format("查询用户信息成功! 卡号:{0} 用户ID:{1} 密码:{2} 有效期:{3}-{4}-{5} 卡类型:{6}", cardNo, txtInfoUserId.Text,
                            passwor, LmtTime.wYear, LmtTime.wMonth, LmtTime.wDay, cardType));
                        break;
                    }
                case 0x07:		//无权限
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 根据用户ID删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUserByUid_Click(object sender, EventArgs e)
        {
            if (txtInfoUserId.Text == null || txtInfoUserId.Text.Length <= 0)
            {
                MessageBox.Show("请输入用户ID");
                return;
            }
            int nRetValue = CHD.API.CHD805.DelUserByUserID(PortId, NetId, txtInfoUserId.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(string.Format("删除用户ID:{0} 成功! ", txtInfoUserId.Text));
                    break;
                case 0x07:		//无权限
                    PrintMessage("删除用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("删除用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 根据卡号读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByCardNo_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Text == null || txtCardNo.Text.Length <= 0)
            {
                MessageBox.Show("请输入卡号！");
                return;
            }
            CHD.API.SYSTEMTIME LmtTime;
            StringBuilder userId = new StringBuilder();
            StringBuilder passwor = new StringBuilder();
            uint cardType = 0;
            int nRetValue = CHD.API.CHD805.ReadUserByCardNo(PortId, NetId, txtCardNo.Text, userId, passwor, out LmtTime, out cardType);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功

                        PrintMessage(string.Format("查询用户信息成功! 卡号:{0} 用户ID:{1} 密码:{2} 有效期:{3}-{4}-{5} 卡类型:{6}", txtCardNo.Text, userId,
                            passwor, LmtTime.wYear, LmtTime.wMonth, LmtTime.wDay, cardType));
                        break;
                    }
                case 0x07:		//无权限
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 根据卡号删除用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUserByCardNo_Click(object sender, EventArgs e)
        {
            if (txtCardNo.Text == null || txtCardNo.Text.Length <= 0)
            {
                MessageBox.Show("请输入用户ID");
                return;
            }
            int nRetValue = CHD.API.CHD805.DelUserByCardNo(PortId, NetId, txtCardNo.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(string.Format("删除卡号:{0} 成功! ", txtInfoUserId.Text));
                    break;
                case 0x07:		//无权限
                    PrintMessage("删除用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("删除用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("删除用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }

        #endregion



        #region 门禁参数设置1

        /// <summary>
        /// 设置门锁继电器执行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetRelayTime_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorRelayDelay(PortId, NetId, uint.Parse(txtRelayTime.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置门锁继电器执行实行成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置门锁继电器执行实行失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置门锁继电器执行实行失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置开门后等待进入时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOpenTime_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorOpenDelay(PortId, NetId, uint.Parse(txtOpenTime.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置开门后等待进入时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置开门后等待进入时间失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置开门后等待进入时间失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 设置红外报警确认时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetInfraTime_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorAlarmConfirm(PortId, NetId, uint.Parse(txtInfraTime.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置红外报警确认时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置红外报警确认时间失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(string.Format("设置红外报警确认时间失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置安防报警驱动延时时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSetAlarmTime_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorAlarmDelay(PortId, NetId, uint.Parse(txtAlarmTime.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置安防报警驱动延时时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置安防报警驱动延时时间失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置安防报警驱动延时时间失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置红外监控延时时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLeaveDelay_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorLeaveDelay(PortId, NetId, uint.Parse(txtLeaveDelay.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置红外监控延时时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置红外监控延时时间失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置红外监控延时时间失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCurrent_Click(object sender, EventArgs e)
        {
            uint param, RelayTime, OpenTime, InfraTime, AlarmTime, LeaveDelay;

            int result = CHD.API.CHD805.ReadDoorParam(PortId, NetId, out param, out RelayTime, out OpenTime, out InfraTime, out AlarmTime, out LeaveDelay);
            if (result == 0)
            {
                PrintMessage("读取设备当前设置成功!");
                txtRelayTime.Text = RelayTime.ToString();
                txtOpenTime.Text = OpenTime.ToString();
                txtAlarmTime.Text = AlarmTime.ToString();
                txtInfraTime.Text = InfraTime.ToString();
            }
            else
            {
                PrintMessage(string.Format("读取设备当前设置失败! 错误代码:{0}", result));
            }
        }



        /// <summary>
        /// 设置感应器特性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetParam1_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorCtrl1(PortId, NetId, GetParam(sender));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置门磁、红外等感应器的特性成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置门磁、红外等感应器的特性失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(string.Format("设置门磁、红外等感应器的特性失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 远程监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoteMonit_Click(object sender, EventArgs e)
        {
            uint n = 0, n1 = 0;
            int nRetValue = CHD.API.CHD805.ReadDoorState(PortId, NetId, out n, out n1);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("远程监控成功! SM的工作状态:{0}  SM监控线路的状态：{1}", n, n1));
                    break;
                case 0x07:		//无权限
                    PrintMessage("远程监控失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("远程监控失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 单一放行，带操作员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoteOpenDoorWithOpt_Click(object sender, EventArgs e)
        {
            if (txtAdminCode.Text.Length != 10)
            {
                MessageBox.Show("操作员代码有误，正确的操作员代码为10位");
                return;
            }
            int nRetValue = CHD.API.CHD805.RemoteOpenDoor(PortId, NetId, txtAdminCode.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("单一放行成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("单一放行失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("单一放行失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 单一放行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoteOpenDoor_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.RemoteOpenDoor(PortId, NetId, null);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("单一放行成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("单一放行失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("单一放行失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置系统使用的感应卡种类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSysCardType_Click(object sender, EventArgs e)
        {
            uint n = 0;
            if (cmbCardType.SelectedIndex == 0)
            {
                n = 26;
            }
            else if (cmbCardType.SelectedIndex == 1)
            {
                n = 36;
            }
            else if (cmbCardType.SelectedIndex == 2)
            {
                n = 44;
            }
            else if (cmbCardType.SelectedIndex == 3)
            {
                n = 34;
            }

            int nRetValue = CHD.API.CHD805.SetRFCardType(PortId, NetId, n);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设定系统使用的感应卡种类 成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设定系统使用的感应卡种类 失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设定系统使用的感应卡种类 失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置卡片编号的获取方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetCardGenarCardNoType_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetRFCard(PortId, NetId, (uint)cmbCardNoMethod.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置系统支持维根感应头的种类及卡片编号的获取方法成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置系统支持维根感应头的种类及卡片编号的获取方法失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置系统支持维根感应头的种类及卡片编号的获取方法失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }


        #endregion



        #region 门禁参数设置2
        private void btnReadParam_Click(object sender, EventArgs e)
        {
            uint ctrParm, a, b, k, d, f;
            int nRetValue = CHD.API.CHD805.ReadDoorParam(PortId, NetId, out ctrParm, out a, out b, out k, out d, out f);
            if (nRetValue == 0)
            {
                PrintMessage("读取设备当前设置成功!");
                string s = Convert.ToString(ctrParm, 2);
                int fillCount = 8 - s.Length;
                for (int i = 1; i <= fillCount; i++)
                {
                    s = "0" + s;
                }
                Button btn = sender as Button;
                StringBuilder sb = new StringBuilder();
                var controls = from Control c in btn.Parent.Controls
                               where c is GroupBox
                               orderby c.Tag
                               select c;
                foreach (var ctr in controls)
                {
                    foreach (Control rad in ctr.Controls)
                    {
                        if (rad is RadioButton)
                        {
                            RadioButton radioBtn = rad as RadioButton;
                            if (radioBtn.Text == "启用")
                            {
                                radioBtn.Checked = s.Substring(Convert.ToInt32(ctr.Tag), 1) == "1" ? true : false;
                            }
                            else
                            {
                                radioBtn.Checked = s.Substring(Convert.ToInt32(ctr.Tag), 1) == "0" ? true : false;
                            }
                        }
                    }
                }
            }
            else
            {
                PrintMessage(string.Format("读取设备当前设置失败! 错误代码: {0}", nRetValue));
            }
        }



        /// <summary>
        /// 设置门开关监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetWatchDoor_Click(object sender, EventArgs e)
        {
            int rad = radWatchDoor.Checked ? 1 : 0;
            String txt = radWatchDoor.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetWatchDoor(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(string.Format("{0}监视门开关感应器状态成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}监视门开关感应器状态失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}监视门开关感应器状态失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置红外监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWatchInfrared_Click(object sender, EventArgs e)
        {
            int rad = radWatchInfrared.Checked ? 1 : 0;
            String txt = radWatchInfrared.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetWatchInfrared(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}监视红外感应器状态成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}监视红外感应器状态失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}监视红外感应器状态失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置断电/加电自动上锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLockType_Click(object sender, EventArgs e)
        {
            int rad = radLockType.Checked ? 1 : 0;
            String txt = radLockType.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetLockType(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}断电、加电自动锁门成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}断电、加电自动锁门失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}断电、加电自动锁门失败! 错误代码: %d", txt, nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 设置手动开门按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualKey_Click(object sender, EventArgs e)
        {
            int rad = radManualKey.Checked ? 1 : 0;
            String txt = radManualKey.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetManualKey(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}开门状态时开关感应器位开路状态成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}开门状态时开关感应器位开路状态失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}开门状态时开关感应器位开路状态失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置门状态开关感应器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLockOpenType_Click(object sender, EventArgs e)
        {

            int rad = radLockOpenType.Checked ? 1 : 0;
            String txt = radLockOpenType.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetLockOpenType(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}报警状态时红外感应器开路状态成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}报警状态时红外感应器开路状态失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}报警状态时红外感应器开路状态失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置报警状态红外感应器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfraredType_Click(object sender, EventArgs e)
        {
            int rad = radInfraredType.Checked ? 1 : 0;
            String txt = radInfraredType.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetInfraredType(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}刷卡后需要密码成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}刷卡后需要密码失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}刷卡后需要密码失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 设置刷卡+密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithPassword_Click(object sender, EventArgs e)
        {
            int rad = radWithPassword.Checked ? 1 : 0;
            String txt = radWithPassword.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetWithPassword(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}键盘<ENT>键成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}键盘<ENT>键失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}键盘<ENT>键失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 设置ENT键开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeyEnt_Click(object sender, EventArgs e)
        {
            int rad = radKeyEnt.Checked ? 1 : 0;
            String txt = radKeyEnt.Checked ? "启用" : "禁用";
            int nRetValue = CHD.API.CHD805.SetKeyEnt(PortId, NetId, (uint)rad);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("{0}手动开门按钮成功!", txt));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("{0}手动开门按钮失败! 错误提示: 无权限!", txt));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("{0}手动开门按钮失败! 错误代码: {1}", txt, nRetValue));
                    break;
            }

        }

        #endregion



        #region 门禁参数设置3
        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetParam3_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorCtrl2(PortId, NetId, GetParam(sender));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置门禁控制器参数成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置门禁控制器参数失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置门禁控制器参数失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取门禁参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadParam3_Click(object sender, EventArgs e)
        {
            uint nCtrlParam = 0, nu;

            int nRetValue = CHD.API.CHD805.ReadDoorCtrl(PortId, NetId, out nCtrlParam, out  nu);
            if (nRetValue == 0)
            {
                string s = Convert.ToString(nCtrlParam, 2);
                int fillCount = 8 - s.Length;
                for (int i = 1; i <= fillCount; i++)
                {
                    s = "0" + s;
                }
                Button btn = sender as Button;
                StringBuilder sb = new StringBuilder();
                var controls = from Control c in btn.Parent.Controls
                               where c is GroupBox
                               orderby c.Tag
                               select c;
                foreach (var ctr in controls)
                {
                    foreach (Control rad in ctr.Controls)
                    {
                        if (rad is RadioButton)
                        {
                            RadioButton radioBtn = rad as RadioButton;
                            if (radioBtn.Text == "启用")
                            {
                                radioBtn.Checked = s.Substring(Convert.ToInt32(ctr.Tag), 1) == "1" ? true : false;
                            }
                        }
                    }
                }

                PrintMessage("读取设备当前设置成功!");

            }
            else
            {
                PrintMessage(String.Format("读取设备当前设置失败! 错误代码: {0}", nRetValue));
            }
        }

        #endregion



        #region 门禁参数设置4
        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetParam4_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetDoorCtrl3(PortId, NetId, GetParam(sender));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置门禁控制器参数成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置门禁控制器参数失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置门禁控制器参数失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 读取门禁参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadParam4_Click(object sender, EventArgs e)
        {
            uint nCtrlParam = 0, nu;

            int nRetValue = CHD.API.CHD805.ReadDoorCtrl(PortId, NetId, out nu, out  nCtrlParam);
            if (nRetValue == 0)
            {
                string s = Convert.ToString(nCtrlParam, 2);
                int fillCount = 8 - s.Length;
                for (int i = 1; i <= fillCount; i++)
                {
                    s = "0" + s;
                }
                Button btn = sender as Button;
                StringBuilder sb = new StringBuilder();
                var controls = from Control c in btn.Parent.Controls
                               where c is GroupBox
                               orderby c.Tag
                               select c;
                foreach (var ctr in controls)
                {
                    foreach (Control rad in ctr.Controls)
                    {
                        if (rad is RadioButton)
                        {
                            RadioButton radioBtn = rad as RadioButton;
                            if (radioBtn.Text == "启用")
                            {
                                radioBtn.Checked = s.Substring(Convert.ToInt32(ctr.Tag), 1) == "1" ? true : false;
                            }
                        }
                    }
                }

                PrintMessage("读取设备当前设置成功!");

            }
            else
            {
                PrintMessage(String.Format("读取设备当前设置失败! 错误代码: {0}", nRetValue));
            }

        }

        #endregion



        #region 记录管理

        /// <summary>
        /// 初始化设备记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.InitRec(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(("初始化记录成功! "));
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage(("初始化记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage(String.Format("初始化记录失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取设备记录区状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryRecStatu_Click(object sender, EventArgs e)
        {
            short nBottom = 0, nSaveP = 0, nLoadP = 0, nMF = 0, nMaxLen = 0;

            int nRetValue = CHD.API.CHD805.ReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMF, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("读取记录区状态成功! Bottom={0} nSaveP={1} nLoadP={2} nMF={3} nMaxLen={4}", nBottom, nSaveP, nLoadP, nMF, nMaxLen));
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    break;
                default:		//Failed
                    PrintMessage(String.Format("读取记录失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 恢复记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecovery_Click(object sender, EventArgs e)
        {
            short m_nRecCount = 0;
            try
            {
                m_nRecCount = Convert.ToInt16(txtRecovery.Text);
            }
            catch { MessageBox.Show("请正确输入恢复记录数！"); return; }
            short nBottom = 0, nSaveP = 0, nLoadP = 0, nMF = 0, nMaxLen = 0;
            int nRetValue = CHD.API.CHD805.ReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMF, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    {
                        if (nMF == 0x00)
                        {
                            if (m_nRecCount >= nLoadP)
                            {
                                m_nRecCount = (short)(nLoadP - nBottom);
                                nLoadP = nBottom;
                            }
                            else
                            {
                                nLoadP = (short)(nLoadP - m_nRecCount);
                            }
                        }
                        else if (nMF == 0x80)
                        {
                            if (m_nRecCount > (nLoadP - nSaveP))
                            {
                                m_nRecCount = (short)(nLoadP - nSaveP);
                                nLoadP = (short)(nSaveP + 1);
                            }
                            else
                            {
                                nLoadP = (short)(nLoadP - m_nRecCount);
                            }
                        }
                        else if (nMF == 0x81)
                        {
                            PrintMessage("恢复记录失败! 提示: 所有记录已经被新记录覆盖! ");
                            break;
                        }
                        else
                        {
                            PrintMessage(String.Format("恢复记录失败! 提示: 未知记录标识:{0}!", nMF));
                            break;
                        }
                        nRetValue = CHD.API.CHD805.SetRecReadPoint(PortId, NetId, (uint)nLoadP, (uint)nMF);
                        switch (nRetValue)
                        {
                            case 0x00:		//Success
                                PrintMessage(String.Format("恢复记录成功! 共恢复:{0} 条记录", m_nRecCount));
                                break;
                            case 0x07:		//Failed
                                PrintMessage("恢复记录失败! 提示: 无权限!");
                                break;
                            default:		//Failed
                                PrintMessage(String.Format("恢复记录失败! 错误代码: {0}", nRetValue));
                                break;
                        }
                    }
                    break;
                default:		//Failed
                    PrintMessage(String.Format("恢复记录失败! 提示: 获取记录状态失败! 错误代码:{0}", nRetValue));
                    break;
            }
        }

        private void btnReadNewRec_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME time = new CHD.API.SYSTEMTIME();
            StringBuilder sb = new StringBuilder();
            uint nRecState = 0, nRecRemark = 0;
            int nRetValue = CHD.API.CHD805.ReadNewRec(PortId, NetId, sb, ref time, ref nRecState, ref nRecRemark);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(FormatLog(sb.ToString(), CHD.Common.ParasTime(time), nRecState, nRecRemark));
                    break;
                case 0xE4:		//Failed
                    PrintMessage("设备内无记录!");
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    break;
                default:		//Failed
                    PrintMessage(String.Format("读取记录失败! 错误代码: {0}", nRetValue));
                    break;
            }
            // MessageBox.Show(DateTime.Now.ToString());
        }



        /// <summary>
        /// 按位置读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByPos_Click(object sender, EventArgs e)
        {
            uint m_nPos = 0;
            try
            {
                m_nPos = Convert.ToUInt16(txtRecPos.Text);
            }
            catch
            {
                MessageBox.Show("请正确输入记录序号");
                return;
            }
            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[15];
            uint nRecState = 0, nRecRemark = 0;
            int nRetValue = CHD.API.CHD805.ReadRecByPoint(PortId, NetId, m_nPos, szRecSource, out RecTime, out nRecState, out nRecRemark);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("读取记录序号:[{0}]成功! {1}", m_nPos, FormatLog(Encoding.Default.GetString(szRecSource), CHD.Common.ParasTime(RecTime), nRecState, nRecRemark)));
                    break;
                case 0xE4:		//Failed
                    PrintMessage("设备内无记录!");
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    break;
                default:		//Failed
                    PrintMessage(String.Format("读取记录失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }

        private void btnReadRecByOrder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
            rightMenu.Items.Add("读取第一条", null, (obj, ee) => { ReadOneRecord(); });
            rightMenu.Items.Add("读取全部", null, (obj, ee) => { while (ReadOneRecord()) { System.Threading.Thread.Sleep(100); } });
            rightMenu.Show(Cursor.Position);
        }

        private void btnReadRec_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
            rightMenu.Items.Add("读取第一条", null, (obj, ee) => { ReadRecordWithPoint(); });
            rightMenu.Items.Add("读取全部", null, (obj, ee) => { while (ReadRecordWithPoint ()) { System.Threading.Thread.Sleep(100); } });
            rightMenu.Show(Cursor.Position);
        }


        private bool ReadOneRecord()
        {

            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[32];
            uint nRecState = 0, nRecRemark = 0;
            bool nRetFlag = false;
            int nRetValue = CHD.API.CHD805.ReadOneRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(FormatLog(Encoding.Default.GetString(szRecSource).Trim(), CHD.Common.ParasTime(RecTime), nRecState, nRecRemark));
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




        private bool ReadRecordWithPoint()
        {

            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[32];
            uint nRecPos = 0, nRecState = 0, nRecRemark = 0;
            bool nRetFlag = false;

            int nRetValue = CHD.API.CHD805.ReadRecWithPoint(PortId, NetId, out nRecPos, szRecSource, out RecTime, out nRecState, out nRecRemark);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("记录序号:{0} {1}", nRecPos, FormatLog(Encoding.Default.GetString(szRecSource).Trim(), CHD.Common.ParasTime(RecTime), nRecState, nRecRemark)));
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




        #endregion



        #region 门禁时段1
        /// <summary>
        /// 初始化时段列表
        /// </summary>
        private void InitDataGrid()
        {
            dgWorkTime.Rows.Add(new object[] { "工作日一", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWorkTime.Rows.Add(new object[] { "工作日二", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWorkTime.Rows.Add(new object[] { "工作日三", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWorkTime.Rows.Add(new object[] { "工作日四", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWorkTime.Rows.Add(new object[] { "非工作日", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });

            dgWeekTime.Rows.Add(new object[] { "星期一", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期二", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期三", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期四", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期五", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期六", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgWeekTime.Rows.Add(new object[] { "星期日", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });

            dgSpeciaTime.Rows.Add(new object[] { "门常开", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgSpeciaTime.Rows.Add(new object[] { "门常闭", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgSpeciaTime.Rows.Add(new object[] { "刷卡加密验证", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            dgSpeciaTime.Rows.Add(new object[] { "门磁红外监控", "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });

        }





        /// <summary>
        /// 工作时段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgWorkTime_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgWorkTime.Rows[e.RowIndex].Selected = true;
                System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add(String.Format("读取[{0}]时段", dgWorkTime.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { ReadWorkTime(e.RowIndex); });
                if (e.RowIndex != 4)
                {
                    rightMenu.Items.Add("读取所有工作日时段", null, (obj, ee) =>
                    {
                        for (int index = 0; index < 4; ++index)
                        {
                            ReadWorkTime(index);
                        }
                    });
                }
                rightMenu.Items.Add("-");
                rightMenu.Items.Add(String.Format("设置[{0}]时段", dgWorkTime.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { SetWorkTime(e.RowIndex); });
                if (e.RowIndex != 4)
                {
                    rightMenu.Items.Add("设置所有工作日时段", null, (obj, ee) => { for (int index = 0; index < 4; ++index) SetWorkTime(index); });
                }
                rightMenu.Show(Cursor.Position);
            }
        }



        /// <summary>
        /// 星期时段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgWeekTime_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgWeekTime.Rows[e.RowIndex].Selected = true;
                System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add(String.Format("读取[{0}]时段", dgWeekTime.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { ReadWeekTime(e.RowIndex); });
                rightMenu.Items.Add("读取所有星期时段", null, (obj, ee) => { for (int nWeek = 0; nWeek < 7; ++nWeek) ReadWeekTime(nWeek); });
                rightMenu.Items.Add("-");
                rightMenu.Items.Add(String.Format("设置[{0}]时段", dgWeekTime.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { SetWeekTime(e.RowIndex); });
                rightMenu.Items.Add("设置所有星期时段", null, (obj, ee) => { for (int nWeek = 0; nWeek < 7; ++nWeek) SetWeekTime(nWeek); });
                rightMenu.Show(Cursor.Position);
            }
        }



        /// <summary>
        /// 读取工作/非工作日时段
        /// </summary>
        /// <param name="index"></param>
        private void ReadWorkTime(int index)
        {
            byte[] sb = new byte[120];
            int result = 0;
            if (index != 4)
            {
                result = CHD.API.CHD805.ReadWorkLmtTime(PortId, NetId, (uint)(index + 1), sb);
            }
            else
            {
                result = CHD.API.CHD805.ReadUnWorkLmtTime(PortId, NetId, sb);
            }
            if (result == 0)
            {
                String returnStr = Encoding.Default.GetString(sb).Trim();
                String strSubItemText = "";
                for (int i = 1; i < 9; ++i)
                {
                    strSubItemText = returnStr.Substring(i * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                    strSubItemText += ":";
                    strSubItemText += returnStr.Substring(i * 4 - 4 + 2, 2);
                    dgWorkTime.Rows[index].Cells[i].Value = strSubItemText;
                    strSubItemText = "";
                }
                PrintMessage(String.Format("读取设备[{0}]时段成功，时段：{1}", dgWorkTime.Rows[index].Cells[0].Value, returnStr));
            }
            else
            {
                PrintMessage(String.Format("读取设备[{0}]时段失败! 错误代码: {1}", dgWorkTime.Rows[index].Cells[0].Value, result));
            }
        }



        /// <summary>
        /// 设置工作/非工作日时段
        /// </summary>
        /// <param name="index"></param>
        private void SetWorkTime(int index)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 9; ++i)
            {
                sb.Append(dgWorkTime.Rows[index].Cells[i].Value);
            }
            string timeStr = sb.ToString().Replace(":", "");
            int result = 0;
            if (index != 4)
            {
                result = CHD.API.CHD805.SetWorkLmtTime(PortId, NetId, (uint)(index + 1), timeStr);
            }
            else
            {
                result = CHD.API.CHD805.SetUnWorkLmtTime(PortId, NetId, timeStr);
            }
            PrintExcuteResult(result, String.Format("设置设备[{0}]时段成功! 时段:{1}", dgWorkTime.Rows[index].Cells[0].Value, timeStr), String.Format("设置设备[{0}]时段失败!", dgWorkTime.Rows[index].Cells[0].Value));
        }



        /// <summary>
        /// 读取星期时段
        /// </summary>
        /// <param name="weekIndex"></param>
        private void ReadWeekTime(int weekIndex)
        {
            byte[] sb = new byte[120];
            int result = CHD.API.CHD805.ReadWeekLmtTime(PortId, NetId, (uint)cmbWeekTime.SelectedIndex, (uint)(weekIndex + 1), sb);

            if (result == 0)
            {
                String returnStr = Encoding.Default.GetString(sb).Trim();
                String strSubItemText = "";
                for (int i = 1; i < 13; ++i)
                {
                    strSubItemText = returnStr.Substring(i * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                    strSubItemText += ":";
                    strSubItemText += returnStr.Substring(i * 4 - 4 + 2, 2);
                    dgWeekTime.Rows[weekIndex].Cells[i].Value = strSubItemText;
                    strSubItemText = "";
                }
                PrintMessage(String.Format("读取设备[{0}]时段成功，时段：{1}", dgWeekTime.Rows[weekIndex].Cells[0].Value, returnStr));
            }
            else
            {
                PrintMessage(String.Format("读取设备[{0}]时段失败! 错误代码: {1}", dgWeekTime.Rows[weekIndex].Cells[0].Value, result));
            }
        }



        /// <summary>
        /// 设置星期时段
        /// </summary>
        /// <param name="weekIndex"></param>
        private void SetWeekTime(int weekIndex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 13; ++i)
            {
                sb.Append(dgWeekTime.Rows[weekIndex].Cells[i].Value);
            }
            string timeStr = sb.ToString().Replace(":", "");
            int result = CHD.API.CHD805.SetWeekLmtTime(PortId, NetId, (uint)(cmbWeekTime.SelectedIndex), (uint)(weekIndex + 1), timeStr);
            PrintExcuteResult(result, String.Format("设置设备[{0}]时段成功! 时段:{1}", dgWeekTime.Rows[weekIndex].Cells[0].Value, timeStr), String.Format("设置设备[{0}]时段失败!", dgWeekTime.Rows[weekIndex].Cells[0].Value));
        }


        /// <summary>
        /// 验证时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgWorkTime_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = sender as DataGridView;
            String value = dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value != "FF:FF")
            {
                if (!ValidData(value))
                {
                    dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "FF:FF";
                }
            }

        }


        /// <summary>
        /// 辅助函数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool ValidData(String s)
        {
            String[] values = s.Split(':');
            if (values.Length < 2)
            {
                MessageBox.Show("请正确输入时间，格式为HH:MM");
                return false;
            }
            try
            {
                uint hh = Convert.ToUInt32(values[0]);
                uint mm = Convert.ToUInt32(values[1]);
                if (hh > 23)
                {
                    MessageBox.Show("时间的小时部分不能大于23，请重新输入！");
                    return false;
                }
                if (mm > 59)
                {
                    MessageBox.Show("时间的分钟部分不能大于59，请重新输入！");
                    return false;
                }

                return true;

            }
            catch
            {
                MessageBox.Show("请输入正确的时间数字");
                return false;
            }

        }


        #endregion


        #region 门禁时段2
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSpeciaTime_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dg = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                dg.Rows[e.RowIndex].Selected = true;
                System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add(String.Format("读取[{0}]时段", dg.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { ReadDoorLmtTime2(e.RowIndex); });
                rightMenu.Items.Add(String.Format("设置[{0}]时段", dg.Rows[e.RowIndex].Cells[0].Value), null, (obj, ee) => { SetDoorLmtTime2(e.RowIndex); });
                rightMenu.Show(Cursor.Position);
            }
        }




        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="index"></param>
        private void ReadDoorLmtTime2(int index)
        {
            int nRetValue = 0;
            byte[] sb = new byte[100];
            switch (index)
            {
                case 0:
                    nRetValue = CHD.API.CHD805.ReadOpenLmtTime(PortId, NetId, sb);
                    break;
                case 1:
                    nRetValue = CHD.API.CHD805.ReadCloseLmtTime(PortId, NetId, sb);
                    break;
                case 2:
                    nRetValue = CHD.API.CHD805.ReadPwdLmtTime(PortId, NetId, sb);
                    break;
                case 3:
                    nRetValue = CHD.API.CHD805.ReadWatchLmtTime(PortId, NetId, sb);
                    break;
            }
            switch (nRetValue)
            {
                case 0x00:		//Success
                    {
                        string strTemp = Encoding.Default.GetString(sb).Trim();
                        string strSubItemText = "";
                        for (int i = 1; i < 9; ++i)
                        {
                            strSubItemText = strTemp.Substring(i * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                            strSubItemText += ":";
                            strSubItemText += strTemp.Substring(i * 4 - 4 + 2, 2);
                            dgSpeciaTime.Rows[index].Cells[i].Value = strSubItemText;
                            strSubItemText = "";
                        }
                        PrintMessage(String.Format("读取设备[{0}]时段成功! 时段:{1}", dgSpeciaTime.Rows[index].Cells[0].Value, strTemp));
                        break;
                    }
                default:		//Failed
                    PrintMessage(String.Format("读取设备[{0}]时段失败! 错误代码: {1}", dgSpeciaTime.Rows[index].Cells[0].Value, nRetValue));
                    break;
            }
        }




        /// <summary>
        /// 设置时段
        /// </summary>
        /// <param name="index"></param>
        private void SetDoorLmtTime2(int index)
        {
            String strTemp = "";
            for (int i = 1; i < 9; ++i)
            {
                strTemp += dgSpeciaTime.Rows[index].Cells[i].Value;
            }
            string timeStr = strTemp.Replace(":", "");
            int result = 0;
            switch (index)
            {
                case 0:
                    result = CHD.API.CHD805.SetOpenLmtTime(PortId, NetId, timeStr);
                    break;
                case 1:
                    result = CHD.API.CHD805.SetCloseLmtTime(PortId, NetId, timeStr);
                    break;
                case 2:
                    result = CHD.API.CHD805.SetPwdLmtTime(PortId, NetId, timeStr);
                    break;
                case 3:
                    result = CHD.API.CHD805.SetWatchLmtTime(PortId, NetId, timeStr);
                    break;
            }
            PrintExcuteResult(result, "设置设备时段成功！", String.Format("设置设备[{0}]时段失败!", dgSpeciaTime.Rows[index].Cells[0].Value));

        }



        /// <summary>
        /// 设置每周休息日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetWeekend_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.SetWeekend(PortId, NetId, (uint)(cmbSetWeek1.SelectedIndex + 1), (uint)(cmbSetWeek2.SelectedIndex + 1));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("设置每周休息日[ 星期:{0}、星期:{1} ]成功! ", cmbSetWeek1.SelectedIndex + 1, cmbSetWeek2.SelectedIndex + 1));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("设置每周休息日[ 星期:{0}、星期:{1} ]失败! 错误提示: 无权限!", cmbSetWeek1.SelectedIndex + 1, cmbSetWeek2.SelectedIndex + 1));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("设置每周休息日[ 星期:{0}、星期:{1} ]失败! 错误代码: {2}", cmbSetWeek1.SelectedIndex + 1, cmbSetWeek2.SelectedIndex + 1, nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 读取设备休息日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUnworkWeek_Click(object sender, EventArgs e)
        {
            uint nWeek1 = 0, nWeek2 = 0;
            int nRetValue = CHD.API.CHD805.ReadWeekend(PortId, NetId, out nWeek1, out nWeek2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("读取每周休息日[ 星期:{0}、星期:{1} ]成功! ", nWeek1, nWeek2));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("读取每周休息日[ 星期:{0}、星期:{1} ]失败! 错误提示: 无权限!", nWeek1, nWeek2));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("读取每周休息日[ 星期:{0}、星期:{1} ]失败! 错误代码:{2}", nWeek1, nWeek2, nRetValue));
                    break;
            }
        }

        /// <summary>
        /// 增加节日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSpecial_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.AddHoliday(PortId, NetId, (uint)(dpSpeciaDate.Value.Month), (uint)(dpSpeciaDate.Value.Day));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("增加特殊日期(节、假日)[ {0}月{1}日 ]成功! ", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("增加特殊日期(节、假日)[ {0}月{1}日 ]失败! 错误提示: 无权限!", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day));
                    break;
                default:		//其他值表示失败 
                    PrintMessage(String.Format("增加特殊日期(节、假日)[ {0}月{1}日 ]失败! 错误代码: {2}", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day, nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 删除节日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSpecialDate_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.DelHoliday(PortId, NetId, (uint)(dpSpeciaDate.Value.Month), (uint)(dpSpeciaDate.Value.Day));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("删除殊日期(节、假日)[ {0}月{1}日 ]成功! ", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day));
                    break;
                case 0x07:		//无权限             
                    PrintMessage(String.Format("删除殊日期(节、假日)[ {0}月{1}日 ]失败! 错误提示: 无权限!", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day));
                    break;
                case 0xE4:		//无该项              
                    PrintMessage(String.Format("删除殊日期(节、假日)[ {0}月{1}日 ]失败! 错误提示: 未设置该日期!", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day));
                    break;
                default:		//其他值表示失败     
                    PrintMessage(String.Format("删除殊日期(节、假日)[ {0}月{1}日 ]失败! 错误代码: {2}", dpSpeciaDate.Value.Month, dpSpeciaDate.Value.Day, nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 删除全部节假日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllSpecialDate_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.DelAllHoliday(PortId, NetId);//来至源码
            //int nRetValue = CHD.API.CHD805.DelHoliday(PortId, NetId);//来至文档
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("清除设备所有特殊日期成功! ");
                    break;
                case 0x07:		//无权限
                    PrintMessage("清除设备所有特殊日期失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("清除设备所有特殊日期失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取所有节假日
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSpecialDate_Click(object sender, EventArgs e)
        {
            uint nHolidaysCoun = 0;
            StringBuilder sb = new StringBuilder();
            int nRetValue = CHD.API.CHD805.ReadHolidays(PortId, NetId, out nHolidaysCoun, sb);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    {
                        string strTemp = sb.ToString();
                        String strOutText = "";
                        for (int i = 0; i < nHolidaysCoun; ++i)
                        {
                            strOutText += strTemp.Substring(i * 4, 2);
                            strOutText += "月";
                            strOutText += strTemp.Substring(i * 4 + 2, 2);
                            strOutText += "日;";
                        }
                        PrintMessage(String.Format("读取特殊日期成功! 特殊日期数:{0}: {1}", nHolidaysCoun, strOutText));
                        break;
                    }
                default:		//Failed
                    PrintMessage(String.Format("读取特殊日期失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }

        /// <summary>
        /// 远程常开门(开门后延时一段时间关门)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpecialOpenDoor_Click(object sender, EventArgs e)
        {
            uint time = 0;
            try
            {
                time = Convert.ToUInt16(txtSpecialKeep.Text);
            }
            catch
            {
                MessageBox.Show("开门后保持分钟数");
                return;
            }
            int nRetValue = CHD.API.CHD805.AlwaysOpenDoor(PortId, NetId, time);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("开门成功! 保持开门状态时间:{0}分钟", time));
                    break;
                case 0x07:		//无权限
                    PrintMessage("开门失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("开门失败! 错误代码:{0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 解除常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSpeOpenDoor_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.AlwaysOpenDoor(PortId, NetId, 0);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("解除常开门成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("解除常开门失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("解除常开门失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 设置常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpecialColose_Click(object sender, EventArgs e)
        {
            uint time = 0;
            try
            {
                time = Convert.ToUInt16(txtSpecKeepClose.Text);
            }
            catch
            {
                MessageBox.Show("开门后保持分钟数");
                return;
            }
            int nRetValue = CHD.API.CHD805.AlwaysCloseDoor(PortId, NetId, time);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("关门成功! 保持关门状态时间:{0}分钟", txtSpecKeepClose.Text));
                    break;
                case 0x07:		//无权限
                    PrintMessage("关门失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("关门失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 解除常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpectialCancel_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD805.AlwaysCloseDoor(PortId, NetId, 0);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("解除常闭门成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("解除常闭门失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("解除常闭门失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }

        #endregion



    }
}
