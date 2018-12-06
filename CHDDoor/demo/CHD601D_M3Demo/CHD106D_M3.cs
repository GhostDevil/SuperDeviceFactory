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

namespace CHD106D_M3Demo
{
    public partial class CHD106D_M3 : Form
    {
        private int portId;
        private bool nonNumberEntered;
        public CHD106D_M3()
        {
            InitializeComponent();
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

        public CHD106D_M3(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数
        private void InitCmp()
        {
            for (int i = 1; i <= 255; i++)
            {
                cmbSection.Items.Add(i.ToString());
                cmbTimeGroup.Items.Add(i);
                cmbUserCardType.Items.Add(String.Format("{0}类卡", i));
            }
            cmbSection.SelectedIndex = 0;
            cmbBautRate.SelectedIndex = 0;
            cmbConsumeMode.SelectedIndex = 0;
            cmbOperatorMode.SelectedIndex = 0;
            cmbNameList.SelectedIndex = 0;
            cmbCardConsumeLimited.SelectedIndex = 0;
            cmbCardTypeLimited.SelectedIndex = 0;
            cmbConsumeLimited.SelectedIndex = 0;
            cmbIndex.SelectedIndex = 0;

            cmbTimeGroup.SelectedIndex = 0;
            cmbCardType.SelectedIndex = 0;
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
        #endregion






        #region 设置参数


        private void tcPrice_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        nonNumberEntered = true;
                    }
                }
            }
        }

