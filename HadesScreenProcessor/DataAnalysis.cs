using System;
using System.Collections.Generic;
using System.Text;

namespace HadesScreenProcessor
{
    /// <summary>
    /// <para>日 期:2015-07-06</para>
    /// <para>作 者:痞子少爷</para>
    /// <para>描 述:淳中哈迪斯大屏处理器数据对象</para>
    /// </summary>
    public class DataAnalysis
    {
        #region  显示器信息 
        /// <summary>
        /// 显示器信息
        /// </summary>
        public struct DisplayInfo
        {
            
            public string total_line;
            
            public string total_pix;

            public string act_vpos;

            public string act_vsize;

            public string act_hpos;

            public string act_hsize;
            public string hsync_wid;
            public string vsync_wid;
            public string dis_freq;
        }
        #endregion

        #region 窗口属性信息 
        /// <summary>
        /// 窗口属性信息
        /// </summary>
        public struct WindowInfo
        {
            /// <summary>
            /// 信号源ID
            /// </summary>
            public string source;
            /// <summary>
            /// 窗口ID
            /// </summary>
            public string screen;
            /// <summary>
            /// 图像水平起始
            /// </summary>
            public string src_hstart;
            /// <summary>
            /// 图像水平宽度
            /// </summary>
            public string src_hsize;
            /// <summary>
            /// 图像垂直起始
            /// </summary>
            public string src_vstart;
            /// <summary>
            /// 图像垂直高度
            /// </summary>
            public string src_vsize;
            /// <summary>
            /// 窗口水平起始像素
            /// </summary>
            public string hstart;
            /// <summary>
            /// 窗口水平终止像素
            /// </summary>
            public string hend;
            /// <summary>
            /// 窗口垂直起始像素
            /// </summary>
            public string vstart;
            /// <summary>
            /// 窗口垂直终止像素
            /// </summary>
            public string vend;
            /// <summary>
            /// 占用屏幕id
            /// </summary>
            public int screenId;
        }
        #endregion

        #region  打开窗口结构 
        /// <summary>
        /// 打开窗口结构
        /// </summary>
        public struct OpenWindow
        {
            /// <summary>
            /// 表示在那组拼接屏上开窗口
            /// </summary>
            public string Screen_ID;
            /// <summary>
            /// 表示窗口的ID号，范围为0-65535。这号唯一的标示了窗口。
            /// </summary>
            public string W_ID;
            /// <summary>
            /// 表示窗口的信号源，范围为1-最大输入规模。
            /// </summary>
            public string SourceCh;
            /// <summary>
            /// 表示输入信号源的水平截取起始位置。它不能大于输入的水平分辨率。
            /// </summary>
            public string src_hstart;
            /// <summary>
            /// 表示输入信号源的水平截取大小。注意：src_hsize加上src_hstart不能大于输入的水平分辨率。为0表示，采用原始信号源的水平大小，同时src_hstart无效。
            /// </summary>
            public string src_hsize;
            /// <summary>
            /// 表示输入信号源的垂直截取起始位置。它不能大于输入的垂直分辨率。
            /// </summary>
            public string src_vstart;
            /// <summary>
            /// 表示输入信号源的垂直截取大小。注意：src_vsize加上src_vstart不能大于输入的垂直分辨率。为0表示，采用原始信号源的垂直大小，同时src_vstart无效。
            /// </summary>
            public string src_vsize;
            /// <summary>
            /// 表示窗口的水平起始像素点的位置。
            /// </summary>
            public string x0;
            /// <summary>
            /// 表示窗口的垂直起始像素点的位置。
            /// </summary>
            public string y0;
            /// <summary>
            /// 表示窗口的水平结束像素点的位置。
            /// </summary>
            public string x1;
            /// <summary>
            /// 表示窗口的垂直结束像素点的位置。
            /// </summary>
            public string y1;
            /// <summary>
            /// 通道号(bnc画面分割使用)
            /// </summary>
            public string channelNo;
        }
        #endregion

