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

namespace CHD806D4M3Demo
{
    public partial class CHD806D4M3 : Form
    {
        private int portId;
        private int[] ctrParam = new int[4];
        public CHD806D4M3()
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

        public CHD806D4M3(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数

        private void InitCmp()
        {
            cmbDoorId_open.SelectedIndex = 0;
            cmbDoorId_Ctr.SelectedIndex = 0;
            cmbDoorId_week.SelectedIndex = 0;
            cmbDoorId_specia.SelectedIndex = 0;
            dataGridView3.Rows.Add(new object[] { "星期一", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期二", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期三", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期四", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期五", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期六", 1, 1, 1, 1, 0, 0, 0, 0 });
            dataGridView3.Rows.Add(new object[] { "星期日", 1, 1, 1, 1, 0, 0, 0, 0 });

            dgTimeSpan.Rows.Add(new object[] { 0, "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00" });
            for (int i = 1; i <= 31; i++)
            {
                dgTimeSpan.Rows.Add(new object[] { i, "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            }
            dataGridView2.Rows.Add(new object[] { "1门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "2门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "3门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "4门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "5门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "6门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "7门互锁设置" });
            dataGridView2.Rows.Add(new object[] { "8门互锁设置" });
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 1; j < 9; ++j)
                {
                    if (i + 1 == j)
                    {
                        dataGridView2.Rows[i].Cells[j].Value = "--";
                    }
                    else
                    {
                        dataGridView2.Rows[i].Cells[j].Value = "×";
                    }
                }
            }


            var m_bit = (from Control cmb in button50.Parent.Controls
                         where cmb is ComboBox
                         orderby cmb.Tag ascending
                         select (ComboBox)cmb).ToList();
            foreach (var item in m_bit)
            {
                item.SelectedIndex = 0;

            }
            comboBox21.SelectedIndex = 0;
            comboBox22.SelectedIndex = 0;
            cmbDoorId_Other.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

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



        #region 卡/用户管理
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(dpUserLmtTime.Value);
            try
            {
                if (int.Parse(txtDooRight1.Text) == 0 || int.Parse(txtDooRight1.Text) > 255)
                {
                    MessageBox.Show("门1权限输入错误");
                    return;
                }
                if (int.Parse(txtDooRight2.Text) == 0 || int.Parse(txtDooRight2.Text) > 255)
                {
                    MessageBox.Show("门2权限输入错误");
                    return;
                }
                if (int.Parse(txtDooRight3.Text) == 0 || int.Parse(txtDooRight3.Text) > 255)
                {
                    MessageBox.Show("门3权限输入错误");
                    return;
                }
                if (int.Parse(txtDooRight4.Text) == 0 || int.Parse(txtDooRight4.Text) > 255)
                {
                    MessageBox.Show("门4权限输入错误");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请正确输入门权限");
                return;
            }

            int nRetValue = CHD.API.CHD806D4M3.DAddUserlEx(PortId, NetId,
                txtCardNo.Text,			//卡号
                txtUserId.Text,			//用户ID
                txtDooPwd.Text,		//开门密码
                ref LmtTime,				//有效期
                uint.Parse(txtDooRight1.Text), uint.Parse(txtDooRight2.Text), uint.Parse(txtDooRight3.Text), uint.Parse(txtDooRight4.Text));		//门权限
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
                    PrintMessage(String.Format("卡号:{0} 重复!", txtCardNo.Text));
                    break;
                case 0xE8:		//用户信息项全部重复设置
                    PrintMessage("用户信息项全部重复设置!");
                    break;
                default:		//其他值表示失败
                    PrintMessage("增加用户失败! " + nRetValue);
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

            StringBuilder szUserID = new StringBuilder();
            StringBuilder szPasswd = new StringBuilder();
            StringBuilder szRetCardNo = new StringBuilder();
            uint[] nDoorRight = new uint[4];
            CHD.API.SYSTEMTIME LmtTim;
            int nRetValue = CHD.API.CHD806D4M3.DReadUserInfoByCardNoEx(PortId, NetId,
                txtCardNo_R.Text,
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTim,
                nDoorRight, 4);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3}",
                            szRetCardNo,						//用户
                            szUserID,							//卡号
                            szPasswd,							//密码
                            CHD.Common.ParasTime(LmtTim)));	//有效期
                        for (int i = 0; i < 4; ++i)
                            PrintMessage(String.Format("第{0} 门权限:0x{1}", i + 1, nDoorRight[i].ToString("X2")));
                        break;
                    }
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage("读取用户信息失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 根据用户ID读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByUID_Click(object sender, EventArgs e)
        {
            StringBuilder szUserID = new StringBuilder();
            StringBuilder szPasswd = new StringBuilder();
            StringBuilder szRetCardNo = new StringBuilder();
            uint[] nDoorRight = new uint[4];
            CHD.API.SYSTEMTIME LmtTim;
            int nRetValue = CHD.API.CHD806D4M3.DReadUserInfoByUserIDEx(PortId, NetId,
                txtUserId_R.Text,
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTim,
                nDoorRight, 4);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3}",
                            szRetCardNo,						//用户
                            szUserID,							//卡号
                            szPasswd,							//密码
                            CHD.Common.ParasTime(LmtTim)));	//有效期
                        for (int i = 0; i < 4; ++i)
                            PrintMessage(String.Format("第{0} 门权限:0x{1}", i + 1, nDoorRight[i].ToString("X2")));
                        break;
                    }
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage("读取用户信息失败! 错误代码: " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// 根据存储位置读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByPos_Click(object sender, EventArgs e)
        {
            byte[] szUserID = new byte[12];
            byte[] szPasswd = new byte[12];
            byte[] szRetCardNo = new byte[12];
            uint[] nDoorRight = new uint[4];
            CHD.API.SYSTEMTIME LmtTim;
            int nRetValue = CHD.API.CHD806D4M3.DReadUserInfoByPosEx(PortId, NetId,
                uint.Parse(txtPosition_R.Text),
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTim,
                nDoorRight, 4);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3}",
                            Encoding.Default.GetString(szRetCardNo),						//用户
                            Encoding.Default.GetString(szUserID),							//卡号
                            Encoding.Default.GetString(szPasswd),							//密码
                            CHD.Common.ParasTime(LmtTim)));	//有效期
                        for (int i = 0; i < 4; ++i)
                            PrintMessage(String.Format("第{0} 门权限:0x{1}", i + 1, nDoorRight[i].ToString("X2")));
                        break;
                    }
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空");
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage("读取用户信息失败! 错误提示: SM内部无相应信息项");
                    break;
                default:		//其他值表示失败
                    PrintMessage("读取用户信息失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 根据卡号删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUserByCardNo_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D4M3.D4DelUserByCardNo(PortId, NetId, txtCardNo_R.Text, 0);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("删除卡号:{0} 成功! ", txtCardNo_R.Text));
                    break;
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
                    PrintMessage("读取用户信息失败! 错误代码: " + nRetValue);
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
            int nUserCount = 0;


            int nRetValue = CHD.API.CHD806D4M3.DReadUserCount(PortId, NetId, out nUserCount);
            if (nRetValue == 0x00)
            {
                textBox36.Text = nUserCount.ToString();

                PrintMessage("读取用户数量成功! 用户数量: " + nUserCount);
            }
            else
            {
                PrintMessage("读取用户数量失败! 错误代码: " + nRetValue);
            }

        }



        /// <summary>
        /// 清空用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllUser_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DDelAllUser(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("删除设备所有用户成功!");
                    break;
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
                    PrintMessage("读取用户信息失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取紧急开门密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadPwd_Click(object sender, EventArgs e)
        {
            byte[] szPwd1 = new byte[20];
            byte[] szPwd2 = new byte[20];
            int nRetValue = CHD.API.CHD806D4M3.DReadSuperPwdEx(PortId, NetId, (uint)(cmbDoorId_open.SelectedIndex + 1), szPwd1, szPwd2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    txtSuperPwd1.Text = Encoding.Default.GetString(szPwd1);
                    txtSuperPwd2.Text = Encoding.Default.GetString(szPwd2);
                    PrintMessage(String.Format("操作成功! 第 {0} 门密码1:{1} 密码2:{2} ", cmbDoorId_open.SelectedIndex + 1, Encoding.Default.GetString(szPwd1), Encoding.Default.GetString(szPwd2)));
                    break;
                default:		//其他值表示失败
                    PrintMessage("读取密码失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置紧急开门密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPwd_Click(object sender, EventArgs e)
        {
            if (txtSuperPwd1.Text.Length != 8 || txtSuperPwd2.Text.Length != 8)
            {
                MessageBox.Show("密码输入错误,长度应为8位"); 
                return;
            }
            int nRetValue = CHD.API.CHD806D4M3.DSetSuperPwdEx(PortId, NetId, (uint)(cmbDoorId_open.SelectedIndex + 1), txtSuperPwd1.Text.Trim(), txtSuperPwd2.Text.Trim(), "EEEEEEEE", "FFFFFFFF");
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置密码成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置密码失败! 错误提示: 无权限!");
                    break;
                default:		//其他值表示失败
                    PrintMessage("设置密码失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取特权卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSpeciaCard_Click(object sender, EventArgs e)
        {
            byte[] szCard1 = new byte[10];
            byte[] szCard2 = new byte[10];
            //string szCard1 = "1234567890";
            //string szCard2 = "1234567890";
            int nRetValue = CHD.API.CHD806D4M3.DReadSuperCardEx(PortId, NetId, (uint)(cmbDoorId_open.SelectedIndex + 1), szCard1, szCard2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    txtSuperCard1.Text = Encoding.Default.GetString(szCard1);
                    txtSuperCard2.Text = Encoding.Default.GetString(szCard2);

                    PrintMessage(String.Format("读取 {0} 门超级卡成功! 密码1:{1} 密码2:{2}", cmbDoorId_open.SelectedIndex + 1, Encoding.Default.GetString(szCard1), Encoding.Default.GetString(szCard2)));
                    break;
                default:		//其他值表示失败
                    PrintMessage("读取超级卡失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置特权卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSpeciaCard_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DSetSuperCardEx1(PortId, NetId, (uint)(cmbDoorId_open.SelectedIndex + 1), txtSuperCard1.Text, txtSuperCard2.Text, null, null);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("设置 {0} 超级卡成功!", cmbDoorId_open.SelectedIndex + 1));
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置超级卡失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置超级卡失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 删除特权卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSpeciaCard_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DDeleteSuperCardEx(PortId, NetId, (uint)(cmbDoorId_open.SelectedIndex + 1));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("删除超级卡成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置超级卡失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置超级卡失败! 代码: " + nRetValue);
                    break;
            }
        }
        #endregion



        #region 门禁参数设置

        private void SetTreeNodeChecked()
        {
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
                    treeView1.Nodes[i].Nodes[j].Checked = pm.Substring(j, 1) == "1" ? true : false;
                }
                treeView1.Nodes[i].Text = "控制字节" + i + "   " + ctrParam[i];
            }
        }


        /// <summary>
        /// 读取门禁参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAllCtrParam_Click(object sender, EventArgs e)
        {
            int RelayDelay = 0, OpenDelay = 0, IrSureDelay = 0, IrOnDelay = 0;


            int nRetValue = CHD.API.CHD806D4M3.DReadCtrlParamEx(PortId, NetId, cmbDoorId_Ctr.SelectedIndex + 1,//Read one door
                out ctrParam[0], out RelayDelay, out OpenDelay, out IrSureDelay, out IrOnDelay, out ctrParam[1], out ctrParam[2], out ctrParam[3]);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        txtRelayDelay.Text = RelayDelay.ToString();
                        txtOpenDelay.Text = OpenDelay.ToString();
                        txtIrSureDelay.Text = IrSureDelay.ToString();
                        txtIrOnDelay.Text = IrOnDelay.ToString();
                        SetTreeNodeChecked();
                        PrintMessage(String.Format("读取 {0} 门控制参数成功!", cmbDoorId_Ctr.SelectedIndex + 1));
                    }
                    break;
                default:
                    PrintMessage("读取控制参数失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置门禁参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOneByOne_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DSetCtrlParam(PortId, NetId, (uint)(cmbDoorId_Ctr.SelectedIndex + 1), (uint)(ctrParam[0]), uint.Parse(txtRelayDelay.Text), uint.Parse(txtOpenDelay.Text), uint.Parse(txtIrSureDelay.Text), uint.Parse(txtIrOnDelay.Text), (uint)ctrParam[1], (uint)ctrParam[2], (uint)ctrParam[3]);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("设置 {0} 门控制参数成功!", cmbDoorId_Ctr.SelectedIndex + 1));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("设置 {0} 门控制参数失败! 错误提示: 无权限!", cmbDoorId_Ctr.SelectedIndex + 1));
                    break;
                default:
                    PrintMessage(String.Format("设置{0} 门控制参数失败! 错误码:{1}", cmbDoorId_Ctr.SelectedIndex + 1, nRetValue));
                    break;
            }
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text.Contains("控制字节"))
            {
                e.Cancel = true;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text.Contains("控制字节"))
            {
                e.Node.Checked = false;
            }
            else
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
                e.Node.Parent.Text = string.Format("{0}{1}   {2}", "控制字节", e.Node.Parent.Index, number);
                ctrParam[e.Node.Parent.Index] = number;
            }
        }

        #endregion



        #region 记录管理
        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DInitRec(PortId, NetId);
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
        private void btnQueryRecStatu_Click(object sender, EventArgs e)
        {
            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0;
            int nRetValue = CHD.API.CHD806D4M3.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
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
        /// 当前设备记录总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecCount_Click(object sender, EventArgs e)
        {
            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0, nRecordCount = 0;

            int nRetValue = CHD.API.CHD806D4M3.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    if (nSaveP >= nLoadP)
                        nRecordCount = nSaveP - nLoadP;
                    else
                        nRecordCount = nMaxLen - (nLoadP - nSaveP);
                    PrintMessage("读取成功! 设备当前未读取记录数: " + nRecordCount);
                    break;
                default:		//Failed
                    PrintMessage("读取失败! 错误代码: " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// 设定读指针
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecovery_Click(object sender, EventArgs e)
        {
            int pointer = 0;
            try
            {
                pointer = Convert.ToInt32(txtRecovery.Text);
                if (pointer > 65535)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("LOADP 输入错误");
                return;
            }

            int nRetValue = CHD.API.CHD806D4M3.D4SetRecReadPoint(PortId, NetId, (uint)pointer);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(("设定读指针成功"));
                    break;
                default:		//Failed
                    PrintMessage("设定读指针失败! 错误代码: " + nRetValue);
                    break;
            }
        }
        private string FormatLog(byte[] szRec, DateTime RecTime, int nRecState, int nRecRemark, int nRecLineState)
        {
            string szRecSourceT = Encoding.Default.GetString(szRec);
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


        /// <summary>
        /// 应答方式读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadNewRec_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[20];
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int bExitLoop = 1, bAckFlag = 1;

            int nRetvalue = CHD.API.CHD806D4M3.DReadRecStart(PortId, NetId);
            if (nRetvalue != 0)
            {
                if (nRetvalue == 0xE4)
                {
                    PrintMessage(String.Format("无记录读取! 错误码: {0} 提示:设备内无记录", nRetvalue));
                }
                return;
            }
            PrintMessage("开始应答方式读取!");

            btnReadNewRec.Text = "正在读取...";
            btnReadNewRec.Enabled = false;

            while (bExitLoop == 1)
            {
                nRetvalue = CHD.API.CHD806D4M3.DReadRecAck(PortId, NetId, (uint)bAckFlag, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
                bAckFlag = 1;
                switch (nRetvalue)
                {
                    case 0x00:		//Success
                        bAckFlag = 0;
                        PrintMessage(String.Format("{0} 门: {1}", nDoorID, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
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
                        bExitLoop = 0;
                        PrintMessage("读取记录失败! 错误代码: " + nRetvalue);
                        break;
                }
            }
            btnReadNewRec.Enabled = true;
            btnReadNewRec.Text = "开始读取记录";

            //关闭应答模式
            nRetvalue = CHD.API.CHD806D4M3.DReadRecStop(PortId, NetId);
            if (nRetvalue == 0)
            {
                PrintMessage("关闭应答方式!");
            }
            else
            {
                PrintMessage("关闭应答方式失败! 错误码: " + nRetvalue);
            }
            btnReadNewRec.Text = "读取";
            btnReadNewRec.Enabled = true; ;
        }



        /// <summary>
        /// 查询最新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRec_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[20];
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            int nRetValue = CHD.API.CHD806D4M3.DReadOneNewRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("{0} 门: {1}", nDoorID, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
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
        /// 随机读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByPos_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[20];
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            int nRetValue = CHD.API.CHD806D4M3.DReadRecByPoint(PortId, NetId, short.Parse(txtRecovery.Text), szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success

                    PrintMessage(String.Format("{0} 门: 读取记录序号:[{1}]成功! {2}", nDoorID, txtRecovery.Text, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
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
        /// 顺序读取一条历史记录(带存储位置)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button59_Click(object sender, EventArgs e)
        {
            ReadRecordWithPoint();
        }


        private bool ReadRecordWithPoint()
        {

            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[20];
            int nRecPos = 0, nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            bool nRetFlag = false;

            int nRetValue = CHD.API.CHD806D4M3.DReadRecWithPoint(PortId, NetId, out nRecPos, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("{0} 门: 记录序号:{1} {2}", nDoorID, nRecPos, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
                    nRetFlag = true;
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage("设备所有记录已经读完!");
                    nRetFlag = false;
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码:" + nRetValue);
                    nRetFlag = false;
                    break;
            }
            return nRetFlag;
        }



        /// <summary>
        /// 顺序读取一条历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button58_Click(object sender, EventArgs e)
        {
            ReadOneRecord();
        }

        private bool ReadOneRecord()
        {

            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[20];
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            bool nRetFlag = false;

            int nRetValue = CHD.API.CHD806D4M3.DReadOneRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success

                    PrintMessage(String.Format("{0} 门: {1}", nDoorID, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
                    nRetFlag = true;
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
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



        #region 门禁时段设置
        private int FormatDoorID()
        {
            if (cmbDoorId_week.SelectedIndex < 4)
            {
                return cmbDoorId_week.SelectedIndex + 1;
            }
            else
            {
                return 0xff;
            }
        }


        private uint FormatDoorID1()
        {
            if (cmbDoorId_specia.SelectedIndex < 2)
            {
                return (uint)(cmbDoorId_specia.SelectedIndex + 1);
            }
            else
            {
                return 0xff;
            }
        }
        private void GetWeekListTime()
        {

            byte[] Time = new byte[8];

            PrintMessage("开始读星期时间段列表");
            for (int nWeek = 1; nWeek <= 7; ++nWeek)
            {
                int nRetValue = CHD.API.CHD806D4M3.DReadWeekTime(PortId, NetId, (uint)(FormatDoorID())/*门号*/, (uint)nWeek/*星期*/, Time);
                if (nRetValue == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        dataGridView3.Rows[nWeek-1].Cells[i+1].Value = Time[i];
                    }
                    //dataGridView3.UpdateCellValue(nWeek - 1, nWeek - 1);
                    //dataGridView3.Update();
                    PrintMessage(String.Format("读取 {0} 号门 星期{1} 的时段:[ {2} {3}  {4} {5} {6} {7} {8} {9} ]", FormatDoorID(), nWeek,Time[0], Time[1], Time[2], Time[3],Time[4], Time[5], Time[6], Time[7]));
                    System.Threading.Thread.Sleep(30);
                }
                else
                {
                    PrintMessage(String.Format("读取 {0} 号门 星期{1} 的时段失败!错误码: {2}", FormatDoorID(), nWeek, nRetValue));
                }
            }
            PrintMessage("星期时间段列表读取完毕!");
        }


        private void GetHolidayListTime()
        {

            int nRetValue = 0, nLmtMonth = 0, nLmtDay = 0;
            byte[] Time = new byte[8];
            //TCHAR strMsg[200] = {0};
            PrintMessage(("开始读取节、假日时间段列表"));
            dataGridView4.Rows.Clear();
            uint i = 0;
            do
            {
                nRetValue = CHD.API.CHD806D4M3.DReadHolidayTime(PortId, NetId, FormatDoorID1()/*门ID*/, ++i, out nLmtMonth, out nLmtDay, Time);
                if (nRetValue == 0)
                {
                    dataGridView4.Rows.Add(new object[] { nLmtMonth, nLmtDay, Time[0], Time[1], Time[2], Time[3], Time[4], Time[5], Time[6], Time[7] });
                    PrintMessage(String.Format(" 读取 {0}门 节假日:序号{1} {2} 月 {3} 日[ {4} {5} {6} {7} {8} {9} {10} {11} ]时段成功!", FormatDoorID1(), i, nLmtMonth, nLmtDay,
                    Time[0],Time[1],Time[2],Time[3],Time[4],Time[5],Time[6],Time[7]));
                }
                else if (nRetValue == 0xE5)
                {
                    break;
                }
                else
                {
                    PrintMessage(String.Format("读取 {0}门 节假日:序号{1} {2} 月 {3} 日 时段失败! 错误码: {4}", FormatDoorID1(), i - 1, nLmtMonth, nLmtDay, nRetValue));
                }
            } while (nRetValue == 0x00);
            PrintMessage(("节、假日时间段列表读取完毕!"));
        }



        private void SetWeekListTime()
        {
            String strTmp;
            String strTimeList = "";
            for (int i = 0; i < 7; ++i)
            {
                for (int j = 1; j < 9; ++j)
                {
                    strTmp = dataGridView3.Rows[i].Cells[j].Value.ToString();
                    strTmp = int.Parse(strTmp).ToString("X2"); //转换成16进制
                    strTimeList += strTmp;
                }
            }
            int nRetValue = CHD.API.CHD806D4M3.DSetWeekTime(PortId, NetId, (uint)FormatDoorID()/*门ID*/, new StringBuilder(strTimeList));
            if (nRetValue == 0)
            {
                PrintMessage(String.Format("星期时间段列表设置成功!\n\n [ {0} ]", strTimeList));
            }
            else if (nRetValue == 0x07)
            {
                PrintMessage(("星期时间段列表设置失败! 没有权限"));
            }
            else PrintMessage("星期时间段列表设置失败! 错误码:" + nRetValue);
        }



        private void SetHolidayListTime()
        {
            //char szMsg[200] = {0};
            int nLmtMonth = 5;
            int nLmtDay = 1;
            String strTimeList = "";
            String strTmp = "";
            int nMax = dataGridView4.Rows.Count;

            if (nMax == 0) return;
            for (int i = 0; i < nMax; ++i)
            {
                strTimeList = "";
                for (int j = 2; j < 10; ++j)
                {
                    strTmp = dataGridView4.Rows[i].Cells[j].Value.ToString();
                    strTmp = int.Parse(strTmp).ToString("X2"); //转换成16进制
                    strTimeList += strTmp;
                }
                nLmtMonth = int.Parse(dataGridView4.Rows[i].Cells[0].Value.ToString());
                nLmtMonth = (0 < nLmtMonth && nLmtMonth < 13) ? nLmtMonth : 5;

                nLmtDay = int.Parse(dataGridView4.Rows[i].Cells[1].Value.ToString());
                nLmtDay = (0 < nLmtDay && nLmtDay < 32) ? nLmtDay : 1;

                int nRetValue = CHD.API.CHD806D4M3.DSetHolidayTime(PortId, NetId, FormatDoorID1()/*门ID*/, (uint)nLmtMonth/*月*/, (uint)nLmtDay/*日*/, new StringBuilder(strTimeList));
                if (nRetValue == 0)
                {
                    PrintMessage(String.Format("设置节假日: {0} 月 {1} 日时段成功! [ {2} ]", nLmtMonth, nLmtDay, strTimeList));
                }
                else if (nRetValue == 0x07)
                {
                    PrintMessage(("二门星期时间段列表设置失败! 没有权限"));
                }
                else PrintMessage(String.Format("设置节假日: {0} 月 {1} 日时段失败! 错误码: {2}", nLmtMonth, nLmtDay, nRetValue));
            }
            PrintMessage(("设置二门节、假日时段完毕!"));
        }


        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            GetWeekListTime();
            GetHolidayListTime();
        }

        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add("增加特殊日期", null, (obj, ee) => { dataGridView4.Rows.Add(new object[] { 5, 1, 1, 1, 1, 1, 0, 0, 0, 0 }); });
                rightMenu.Items.Add("删除特殊日期", null, (obj, ee) => { dataGridView4.Rows.RemoveAt(e.RowIndex); });
                rightMenu.Show(Cursor.Position);
            }
        }



        /// <summary>
        /// 设置时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            SetWeekListTime();
            SetHolidayListTime();
        }
        #endregion



        #region 时间段设置
        /// <summary>
        /// 读取时间段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button56_Click(object sender, EventArgs e)
        {
            StringBuilder szTimeSlot = new StringBuilder();
            String strTemp, strItemText;
            for (int i = 0; i < 32; ++i)
            {
                int nRetValue = CHD.API.CHD806D4M3.DReadListTime(PortId, NetId, (uint)i, szTimeSlot);
                if (nRetValue == 0x00)
                {
                    strTemp = szTimeSlot.ToString();
                    for (int j = 1; j < 9; ++j)
                    {
                        strItemText = strTemp.Substring(j * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                        strItemText += ":";
                        strItemText += strTemp.Substring(j * 4 - 4 + 2, 2);
                        dgTimeSpan.Rows[i].Cells[j].Value = strItemText;
                    }
                    PrintMessage(String.Format("读取第{0}张表成功:[ {1} ]", i, strTemp));
                }
                else
                {
                    PrintMessage(String.Format("读取第{0}张表失败! 错误码: {1}", i, nRetValue));
                }
            }
        }




        /// <summary>
        /// 设置时间段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button55_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; ++i)
            {
                String strText = String.Empty;
                for (int j = 1; j < 9; ++j)
                {
                    strText += dgTimeSpan.Rows[i].Cells[j].Value.ToString();
                }
                strText = strText.Replace(":", "");
                int nRetValue = CHD.API.CHD806D4M3.DSetListTime(PortId, NetId, (uint)i, new StringBuilder(strText));
                if (nRetValue == 0)
                {
                    PrintMessage(String.Format("设置第{0}张表: {1} 成功!", i, strText));
                }
                else if (nRetValue == 0x07)
                {
                    PrintMessage(String.Format("设置第{0}张表: {1} 失败! 错误码: 无权限", i, strText));
                }
                else
                {
                    PrintMessage(string.Format("设置第{0}张表: {1} 失败! 错误码: {2}", i, strText, nRetValue));
                }
            }
            PrintMessage(("设置时间段完毕!"));
        }
        #endregion



        #region 门互锁设置
        /// <summary>
        /// 设置关联反遣返
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button50_Click(object sender, EventArgs e)
        {
            uint[] nLock = new uint[8];	// 支持到8门控制器
            String str = "";

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 5; ++j)
                {
                    if (i + 1 != j)
                    {
                        str = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        if (0 <= str.IndexOf(("√")))
                        {
                            nLock[i] |= (uint)(0x01 << (j - 1));
                        }
                    }
                }
            }
            Button btn = sender as Button;
            var m_bit = (from Control cmb in btn.Parent.Controls
                         where cmb is ComboBox
                         orderby cmb.Tag ascending
                         select (ComboBox)cmb).ToList();

            uint[] nData = new uint[8];
            for (int i = 0; i < 4; i++)
            {
                nData[i] = (uint)(m_bit[i].SelectedIndex * 128 + m_bit[i + 1].SelectedIndex * 64 + m_bit[i + 2].SelectedIndex * 32 + m_bit[i + 3].SelectedIndex * 16 + nLock[i]);
            }

            int nRetValue = CHD.API.CHD806D4M3.DSetAssociatedLock4(PortId, NetId, nData);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置关联反遣返成功!");

                    break;
                default:
                    PrintMessage("设置关联反遣返失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取门互锁设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button51_Click(object sender, EventArgs e)
        {
            uint[] nLock = new uint[8];	// 支持到8门控制器

            int nRetValue = CHD.API.CHD806D4M3.DReadInterlockEx4(PortId, NetId, nLock);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {
                        PrintMessage("读取互锁配置成功!");
                        for (int i = 0; i < 4; ++i)
                        {
                            for (int j = 1; j < 5; ++j)
                            {
                                if (i + 1 != j)
                                {
                                    if ((nLock[i] & (0x01 << (j - 1))) > 0)
                                        dataGridView2.Rows[i].Cells[j].Value = "√";
                                    else
                                        dataGridView2.Rows[i].Cells[j].Value = "×";
                                }
                            }
                            PrintMessage(String.Format("{0} 门互锁字节:{1}", i + 1, nLock[i]));
                        }
                        break;
                    }
                default:
                    PrintMessage("读取互锁配置失败! 代码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置门互锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button52_Click(object sender, EventArgs e)
        {
            uint[] nLock = new uint[8];
            String str = "";

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 5; ++j)
                {
                    if (i + 1 != j)
                    {
                        str = dataGridView2.Rows[i].Cells[j].Value.ToString();
                        if (0 <= str.IndexOf("√"))
                        {
                            nLock[i] |= (uint)(0x01 << (j - 1));
                        }
                    }
                }
            }

            int nRetValue = CHD.API.CHD806D4M3.DSetInterlockEx4(PortId, NetId, nLock);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置互锁配置成功!");

                    break;
                default:
                    PrintMessage("设置互锁配置失败! 代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置关联反遣返
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button54_Click(object sender, EventArgs e)
        {


            if (comboBox21.SelectedIndex == 0)
            {
                int nRetValue = CHD.API.CHD806D4M3.DClearRestrict4(PortId, NetId, (uint)comboBox21.SelectedIndex + 1, textBox51.Text);
                switch (nRetValue)
                {
                    case 0x00:		//成功
                        PrintMessage("清除反遣返人员限制成功!");

                        break;
                    default:
                        PrintMessage("清除反遣返人员限制失败! 代码: " + nRetValue);
                        break;
                }
            }
            else
            {
                int nRetValue = CHD.API.CHD806D4M3.DClearRestrict4_1(PortId, NetId, (uint)comboBox21.SelectedIndex + 1, (uint)comboBox22.SelectedIndex + 1);
                switch (nRetValue)
                {
                    case 0x00:		//成功
                        PrintMessage("清除反遣返人员限制成功!");

                        break;
                    default:
                        PrintMessage("清除反遣返人员限制失败! 代码:" + nRetValue);
                        break;
                }
            }
        }



        /// <summary>
        /// 设置区域次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button53_Click(object sender, EventArgs e)
        {


            if (textBox47.Text.Length == 0 || int.Parse(textBox47.Text) > 255)
            {
                MessageBox.Show("第1门的次数输入错误");
                return;
            }
            if (textBox48.Text.Length == 0 || int.Parse(textBox48.Text) > 255)
            {
                MessageBox.Show("第2门的次数输入错误");
                return;
            }
            if (textBox49.Text.Length == 0 || int.Parse(textBox49.Text) > 255)
            {
                MessageBox.Show("第3门的次数输入错误");
                return;
            }
            if (textBox50.Text.Length == 0 || int.Parse(textBox50.Text) > 255)
            {
                MessageBox.Show("第4门的次数输入错误");
                return;
            }

            int nRetValue = CHD.API.CHD806D4M3.DSetSectionCount(PortId, NetId, uint.Parse(textBox47.Text), uint.Parse(textBox48.Text), uint.Parse(textBox49.Text), uint.Parse(textBox50.Text));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置区域次数限制成功!");

                    break;
                default:
                    PrintMessage("设置区域次数限制失败! 代码: " + nRetValue);
                    break;
            }

        }
        #endregion



        #region 其他

        /// <summary>
        /// 开启常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAlwaysOpenDoor_Click(object sender, EventArgs e)
        {
            uint doorId = cmbDoorId_Other.SelectedIndex == 8 ? 255 : (uint)cmbDoorId_Other.SelectedIndex + 1;
            int nRetValue = CHD.API.CHD806D4M3.DAlwaysOpenDoor(PortId, NetId, doorId, uint.Parse(textBox43.Text), txtOperator.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("开启常开门成功!");
                    break;
                default:
                    PrintMessage("开启常开门失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 取消常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAlwaysOpenDoor_Click(object sender, EventArgs e)
        {
            uint doorId = cmbDoorId_Other.SelectedIndex == 8 ? 255 : (uint)cmbDoorId_Other.SelectedIndex + 1;
            int nRetValue = CHD.API.CHD806D4M3.DAlwaysOpenDoor(PortId, NetId, doorId, 0, txtOperator.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("取消常开门成功!");
                    break;
                default:
                    PrintMessage("取消常开门失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 开启常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAlwaysCloseDoor_Click(object sender, EventArgs e)
        {
            uint doorId = cmbDoorId_Other.SelectedIndex == 8 ? 255 : (uint)cmbDoorId_Other.SelectedIndex + 1;
            int nRetValue = CHD.API.CHD806D4M3.DAlwaysCloseDoor(PortId, NetId, doorId, uint.Parse(textBox43.Text), txtOperator.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("开启常闭门成功!");
                    break;
                default:
                    PrintMessage("开启常闭门失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 取消常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAlwaysCloseDorr_Click(object sender, EventArgs e)
        {
            uint doorId = cmbDoorId_Other.SelectedIndex == 8 ? 255 : (uint)cmbDoorId_Other.SelectedIndex + 1;
            int nRetValue = CHD.API.CHD806D4M3.DAlwaysCloseDoor(PortId, NetId, doorId, 0, txtOperator.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("取消常闭门成功!");
                    break;
                default:
                    PrintMessage("取消常闭门失败! 错误码:" + nRetValue);
                    break;
            }
        }

        


        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            int nRerValue;

            if (!chkWithOpr.Checked)
            {
                nRerValue = CHD.API.CHD806D4M3.DRemoteOpenDoor(PortId, NetId, (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF));
            }
            else
            {

                nRerValue = CHD.API.CHD806D4M3.DRemoteOpenDoorWithUser(PortId, NetId, (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF), txtOperator.Text);
            }

            if (nRerValue == 0x00)
            {
                if (chkWithOpr.Checked)
                    PrintMessage(String.Format("远程开 {0} 门成功! 操作员:{1}", (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF), txtOperator.Text));
                else
                    PrintMessage(String.Format("远程开 {0} 门成功!", (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF)));
            }
            else if (nRerValue == 0x07)
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误提示: 没有权限!", (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF)));
            }
            else
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误码:{1}", (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF), nRerValue));
            }
        }



        /// <summary>
        /// 读取剩余时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadlastTime_Click(object sender, EventArgs e)
        {
            uint nState = 0, nDelay = 0;
            uint doorId = (uint)((0 <= cmbDoorId_Other.SelectedIndex && cmbDoorId_Other.SelectedIndex < 8) ? cmbDoorId_Other.SelectedIndex + 1 : 0xFF);

            int nRerValue = CHD.API.CHD806D4M3.DReadAlwaysStateEx(PortId, NetId, doorId, out nState, out nDelay);
            if (nRerValue == 0x00)
            {
                PrintMessage(String.Format("读取常开/闭门成功! 门号:{0} 状态字节:{1}, 剩余时间:{2} 分钟", doorId, nState.ToString("X1"), nDelay));
            }
            else if (nRerValue == 0x07)
            {
                PrintMessage("读取常开/闭门失败! 错误提示: 没有权限!");
            }
            else
            {
                PrintMessage("读取常开/闭门失败! 错误码:" + nRerValue);
            }
        }



        /// <summary>
        /// 读取时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime;

            int nRetValue = CHD.API.CHD806D4M3.DReadDateTime(PortId, NetId, out stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("读取时间：{0} 星期{1}", CHD.Common.ParasTime(stime), stime.wDayOfWeek));
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
        private void btnSetTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime = CHD.Common.ParasTime(DateTime.Now);
            int nRetValue = CHD.API.CHD806D4M3.DSetDateTime(PortId, NetId, ref stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("同步时间成功：{0} 星期{1}", CHD.Common.ParasTime(stime), stime.wDayOfWeek));
                    break;
                default:
                    PrintMessage("同步时间失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设备版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadVesion_Click(object sender, EventArgs e)
        {
            StringBuilder szVersion = new StringBuilder();

            int nRetValue = CHD.API.CHD806D4M3.DReadVersion1(PortId, NetId, szVersion);
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



        /// <summary>
        /// 监控状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadMoniteState_Click(object sender, EventArgs e)
        {
            uint[] nWorkState = new uint[8];
            uint[] nLineState = new uint[8];
            int nRetValue = CHD.API.CHD806D4M3.DReadDoorStateEx(PortId, NetId, nWorkState, nLineState, 8/* 8 or 4 */);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("读取监控状态成功!");
                        for (int i = 0; i < 8; ++i)
                        {
                            PrintMessage(String.Format("第{0}门工作状态字：{1} 第一门线路状态字：{2} ", i + 1, nWorkState[i].ToString("X2"), nLineState[i].ToString("X2")));
                        }
                        break;
                    }
                default:
                    PrintMessage("读取设备信息失败! 错误码: " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// 读取主动上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUploadChanner_Click(object sender, EventArgs e)
        {
            int nChannel, nHost;


            int nRetValue = CHD.API.CHD806D4M3.DReadTransmitChannel(PortId, NetId, out nChannel, out nHost);
            switch (nRetValue)
            {
                case 0x00:
                    textBox46.Text = nChannel.ToString();

                    PrintMessage("读取主动上传通道成功! 上传通道成功: " + nChannel);
                    break;
                default:
                    PrintMessage("读取主动上传通道失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置主动上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetUpLoadChanner_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DSetTransmitChannel(PortId, NetId, uint.Parse(textBox46.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置主动上传通道成功! ");
                    break;
                default:
                    PrintMessage("设置主动上传通道失败! 错误码: " + nRetValue);
                    break;
            }
        }



        private uint FormatRelay()
        {
            if (comboBox2.SelectedIndex < 5)
            {
                return (uint)comboBox2.SelectedIndex;
            }
            else
            {
                return 0xff;
            }
        }


        /// <summary>
        /// 开启报警继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAlarm_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DOpenAlarm(PortId, NetId, FormatRelay());
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("开启 {0} 号报警继电器成功!", FormatRelay()));
                    break;
                default:
                    PrintMessage(String.Format("开启 {0} 号报警继电器失败! 错误码: {1}", FormatRelay(), nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 关闭报警继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAlarm_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DCloseAlarm(PortId, NetId, FormatRelay());
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("关闭 {0} 号报警继电器成功!", FormatRelay()));
                    break;
                default:
                    PrintMessage(String.Format("关闭{0}号报警继电器失败! 错误码: {1}", FormatRelay(), nRetValue));
                    break;
            }

        }



        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetBudruer_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DSetBaudrate(PortId, NetId, (uint)comboBox3.SelectedIndex);
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
        /// 解除胁迫报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnAlarm_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DMenaceClose(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("关闭胁迫报警成功!");
                    break;
                default:
                    PrintMessage("关闭胁迫报警失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOn_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DLinkOn(PortId, NetId, textBox44.Text, textBox45.Text);
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
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOff_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DLinkOff(PortId, NetId);
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
        private void chkWithOpr_CheckedChanged(object sender, EventArgs e)
        {
            txtOperator.Enabled =btnOpenAlwaysOpenDoor.Enabled=btnCloseAlwaysOpenDoor.Enabled=btnOpenAlwaysCloseDoor.Enabled=btnCloseAlwaysCloseDorr.Enabled= chkWithOpr.Checked;
        }
        #endregion




    }
}
