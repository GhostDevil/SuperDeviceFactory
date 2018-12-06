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

namespace CHD603SDemo
{
    public partial class CHD603S : Form
    {
        private int portId;

        public CHD603S()
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

        public CHD603S(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数
        private void InitCmp()
        {
            for (int i = 0; i < 255; i++)
            {
                cmbCardType.Items.Add(i.ToString());
                cmbBatch.Items.Add(i.ToString());
                comboBox10.Items.Add(i.ToString());
                comboBox12.Items.Add(i.ToString());
                comboBox14.Items.Add(i.ToString());
                comboBox11.Items.Add(i.ToString());
                comboBox13.Items.Add(i.ToString());

            }
            cmbCardType.SelectedIndex = 0;
            cmbBatch.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            for (int i = 0; i < 40; i++)
            {
                comboBox7.Items.Add(i);
            }
            for (int i = 1; i < 255; i++)
            {
                comboBox9.Items.Add(i);
            }
            comboBox7.SelectedItem = 0;
            comboBox9.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;


            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;
            comboBox13.SelectedIndex = 0;
            comboBox14.SelectedIndex = 0;

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
        #endregion



        /// <summary>
        /// 读取卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            byte[] cardNo = new byte[20];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadCardNo(PortId, NetId, cardNo);
            if (nRt == 0)
            {
                PrintMessage("读取卡号成功:" + Encoding.Default.GetString(cardNo));
                textBox27.Text = textBox26.Text = txtCardNo.Text = textBox28.Text = textBox30.Text = textBox37.Text = textBox42.Text = textBox43.Text = textBox52.Text = textBox53.Text = Encoding.Default.GetString(cardNo);
            }
            else
            {
                PrintMessage("读取卡号失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 初始化卡密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD603S.ConsumeCard_InitPassword(PortId, NetId, textBox27.Text, 1, 0);
            if (nRt == 0)
            {
                PrintMessage("初始化密码成功");
            }
            else
            {
                PrintMessage("初始化密码失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            byte[] version = new byte[60];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadVersion(PortId, NetId, version);
            if (nRt == 0)
            {
                PrintMessage("读取设备版本成功:" + Encoding.Default.GetString(version));
            }
            else
            {
                PrintMessage("读取设备版本失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取卡类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            short dwType = 0;
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadCardType(PortId, NetId, out dwType);
            if (nRt == 0)
            {
                if (dwType == 0x0400)
                {
                    PrintMessage("读取设备版本成功: 卡类型: mifare one");
                }
            }
            else
            {
                PrintMessage("读取设备版本失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 次数/定额 写金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            if (textBox29.Text.Length == 0)
            {
                MessageBox.Show("没有输入数据");
                return;
            }
            short dwMoney = (short)(short.Parse(textBox29.Text) * 100);
            short[] dwTemp = new short[8];
            dwTemp[0] = (short)((dwMoney & 0xF0000000) >> 28);
            dwTemp[1] = (short)((dwMoney & 0x0F000000) >> 24);
            dwTemp[2] = (short)((dwMoney & 0x00F00000) >> 20);
            dwTemp[3] = (short)((dwMoney & 0x000F0000) >> 16);
            dwTemp[4] = (short)((dwMoney & 0x0000F000) >> 12);
            dwTemp[5] = (short)((dwMoney & 0x00000F00) >> 8);
            dwTemp[6] = (short)((dwMoney & 0x000000F0) >> 4);
            dwTemp[7] = (short)((dwMoney & 0x0000000F)); dwTemp[6].ToString("X1");

            string tcSendMoney = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", dwTemp[6].ToString("X1"), dwTemp[7].ToString("X1"), dwTemp[4].ToString("X1"), dwTemp[5].ToString("X1"), dwTemp[2].ToString("X1"), dwTemp[3].ToString("X1"), dwTemp[0].ToString("X1"), dwTemp[1].ToString("X1"));
            int nRt = CHD.API.CHD603S.ConsumeCard_WriteCount(PortId, NetId, textBox28.Text, 9, tcSendMoney);
            if (nRt == 0)
            {
                PrintMessage("写金额/次数成功");
            }
            else
            {
                PrintMessage("写金额/次数失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 次数/定额 读金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            short dwMoney = 0;
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadCount(PortId, NetId, textBox28.Text, 9, out dwMoney);
            if (nRt == 0)
            {
                PrintMessage("读金额成功, 金额: " + dwMoney / 100);
            }
            else
            {
                PrintMessage("读金额失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 写金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            if (textBox36.Text.Length == 0)
            {
                MessageBox.Show("没有输入数据");
                return;
            }
            short dwMoney = (short)(short.Parse(textBox36.Text) * 100);
            short[] dwTemp = new short[8];
            dwTemp[0] = (short)((dwMoney & 0xF0000000) >> 28);
            dwTemp[1] = (short)((dwMoney & 0x0F000000) >> 24);
            dwTemp[2] = (short)((dwMoney & 0x00F00000) >> 20);
            dwTemp[3] = (short)((dwMoney & 0x000F0000) >> 16);
            dwTemp[4] = (short)((dwMoney & 0x0000F000) >> 12);
            dwTemp[5] = (short)((dwMoney & 0x00000F00) >> 8);
            dwTemp[6] = (short)((dwMoney & 0x000000F0) >> 4);
            dwTemp[7] = (short)((dwMoney & 0x0000000F)); dwTemp[6].ToString("X1");

            string tcSendMoney = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", dwTemp[6].ToString("X1"), dwTemp[7].ToString("X1"), dwTemp[4].ToString("X1"), dwTemp[5].ToString("X1"), dwTemp[2].ToString("X1"), dwTemp[3].ToString("X1"), dwTemp[0].ToString("X1"), dwTemp[1].ToString("X1"));
            int nRt = CHD.API.CHD603S.ConsumeCard_WriteMoney(PortId, NetId, textBox26.Text, 8, tcSendMoney);
            if (nRt == 0)
            {
                PrintMessage("写金额/次数成功");
            }
            else
            {
                PrintMessage("写金额/次数失败! 错误码:" + nRt);
            }

        }



        /// <summary>
        /// 读金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {
            short dwMoney = 0;
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadMoney(PortId, NetId, textBox26.Text, 8, out dwMoney);
            if (nRt == 0)
            {
                PrintMessage("读金额成功, 金额: " + dwMoney / 100);
            }
            else
            {
                PrintMessage("读金额失败! 错误码: " + nRt);
            }
        }


        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox31.Text.Length == 0)
            {
                MessageBox.Show("没有输入金额");
                return;
            }
            short dwMoney = (short)(short.Parse(textBox31.Text) * 100);
            short[] dwTemp = new short[8];
            dwTemp[0] = (short)((dwMoney & 0xF0000000) >> 28);
            dwTemp[1] = (short)((dwMoney & 0x0F000000) >> 24);
            dwTemp[2] = (short)((dwMoney & 0x00F00000) >> 20);
            dwTemp[3] = (short)((dwMoney & 0x000F0000) >> 16);
            dwTemp[4] = (short)((dwMoney & 0x0000F000) >> 12);
            dwTemp[5] = (short)((dwMoney & 0x00000F00) >> 8);
            dwTemp[6] = (short)((dwMoney & 0x000000F0) >> 4);
            dwTemp[7] = (short)((dwMoney & 0x0000000F)); dwTemp[6].ToString("X1");

            string tcSendMoney = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", dwTemp[6].ToString("X1"), dwTemp[7].ToString("X1"), dwTemp[4].ToString("X1"), dwTemp[5].ToString("X1"), dwTemp[2].ToString("X1"), dwTemp[3].ToString("X1"), dwTemp[0].ToString("X1"), dwTemp[1].ToString("X1"));
            int nRt = CHD.API.CHD603S.ConsumeCard_Recharge(PortId, NetId, textBox30.Text, 8, tcSendMoney);
            if (nRt == 0)
            {
                PrintMessage("充值成功");
            }
            else
            {
                PrintMessage("充值失败! 错误码:" + nRt);
            }

        }



        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            if (textBox31.Text.Length == 0)
            {
                MessageBox.Show("没有输入金额");
                return;
            }
            short dwMoney = (short)(short.Parse(textBox31.Text) * 100);
            short[] dwTemp = new short[8];
            dwTemp[0] = (short)((dwMoney & 0xF0000000) >> 28);
            dwTemp[1] = (short)((dwMoney & 0x0F000000) >> 24);
            dwTemp[2] = (short)((dwMoney & 0x00F00000) >> 20);
            dwTemp[3] = (short)((dwMoney & 0x000F0000) >> 16);
            dwTemp[4] = (short)((dwMoney & 0x0000F000) >> 12);
            dwTemp[5] = (short)((dwMoney & 0x00000F00) >> 8);
            dwTemp[6] = (short)((dwMoney & 0x000000F0) >> 4);
            dwTemp[7] = (short)((dwMoney & 0x0000000F)); dwTemp[6].ToString("X1");

            string tcSendMoney = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", dwTemp[6].ToString("X1"), dwTemp[7].ToString("X1"), dwTemp[4].ToString("X1"), dwTemp[5].ToString("X1"), dwTemp[2].ToString("X1"), dwTemp[3].ToString("X1"), dwTemp[0].ToString("X1"), dwTemp[1].ToString("X1"));
            int nRt = CHD.API.CHD603S.ConsumeCard_ChargeBack(PortId, NetId, textBox30.Text, 8, tcSendMoney);
            if (nRt == 0)
            {
                PrintMessage("扣款成功");
            }
            else
            {
                PrintMessage("扣款失败! 错误码:" + nRt);
            }
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
        /// 新建用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {

            Encoding ecd = Encoding.Default;
            byte[] tcCardNo = ecd.GetBytes(txtCardNo.Text);
            byte[] tcWorkNo = ecd.GetBytes(txtWorkNo.Text);
            byte[] tcPsw = ecd.GetBytes(txtPwd.Text);
            if (txtCardNo.Text.Length != 8)
            {
                MessageBox.Show("卡号输入不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtWorkNo.Text.Length == 0 || txtWorkNo.Text.Length != 8)
            {
                MessageBox.Show("工号输入不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWorkNo.Focus();
                return;
            }
            if (txtPwd.Text.Length == 0 || txtPwd.Text.Length != 6)
            {
                MessageBox.Show("密码输入不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPwd.Focus();
                return;
            }
            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("用户名输入不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }

            CHD.API.SYSTEMTIME stUseTime = CHD.Common.ParasTime(dpUseTime.Value);
            List<byte> veData = new List<byte>();
            List<byte> veData1 = new List<byte>();
            {
                for (int i = 0; i < tcCardNo.Length; ++i)
                {//用户基本信息的卡号
                    byte number = tcCardNo[i];
                    veData1.Add(number);
                }
                for (int i = 0; i < tcPsw.Length; ++i)
                {//用户基本信息的密码
                    byte number = tcPsw[i];
                    veData1.Add(number);
                }
                for (int i = 0; i < 6; ++i)
                {
                    veData1.Add(0x30);
                }

                string year = stUseTime.wYear.ToString("D4");
                byte[] byear = Encoding.Default.GetBytes(year);
                string month = stUseTime.wMonth.ToString("D2");
                byte[] bmonth = Encoding.Default.GetBytes(month);
                string day = stUseTime.wDay.ToString("D2");
                byte[] bday = Encoding.Default.GetBytes(day);
                byte btYear2 = byte.Parse(year.Substring(3, 1));
                byte btYear1 = byte.Parse(year.Substring(2, 1));
                byte Month2 = byte.Parse(month.Substring(1, 1));
                byte Month1 = byte.Parse(month.Substring(0, 1));
                byte Day2 = byte.Parse(day.Substring(1, 1));
                byte Day1 = byte.Parse(day.Substring(0, 1));
                veData1.Add(byear[2]);
                veData1.Add(byear[3]);
                veData1.Add(bmonth[0]);
                veData1.Add(bmonth[1]);
                veData1.Add(bday[0]);
                veData1.Add(bday[1]);

                for (int i = 0; i < 6; ++i)
                {
                    veData1.Add(0x30);
                }
            }
            {
                for (int i = 0; i < tcWorkNo.Length; ++i)
                {//工号
                    byte number = tcWorkNo[i];
                    veData.Add(number);
                }
                for (int i = 0; i < 4; ++i)
                {
                    veData.Add(0x30);
                }

                int iCardType = Convert.ToInt32(cmbCardType.SelectedItem);
                int iBatch = Convert.ToInt32(cmbBatch.SelectedItem);
                byte btCardType2 = ByteToBCD((byte)iCardType, 2);
                byte btCardType1 = ByteToBCD((byte)iCardType, 1);
                byte btBatch2 = ByteToBCD((byte)iBatch, 2);
                byte btBatch1 = ByteToBCD((byte)iBatch, 1);

                veData.Add(btCardType2);
                veData.Add(btCardType1);
                byte[] cTemp = new byte[260];
                byte[] dd = ecd.GetBytes(txtUserName.Text);
                for (int i = 0; i < dd.Length; i++)
                {
                    cTemp[i] = dd[i];
                }
                int nLength = dd.Length;
                for (int i = 0; i < nLength; ++i)
                {
                    cTemp[15 - i * 2] = ByteToBCD(cTemp[nLength - i - 1], 1);
                    cTemp[15 - i * 2 - 1] = ByteToBCD(cTemp[nLength - i - 1], 2);
                }

                for (int i = 0; i < (15 - nLength * 2 + 1) / 2; i++)
                {
                    cTemp[i * 2] = 0x32;
                    cTemp[i * 2 + 1] = 0x30;
                }

                for (int i = 0; i < (15 + 1); i++)
                {
                    veData.Add(cTemp[i]);
                }

                veData.Add(btBatch2);
                veData.Add(btBatch1);

                int nRt = CHD.API.CHD603S.ConsumeCard_WriteBlockData(PortId, NetId, txtCardNo.Text, 1, 0, veData1.ToArray(), veData.ToArray(), null);
                if (nRt == 0)
                {
                    PrintMessage("写用户信息成功");
                }
                else
                {
                    PrintMessage("写用户信息失败! 错误码: " + nRt);
                }


            }
        }




        /// <summary>
        /// 读取用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            byte[] btData1 = new byte[260];
            byte[] btData2 = new byte[260];
            String strReadUserInfo = "";
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, txtCardNo.Text, 1, 0, btData1, btData2, null);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1);
                string strInfoView1 = Encoding.Default.GetString(btData2);
                strReadUserInfo += " 工号: ";
                strReadUserInfo += Encoding.Default.GetString(btData2, 0, 8);
                strReadUserInfo += " 卡类: ";
                strReadUserInfo += int.Parse(Encoding.Default.GetString(btData2, 12, 2));
                strReadUserInfo += " 姓名: ";

                {//提取姓名
                    List<byte> nData = new List<byte>();
                    for (int i = 0; i < 8; ++i)
                    {
                        string ss = strInfoView1.Substring(14 + 2 * i, 2);
                        byte btTemp = (byte)(Convert.ToInt32(ss, 16));
                        if (btTemp != 0x00)
                        {
                            nData.Add(btTemp);
                        }
                    }
                    strReadUserInfo += Encoding.Default.GetString(nData.ToArray());
                }
                strReadUserInfo += " 批次: ";
                int iBatch = Convert.ToInt32(strInfoView1.Substring(30, 2), 16);
                strReadUserInfo += iBatch;
                strReadUserInfo += " 卡号: ";
                strReadUserInfo += strInfoView.Substring(0, 8);
                strReadUserInfo += " 密码: ";
                strReadUserInfo += strInfoView.Substring(8, 6);
                strReadUserInfo += " 有效期: ";
                strReadUserInfo += strInfoView.Substring(20, 2);
                strReadUserInfo += " 年 ";
                strReadUserInfo += strInfoView.Substring(22, 2);
                strReadUserInfo += " 月 ";
                strReadUserInfo += strInfoView.Substring(24, 2);
                strReadUserInfo += " 日 ";
                PrintMessage("读用户信息成功:" + strReadUserInfo);
            }
            else
            {
                PrintMessage("读用户信息失败! 错误码: " + nRt);
            }


        }



        /// <summary>
        /// 设置住户权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button40_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var groupBoxs = from Control c in btn.Parent.Controls
                            where c is GroupBox
                            select c;
            foreach (var ctr in groupBoxs)
            {
                var textBoxs = from Control txt in ctr.Controls
                               where txt is TextBox
                               select
                               (TextBox)txt;
                foreach (var textBox in textBoxs)
                {
                    if (textBox.Text.Length == 0 || int.Parse(textBox.Text) > 255)
                    {
                        MessageBox.Show("楼层输入不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox.Focus();
                        return;
                    }
                }
            }




            byte tcLiftGrant_StartFloor1 = Byte.Parse(textBox55.Text);
            byte tcLiftGrant_EndFloor1 = Byte.Parse(textBox54.Text);

            byte tcLiftGrant_StartFloor2 = Byte.Parse(textBox58.Text);
            byte tcLiftGrant_EndFloor2 = Byte.Parse(textBox59.Text);

            byte tcLiftGrant_StartFloor3 = Byte.Parse(textBox62.Text);
            byte tcLiftGrant_EndFloor3 = Byte.Parse(textBox63.Text);

            byte tcLiftGrant_StartFloor4 = Byte.Parse(textBox56.Text);
            byte tcLiftGrant_EndFloor4 = Byte.Parse(textBox57.Text);

            byte tcLiftGrant_StartFloor5 = Byte.Parse(textBox60.Text);
            byte tcLiftGrant_EndFloor5 = Byte.Parse(textBox61.Text);

            List<byte> veData = new List<byte>();

            int btLiftGrant_Building1 = int.Parse(comboBox10.SelectedItem.ToString());
            if (checkBox8.Checked)
            {
                btLiftGrant_Building1 += 128;
            }


            int btLiftGrant_Building2 = int.Parse(comboBox12.SelectedItem.ToString());
            if (checkBox10.Checked)
            {
                btLiftGrant_Building2 += 128;
            }

            int btLiftGrant_Building3 = int.Parse(comboBox14.SelectedItem.ToString());
            if (checkBox12.Checked)
            {
                btLiftGrant_Building3 += 128;
            }

            int btLiftGrant_Building4 = int.Parse(comboBox11.SelectedItem.ToString());
            if (checkBox9.Checked)
            {
                btLiftGrant_Building4 += 128;
            }

            int btLiftGrant_Building5 = int.Parse(comboBox13.SelectedItem.ToString());
            if (checkBox11.Checked)
            {
                btLiftGrant_Building5 += 128;
            }

            veData.Add(ByteToBCD((byte)btLiftGrant_Building1, 2));
            veData.Add(ByteToBCD((byte)btLiftGrant_Building1, 1));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor1, 2));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor1, 1));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor1, 2));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor1, 1));

            veData.Add(ByteToBCD((byte)btLiftGrant_Building2, 2));
            veData.Add(ByteToBCD((byte)btLiftGrant_Building2, 1));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor2, 2));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor2, 1));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor2, 2));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor2, 1));

            veData.Add(ByteToBCD((byte)btLiftGrant_Building3, 2));
            veData.Add(ByteToBCD((byte)btLiftGrant_Building3, 1));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor3, 2));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor3, 1));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor3, 2));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor3, 1));

            veData.Add(ByteToBCD((byte)btLiftGrant_Building4, 2));
            veData.Add(ByteToBCD((byte)btLiftGrant_Building4, 1));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor4, 2));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor4, 1));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor4, 2));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor4, 1));

            veData.Add(ByteToBCD((byte)btLiftGrant_Building5, 2));
            veData.Add(ByteToBCD((byte)btLiftGrant_Building5, 1));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor5, 2));
            veData.Add(ByteToBCD(tcLiftGrant_StartFloor5, 1));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor5, 2));
            veData.Add(ByteToBCD(tcLiftGrant_EndFloor5, 1));


            int nRt = CHD.API.CHD603S.ConsumeCard_WriteBlockData(PortId, NetId, textBox53.Text, 12, 0, veData.ToArray(), null, null);
            if (nRt == 0)
            {
                PrintMessage("设置住户权限成功");
            }
            else
            {
                PrintMessage("设置住户权限失败! 错误码: " + nRt);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            String tcCardNo = textBox37.Text;
            byte[] tcFlag = Encoding.Default.GetBytes(comboBox5.SelectedIndex.ToString());
            String tcOrder = textBox41.Text;
            String tcRemain = textBox40.Text;
            String tcTotal = textBox39.Text;
            String tcCount = textBox38.Text;
            int nSectorNo = 0, nFlag = 0;
            nSectorNo = comboBox5.SelectedIndex;
            nFlag = comboBox4.SelectedIndex;

            if (tcCardNo.Length == 0
                || tcOrder.Length == 0
                || tcRemain.Length == 0
                || tcTotal.Length == 0
                || tcCount.Length == 0)
            {
                MessageBox.Show("输入信息不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<byte> veData = new List<byte>();
            veData.Add(0x30);
            veData.Add(tcFlag[0]);
            {
                CHD.API.SYSTEMTIME stTime = CHD.Common.ParasTime(CHD.Common.CombinDateTime(dateTimePicker10.Value, dateTimePicker11.Value));
                byte[] tcDateAndTime = Encoding.Default.GetBytes(stTime.wMonth.ToString("D2") + stTime.wDay.ToString("D2") + stTime.wHour.ToString("D2") + stTime.wMinute.ToString("D2") + stTime.wSecond.ToString("D2"));

                for (int i = 0; i < 10; i++)
                {
                    veData.Add(tcDateAndTime[i]);
                }
            }
            {
                int dwMoney = int.Parse(tcOrder);//注意此处
                int[] dwTemp = new int[6];
                dwTemp[0] = ((dwMoney & 0x00F00000) >> 20);
                dwTemp[1] = ((dwMoney & 0x000F0000) >> 16);
                dwTemp[2] = ((dwMoney & 0x0000F000) >> 12);
                dwTemp[3] = ((dwMoney & 0x00000F00) >> 8);
                dwTemp[4] = ((dwMoney & 0x000000F0) >> 4);
                dwTemp[5] = ((dwMoney & 0x0000000F));

                byte[] tcSendMoney = Encoding.Default.GetBytes(dwTemp[4].ToString("X1") + dwTemp[5].ToString("X1") + dwTemp[2].ToString("X1") + dwTemp[3].ToString("X1") + dwTemp[0].ToString("X1") + dwTemp[1].ToString("X1"));
                for (int i = 0; i < 6; i++)
                {
                    veData.Add(tcSendMoney[i]);
                }
            }
            {
                int dwMoney = int.Parse(tcRemain);//注意此处
                int[] dwTemp = new int[6];
                dwTemp[0] = (dwMoney & 0x00F00000) >> 20;
                dwTemp[1] = (dwMoney & 0x000F0000) >> 16;
                dwTemp[2] = (dwMoney & 0x0000F000) >> 12;
                dwTemp[3] = (dwMoney & 0x00000F00) >> 8;
                dwTemp[4] = (dwMoney & 0x000000F0) >> 4;
                dwTemp[5] = (dwMoney & 0x0000000F);

                byte[] tcSendMoney = Encoding.Default.GetBytes(dwTemp[4].ToString("X1") + dwTemp[5].ToString("X1") + dwTemp[2].ToString("X1") + dwTemp[3].ToString("X1") + dwTemp[0].ToString("X1") + dwTemp[1].ToString("X1"));
                for (int i = 0; i < 6; i++)
                {
                    veData.Add(tcSendMoney[i]);
                }
            }
            {
                int dwMoney = int.Parse(tcTotal);//注意此处
                int[] dwTemp = new int[6];
                dwTemp[0] = (dwMoney & 0x00F00000) >> 20;
                dwTemp[1] = (dwMoney & 0x000F0000) >> 16;
                dwTemp[2] = (dwMoney & 0x0000F000) >> 12;
                dwTemp[3] = (dwMoney & 0x00000F00) >> 8;
                dwTemp[4] = (dwMoney & 0x000000F0) >> 4;
                dwTemp[5] = (dwMoney & 0x0000000F);

                byte[] tcSendMoney = Encoding.Default.GetBytes(dwTemp[4].ToString("X1") + dwTemp[5].ToString("X1") + dwTemp[2].ToString("X1") + dwTemp[3].ToString("X1") + dwTemp[0].ToString("X1") + dwTemp[1].ToString("X1"));
                for (int i = 0; i < 6; i++)
                {
                    veData.Add(tcSendMoney[i]);
                }
            }
            {
                byte dwCount = byte.Parse(tcCount);
                byte btA = ByteToBCD(dwCount, 2);
                byte btB = ByteToBCD(dwCount, 1);
                veData.Add(btA);
                veData.Add(btB);
            }

            int nRt = CHD.API.CHD603S.ConsumeCard_WriteBlockData(PortId, NetId, tcCardNo, 10, nSectorNo, veData.ToArray(), null, null);
            if (nRt == 0)
            {
                PrintMessage("写消费记录成功");
            }
            else
            {
                PrintMessage("写消费记录失败! 错误码: " + nRt);
            }

        }



        /// <summary>
        /// 读取住户权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button41_Click(object sender, EventArgs e)
        {
            byte[] btData1 = new byte[260];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, textBox53.Text, 12, 0, btData1, null, null);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1);

                {
                    string strNode = "";
                    string strBuilding = strInfoView.Substring(0, 2);
                    byte btGrant1 = Convert.ToByte(strBuilding, 16);
                    if (btGrant1 > 127)
                    {
                        int btTemp = btGrant1 - 128;
                        strNode += "第一节点: 楼栋编号: ";
                        strNode += btTemp;
                        strNode += " 权限: 特权卡";
                    }
                    else
                    {
                        strNode += "第一节点: 楼栋编号: ";
                        strNode += btGrant1;
                        strNode += " 权限: 普通权限 ";
                    }
                    strNode += " 开始楼层: ";
                    byte iStart = Convert.ToByte(strInfoView.Substring(2, 2), 16);
                    strNode += iStart;
                    strNode += " 结束楼层: ";
                    byte iEnd = Convert.ToByte(strInfoView.Substring(4, 2), 16);
                    strNode += iEnd;
                    PrintMessage(strNode);
                }
                {
                    string strNode = "";
                    string strBuilding = strInfoView.Substring(6, 2);
                    byte btGrant1 = Convert.ToByte(strBuilding, 16);
                    if (btGrant1 > 127)
                    {
                        int btTemp = btGrant1 - 128;
                        strNode += "第二节点: 楼栋编号: ";
                        strNode += btTemp;
                        strNode += " 权限: 特权卡";
                    }
                    else
                    {
                        strNode += "第二节点: 楼栋编号: ";
                        strNode += btGrant1;
                        strNode += " 权限: 普通权限 ";
                    }
                    strNode += " 开始楼层: ";
                    byte iStart = Convert.ToByte(strInfoView.Substring(8, 2), 16);
                    strNode += iStart;
                    strNode += " 结束楼层: ";
                    byte iEnd = Convert.ToByte(strInfoView.Substring(10, 2), 16);
                    strNode += iEnd;
                    PrintMessage(strNode);
                }
                {
                    string strNode = "";
                    string strBuilding = strInfoView.Substring(12, 2);
                    byte btGrant1 = Convert.ToByte(strBuilding, 16);
                    if (btGrant1 > 127)
                    {
                        int btTemp = btGrant1 - 128;
                        strNode += "第三节点: 楼栋编号: ";
                        strNode += btTemp;
                        strNode += " 权限: 特权卡";
                    }
                    else
                    {
                        strNode += "第三节点: 楼栋编号: ";
                        strNode += btGrant1;
                        strNode += " 权限: 普通权限 ";
                    }
                    strNode += " 开始楼层: ";
                    byte iStart = Convert.ToByte(strInfoView.Substring(14, 2), 16);
                    strNode += iStart;
                    strNode += " 结束楼层: ";
                    byte iEnd = Convert.ToByte(strInfoView.Substring(16, 2), 16);
                    strNode += iEnd;
                    PrintMessage(strNode);
                }
                {
                    string strNode = "";
                    string strBuilding = strInfoView.Substring(18, 2);
                    byte btGrant1 = Convert.ToByte(strBuilding, 16);
                    if (btGrant1 > 127)
                    {
                        int btTemp = btGrant1 - 128;
                        strNode += "第四节点: 楼栋编号: ";
                        strNode += btTemp;
                        strNode += " 权限: 特权卡";
                    }
                    else
                    {
                        strNode += "第四节点: 楼栋编号: ";
                        strNode += btGrant1;
                        strNode += " 权限: 普通权限 ";
                    }
                    strNode += " 开始楼层: ";
                    byte iStart = Convert.ToByte(strInfoView.Substring(20, 2), 16);
                    strNode += iStart;
                    strNode += " 结束楼层: ";
                    byte iEnd = Convert.ToByte(strInfoView.Substring(22, 2), 16);
                    strNode += iEnd;
                    PrintMessage(strNode);
                }
                {
                    string strNode = "";
                    string strBuilding = strInfoView.Substring(24, 2);
                    byte btGrant1 = Convert.ToByte(strBuilding, 16);
                    if (btGrant1 > 127)
                    {
                        int btTemp = btGrant1 - 128;
                        strNode += "第五节点: 楼栋编号: ";
                        strNode += btTemp;
                        strNode += " 权限: 特权卡";
                    }
                    else
                    {
                        strNode += "第五节点: 楼栋编号: ";
                        strNode += btGrant1;
                        strNode += " 权限: 普通权限 ";
                    }
                    strNode += " 开始楼层: ";
                    byte iStart = Convert.ToByte(strInfoView.Substring(26, 2), 16);
                    strNode += iStart;
                    strNode += " 结束楼层: ";
                    byte iEnd = Convert.ToByte(strInfoView.Substring(28, 2), 16);
                    strNode += iEnd;

                    PrintMessage(strNode);
                }
                PrintMessage("读取住户权限成功");
            }
            else
            {
                PrintMessage("读取住户权限失败! 错误码: " + nRt);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            byte[] btData1 = new byte[260];

            string strInfo = "标志: ";
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, textBox37.Text, 10, comboBox5.SelectedIndex, btData1, null, null);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1);
                string strFlag = strInfoView.Substring(0, 2);
                string strMonth = strInfoView.Substring(2, 2);
                string strDay = strInfoView.Substring(4, 2);
                string strHour = strInfoView.Substring(6, 2);
                string strMinute = strInfoView.Substring(8, 2);
                string strSecond = strInfoView.Substring(10, 2);

                int iFlag = int.Parse(strFlag);
                if (iFlag == 1)
                {
                    strInfo += " 消费 ";
                }
                if (iFlag == 2)
                {
                    strInfo += " 退款 ";
                }
                if (iFlag == 3)
                {
                    strInfo += " 充值 ";
                }
                strInfo += " 时间: ";
                strInfo += strMonth;
                strInfo += "-";
                strInfo += strDay;
                strInfo += " ";
                strInfo += strHour;
                strInfo += ":";
                strInfo += strMinute;
                strInfo += ":";
                strInfo += strSecond;

                strInfo += " 交易额: ";

                {
                    string strOrder1 = strInfoView.Substring(12, 2);
                    string strOrder2 = strInfoView.Substring(14, 2);
                    string strOrder3 = strInfoView.Substring(16, 2);
                    string strOrder = strOrder3;
                    strOrder += strOrder2;
                    strOrder += strOrder1;
                    int dwOrder = Convert.ToInt32(strOrder, 16);
                    strInfo += dwOrder;
                }
                strInfo += " 余额: ";
                {
                    string strRemain1 = strInfoView.Substring(18, 2);
                    string strRemain2 = strInfoView.Substring(20, 2);
                    string strRemain3 = strInfoView.Substring(22, 2);
                    string strRemain = strRemain3;
                    strRemain += strRemain2;
                    strRemain += strRemain1;
                    int dwRemain = Convert.ToInt32(strRemain, 16);
                    strInfo += dwRemain;
                }
                strInfo += " 单日统计: ";
                {
                    string strTotal1 = strInfoView.Substring(24, 2);
                    string strTotal2 = strInfoView.Substring(26, 2);
                    string strTotal3 = strInfoView.Substring(28, 2);
                    string strTotal = strTotal3;
                    strTotal += strTotal2;
                    strTotal += strTotal1;
                    int dwTotal = Convert.ToInt32(strTotal, 16);
                    strInfo += dwTotal;
                }
                strInfo += " 次数: ";
                {
                    string strCount = strInfoView.Substring(30, 2);
                    int dwCount = Convert.ToInt32(strCount, 16);
                    strInfo += dwCount;
                }
                PrintMessage("读用户信息成功");
                PrintMessage(strInfo);
            }
            else
            {
                PrintMessage("读用户信息失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取卡片块信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {
            int nSectorNo = int.Parse(comboBox7.SelectedItem.ToString());
            int nBlockNo = comboBox6.SelectedIndex;

            byte[] btData1 = new byte[260];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, textBox42.Text, nSectorNo, nBlockNo, btData1, null, null);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1);
                PrintMessage("读取卡片块数据成功," + strInfoView);
            }
            else
            {
                PrintMessage("读取卡片块数据失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 电梯读取卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            byte[] cardNo = new byte[20];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadCardNo(PortId, NetId, cardNo);
            if (nRt == 0)
            {
                PrintMessage("读取卡号成功:" + Encoding.Default.GetString(cardNo));
                textBox43.Text = textBox45.Text = textBox52.Text = textBox53.Text = Encoding.Default.GetString(cardNo);
            }
            else
            {
                PrintMessage("读取卡号失败! 错误码:" + nRt);
            }
        }




        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD603S.ConsumeCard_InitPassword(PortId, NetId, textBox43.Text, 1, 1);
            if (nRt == 0)
            {
                PrintMessage("初始化密码成功");
            }
            else
            {
                PrintMessage("初始化密码失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            int nItem = comboBox8.SelectedIndex;
            byte[] btData1 = new byte[260];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, textBox45.Text, 1, nItem, btData1, null, null);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1); ;
                string strCardNo = strInfoView.Substring(0, 8);
                string strPSW = strInfoView.Substring(8, 6);
                string strStartTime = strInfoView.Substring(14, 6);
                string strEndTime = strInfoView.Substring(20, 6);
                string strResidentCode = strInfoView.Substring(26, 6);
                string strTemp = "";
                strTemp += "卡号: ";
                strTemp += strCardNo;
                strTemp += " 密码: ";
                strTemp += strPSW;
                strTemp += " 开始时间: ";
                strTemp += strStartTime.Substring(0, 2);
                strTemp += " 年 ";
                strTemp += strStartTime.Substring(2, 2);
                strTemp += " 月 ";
                strTemp += strStartTime.Substring(4, 2);
                strTemp += " 日 ";

                strTemp += " 结束时间: ";
                strTemp += strEndTime.Substring(0, 2);
                strTemp += " 年 ";
                strTemp += strEndTime.Substring(2, 2);
                strTemp += " 月 ";
                strTemp += strEndTime.Substring(4, 2);
                strTemp += " 日 ";

                strTemp += " 住户代码: ";
                strTemp += strResidentCode;
                PrintMessage("读用户信息成功, " + strTemp);
            }
            else
            {
                PrintMessage("读用户信息失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 写用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            byte[] tcCardNo = Encoding.Default.GetBytes(textBox45.Text);
            byte[] tcPsw = Encoding.Default.GetBytes(textBox44.Text);

            byte[] tcResidentCode = Encoding.Default.GetBytes(textBox46.Text);
            int nItem = int.Parse(comboBox8.Text);

            List<byte> veData = new List<byte>();
            for (int i = 0; i < tcCardNo.Length; i++)
            {
                veData.Add(tcCardNo[i]);
            }
            for (int i = 0; i < tcPsw.Length; i++)
            {
                veData.Add(tcPsw[i]);
            }

            //-------------------------------------------------------//
            byte[] cStartTime = Encoding.Default.GetBytes(dateTimePicker12.Value.ToString("yyyyMMdd"));
            byte[] cEndTime = Encoding.Default.GetBytes(dateTimePicker13.Value.ToString("yyyyMMdd"));
            {
                byte btYear2 = cStartTime[2];
                byte btYear1 = cStartTime[3];
                byte Month2 = cStartTime[4];
                byte Month1 = cStartTime[5];
                byte Day2 = cStartTime[6];
                byte Day1 = cStartTime[7];
                veData.Add(btYear2);
                veData.Add(btYear1);
                veData.Add(Month2);
                veData.Add(Month1);
                veData.Add(Day2);
                veData.Add(Day1);
            }
            {
                byte btYear2 = cEndTime[2];
                byte btYear1 = cEndTime[3];
                byte Month2 = cEndTime[4];
                byte Month1 = cEndTime[5];
                byte Day2 = cEndTime[6];
                byte Day1 = cEndTime[7];
                veData.Add(btYear2);
                veData.Add(btYear1);
                veData.Add(Month2);
                veData.Add(Month1);
                veData.Add(Day2);
                veData.Add(Day1);
            }
            //-------------------------------------------------------//

            for (int i = 0; i < tcResidentCode.Length; i++)
            {
                veData.Add(tcResidentCode[i]);
            }

            int nRt = CHD.API.CHD603S.ConsumeCard_WriteBlockData(PortId, NetId, textBox45.Text, 1, nItem, veData.ToArray(), null, null);
            if (nRt == 0)
            {
                PrintMessage("写用户信息成功");
            }
            else
            {
                PrintMessage("写用户信息失败! 错误码: " + nRt);
            }


        }



        /// <summary>
        /// 读取电梯参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button39_Click(object sender, EventArgs e)
        {


            byte[] btData1 = new byte[260];
            byte[] btData2 = new byte[260];
            byte[] btData3 = new byte[260];
            int nRt = CHD.API.CHD603S.ConsumeCard_ReadBlockData(PortId, NetId, textBox52.Text, 11, 0, btData1, btData2, btData3);
            if (nRt == 0)
            {
                string strInfoView = Encoding.Default.GetString(btData1);
                string strInfoView1 = Encoding.Default.GetString(btData2);
                string strInfoView2 = Encoding.Default.GetString(btData3);
                string strTemp = "";

                {
                    {
                        strTemp += "起始时间: ";
                        strTemp += strInfoView.Substring(0, 2);
                        strTemp += " 年 ";
                        strTemp += strInfoView.Substring(2, 2);
                        strTemp += " 月 ";
                        strTemp += strInfoView.Substring(4, 2);
                        strTemp += " 日 ";
                        strTemp += strInfoView.Substring(6, 2);
                        strTemp += " 时 ";

                        strTemp += " 结束时间: ";
                        strTemp += strInfoView.Substring(8, 2);
                        strTemp += " 年 ";
                        strTemp += strInfoView.Substring(10, 2);
                        strTemp += " 月 ";
                        strTemp += strInfoView.Substring(12, 2);
                        strTemp += " 日 ";
                        strTemp += strInfoView.Substring(14, 2);
                        strTemp += " 时";

                        strTemp += " 权限: ";
                        strTemp += Convert.ToInt32(strInfoView.Substring(16, 2), 16);
                        strTemp += " 变更属性: ";
                        strTemp += Convert.ToInt32(strInfoView.Substring(18, 2), 16);

                        strTemp += " 变更时间: ";
                        strTemp += strInfoView.Substring(20, 2);
                        strTemp += " 年 ";
                        strTemp += strInfoView.Substring(22, 2);
                        strTemp += " 月 ";
                        strTemp += strInfoView.Substring(24, 2);
                        strTemp += " 日 ";
                        strTemp += strInfoView.Substring(26, 2);
                        strTemp += " 时";
                        strTemp += strInfoView.Substring(28, 2);
                        strTemp += " 分";
                    }
                    {
                        strTemp += " 住户密码: ";
                        strTemp += strInfoView1.Substring(0, 6);
                    }
                    {
                        strTemp += " 挂失用户卡号1: ";
                        strTemp += strInfoView2.Substring(0, 8);
                        strTemp += " 挂失用户卡号2: ";
                        strTemp += strInfoView2.Substring(8, 8);
                        strTemp += " 解挂用户卡号1: ";
                        strTemp += strInfoView2.Substring(16, 8);
                        strTemp += " 解挂用户卡号2: ";
                        strTemp += strInfoView2.Substring(24, 8);
                    }
                }
                PrintMessage("读电梯参数成功, " + strTemp);
            }
            else
            {
                PrintMessage("读电梯参数失败! 错误码: " + nRt);
            }
        }



        private byte BCDToByte(byte cA, byte cB)
        {
            byte cHByte = 0x00, cLByte = 0x00;
            if ((cA >= 48) && (cA <= 57))
            {
                cHByte = (byte)(cA - 48);
            }
            else
            {
                if ((cA >= 97) && (cA <= 102))
                {//小写字母
                    cHByte = (byte)(cA - 87);
                }
                else
                {//大写字母
                    cHByte = (byte)(cA - 55);
                }
            }

            if ((cB >= 48) && (cB <= 57))
            {//数字
                cLByte = (byte)(cB - 48);
            }
            else
            {
                if ((cB >= 97) && (cB <= 102))
                {//小写字母
                    cLByte = (byte)(cB - 87);
                }
                else
                {//大写字母
                    cLByte = (byte)(cB - 55);
                }
            }
            return (byte)(((cHByte << 4) + cLByte));
        }



        byte XORCheck(List<byte> veData)
        {
            byte btXor = 0x00;
            for (int i = 0; i < veData.Count; i++, i++)
            {
                byte btTemp = BCDToByte(veData[i], veData[i + 1]);
                btXor ^= btTemp;
            }
            return btXor;
        }


        /// <summary>
        /// 设置电梯参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button38_Click(object sender, EventArgs e)
        {

            string tcGrant = comboBox9.SelectedItem.ToString();

            byte[] tcPsw = Encoding.Default.GetBytes(textBox47.Text);
            byte[] tcLossCardNo1 = Encoding.Default.GetBytes(textBox48.Text);
            byte[] tcLossCardNo2 = Encoding.Default.GetBytes(textBox51.Text);
            byte[] tcFindCardNo1 = Encoding.Default.GetBytes(textBox49.Text);
            byte[] tcFindCardNo2 = Encoding.Default.GetBytes(textBox50.Text);

            int nItem = comboBox9.SelectedIndex;

            List<byte> veData = new List<byte>();
            List<byte> veData1 = new List<byte>();
            List<byte> veData2 = new List<byte>();

            {
                {
                    byte[] cStartDate = Encoding.Default.GetBytes(CHD.Common.CombinDateTime(dateTimePicker16.Value, dateTimePicker17.Value).ToString("yyyyMMddhh"));
                    byte[] cEndDate = Encoding.Default.GetBytes(CHD.Common.CombinDateTime(dateTimePicker18.Value, dateTimePicker19.Value).ToString("yyyyMMddhh"));

                    {
                        byte btYear2 = cStartDate[2];
                        byte btYear1 = cStartDate[3];
                        byte Month2 = cStartDate[4];
                        byte Month1 = cStartDate[5];
                        byte Day2 = cStartDate[6];
                        byte Day1 = cStartDate[7];
                        byte Hour2 = cStartDate[8];
                        byte Hour1 = cStartDate[9];
                        veData.Add(btYear2);
                        veData.Add(btYear1);
                        veData.Add(Month2);
                        veData.Add(Month1);
                        veData.Add(Day2);
                        veData.Add(Day1);
                        veData.Add(Hour2);
                        veData.Add(Hour1);
                    }
                    {
                        byte btYear2 = cEndDate[2];
                        byte btYear1 = cEndDate[3];
                        byte Month2 = cEndDate[4];
                        byte Month1 = cEndDate[5];
                        byte Day2 = cEndDate[6];
                        byte Day1 = cEndDate[7];
                        byte Hour2 = cEndDate[8];
                        byte Hour1 = cEndDate[9];
                        veData.Add(btYear2);
                        veData.Add(btYear1);
                        veData.Add(Month2);
                        veData.Add(Month1);
                        veData.Add(Day2);
                        veData.Add(Day1);
                        veData.Add(Hour2);
                        veData.Add(Hour1);
                    }
                }

                byte btGrant = byte.Parse(tcGrant);
                byte btGrant2 = ByteToBCD(btGrant, 2);
                byte btGrant1 = ByteToBCD(btGrant, 1);
                veData.Add(btGrant2);
                veData.Add(btGrant1);
                {
                    var checkBoxs = from Control c in groupBox12.Controls
                                    where c is CheckBox
                                    orderby c.Name descending
                                    select (CheckBox)c;
                    string ctrByte = "";
                    foreach (var item in checkBoxs)
                    {
                        ctrByte += (item.Checked) ? "1" : "0";
                    }
                    byte btEditAttribute = Convert.ToByte(ctrByte, 2);
                    byte btTemp2 = ByteToBCD(btEditAttribute, 2);
                    byte btTemp1 = ByteToBCD(btEditAttribute, 1);
                    veData.Add(btTemp2);
                    veData.Add(btTemp1);
                }
                {
                    byte[] cEditTime = Encoding.Default.GetBytes(CHD.Common.CombinDateTime(dateTimePicker14.Value, dateTimePicker15.Value).ToString("yyyyMMddHHmm"));

                    {
                        byte btYear2 = cEditTime[2];
                        byte btYear1 = cEditTime[3];
                        byte Month2 = cEditTime[4];
                        byte Month1 = cEditTime[5];
                        byte Day2 = cEditTime[6];
                        byte Day1 = cEditTime[7];
                        byte Hour2 = cEditTime[8];
                        byte Hour1 = cEditTime[9];
                        byte Minute2 = cEditTime[10];
                        byte Minute1 = cEditTime[11];
                        veData.Add(btYear2);
                        veData.Add(btYear1);
                        veData.Add(Month2);
                        veData.Add(Month1);
                        veData.Add(Day2);
                        veData.Add(Day1);
                        veData.Add(Hour2);
                        veData.Add(Hour1);
                        veData.Add(Minute2);
                        veData.Add(Minute1);
                    }
                }
                byte btXor = XORCheck(veData);
                byte btXor2 = ByteToBCD(btXor, 2);
                byte btXor1 = ByteToBCD(btXor, 1);
                veData.Add(btXor2);
                veData.Add(btXor1);
            }
            {
                for (int i = 0; i < tcPsw.Length; i++)
                {
                    veData1.Add(tcPsw[i]);
                }
                for (int i = 0; i < 24; i++)
                {
                    veData1.Add(0x30);
                }
                byte btXor = XORCheck(veData1);
                byte btXor2 = ByteToBCD(btXor, 2);
                byte btXor1 = ByteToBCD(btXor, 1);
                veData1.Add(btXor2);
                veData1.Add(btXor1);
            }
            {
                for (int i = 0; i < tcLossCardNo1.Length; i++)
                {
                    veData2.Add(tcLossCardNo1[i]);
                }
                for (int i = 0; i < tcLossCardNo2.Length; i++)
                {
                    veData2.Add(tcLossCardNo2[i]);
                }
                for (int i = 0; i < tcFindCardNo1.Length; i++)
                {
                    veData2.Add(tcFindCardNo1[i]);
                }
                for (int i = 0; i < tcFindCardNo2.Length; i++)
                {
                    veData2.Add(tcFindCardNo2[i]);
                }
            }


            int nRt = CHD.API.CHD603S.ConsumeCard_WriteBlockData(PortId, NetId, textBox52.Text, 11, 0, veData.ToArray(), veData1.ToArray(), veData2.ToArray());
            if (nRt == 0)
            {
                PrintMessage("设置电梯参数成功");
            }
            else
            {
                PrintMessage("设置电梯参数失败! 错误码:" + nRt);
            }
        }

    }
}