        #region  解析获取窗口信息数据 
        /// <summary>
        /// 解析获取窗口信息数据
        /// </summary>
        /// <param name="data">通讯返回的数据</param>
        /// <param name="length">数据长度</param>
        /// <param name="singleRow">行窗口数量</param>
        /// <returns>返回窗口信息对象</returns>
        public static WindowInfo GetWindowInfo(byte[] data, int length,int singleRow=6)
        {
            string strMsg = Encoding.UTF8.GetString(data, 0, length);
           
            string[] ss = strMsg.Replace("\r", "").Substring(strMsg.IndexOf(":") + 1).Replace("is  ", ",").Replace(" ", " ").Split('\n');
            try
            {
                if (!strMsg.Contains("window is:\r\n"))
                    new WindowInfo() { screen = "-1" };
                WindowInfo info = new WindowInfo() { source = ss[1].Split(',')[1], screen = strMsg.Substring(0, strMsg.IndexOf(":")).Split(' ')[1], src_hstart = ss[3].Split(',')[1], src_hsize = ss[4].Split(',')[1], src_vstart = ss[5].Split(',')[1], src_vsize = ss[6].Split(',')[1], hstart = ss[7].Split(',')[1], hend = ss[8].Split(',')[1], vstart = ss[9].Split(',')[1], vend = ss[10].Split(',')[1] };
                int id = int.Parse(info.hstart) / 1920;
                id += ((int.Parse(info.vstart) / 1080) * singleRow);
                info.screenId = id;
                return info;
            }
            catch(Exception) { }
            return new WindowInfo() { screen = "-1" };
        }
        #endregion

        /// <summary>
        /// 获取打开窗口id
        /// </summary>
        /// <param name="data">通讯返回的数据</param>
        /// <param name="length">数据长度</param>
        /// <returns>返回已打开窗口id</returns>
        public static List<int> GetOpenWindowId(byte[] data, int length)
        {
            List<int> ids = new List<int>();
            string strMsg = Encoding.UTF8.GetString(data, 0, length);
            string[] ss = strMsg.Replace("\r", "").Substring(strMsg.IndexOf(":") + 1).Replace("is", "").Replace("   ", " ").Split('\n');
            if (ss[1] != "")
                foreach (string item in ss[1].Split(','))
                    if(item!="")
                        ids.Add(int.Parse(item));
            return ids;
        }
        /// <summary>
        /// 大屏分辨率
        /// </summary>
        private enum Resolution
        {
            /// <summary>
            /// 1024*768
            /// </summary>
            //A=1024768,
            //1280720,
            //12801024,
            //1360768,
            //14001050,
            //1400900,
            //16001200,
            //16801050,
            //19201080,
           // 19201200

            //<sset,0,806,1344,35,768,296,1024,136,6,65,0,1,1> //1024x768
            //<sset,0,1066,1688,41,1024,360,1280,112,3,108,0,0,0> //1280x1024
            //<sset,0,795,1792,24,768,368,1360,112,3,85,32768,0,0> //1360x768
            //<sset,0,1089,1864,36,1050,378,1400,144,4,121,49152,0,0> //1400x1050
            //<sset,0,934,1904,31,900,384,1440,152,6,106,46622,0,0> //1440x900
            //<sset,0,1250,2160,48,1200,496,1600,192,3,162,0,0,0> //1600x1200
            //<sset,0,1089,2240,35,1050,456,1680,176,6,146,0,0,0> //1680x1050
            //<sset,0,1125,2200,41,1080,192,1920,44,5,148,32768,0,0> //1920x1080
            //<sset,0,1235,2080,31,1200,118,1920,32,6,154,0,0,0> //1920x1200
            //<sset,0,750,1650,25,720,260,1280,40,5,74,16384,0,0> //1280x720
        }
    }
}
