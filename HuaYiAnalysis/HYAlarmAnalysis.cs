using SuperDeviceFactory.AlarmStarHelper;
using SuperDeviceFactory.CountMsg;
using SuperDeviceFactory.LayoutHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SuperDeviceFactory.HuaYiAnalysis
{
    /// <summary>
    /// 华亿智能分析数据解析
    /// </summary>
    public class HYAlarmAnalysis
    {
        #region  内部变量 

        static byte K2_00_SyncByte0;
        static byte K2_01_SyncByte1;
        static byte K2_02_SyncByte2;
        static byte K2_03_SyncByte3;
        static byte K2_04_MsgLen1;
        static byte K2_05_MsgLen2;
        static byte K2_06_MsgLen3;
        static byte K2_07_MsgLen4;
        static byte K2_08_DeviceId1;
        static byte K2_09_DeviceId2;
        static byte K2_10_DeviceId3;
        static byte K2_11_DeviceId4;
        static byte[] XmlDta;
        static Byte[] MsgLen = new byte[4];
        #endregion

        /// <summary>
        /// 报警事件委托
        /// </summary>
        /// <param name="deviceIp">设备ip地址</param>
        /// <param name="msg">报警事件信息</param>
        public delegate void AlarmEventHandle(string deviceIp,AlarmEventMsg msg);
        /// <summary>
        /// 报警事件
        /// </summary>
        public static event AlarmEventHandle AlarmEvent;
        /// <summary>
        /// 轨迹事件委托
        /// </summary>
        /// <param name="msg">轨迹事件</param>
        public delegate void TrackEventHandle(string deviceIp, XMLLayoutMessage msg);
        /// <summary>
        /// 轨迹事件
        /// </summary>
        public static event TrackEventHandle TrackEvent;
        /// <summary>
        /// 人流事件委托
        /// </summary>
        /// <param name="msg"></param>
        public delegate void PersonEventHandle(string deviceIp, CountingEventMsg msg);
        /// <summary>
        /// 人流事件
        /// </summary>
        public static event PersonEventHandle PersonEvent;

        //public AlarmAnalysis(byte[] bytes)
        //{

        //    this.K2_00_SyncByte0 = bytes[0];
        //    this.K2_01_SyncByte1 = bytes[1];
        //    this.K2_02_SyncByte2 = bytes[2];
        //    this.K2_03_SyncByte3 = bytes[3];

        //    MsgLen[0] = bytes[4];
        //    MsgLen[1] = bytes[5];
        //    MsgLen[2] = bytes[6];
        //    MsgLen[3] = bytes[7];

        //    this.K2_08_DeviceId1 = bytes[8];
        //    this.K2_09_DeviceId2 = bytes[9];
        //    this.K2_10_DeviceId3 = bytes[10];
        //    this.K2_11_DeviceId4 = bytes[11];

        //}



        int GetMsgLength()
        {
            return BitConverter.ToInt32(MsgLen, 0);
        }

        static int GetDeviceId()
        {
            return ((((((K2_11_DeviceId4 * 0x100) + K2_10_DeviceId3) * 0x100) + K2_09_DeviceId2) * 0x100) + K2_08_DeviceId1);
        }
        /// <summary>
        /// 接收到的数据是否正确
        /// </summary>
        /// <returns>正确返回true，否则数据不可用</returns>
        public static bool IsAlarmData()
        {
            if (K2_00_SyncByte0 == 2 && K2_01_SyncByte1 == 111 & K2_02_SyncByte2 == 1 && K2_03_SyncByte3 == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取数据结构
        /// </summary>
        /// <param name="bytes">接收到的原始数据</param>
        /// <returns>返回数据结构对象</returns>
        public static HYAlarmData.MsgStrut GetObjectData(byte[] bytes)
        {
            try
            {
                K2_00_SyncByte0 = bytes[0];
                K2_01_SyncByte1 = bytes[1];
                K2_02_SyncByte2 = bytes[2];
                K2_03_SyncByte3 = bytes[3];
                if (!IsAlarmData())
                    return new HYAlarmData.MsgStrut();
                //数据总长度（整个数据长度）
                MsgLen[0] = bytes[4];
                MsgLen[1] = bytes[5];
                MsgLen[2] = bytes[6];
                MsgLen[3] = bytes[7];

                //设备ID(客户编号)
                K2_08_DeviceId1 = bytes[8];
                K2_09_DeviceId2 = bytes[9];
                K2_10_DeviceId3 = bytes[10];
                K2_11_DeviceId4 = bytes[11];

                XmlDta = new byte[bytes.Length];
                bytes.CopyTo(XmlDta,0);
                string xml = Encoding.ASCII.GetString(XmlDta,12,bytes.Length-12).Trim('\0').Trim('\r').Trim('\n');
                XmlDocument document = new XmlDocument();

                document.LoadXml(xml);
                string localName = document.DocumentElement.LocalName;
                return new HYAlarmData.MsgStrut() { DataId = string.Format("{0}{1}{2}{3}", bytes[0], bytes[1], bytes[2], bytes[3]), Length = BitConverter.ToInt32(MsgLen, 0), DeviceId = GetDeviceId(), XmlData = xml , AlarmType=GetXmlType(localName) };
            }
            catch (Exception ex) { return new HYAlarmData.MsgStrut(); }
        }
        ///// <summary>
        ///// 获取数据结构
        ///// </summary>
        ///// <param name="xmlStr">接收到的xml数据</param>
        ///// <returns>返回数据结构对象</returns>
        //public static AlarmData.MsgStrut GetObjectData(string xmlStr)
        //{
        //    XmlDocument document = new XmlDocument();
        //    document.LoadXml(xmlStr);
        //    string localName = document.DocumentElement.LocalName;
        //    return new AlarmData.MsgStrut() { DataId = "0x02,0x6f,0x01,0x01", Length = BitConverter.ToInt32(MsgLen, 0), DeviceId = GetDeviceId(), XmlData = xmlStr, AlarmType = GetXmlType(localName) };
        //}
        static HYAlarmData.XmlType GetXmlType(string code)
        {
            return (HYAlarmData.XmlType)Enum.Parse(typeof(HYAlarmData.XmlType), code);
        }
        /// <summary>
        /// 获得报警类型
        /// </summary>
        /// <param name="alarmCode">报警代码</param>
        /// <returns>返回报警类型</returns>
        public static HYAlarmData.AlarmType GetAlarmType(string alarmCode)
        {
            return (HYAlarmData.AlarmType)Enum.Parse(typeof(HYAlarmData.AlarmType), alarmCode);
        }
        /// <summary>
        /// 获取事件名称
        /// </summary>
        /// <param name="type">报警类型</param>
        /// <returns>返回事件名称</returns>
        public static string GetEventName(HYAlarmData.AlarmType type)
        {
            try
            {
                switch (type)
                {
                    case HYAlarmData.AlarmType.ALARM_NOREFERENCEFOUND:
                        return "场景丢失";
                    case HYAlarmData.AlarmType.ALARM_VIDEOLOSSE:
                        return "视频丢失";
                    case HYAlarmData.AlarmType.ALARM_VMD:
                        return "物体移动侦测";
                    case HYAlarmData.AlarmType.ALARM_VMD_HUMAN:
                        return "物体移动侦测（人）";
                    case HYAlarmData.AlarmType.ALARM_VMD_VEHICLE:
                        return "物体移动侦测（车）";
                    case HYAlarmData.AlarmType.ALARM_VMD_OTHER:
                        return "物体移动侦测（其他）";
                    case HYAlarmData.AlarmType.ALARM_STATICOBJECT:
                        return "物体滞留侦测";
                    case HYAlarmData.AlarmType.ALARM_STATICOBJECT_HUMAN:
                        return "物体滞留侦测（人）";
                    case HYAlarmData.AlarmType.ALARM_STATICOBJECT_VEHICLE:
                        return "物体滞留侦测（车）";
                    case HYAlarmData.AlarmType.ALARM_STATICOBJECT_OTHER:
                        return "物体滞留侦测（其他）";
                    case HYAlarmData.AlarmType.ALARM_PRESENCE:
                        return "突然出现侦测";
                    case HYAlarmData.AlarmType.ALARM_DIRECTIONALMOTION:
                        return "定向移动侦测";
                    case HYAlarmData.AlarmType.ALARM_OBJECTSTARTED:
                        return "物体启动侦测";
                    case HYAlarmData.AlarmType.ALARM_PATHDETECTION:
                        return "移动路径侦测";
                    case HYAlarmData.AlarmType.ALARM_PATHDETECTION_HUMAN:
                        return "移动路径侦测（人）";
                    case HYAlarmData.AlarmType.ALARM_PATHDETECTION_VEHICLE:
                        return "移动路径侦测（车）";
                    case HYAlarmData.AlarmType.ALARM_PATHDETECTION_OTHER:
                        return "移动路径侦测（其他）";
                    case HYAlarmData.AlarmType.ALARM_OBJECTREMOVAL:
                        return "物体移走侦测";
                    case HYAlarmData.AlarmType.ALARM_SPEED:
                        return "速度侦测";
                    case HYAlarmData.AlarmType.ALARM_LOITERING:
                        return "徘徊侦测";
                    case HYAlarmData.AlarmType.ALARM_AREACOVERAGE:
                        return "密度侦测";
                    case HYAlarmData.AlarmType.ALARM_MOTIONACTIVITY:
                        return "活动值侦测";
                    case HYAlarmData.AlarmType.ALARM_FLOWCOUNTING:
                        return "人流统计侦测";
                    case HYAlarmData.AlarmType.ALARM_CARCOUNTING:
                        return "车流统计侦测";
                    case HYAlarmData.AlarmType.ALARM_TRIP:
                        return "绊线侦测";
                    case HYAlarmData.AlarmType.ALARM_TRIP_HUMAN:
                        return "绊线侦测（人）";
                    case HYAlarmData.AlarmType.ALARM_TRIP_VEHICLE:
                        return "绊线侦测（车）";
                    case HYAlarmData.AlarmType.ALARM_TRIP_OTHER:
                        return "绊线侦测（其他）";
                    case HYAlarmData.AlarmType.ALARM_DOUBLETRIP:
                        return "双绊线侦测";
                    case HYAlarmData.AlarmType.ALARM_DOUBLETRIP_HUMAN:
                        return "双绊线侦测（人）";
                    case HYAlarmData.AlarmType.ALARM_DOUBLETRIP_VEHICLE:
                        return "双绊线侦测（车）";
                    case HYAlarmData.AlarmType.ALARM_DOUBLETRIP_OTHER:
                        return "双绊线侦测（其他）";
                    case HYAlarmData.AlarmType.ALARM_DEPENDENCY:
                        return "复合规则";
                    default:
                        return "未知规则";
                }
            }
            catch (Exception) {
                return "未知规则";
            }
            
        }

        #region  解析报警数据 
        /// <summary>
        /// 解析报警数据
        /// </summary>
        /// <param name="deviceIp">设备ip</param>
        /// <param name="msg">结构数据</param>
        public static void AnalysisAlarmData(string deviceIp,HYAlarmData.MsgStrut msg)
        {
            try
            {
                if (msg.XmlData == "" || msg.XmlData == "null")
                    return;
                switch (msg.AlarmType)
                {
                    case HYAlarmData.XmlType.AlarmEventMsg:
                        AlarmEvent?.Invoke(deviceIp,SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<AlarmEventMsg>(msg.XmlData));
                        break;
                    case HYAlarmData.XmlType.XMLLayoutMessage:
                        TrackEvent?.Invoke(deviceIp,SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<XMLLayoutMessage>(msg.XmlData));
                        break;
                    case HYAlarmData.XmlType.CountingEventMsg:
                        PersonEvent?.Invoke(deviceIp,SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<CountingEventMsg>(msg.XmlData));
                        break;

                }
            }
            catch (Exception) { }

            //string str;
            //int offset = 0;

            //GetObjectData(buffer);
            //if (GetSync())
            //{
            //    offset = 0;
            //    if (GetMsgLength() > 0)
            //    {
            //        goto Label_00B7;
            //    }
            //}
            //return ;
            //Label_0095:
            ////offset += Sock.Receive(buffer, offset, header.GetMsgLength() - 12 - offset, SocketFlags.None);

            //if (offset >= GetMsgLength() - 12)
            //    goto Label_00C9;
            //else
            //    goto Label_00B7;
            //Label_00B7:
            //if (offset < GetMsgLength())
            //    goto Label_0095;
            //Label_00C9:
            //str = Encoding.ASCII.GetString(buffer, 0, GetMsgLength() - 12);
            ////return .SuperConfig.Xml.XmlSerialization.XmlDeserialize<AlarmData>(str);
            //XmlDocument document = new XmlDocument();
            //document.LoadXml(str);
            //string localName = document.DocumentElement.LocalName;
            //object[] args = new object[2];
            //args[0] = str;
            //args[1] = localName;

            ////    base.Invoke(new dlgt_RefreshGui(this.RefreshGui), args);
            ////}
        }
        #endregion
        /// <summary>
        /// 获取报警截图
        /// </summary>
        /// <param name="XmlMessage">xml格式数据</param>
        /// <returns>返回报警信息图片</returns>
        public Bitmap GetAlarmImage(string XmlMessage)
        {
            try
            {
                Bitmap bitmap=null;

                if (XmlMessage.IndexOf("AlarmEventMsg") != -1)
                    {
                        AlarmEventMsg alrm = SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<AlarmEventMsg>(XmlMessage);
                       
                        if (!string.IsNullOrEmpty(alrm.AlarmBitmap))
                        {
                            MemoryStream stream = new MemoryStream(Convert.FromBase64String(alrm.AlarmBitmap));
                            bitmap = new Bitmap(stream);
                            Pen pen = new Pen(Color.Red, 1);
                            if (!string.IsNullOrEmpty(alrm.TrackUpLeft))
                            {
                                using (Graphics graphics = Graphics.FromImage(bitmap))
                                {
                                    Point ul = new Point(Convert.ToInt32(alrm.TrackUpLeft.Split(',')[0]), Convert.ToInt32(alrm.TrackUpLeft.Split(',')[1]));
                                    Point rd = new Point(Convert.ToInt32(alrm.TrackDownRight.Split(',')[0]), Convert.ToInt32(alrm.TrackDownRight.Split(',')[1]));
                                    graphics.DrawRectangle(pen, new Rectangle(ul.X, ul.Y, rd.X - ul.X, rd.Y - ul.Y));
                                }
                            }

                        }
                        else
                        {
                            return null;
                        }
                        
                       
                    }
                return bitmap;
                

            }
            catch (Exception)
            {
                return null;
              
            }
        }

       /// <summary>
       /// 获取状态截图
       /// </summary>
       /// <param name="XmlMessage">xml格式数据</param>
       /// <param name="imgWidth">图片宽度</param>
       /// <param name="imgHeight">图片高度</param>
       /// <returns>返回状态图片</returns>
        public Bitmap GetStatusImage(string XmlMessage,int imgWidth, int imgHeight)
        {
            try
            {
                Bitmap bitmap = null;

                if (XmlMessage.IndexOf("XMLLayoutMessage") != -1)
                {
                    XMLLayoutMessage alrm = SuperFramework.SuperConfig.Xml.XmlSerialization.XmlDeserialize<XMLLayoutMessage>(XmlMessage);

                    int feedFilter = 0;
                    if (feedFilter > 0)
                    {
                        if (Convert.ToInt32(alrm.FeedNumber) != feedFilter)
                        {
                            return null;
                        }
                    }
                    // this.listLog.Text = XmlMessage;
                    bitmap = new Bitmap(imgWidth, imgHeight);
                    Pen pen = new Pen(Color.Red, 1);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {

                        int _w = Convert.ToInt32(alrm.FrameSize.Split(',')[0]);
                        int _h = Convert.ToInt32(alrm.FrameSize.Split(',')[1]);
                        for (int i = 0; i < alrm.LayoutElementList.Length; i++)
                        {
                            XMLLayoutMessageLayoutElementListLayoutElement elements = alrm.LayoutElementList[i];
                            pen.Color = Color.FromArgb(Convert.ToInt32(elements.Color.Split(',')[0]), Convert.ToInt32(elements.Color.Split(',')[1]), Convert.ToInt32(elements.Color.Split(',')[2]));

                            if (elements.ThePoints != null)
                            {
                                List<Point> lisp = new List<Point>();
                                string[] points = elements.ThePoints.ElementPoints.Split(';');
                                for (int n = 0; n < points.Length; n++)
                                {
                                    Point poin = new Point((int)(Convert.ToDouble(points[n].Split(',')[0]) * ((double)bitmap.Width / (double)_w)), (int)(Convert.ToDouble(points[n].Split(',')[1]) * ((double)bitmap.Height / (double)_h)));
                                    lisp.Add(poin);
                                }
                                graphics.DrawLines(pen, lisp.ToArray());
                                if (elements.Layer.Contains("Alarmed") && !elements.ThePoints.TrackID.Contains("-1"))
                                {
                                    Font f = new Font("仿宋", 8, FontStyle.Regular);

                                    graphics.DrawString(elements.ThePoints.TrackID, f, new SolidBrush(Color.Black), lisp[0]);
                                }
                            }
                            else if (elements.TheText != null)
                            {
                                List<Point> lisp = new List<Point>();
                                string[] points = elements.TheText.PointToDraw.Split(',');

                                Point poin = new Point((int)(Convert.ToDouble(points[0]) * ((double)bitmap.Width / (double)_w)), (int)(Convert.ToDouble(points[1]) * ((double)bitmap.Height / (double)_h)));

                                graphics.DrawLines(pen, lisp.ToArray());

                                Font f = new Font("仿宋", 8, FontStyle.Regular);

                                graphics.DrawString(elements.TheText.Text, f, new SolidBrush(Color.Black), poin);

                            }

                        }
                    }

                }
                return bitmap;

            }
            catch (Exception)
            {
                return null;

            }
        }
        
    }

}

