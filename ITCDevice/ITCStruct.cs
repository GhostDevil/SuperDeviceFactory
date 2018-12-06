using System.Runtime.InteropServices;

namespace ITCDevice
{
    /// <summary>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-09-01</para>
    /// <para>说明：ITC 结构</para>
    /// </summary>
    public static class ITCStruct
    {
        /// <summary>
        /// 播放文件
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct PlayFile   
        {
            /// <summary>
            /// 文件序号，播放的服务器上的文件ID，ID小于等于0则播放本机文件
            /// </summary>
            public int fid;
            /// <summary>
            /// 服务器文件显示名或本机文件全路径名。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string fname; 
            /// <summary>
            /// 音量
            /// </summary>
                         
            public int fvol;
        }
        /// <summary>
        /// 终端状态
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TermAttr   
        {
            /// <summary>
            /// 终端ID
            /// </summary>
            public uint tid;   
            /// <summary>
            ///  终端状态：-1-不连通，0-空闲, >0-使用中
            /// </summary>
            public int status; 
            /// <summary>
            /// 活动会话ID
            /// </summary>
            public int a_sid; 
            /// <summary>
            /// 音量：0~56，0最小，56最大
            /// </summary>
            public int vol;     
            /// <summary>
            /// 终端IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string ipaddr; 
            /// <summary>
            /// 中继服务器IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string fwdaddr; 
            /// <summary>
            /// 中继服务器IP地址
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string name; 
        }
        /// <summary>
        /// 呼叫地址
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CallAddr
        {
            /// <summary>
            /// 终端ID
            /// </summary>
            public uint tid;
            /// <summary>
            /// 终端面板号： 0 -- 终端主机；1 ~ 8 - 终端分控面板
            /// </summary>
            public int box_id;   
        }
        /// <summary>
        /// 播放模式
        /// </summary>
        public enum CycMode
        {
            /// <summary>
            /// 单曲播放（即只播放一次）
            /// </summary>
            PLAY_CYC_DAN = 0x0001,
            /// <summary>
            /// 单曲循环播放（循环播放一个曲目）
            /// </summary>
            PLAY_CYC_DANCIRCLE = 0x0002,
            /// <summary>
            ///  顺序播放（按序播放全部歌曲一次）
            /// </summary>
            PLAY_CYC_DANORDE = 0x0003,
            /// <summary>
            ///  循环播放（循环播放所有歌曲）
            /// </summary>
            PLAY_CYC_ALLCIRCLE = 0x0004    
        }
        /// <summary>
        /// 播放控制
        /// </summary>
        public enum PlayControl
        {
            /// <summary>
            /// 停止广播
            /// </summary>
            PLAY_CTRL_STOP = 1,
            /// <summary>
            /// 跳转至第pos曲播放
            /// </summary>
            PLAY_CTRL_JUMPFILE = 2,
            /// <summary>
            /// 跳至下一曲播放
            /// </summary>
            PLAY_CTRL_NEXT = 3,
            /// <summary>
            /// 跳至上一曲播放
            /// </summary>
            PLAY_CTRL_PREV = 4,
            /// <summary>
            /// 暂停播放
            /// </summary>
            PLAY_CTRL_PAUSE = 5,
            /// <summary>
            /// 恢复播放
            /// </summary>
            PLAY_CTRL_RESTORE = 6,
            /// <summary>
            /// 跳转到当前曲pos秒处位置  
            /// </summary>
            PLAY_CTRL_JUMPTIME = 7
        }
    }
}
