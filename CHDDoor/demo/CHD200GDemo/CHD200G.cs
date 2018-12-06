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

namespace CHD200GDemo
{
    public partial class CHD200G : Form
    {
        private int portId;
        private uint m_nDataSize;
        private byte[] fingerData;
        private byte[] m_pFingerData = new byte[1024 * 2];
        public byte[] M_pFingerData { get { return m_pFingerData; } set { m_pFingerData = value; } }
        public uint M_nDataSize { get { return m_nDataSize; } set { m_nDataSize = value; } }
        public CHD200G()
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

        public CHD200G(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            InitCoponent();

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

        public void InitCoponent()
        {
            foreach (Control c in grpCtrParam.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox cmb = c as ComboBox;
                    cmb.SelectedIndex = 0;
                }
            }
            foreach (Control c in grpDevMgr.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox cmb = c as ComboBox;
                    cmb.SelectedIndex = 0;
                }
            }
            cmbDeviceType.SelectedIndex = 2;
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
        public void PrintMessage(String msg)
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



        /// <summary>
        /// 确认权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOn_Click(object sender, EventArgs e)
        {
            if (CheckPwd())
            {
                int result = CHD.API.CHD200G.FrLinkOn(PortId, NetId, txtPwd.Text);
                PrintExcuteResult(result, "权限确认成功", "权限确认失败!");
            }
        }



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkOff_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrLinkOff(PortId, NetId), "取消权限成功", "取消权限失败! ");
        }




        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPwd_Click(object sender, EventArgs e)
        {
            if (CheckPwd())
            {
                PrintExcuteResult(CHD.API.CHD200G.FrSetDevPwd(PortId, NetId, new StringBuilder(txtPwd.Text)), "设置密码成功", "设置密码失败! ");
            }
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <returns></returns>
        private bool CheckPwd()
        {
            if (txtPwd.Text.Length != 10)
            {
                PrintMessage("密码长度不是10位, 请重新输入");
                return false;
            }
            char[] pwdChar = txtPwd.Text.ToCharArray();
            for (int i = 0; i < pwdChar.Length; i++)
            {
                if (!((pwdChar[i] >= 'A' && pwdChar[i] <= 'F') || (pwdChar[i] >= '0' && pwdChar[i] <= '9')))
                {
                    PrintMessage("密码输入有字母时,输入范围为大写的A~F");
                    return false;
                }
            }
            return true;
        }



        /// <summary>
        /// 设置管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetAdmin_Click(object sender, EventArgs e)
        {
            string admin1 = "66666666";
            string admin2 = "88888888";
            PrintExcuteResult(CHD.API.CHD200G.FrSetAdmin(PortId, NetId, 0, admin1, 0, admin2), String.Format("设置超级管理员成功, 超级管理员1:{0}，超级管理员2:{1}", admin1, admin2), "设置超级管理员失败!");
        }



        /// <summary>
        /// 更改网络地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeNetAddr_Click(object sender, EventArgs e)
        {
            uint netGrop = 0;
            uint newNetId = 0;
            try
            {
                netGrop = Convert.ToUInt16(txtNetGroup.Text);
                newNetId = Convert.ToUInt16(txtNewNetId.Text);
            }
            catch
            {
                PrintMessage("请正确输入网络分组或新网络ID");
                return;
            }
            if (netGrop == 0 || netGrop > 15)
            {
                PrintMessage("网络Group输入错误");
                return;
            }
            if (newNetId == 0 || newNetId > 254)
            {
                PrintMessage("网络ID输入错误");
                return;
            }

            PrintExcuteResult(CHD.API.CHD200G.FrNewNetAddr1(PortId, NetId, netGrop, newNetId), "设置网络ID成功", "设置网络ID失败!");
        }



        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetBaudrate_Click(object sender, EventArgs e)
        {
           // PrintExcuteResult(CHD.API.CHD200G.FrSetBaudrate(PortId, NetId, (uint)(cmbBaudrate.SelectedIndex)), "设置波特率成功", "设置波特率失败!");
        }



        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadVersion_Click(object sender, EventArgs e)
        {
            byte[] version = new byte[30];
            PrintExcuteResult(CHD.API.CHD200G.FrReadVersion(PortId, NetId, version), String.Format("读取设备版本成功:{0}", Encoding.Default.GetString(version)), "读取设备版本失败!");
        }



        /// <summary>
        /// 读取管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadAdmin_Click(object sender, EventArgs e)
        {
            int admType1, admType2;
            StringBuilder admin1 = new StringBuilder();
            StringBuilder admin2 = new StringBuilder();
            int result = CHD.API.CHD200G.FrReadAdmin(PortId, NetId, out admType1, admin1, out admType2, admin2);
            if (result == 0)
            {
                PrintMessage(String.Format("读取管理员成功, 管理员1:{0} 类型:{1}，超级管理员2:{2} 类型:{3}", admin1, admType1, admin2, admType2));
            }
            else
            {
                PrintMessage("读取管理员失败!");
            }
        }



        /// <summary>
        /// 删除指纹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelFinger_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrDelOneFinger(PortId, NetId, uint.Parse(txtFingerIndex.Text)), "删除指纹成功", "删除指纹失败!");
        }



        /// <summary>
        /// 注册指纹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegFinger_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD200G.FrLinkOn(PortId, NetId, txtPwd.Text);
            if (nRt == 0)
            {
                PrintMessage("权限确认成功");
                System.Threading.Thread.Sleep(30);//延时一下
                if (cmbDeviceType.SelectedIndex == 2)
                {
                    nRt = CHD.API.CHD200G.FrRegFingerStart1(PortId, NetId, int.Parse(txtBlockSize.Text));
                }
                else
                {
                    nRt = CHD.API.CHD200G.FrRegFingerStart(PortId, NetId);
                }

                if (nRt == 0)
                {
                    PrintMessage("启动指纹注册成功");
                    m_nDataSize = 0;//清空数据
                    txtFingerData.Text = "";//刷新显示
                    RegFinger regFinger = new RegFinger(PortId, NetId, cmbDeviceType.SelectedIndex==2);
                    regFinger.Location = new Point(this.Location.X/2, this.Location.Y/2);
                    regFinger.OnRegStateChange += ((s) => { PrintMessage(s); });
                    regFinger.OnReadFingerDataComplete += (fg) => { fingerData = fg.ToArray(); };
                    regFinger.ShowDialog();
                    if (nRt == 0)
                    {
                        PrintMessage("停止指纹注册成功!");
                    }
                    else
                    {
                        PrintMessage(String.Format("停止指纹注册失败! 错误码:{0}", nRt));

                    }
                }
                else
                {
                    PrintMessage(String.Format("启动指纹注册失败! 错误码: {0}", nRt));

                }

            }
            else
            {
                PrintMessage(String.Format("权限确认失败! 错误码: {0}", nRt));
            }
        }



        /// <summary>
        /// 删除所有指纹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllFinger_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrDelAllFinger(PortId, NetId), "删除所有指纹成功", "删除所有指纹失败!");
        }



        /// <summary>
        /// 刷新指纹数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshFinger_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrRefreshData(PortId, NetId), "刷新指纹数据成功", "刷新指纹数据失败!");
        }



        /// <summary>
        /// 设置控制参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetCtrParam_Click(object sender, EventArgs e)
        {
            int[] iCurSelect = new int[6];
            iCurSelect[0] = cmbD01.SelectedIndex;
            iCurSelect[1] = cmbD4.SelectedIndex;
            iCurSelect[2] = cmbD5.SelectedIndex;
            iCurSelect[3] = cmbD6.SelectedIndex;
            iCurSelect[4] = cmbD7.SelectedIndex;
            iCurSelect[5] = cmbWorkMode.SelectedIndex;
            uint iData = (uint)(iCurSelect[5] * 256 + iCurSelect[4] * 128 + iCurSelect[3] * 64 + iCurSelect[2] * 32 + iCurSelect[1] * 16 + iCurSelect[0]);
            PrintExcuteResult(CHD.API.CHD200G.FrSetCtrl1(PortId, NetId, iData), "设置控制参数成功", "设置控制参数失败!");
        }



        /// <summary>
        /// 读取控制参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCtrParam_Click(object sender, EventArgs e)
        {
            uint iData = 0;
            int nRt = CHD.API.CHD200G.CHD200H_FrGetCtrl1(PortId, NetId, out iData);
            if (nRt == 0)
            {
                PrintMessage("读取控制参数成功");

                String str = "工作模式: ";
                uint nWorkMode = iData / 256;

                if (nWorkMode == 0)
                {
                    str += " 按键或卡或指纹 ";
                }
                else if (nWorkMode == 1)
                {
                    str += " 刷卡+指纹 ";
                }
                else if (nWorkMode == 2)
                {
                    str += " ID+指纹 ";
                }
                else if (nWorkMode == 3)
                {
                    str += " ID+密码 ";
                }
                else if (nWorkMode == 4)
                {
                    str += " 刷卡+密码 ";
                }
                else if (nWorkMode == 5)
                {
                    str += " 指纹+密码 ";
                }
                else if (nWorkMode == 6)
                {
                    str += " 卡指纹+指纹 ";
                }
                else if (nWorkMode == 7)
                {
                    str += " 刷卡+指纹+密码 ";
                }

                iData = iData - nWorkMode * 256;
                String tcConvert = Convert.ToString(iData, 2);
                if (tcConvert.Length != 8)
                {
                    int iLength =8-tcConvert.Length;
                    for (int i = 0; i < iLength; i++)
                    {
                        tcConvert += ("0" + tcConvert);
                    }
                }

                if (tcConvert[0] == '1')
                {
                    str += " 按键韦根输出格式: 8韦根 ";
                }
                else
                {
                    str += " 按键韦根输出格式: 4韦根 ";
                }
                if (tcConvert[1] == '1')
                {
                    str += " 输出数据: 输出用户ID ";
                }
                else
                {
                    str += " 输出数据: 输出用户卡号 ";
                }

                if (tcConvert[2] == '1')
                {
                    str += " 安装位置: 出 ";
                }
                else
                {
                    str += " 安装位置: 进 ";
                }

                if (tcConvert[3] == '1')
                {
                    str += " 编号转换方法: BCD码 ";
                }
                else
                {
                    str += " 编号转换方法: 十六进制 ";
                }
                if (tcConvert[6] == '1')
                {
                    str += " 维根输出格式: 44BIT ";
                }
                else
                {
                    if (tcConvert[7] == '1')
                    {
                        str += " 维根输出格式: 26BIT ";
                    }
                    else
                    {
                        str += " 维根输出格式: 28BIT ";
                    }
                }

                PrintMessage(str);
            }
            else
            {
                PrintMessage(String.Format("读取控制参数失败! 错误码: {0}", nRt));
            }

        }



        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            uint m_nFingerIndex1 = Convert.ToUInt16(txtFingerNo1.Text);
            uint m_nFingerIndex2 = Convert.ToUInt16(txtFingerNo2.Text);
            uint m_nFingerIndex3 = Convert.ToUInt16(txtFingerNo3.Text);
            if (m_nFingerIndex1 > 60000 || m_nFingerIndex2 > 60000 || m_nFingerIndex3 > 60000)
            {
                PrintMessage("指纹编号设置不正确,请重新设置");
                return;
            }
            if (txtUserPwd.Text.Length != 4)
            {
                PrintMessage("密码长度不对,长度应为4");
                return;
            }
            if (txtUserId.Text.Length != 8)
            {
                PrintMessage("用户ID长度不对");
                return;
            }

            if (cmbDeviceType.SelectedIndex == 0)//CHD200A设备
            {
                PrintExcuteResult(CHD.API.CHD200G.FrAddOneUser(PortId, NetId, txtUserId.Text, txtUserName.Text, txtOrderNo.Text, txtUserPwd.Text, m_nFingerIndex1, m_nFingerIndex2, m_nFingerIndex3), "增加用户成功", "增加用户失败!");
            }
            else //CHD200G设备
            {
                PrintExcuteResult(CHD.API.CHD200G.FrAddOneUserEx(PortId, NetId, txtUserId.Text, txtCarNo.Text, txtUserName.Text, txtOrderNo.Text, txtUserPwd.Text, m_nFingerIndex1, m_nFingerIndex2, m_nFingerIndex3), "增加用户成功", "增加用户失败!");

            }
        }



        /// <summary>
        /// 读取用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUser_Click(object sender, EventArgs e)
        {
            String strMsg;

            int nRt;
            uint nFingerIndex1, nFingerIndex2, nFingerIndex3;
            StringBuilder szUserID = new StringBuilder();
            StringBuilder szCardNo = new StringBuilder();
            StringBuilder szUserName = new StringBuilder();
            StringBuilder szUserNo = new StringBuilder();
            StringBuilder szUserPwd = new StringBuilder();

            if (cmbDeviceType.SelectedIndex == 0) //CHD200A
            {
                //按位置读取
                //int nRt = FrReadUserInfo(m_nIndex, btDeviceID, nPos, szUserID, szUserName, szUserNo, szUserPwd, &nFingerIndex1, &nFingerIndex2, &nFingerIndex3);
                //按用户ID读取
                nRt = CHD.API.CHD200G.FrReadUserInfoByUserID(PortId, NetId, txtUserId.Text, szUserName, szUserNo, szUserPwd, out nFingerIndex1, out nFingerIndex2, out nFingerIndex3);
                if (nRt == 0)
                {

                    strMsg = String.Format("读取用户信息成功, 用户ID:{0} 用户名:{1} 用户编号:{2} 密码:{3} 指纹1:{4} 指纹2:{5} 指纹3:{6}", txtUserId.Text, szUserName, szUserNo, szUserPwd, nFingerIndex1, nFingerIndex2, nFingerIndex3);
                }
                else
                {
                    strMsg = String.Format("读取用户信息失败! 错误码: {0}", nRt);
                }
            }

            else
            {
                //按位置读取
                //int nRt = FrReadUserInfoEx(m_nIndex, btDeviceID, nPos, szUserID, szCardNo, szUserPwd, &nFingerIndex1, &nFingerIndex2, &nFingerIndex3);
                //按用户ID读取
                nRt = CHD.API.CHD200G.FrReadUserInfoByUserIDEx(PortId, NetId, txtUserId.Text, szCardNo, szUserPwd, out nFingerIndex1, out nFingerIndex2, out nFingerIndex3);
                if (nRt == 0)
                {

                    strMsg = String.Format("读取用户信息成功, 用户ID:{0} 卡号:{1} 密码:{2} 指纹1:{3} 指纹2:{4} 指纹3:{5}", txtUserId.Text, szCardNo, szUserPwd, nFingerIndex1, nFingerIndex2, nFingerIndex3);
                }
                else
                {
                    strMsg = string.Format("读取用户信息失败! 错误码: {0}", nRt);
                }
            }
            PrintMessage(strMsg);
        }



        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUser_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrDelOneUser(PortId, NetId, txtUserId.Text), "删除用户成功", "删除用户失败!");
        }



        /// <summary>
        /// 读取用户数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadUserCount_Click(object sender, EventArgs e)
        {
            uint nUserCnt = 0, nFingerCnt = 0;
            int nRt = CHD.API.CHD200G.FrReadUserNum(PortId, NetId, out nUserCnt, out nFingerCnt);
            if (nRt == 0)
            {
                PrintMessage(string.Format("读取用户数量成功, 用户数量:{0}， 指纹数量:{1}", nUserCnt, nFingerCnt));
            }
            else
            {
                PrintMessage(String.Format("读取用户数量失败! 错误码: {0}", nRt));
            }
        }



        public string ShowData(Byte[] pFingerData, uint m_nDataSize)
        {
            String strFinger = "";
            for (uint i = 0; i < m_nDataSize; ++i)
            {
                strFinger += pFingerData[i].ToString("X2");
                if ((i + 1) % 68 == 0)
                {
                    //strFinger += "\r\n"; //加入回车
                }
            }
           return strFinger;

        }


        /// <summary>
        /// 删除所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelAllUser_Click(object sender, EventArgs e)
        {
            PrintExcuteResult(CHD.API.CHD200G.FrDelAllUser(PortId, NetId), "删除所有用户成功", "删除所有用户失败!");
        }



        /// <summary>
        /// 读取指纹数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFinger_Click(object sender, EventArgs e)
        {
            byte[] pTmp = new byte[256 * 10];
            StringBuilder sb = new StringBuilder();
            bool bReadEnd = false;
            uint nDataSize = 0, nSize = 0, nDataCount = 0;

            int nRt = 0;
            if (cmbDeviceType.SelectedIndex == 2)
            {
                nRt = CHD.API.CHD200G.FrGetFingerBegin1(PortId, NetId, uint.Parse(txtFingerIndex.Text), ref nDataSize, int.Parse(txtBlockSize.Text));
            }
            else
            {
                //nRt = FrGetFingerBegin(m_nIndex, btDeviceID, m_nFingerIndex, &nDataSize);
            }

            if (nRt == 0)
            {
                if (nDataSize != 0)
                {
                    nDataCount = 0;//读取到的数据计数
                    m_nDataSize = 0;
                    txtFingerData.Text = "";//更新数据显示控件
                    System.Threading.Thread.Sleep(50);//这里稍做一下延时
                    for (uint i = 0; i < 19 && bReadEnd == false; ++i)//最多循环8次把数据读完
                    {
                        for (int j = 0; j < 3; ++j)//读取数据，如果读取失败则继续读取(重复3次)
                        {
                            if (cmbDeviceType.SelectedIndex == 2)
                            {
                                nRt = CHD.API.CHD200G.FrGetFingerData1(PortId, NetId, i, out nSize, pTmp);
                            }
                            else
                            {
                                //nRt = FrGetFingerData(m_nIndex, btDeviceID, i, &nSize, pTmp);
                            }

                            if (nRt == 0)
                            {
                                //memcpy(m_pFingerData+nDataCount, pTmp, nSize);//保存数据
                                nDataCount += nSize;
                                sb.Append(ShowData(pTmp,nSize));
                                if (nDataCount == nDataSize)//判断时候数据是否读完
                                {
                                    bReadEnd = true;
                                    m_nDataSize = nDataCount;//更新数据长度
                                    //ShowData(pTmp, m_nDataSize);
                                    this.txtFingerData.Text = sb.ToString();
                                }
                                break; //读取指纹包的循环
                            }
                            else
                            {
                                PrintMessage(String.Format("尝试第 %d 次读取指纹数据包失败! 错误码:{0}", j + 1, nRt));
                            }
                        }

                        if (nRt == 0)
                        {
                            PrintMessage(String.Format("第 {0} 次读取成功! 数据大小:{1}, 累计总大小:{2}", i + 1, nSize, nDataCount));
                        }
                        else
                        {
                            PrintMessage(String.Format("第 {0} 次读取失败!", i + 1));
                            break;
                        }
                    }
                }
                else
                {
                    PrintMessage(String.Format("读取指纹数据成功! 数据长度为{0}", nDataSize));
                }
            }
            else
            {
                PrintMessage(String.Format("读取指纹数据失败! 错误码: {0}", nRt));
            }
        }



        /// <summary>
        /// 新增指纹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFinger_Click(object sender, EventArgs e)
        {
            if (fingerData == null)
            {
                PrintMessage("无指纹数据，请先注册指纹数据!");
                return;
 
            }
            int m_nDataSize =this.fingerData.Length;
            int nSize = 0;
            int fingerIndex = int.Parse(txtFingerIndex.Text);
            if (fingerIndex < 9 || fingerIndex > 60000)
            {
                PrintMessage("指纹编号要大于9，小于60000");
                return;
            }
            if (m_nDataSize == 0)
            {
                PrintMessage("无指纹数据，请先注册指纹数据!");
                return;
            }

            int nRt = 0;
            if (cmbDeviceType.SelectedIndex == 2)
            {
                nRt = CHD.API.CHD200G.FrSetFingerBegin1(PortId, NetId, (uint)fingerIndex, (uint)m_nDataSize, 80);//
            }
            else
            {
                //nRt = FrSetFingerBegin(m_nIndex, btDeviceID, m_nFingerIndex, m_nDataSize);//
            }
            if (nRt == 0)
            {
                int nLoop = (m_nDataSize + 80 - 1) / 80;//计算循环次数
                PrintMessage(String.Format("指纹数据大小:{0}, 每次发送:{1} 共需要 {2} 次发送完", m_nDataSize, 80, nLoop));

                for (uint i = 0; i < nLoop; ++i)//循环把数据写入指纹机
                {
                    nSize = 80; //每次发送150字节数据
                    if (i == nLoop - 1) nSize = m_nDataSize % 80; //计算最后一次数据大小

                    for (int j = 0; j < 3; ++j)//发送数据，如果发送失败则继续发送(重复3次)
                    {
                        System.Threading.Thread.Sleep(25);//稍微延时一下
                        if (cmbDeviceType.SelectedIndex == 2)
                        {

                            //复制数据
                            uint length = (uint)(this.fingerData.Length - 80 * i);
                            Byte[] data = new byte[length];
                            for (uint k = 0; k < length; k++)
                            {
                                data[k] = fingerData[80 * i + k];
                            }


                            nRt = CHD.API.CHD200G.FrSetFingerData1(PortId, NetId, i, nSize, data);
                        }
                        else
                        {
                            //nRt = FrSetFingerData(m_nIndex, btDeviceID, i, nSize, m_pFingerData+80*i);
                        }

                        if (nRt == 0)
                            break; //发送指纹包的循环
                        else
                        {
                            PrintMessage(String.Format("尝试第 {0} 次发送指纹数据包失败! 错误码:{1}", j + 1, nRt));

                        }
                    }
                    if (nRt != 0)
                    {
                        PrintMessage(String.Format("第 {0} 次发送失败!", i + 1));
                        break;
                    }
                    else
                    {
                        PrintMessage(String.Format("第 {0} 次发送成功! 数据大小:{1}", i + 1, nSize));
                    }
                }
                if (nRt == 0) PrintMessage("成功下发指纹数据!");
                //指纹数据全部发送完，
                //发送指令通知指纹机结束本书操作
                if (cmbDeviceType.SelectedIndex == 2)
                {
                    nRt = CHD.API.CHD200G.FrSetFingerEnd1(PortId, NetId);
                }
                else
                {
                    //nRt = FrSetFingerEnd(m_nIndex, btDeviceID);
                }


                if (nRt == 0)
                {
                    PrintMessage("结束设置指纹数据成功!");
                }
                else
                    PrintMessage(String.Format("结束设置指纹数据失败! 错误码:{0}", nRt));
            }
            else
            {
                PrintMessage(String.Format("增加指纹失败!错误码{0}", nRt));
            }
        }




    }
}
