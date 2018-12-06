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

namespace CHD689Demo
{
    public partial class CHD689 : Form
    {
        private int portId;

        public CHD689()
        {
            InitializeComponent();
            InitDataView();
            for (int i = 1; i <= 32; i++)
            {
                cmbGroupNo.Items.Add(i.ToString("D2"));
            }
            this.Load += (o, e) =>
            {
                ConnectDevice conDivice = new ConnectDevice(1);
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

        public CHD689(int ptID)
        {

            InitializeComponent();
            this.portId = ptID;
            InitCmp();

        }




        #region 辅助函数

        private void InitCmp()
        {
            cmbCardType.SelectedIndex = 0;
            cmbShowConten.SelectedIndex = 0;
            cmbEmpCardNo.SelectedIndex = 0;
            cmbEmpTable.SelectedIndex = 0;
            cmbBanZu.SelectedIndex = 0;
            for (int i = 1; i <= 32; i++)
                cmbGroupNo.Items.Add(i.ToString("D2"));
            cmbGroupNo.SelectedIndex = 0;


            for (int i = 1; i <= 8; i++)
            {
                if (i == 1)
                {
                    dataGridView3.Rows.Add(new object[] { i, "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00", "00:00" });
                }
                else
                {
                    dataGridView3.Rows.Add(new object[] { i, "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
                }
            }



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




        private CHD.API.SYSTEMTIME ParasTime(DateTime dt)
        {
            if (dt == null)
                dt = DateTime.Now;
            CHD.API.SYSTEMTIME time = new CHD.API.SYSTEMTIME();
            time.wYear = (ushort)dt.Year;
            time.wMilliseconds = (ushort)dt.Millisecond;
            time.wDay = (ushort)dt.Day;
            time.wHour = (ushort)dt.Hour;
            time.wMonth = (ushort)dt.Month;
            time.wSecond = (ushort)dt.Second;
            time.wMinute = (ushort)dt.Minute;
            return time;

        }


        private void PrintExcuteResult(int result, string infor)
        {
            if (result == 0)
            {
                PrintMessage(infor);
            }
            else
            {
                PrintMessage(string.Format("{0}Failed!{1}", "set", result));
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


        private void InitDataView()
        {
            for (int i = 1; i <= 8; i++)
            {
                dataGridView3.Rows.Add(new Object[] { i, "00:00", "23:59", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF", "FF:FF" });
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
            CHD.API.SYSTEMTIME time = CHD.Common.ParasTime(dpAddValidDate.Value);
            int nRetValue = CHD.API.CHD689.AtAddUserWithName(PortId, NetId, txtAddCardNo.Text, txtAddOrder.Text, txtAddUserName.Text, txtAddUserId.Text, null, uint.Parse(txtAddBanCi.Text), ref time);

            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("增加用户成功! 卡号:{0} 用户ID:{1} 用户名:{2} 用户编号:{3}, 班次:{4}", txtCardNo.Text, txtAddOrder.Text, txtAddUserName.Text, txtAddOrder.Text, txtAddBanCi.Text));
                    break;
                case 0x07:		//SM内已满
                    PrintMessage("读取用户信息失败! 错误提示: 无权限!");
                    break;
                case 0xE2:		//SM内已满
                    PrintMessage("SM内已满!");
                    break;
                case 0xE6:		//用户ID号重复
                    PrintMessage(String.Format("用户ID:{0} 重复!", txtAddOrder.Text));
                    break;
                case 0xE7:		//卡号重复
                    PrintMessage(String.Format("卡号:{0} 重复!", txtAddCardNo.Text));
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
        /// 通过卡号读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadByCardNo_Click(object sender, EventArgs e)
        {
            StringBuilder userId = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            uint banci = 0;
            CHD.API.SYSTEMTIME time;
            int nRetValue = CHD.API.CHD689.AtReadUserByCardNo(PortId, NetId, txtCardNo.Text, userId, sb, out banci, out time);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        //成功
                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 班次:{2} 有效期:{3}", txtCardNo.Text, userId, banci, CHD.Common.ParasTime(time)));
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
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 通过卡号删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUserByCardNo_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD689.AtDelUserByCardNo(PortId, NetId, txtCardNo.Text);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(String.Format("删除卡号:{0} 成功! ", txtCardNo.Text));
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
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 通过用户ID读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadByUserId_Click(object sender, EventArgs e)
        {
            StringBuilder cardNo = new StringBuilder();
            uint banci = 0;
            CHD.API.SYSTEMTIME time;
            int nRetValue = CHD.API.CHD689.AtReadUserByUserID(PortId, NetId, txtUserId.Text, cardNo, null, out banci, out time);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功
                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 班次:{2} 有效期:{3}", txtUserId.Text, cardNo, banci, CHD.Common.ParasTime(time)));
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
        /// 通过位置读取用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadByPos_Click(object sender, EventArgs e)
        {
            byte[] cardNo = new byte[10];
            byte[] userId = new byte[10];
            uint banci = 0;
            CHD.API.SYSTEMTIME time;
            int nRetValue = CHD.API.CHD689.AtReadUserByPoint(PortId, NetId, uint.Parse(txtPosition.Text), cardNo, userId, null, out banci, out time);
            switch (nRetValue)
            {
                case 0x00:
                    {		//成功
                        PrintMessage(String.Format("读取用户信息成功! 卡号:{0} 用户ID:{1} 班次:{2} 有效期:{3}", Encoding.Default.GetString(cardNo), Encoding.Default.GetString(userId), banci, CHD.Common.ParasTime(time)));
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
        /// 读取用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserCount_Click(object sender, EventArgs e)
        {
            uint nUserCount = 0;
            int nRetValue = CHD.API.CHD689.AtReadUserCount(PortId, NetId, out nUserCount);
            if (nRetValue == 0x00)
            {
                txtUserCount.Text = nUserCount.ToString();
                PrintMessage(String.Format("读取用户数量成功! 用户数量: {0}", nUserCount));
            }
            else
            {
                PrintMessage(String.Format("读取用户数量失败! 错误代码: {0}", nRetValue));
            }
        }



        /// <summary>
        /// 清空所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllUser_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD689.AtDelAllUser(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//成功
                    PrintMessage(("删除设备所有用户成功!"));
                    break;
                case 0x07:		//无权限
                    PrintMessage(("读取用户信息失败! 错误提示: 无权限!"));
                    break;
                case 0xE4:		//无相应信息
                    PrintMessage(("读取用户信息失败! 错误提示: SM内部相应设置项的存储空间已空"));
                    break;
                case 0xE5:		//无相应信息
                    PrintMessage(("读取用户信息失败! 错误提示: SM内部无相应信息项"));
                    break;
                default:		//其他值表示失败
                    PrintMessage(String.Format("读取用户信息失败! 错误代码: {0}", nRetValue));
                    break;
            }

        }

        #endregion



        #region 记录管理
        /// <summary>
        /// 初始化记录区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitRec_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD689.AtInitRec(PortId, NetId);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(("初始化记录成功! "));
                    break;
                case 0x07:		//Failed
                    PrintMessage(("初始化记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage(("初始化记录失败! 错误代码:" + nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 读取设备记录区状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecState_Click(object sender, EventArgs e)
        {
            uint nBottom = 0, nSaveP = 0, nLoadP = 0, nMF = 0, nMaxLen = 0;

            int nRetValue = CHD.API.CHD689.AtReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMF, out nMaxLen);
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
                    PrintMessage("读取记录失败! 错误代码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取设备当前记录总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecCount_Click(object sender, EventArgs e)
        {
            uint nBottom = 0, nSaveP = 0, nLoadP = 0, nMF = 0, nMaxLen = 0, nRecordCount = 0;

            int nRetValue = CHD.API.CHD689.AtReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMF, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    if (nSaveP >= nLoadP)
                        nRecordCount = nSaveP - nLoadP;
                    else
                        nRecordCount = nMaxLen - (nLoadP - nSaveP);
                    PrintMessage("读取成功! 设备当前未读取记录数:" + nRecordCount);
                    break;
                default:		//Failed
                    PrintMessage("读取失败! 错误代码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 查询最新刷卡记录1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadLatest1_Click(object sender, EventArgs e)
        {
            uint nRet = 0;
            byte[] szInfo = new byte[10];
            int nRetValue = CHD.API.CHD689.AtReadCardNo(PortId, NetId, out nRet, szInfo);
            switch (nRetValue)
            {
                case 0x00:
                    {		//Success

                        PrintMessage(String.Format("查询最新刷卡记录成功! {0}  卡号: {1}", nRet == 0 ? "无新刷卡" : "有新刷卡", Encoding.Default.GetString(szInfo)));
                        break;
                    }
                case 0x07:		//Failed
                    PrintMessage(("查询最新刷卡记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage(("查询最新刷卡记录失败! 错误代码:" + nRetValue));
                    break;
            }
        }



        /// <summary>
        /// 查询最新刷卡记录1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadLatest2_Click(object sender, EventArgs e)
        {
            byte[] szInfo = new byte[10];
            CHD.API.SYSTEMTIME st;
            int nRetValue = CHD.API.CHD689.AtReadCardNo1(PortId, NetId, szInfo, out st);
            switch (nRetValue)
            {
                case 0x00:
                    {		//Success

                        PrintMessage(String.Format("查询最新刷卡记录成功! 卡号：{0}，时间:{1}", Encoding.Default.GetString(szInfo), CHD.Common.ParasTime(st)));
                        break;
                    }
                case 0x07:		//Failed
                    PrintMessage(("查询最新刷卡记录失败! 错误提示: 无权限"));
                    break;
                default:		//Failed
                    PrintMessage(("查询最新刷卡记录失败! 错误代码:" + nRetValue));
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
            uint m_nRecCount = 0;
            try
            {
                m_nRecCount = Convert.ToUInt16(txtRecovery.Text);
            }
            catch
            {
                MessageBox.Show("请正确输入要恢复的记录数");
                return;
            }

            uint nBottom = 0, nSaveP = 0, nLoadP = 0, nMF = 0, nMaxLen = 0;

            int nRetValue = CHD.API.CHD689.AtReadRecInfo(PortId, NetId, out nBottom, out nSaveP, out nLoadP, out nMF, out nMaxLen);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    {
                        if (nMF == 0x00)
                        {
                            if (m_nRecCount >= nLoadP)
                            {
                                m_nRecCount = nLoadP - nBottom;
                                nLoadP = nBottom;
                            }
                            else
                            {
                                nLoadP = nLoadP - m_nRecCount;
                            }
                        }
                        else if (nMF == 0x80)
                        {
                            if (m_nRecCount > (nLoadP - nSaveP))
                            {
                                m_nRecCount = (nLoadP - nSaveP);
                                nLoadP = nSaveP + 1;
                            }
                            else
                            {
                                nLoadP = nLoadP - m_nRecCount;
                            }
                        }
                        else if (nMF == 0x81)
                        {
                            PrintMessage("恢复记录失败! 提示: 所有记录已经被新记录覆盖! ");
                            break;
                        }
                        else
                        {
                            PrintMessage("恢复记录失败! 提示: 未知记录标识:" + nMF);
                            break;
                        }
                        nRetValue = CHD.API.CHD689.AtSetRecReadPoint(PortId, NetId, nLoadP, nMF);
                        switch (nRetValue)
                        {
                            case 0x00:		//Success
                                PrintMessage(String.Format("恢复记录成功! 共恢复:{0} 条记录", m_nRecCount));
                                txtRecovery.Text = m_nRecCount.ToString();
                                break;
                            case 0x07:		//Failed
                                PrintMessage("恢复记录失败! 提示: 无权限!");
                                break;
                            default:		//Failed
                                PrintMessage("恢复记录失败! 错误代码:" + nRetValue);
                                break;
                        }
                    }
                    break;
                default:		//Failed
                    PrintMessage("恢复记录失败! 提示: 获取记录状态失败! 错误代码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 带顺序读取记录
        /// </summary>
        /// <returns></returns>
        private bool ReadRecordWithPoint()
        {

            CHD.API.SYSTEMTIME RecTime = new CHD.API.SYSTEMTIME();
            byte[] szRecSource = new byte[10];
            uint nRecPos = 0, aa = 0, bb = 0;
            bool nRetFlag = false;

            int nRetValue = CHD.API.CHD689.AtReadRecWithPoint(PortId, NetId, ref nRecPos, szRecSource, ref RecTime, ref aa, ref bb);
            switch (nRetValue)
            {
                case 0x00:
                    {		//Success

                        PrintMessage(String.Format("卡号:{0} 时间:{1}", Encoding.Default.GetString(szRecSource), CHD.Common.ParasTime(RecTime)));
                        nRetFlag = true;
                        break;
                    }
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


        /// <summary>
        /// 带顺序读取一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecWithOrder_Click(object sender, EventArgs e)
        {
            ReadRecordWithPoint();
        }



        /// <summary>
        /// 带顺序读取所有记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAllRecWithOrder_Click(object sender, EventArgs e)
        {
            while (ReadRecordWithPoint()) { };

        }




        /// <summary>
        /// 读取记录
        /// </summary>
        /// <returns></returns>
        private bool ReadRecord()
        {
            CHD.API.SYSTEMTIME RecTime = new CHD.API.SYSTEMTIME();
            byte[] szRecSource = new byte[10];
            bool nRetFlag = false;
            uint aa = 0, bb = 0;
            int nRetValue = CHD.API.CHD689.AtReadOneRec(PortId, NetId, szRecSource, ref RecTime, ref aa, ref bb);
            switch (nRetValue)
            {
                case 0x00:		//Success
                    PrintMessage(String.Format("卡号:{0} 时间:{1}", Encoding.Default.GetString(szRecSource), CHD.Common.ParasTime(RecTime)));
                    nRetFlag = true;
                    break;
                case 0xE4:		//Failed
                    PrintMessage(("设备内无记录!"));
                    break;
                case 0xE5:		//Failed
                    PrintMessage(("设备所有记录已经读完!"));
                    nRetFlag = false;
                    break;
                default:		//Failed
                    PrintMessage(("读取记录失败! 错误代码:" + nRetValue));
                    nRetFlag = false;
                    break;
            }

            return nRetFlag;
        }



        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadRecByOrder_Click(object sender, EventArgs e)
        {
            ReadRecord();
        }



        /// <summary>
        /// 读取全部记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAllByOrder_Click(object sender, EventArgs e)
        {
            while (ReadRecord())
            { }
        }

        #endregion



        #region 控制器配置
        private void DoComboDataExchange(uint ctrParam)
        {
            uint nSel = 0;

            nSel = ((ctrParam & 0xC0) >> 6);
            cmbCardType.SelectedIndex = (int)nSel;

            nSel = (ctrParam & 0x20);
            cmbShowConten.SelectedIndex = (int)nSel;

            nSel = (ctrParam & 0x10);
            cmbEmpCardNo.SelectedIndex = (int)nSel;

            nSel = (ctrParam & 0x04);
            cmbEmpTable.SelectedIndex = (int)nSel;

            nSel = (ctrParam & 0x01);
            cmbBanZu.SelectedIndex = (int)nSel;
        }


        /// <summary>
        /// 读取设备参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCtr_Click(object sender, EventArgs e)
        {
            uint nCtrlParam = 0, nRelayDelay = 0, nRepeatDelay = 0, nAlarmDelay = 0;
            int nRetValue = CHD.API.CHD689.AtReadCtrlParam(PortId, NetId, ref nCtrlParam, ref nRelayDelay, ref nRepeatDelay, ref nAlarmDelay);
            switch (nRetValue)
            {
                case 0x00:
                    //m_nCtrlByte = nCtrlParam;
                    txtRelayDelay.Text = nRelayDelay.ToString();
                    txtRepeatDelay.Text = nRepeatDelay.ToString();
                    txtAlarmDelay.Text = nAlarmDelay.ToString();
                    DoComboDataExchange(nCtrlParam);
                    PrintMessage(String.Format("读取考勤钟控制参数成功! 控制参数:{0} 继电器驱动延时: {1} 重复刷卡控制时间: {2} 报警延时:{3}", nCtrlParam, nRelayDelay, nRepeatDelay, nAlarmDelay));
                    break;
                default:
                    PrintMessage("读取考勤钟控制参数失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置设备参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetCtr_Click(object sender, EventArgs e)
        {
            int m_nCtrlByte = 0;
            int[] nItem = new int[5];
            nItem[0] = cmbCardType.SelectedIndex;
            nItem[1] = cmbShowConten.SelectedIndex;
            nItem[2] = cmbEmpCardNo.SelectedIndex;
            nItem[3] = cmbEmpTable.SelectedIndex;
            nItem[4] = cmbBanZu.SelectedIndex;

            if (nItem[0] == 0)
            {
                m_nCtrlByte = 128 + nItem[4] * 32 + nItem[3] * 16 + nItem[2] * 4 + nItem[1];
            }
            else if (nItem[0] == 1)
            {
                m_nCtrlByte = nItem[4] * 32 + nItem[3] * 16 + nItem[2] * 4 + nItem[1];
            }
            else if (nItem[0] == 2)
            {
                m_nCtrlByte = 64 + nItem[4] * 32 + nItem[3] * 16 + nItem[2] * 4 + nItem[1];
            }

            int nRetValue = CHD.API.CHD689.AtSetCtrlParam(PortId, NetId, (uint)m_nCtrlByte);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置设置考勤钟控制参数!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置设置考勤钟控制参数失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置设置考勤钟控制参数失败! 错误码:" + nRetValue);
                    break;
            }
            nRetValue = CHD.API.CHD689.AtSetClockOpenTime(PortId, NetId, uint.Parse(txtRelayDelay.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置设置考勤钟确认继电器执行时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置设置考勤钟确认继电器执行时间败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置设置考勤钟确认继电器执行时间失败! 错误码:" + nRetValue);
                    break;
            }
            nRetValue = CHD.API.CHD689.AtSetClockRepeatDelay(PortId, NetId, uint.Parse(txtRepeatDelay.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置考勤钟允许重复打卡时间间隔成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置考勤钟允许重复打卡时间间隔失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置考勤钟允许重复打卡时间间隔失败! 错误码:" + nRetValue);
                    break;
            }

            nRetValue = CHD.API.CHD689.AtSetClockAlarmDelay(PortId, NetId, uint.Parse(txtAlarmDelay.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置考勤钟报时继电器输出时间成功!");
                    break;
                case 0x07:		//无权限
                    PrintMessage("设置考勤钟报时继电器输出时间失败! 错误提示: 无权限!");
                    break;
                default:
                    PrintMessage("设置考勤钟报时继电器输出时间失败! 错误码:" + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 读取考勤报时组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadGroupCtr_Click(object sender, EventArgs e)
        {
            uint nIndex = 0, nAlarmHour = 0, nAlarmMin = 0;
            nIndex = (uint)(cmbGroupNo.SelectedIndex + 1);
            int nRetValue = CHD.API.CHD689.AtReadClockAlarm(PortId, NetId, nIndex, ref nAlarmHour, ref nAlarmMin);
            switch (nRetValue)
            {
                case 0x00:

                    PrintMessage(String.Format("读取 {0} 报时组成功! {1}:{2}", nIndex.ToString("D2"),nAlarmHour,nAlarmMin));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("读取 {0} 报时组失败! 错误提示: 无权限!", nIndex.ToString("D2")));
                    break;
                default:
                    PrintMessage(String.Format("读取 {0} 报时组失败! 错误码:{1}", nIndex.ToString("D2"), nRetValue));
                    break;
            }
        }




        /// <summary>
        /// 设置考勤报时组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetGroupCtr_Click(object sender, EventArgs e)
        {

            uint nIndex = 0;
            CHD.API.SYSTEMTIME st;
            st.wHour = (ushort)(dpCtrTime.Value.Hour);
            st.wMinute = (ushort)(dpCtrTime.Value.Minute);
            st.wSecond = (ushort)(dpCtrTime.Value.Second);

            nIndex = (uint)(cmbGroupNo.SelectedIndex + 1);
            int nRetValue = CHD.API.CHD689.AtSetClockAlarm(PortId, NetId, nIndex, st.wHour, st.wMinute);
            switch (nRetValue)
            {
                case 0x00:

                    PrintMessage(String.Format("设置 {0} 报时组成功!", nIndex.ToString("D2")));
                    break;
                case 0x07:		//无权限
                    PrintMessage(String.Format("设置 {0} 报时组失败! 错误提示: 无权限!", nIndex.ToString("D2")));
                    break;
                default:
                    PrintMessage(String.Format("设置 {0} 报时组失败! 错误码:{1}", nIndex.ToString("D2"), nRetValue));
                    break;
            }
        }

        #endregion



        #region 时间段设置
        /// <summary>
        /// 读取时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadPeriod_Click(object sender, EventArgs e)
        {
            StringBuilder szTimeSlot = new StringBuilder();
            String strTemp, strItemText;
            for (int i = 0; i < 8; ++i)
            {
                int nRetValue = CHD.API.CHD689.AtReadPeriod(PortId, NetId, (uint)(i + 1), szTimeSlot);
                if (nRetValue == 0x00)
                {
                    strTemp = szTimeSlot.ToString();
                    for (int j = 1; j < 13; ++j)
                    {
                        strItemText = strTemp.Substring(j * 4 - 4, 2);// + _T(":") + strTemp.Mid(i*4+2, 2);
                        strItemText += ":";
                        strItemText += strTemp.Substring(j * 4 - 4 + 2, 2);
                        dataGridView3.Rows[i].Cells[j].Value = strItemText;
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
        private void btnSetPeriod_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; ++i)
            {
                String strText = String.Empty;
                for (int j = 1; j < 13; ++j)
                {
                    strText += dataGridView3.Rows[i].Cells[j].Value.ToString();
                }
                string timeStr = strText.Replace(":", "");
                int nRetValue = CHD.API.CHD689.AtSetPeriod(PortId, NetId, (uint)i, timeStr);
                if (nRetValue == 0)
                {
                    PrintMessage(String.Format("设置第{0}张表: {1} 成功!", i.ToString("D2"), timeStr));
                }
                else if (nRetValue == 0x07)
                {
                    PrintMessage(String.Format("设置第{0}张表: {1} 失败! 错误码: 无权限", i.ToString("D2"), timeStr));
                }
                else
                {
                    PrintMessage(String.Format("设置第{0}张表: {1} 失败! 错误码: {2}", i.ToString("D2"), timeStr, nRetValue));
                }
            }

            PrintMessage("设置时间段完毕!");
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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



        #region 综合设置
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangePwd_Click(object sender, EventArgs e)
        {

            int nRetValue = CHD.API.CHD689.NewPassword(PortId, NetId, new StringBuilder(txtSysPwd.Text), new StringBuilder(txtDevPwd.Text));
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("修改设备密钥成功");
                    break;
                default:
                    PrintMessage("修改设备密钥失败! 错误码:" + nRetValue);
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
            byte[] szVersion = new byte[15];

            int nRetValue = CHD.API.CHD689.AtReadVersion(PortId, NetId, szVersion);
            switch (nRetValue)
            {
                case 0x00:
                    {
                        PrintMessage("读取设备版本信息成功:" + Encoding.Default.GetString(szVersion));
                        break;
                    }
                default:
                    {
                        PrintMessage("读取设备信息失败! 错误码:" + nRetValue);
                        break;
                    }
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

            int nRetValue = CHD.API.CHD689.AtReadDateTime(PortId, NetId, out stime);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage(String.Format("读取时间：{0} 星期{1}", CHD.Common.ParasTime(stime).ToString(), stime.wDayOfWeek));
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

            int nRetValue = CHD.API.CHD689.AtSetDateTime(PortId, NetId, ref stime);
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
        /// 远程开启继电器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDelay_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD689.AtSetOpenDoor(PortId, NetId, 1);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("远程开启继电器成功");
                    break;
                default:
                    PrintMessage("远程开启继电器失败! 错误码: " + nRetValue);
                    break;
            }
        }



        /// <summary>
        /// 设置显示屏显示文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetShowText_Click(object sender, EventArgs e)
        {
            int nRetValue = CHD.API.CHD689.AtSetHintNow(PortId, NetId, 5, textBox12.Text);
            switch (nRetValue)
            {
                case 0x00:
                    PrintMessage("设置显示文字成功:" + textBox12.Text);
                    break;
                default:
                    PrintMessage("设置显示文字失败! 错误码: " + nRetValue);
                    break;
            }
        }
        #endregion



    }
}
