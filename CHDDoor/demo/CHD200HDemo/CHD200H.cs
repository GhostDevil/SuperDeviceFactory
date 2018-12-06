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

namespace CHD200HDemo
{
    public partial class CHD200H : Form
    {
        private int portId;
       
        public CHD200H()
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

        public CHD200H(int ptID)
        {
            InitializeComponent();
            this.portId = ptID;
            
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

        private void btnRead_Click(object sender, EventArgs e)
        {
            double pnCurMean, pnCurTest;
            int result = CHD.API.CHDLH.ModBusThReadSensor(PortId, NetId, out pnCurMean, out pnCurTest);
            if (result == 0x00)
            {
                PrintMessage(String.Format("读烟雾浓度值成功, 当前平均值: {0}  当前测试: {1}", pnCurMean, pnCurTest));
            }
            else
            {
                PrintMessage(String.Format("读烟雾浓度值失败! 错误码:{0}", result));
            }
        }
    }
}
