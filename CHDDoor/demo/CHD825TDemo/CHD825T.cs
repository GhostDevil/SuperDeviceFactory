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

namespace CHD825TDemo
{
    public partial class CHD825T : Form
    {
        private int portId;

        public CHD825T()
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

        public CHD825T(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }


        #region 辅助函数
        /// <summary>
        /// 供反射调用
        /// </summary>
        /// <param name="act"></param>
        public void ShowUI(Action act)
        {
            this.FormClosed += (e, o) => { act(); };
            this.ShowDialog();
            
        }




        void InitCmp()
        {
            cmbInOrOut.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
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



        #region 设置
        /// <summary>
        /// Set Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetDate_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME stime = CHD.Common.ParasTime(DateTime.Now);


            int nRetValue = CHD.API.CHD825T.CHD825T_SetDateTime(PortId, NetId, ref stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("Synchronization time success:{0} week{1}", CHD.Common.ParasTime(stime).ToString(), stime.wDayOfWeek));
                    break;
                default:
                    PrintMessage("Synchronization time failed! " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Carried Stem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarriedtime_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD825T.CHD825T_ControlDoor(PortId, NetId, 1);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Release command success");
                    break;
                default:
                    PrintMessage("Release command failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD825T.CHD825T_Init(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Initialization success");
                    break;
                default:
                    PrintMessage("Initialization failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Read Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadDate_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME st;

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadDateTime(PortId, NetId, out st);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Read system time success, " + CHD.Common.ParasTime(st).ToString());
                    break;
                default:
                    PrintMessage("Read system time failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Control Entry/Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD825T.CHD825T_SetInOrOut(PortId, NetId, cmbInOrOut.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set Entry,Exit success");
                    break;
                default:
                    PrintMessage("Set Entry,Exit failed! error code " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// Display immediately
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox36.Text.Length > 30)
            {
                MessageBox.Show("Length input error");
                return;
            }

            int nRetValue = CHD.API.CHD825T.CHD825T_LEDView(PortId, NetId, uint.Parse(textBox35.Text), textBox36.Text, (uint)(textBox36.Text.Length));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set LED display command success");
                    break;
                default:
                    PrintMessage("Set LED display command failed! error code" + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// Fixed display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox34.Text.Length > 50 || textBox33.Text.Length > 50 || textBox32.Text.Length > 50)
            {
                MessageBox.Show("input length input error");
                return;
            }

            int nRetValue = CHD.API.CHD825T.CHD825T_LEDView1(PortId, NetId, 0, textBox34.Text, (uint)(textBox34.Text.Length));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set LED display command success");
                    break;
                default:
                    PrintMessage("Set LED display command failed! error code " + nRetValue);
                    break;
            }

            nRetValue = CHD.API.CHD825T.CHD825T_LEDView1(PortId, NetId, 1, textBox33.Text, (uint)(textBox33.Text.Length));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set LED display command success");
                    break;
                default:
                    PrintMessage("Set LED display command failed! error code " + nRetValue);
                    break;
            }


            nRetValue = CHD.API.CHD825T.CHD825T_LEDView1(PortId, NetId, 2, textBox32.Text, (uint)(textBox32.Text.Length));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set LED display command success");
                    break;
                default:
                    PrintMessage("Set LED display command failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// LOADP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            if (textBox31.Text.Length == 0 || int.Parse(textBox31.Text) > 65535)
            {
                MessageBox.Show("Loadp wrong");
                return;
            }
            int nRetValue = CHD.API.CHD825T.CHD825T_SetReadPointer(PortId, NetId, uint.Parse(textBox31.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set doorphone records read pointer success");
                    break;
                default:
                    PrintMessage("Set doorphone records read pointer failed! error code " + nRetValue);
                    break;
            }

        }



        /// <summary>
        /// Del User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            if (textBox26.Text.Length != 10)
            {
                MessageBox.Show("Card number input is not correct");
                return;
            }

            int nRetValue = CHD.API.CHD825T.CHD825T_DeleteCard(PortId, NetId, textBox26.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Delete user card success");
                    break;
                default:
                    PrintMessage("Delete user card failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {
            if (textBox26.Text.Length != 10)
            {
                MessageBox.Show("Card number input is not correct");
                return;
            }
            if (textBox27.Text.Length != 8)
            {
                MessageBox.Show("User ID input is not correct");
                return;
            }
            if (textBox28.Text.Length != 4)
            {
                MessageBox.Show("Password input is not correct");
                return;
            }
            CHD.API.SYSTEMTIME st = CHD.Common.ParasTime(dateTimePicker9.Value);
            int nRetValue = CHD.API.CHD825T.CHD825T_AddUser(PortId, NetId, textBox26.Text, textBox27.Text, textBox28.Text, ref st);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Add a user success");
                    break;
                default:
                    PrintMessage("Add a user failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Read user quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            int nCount = 0;

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadUserCount(PortId, NetId, out nCount);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success, " + nCount);
                        break;
                    }
                default:
                    PrintMessage("failed! error code " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// Del All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD825T.CHD825T_DeleteAllCard(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Delete all users success");
                    break;
                default:
                    PrintMessage("Delete all users failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            if (textBox26.Text.Length != 10)
            {
                MessageBox.Show("card is wrong");
                return;
            }
            CHD.API.SYSTEMTIME st;
            StringBuilder strCard = new StringBuilder();
            StringBuilder strUserID = new StringBuilder();
            StringBuilder strPwd = new StringBuilder();
            int nRetValue = CHD.API.CHD825T.CHD825T_ReadUser(PortId, NetId, textBox26.Text, strCard, strUserID, strPwd, out st);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage(String.Format("success, card: {0}  user ID: {1}  pwd: {2}  date:{3}/{4}/{5]", textBox26.Text, strUserID, strPwd, st.wYear, st.wMonth, st.wDay));
                        break;
                    }
                default:
                    PrintMessage("failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Set Record Pointer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            if (textBox37.Text.Length == 0 || int.Parse(textBox37.Text) > 65535)
            {
                MessageBox.Show("Savep input error");
                return;
            }
            if (textBox30.Text.Length == 0 || int.Parse(textBox30.Text) > 65535)
            {
                MessageBox.Show("Loadp input error");
                return;
            }
            int nRetValue = CHD.API.CHD825T.CHD825T_SetRecPoint(PortId, NetId, uint.Parse(textBox37.Text), uint.Parse(textBox30.Text), comboBox2.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("Set the door machine whole record area pointer to success");
                    break;
                default:
                    PrintMessage("Set the door machine whole record area pointer to failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Random Read Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox29.Text.Length == 0 || int.Parse(textBox29.Text) > 65535)
                {
                    MessageBox.Show("Loadp wrong");
                    return;
                }
                CHD.API.SYSTEMTIME RecTime = new CHD.API.SYSTEMTIME();
                StringBuilder szRecSource = new StringBuilder("");
                uint nRecState, nRecRemark;
                int dd = int.Parse(textBox29.Text);
                int bRet = 0;
                int nRetValue = CHD.API.CHD825T.CHD825T_ReadRecByRandom(PortId, NetId, dd, ref bRet,  szRecSource, ref RecTime, out nRecState, out nRecRemark);
                switch (nRetValue)
                {
                    case 0x00:
                        {
                            if (bRet != 0)
                            {

                                PrintMessage(String.Format("success, card:{0}  date:{1}/{2}/{3} {4}:{5}:{6}   state:{7}  remark:{8}", szRecSource, RecTime.wYear, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, RecTime.wSecond, nRecState, nRecRemark));
                                break;
                            }
                            else
                            {
                                PrintMessage(String.Format("success, flag:{0}  addr:{1}  date:{2}/{3} {4]:{5}  no:{6}", szRecSource, nRecState, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, nRecRemark));
                                break;
                            }
                        }
                    default:
                        PrintMessage("failed! error code " + nRetValue);
                        break;
                }
            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }




        /// <summary>
        /// Read One Record in order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szCard = new StringBuilder();
            int nRecState = 0, nRecRemark = 0;


            int Flag = 0, addr = 0, iNo = 0, nLen = 0;
            int nRetValue = CHD.API.CHD825T.CHD825T_ReadOneRec(PortId, NetId, szCard, out RecTime, out nRecState, out nRecRemark, out Flag, out addr, out iNo, out nLen);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        if (nLen == 14)
                        {

                            PrintMessage(String.Format("success, card:{0}  date:{1}/{2}/{3} {4}:{5}:{6}   state:{7}  remark:{8]", szCard, RecTime.wYear, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, RecTime.wSecond, nRecState, nRecRemark));
                            break;
                        }
                        else
                        {
                            PrintMessage(String.Format("success, no paper:{0}  card type:{1}  addr:{2} date:{3}/{4} {5}:{6}  No:{7}", (Flag >> 8), Flag & 0xff, addr, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, iNo));
                            break;
                        }
                    }
                default:
                    PrintMessage("failed! error code " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Read Record Parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button34_Click(object sender, EventArgs e)
        {
            int nBottom = 0;
            int nSavep = 0;
            int Loadp = 0;
            int mf = 0;
            int maxlen = 0;

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadBottom(PortId, NetId, out nBottom, out nSavep, out Loadp, out mf, out maxlen);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("success, Bottom:{0}  Savep:{1}   Loadp:{2} MF:{3}  maxlen:{4}", nBottom, nSavep, Loadp, mf, maxlen));
                    break;
                default:
                    PrintMessage("failed! error code " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// Query the latest event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME RecTime;
            StringBuilder szCard = new StringBuilder();
            int nRecState = 0, nRecRemark = 0;

            int Flag = 0, addr = 0, iNo = 0, nLen = 0;
            int nRetValue = CHD.API.CHD825T.CHD825T_ReadNewEvent(PortId, NetId, szCard, out RecTime, out nRecState, out nRecRemark, out Flag, out addr, out iNo, out nLen);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        if (nLen == 14)
                        {

                            PrintMessage(String.Format("success, card no:{0}  date:{1}/{2}/{3} {4}:{5}:{6}   state:{7}  remark:{8}", szCard, RecTime.wYear, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, RecTime.wSecond, nRecState, nRecRemark));
                            break;
                        }
                        else
                        {
                            PrintMessage(String.Format("success, no paper:{0}  card type :{1}  addr:{2} date:{3}/{4} {5}:{6}  no:{7]", (Flag >> 8), Flag & 0xff, addr, RecTime.wMonth, RecTime.wDay, RecTime.wHour, RecTime.wMinute, iNo));
                            break;
                        }
                    }
                default:
                    PrintMessage("failed! error code " + nRetValue);
                    break;
            }

        }

        #endregion



        #region 读取
        private void button47_Click(object sender, EventArgs e)
        {
            if (textBox43.Text.Length == 0 || int.Parse(textBox43.Text) > 65535)
            {
                MessageBox.Show("pos is wrong");
                return;
            }
            StringBuilder strCard = new StringBuilder();
            StringBuilder strUserID = new StringBuilder();
            StringBuilder strPwd = new StringBuilder();
            CHD.API.SYSTEMTIME st;
            int nRetValue = CHD.API.CHD825T.CHD825T_ReadUserByPos(PortId, NetId, int.Parse(textBox43.Text), strCard, strUserID, strPwd, out st);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        String dateTimeStr=st.wYear.ToString()+"/"+st.wMonth+"/"+ st.wDay;

                        PrintMessage(String.Format("success, card no: {0}  user ID: {1}  pwd: {2}  date:{3}", strCard.ToString().Substring(0,10), strUserID, strPwd, dateTimeStr));
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Read version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button48_Click(object sender, EventArgs e)
        {
            StringBuilder strVersion = new StringBuilder();

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadVersion(PortId, NetId, strVersion);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage("success, version:" + strVersion.ToString());
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Park State Set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button49_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD825T.CHD825T_SetParkState(PortId, NetId, comboBox6.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }




        /// <summary>
        /// Add Logo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button57_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "MyType Files (*.bmp)|*.bmp|All Files (*.*)|*.*";
            if (openFile.ShowDialog() == DialogResult.OK) {
                byte[] logoData=System.IO.File.ReadAllBytes(openFile.FileName);
                IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(logoData.Length) * logoData.Length);

                int nRetValue = CHD.API.CHD825T.CHD825T_WriteLogoInfo(PortId, NetId, buffer, (uint)(logoData.Length));
		switch (nRetValue)
		{
		case 0x00:{
			 PrintMessage("logo add success");
			break;
				  }
		default:
			 PrintMessage("logo add failed! error code: "+ nRetValue);
			break;
		}
            }
        }


        /// <summary>
        /// Control Relay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button51_Click(object sender, EventArgs e)
        {
            if (textBox51.Text.Length == 0 || int.Parse(textBox51.Text) == 0 || int.Parse(textBox51.Text) > 255)
            {
                MessageBox.Show("Data is wrong");
                return;
            }
            int nRetValue = CHD.API.CHD825T.CHD825T_RemoteControlRelay(PortId, NetId, (uint)(comboBox7.SelectedIndex + 1), byte.Parse(textBox51.Text));
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }

        }




        /// <summary>
        /// Set printer type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button45_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD825T.CHD825T_SetPrinterCmd6(PortId, NetId, comboBox5.SelectedIndex);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Print ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button44_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD825T.CHD825T_SetPrinterCmd5(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        ///  Set Customer Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button43_Click(object sender, EventArgs e)
        {
            if (textBox42.Text.Length != 2)
            {
                MessageBox.Show("Data is wrong");
                return;
            }
            int nRetValue = CHD.API.CHD825T.CHD825T_SetPrinterCmd4(PortId, NetId, textBox42.Text);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// Read Customer Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button39_Click(object sender, EventArgs e)
        {
            StringBuilder  btData=new StringBuilder();
	int nRetValue=CHD.API.CHD825T.CHD825T_ReadPrinterCmd4(PortId, NetId, btData);
	switch (nRetValue)
	{
	case 0x00:{

        PrintMessage("success, " + btData.ToString());
		break;
			  }
	default:
		 PrintMessage("failed! error code: "+ nRetValue);
		break;
	}
        }



        /// <summary>
        /// SetPrinterCmd3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button42_Click(object sender, EventArgs e)
        {
           
	if (textBox41.Text.Length > 48)
	{
		MessageBox.Show("Data is wrong");
		return;
	}
    byte[] cData = Encoding.Unicode.GetBytes(textBox41.Text);
 
	int nRetValue=CHD.API.CHD825T.CHD825T_SetPrinterCmd3(PortId, NetId, cData, (uint)(cData.Length));
	switch (nRetValue)
	{
	case 0x00:{
		 PrintMessage("success");
		break;
			  }
	default:
		 PrintMessage("failed! error code: "+ nRetValue);
		break;
	}
        }



        /// <summary>
        /// ReadPrinterCmd3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button38_Click(object sender, EventArgs e)
        {
            byte[] btData=new byte[60];
	 
	int nRetValue=CHD.API.CHD825T.CHD825T_ReadPrinterCmd3(PortId, NetId, btData);
	switch (nRetValue)
	{
	case 0x00:{

        PrintMessage("success, " + Encoding.Unicode.GetString(btData));
		break;
			  }
	default:
		 PrintMessage("failed! error code:"+ nRetValue);
		break;
	}
        }




        /// <summary>
        /// SetPrinterCmd2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button41_Click(object sender, EventArgs e)
        {
            
	if (textBox40.Text.Length > 48)
	{
		MessageBox.Show("Data is wrong");
		return;
	}

    byte[] cData = Encoding.Default.GetBytes(textBox40.Text);
	int nRetValue=CHD.API.CHD825T.CHD825T_SetPrinterCmd2(PortId, NetId, cData,  (uint)(cData.Length));
	switch (nRetValue)
	{
	case 0x00:{
		 PrintMessage("success");
		break;
			  }
	default:
		 PrintMessage("failed! error code: "+ nRetValue);
		break;
	}
        }



        /// <summary>
        /// ReadPrinterCmd2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            StringBuilder btData = new StringBuilder();

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadPrinterCmd2(PortId, NetId, btData);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage("success, " + btData.ToString());
                        break;
                    }
                default:
                    PrintMessage("failed! error code:" + nRetValue);
                    break;
            }

        }




        /// <summary>
        /// SetPrinterCmd1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button40_Click(object sender, EventArgs e)
        {
            if (textBox39.Text.Length > 48)
            {
                MessageBox.Show("Data is wrong");
                return;
            }

            byte[] cData = Encoding.Default.GetBytes(textBox39.Text);
            int nRetValue = CHD.API.CHD825T.CHD825T_SetPrinterCmd1(PortId, NetId, cData, (uint)(cData.Length));
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("success");
                        break;
                    }
                default:
                    PrintMessage("failed! error code: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// ReadPrinterCmd1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            StringBuilder btData = new StringBuilder();

            int nRetValue = CHD.API.CHD825T.CHD825T_ReadPrinterCmd1(PortId, NetId, btData);
            switch (nRetValue)
            {
                case 0x00:
                    {

                        PrintMessage("success, " +btData.ToString());
                        break;
                    }
                default:
                    PrintMessage("failed! error code:" + nRetValue);
                    break;
            }
        }
        #endregion

    }
}