        private void tcPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }




        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOn_Click(object sender, EventArgs e)
        {
            if (txtPwd.Text.Length != 10)
            {
                MessageBox.Show("密码长度不正确，长度应为10");
                return;
            }
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_LinkOn(PortId, NetId, txtPwd.Text), "确认权限成功", "确认权限失败! ");

        }



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOff_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_LinkOff(PortId, NetId), "访问权限取消成功", "访问权限取消失败!");
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtChangePwd_Click(object sender, EventArgs e)
        {

            if (txtPwd.Text.Length != 10)
            {
                MessageBox.Show("密码长度不正确，长度应为10");
                return;
            }
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetDevPwd(PortId, NetId, txtPwd.Text), "修改密码成功", "修改密码失败! ");
        }



        /// <summary>
        /// 设置设备日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME time = CHD.Common.ParasTime(DateTime.Now);
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetDateTime(PortId, NetId, ref time), "设置时间成功", "设置时间失败!");
        }



        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_InitRecorde(PortId, NetId), "初始化成功", "初始化失败!");
        }




        private byte ByteToBCD(byte cOneByte, int nHLPos)
        {
            byte cHByte = 0x00, cLByte = 0x00;
            if (nHLPos == 1)
            {
                cLByte = (byte)(cOneByte & 0x0f);
                if ((cLByte >= 0) && (cLByte <= 9))
                {
                    return (byte)(48 + cLByte);
                }
                else
                {
                    return (byte)(55 + cLByte);
                }
            }
            else
            {
                cHByte = (byte)(cOneByte >> 4);
                if (cHByte >= 0 && cHByte <= 9)
                {
                    return (byte)(48 + cHByte);
                }
                else
                {
                    return (byte)(55 + cHByte);
                }
            }
        }


        /// <summary>
        /// 设置商品条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetProductNo_Click(object sender, EventArgs e)
        {
            if (tcCode.Text.Length == 0 || tcPrice.Text.Length == 0 || tcName.Text.Length == 0)
            {
                MessageBox.Show("请将商品信息填写完整");
                return;
            }

            byte[] Code = Encoding.Default.GetBytes(tcCode.Text);

            byte[] PName = Encoding.Default.GetBytes(tcName.Text);

            //*****************************************************************//
            string strPrice = (Convert.ToInt16(tcPrice.Text)*100).ToString("X6");
            string[] sTemp = new string[4];
            sTemp[0] = strPrice;
            sTemp[1] = sTemp[0].Substring(4, 2);
            sTemp[2] = sTemp[0].Substring(2, 2);
            sTemp[3] = sTemp[0].Substring(0, 2);
            string s = sTemp[1] + sTemp[2] + sTemp[3];
            byte[] Price = Encoding.Default.GetBytes(s);

            //*****************************************************************//
            byte[] cName = new byte[260];
            byte[] dd = Encoding.Default.GetBytes(tcName.Text);
            for (int i = 0; i < dd.Length; i++)
            {
                cName[i] = dd[i];
            }
            int nLen = dd.Length;
            for (int i = 0; i < nLen; ++i)
            {
                cName[23 - i * 2] = ByteToBCD(cName[nLen - i - 1], 1);
                cName[23 - i * 2 - 1] = ByteToBCD(cName[nLen - i - 1], 2);
            }

            for (int i = 0; i < (23 - nLen * 2 + 1) / 2; i++)
            {
                cName[i * 2] = 0x32;
                cName[i * 2 + 1] = 0x30;
            }

            int nRt = CHD.API.CHD601D_M3.Consume_EditCode(PortId, NetId, Code, Convert.ToDouble(tcPrice.Text), cName);
            if (nRt == 0)
            {
                PrintMessage("设置商品编码成功");
            }
            else
            {
                PrintMessage("设置商品编码失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 删除所有商品条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllPdctNo_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_DeleteAllCode(PortId, NetId), "删除所有商品编码成功", "删除所有商品编码失败!");
        }



        /// <summary>
        /// 移动记录指针
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMovePointer_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_MovePointer(PortId, NetId, int.Parse(txtPointerPos.Text)), "移动读记录指针成功", "移动读记录指针失败!");
        }



        /// <summary>
        /// 设置就餐时间段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            DateTimePicker[] dps = (from Control n in btn.Parent.Controls
                                    where n is DateTimePicker
                                    orderby n.Tag ascending
                                    select (DateTimePicker)n).ToArray();
            CHD.API.SYSTEMTIME[] st = new CHD.API.SYSTEMTIME[16];
            for (int i = 0; i < dps.Length; i++)
            {
                st[i] = CHD.Common.ParasTime(dps[i].Value);
            }
            int nRt = CHD.API.CHD601D_M3.Consume_EatPeriod(PortId, NetId, cmbTimeGroup.SelectedIndex + 1, ref st[0], ref st[1], ref st[2], ref st[3], ref st[4], ref st[5], ref st[6],
        ref st[7], ref st[8], ref st[9], ref st[10], ref st[11], ref st[12], ref st[13], ref st[14], ref st[15]);
            if (nRt == 0)
            {
                PrintMessage("就餐时间段设定成功");
            }
            else
            {
                PrintMessage("就餐时间段设定失败! 错误码: " + nRt);
            }

        }




        /// <summary>
        /// 分段定值金额设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string[] cMoney = new string[8];
            TextBox[] txs = (from Control n in btn.Parent.Controls
                             where n is TextBox
                             orderby n.Tag ascending
                             select (TextBox)n).ToArray();
            for (int i = 0; i < txs.Length; i++)
            {
                if (txs[i].Text.Length == 0)
                {
                    MessageBox.Show("没有输入金额");
                    txs[i].Focus();
                    return;
                }
                cMoney[i] = Convert.ToInt32(txs[i].Text).ToString("X6");
                string[] sTemp = new string[4];
                sTemp[0] = cMoney[i];
                sTemp[1] = sTemp[0].Substring(4, 2);
                sTemp[2] = sTemp[0].Substring(2, 2);
                sTemp[3] = sTemp[0].Substring(0, 2);
                string s = sTemp[1] + sTemp[2] + sTemp[3];
                cMoney[i] = s;
            }


            DateTimePicker[] dps = (from Control n in btn.Parent.Controls
                                    where n is DateTimePicker
                                    orderby n.Tag ascending
                                    select (DateTimePicker)n).ToArray();

            CHD.API.SYSTEMTIME[] st = new CHD.API.SYSTEMTIME[16];
            for (int i = 0; i < dps.Length; i++)
            {
                st[i] = CHD.Common.ParasTime(dps[i].Value);
            }

            int nRt = CHD.API.CHD601D_M3.Consume_SetMoney(PortId, NetId, ref st[0], ref st[1], cMoney[0],
                                                    ref st[2], ref st[3], cMoney[1],
                                                    ref st[4], ref st[5], cMoney[2],
                                                    ref st[6], ref st[7], cMoney[3],
                                                    ref st[8], ref st[9], cMoney[4],
                                                    ref st[10], ref st[11], cMoney[5],
                                                    ref st[12], ref st[13], cMoney[6],
                                                    ref st[14], ref st[15], cMoney[7]);
            if (nRt == 0)
            {
                PrintMessage("分段定值金额设定成功");
            }
            else
            {
                PrintMessage("分段定值金额设定失败! 错误码:" + nRt);
            }
        }
        #endregion




        #region 设置账户信息
        /// <summary>
        /// 系统参数设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSysParameter_Click(object sender, EventArgs e)
        {
            int devId = 0;
            try
            {
                devId = Convert.ToInt32(txtDevID.Text);
                if (devId > 254)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("设备ID输入不正确");
                return;
            }

            //sprintf_s(cBautRate, "%06X", _wtol(tcBautRate));
            String cBautRate = Convert.ToUInt32(cmbBautRate.SelectedItem).ToString("X6");
            string[] sTemp = new string[4];
            sTemp[0] = cBautRate;
            sTemp[1] = sTemp[0].Substring(4, 2);
            sTemp[2] = sTemp[0].Substring(2, 2);
            sTemp[3] = sTemp[0].Substring(0, 2);
            string s = sTemp[1] + sTemp[2] + sTemp[3];
            cBautRate = s;

            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetSysParameter(PortId, NetId, cmbSection.SelectedIndex + 1, devId, cBautRate, cmbConsumeMode.SelectedIndex + 1, cmbOperatorMode.SelectedIndex), "系统参数设定成功", "系统参数设定失败!");
        }



        /// <summary>
        /// 补贴参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSubsides_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD601D_M3.Consume_SetSubsides(PortId, NetId,
                checkBox1.Checked ? 1 : 0,
                checkBox2.Checked ? 1 : 0,
                checkBox3.Checked ? 1 : 0);
            if (nRt == 0)
            {
                PrintMessage("补贴参数设定成功");
            }
            else
            {
                PrintMessage("补贴参数设定失败! 错误码: " + nRt);
            }

        }




        /// <summary>
        /// 折扣设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDisCount_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetDisCount(PortId, NetId, cmbCardType.SelectedIndex + 1, int.Parse(textBox88.Text)), "折扣设定成功", "折扣设定失败!");
        }



        /// <summary>
        /// 消费限制设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimitedConsume_Click(object sender, EventArgs e)
        {
            if (txtLimitCount.Text.Length == 0 || txtLimitMoney.Text.Length == 0 || txtDayLimitCount.Text.Length == 0 || txtDayLimitMoney.Text.Length == 0)
            {
                MessageBox.Show("请输入完整信息");
                return;
            }

            if (int.Parse(txtLimitCount.Text) > 3)
            {
                MessageBox.Show("餐消费次数最多为3次");
                return;
            }
            String cLimitMoney = int.Parse(txtLimitMoney.Text).ToString("X6");
            string cDayLimitMoney = int.Parse(txtDayLimitMoney.Text).ToString("X6");
            {
                //sprintf_s(cLimitMoney, "%06X", _wtol(tcData[1]));
                string[] sTemp = new string[4];
                sTemp[0] = cLimitMoney;
                sTemp[1] = sTemp[0].Substring(4, 2);
                sTemp[2] = sTemp[0].Substring(2, 2);
                sTemp[3] = sTemp[0].Substring(0, 2);
                string s = sTemp[1] + sTemp[2] + sTemp[3];
                cLimitMoney = s;
            }
            {
                string[] sTemp = new string[4];
                sTemp[0] = cDayLimitMoney;
                sTemp[1] = sTemp[0].Substring(4, 2);
                sTemp[2] = sTemp[0].Substring(2, 2);
                sTemp[3] = sTemp[0].Substring(0, 2);
                string s = sTemp[1] + sTemp[2] + sTemp[3];
                cDayLimitMoney = s;
            }

            int nRt = CHD.API.CHD601D_M3.Consume_LimitedConsume(PortId, NetId, cmbCardConsumeLimited.SelectedIndex, cmbCardTypeLimited.SelectedIndex + 1,
                cmbConsumeLimited.SelectedIndex, int.Parse(txtLimitCount.Text), cLimitMoney, int.Parse(txtDayLimitCount.Text), cDayLimitMoney, cmbNameList.SelectedIndex,
                checkBox4.Checked ? 1 : 0,
                checkBox5.Checked ? 1 : 0,
                checkBox6.Checked ? 1 : 0);
            if (nRt == 0)
            {
                PrintMessage("消费限制设定成功");
            }
            else
            {
                PrintMessage("消费限制设定失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 管理员设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAdmin_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD601D_M3.Consume_SetAdmin(PortId, NetId, cmbIndex.SelectedIndex + 1, txtCardNo.Text, txtAdminCode.Text);
            if (nRt == 0)
            {
                PrintMessage("管理员设定成功");
            }
            else
            {
                PrintMessage("管理员设定失败! 错误码: " + nRt);
            }

        }



        /// <summary>
        /// 写入补贴金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetMoneyOperat_Click(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDouble(txttcMoney.Text);
            }
            catch
            {
                MessageBox.Show("金额输入不正确");
                return;
            }
            string cMoney = long.Parse(txttcMoney.Text).ToString("X6");
            string[] sTemp = new string[4];
            sTemp[0] = cMoney;
            sTemp[1] = sTemp[0].Substring(4, 2);
            sTemp[2] = sTemp[0].Substring(2, 2);
            sTemp[3] = sTemp[0].Substring(0, 2);
            string s = sTemp[1] + sTemp[2] + sTemp[3];
            cMoney = s;
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetMoneyOperat(PortId, NetId, txttcUserID.Text, 1, cMoney), "补贴金额写入成功", "补贴金额写入失败!");
        }




        /// <summary>
        /// 账户扣款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button52_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(txtMoney2.Text);
            }
            catch
            {
                MessageBox.Show("金额输入不正确");
                return;
            }
            string cMoney = long.Parse(txtMoney2.Text).ToString("X6");
            string[] sTemp = new string[4];
            sTemp[0] = cMoney;
            sTemp[1] = sTemp[0].Substring(4, 2);
            sTemp[2] = sTemp[0].Substring(2, 2);
            sTemp[3] = sTemp[0].Substring(0, 2);
            string s = sTemp[1] + sTemp[2] + sTemp[3];
            cMoney = s;
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetMoneyOperat(PortId, NetId, txtUserId2.Text, 2, cMoney), "账户扣款成功", "账户扣款失败!");
        }



        /// <summary>
        /// 账户金额同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button53_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(txtMoney3.Text);
            }
            catch
            {
                MessageBox.Show("金额输入不正确");
                return;
            }
            string cMoney = long.Parse(txtMoney3.Text).ToString("X6");
            string[] sTemp = new string[4];
            sTemp[0] = cMoney;
            sTemp[1] = sTemp[0].Substring(4, 2);
            sTemp[2] = sTemp[0].Substring(2, 2);
            sTemp[3] = sTemp[0].Substring(0, 2);
            string s = sTemp[1] + sTemp[2] + sTemp[3];
            cMoney = s;
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetMoneyOperat(PortId, NetId, txtUserId3.Text, 3, cMoney), "账户金额同步成功", "账户金额同步失败!");

        }

        #endregion




        #region 卡信息

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            byte[] cName = new byte[260];
            byte[] dd=Encoding.Default.GetBytes(txtUserName.Text);
            for (int i = 0; i < dd.Length; i++)
            {
                cName[i] = dd[i];
            }
            {
                int nLen = dd.Length;
                for (int i = 0; i < nLen; ++i)
                {
                    cName[15 - i * 2] = ByteToBCD(cName[nLen - i - 1], 1);
                    cName[15 - i * 2 - 1] = ByteToBCD(cName[nLen - i - 1], 2);
                }

                for (int i = 0; i < (15 - nLen * 2 + 1) / 2; i++)
                {
                    cName[i * 2] = 0x32;
                    cName[i * 2 + 1] = 0x30;
                }
            }

            CHD.API.SYSTEMTIME st = CHD.Common.ParasTime(dpUserLmtDate.Value);
            int nRt = CHD.API.CHD601D_M3.Consume_AddOneUser(PortId, NetId, txtCardNo.Text, txtUserId.Text, txtUserPwd.Text, ref st, cmbUserCardType.SelectedIndex + 1, cName, int.Parse(txtBatch.Text));
            if (nRt == 0)
            {
                PrintMessage("添加用户成功");
            }
            else
            {
                PrintMessage("添加用户失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUser_Click(object sender, EventArgs e)
        {
            string cData = "";
            switch (cmbDelUserIndex.SelectedIndex)
            {
                case 0:
                    {
                        cData = txtDelUserCardNo.Text;
                        break;
                    }
                case 1:
                    {
                        cData = txtDelUserUserNo.Text;
                        break;
                    }
                case 2:
                    {
                        cData = "0000000000";
                        break;
                    }
                default:
                    break;
            }

            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_DeleteUser(PortId, NetId, cmbDelUserIndex.SelectedIndex, cData), "删除用户成功", "删除用户失败! ");
        }



        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetLoss_Click(object sender, EventArgs e)
        {
            SetLoss(1);
        }

        private void SetLoss(int type)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder();
            foreach (Control c in groupBox13.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text != "00000000")
                    {
                        sb.Append(textBox.Text);
                        count++;
                    }
                }
            }
            PrintExcuteResult(CHD.API.CHD601D_M3.Consume_SetLoss(PortId, NetId, type, count, sb.ToString()), type == 1 ? "挂失成功" : "解挂成功", type == 1 ? "挂失失败!" : "解挂失败!");

        }



        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetUnloss_Click(object sender, EventArgs e)
        {
            SetLoss(2);
        }
        #endregion




        #region 读取信息
        /// <summary>
        /// 读取商品条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadConsumeCard_Click(object sender, EventArgs e)
        {

            byte[] btUserInfo = new byte[120];

            int nRt = CHD.API.CHD601D_M3.Consume_ReadConsumeCard(PortId, NetId, txtReadConsumeCode.Text, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);
                String strCode = strInfo.Substring(0, 12);
                String strPrice = strInfo.Substring(12, 6);// strInfo.Substring(16, 2) + strInfo.Substring(14, 2) + strInfo.Substring(12, 2);
                String strName = strInfo.Substring(18, 24);
                List<byte> pName = new List<byte>();
                for (int i = 0; i < strName.Length; i++, i++)
                {
                    string ss = strName.Substring(i, 2);
                    byte btTemp = (byte)(Convert.ToInt32(ss, 16));
                    pName.Add(btTemp);

                }
                string sss = Encoding.Default.GetString(pName.ToArray());
                PrintMessage(String.Format("读取商品条码成功: 商品条码: {0} 价格: {1}  名称: {2}", strCode, (Convert.ToInt32(strPrice, 16))/100.0, sss.Trim()));
            }
            else
            {
                PrintMessage("读取商品条码失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取批次时间段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadBatch_Click(object sender, EventArgs e)
        {
            if (txtReadBatch.Text.Length == 0)
            {
                MessageBox.Show("没有输入批次");
                return;
            }

            byte[] btUserInfo = new byte[260];
            int nRt = CHD.API.CHD601D_M3.Consume_ReadBatch(PortId, NetId, int.Parse(txtReadBatch.Text), btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < strInfo.Length; i++, i++, i++, i++)
                {
                    sb.Append(strInfo.Substring(i, 2));
                    sb.Append(" : ");
                    sb.Append(strInfo.Substring(i + 2, 2));
                    sb.Append(" ");
                }
                PrintMessage("读取批次时间段成功: 时间段: " + sb.ToString());
            }
            else
            {
                PrintMessage("读取批次时间段失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取管理员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAdmin_Click(object sender, EventArgs e)
        {

            StringBuilder btUserInfo = new StringBuilder();
            int nRt = CHD.API.CHD601D_M3.Consume_ReadAdmin(PortId, NetId, txtReadAdminCardNO.Text, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = btUserInfo.ToString();

                PrintMessage(String.Format("读取管理员信息成功: 卡号: {0} 权限索引: {1}  管理员代码: {2}",
                    strInfo.Substring(0, 10), strInfo.Substring(10, 2), strInfo.Substring(12, 4)));
            }
            else
            {
                PrintMessage("读取管理员信息失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取记录应答
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecordAck_Click(object sender, EventArgs e)
        {
            if (txtRecPoint.Text.Length == 0)
            {
                MessageBox.Show("没有输入数据");
                return;
            }


            StringBuilder btUserInfo = new StringBuilder();
            int nRt = CHD.API.CHD601D_M3.Consume_ReadRecordAck(PortId, NetId, int.Parse(txtRecPoint.Text), btUserInfo);
            if (nRt == 0)
            {
                String strInfo = btUserInfo.ToString();
                PrintMessage(String.Format(@"读取记录应答成功: 事件代码: {0} 消费机区域: {1} 消费机地址:{2} 年月日:{3}-{4}-{5} 消费序号:{6} 操作员代码:{7} 时分秒:{8}:{9}:{10}
					   金额:{11} 事件描述:{12} 记录指针:{13}",
                               strInfo.Substring(0, 2),
                               strInfo.Substring(2, 2),
                               strInfo.Substring(4, 2),
                               strInfo.Substring(6, 2),
                               strInfo.Substring(8, 2),
                               strInfo.Substring(10, 2),
                               strInfo.Substring(12, 4),
                               strInfo.Substring(16, 4),
                               strInfo.Substring(20, 2),
                               strInfo.Substring(22, 2),
                               strInfo.Substring(24, 2),
                               strInfo.Substring(30, 2) + strInfo.Substring(28, 2) + strInfo.Substring(26, 2),
                               strInfo.Substring(32, 32),
                               strInfo.Substring(64, 4)));
            }
            else
            {
                PrintMessage("读取记录应答失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 按指针记录读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecordAckByPointer_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(txtRecPoint.Text);
            }
            catch
            {
                MessageBox.Show("记录指针输入错误！");
                return;
            }
            byte[] btUserInfo = new byte[120];
            int point = int.Parse(txtRecPoint.Text);
            int nRt = CHD.API.CHD601D_M3.Consume_ReadRecordByPointer(PortId, NetId, point, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);
                PrintMessage(String.Format(@"按指针读取记录成功: 事件代码: {0} 消费机区域: {1} 消费机地址:{2} 年月日:{3}-{4}-{5} 消费序号:{6} 操作员代码:{7} 时分秒:{8}:{9}:{10}
					   金额:{11} 事件描述:{12} 记录指针:{13}",
                               strInfo.Substring(0, 2),
                               strInfo.Substring(2, 2),
                               strInfo.Substring(4, 2),
                               strInfo.Substring(6, 2),
                               strInfo.Substring(8, 2),
                               strInfo.Substring(10, 2),
                               strInfo.Substring(12, 4),
                               strInfo.Substring(16, 4),
                               strInfo.Substring(20, 2),
                               strInfo.Substring(22, 2),
                               strInfo.Substring(24, 2),
                               strInfo.Substring(30, 2) + strInfo.Substring(28, 2) + strInfo.Substring(26, 2),
                               strInfo.Substring(32, 32),
                               strInfo.Substring(64, 4)));
            }
            else
            {
                PrintMessage("按指针读取记录失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 按ID读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserInfoById_Click(object sender, EventArgs e)
        {

            byte[] btUserInfo = new byte[120];
            int nRt = CHD.API.CHD601D_M3.Consume_ReadUserID(PortId, NetId, txtReadInfoUserId.Text, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);

                char[] user = "萨法".ToCharArray();
                String cName = "";//Encoding.Unicode.GetString(user);

                PrintMessage(String.Format("读取用户信息成功: 卡号: {0} 工号: {1}  密码: {2}  有效期: {3}-{4}-{5} 卡类: {6}, 姓名: {7}, 批次: {8}",
                    strInfo.Substring(0, 10), strInfo.Substring(10, 8), strInfo.Substring(18, 6),
                     strInfo.Substring(24, 4), strInfo.Substring(28, 2), strInfo.Substring(30, 2),
                     strInfo.Substring(32, 2), cName, strInfo.Substring(52, 2)));
            }
            else
            {
                PrintMessage("读取用户信息失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 按按卡号读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserInfoByCardNo_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();
            int nRt = CHD.API.CHD601D_M3.Consume_ReadCardNo(PortId, NetId, txtReadInfoUserId.Text, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = btUserInfo.ToString();
                String cName = strInfo.Substring(34, 16);

                PrintMessage(String.Format("读取用户信息成功: 卡号: {0} 工号: {1}  密码: {2}  有效期: {3}-{4}-{5} 卡类: {6}, 姓名: {7}, 批次: {8}",
                    strInfo.Substring(0, 10), strInfo.Substring(10, 8), strInfo.Substring(18, 6),
                     strInfo.Substring(24, 4), strInfo.Substring(28, 2), strInfo.Substring(30, 2),
                     strInfo.Substring(32, 2), tcName, strInfo.Substring(52, 2)));
            }
            else
            {
                PrintMessage("读取用户信息失败! 错误码: " + nRt);
            }
        }

        private void btnReadVersion_Click(object sender, EventArgs e)
        {

            //StringBuilder szVersion = new StringBuilder();
            byte[] szVersion = new byte[60];
            int nRt = CHD.API.CHD601D_M3.Consume_ReadVersion(PortId, NetId, szVersion);
            if (nRt == 0)
            {
                PrintMessage("读取设备版本成功:" + Encoding.Default.GetString(szVersion));
            }
            else
            {
                PrintMessage("读取设备版本失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserCount_Click(object sender, EventArgs e)
        {
            int lCount = 0;
            int nRt = CHD.API.CHD601D_M3.Consume_ReadUserCount(PortId, NetId, out lCount);
            if (nRt == 0)
            {
                PrintMessage("读取用户数量成功:" + lCount);
            }
            else
            {
                PrintMessage("读取用户数量失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadOneRec_Click(object sender, EventArgs e)
        {
            byte[] btUserInfo = new byte[260];

            int nRt = CHD.API.CHD601D_M3.Consume_ReadOneRecord(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);

                PrintMessage(String.Format(@"读取一条记录成功: 事件代码: {0} 消费机区域: {1} 消费机地址:{2} 年月日:{3}-{4}-{5} 消费序号:{6} 操作员代码:{7} 时分秒:{8}:{9}:{10}
					   金额:{11} 事件描述:{12} 记录指针:{13}",
                                      strInfo.Substring(0, 2),
                                      strInfo.Substring(2, 2),
                                      strInfo.Substring(4, 2),
                                      strInfo.Substring(6, 2),
                                      strInfo.Substring(8, 2),
                                      strInfo.Substring(10, 2),
                                      strInfo.Substring(12, 4),
                                      strInfo.Substring(16, 4),
                                      strInfo.Substring(20, 2),
                                      strInfo.Substring(22, 2),
                                      strInfo.Substring(24, 2),
                                      Convert.ToInt32(strInfo.Substring(30, 2) + strInfo.Substring(28, 2) + strInfo.Substring(26, 2),16),
                                      strInfo.Substring(32, 32),
                                      strInfo.Substring(64, 4)));
            }
            else
            {
                PrintMessage("读取一条记录失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 读记录参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecParam_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();
            int nRt = CHD.API.CHD601D_M3.Consume_ReadRecordParameter(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = btUserInfo.ToString();
                String strButtom = strInfo.Substring(0, 4);
                String strLOADP = strInfo.Substring(4, 4);
                String strSAVEP = strInfo.Substring(8, 4);
                String strMF = strInfo.Substring(12, 2);
                String strMAXLEN = strInfo.Substring(14, 4);
                PrintMessage(String.Format("读取记录参数成功: 桶底BOTTOM: {0}, 新记录存放指针: {1},	读取记录位置指针: {2}, 标志MF :{3}, 柜桶最大深度: {4}",
                    strButtom, strLOADP, strSAVEP, strMF, strMAXLEN));
            }
            else
            {
                PrintMessage("读取记录参数失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadDevTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME st;
            int nRt = CHD.API.CHD601D_M3.Consume_ReadDateTime(PortId, NetId, out st);
            if (nRt == 0)
            {
                String sTemp = "";
                if (st.wDayOfWeek == 1)
                {
                    sTemp = "周一";
                }
                else if (st.wDayOfWeek == 2)
                {
                    sTemp = "周二";
                }
                else if (st.wDayOfWeek == 3)
                {
                    sTemp = "周三";
                }
                else if (st.wDayOfWeek == 4)
                {
                    sTemp = "周四";
                }
                else if (st.wDayOfWeek == 5)
                {
                    sTemp = "周五";
                }
                else if (st.wDayOfWeek == 6)
                {
                    sTemp = "周六";
                }
                else if (st.wDayOfWeek == 7)
                {
                    sTemp = "周日";
                }
                PrintMessage(String.Format("读取时间成功:{0}-{1}-{2} {3}:{4}:{5} {6}", st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, sTemp));


            }
            else
            {
                PrintMessage("读取时间失败! 错误码: " + nRt);
            }

        }



        /// <summary>
        /// 读取定值模式金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFixed_Click(object sender, EventArgs e)
        {
            byte[] btUserInfo = new byte[120];
            int nRt = CHD.API.CHD601D_M3.Consume_ReadFixed(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);

                for (int i = 0, j = 0; j < strInfo.Length / 14; j++)
                {
                    String s = "";
                    s += strInfo.Substring(i, 2);
                    s += " : ";
                    s += strInfo.Substring(i + 2, 2);
                    s += " ";
                    s += strInfo.Substring(i + 4, 2);
                    s += " : ";
                    s += strInfo.Substring(i + 6, 2);
                    s += " ";
                    String s1 = strInfo.Substring(i + 12, 2) + strInfo.Substring(i + 10, 2) + strInfo.Substring(i + 8, 2);
                    PrintMessage(String.Format("读取定值模式金额成功: 时间段: {0} 金额: {1}", s, Convert.ToInt32(s1, 16)));

                    i += 14;
                }
            }
            else
            {
                PrintMessage("读取定值模式金额失败! 错误码: " + nRt);

            }
        }



        /// <summary>
        /// 读取消费限制信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadConsumeLimited_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();
            int nRt = CHD.API.CHD601D_M3.Consume_ReadConsumeLimited(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = btUserInfo.ToString();

                String bCardTypeLimited = strInfo.Substring(0, 2);
                String iCardType = strInfo.Substring(2, 16);
                String bConsumeLimited = strInfo.Substring(18, 2);
                String iMealsLimited = strInfo.Substring(20, 2);
                String sMoney = strInfo.Substring(26, 2) + strInfo.Substring(24, 2) + strInfo.Substring(22, 2);
                String iDayLimited = strInfo.Substring(28, 2);
                String sDayMoney = strInfo.Substring(34, 2) + strInfo.Substring(32, 2) + strInfo.Substring(30, 2);
                String bNameList = strInfo.Substring(36, 2);
                PrintMessage(String.Format(@"读取消费限制信息成功:卡类消费限制: {0} 卡类限制: {1}类卡  时段消费限制: {2}  餐限次: {3} 餐限额: {4}
					   天限次: {5} 天限额: {6} 黑白名单支持: {7} {8} {9} {10}",
                    bCardTypeLimited == "1" ? "卡类限制" : "无限制",
                    iCardType,
                    bConsumeLimited == "1" ? "特定时段" : "任意时段",
                    iMealsLimited,
                    sMoney,
                    iDayLimited,
                    sDayMoney,
                    bNameList == "1" ? "白名单" : "黑名单",
                    strInfo.Substring(38, 2) == "1" ? "允许现金支付" : "不允许现金支付",
                    strInfo.Substring(40, 2) == "1" ? "允许打折支付" : "不允许打折支付",
                    strInfo.Substring(42, 2) == "1" ? "允许输密操作" : "不允许输密操作"));
            }
            else
            {
                PrintMessage("读取消费限制信息失败! 错误码: " + nRt);
            }
        }
        #endregion



    }
}
