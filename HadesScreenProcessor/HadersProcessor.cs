using System;
using System.Collections.Generic;

namespace HadesScreenProcessor
{
    /// <summary>
    /// <para>日 期:2015-07-06</para>
    /// <para>作 者:痞子少爷</para>
    /// <para>描 述:淳中哈迪斯大屏处理器控制（仅支持1080分辨率）</para>
    /// </summary>
    public class HadersProcessor
    {

        private SuperFramework.SuperSocket.TCP.TCPSyncClient client = null;
        string ipAddress, netPort = "";
        int mode1 = 9, mode2 = 0, mode3 = 9, mode4 = 0;
        int[][] modes = new int[4][];
        static List<DataAnalysis.OpenWindow> openWindows = new List<DataAnalysis.OpenWindow>();

        #region  委托事件 
        /// <summary>
        /// 接受数据事件委托
        /// </summary>
        public delegate void OnRevMessage(byte[] message, int length);
        /// <summary>
        /// 错误消息事件委托
        /// </summary>
        public delegate void OnExceptionMsg(string msg);
        /// <summary>
        /// 连接状态事件委托
        /// </summary>
        public delegate void OnStateInfo(string msg, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state);
        /// <summary>
        /// 接受数据事件
        /// </summary>
        public event OnRevMessage RevMessage;
        /// <summary>
        /// 错误消息事件
        /// </summary>
        public event OnExceptionMsg ErrorMsg;
        /// <summary>
        /// 连接状态事件
        /// </summary>
        public event OnStateInfo StateInfo;
        #endregion

        #region 构造函数 
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="ip">哈迪斯大屏处理器ip地址</param>
        /// <param name="caseMode">截屏起始点 4*4</param>
        /// <param name="port">通讯端口，默认1024</param>
        public HadersProcessor(string ip, int[][] caseMode,string port = "1024")
        {
            ipAddress = ip;
            netPort = port;
            modes = caseMode;
            client = new SuperFramework.SuperSocket.TCP.TCPSyncClient(ipAddress.Trim(), int.Parse(netPort.Trim()), 500) { IsClosed = false };
            client.OnStateInfo += Client_OnStateInfo; ;
            client.OnReceviceByte += Client_OnReceviceByte; ;
            client.OnExceptionMsg += Client_OnExceptionMsg; ;
        }
        #endregion

