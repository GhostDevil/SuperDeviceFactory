
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

namespace CHD806D2M3BDemo
{
    public partial class CHD806D2M3B : Form
    {
        private int portId;
        private List<CUSTOMINFO> customInfo = new List<CUSTOMINFO>();
        private List<READINFO> pInfos = new List<READINFO>();
        private ASSINFO[] m_AssInfo1 = new ASSINFO[12];
        private ASSINFO[] m_AssInfo2 = new ASSINFO[12];
        private bool m_bLorad;
        private int m_nDoorID;
        public CHD806D2M3B()
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

        public CHD806D2M3B(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数
        private void InitCmp()
        {
            for (int i = 0; i < 256; i++)
            {
                cmbBankCode.Items.Add(i.ToString("D3"));
                cmbLevel.Items.Add(i.ToString("D3"));
                comboBox6.Items.Add(i.ToString("D3"));
            }
            cmbBankCode.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            for (int i = 0; i < 32; i++)
            {
                cmbCardType1.Items.Add(string.Format("第{0}类卡", i.ToString("D2")));
                cmbCardType2.Items.Add(string.Format("第{0}类卡", i.ToString("D2")));
                if (i > 0)
                {
                    cmbDoorId_specia.Items.Add(i);
                }
            }
            cmbCardType1.SelectedIndex = 0;
            cmbCardType2.SelectedIndex = 0;
            cmbDoor1.SelectedIndex = cmbDoor2.SelectedIndex = 4;
            cmbDoorNo.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;

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
        /// 端口ID
        /// </summary>
        public uint PortId
        {
            get { return (uint)this.portId; }
        }
        #endregion




        #region 综合设置
        private uint FormatDoorID()
        {
            if (cmbDoorNo.SelectedIndex > 1)
            {
                return 255;
            }
            else
            {
                return (uint)(cmbDoorNo.SelectedIndex + 1);
            }
        }


        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoteOpenDoor_Click(object sender, EventArgs e)
        {

            int nRerValue;
            if (!chkWithOperator.Checked)
            {
                nRerValue = CHD.API.CHD806D2M3B.DRemoteOpenDoor(PortId, NetId, FormatDoorID());
            }
            else
            {

                nRerValue = CHD.API.CHD806D2M3B.DRemoteOpenDoorWithUser(PortId, NetId, FormatDoorID(), txtUserId.Text);
            }

            if (nRerValue == 0x00)
            {
                if (chkWithOperator.Checked)
                    PrintMessage(String.Format("远程开 {0} 门成功! 操作员:{1}", FormatDoorID(), txtUserId.Text));
                else
                    PrintMessage(String.Format("远程开 {0} 门成功!", FormatDoorID()));
            }
            else if (nRerValue == 0x07)
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误提示: 没有权限!", FormatDoorID(), nRerValue));
            }
            else
            {
                PrintMessage(String.Format("远程开 {0} 门失败! 错误码:{1}", FormatDoorID(), nRerValue));
            }
        }



        /// <summary>
        /// 长开/闭门状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoorState_Click(object sender, EventArgs e)
        {
            uint nState = 0, nDelay = 0;
            int nRerValue = CHD.API.CHD806D2M3B.DReadAlwaysStateEx(PortId, NetId, FormatDoorID(), out nState, out nDelay);
            if (nRerValue == 0x00)
            {
                PrintMessage(String.Format("读取常开/闭门成功! 门号:{0} 状态字节:{1}, 剩余时间:{2} 分钟", FormatDoorID(), nState.ToString("X10"), nDelay));
            }
            else if (nRerValue == 0x07)
            {
                PrintMessage("读取常开/闭门失败! 错误提示: 没有权限!");
            }
            else
            {
                PrintMessage(String.Format("读取常开/闭门失败! 错误码:{0}", nRerValue));
            }
        }



        /// <summary>
        /// 开启常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlwaysOpenDoor_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD806D2M3B.DAlwaysOpenDoor(PortId, NetId, FormatDoorID(), uint.Parse(txtRelay.Text), txtUserId.Text), "开启常开门成功!", "开启常开门失败!");
        }



        /// <summary>
        /// 关闭常开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAlwaysOpenDoor_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD806D2M3B.DAlwaysOpenDoor(PortId, NetId, FormatDoorID(), 0, txtUserId.Text), "关闭常开门成功!", "关闭常开门失败!");
        }



