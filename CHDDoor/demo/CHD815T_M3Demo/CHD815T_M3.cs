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

namespace CHD815T_M3Demo
{
    public partial class CHD815T_M3 : Form
    {
        private int portId;

        public CHD815T_M3()
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

        public CHD815T_M3(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;

        }


        #region 辅助函数

        private void InitCMP()
        {
            var Comboxs = from Control cmb in groupBox7.Controls
                          where cmb is ComboBox
                          select (ComboBox)cmb;
            foreach (var item in Comboxs)
            {
                item.SelectedIndex = 0;
            }
            m_CommandGuide.SelectedIndex = 0;
            m_CarType.SelectedIndex = 0;
            comboBox22.SelectedIndex = 0;
            comboBox21.SelectedIndex = 0;
            comboBox23.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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



        #region 设置参数
        /// <summary>
        /// 权限确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {

            if (textBox26.Text.Length != 10)
            {
                MessageBox.Show("密码没有输入正确");
                return;
            }


            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_LinkOn(PortId, NetId, textBox26.Text);
            if (nRt == 0)
            {
                PrintMessage("确认权限成功");
            }
            else
            {
                PrintMessage("确认权限失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 取消权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_LinkOff(PortId, NetId);
            if (nRt == 0)
            {
                PrintMessage("访问权限取消成功");
            }
            else
            {
                PrintMessage("访问权限取消失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox26.Text.Length != 10)
            {
                MessageBox.Show("密码没有输入正确,长度应为10位");
                return;
            }
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetDevPwd(PortId, NetId, textBox26.Text);
            if (nRt == 0)
            {
                PrintMessage("修改密码成功");
            }
            else
            {
                PrintMessage("修改密码失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            byte[] szVersion = new byte[15];
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadVersion(PortId, NetId, szVersion);
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
        /// 设置设备日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            CHD.API.SYSTEMTIME time = CHD.Common.ParasTime(DateTime.Now);

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetDateTime(PortId, NetId, ref time);
            if (nRt == 0)
            {
                PrintMessage("设置时间成功");
            }
            else
            {
                PrintMessage("设置时间失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取设备时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {

            CHD.API.SYSTEMTIME st;
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadDateTime(PortId, NetId, out st);
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
        /// 读取用户数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button23_Click(object sender, EventArgs e)
        {
            int lCount = 0;
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadUserCount(PortId, NetId, out lCount);
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
        /// 增加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {

            if (textBox27.Text.Length != 10 || textBox28.Text.Length != 8 || textBox29.Text.Length != 4)
            {
                MessageBox.Show("输入信息不正确, 请重新输入");
                return;
            }
            CHD.API.SYSTEMTIME st = CHD.Common.ParasTime(dateTimePicker9.Value);
            int nItem = comboBox2.SelectedIndex;
            byte btPrivelege = 0x00;
            if (nItem == 0)
            {
                btPrivelege = 0xff;
            }
            else
            {
                btPrivelege = 0x00;
            }

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_AddoneUser(PortId, NetId, textBox27.Text, textBox28.Text, textBox29.Text, ref st, btPrivelege);
            if (nRt == 0)
            {
                PrintMessage("添加一个用户成功");
            }
            else
            {
                PrintMessage("添加一个用户失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 删除该用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button26_Click(object sender, EventArgs e)
        {

            if (textBox27.Text.Length != 10)
            {
                MessageBox.Show("输入信息不正确, 请重新输入");
                return;
            }
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_DeleteOneUser(PortId, NetId, textBox27.Text);
            if (nRt == 0)
            {
                PrintMessage("删除一个用户成功");
            }
            else
            {
                PrintMessage("删除一个用户失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 删除全部用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button27_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_DeleteAllUser(PortId, NetId, "0000000000");
            if (nRt == 0)
            {
                PrintMessage("删除全部用户成功");
            }
            else
            {
                PrintMessage("删除全部用户失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 查询用户卡是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button28_Click(object sender, EventArgs e)
        {

            if (textBox27.Text.Length == 0)
            {
                MessageBox.Show("没有输入用户卡号");
                return;
            }
            byte[] btUserInfo = new byte[32];

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_UserCardExistsOrNot(PortId, NetId, textBox27.Text, btUserInfo);
            if (nRt == 0)
            {
                String strInfo = Encoding.Default.GetString(btUserInfo);
                string strUserCard = strInfo.Substring(0, 10);
                string strUserID = strInfo.Substring(10, 8);
                string strPwd = strInfo.Substring(18, 4);
                string strYear = strInfo.Substring(22, 4);
                string strMonth = strInfo.Substring(26, 2);
                string strDay = strInfo.Substring(28, 2);
                string strPrivilege;
                int iPrivilege = Convert.ToInt32(strInfo.Substring(30, 2),16);
                if (iPrivilege == 0xff)
                {
                    strPrivilege = "月卡用户";
                }
                else
                {
                    strPrivilege = "临时卡用户";
                }
                PrintMessage(String.Format("读取用户登记资料成功: 用户卡号:{0}, 用户ID: {1}, 用户密码: {2}, {3}年{4}月{5}日, {6}", strUserCard, strUserID, strPwd,
                   strYear, strMonth, strDay, strPrivilege));
            }
            else
            {
                PrintMessage("读取用户登记资料失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取用户资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button24_Click(object sender, EventArgs e)
        {
            if (textBox33.Text.Length == 0 || int.Parse(textBox33.Text) > 65535)
            {
                MessageBox.Show("输入的数据不正确,请重新输入");
                return;
            }

            byte[] btUserInfo = new byte[34];

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadUserInfo(PortId, NetId, int.Parse(textBox33.Text), btUserInfo);
            if (nRt == 0)
            {
                string strInfo = Encoding.Default.GetString(btUserInfo);

                string strUserCard = strInfo.Substring(0, 10);
                string strUserID = strInfo.Substring(10, 8);
                string strPwd = strInfo.Substring(18, 4);
                string strYear = strInfo.Substring(22, 4);
                string strMonth = strInfo.Substring(26, 2);
                string strDay = strInfo.Substring(28, 2);
                string strPrivilege;
                int iPrivilege =Convert.ToInt32(strInfo.Substring(30, 2),16);
                if (iPrivilege == 0xff)
                {
                    strPrivilege = "月卡用户";
                }
                else
                {
                    strPrivilege = "临时卡用户";
                }
                PrintMessage(String.Format("读取用户登记资料成功: 用户卡号:{0}, 用户ID: {1}, 用户密码: {2}, {3}年{4}月{5}日, {6}", strUserCard, strUserID, strPwd,
                   strYear, strMonth, strDay, strPrivilege));
            }
            else
            {
                PrintMessage("读取用户登记资料失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 远程抬杠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_RemoteControlLevel(PortId, NetId, comboBox3.SelectedIndex + 1, textBox30.Text);
            if (nRt == 0)
            {
                PrintMessage("远程抬杠成功");
            }
            else
            {
                PrintMessage("远程抬杠失败! 错误码:" + nRt);
            }
        }




        /// <summary>
        /// 远程播放语音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button30_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_RemotePlayMedia(PortId, NetId, comboBox4.SelectedIndex + 1, int.Parse(textBox31.Text));
            if (nRt == 0)
            {
                PrintMessage("远程播放语音成功");
            }
            else
            {
                PrintMessage("远程播放语音失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 远程播放金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button31_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_RemoteMoney(PortId, NetId, comboBox5.SelectedIndex + 1, int.Parse(textBox34.Text), int.Parse(textBox35.Text));
            if (nRt == 0)
            {
                PrintMessage("远程播放金额成功");
            }
            else
            {
                PrintMessage("远程播放金额失败! 错误码: " + nRt);
            }
        }




        /// <summary>
        /// 远程播放有效期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {

            if (textBox32.Text.Length == 0 || int.Parse(textBox32.Text) > 65535)
            {
                MessageBox.Show("剩余天数输入不正确");
                return;
            }
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_RemoteRemainDate(PortId, NetId, comboBox6.SelectedIndex + 1, int.Parse(textBox32.Text));
            if (nRt == 0)
            {
                PrintMessage("远程播放有效期成功");
            }
            else
            {
                PrintMessage("远程播放有效期失败! 错误码: " + nRt);
            }
        }

        #endregion



        #region 设置属性
        /// <summary>
        /// 设置设备属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetDeviceAtt(PortId, NetId,
                comboBox7.SelectedIndex, checkBox5.Checked ? 1 : 0, 0,
                1, comboBox10.SelectedIndex, comboBox11.SelectedIndex, comboBox12.SelectedIndex,
                comboBox13.SelectedIndex, checkBox1.Checked ? 1 : 0, 0,
                1, comboBox16.SelectedIndex, comboBox17.SelectedIndex, comboBox18.SelectedIndex);
            if (nRt == 0)
            {
                PrintMessage("设置设备属性成功");
            }
            else
            {
                PrintMessage("设置设备属性失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        ///读取设备属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button34_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadDeviceAttributes(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                string strInfo = btUserInfo.ToString();

                string strDeviceChannelAttributes = "";
                if (int.Parse(strInfo.Substring(0, 2)) == 0)
                {
                    strDeviceChannelAttributes = "无属性";
                }
                else if (int.Parse(strInfo.Substring(0, 2)) == 1)
                {
                    strDeviceChannelAttributes = "进";
                }
                else if (int.Parse(strInfo.Substring(0, 2)) == 2)
                {
                    strDeviceChannelAttributes = "出";
                }

                string strApplyAttributes = int.Parse(strInfo.Substring(2, 2)) > 0 ? "收费" : "不收费";
                string strGroundSensor = int.Parse(strInfo.Substring(8, 2)) > 0 ? "配置地感" : "不配置地感";
                string strCardMachine = int.Parse(strInfo.Substring(10, 2)) > 0 ? "配置吞吐卡机" : "不配置吞吐卡机";
                string strView = int.Parse(strInfo.Substring(12, 2)) > 0 ? "配置显示屏" : "不配置显示屏";

                string strDeviceChannelAttributes1 = "";
                if (int.Parse(strInfo.Substring(16, 2)) == 0)
                {
                    strDeviceChannelAttributes1 = "无属性";
                }
                else if (int.Parse(strInfo.Substring(16, 2)) == 1)
                {
                    strDeviceChannelAttributes1 = "进";
                }
                else if (int.Parse(strInfo.Substring(16, 2)) == 2)
                {
                    strDeviceChannelAttributes1 = "出";
                }

                string strApplyAttributes1 = int.Parse(strInfo.Substring(18, 2)) > 0 ? "收费" : "不收费";
                string strGroundSensor1 = int.Parse(strInfo.Substring(24, 2)) > 0 ? "配置地感" : "不配置地感";
                string strCardMachine1 = int.Parse(strInfo.Substring(26, 2)) > 0 ? "配置吞吐卡机" : "不配置吞吐卡机";
                string strView1 = int.Parse(strInfo.Substring(28, 2)) > 0 ? "配置显示屏" : "不配置显示屏";

                PrintMessage(String.Format(@"读取设备属性成功: 
					   通道A: 设备通道属性: {0}, 收费属性: {1}, 源区域: 00, 目的区域: 01 地感: {2}, 吞吐卡机: {3}  显示屏: {4} 
					   通道B: 设备通道属性: {5}, 收费属性: {6}, 源区域: 00, 目的区域: 01 地感: {7}, 吞吐卡机: {8}  显示屏: {9} ",
                              strDeviceChannelAttributes, strApplyAttributes, strGroundSensor, strCardMachine, strView,
                              strDeviceChannelAttributes1, strApplyAttributes1, strGroundSensor1, strCardMachine1, strView1));
            }
            else
            {
                PrintMessage("读取设备属性失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 设置设备参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetParameter(PortId, NetId, checkBox2.Checked ? 1 : 0,
        checkBox3.Checked ? 1 : 0, checkBox4.Checked ? 1 : 0);
            if (nRt == 0)
            {
                PrintMessage("设置设备参数成功");
            }
            else
            {
                PrintMessage("设置设备参数失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 读取设备参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button36_Click(object sender, EventArgs e)
        {
            StringBuilder btData = new StringBuilder();

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadParameter(PortId, NetId, btData);
            if (nRt == 0)
            {
                string strInfo = btData.ToString();

                string sTemp = "";
                if (int.Parse(strInfo.Substring(0, 2)) != 0)
                {
                    sTemp += "配置地感 ";
                }
                else
                {
                    sTemp += "不配置地感 ";
                }
                if (int.Parse(strInfo.Substring(2, 2)) != 0)
                {
                    sTemp += " 配置吞吐卡机 ";
                }
                else
                {
                    sTemp += " 不配置吞吐卡机 ";
                }
                if (int.Parse(strInfo.Substring(4, 2)) != 0)
                {
                    sTemp += " 配置显示屏 ";
                }
                else
                {
                    sTemp += " 不配置显示屏 ";
                }
                PrintMessage("读取设备参数成功 " + sTemp);
            }
            else
            {
                PrintMessage("读取设备参数失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 恢复记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button37_Click(object sender, EventArgs e)
        {
            if (textBox36.Text.Length == 0 || int.Parse(textBox36.Text) > 65535)
            {
                MessageBox.Show("记录数输入不正确");
                textBox36.Focus();
                return;
            }


            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_RecoverAllRecord(PortId, NetId, int.Parse(textBox36.Text));
            if (nRt == 0)
            {
                PrintMessage("恢复记录数成功");
            }
            else
            {
                PrintMessage("恢复记录数失败! 错误码: " + nRt);
            }
        }



        /// <summary>
        /// 初始化记录区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button38_Click(object sender, EventArgs e)
        {
            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_DeleteAllRecord(PortId, NetId);
            if (nRt == 0)
            {
                PrintMessage("初始化记录区成功");
            }
            else
            {
                PrintMessage("初始化记录区失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 读取记录参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button39_Click(object sender, EventArgs e)
        {
            byte[] btUserInfo = new byte[18];

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadRecordParameter(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                string strInfo = Encoding.Default.GetString(btUserInfo);

                string strButtom = strInfo.Substring(0, 4);
                string strLOADP = strInfo.Substring(4, 4);
                string strSAVEP = strInfo.Substring(8, 4);
                string strMF = strInfo.Substring(12, 2);
                string strMAXLEN = Convert.ToInt32(strInfo.Substring(14, 4),16).ToString();
                PrintMessage(String.Format("读取记录参数成功: 桶底BOTTOM: {0}, 新记录存放指针: {1},	读取记录位置指针: {2}, 标志MF :{3}, 柜桶最大深度: {4}",
                    strButtom, strLOADP, strSAVEP, strMF, strMAXLEN));
            }
            else
            {
                PrintMessage("读取记录参数失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 读取历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button40_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadHistoryRecord(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                string strInfo = btUserInfo.ToString();

                string strEventCode = strInfo.Substring(0, 10);
                string strEventTime = strInfo.Substring(10, 4);
                strEventTime += " 年 ";
                strEventTime += strInfo.Substring(14, 2);
                strEventTime += " 月 ";
                strEventTime += strInfo.Substring(16, 2);
                strEventTime += " 日 ";
                strEventTime += strInfo.Substring(18, 2);
                strEventTime += " 时 ";
                strEventTime += strInfo.Substring(20, 2);
                strEventTime += " 分 ";
                strEventTime += strInfo.Substring(22, 2);
                strEventTime += " 秒 ";
                string strEventType = strInfo.Substring(24, 2);
                string strChannel = strInfo.Substring(26, 2);
                string strChannelDirections = strInfo.Substring(28, 2);
                string strEventContent = strInfo.Substring(30, 6);
                PrintMessage(String.Format("读取最新事件成功: 事件代码: {0}  事件时间: {1}  事件类型: {2}  通道号: {3}  通道方向: {4}  事件内容: {5}",
                    strEventCode, strEventTime, strEventType, strChannel, strChannelDirections, strEventContent));
            }
            else
            {
                PrintMessage("读取最新事件失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 顺序读取记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button41_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadRecordSerial(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                string strInfo = btUserInfo.ToString();

                string strEventCode = strInfo.Substring(0, 10);
                string strEventTime = strInfo.Substring(10, 4);
                strEventTime += " 年 ";
                strEventTime += strInfo.Substring(14, 2);
                strEventTime += " 月 ";
                strEventTime += strInfo.Substring(16, 2);
                strEventTime += " 日 ";
                strEventTime += strInfo.Substring(18, 2);
                strEventTime += " 时 ";
                strEventTime += strInfo.Substring(20, 2);
                strEventTime += " 分 ";
                strEventTime += strInfo.Substring(22, 2);
                strEventTime += " 秒 ";
                string strEventType = strInfo.Substring(24, 2);
                string strChannel = strInfo.Substring(26, 2);
                string strChannelDirections = strInfo.Substring(28, 2);
                string strEventContent = strInfo.Substring(30, 6);
                PrintMessage(String.Format("读取最新事件成功: 事件代码: {0}  事件时间: {1}  事件类型: {2}  通道号: {3}  通道方向: {4}  事件内容: {5}",
                    strEventCode, strEventTime, strEventType, strChannel, strChannelDirections, strEventContent));
            }
            else
            {
                PrintMessage("读取最新事件失败! 错误码:" + nRt);
            }
        }



        /// <summary>
        /// 读取最新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button42_Click(object sender, EventArgs e)
        {
            StringBuilder btUserInfo = new StringBuilder();

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadNewEvent(PortId, NetId, btUserInfo);
            if (nRt == 0)
            {
                string strInfo = btUserInfo.ToString();

                string strEventCode = strInfo.Substring(0, 10);
                string strEventTime = strInfo.Substring(10, 4);
                strEventTime += " 年 ";
                strEventTime += strInfo.Substring(14, 2);
                strEventTime += " 月 ";
                strEventTime += strInfo.Substring(16, 2);
                strEventTime += " 日 ";
                strEventTime += strInfo.Substring(18, 2);
                strEventTime += " 时 ";
                strEventTime += strInfo.Substring(20, 2);
                strEventTime += " 分 ";
                strEventTime += strInfo.Substring(22, 2);
                strEventTime += " 秒 ";
                string strEventType = strInfo.Substring(24, 2);
                string strChannel = strInfo.Substring(26, 2);
                string strChannelDirections = strInfo.Substring(28, 2);
                string strEventContent = strInfo.Substring(30, 6);
                PrintMessage(String.Format("读取最新事件成功: 事件代码: {0}  事件时间: {1}  事件类型: {2}  通道号: {3}  通道方向: {4}  事件内容: {5}",
                    strEventCode, strEventTime, strEventType, strChannel, strChannelDirections, strEventContent));
            }
            else
            {
                PrintMessage("读取最新事件失败! 错误码:" + nRt);
            }
        }


        #endregion



        #region 设置标准
        /// <summary>
        /// 设置收费标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button43_Click(object sender, EventArgs e)
        {
            try
            {
                int iCarType = m_CarType.SelectedIndex + 1;
                int iPerDate_Max_Ceiling = int.Parse(m_PerDate_Max_Ceiling.Text);
                int iFirstPeriodOfParkingFree = int.Parse(m_FirstPeriodOfParkingFree.Text);
                int iFirstPeriodOfApply = int.Parse(m_FirstPeriodOfApply.Text);
                int iFirstPeriodOfApplyStd = int.Parse(m_FirstPeriodOfApplyStd.Text);
                int iApply_Interval = int.Parse(m_Apply_Interval.Text);
                int iInterval_Apply_Std = int.Parse(m_Interval_Apply_Std.Text);
                CHD.API.SYSTEMTIME stPeakHour_Period1 = CHD.Common.ParasTime(m_PeakHour_Period1.Value);
                CHD.API.SYSTEMTIME stPeakHour_Period2 = CHD.Common.ParasTime(m_PeakHour_Period2.Value);
                int iPeakHour_ParkingFree = int.Parse(m_PeakHour_ParkingFree.Text);
                int iPeakHour_First = int.Parse(m_PeakHour_First.Text);
                int iPeakHour_ApplyStd = int.Parse(m_PeakHour_ApplyStd.Text);
                int iPeakHour_Interval = int.Parse(m_PeakHour_Interval.Text);
                int iPeakHour_Interval_ApplyStd = int.Parse(m_PeakHour_Interval_ApplyStd.Text);
                int iPeakHour_Ceiling = int.Parse(m_PeakHour_Ceiling.Text);
                int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetApplyStd(PortId, NetId, iCarType, iPerDate_Max_Ceiling, iFirstPeriodOfParkingFree,
                    iFirstPeriodOfApply, iFirstPeriodOfApplyStd, iApply_Interval, iInterval_Apply_Std, stPeakHour_Period1, stPeakHour_Period2,
                    iPeakHour_ParkingFree, iPeakHour_First, iPeakHour_ApplyStd, iPeakHour_Interval, iPeakHour_Interval_ApplyStd, iPeakHour_Ceiling);
                if (nRt == 0)
                {
                    PrintMessage("设置收费标准成功");
                }
                else
                {
                    PrintMessage("设置收费标准失败! 错误码: " + nRt);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请完整的输入收费标准数据！");
            }
        }



        /// <summary>
        /// 读取收费标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button44_Click(object sender, EventArgs e)
        {
            StringBuilder btInfo=new StringBuilder();

	int nRt = CHD.API.CHD815T_M3.CHD815T_M3_ReadApplyStd(PortId, NetId, 1, m_CarType.SelectedIndex + 1, btInfo);
	if(nRt == 0){
		PrintMessage("读取收费标准成功");
	}
	else{
		PrintMessage("读取收费标准失败! 错误码: "+ nRt);
	}
        }




        /// <summary>
        /// 设置上传参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button45_Click(object sender, EventArgs e)
        {
            String param=String.Format("000{0}{1}{2}{3}{4}",checkBox10.Checked?1:0,checkBox9.Checked?1:0,checkBox8.Checked?1:0,checkBox7.Checked?1:0,checkBox6.Checked?1:0);
            int iParameter = Convert.ToInt32(param, 2);
	int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetUpdateParameter(PortId, NetId, iParameter);
	if(nRt == 0){
		PrintMessage("设置上传参数成功");
	}
	else{
		PrintMessage("设置上传参数失败! 错误码: "+ nRt);
	}
        }

        #endregion


        #region 显示方式
        /// <summary>
        /// 设置固定显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button46_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetViewFixed(PortId, NetId, comboBox22.SelectedIndex + 1, comboBox21.SelectedIndex + 0x90, textBox51.Text, textBox51.Text.Length);
	if(nRt == 0){
		PrintMessage("设置固定显示成功");
	}
	else{
		PrintMessage("设置固定显示失败! 错误码: "+ nRt);
	}
        }




        /// <summary>
        /// 设置立即显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button47_Click(object sender, EventArgs e)
        {

            int nRt = CHD.API.CHD815T_M3.CHD815T_M3_SetViewFast(PortId, NetId, comboBox23.SelectedIndex + 1, textBox52.Text, textBox52.Text.Length);
	if(nRt == 0){
		PrintMessage("设置立即显示成功");
	}
	else{
		PrintMessage("设置立即显示失败! 错误码: "+ nRt);
	}
        }
        #endregion





    }
}
