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

namespace CHD806D2CPDemo
{
    public partial class CHD806D2CP : Form
    {
        private int portId;
        private int[] ctrParam = new int[4];
        public CHD806D2CP()
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

        public CHD806D2CP(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数

        private void InitCmp()
        {
            cmbDoor1Privilege.SelectedIndex = 0;
            cmbDoor2Privilege.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            cmbDoorId_Ctr.SelectedIndex = 0;
            cmbDoorId_week.SelectedIndex = 0;
            cmbDoorId_specia.SelectedIndex = 0;
            dgTimeSpan.Rows.Add(new object[] { 0, "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00" });
            for (int i = 1; i <= 31; i++)
            {
                dgTimeSpan.Rows.Add(new object[] { i, "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
            }
            m_WeekList.Rows.Add(new object[] { "星期一", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期二", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期三", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期四", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期五", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期六", 1, 1, 1, 1, 0, 0, 0, 0 });
            m_WeekList.Rows.Add(new object[] { "星期日", 1, 1, 1, 1, 0, 0, 0, 0 });


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
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdduser_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(dpLmtTime.Value);

            uint nDoor1Privilege = 0;
            switch (cmbDoor1Privilege.SelectedIndex)
            {
                case 0:
                    nDoor1Privilege = 0x40;	//第一类卡
                    break;
                case 1:
                    nDoor1Privilege = 0x41;	//第二类卡
                    break;
                case 2:
                    nDoor1Privilege = 0x42;	//第三类卡
                    break;
                case 3:
                    nDoor1Privilege = 0x43;	//第四类卡
                    break;
                case 4:
                    nDoor1Privilege = 0xC0;	//特权卡
                    break;
                default:
                    nDoor1Privilege = 0x40;	//第一类卡
                    break;
            }



            int nRetValue = CHD.API.CHD806D2CP.DAddUserEx(PortId, NetId,
                txtAddCardNo.Text,			//卡号
                txtAddUserID.Text,			//用户ID
                txtAddDoorPwd.Text,		//开门密码
                ref LmtTime,				//有效期
                ref nDoor1Privilege, 0);		//门权限
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(string.Format("增加用户成功! 卡号:{0} 用户ID:{1} ", txtAddCardNo.Text, txtAddUserID.Text));
                    break;
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE2:		//SM内已满
                    PrintMessage("SM内已满!");
                    break;
                case 0xE6:		//用户ID号重复
                    PrintMessage(String.Format("用户ID:{0} 重复!", txtAddUserID.Text));
                    break;
                case 0xE7:		//卡号重复
                    PrintMessage("卡号:" + txtAddCardNo.Text + "重复!");
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
        private void button18_Click(object sender, EventArgs e)
        {
            byte[] szRetCardNo = new byte[11];
            byte[] szUserID = new byte[11];
            byte[] szPasswd = new byte[11];
            int nDoorRight1, nDoorRight2;
            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(DateTime.Now);


            int nRetValue = CHD.API.CHD806D2CP.DReadUserInfoByCardNo(PortId, NetId,
                textBox26.Text,
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTime,
                out nDoorRight1, out nDoorRight2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3}  第1门权限：{4},第二门权限：{5}",
                           Encoding.Default.GetString(szRetCardNo),							//卡号
                           Encoding.Default.GetString(szUserID),						//用户
                           Encoding.Default.GetString(szPasswd),							//密码
                            CHD.Common.ParasTime(LmtTime), nDoorRight1.ToString("X2"), nDoorRight2.ToString("X2")));	//有效期
                        //for ( UINT i=0; i<0; ++i)
                        //    PrintMessage("第 %d 门权限:0x%02x", i+1, nDoorRight[i] );
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
        private void button19_Click(object sender, EventArgs e)
        {
            byte[] szRetCardNo = new byte[11];
            byte[] szUserID = new byte[11];
            byte[] szPasswd = new byte[11];
            int nDoorRight1, nDoorRight2;
            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(DateTime.Now);


            int nRetValue = CHD.API.CHD806D2CP.DReadUserInfoByUserID(PortId, NetId,
                textBox30.Text,
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTime,
                out nDoorRight1, out nDoorRight2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3} 第1门权限：{4},第二门权限：{5}",
                        Encoding.Default.GetString(szRetCardNo),							//卡号
                           Encoding.Default.GetString(szUserID),						//用户
                           Encoding.Default.GetString(szPasswd),							//密码
                            CHD.Common.ParasTime(LmtTime), nDoorRight1.ToString("X2"), nDoorRight2.ToString("X2")));	//有效期
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
        private void button20_Click(object sender, EventArgs e)
        {
            byte[] szRetCardNo = new byte[11];
            byte[] szUserID = new    byte[11];
            byte[] szPasswd = new    byte[11];
            int nDoorRight1, nDoorRight2;
            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(DateTime.Now);


            int nRetValue = CHD.API.CHD806D2CP.DReadUserInfoByPos(PortId, NetId,
                uint.Parse(textBox31.Text),
                szRetCardNo,
                szUserID,
                szPasswd,
                out LmtTime,
                out nDoorRight1, out nDoorRight2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    {

                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 开门密码:{2} 有效期:{3} 第1门权限：{4},第二门权限：{5}",
                           Encoding.Default.GetString(szRetCardNo),							//卡号
                           Encoding.Default.GetString(  szUserID),						//用户
                           Encoding.Default.GetString( szPasswd),							//密码
                            CHD.Common.ParasTime(LmtTime), nDoorRight1.ToString("X2"), nDoorRight2.ToString("X2")));	//有效期
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
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DDelUserByCardNo(PortId, NetId, textBox26.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("删除卡号:{0} 成功! ", textBox26.Text));
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
        /// 读取用户计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            int nUserCount = 0;


            int nRetValue = CHD.API.CHD806D2CP.DReadUserCount(PortId, NetId, out nUserCount);
            if (nRetValue == 0x00)
            {
                textBox40.Text = nUserCount.ToString();

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
        private void button50_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DDelAllUser(PortId, NetId);
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
                    PrintMessage("读取用户信息失败! 错误代码:" + nRetValue);
                    break;
            }

        }




        /// <summary>
        /// 读取密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {

            byte[] szPwd1 = new byte[10];
            byte[] szPwd2 = new byte[10];
            byte[] szPwd3 = new byte[10];
            byte[] szPwd4 = new byte[10];
            int nRetValue = 0;


            nRetValue = CHD.API.CHD806D2CP.DReadSuperPwd(PortId, NetId, szPwd1, szPwd2, szPwd3, szPwd4);
            if (0x00 == nRetValue)
            {

                PrintMessage(String.Format("操作成功! 第一门密码1:{0} 第一门密码2:{1} 第二门密码1:{2} 第二门密码2:{3} ",
                    Encoding.Default.GetString(szPwd1), Encoding.Default.GetString(szPwd2), Encoding.Default.GetString(szPwd3), Encoding.Default.GetString(szPwd4)));
            }
            else
            {
                PrintMessage("操作失败! 错误码:" + nRetValue);
            }
        }




        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            if (textBox32.Text == textBox33.Text)
            {
                MessageBox.Show("密码1和密码2不能相同");
                return;
            }
            if (textBox34.Text == textBox38.Text)
            {
                MessageBox.Show("密码3和密码4不能相同");
                return;
            }
            int nRetValue = CHD.API.CHD806D2CP.DSetSuperPwd(PortId, NetId, textBox32.Text, textBox33.Text, textBox34.Text, textBox38.Text);
            switch (nRetValue)
            {
                case 0x00:		//无权限
                    PrintMessage("设置密码成功");
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
        private void button26_Click(object sender, EventArgs e)
        {
            StringBuilder szCard1 = new StringBuilder();
            StringBuilder szCard2 = new StringBuilder();
            StringBuilder szCard3 = new StringBuilder();
            StringBuilder szCard4 = new StringBuilder();
            int nRetValue = CHD.API.CHD806D2CP.DReadSuperCard(PortId, NetId, szCard1, szCard2, szCard3, szCard4);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功
                        textBox35.Text = szCard1.ToString();
                        textBox36.Text = szCard2.ToString();
                        textBox37.Text = szCard3.ToString();
                        textBox39.Text = szCard4.ToString();
                        PrintMessage(string.Format("读取门超级卡成功! 卡1:{0} 卡2:{1}  卡3：{2}  卡4：{3}", szCard1, szCard2, szCard3, szCard4));
                        break;
                    }
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
        private void button27_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DSetSuperCardEx(PortId, NetId, textBox35.Text, textBox36.Text, textBox37.Text, textBox39.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("设置超级卡成功!");
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
        private void button28_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DDeleteSuperCardEx(PortId, NetId, 0);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage("删除超级卡成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("删除超级卡失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("删除超级卡失败! 代码:" + nRetValue);
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

            int doorID = cmbDoorId_Ctr.SelectedIndex == 2 ? 255 : cmbDoorId_Ctr.SelectedIndex + 1;
            int nRetValue = CHD.API.CHD806D2CP.DReadCtrlParamEx(PortId, NetId, doorID,//Read one door
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
            uint doorID = (uint)(cmbDoorId_Ctr.SelectedIndex == 2 ? 255 : cmbDoorId_Ctr.SelectedIndex + 1);
            int nRetValue = CHD.API.CHD806D2CP.DSetCtrlParam(PortId, NetId, doorID, (uint)(ctrParam[0]), uint.Parse(txtRelayDelay.Text), uint.Parse(txtOpenDelay.Text), uint.Parse(txtIrSureDelay.Text), uint.Parse(txtIrOnDelay.Text), (uint)ctrParam[1], (uint)ctrParam[2], (uint)ctrParam[3]);
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
        /// 顺序读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByOrder_Click(object sender, EventArgs e)
        {

        }




        /// <summary>
        /// 初始化记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DInitRec(PortId, NetId);
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
            int nRetValue = CHD.API.CHD806D2CP.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
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
        private void button31_Click(object sender, EventArgs e)
        {

            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0, nRecordCount = 0;

            int nRetValue = CHD.API.CHD806D2CP.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
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
        /// <summary>
        /// 读取设备最新的一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadNewRec_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            byte[] szRecSource = new byte[32];
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int nRetValue = CHD.API.CHD806D2CP.DReadOneNewRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(FormatLog(new StringBuilder(Encoding.Default.GetString(szRecSource)), CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState));
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
        /// 按位置读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByPos_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            int nRetValue = CHD.API.CHD806D2CP.DReadRecByPoint(PortId, NetId, short.Parse(txtRecPos.Text), szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
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

        private void btnReadRec_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region 时间段设置
        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            StringBuilder szTimeSlot = new StringBuilder();
            String strTemp, strItemText;
            for (int i = 0; i < 32; ++i)
            {
                int nRetValue = CHD.API.CHD806D2CP.DReadListTime(PortId, NetId, (uint)i, szTimeSlot);
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
        /// 设置时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
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


        #region 其他
        /// <summary>
        /// 读取时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime;
            int nRetValue = CHD.API.CHD806D2CP.DReadDateTime(PortId, NetId, out stime);
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
        private void btnSetTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime = CHD.Common.ParasTime(DateTime.Now);

            int nRetValue = CHD.API.CHD806D2CP.DSetDateTime(PortId, NetId, ref stime);
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
        /// 设备版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadVersion_Click(object sender, EventArgs e)
        {

            StringBuilder szVersion = new StringBuilder();

            int nRetValue = CHD.API.CHD806D2CP.DReadVersion(PortId, NetId, szVersion);
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
        private void btnReadState_Click(object sender, EventArgs e)
        {
            int pnWorkState1, pnLineState1;
            int pnWorkState2, pnLineState2;

            int nRetValue = CHD.API.CHD806D2CP.DReadDoorState(PortId, NetId, out  pnWorkState1, out  pnLineState1, out  pnWorkState2, out  pnLineState2);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("读取监控状态成功!");
                        PrintMessage(String.Format("第{0}门工作状态字：{1} 线路状态字：{2} ", 1, pnWorkState1.ToString("X2"), pnLineState1.ToString("X2")));
                        PrintMessage(String.Format("第{0}门工作状态字：{1} 线路状态字：{2} ", 2, pnWorkState2.ToString("X2"), pnLineState2.ToString("X2")));

                        break;
                    }
                default:
                    PrintMessage("读取设备信息失败! 错误码: " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button39_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DLinkOn(PortId, NetId, textBox42.Text, textBox41.Text);
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
        private void button42_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DLinkOff(PortId, NetId);
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
        /// 更改访问密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button41_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DNewPwd(PortId, NetId, textBox42.Text, textBox41.Text);
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
        /// 解除胁迫报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button40_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DMenaceClose(PortId, NetId);
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
        /// 开启报警继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button38_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DOpenAlarm(PortId, NetId, Convert.ToUInt16(comboBox5.SelectedItem));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("开启 {0} 号报警继电器成功!", comboBox5.SelectedItem.ToString()));
                    break;
                default:
                    PrintMessage(String.Format("开启 {0} 号报警继电器失败! 错误码: {1}", comboBox5.SelectedItem.ToString(), nRetValue));
                    break;
            }
        }




        /// <summary>
        /// 关闭报警继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button49_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DCloseAlarm(PortId, NetId, Convert.ToUInt16(comboBox5.SelectedItem));// 这里也需要转换
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("关闭报警继电器{0}成功!", comboBox5.SelectedItem.ToString()));
                    break;
                default:
                    PrintMessage(String.Format("关闭报警继电器{0}失败! 错误码: %d", comboBox5.SelectedItem.ToString(), nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            int nChannel, nHost;


            int nRetValue = CHD.API.CHD806D2CP.DReadTransmitChannel(PortId, NetId, out nChannel, out nHost);
            switch (nRetValue)
            {
                case 0x00:
                    textBox44.Text = nChannel.ToString();

                    PrintMessage("读取主动上传通道成功! 上传通道成功: " + nChannel);
                    break;
                default:
                    PrintMessage("读取主动上传通道失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D4M3.DSetTransmitChannel(PortId, NetId, uint.Parse(textBox44.Text));
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox45.Enabled = btnOpenAlwaysOpenDoor.Enabled = btnCloseAlwaysOpenDoor.Enabled = checkBox1.Checked;
            btnOpenAlwaysCloseDoor.Enabled = btnCloseAlwaysCloseDoor.Enabled = btnAlwaysOpenDoorState.Enabled = checkBox1.Checked;
        }



        private uint FormatDoorID()
        {
            if (comboBox5.SelectedIndex > 1)
            {
                return 255;
            }
            else
            {
                return (uint)(comboBox5.SelectedIndex + 1);
            }
        }


        /// <summary>
        /// 开启常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAlwaysOpenDoor_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D2CP.DAlwaysOpenDoor(PortId, NetId, FormatDoorID(), uint.Parse(textBox43.Text), textBox45.Text);
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
            int nRetValue = CHD.API.CHD806D2CP.DAlwaysOpenDoor(PortId, NetId, FormatDoorID(), 0, textBox45.Text);
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
            int nRetValue = CHD.API.CHD806D2CP.DAlwaysCloseDoor(PortId, NetId, FormatDoorID(), uint.Parse(textBox43.Text), textBox45.Text);
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
        private void btnCloseAlwaysCloseDoor_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2CP.DAlwaysCloseDoor(PortId, NetId, FormatDoorID(), 0, textBox45.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("关闭常闭门成功!");
                    break;
                default:
                    PrintMessage("关闭常闭门失败! 错误码:" + nRetValue);
                    break;
            }
        }





        /// <summary>
        /// 常开门状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlwaysOpenDoorState_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button48_Click(object sender, EventArgs e)
        {
            int nRerValue;


            if (checkBox1.Checked)
            {
                nRerValue = CHD.API.CHD806D2CP.DRemoteOpenDoor(PortId, NetId, FormatDoorID());
            }
            else
            {

                nRerValue = CHD.API.CHD806D2CP.DRemoteOpenDoorWithUser(PortId, NetId, FormatDoorID(), textBox45.Text);
            }

            if (nRerValue == 0x00)
            {
                if (checkBox1.Checked)
                    PrintMessage(String.Format("远程开 {0} 门成功! 操作员:{1}", FormatDoorID(), textBox45.Text));
                else
                    PrintMessage(String.Format("远程开 {0} 门成功!", FormatDoorID()));
            }
            else if (nRerValue == 0x07)
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误提示: 没有权限!", FormatDoorID()));
            }
            else
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误码:{1}", FormatDoorID(), nRerValue));
            }
        }

        #endregion

        private void GetWeekListTime()
        {
            byte[] Time = new byte[64];
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);
            PrintMessage("开始读星期时间段列表");
            for (int nWeek = 1; nWeek <= 7; ++nWeek)
            {
                int nRetValue = CHD.API.CHD806D2CP.DReadWeekTime(PortId, NetId, doorId/*转换后的门号*/, (uint)nWeek/*星期*/, Time);
                if (nRetValue == 0)
                {
                    m_WeekList.Rows[nWeek - 1].Cells[1].Value = Time[0];
                    m_WeekList.Rows[nWeek - 1].Cells[2].Value = Time[1];
                    m_WeekList.Rows[nWeek - 1].Cells[3].Value = Time[2];
                    m_WeekList.Rows[nWeek - 1].Cells[4].Value = Time[3];
                    m_WeekList.Rows[nWeek - 1].Cells[5].Value = Time[4];
                    m_WeekList.Rows[nWeek - 1].Cells[6].Value = Time[5];
                    m_WeekList.Rows[nWeek - 1].Cells[7].Value = Time[6];
                    m_WeekList.Rows[nWeek - 1].Cells[8].Value = Time[7];
                    PrintMessage(String.Format("读取 {0} 号门 星期{1} 的时段:[ {2} {3} {4} {5} {6} {7} {8} {9} ]", doorId, nWeek,
                        Time[0], Time[1], Time[2], Time[3],
                        Time[4], Time[5], Time[6], Time[8]));
                    System.Threading.Thread.Sleep(30);
                }
                else
                {
                    PrintMessage(String.Format("读取 {0} 号门 星期{1} 的时段失败!错误码: {2}", doorId, nWeek, nRetValue));
                }
            }
            PrintMessage("星期时间段列表读取完毕!");

        }




        private void SetWeekListTime()
        {
            String strTmp;
            String strTimeList = "";
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);
            for (int i = 0; i < 7; ++i)
            {
                for (int j = 1; j < 9; ++j)
                {
                    strTmp = m_WeekList.Rows[i].Cells[j].Value.ToString();
                    strTimeList += (Convert.ToInt16(strTmp).ToString("X2"));
                }
            }
            byte[] cInfo = Encoding.Default.GetBytes(strTimeList);
            int nRetValue = CHD.API.CHD806D2CP.DSetWeekTime(PortId, NetId, doorId/*转换后的门号*/, cInfo);
            if (nRetValue == 0)
            {
                PrintMessage("星期时间段列表设置成功!\n\n [ " + strTimeList + "]");
            }
            else if (nRetValue == 0x07)
            {
                PrintMessage("星期时间段列表设置失败! 没有权限");
            }
            else PrintMessage("星期时间段列表设置失败! 错误码:" + nRetValue);
        }



        private void SetHolidayListTime()
        {
            int nLmtMonth = 5;
            int nLmtDay = 1;
            String strTimeList = "";
            String strTmp = "";
            int nMax = m_HolidayList.Rows.Count;
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);
            if (nMax == 0) return;
            for (int i = 0; i < nMax; ++i)
            {
                strTimeList = "";
                for (int j = 2; j < 10; ++j)
                {
                    strTmp = m_HolidayList.Rows[i].Cells[j].Value.ToString();
                    //strTmp.Format(_T("%02x"), _ttoi(strTmp)); //转换成16进制
                    strTimeList += (Convert.ToInt16(strTmp).ToString("X2"));
                }
                nLmtMonth = Convert.ToInt32(m_HolidayList.Rows[i].Cells[0].Value);
                nLmtMonth = (0 < nLmtMonth && nLmtMonth < 13) ? nLmtMonth : 5;

                nLmtDay = Convert.ToInt32(m_HolidayList.Rows[i].Cells[1].Value);
                nLmtDay = (0 < nLmtDay && nLmtDay < 32) ? nLmtDay : 1;

                byte[] cInfo = Encoding.Default.GetBytes(strTimeList);
                int nRetValue = CHD.API.CHD806D2CP.DSetHolidayTime(PortId, NetId, doorId/*转换后的门号*/,
                    nLmtMonth/*月*/, nLmtDay/*日*/, cInfo);
                if (nRetValue == 0)
                {
                    PrintMessage(String.Format("设置节假日: {0} 月 {1} 日时段成功! [ {2} ]", nLmtMonth, nLmtDay, strTimeList));
                }
                else if (nRetValue == 0x07)
                {
                    PrintMessage("二门星期时间段列表设置失败! 没有权限");
                }
                else PrintMessage(String.Format("设置节假日: {0} 月 {1} 日时段失败! 错误码: {2}", nLmtMonth, nLmtDay, nRetValue));
            }
            PrintMessage("设置二门节、假日时段完毕!");
        }



        private void GetHolidayListTime()
        {

            int nRetValue = 0, nLmtMonth = 0, nLmtDay = 0;
            byte[] Time = new byte[64];
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex+1);


            PrintMessage("开始读取节、假日时间段列表");
            m_HolidayList.Rows.Clear();
            uint i = 0;
            do
            {
                nRetValue = CHD.API.CHD806D2CP.DReadHolidayTime(PortId, NetId, doorId/*转换后的门号*/,
                    ++i, out nLmtMonth, out nLmtDay, Time);
                if (nRetValue == 0)
                {
                    m_HolidayList.Rows.Add(new object[] { nLmtMonth, nLmtDay, Time[0], Time[1], Time[2], Time[3], Time[4], Time[5], Time[6], Time[7] });
                    PrintMessage(String.Format(" 读取 {0}门 节假日:序号{1} {2} 月 {3} 日[ {4} {5} {6} {7} {8} {9} {10} {11} ]时段成功!", doorId, i, nLmtMonth, nLmtDay,
                       Time[0], Time[1], Time[2], Time[3], Time[4], Time[5], Time[6], Time[7]));
                }
                else if (nRetValue == 0xE5)
                {
                    break;
                }
                else
                {
                    PrintMessage(String.Format("读取 {0}门 节假日:序号{1} {2} 月 {3} 日 时段失败! 错误码: {4}", doorId, i - 1, nLmtMonth, nLmtDay, nRetValue));
                }
            } while (nRetValue == 0x00);
            PrintMessage("节、假日时间段列表读取完毕!");
        }


        private void button52_Click(object sender, EventArgs e)
        {
            GetWeekListTime();
            GetHolidayListTime();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            SetWeekListTime();
            SetHolidayListTime();
        }

        private void m_HolidayList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenuStrip rightMenu = new ContextMenuStrip();
                rightMenu.Items.Add("增加特殊日期", null, (obj, ee) => { AddSpecialDate(); });
                rightMenu.Items.Add("删除特殊日期", null, (obj, ee) => { DelSpecialDate(); });
                rightMenu.Show(Cursor.Position);
            }
        }


        private void AddSpecialDate()
        {
            m_HolidayList.Rows.Add(new Object[]{5,1,0,0,0,0,0,0,0,0});
        }




        private void DelSpecialDate()
        {
            m_HolidayList.Rows.RemoveAt(m_HolidayList.SelectedRows[0].Index);
        }





    }
}