        #region  连接哈迪斯大屏处理器 
        /// <summary>
        /// 连接大屏处理器
        /// </summary>
        /// <returns>成功返回true，否则失败</returns>
        public bool Connection()
        {
            return client.StartConnection();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns>成功返回true，否则失败</returns>
        public bool DisConection()
        {
            return client.StopConnection();
        }
        #endregion

        #region  事件处理 
        private void Client_OnExceptionMsg(string msg)
        {
            ErrorMsg?.Invoke(msg);
        }

        private void Client_OnReceviceByte(System.Net.Sockets.Socket temp, byte[] data, int length)
        {
            RevMessage?.Invoke(data, length);
        }

        private void Client_OnStateInfo(string msg, SuperFramework.SuperSocket.TCP.TCPSyncSocketEnum.SocketState state)
        {
            StateInfo?.Invoke(msg, state);
        }
        #endregion

        #region  发送命令 
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令字符串</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SendCmd(string cmd)
        {
            try
            {
                //string strMsg = txtSendMsg.Text.Trim();
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(cmd);
                byte[] arrSendMsg = new byte[arrMsg.Length + 1];
                arrSendMsg[0] = 0; // 用来表示发送的是消息数据
                Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
                // sockClient.Send(arrSendMsg); // 发送消息；
                client.SendData(arrSendMsg);
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion

        #region  关闭窗口 

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="windowNo">窗口号，-1表示全部关闭</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool CloseWindow(int windowNo = -1)
        {
            if (windowNo != -1)
            {
                if (openWindows.Count > 0)
                {
                    for (int i = 0; i < openWindows.Count; i++)
                    {
                        if (openWindows[i].W_ID == (windowNo - 1).ToString())
                        {
                            openWindows.Remove(openWindows[i]);
                            break;
                        }
                    }
                }
                return SendCmd(string.Format("<shut,{0}>", windowNo - 1));
            }
            else
            {
                openWindows.Clear();
                return SendCmd("<rset,0>");
            }
        }
        #endregion

        #region  打开窗口 
        static int y = 0;
        int nowWindowId = -1;
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="screenNo">屏幕号</param>
        /// <param name="screenWidth">窗口宽度</param>
        /// <param name="screenHeight">窗口高度</param>
        /// <param name="channelNo">通道号</param>
        /// <param name="sourcesNo">信号源</param>
        /// <param name="count">总窗口数量</param>
        /// <param name="rowCount">布局行数</param>
        public void OpenWindow(int screenNo, int screenWidth, int screenHeight, int channelNo, int sourcesNo,int count=18 ,int rowCount=3)
        {
            try
            {
                int userNo = screenNo;
                if (screenNo > count)
                {
                    if (screenNo % count == 0)
                        screenNo = count;
                    else
                        screenNo = screenNo % count;
                }
                int splitNum = count / rowCount;
                //int m = 0, n = 0;
                int x = screenNo % splitNum;
                //m = 1; n = 0;
                int index = userNo - 1;//screenNo - 1;
                nowWindowId = index;
                y = screenNo / splitNum;
                if (screenNo % splitNum == 1)
                    x = 1;
                if (screenNo % splitNum == 0)
                {
                    x = splitNum;
                    --y;
                }

                int width = screenWidth, height = screenHeight;
                int oldWidth = 1920, oldHeight = 1080;
                int zx = 0, zy = 0;
                //if (index >= count)
                //{

                //    //zx = oldWidth / 2;
                //    //zy = oldHeight / 2;
                //    //m = 1; n = 0;
                //    x = (screenNo - count) % 5;
                //    if ((screenNo - count) % 5 == 1)
                //        x = 1;
                //    if ((screenNo - count) % 5 == 0)
                //    {
                //        x = 5;
                //        --y;
                //    }
                //}
                //if (index >= count && index <= 22)
                //{
                //    y = 0;
                //}
                //if (index >= 23 && index <= 27)
                //    y = 1;

                switch (channelNo)
                {
                    case 0:
                        mode1 = 0;
                        mode2 = 0;
                        mode3 = 0;
                        mode4 = 0;
                        break;
                    case 1:
                        if (modes.Length > 0)
                        {
                            mode1 = modes[0][0];
                            mode2 = modes[0][1];
                            mode3 = modes[0][2];
                            mode4 = modes[0][3];
                        }
                        break;
                    case 2:
                        if (modes.Length > 0)
                        {
                            mode1 = modes[1][0];
                            mode2 = modes[1][1];
                            mode3 = modes[1][2];
                            mode4 = modes[1][3];
                        }
                        break;
                    case 3:
                        if (modes.Length > 0)
                        {
                            mode1 = modes[2][0];
                            mode2 = modes[2][1];
                            mode3 = modes[2][2];
                            mode4 = modes[2][3];
                        }
                        break;
                    case 4:
                        if (modes.Length > 0)
                        {
                            mode1 = modes[3][0];
                            mode2 = modes[3][1];
                            mode3 = modes[3][2];
                            mode4 = modes[3][3];
                        }
                        break;
                }
                //DataAnalysis.OpenWindow win = new DataAnalysis.OpenWindow() { Screen_ID = "0", SourceCh = sourcesNo.ToString(), W_ID = (index).ToString(), x0 = (zx + oldWidth * (x - 1)).ToString(), y0 = ((zy) + oldHeight * y).ToString(), x1 = (zx + (width * x - n)).ToString(), y1 = (zy + (height * (y + 1) - m)).ToString(), src_hstart = mode1.ToString(), src_hsize = mode2.ToString(), src_vstart = mode3.ToString(), src_vsize = mode4.ToString(), channelNo=channelNo.ToString() };
                DataAnalysis.OpenWindow win = new DataAnalysis.OpenWindow() { Screen_ID = "0", SourceCh = sourcesNo.ToString(), W_ID = (index).ToString(), x0 = (zx + oldWidth * (x - 1)).ToString(), y0 = (zy + oldHeight * y).ToString(), x1 = (zx + (width + (oldWidth * (x - 1)))).ToString(), y1 = (zy + (height + (oldHeight * y)) - 1).ToString(), src_hstart = mode1.ToString(), src_hsize = mode2.ToString(), src_vstart = mode3.ToString(), src_vsize = mode4.ToString(), channelNo = channelNo.ToString() };
                //string cmd = "<open, 0, " + (index).ToString() + ", " + sourcesNo + "," + mode1 + "," + mode2 + "," + mode3 + "," + mode4 + "," + (zx + width * (x - 1)) + "," + ((zy) + height * y) + "," + (zx + (width * x - n)) + "," + (zy + (height * (y + 1) - m)) + " >";
                //string cmd = "<open, 0, " + (index).ToString() + ", " + sourcesNo + "," + mode1 + "," + mode2 + "," + mode3 + "," + mode4 + "," + (zx + width * (x - 1)) + "," + ((zy) + height * y) + "," + (zx + (width * x - n)) + "," + (zy + (height * (y + 1) - m)) + " >";
                string cmd = string.Format("<open,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}>", win.Screen_ID, win.W_ID, win.SourceCh, win.src_hstart, win.src_hsize, win.src_vstart, win.src_vsize, win.x0, win.y0, win.x1, win.y1);
                SendCmd(cmd);
                if (openWindows.Count == 0)
                    openWindows.Add(win);
                else
                {
                    if (!openWindows.Exists(o => o.W_ID == win.W_ID))
                        openWindows.Add(win);
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region  保存场景 

        /// <summary>
        /// 保存场景
        /// </summary>
        /// <param name="planNo">表示要保存的场景号从 1~32，最多可存 32 个场景。 </param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SavePlan(int planNo)
        {
            return SendCmd(string.Format("<save, {0}, 0 > ", planNo - 1));
        }
        #endregion

        #region  加载场景 

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="planNo">表示要加载的场景号从 1~32，最多可存 32 个场景。 </param>
        /// <returns>成功返回true，否则失败</returns>
        public bool LoadPlan(int planNo)
        {
            return SendCmd(string.Format("<call, {0}, 0 > ", planNo - 1));
        }
        #endregion

        #region  获取窗口信息 
        /// <summary>
        /// 获取窗口信息
        /// </summary>
        /// <param name="windowNo">窗口号</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool GetWindowInfo(int windowNo)
        {
            return SendCmd(string.Format("<widf,{0}>", windowNo - 1));
        }
        #endregion

        #region  窗口显示方式枚举 
        /// <summary>
        /// 窗口显示方式
        /// </summary>
        public enum WindowStyle
        {
            还原=0,
            全屏,
            左半屏,
            右半屏,
            中间屏,
            上左四屏,
            上中四屏,
            上右四屏,
            上左半四屏,
            上右半四屏,
            下左四屏,
            下中四屏,
            下右四屏,
            下左半四屏,
            下右半四屏
        }
        #endregion

        #region  获得已打开的窗口信息 
        /// <summary>
        /// 获得已打开的窗口信息
        /// </summary>
        /// <param name="windowNo">已打开窗口编号</param>
        /// <returns>返回一打开窗口信息对象</returns>

        public DataAnalysis.OpenWindow GetChanageWindowInfo(string windowNo)
        {
            if (openWindows.Count > 0)
            {
                foreach (DataAnalysis.OpenWindow item in openWindows)
                {
                    if (item.W_ID == (int.Parse(windowNo) - 1).ToString())
                        return item;
                }
            }
            return new DataAnalysis.OpenWindow();
        }
        #endregion

        #region  改变窗口显示方式 
        /// <summary>
        ///  改变窗口显示方式
        /// </summary>
        /// <param name="sourcesNo">信号源</param>
        /// <param name="chanelNo">通道号</param>
        /// <param name="style">显示方式</param>
        /// <param name="windowNo">窗口号</param>
        /// <param name="count">总窗口数量</param>
        /// <param name="rowCount">布局行数</param>
        /// <param name="width">窗口分辨率宽度</param>
        /// <param name="height">窗口分辨率高度</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool ChangeWindowStyle(int sourcesNo, int chanelNo, WindowStyle style, int windowNo, int count = 18, int rowCount = 3,int width=1920,int height=1080)
        {
            string cmd = "";
            string sources = sourcesNo.ToString();
            switch (chanelNo)
            {
                case 0:
                    mode1 = 0;
                    mode2 = 0;
                    mode3 = 0;
                    mode4 = 0;
                    break;
                case 1:
                    if (modes.Length > 0)
                    {
                        mode1 = modes[0][0];
                        mode2 = modes[0][1];
                        mode3 = modes[0][2];
                        mode4 = modes[0][3];
                    }
                    break;
                case 2:
                    if (modes.Length > 0)
                    {
                        mode1 = modes[1][0];
                        mode2 = modes[1][1];
                        mode3 = modes[1][2];
                        mode4 = modes[1][3];
                    }
                    break;
                case 3:
                    if (modes.Length > 0)
                    {
                        mode1 = modes[2][0];
                        mode2 = modes[2][1];
                        mode3 = modes[2][2];
                        mode4 = modes[2][3];
                    }
                    break;
                case 4:
                    if (modes.Length > 0)
                    {
                        mode1 = modes[3][0];
                        mode2 = modes[3][1];
                        mode3 = modes[3][2];
                        mode4 = modes[3][3];
                    }
                    break;
            }
            //switch (chanelNo)
            //{
            //    #region  汉中富平庄里
            //    //case 1:
            //    //    mode1 = 9;
            //    //    mode3 = 9;
            //    //    mode2 = 700;
            //    //    mode4 = 440;
            //    //    break;
            //    //case 2:
            //    //    mode1 = 730;
            //    //    mode2 = 700;
            //    //    mode4 = 440;
            //    //    mode3 = 9;
            //    //    break;
            //    //case 3:
            //    //    mode2 = 700;
            //    //    mode3 = 460;
            //    //    mode4 = 440;
            //    //    mode1 = 9;
            //    //    break;
            //    //case 4:
            //    //    mode1 = 730;
            //    //    mode2 = 700;
            //    //    mode3 = 460;
            //    //    mode4 = 440;
            //    //    break;
            //    //case 0:
            //    //    mode1 = 9;
            //    //    mode2 = 0;
            //    //    mode3 = 9;
            //    //    mode4 = 0;
            //    //    break;
            //    #endregion
            //    case 0:
            //        mode1 = 0;
            //        mode2 = 0;
            //        mode3 = 0;
            //        mode4 = 0;
            //        break;
            //    case 1:
            //        mode1 = 0;
            //        mode2 = 694;
            //        mode3 = 0;
            //        mode4 = 424;
            //        break;
            //    case 2:
            //        mode1 = 694;
            //        mode2 = 694;
            //        mode3 = 0;
            //        mode4 = 424;
            //        break;
            //    case 3:
            //        mode1 = 0;
            //        mode2 = 694;
            //        mode3 = 424;
            //        mode4 = 424;

            //        break;
            //    case 4:
            //        mode1 = 694;
            //        mode2 = 694;
            //        mode3 = 424;
            //        mode4 = 424;
            //        break;
            //}
            int startX, startY, endX, endY = 0;
            switch (style)
            {
                //1920*n-1  1080*n-1
                case WindowStyle.全屏:
                    startX = startY = 0;
                    endX = width * (count / rowCount)-1;//11519;
                    endY = height * rowCount-1;  //3239;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.左半屏:
                    startX = startY = 0;
                    endX = width * (count / rowCount / 2) - 1; //5759;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.右半屏:
                    startX = 1920 * (count / rowCount / 2) ;//5760;
                    startY = 0;
                    endX = width * (count / rowCount) - 1;//11519;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.中间屏:
                    startX = width * 1 /*(count / rowCount)-1*/;//5760;
                    startY = 0;
                    endX = 1920 * (count / rowCount-1)-1;//11519;
                    endY = height * rowCount;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    SetTopWindow(windowNo);
                    //OpenWindow(windowNo + count, 1920, 1080,chanelNo, sourcesNo, count,rowCount);
                    break;
                case WindowStyle.还原:
                   
                    cmd = Before(sourcesNo, windowNo, chanelNo,mode1,mode2,mode3,mode4,count,rowCount);
                    SetBottomWindow(windowNo);
                    return SendCmd(cmd);
                    //SetBottomWindow(windowNo);
                   // break;
                case WindowStyle.上左四屏:
                    startX = startY = 0;
                    endX = width * 2 - 1;//3839;
                    endY = height * 2 - 1;//2159;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.上中四屏:
                    startX = width * (count / rowCount / 2 - 1) ; //3840;
                    startY = 0;
                    endX = width * (count / rowCount /2+1) - 1; //7679;
                    endY = height * 2 - 1; //2159;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.上右四屏:
                    startX = width * (count / rowCount - 2); //7680;
                    startY = 0;
                    endX = width * (count / rowCount) - 1;//11519;
                    endY = height * 2 - 1;//2159;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.上左半四屏:
                    startX = width;
                    startY = 0;
                    endX = width * (count / rowCount/ 2) - 1;//5759;
                    endY = height * 2 - 1;//2159;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.上右半四屏:
                    startX = width * (count / rowCount /2) ; //5760;
                    startY = 0;
                    endX = width * (count / rowCount -1) - 1;//9599;
                    endY = height * 2 - 1;//2159;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.下左四屏:
                    startX = 0;
                    startY = height;
                    endX = width * 2 - 1;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.下中四屏:
                    startX = width * (count/rowCount / 2 - 1) ;
                    startY = height * (rowCount-2);
                    endX = width * (count / rowCount / 2 + 1) - 1; //7679;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.下右四屏:
                    startX = width * (count / rowCount - 2);//7680;
                    startY = height;
                    endX = width * (count / rowCount) - 1;//11519;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.下左半四屏:
                    startX = width;
                    startY = height;
                    endX = width * (count / rowCount / 2) - 1;//5759;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    //SetTopWindow(windowNo);
                    break;
                case WindowStyle.下右半四屏:
                    startX = width * (count / rowCount / 2);//5760;
                    startY = 1080;
                    endX = width * (count / rowCount - 1) - 1;//9599;
                    endY = height * rowCount - 1;
                    cmd = string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9}>", windowNo - 1, sources, mode1, mode2, mode3, mode4, startX, startY, endX, endY);
                    
                    break;
                default:
                    return false;
            }
         
            SetTopWindow(windowNo);
            bool b= SendCmd(cmd);
           // OpenWindow(windowNo + count, 1920, 1080, chanelNo, sourcesNo, count, rowCount);
            return b;
        }

        private string Before(int sourcesNo, int windowNo, int channelNo, int bMode1, int bMode2, int bMode3, int bMode4, int count = 18, int rowCount = 3, int width = 1920, int heigh = 1080)
        {
            CloseWindow(windowNo + count);
            if (openWindows.Count > 0)
            {
                DataAnalysis.OpenWindow item = GetChanageWindowInfo(windowNo.ToString());
                if (item.SourceCh != "")
                    return string.Format("<move,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}>", item.W_ID, sourcesNo, item.src_hstart, item.src_hsize, item.src_vstart, item.src_vsize, item.x0, item.y0, item.x1, item.y1);
            }
            int userNo = sourcesNo;
            if (windowNo > count)
            {
                if (windowNo % count == 0)
                    windowNo = 1;
                else
                    windowNo = windowNo % count;
            }
            int splitNum = count / rowCount;
            int m = 1, n = 0;
            int x = windowNo % splitNum;
            int index = userNo - 1;//windowNo - 1;
            string sources = sourcesNo.ToString();
            y = windowNo / splitNum;
            if (windowNo % splitNum == 1)
                x = 1;
            if (windowNo % splitNum == 0)
            {
                x = splitNum;
                --y;
            }

            
            int zx = 0, zy = 0;
            //if (index >= 18)
            //{
            //    //zx = width / 2;
            //    //zy = heigh / 2;
            //    m = 1; n = 0;
            //    x = (windowNo - 18) % 5;
            //    if ((windowNo - 18) % 5 == 1)
            //        x = 1;
            //    if ((windowNo - 18) % 5 == 0)
            //    {
            //        x = 5;
            //        --y;
            //    }
            //}
            //if (index >= 18 && index <= 22 && x <= 5)
            //{
            //    y = 0;
            //}
            //if (index >= 23 && index <= 27 && x <= 5)
            //    y = 1;
            //switch (channelNo)
            //{
            //    case 1:
            //        mode1 = 9;
            //        mode3 = 9;
            //        mode2 = 700;
            //        mode4 = 440;
            //        break;
            //    case 2:
            //        mode1 = 730;
            //        mode2 = 700;
            //        mode4 = 440;
            //        mode3 = 9;
            //        break;
            //    case 3:
            //        mode2 = 700;
            //        mode3 = 460;
            //        mode4 = 440;
            //        mode1 = 9;
            //        break;
            //    case 4:
            //        mode1 = 730;
            //        mode2 = 700;
            //        mode3 = 460;
            //        mode4 = 440;
            //        break;
            //    case 0:
            //        mode1 = 9;
            //        mode2 = 0;
            //        mode3 = 9;
            //        mode4 = 0;
            //        break;
            //}

            return string.Format("<move,{0}, {1},{2},{3},{4},{5},{6},{7},{8},{9} >", index, sources, bMode1, bMode2, bMode3, bMode4, zx + width * (x - 1), (zy) + heigh * y, zx + (width * x - n), zy + (heigh * (y + 1) - m));
        }
        #endregion

        #region  改变输入源类型（对VGA/YPbPr输入卡起作用） 
        /// <summary>
        ///  改变输入源类型（对VGA/YPbPr输入卡起作用）
        /// </summary>
        /// <param name="sourcesType">输入源类型，0-VGA，1-YPbPr</param>
        /// <param name="chanelNo">通道号</param>
        /// <returns>成功凡湖true，否则失败</returns>
        public bool ChangeSourceType(int sourcesType, int chanelNo)
        {
            return SendCmd(string.Format("<imod, {0}, {1}>", chanelNo, sourcesType));
        }
        #endregion

        ///// <summary>
        ///// 关闭屏幕
        ///// </summary>
        ///// <param name="groupNo">屏幕号</param>
        ///// <returns>成功返回true，否则失败</returns>
        //public bool CloseScreen(int groupNo)
        //{
        //    return SendCmd("<sena," + groupNo.ToString() + ",0>");
        //}
        //#endregion

        //#region  打开屏幕 
        ///// <summary>
        ///// 打开屏幕
        ///// </summary>
        ///// <param name="groupNo">屏幕号</param>
        ///// <returns>成功返回true，否则失败</returns>
        //public bool OpenScreen(int groupNo)
        //{
        //    return SendCmd("<sena," + groupNo.ToString() + ",1 >");
        //}

        #region  拼接屏使能 
        /// <summary>
        /// 拼接屏使能
        /// </summary>
        /// <param name="groupNo">拼接组编号 1-4 </param>
        /// <param name="enable">使能 1使能（打开） 0不使能（关闭）</param>
        /// <returns></returns>
        public bool ScreenEnable(int groupNo, int enable)
        {
            return SendCmd(string.Format("<sena,{0},{1} >", groupNo - 1, enable));
        }
        #endregion

        #region  更改信号源 
        /// <summary>
        /// 更改信号源
        /// </summary>
        /// <param name="windowNo">窗口号</param>
        /// <param name="SingnalNo">信号源</param>
        /// <returns>成功返回true，否则失败</returns>

        public bool ChangeSignal(int windowNo, int SingnalNo)
        {
            return SendCmd(string.Format("<icha, {0}, {1}, 0, 0, 0, 0>", windowNo - 1, SingnalNo));
        }
        #endregion

        #region  将窗口置顶 
        /// <summary>
        /// 将窗口置顶
        /// </summary>
        /// <param name="windowNo">窗口号</param>
        /// <returns>成功返回true，否则失败</returns>

        public bool SetTopWindow(int windowNo)
        {
            return SendCmd(string.Format("<Torb,{0},1>", windowNo - 1));
        }
        #endregion

        #region  将窗口置底 
        /// <summary>
        /// 将窗口置底
        /// </summary>
        /// <param name="windowNo">窗口号</param>
        /// <returns>成功返回true，否则失败</returns>

        public bool SetBottomWindow(int windowNo)
        {
            return SendCmd(string.Format("<Torb,{0},0>", windowNo - 1));
        }
        #endregion

        #region  查询当前输入状态 
        /// <summary>
        /// 查询当前输入状态
        /// </summary>
        public bool SelectInputState()
        {
            return SendCmd("<vinf>");
        }
        #endregion

        #region  查询已开窗信息 
        /// <summary>
        /// 查询已开窗信息
        /// </summary>
        /// <param name="groupId">屏幕分组id</param>
        /// <returns>成功返回true，否则失败</returns>
        public bool SelectOpenWindowInfos(int groupId)
        {
            return SendCmd(string.Format("<winf,{0}>", (groupId - 1)));
        }
        #endregion
    }
}