        /// <summary>
        /// 开启常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAlwaysCloseDoor_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD806D2M3B.DAlwaysCloseDoor(PortId, NetId, FormatDoorID(), uint.Parse(txtRelay.Text), txtUserId.Text), "开启常闭门成功!", "开启常闭门失败!");
        }



        /// <summary>
        /// 关闭常闭门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAlwaysCloseDoor_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD806D2M3B.DAlwaysCloseDoor(PortId, NetId, FormatDoorID(), 0, txtUserId.Text), "关闭常闭门成功!", "关闭常闭门失败!");
        }



        /// <summary>
        /// 设置网络地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetNetAddr_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD806D2M3B.DNewNetID(PortId, NetId, uint.Parse(txtGroupNo.Text), uint.Parse(txtNetAddr.Text)), "设置网络ID成功!", "设置网络ID失败!");
        }



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOn_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DLinkOn(PortId, NetId, txtSysPwd.Text, txtDevPwd.Text);
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
            int nRetValue = CHD.API.CHD806D2M3B.DLinkOff(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("取消权限成功! ");
                    break;
                case 0x07:
                    PrintMessage("取消权限失败! 错误提示: 无权限");
                    break;
                default:
                    PrintMessage("取消权限失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangPwd_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DNewPwd(PortId, NetId, txtSysPwd.Text, txtDevPwd.Text);
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
        /// 读取门内组地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadDooAddr_Click(object sender, EventArgs e)
        {
            uint nInternalID = 0;

            int nRetValue = CHD.API.CHD806D2M3B.DBReadInternalID(PortId, NetId, out nInternalID);
            switch (nRetValue)
            {
                case 0x00:
                    txtDoorAddr.Text = nInternalID.ToString();

                    PrintMessage("读取门内ID成功! " + nInternalID);
                    break;
                default:
                    PrintMessage("读取门内ID失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置门内组地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDooAddr_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DBSetInternalID(PortId, NetId, uint.Parse(txtDoorAddr.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置门内ID成功!");
                    break;
                default:
                    PrintMessage("设置门内ID失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 开启远程驱动继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DOpenAlarm(PortId, NetId, FormatDoorID());
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("开启报警继电器{0}成功!", FormatDoorID()));
                    break;
                default:
                    PrintMessage(String.Format("开启报警继电器{0}失败! 错误码: {1}", FormatDoorID(), nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 关闭远程驱动继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DCloseAlarm(PortId, NetId, FormatDoorID());
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("关闭报警继电器{0}成功!", FormatDoorID()));
                    break;
                default:
                    PrintMessage(String.Format("关闭报警继电器{0}失败! 错误码: {1}", FormatDoorID(), nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取仓库管理模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadDepositoryMode_Click(object sender, EventArgs e)
        {
            uint nDepos1 = 0, nDepos2 = 0;

            int nRetValue = CHD.API.CHD806D2M3B.DBReadDepositoryMode(PortId, NetId, out nDepos1, out nDepos2);
            switch (nRetValue)
            {
                case 0x00:
                    comboBox3.SelectedIndex = (int)((nDepos1 < 3) ? nDepos1 : 0);
                    comboBox4.SelectedIndex = (int)((nDepos2 < 3) ? nDepos2 : 0);

                    PrintMessage(String.Format("读取仓管员模式成功! 门1:{0} 门2:{1}", nDepos1, nDepos2));
                    break;
                default:
                    PrintMessage("读取仓管员模式失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置仓库管理模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDepositoryMode_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DBSetDepositoryMode(PortId, NetId, (uint)(comboBox3.SelectedIndex), (uint)(comboBox4.SelectedIndex));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("设置仓管员模式成功! 门1:{0} 门2:{1}", comboBox3.SelectedIndex, comboBox4.SelectedIndex));
                    break;
                default:
                    PrintMessage("设置仓管员模式失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置人员状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetStatusInit_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DBSetStatusInit(PortId, NetId, textBox32.Text, (uint)(comboBox6.SelectedIndex), (uint)(comboBox5.SelectedIndex));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置人员状态成功!");
                    break;
                default:
                    PrintMessage("设置人员状态失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取主动上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadTransmitChannel_Click(object sender, EventArgs e)
        {
            int nChannel, nHost;

            int nRetValue = CHD.API.CHD806D2M3B.DReadTransmitChannel(PortId, NetId, out nChannel, out nHost);
            switch (nRetValue)
            {
                case 0x00:
                    textBox31.Text = nChannel.ToString();

                    PrintMessage("读取主动上传通道成功! 上传通道成功: " + nChannel);
                    break;
                default:
                    PrintMessage("读取主动上传通道失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        ///设置主动上传通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetTransmitChannel_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToUInt16(textBox31.Text);
            }
            catch
            {
                return;
            }

            int nRetValue = CHD.API.CHD806D2M3B.DSetTransmitChannel(PortId, NetId, uint.Parse(textBox31.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置主动上传通道成功! ");
                    break;
                default:
                    PrintMessage("设置主动上传通道失败! 错误码:" + nRetValue);
                    break;
            }
        }


        /// <summary>
        /// 解除胁迫报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenaceClose_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DMenaceClose(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("关闭胁迫报警成功!");
                    break;
                default:
                    PrintMessage("关闭胁迫报警失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设备版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDevVision_Click(object sender, EventArgs e)
        {
            StringBuilder szVersion = new StringBuilder();

            int nRetValue = CHD.API.CHD806D2M3B.DReadVersion(PortId, NetId, szVersion);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage("读取设备信息成功: " + szVersion.ToString());
                        break;
                    }
                default:
                    PrintMessage("读取设备信息失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 控制器状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnControlerState_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// 读取时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadTime_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime;


            int nRetValue = CHD.API.CHD806D2M3B.DReadDateTime(PortId, NetId, out  stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("读取时间：{0} 星期{1}", CHD.Common.ParasTime(stime), CHD.Common.ParasTime(stime).DayOfWeek));
                    break;
                default:
                    PrintMessage("读取时间失败! 错误码:" + nRetValue);
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
            int nCount = 0;
            int nRetValue = CHD.API.CHD806D2M3B.DReadUserCount(PortId, NetId, out nCount);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("读取用户数量成功!" + nCount);
                    break;
                case 0x07:
                    PrintMessage("读取用户数量失败! 错误提示: 无权限");
                    break;
                default:
                    PrintMessage("读取用户数量失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 远程监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRmoteMonite_Click(object sender, EventArgs e)
        {
            int n = 0, n1 = 0, n2 = 0, n3 = 0;

            int nRetValue = CHD.API.CHD806D2M3B.DReadDoorState(PortId, NetId, out n, out n1, out n2, out n3);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("远程监控成功, 门1工作状态:{0} 门1的线路状态:{1} 门2工作状态:{2} 门2的线路状态{3}", n, n1, n2, n3));
                    break;
                case 0x07:		//无权限
                    PrintMessage("无权限");
                    break;
                default:
                    PrintMessage("远程监控失败! 错误码:" + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDevTime_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CHD.API.SYSTEMTIME stime = CHD.Common.ParasTime(dt);


            int nRetValue = CHD.API.CHD806D2M3B.DSetDateTime(PortId, NetId, ref stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("同步时间成功：{0} 星期:{1}", dt, dt.DayOfWeek));
                    break;
                default:
                    PrintMessage("同步时间失败! 错误码: " + nRetValue);
                    break;
            }
        }
        #endregion



        #region 卡/用户管理
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {

            CHD.API.SYSTEMTIME LmtTime = CHD.Common.ParasTime(DateTime.Parse(txtLimtTime.Text));
            if (4 != txtOpenDooPwd.Text.Length)
            {
                MessageBox.Show("密码长度不对!");
                return;
            }
            int nRetValue = CHD.API.CHD806D2M3B.DBAddUser(PortId, NetId,
                txtCardNo.Text,			//卡号
                txtAddUserId.Text,			//用户ID
                txtOpenDooPwd.Text,			//开门密码
                ref LmtTime,				//有效期
                (uint)(cmbLevel.SelectedIndex),				//级别
                (uint)(cmbBankCode.SelectedIndex),			//银行代码
                (uint)(cmbDoor1.SelectedIndex), (uint)(cmbCardType1.SelectedIndex),
                (uint)(cmbDoor2.SelectedIndex), (uint)(cmbCardType2.SelectedIndex));
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("Add user succeed! Card No.:{0} User ID:{1}", txtCardNo.Text, txtAddUserId.Text));
                    break;
                case 0x07:		//SM内已满
                    PrintMessage(("Add user failed! Error: No authorization!"));
                    break;
                case 0xE2:		//SM内已满
                    PrintMessage(("The device is not enough space!"));
                    break;
                case 0xE6:		//用户ID号重复
                    PrintMessage(String.Format("User ID :{0} has been used!", txtAddUserId.Text));
                    break;
                case 0xE7:		//卡号重复
                    PrintMessage(String.Format("Card No.:{0} has been used!", txtCardNo.Text));
                    break;
                case 0xE8:		//用户信息项全部重复设置
                    PrintMessage(("All the user information repeat settings!"));
                    break;
                default:		//其他值表示失败
                    PrintMessage("Add user failed!" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 删除超级卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSuperCard_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DDeleteSuperCardEx(PortId, NetId, 1);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(("删除超级权限卡成功"));
                    break;
                case 0x07:		//无权限
                    PrintMessage(("无权限"));
                    break;
                default:
                    PrintMessage("删除超级权限卡失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置超级卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSuperCard_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DSetSuperCardEx(PortId, NetId, txtSuperCard1.Text, txtSuperCard2.Text, txtSuperCard3.Text, txtSuperCard4.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("Set superuser card succeed! Card1:{0} Card2:{1} Card1:{2} Card2:{3}", txtSuperCard1.Text, txtSuperCard2.Text, txtSuperCard3.Text, txtSuperCard4.Text));
                    break;
                case 0x07:		//无权限
                    PrintMessage(("Set super card failed! Error: No authorization!!"));
                    break;
                default:
                    PrintMessage("Set super card failed! Error: " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// 读取超级卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSuperCard_Click(object sender, EventArgs e)
        {
            StringBuilder szCard1 = new StringBuilder();
            StringBuilder szCard2 = new StringBuilder();

            int nRetValue = CHD.API.CHD806D2M3B.DReadSuperCardEx(PortId, NetId, 0, szCard1, szCard2);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    txtSuperCard1.Text = szCard1.ToString();
                    txtSuperCard2.Text = szCard2.ToString();

                    PrintMessage(string.Format("Load superuser card succeed! Card1:{0} Card2:{1}", szCard1, szCard2));
                    break;
                default:		//其他值表示失败
                    PrintMessage("Load super user card succeed! Code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置远程超级开门密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSuperCardPwd_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D2M3B.DSetSuperPwdEx(PortId, NetId, 0, txtSuperCardPwd1.Text, txtSuperCardPwd2.Text, txtSuperCardPwd3.Text, txtSuperCardPwd4.Text);
            switch (nRetValue)
            {
                case 0:
                    PrintMessage(("Set password succeed!"));
                    break;
                case 0x07:		//No authorization
                    PrintMessage("Set password failed! Error: No authorization!");
                    break;
                default:		//Other value means failed
                    PrintMessage("Set password failed! Error: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取超级开门密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSuperPwd_Click(object sender, EventArgs e)
        {
            StringBuilder szPwd1 = new StringBuilder();
            StringBuilder szPwd2 = new StringBuilder();

            int nRetValue = CHD.API.CHD806D2M3B.DReadSuperPwdEx(PortId, NetId, 0, szPwd1, szPwd2);
            if (0x00 == nRetValue)
            {
                txtSuperCardPwd1.Text = szPwd1.ToString();
                txtSuperCardPwd2.Text = szPwd2.ToString();
                PrintMessage(String.Format("Read Password 1:{0} First Door Password 2:{1}", szPwd1, szPwd2));
            }
            else
            {
                PrintMessage("Operation failed! Error: " + nRetValue);
            }
        }



        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardReadUserCount_Click(object sender, EventArgs e)
        {

            int nUserCount = 0;


            int nRetValue = CHD.API.CHD806D2M3B.DReadUserCount(PortId, NetId, out nUserCount);
            if (nRetValue == 0x00)
            {
                txtCardUserCount.Text = nUserCount.ToString();
                PrintMessage("Load user count succeed! User count: " + nUserCount);
            }
            else
            {
                PrintMessage("Load User count failed! Error: " + nRetValue);
            }
        }




        /// <summary>
        /// 删除所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllUser_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DDelAllUser(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(("Delete all the users succeed!"));
                    break;
                case 0x07:		//无权限
                    PrintMessage(("Delete all the users failed! Error: No authorization!"));
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage(("Delete all the users failed! Error: SM internal related settings item memory is empty"));
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage(("Delete all the users failed! Error: SM internal no related information item"));
                    break;
                default:		//其他值表示失败
                    PrintMessage("Delete all the users failed! Error: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 按存储位置读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserByPos_Click(object sender, EventArgs e)
        {

            StringBuilder szPasswd = new StringBuilder();
            StringBuilder szCardNo = new StringBuilder();
            StringBuilder szUserID = new StringBuilder();
            uint nLevel = 0, nBankCode = 0, nGroupType1 = 0, nCardType1 = 0, nGroupType2 = 0, nCardType2 = 0;
            CHD.API.SYSTEMTIME LmtTime;

            int nRetValue = CHD.API.CHD806D2M3B.DBReadUserInfoByUserPos(PortId, NetId,
                    uint.Parse(txtUserPos.Text),
                    szCardNo,
                    szUserID,
                    szPasswd,
                    out LmtTime,
                    out nLevel, out nBankCode, out nGroupType1, out nCardType1, out nGroupType2, out nCardType2);
            if (nRetValue == 0x00)
            {
                PrintMessage(String.Format("Load succeed! Card No.:{0} User ID:{1} Open Password:{2} Valid Date:{3} Level:{4} Bank Code:{5}",
                    szCardNo,							//用户
                    szUserID,							//卡号
                    szPasswd,							//密码
                    CHD.Common.ParasTime(LmtTime), //有效期
                    nLevel, nBankCode));
                PrintMessage(String.Format("First door Group:{0} Card type:{1] Second door Group:{2] Card type:{3}", nGroupType1, nCardType1, nGroupType2, nCardType2));
            }


            switch (nRetValue)
            {
                case 0x00:
                    break;
                case 0x07:
                    PrintMessage("Load User Information failed! Error: No authorization!");
                    break;
                case 0xE4:
                    PrintMessage("Load User Information failed! Error: SM internal related settings item memory is empty");
                    break;
                case 0xE5:
                    PrintMessage("Load User Information failed! Error: SM internal no related information item");
                    break;
                default:
                    PrintMessage("Load User Information failed! Error: " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// 按用户ID读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserInfoByUserId_Click(object sender, EventArgs e)
        {

            StringBuilder szCardNo = new StringBuilder();
            StringBuilder szPasswd = new StringBuilder();
            uint nLevel = 0, nBankCode = 0, nGroupType1 = 0, nCardType1 = 0, nGroupType2 = 0, nCardType2 = 0;
            CHD.API.SYSTEMTIME LmtTime;

            int nRetValue = CHD.API.CHD806D2M3B.DBReadUserInfoByUserID(PortId, NetId,
                    txtCardUserId.Text,
                    szCardNo,
                    szPasswd,
                    out LmtTime,
                    out nLevel, out nBankCode, out nGroupType1, out nCardType1, out nGroupType2, out  nCardType2);
            if (nRetValue == 0x00)
            {
                PrintMessage(String.Format("Load succeed! Card No.:{0} User ID:{1} Open Password:{2} Valid Date:{3} Level:{4} Bank Code:{5}",
                    szCardNo,				//用户
                    szCardNo,						//卡号
                    szPasswd,						//密码
                    CHD.Common.ParasTime(LmtTime), //有效期
                    nLevel, nBankCode));
                PrintMessage(String.Format("DoorID:1: Group:{0} Card type:{1}", nGroupType1, nCardType1));
                PrintMessage(String.Format("DoorID:2: Group:{0} Card type:{1}", nGroupType2, nCardType2));
            }

            switch (nRetValue)
            {
                case 0x00:
                    break;
                case 0x07:
                    PrintMessage("Load User Information failed! Error: No authorization!");
                    break;
                case 0xE4:
                    PrintMessage("Load User Information failed! Error: SM internal related settings item memory is empty");
                    break;
                case 0xE5:
                    PrintMessage("Load User Information failed! Error: SM internal no related information item");
                    break;
                default:
                    PrintMessage("Load User Information failed! Error:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 按卡号读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserInfoByCardNo_Click(object sender, EventArgs e)
        {

            StringBuilder szUserID = new StringBuilder();
            StringBuilder szPasswd = new StringBuilder();
            uint nLevel = 0, nBankCode = 0, nGroupType1 = 0, nCardType1 = 0, nGroupType2 = 0, nCardType2 = 0;
            CHD.API.SYSTEMTIME LmtTime;

            int nRetValue = CHD.API.CHD806D2M3B.DBReadUserInfoByCardNo(PortId, NetId,
                    txtCardNoQuer.Text,
                    szUserID,
                    szPasswd,
                    out LmtTime,
                    out nLevel, out nBankCode, out nGroupType1, out nCardType1, out nGroupType2, out nCardType2);
            if (nRetValue == 0x00)
            {
                PrintMessage(String.Format("Load succeed! Card No.:{0} User ID:{1} Open Password:{2} Valid Date:{3} Level:{4} Bank Code:{5}",
                    txtCardNoQuer.Text,					//用户
                    szUserID,							//卡号
                    szPasswd,							//密码
                    CHD.Common.ParasTime(LmtTime), //有效期
                    nLevel, nBankCode));
                PrintMessage(String.Format("DoorID:1: Group:{0} Card type:{1}", nGroupType1, nCardType1));
                PrintMessage(String.Format("DoorID:2: Group:{0} Card type:{1}", nGroupType2, nCardType2));
            }

            switch (nRetValue)
            {
                case 0x00:		//Succeed
                    break;
                case 0x07:		//SM internal full
                    PrintMessage("Load User Information failed! Error: No authorization!");
                    break;
                case 0xE4:		//No related information
                    PrintMessage("Load User Information failed! Error: SM internal related settings item memory is empty");
                    break;
                case 0xE5:		//No related information
                    PrintMessage("Load User Information failed! Error: SM internal no related information item");
                    break;
                default:		//Other value means failed
                    PrintMessage("Load User Information failed! Error:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUser_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD806D2M3B.DDelUserByCardNo(PortId, NetId, txtCardNoQuer.Text);
            if (nRetValue == 0x00)
            {
                PrintMessage(string.Format("Delete the user:{0} succeed!", txtCardNoQuer.Text));
            }

            switch (nRetValue)
            {
                case 0x00:		//成功
                    break;
                case 0x07:		//无权限
                    PrintMessage(("Delete the user failed! Error: No authorization!"));
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage(("Delete the user failed! Error: SM internal related settings item memory is empty"));
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage(("Delete the user failed! Error: SM internal no related information item"));
                    break;
                default:		//其他值表示失败
                    PrintMessage("Delete the user failed! Error: " + nRetValue);
                    break;
            }
        }
        #endregion



        #region 记录管理
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
        /// 顺序读取一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByOrder_Click(object sender, EventArgs e)
        {

            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int nRetValue = CHD.API.CHD806D2M3B.DReadOneRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
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
        /// 顺序读取一条记录并返回记录号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRec_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecPos = 0, nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int nRetValue = CHD.API.CHD806D2M3B.DReadRecWithPoint(PortId, NetId, out nRecPos, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("记录序号:{0} {1}", nRecPos, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
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
        /// 按记录号读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByPos_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;
            int nRetValue = CHD.API.CHD806D2M3B.DReadRecByPoint(PortId, NetId, short.Parse(txtRecPos.Text), szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("读取记录序号:[{0}]成功! {1}", txtRecPos.Text, FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState)));
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
        /// 读取设备最新的一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadNewRec_Click(object sender, EventArgs e)
        {

            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szRecSource = new StringBuilder();
            int nRecState = 0, nRecRemark = 0, nRecLineState = 0, nDoorID = 0;

            int nRetValue = CHD.API.CHD806D2M3B.DReadOneNewRec(PortId, NetId, szRecSource, out RecTime, out nRecState, out nRecRemark, out nRecLineState, out nDoorID);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(FormatLog(szRecSource, CHD.Common.ParasTime(RecTime), nRecState, nRecRemark, nRecLineState));

                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage(("设备所有记录已经读完!"));
                    break;
                default:		//Failed
                    PrintMessage("读取记录失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 初始化记录区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD806D2M3B.DInitRec(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(("初始化记录成功! "));
                    break;
                case 0x07:		//Failed
                    PrintMessage(("初始化记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage(("初始化记录失败! 错误代码: " + nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读设备记录区状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryRecStatu_Click(object sender, EventArgs e)
        {
            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0;
            PrintExcuteResult(CHD.API.CHD806D2M3B.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen), String.Format("读取记录区状态成功! Bottom={0} nSaveP={1} nLoadP={2} nMaxLen={3}", nBottom, nSaveP, nLoadP, nMaxLen), "读取记录失败!");

        }



        /// <summary>
        /// 读取当前设备记录总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button56_Click(object sender, EventArgs e)
        {

            int nBottom = 0, nSaveP = 0, nLoadP = 0, nMaxLen = 0, nRecordCount = 0;
            int nRetValue = CHD.API.CHD806D2M3B.DReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    if (nSaveP >= nLoadP)
                        nRecordCount = nSaveP - nLoadP;
                    else
                        nRecordCount = nMaxLen - (nLoadP - nSaveP);
                    PrintMessage("读取成功! 总数：" + nRecordCount);
                    break;
                default:		//Failed
                    PrintMessage("读取失败! 错误代码: " + nRetValue);
                    break;
            }
        }


        #endregion






        private void btnReadPortTree_Click(object sender, EventArgs e)
        {

            uint nPortCount = 20, nReaderCount = 0;

            // 读取端口数量
            int nRetValue = CHD.API.CHD806D2M3B.DBReadPortCount(PortId, NetId, out nPortCount, out nReaderCount);
            if (nRetValue != 0)
            {
                PrintMessage("Load port information failed! Error:" + nRetValue);
                return;
            }


            // 生成对应数量的TreeItem

            for (uint i = 0; i < nPortCount; ++i)
            {
                // 获取各端口配置
                PORTINFO pInfo = new PORTINFO();

                StringBuilder cName = new StringBuilder();
                nRetValue = CHD.API.CHD806D2M3B.DBReadIOConfig(PortId, NetId, i, cName, out pInfo.nMask, out pInfo.nConfig, out pInfo.nInDelay, out pInfo.nOutDelay);
                if (0 != nRetValue)
                {
                    PrintMessage(String.Format("Load port:{0} logic information failed! Error:{1}", i, nRetValue));
                }
                nRetValue = CHD.API.CHD806D2M3B.DBReadOLogicCfg(PortId, NetId, i, pInfo.szData);
                if (0 != nRetValue)
                {
                    PrintMessage(String.Format("Load port:{0} logic configuration information failed! Error:{1}", i, nRetValue));
                }
                pInfo.szData = cName;
                treeView1.Nodes.Add(String.Format("端口{0}:{1}", i, pInfo.szName));

            }

        }











        private void GetWeekListTime()
        {
            byte[] Time = new byte[64];
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);
            PrintMessage("开始读星期时间段列表");
            for (int nWeek = 1; nWeek <= 7; ++nWeek)
            {
                int nRetValue = CHD.API.CHD806D2M3B.DReadWeekTime(PortId, NetId, doorId/*转换后的门号*/, (uint)nWeek/*星期*/, Time);
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
            int nRetValue = CHD.API.CHD806D2M3B.DSetWeekTime(PortId, NetId, doorId/*转换后的门号*/, cInfo);
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
                int nRetValue = CHD.API.CHD806D2M3B.DSetHolidayTime(PortId, NetId, doorId/*转换后的门号*/,
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
            uint doorId = cmbDoorId_specia.SelectedIndex > 1 ? 255 : (uint)(cmbDoorId_specia.SelectedIndex + 1);


            PrintMessage("开始读取节、假日时间段列表");
            m_HolidayList.Rows.Clear();
            uint i = 0;
            do
            {
                nRetValue = CHD.API.CHD806D2M3B.DReadHolidayTime(PortId, NetId, doorId/*转换后的门号*/,
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



        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button67_Click(object sender, EventArgs e)
        {
            GetWeekListTime();
            GetHolidayListTime();
        }




        /// <summary>
        /// 设置时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button68_Click(object sender, EventArgs e)
        {
            SetWeekListTime();
            SetHolidayListTime();
        }




        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button58_Click(object sender, EventArgs e)
        {
            StringBuilder szTimeSlot = new StringBuilder();
            String strTemp, strItemText;
            for (int i = 0; i < 32; ++i)
            {
                int nRetValue = CHD.API.CHD806D2M3B.DReadListTime(PortId, NetId, (uint)i, szTimeSlot);
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
        private void button57_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; ++i)
            {
                String strText = String.Empty;
                for (int j = 1; j < 9; ++j)
                {
                    strText += dgTimeSpan.Rows[i].Cells[j].Value.ToString();
                }
                strText = strText.Replace(":", "");
                int nRetValue = CHD.API.CHD806D2M3B.DSetListTime(PortId, NetId, (uint)i, new StringBuilder(strText));
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





        private void OnAddItem(CUSTOMINFO pCustominfo)
        {
            StringBuilder str = new StringBuilder();

            int nItem = dataGridView4.Rows.Count;

            // 所属门
            str.Append(pCustominfo.nIndex);
            // 触发点
            str.Append(pCustominfo.nTrigger >> 8);
            str.Append(",");
            str.Append(pCustominfo.nTrigger & 0xFF);
            dataGridView4.Rows[nItem].Cells[1].Value = str.ToString();
            // 状态
            if ((pCustominfo.nMode & 0x01) > 0) dataGridView4.Rows[nItem].Cells[2].Value = "启用";
            else dataGridView4.Rows[nItem].Cells[2].Value = "禁用";
            // 动作
            if ((pCustominfo.nMode & 0x02) > 0) dataGridView4.Rows[nItem].Cells[3].Value = "常开";
            else dataGridView4.Rows[nItem].Cells[3].Value = "常闭";
            // 动作时间
            dataGridView4.Rows[nItem].Cells[4].Value = "---";
            // 时段索引
            dataGridView4.Rows[nItem].Cells[5].Value = pCustominfo.nTimeIndex;
            // 响应点
            dataGridView4.Rows[nItem].Cells[6].Value = (pCustominfo.nResponse >> 8) + "," + (pCustominfo.nResponse & 0xFF);
            customInfo.Add(pCustominfo);

        }



        /// <summary>
        /// 读取自动关联配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button59_Click(object sender, EventArgs e)
        {
            customInfo.Clear();
            dataGridView4.Rows.Clear();
            PrintMessage("Loading...");
            for (uint i = 0; i < 20; ++i)
            {
                uint nTrigger = 0, nMode = 0, nDelay = 0, nTimeIndex = 0, nReply = 0;

                int nRetValue = CHD.API.CHD806D2M3B.DBReadCustomAssCfg(PortId, NetId, i, out nTrigger, out nMode, out nDelay, out nTimeIndex, out nReply);
                if (0 != nRetValue)
                {
                    PrintMessage(String.Format("Load index:{0} custom associated configuration failed! Error:{1}", i, nRetValue));
                }
                CUSTOMINFO pCustom = new CUSTOMINFO();

                pCustom.nIndex = i;
                pCustom.nTrigger = nTrigger;
                pCustom.nMode = nMode;
                pCustom.nTimeIndex = nTimeIndex;
                pCustom.nResponse = nReply;
                OnAddItem(pCustom);
            }
            PrintMessage("Loaded!");

        }




        /// <summary>
        /// 设置自动关联配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button60_Click(object sender, EventArgs e)
        {
            int nCount = dataGridView4.Rows.Count;
            if (0 == nCount)
            {
                MessageBox.Show("请先加载设备配置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PrintMessage(("Setting..."));
            for (int i = 0; i < nCount; ++i)
            {
                CUSTOMINFO pCustom = customInfo[i];
                int nRetValue = CHD.API.CHD806D2M3B.DBSetCustomAssCfg(PortId, NetId, pCustom.nIndex, pCustom.nTrigger, pCustom.nMode, pCustom.nDelay, pCustom.nTimeIndex, pCustom.nResponse);
                if (0 != nRetValue)
                {
                    PrintMessage(String.Format("Set index:{0} custom associated configuration failed! Error:{1}", pCustom.nIndex, nRetValue));
                }
                dataGridView4.Rows[i].Selected = true;

            }
            PrintMessage("Completed!");
        }




        private void ReadConfig(uint nDoorID, ASSINFO[] AssInfo)
        {

            for (uint nIndex = 0; nIndex < 12; ++nIndex) // 读第1门
            {
                AssInfo[nIndex].nIndex = 0xFF;
                uint nTrigger = 0, nMode = 0, nDelay = 0, nPeriodIndex = 0;
                int nRetValue = CHD.API.CHD806D2M3B.DBReadFixedAssCfg(PortId, NetId, nDoorID, nIndex, out nTrigger, out nMode, out nDelay, out nPeriodIndex);
                if (0x00 != nRetValue)
                {
                    PrintMessage(String.Format("Load DoorID:{0} Index:{1} fixed associated configuration failed! Error:{2}", nDoorID, nIndex, nRetValue));
                    continue;
                }
                AssInfo[nIndex].nIndex = nIndex;
                AssInfo[nIndex].nDoorID = nDoorID;
                AssInfo[nIndex].nTrigger = nTrigger;
                AssInfo[nIndex].nMode = nMode;
                AssInfo[nIndex].nDelay = nDelay;
                AssInfo[nIndex].nPeriodIndex = nPeriodIndex;
            }

            PrintMessage(string.Format("Load DoorID:{0} fixed associated configuration is complete!", nDoorID));
        }





        private void SetConfig(uint nDoorID, ASSINFO[] AssInfo)
        {


            for (int nIndex = 0; nIndex < 12; ++nIndex) // 读第1门
            {

                int nRetValue = CHD.API.CHD806D2M3B.DBSetFixedAssCfg(PortId, NetId, AssInfo[nIndex].nDoorID, AssInfo[nIndex].nIndex,
                    AssInfo[nIndex].nTrigger, AssInfo[nIndex].nMode, AssInfo[nIndex].nDelay, AssInfo[nIndex].nPeriodIndex);
                if (0x00 != nRetValue)
                {
                    PrintMessage(String.Format("Set DoorID:{0} Index:{1} fixed associated configuration failed! Error:{2}", nDoorID, nIndex, nRetValue));
                    continue;
                }
            }

            PrintMessage(String.Format("Set DoorID:{0} fixed associated configuration is complete!", nDoorID));

        }



        private void comboBox13_SelectedIndexChanged()
        {
            if (m_bLorad)
            {
                if (0 == m_nDoorID) OnAddItem(m_AssInfo1);
                else OnAddItem(m_AssInfo2);
            }
        }


        private string FormatEvent(uint nIndex)
        {
            String str = "";
            switch (nIndex)
            {
                case 0: str = ("布防输入"); break;
                case 1: str = ("红外输入"); break;
                case 2: str = ("紧急输入"); break;
                case 3: str = ("门磁输入"); break;
                case 4: str = ("手动输入"); break;
                case 5: str = ("开门继电器关联端口"); break;
                case 6: str = ("报警输出端口"); break;
                case 7: str = ("进门DVS联动"); break;
                case 8: str = ("出门DVS联动"); break;
                case 9: str = ("等待确认黄灯"); break;
                case 10: str = ("确认开门绿灯"); break;
                case 11: str = ("未确认开门红灯"); break;
                default: break;
            }
            return str;
        }



        private void OnAddItem(ASSINFO[] pAssinfo)
        {

            dataGridView5.Rows.Clear();

            for (int i = 0; i < 12; ++i)
            {
                // 所属门

                dataGridView5.Rows.Add(new object[] { i, pAssinfo[i].nDoorID });

                // 事件类型
                String str = FormatEvent(pAssinfo[i].nIndex);
                if (str == String.Empty) continue;
                dataGridView5.Rows[i].Cells[1].Value = str;
                // 触发点

                dataGridView5.Rows[i].Cells[2].Value = (pAssinfo[i].nTrigger >> 8) + "" + (pAssinfo[i].nTrigger & 0xFF);
                // 状态
                dataGridView5.Rows[i].Cells[3].Value = (pAssinfo[i].nMode & 0x01) > 0 ? "启用" : "禁用";

                // 输入动作

                dataGridView5.Rows[i].Cells[4].Value = (pAssinfo[i].nMode & 0x02) > 0 ? "高电平" : "低电平";

                // 自动关门
                if (3 == pAssinfo[i].nIndex)
                {
                    dataGridView5.Rows[i].Cells[5].Value = (pAssinfo[i].nMode & 0x04) > 0 ? "禁用" : "启用";
                }
                else
                {
                    dataGridView5.Rows[i].Cells[5].Value = "---";
                }
                // 触发时间

                dataGridView5.Rows[i].Cells[6].Value = pAssinfo[i].nDelay;

                // 时段索引

                dataGridView5.Rows[i].Cells[7].Value = pAssinfo[i].nPeriodIndex;

                // 绑定
                //m_ListCtrl.SetItemData(i, (DWORD)&pAssinfo[i]);
            }
        }






        /// <summary>
        /// 读取事件关联配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button61_Click(object sender, EventArgs e)
        {
            ReadConfig(0, m_AssInfo1); // 1号门
            ReadConfig(1, m_AssInfo2); // 2号门
            m_bLorad = true;
            comboBox13_SelectedIndexChanged();
        }



        /// <summary>
        /// 设置事件关联配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button62_Click(object sender, EventArgs e)
        {
            if (dataGridView5.Rows.Count > 0)
            {
                SetConfig(0, m_AssInfo1); // 1号门
                SetConfig(1, m_AssInfo2); // 2号门
            }
            else
            {
                MessageBox.Show("请先加载设备配置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        /// <summary>
        /// 读取读卡器配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button63_Click(object sender, EventArgs e)
        {
            pInfos.Clear();

            uint nPortCount = 20, nReaderCount = 0;

            // 读取端口数量
            int nRetValue = CHD.API.CHD806D2M3B.DBReadPortCount(PortId, NetId, out nPortCount, out nReaderCount);
            if (nRetValue != 0)
            {
                PrintMessage(String.Format("Load card reader information failed! Error:{0}", nRetValue));
                return;
            }

            //FreeAllInfo();
            dataGridView6.Rows.Clear();

            for (int i = 0; i < nReaderCount; ++i)
            {
                // 获取各端口配置
                uint nDoorID = 0, nMode = 0, nConfig = 0, nCurArea = 0, nInArea = 0;
                StringBuilder sb = new StringBuilder();

                nRetValue = CHD.API.CHD806D2M3B.DBReadCardReaderCfg(PortId, NetId, (uint)i, sb, out nDoorID, out nMode, out nConfig, out  nCurArea, out nInArea);
                if (0 != nRetValue)
                {
                    PrintMessage(String.Format("Load Card reader:{0} logic information failed! Error:{1}", i, nRetValue));
                }

                // 显示到list
                dataGridView6.Rows.Add(new object[] { i, i });


                dataGridView6.Rows[i].Cells[1].Value = sb.ToString();


                dataGridView6.Rows[i].Cells[2].Value = nDoorID;
                // 状态
                dataGridView6.Rows[i].Cells[3].Value = (nMode & 0x01) > 0 ? "启用" : "禁用";

                // 进出场
                dataGridView6.Rows[i].Cells[4].Value = (nMode & 0x02) > 0 ? "出场" : "进场";

                // 仓库区
                dataGridView6.Rows[i].Cells[5].Value = (nMode & 0x04) > 0 ? "否" : "是";

                // 检测库管员
                dataGridView6.Rows[i].Cells[6].Value = (nMode & 0x08) > 0 ? "禁用" : "启用";

                // 中心确认开门

                dataGridView6.Rows[i].Cells[7].Value = (nMode & 0x10) > 0 ? "启用" : "禁用";


                // 校验权限
                dataGridView6.Rows[i].Cells[8].Value = (nConfig & 0x01) > 0 ? "启用" : "禁用";

                // 分组
                dataGridView6.Rows[i].Cells[9].Value = (nConfig & 0x02) > 0 ? "启用" : "禁用";

                // 分类
                dataGridView6.Rows[i].Cells[10].Value = (nConfig & 0x04) > 0 ? "启用" : "禁用";
                // 保存刷卡记录
                dataGridView6.Rows[i].Cells[11].Value = (nConfig & 0x08) > 0 ? "启用" : "禁用";
                // 刷卡+密码
                dataGridView6.Rows[i].Cells[12].Value = (nConfig & 0x10) > 0 ? "启用" : "禁用";
                // 区域管理
                dataGridView6.Rows[i].Cells[13].Value = (nConfig & 0x20) > 0 ? "启用" : "禁用";
                // 指纹+密码
                dataGridView6.Rows[i].Cells[14].Value = (nConfig & 0x40) > 0 ? "启用" : "禁用";
                // 当前区域
                dataGridView6.Rows[i].Cells[15].Value = nCurArea;

                // 进入区域
                dataGridView6.Rows[i].Cells[16].Value = nInArea;




                READINFO pInfo = new READINFO();
                pInfo.nDoorID = nDoorID;
                pInfo.nMode = nMode;
                pInfo.nConfig = nConfig;
                pInfo.nCurArea = nCurArea;
                pInfo.nInArea = nInArea;

                pInfos.Add(pInfo);
            }

        }





        /// <summary>
        /// 设置读卡器配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button64_Click(object sender, EventArgs e)
        {
            String strMsg = "";
            int nCount = dataGridView6.Rows.Count;
            if (0 < nCount)
            {
                for (int i = 0; i < nCount; ++i)
                {
                    READINFO pInfo = pInfos[i];
                    int nRetValue = CHD.API.CHD806D2M3B.DBSetCardReaderCfg(PortId, NetId, (uint)i, pInfo.nDoorID, pInfo.nMode, pInfo.nConfig, pInfo.nCurArea, pInfo.nInArea);
                    if (0 == nRetValue)
                    {
                        strMsg = String.Format("Set Card reader:{0} logic information succeed!", i);
                    }
                    else strMsg = String.Format("Set Card reader:{0} logic information failed! Error:{1}", i, nRetValue);
                }
            }
            PrintMessage(strMsg);
        }

        private void button66_Click(object sender, EventArgs e)
        {

        }















    }




    public struct PORTINFO
    {
        public StringBuilder szName;
        public uint nMask;
        public uint nConfig;
        public uint nInDelay;
        public uint nOutDelay;
        public StringBuilder szData;
    };




    struct CUSTOMINFO
    {
        public uint nIndex;		// 编号
        public uint nTrigger;		// 触发先
        public uint nMode;			// 模式
        public uint nDelay;		// 动作时间
        public uint nTimeIndex;	// 有效时段索引
        public uint nResponse;		// 响应点
    };




    struct ASSINFO
    {
        public uint nDoorID;
        public uint nIndex;
        public uint nTrigger;
        public uint nMode;
        public uint nDelay;
        public uint nPeriodIndex;
    };


    struct READINFO
    {
        public uint nDoorID;
        public uint nMode;
        public uint nConfig;
        public uint nCurArea;
        public uint nInArea;
    };
}

