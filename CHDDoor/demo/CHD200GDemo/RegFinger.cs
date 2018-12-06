using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHD200GDemo
{
    public partial class RegFinger : Form
    {
        private bool is200G;
        private CHD200G mainWind;
        private uint portId;
        private uint netId;
        public event Action<string> OnRegStateChange;
        public event Action<List<byte>> OnReadFingerDataComplete;
        public String Msg { get; set; }
        public RegFinger(uint portId,uint netId,bool is200G)
        {
            InitializeComponent();
            this.portId = portId;
            this.netId = netId;
            this.is200G = is200G;
            mainWind = ((CHD200G)this.Parent);
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            int nRetValue;
            uint nRegState = 0, nFingerSize = 0, nReadSize = 0;
            nRetValue = CHD.API.CHD200G.FrRegGetState(this.portId, this.netId, out nRegState/*当前则出状态*/, out nFingerSize/*指纹数据总大小*/);
            if (nRetValue == 0)
            {
                if (nRegState == 0)
                {
                    OnRegStateChange("注册操作被取消");
                    this.Close();
                }

                if (nRegState == 2)
                {
                    timer1.Stop();
                    OnRegStateChange(String.Format("识别指纹成功! 指纹数据大小:{0}, 开始读取数据...", nFingerSize));
                    List<byte> fingerData = new List<byte>();
                    for (uint i = 0; i < 19; i++)
                    {
                        byte[] tempFingerData = new byte[512];
                        nRetValue = is200G ? CHD.API.CHD200G.FrGetFingerData1(this.portId, this.netId, i, out nReadSize, tempFingerData) : CHD.API.CHD200G.FrGetFingerData(this.portId, this.netId, i, out nReadSize, tempFingerData);
                        if (nRetValue == 0)
                        {
                            for (int j = 0; j < nReadSize; j++)
                            {
                                fingerData.Add(tempFingerData[j]);
                            }
                        }
                        else
                        {
                        }
                    }
                    if (fingerData.Count > 0)
                    {
                        OnRegStateChange("指纹数据读取完成");
                        OnReadFingerDataComplete(fingerData);

                    }
                    else
                    {
                        OnRegStateChange("指纹数据读取失败");

                    }

                    this.Close();

                }
               
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegFinger_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void RegFinger_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }


//      private  int RegFinger()
//{
//    int nRetValue;
//    byte[] pBuff=new byte[256 * 10]; 
//    byte[] pFingerData = ((CHD200G)this.Parent).M_pFingerData;
//    uint nRegState=0, nFingerSize=0, nReadSize=0, nDataCount=0;
//    uint nIndex =mainWind.PortId;
//    uint nNetID = mainWind.NetId;
//    if(pFingerData == null) return 0;
//    for(int i=0;i<pFingerData.Length;i++)
//    {
//        pFingerData[i]=0;
//    }

//    nRetValue =CHD.API.CHD200G.CHD200H_FrRegGetState(nIndex, nNetID, out nRegState/*当前则出状态*/, out nFingerSize/*指纹数据总大小*/);
//    if (nRetValue == 0)
//    {
//        if (nRegState == 0)
//        {
//            mainWind.PrintMessage("注册操作被取消");
//            return 2;
//        }
//        else if(nRegState == 2)//注册成功 那么读取数据
//        {
//            mainWind.PrintMessage(String.Format("识别指纹成功! 指纹数据大小:{0}, 开始读取数据...", nFingerSize));
	
//            nRetValue = CHD.API.CHD200G.CHD200H_FrGetFingerData1(nIndex, nNetID, 0, out nReadSize, pBuff);
//            if(nRetValue == 0)
//            {
//                nDataCount += nReadSize;//统计累计读取大小
//                mainWind.PrintMessage(String.Format("第 {0} 次读取指纹数据包成功! 累计读取大小:{1}", 1, nDataCount));
//                pBuff.CopyTo(pFingerData,0);
//                if (nFingerSize == nDataCount)
//                {
//                    mainWind.PrintMessage("获取指纹数据完毕!");
//                    mainWind.M_nDataSize = nDataCount;//更新数据长度
//                    mainWind.ShowData(new byte[1],0);
//                    return 1;
//                }
//            }
//        }
//    }
//    return 0;
//}
    }
}
