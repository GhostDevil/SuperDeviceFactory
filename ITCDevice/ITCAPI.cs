using System;
using System.Runtime.InteropServices;
using static ITCDevice.ITCStruct;

namespace ITCDevice
{
    /// <summary>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-09-01</para>
    /// <para>说明：ITC API</para>
    /// </summary>
    public static class ITCAPI
    {
        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <param name="ipAddr">服务器IP地址</param>
        /// <param name="user">登录用户名(缺省可以用admin)</param>
        /// <param name="pass">登录密码(缺省可以用admin)</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_Connect")]
        public static extern bool Connect(string ipAddr, string user, string pass);

        /// <summary>
        /// 断开连接
        /// </summary>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_DisConnect")]
        public static extern void DisConnect();

        /// <summary>
        /// 删除指定会话
        /// </summary>
        /// <param name="sid">需要删除的会话ID</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_RMSession")]
        public static extern bool RMSession(uint sid);

        /// <summary>
        /// 创建文件播放
        /// </summary>
        /// <param name="pFList">广播文件列表，指向播放文件结构的数组指针</param>
        /// <param name="fCount">广播文件的数目</param>
        /// <param name="pTList">需要添加到本会话的广播终端列表</param>
        /// <param name="tCount">需要添加的终端数目</param>
        /// <param name="Grade">广播等级（0~999，数值越大级别越高）（整数）</param>
        /// <param name="CycMode">播放模式</param>
        /// <param name="CycCount">循环播放次数。（即要求循环播放多少次，0：表示无限次）</param>
        /// <param name="CycTime">循环播放时长（只有当CycCount = 0时有效，单位为秒。）</param>
        /// <returns>大于0: 返回广播会话ID -1：会话创建失败</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_FilePlayStart")]
        public static extern int FilePlayStart(ref PlayFile[] pFList, int fCount, uint[] pTList, int tCount,int Grade, int CycMode, int CycCount, int CycTime);



        /// <summary>
        /// 开始对某一声卡的实时采播
        /// </summary>
        /// <param name="uMxId">启动采播的声卡序号</param>
        /// <param name="iItem">采播声卡的录音项序号</param>
        /// <param name="pTList">需要添加到本会话的广播终端列表</param>
        /// <param name="tCount">需要添加的终端数目</param>
        /// <param name="Grade">广播等级（0~999，数值越大级别越高）（整数）</param>
        /// <param name="bakFile">备份文件全路径名。空则不备份</param>
        /// <returns>大于0: 返回广播会话ID -1：会话创建失败</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_RealPlayStart")]
        public static extern int RealPlayStart(uint uMxId, int iItem, uint[] pTList, int tCount, int Grade,string bakFile);

        /// <summary>
        /// 停止对某一声卡的实时采播
        /// </summary>
        /// <param name="uMxId">停止采播的声卡序号</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_RealPlayStop")]
        public static extern bool RealPlayStop(uint uMxId);

        /// <summary>
        /// 创建本地文件播放
        /// </summary>
        /// <param name="fileList">广播文件列表，指向播放文件路径指针，多个文件路径用逗号','分隔</param>
        /// <param name="pTermList">需要添加到本会话的广播终端列表, 多个终端ID用逗号','分隔</param>
        /// <param name="Grade">广播等级（0~999，数值越大级别越高）（整数）</param>
        /// <param name="CycMode">播放模式</param>
        /// <param name="CycCount">循环播放次数。（即要求循环播放多少次，0：表示无限次）</param>
        /// <param name="CycTime">循环播放时长（只有当CycCount = 0时有效，单位为秒。）</param>
        /// <returns>大于0: 返回广播会话ID -1：会话创建失败</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_FilePlayStartLocal")]
        public static extern int ITCAPI_FilePlayStartLocal(string fileList, string pTermList, int Grade, int CycMode, int CycCount, int CycTime);

        /// <summary>
        /// 创建服务器文件播放
        /// </summary>
        /// <param name="pProgramList">广播文件列表，指向媒体库节目ID列表，多个节目ID间用逗号','分隔</param>
        /// <param name="pTermList">需要添加到本会话的广播终端列表， 多个终端间用逗号','分隔</param>
        /// <param name="Grade">广播等级（0~999，数值越大级别越高）（整数）</param>
        /// <param name="CycMode">播放模式</param>
        /// <param name="CycCount">循环播放次数。（即要求循环播放多少次，0：表示无限次）</param>
        /// <param name="CycTime">循环播放时长（只有当CycCount = 0时有效，单位为秒。）</param>
        /// <returns>大于0: 返回广播会话ID -1：会话创建失败</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_FilePlayStartServer")]
        public static extern int ITCAPI_FilePlayStartServer(string pProgramList, string pTermList,int Grade, int CycMode, int CycCount, int CycTime);
        
        /// <summary>
        /// 文件播放控制
        /// </summary>
        /// <param name="sid">广播会话ID</param>
        /// <param name="cmd">控制命令</param>
        /// <param name="pos">2:跳转的歌曲序号  7:跳转的曲目时间位置</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_FilePlayCtrl")]
        public static extern bool FilePlayCtrl(int sid, int cmd, int pos);
        /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <param name="pTList">保存返回终端ID列表的缓冲区，pTList==NULL或者nSize小于等于0只返回终端数目</param>
        /// <param name="nSize">允许返回的终端ID数目</param>
        /// <returns>终端数目。pTList!=NULL 且 nSize>0 时，相应ID填写到pTList中。</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_GetTermList")]
        unsafe public static extern int ITCAPI_GetTermList(IntPtr pTList, int nSize);


        /// <summary>
        /// 获取终端状态属性
        /// </summary>
        /// <param name="tid">需要查询的终端ID</param>
        /// <param name="pTerm">返回终端属性的指针</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_GetTermStatus")]
        unsafe public static extern bool ITCAPI_GetTermStatus(uint tid, IntPtr pTerm);

        /// <summary>
        /// 控制终端发起对讲(目前2.3.12.D06版本只支持终端主机发起对讲，不支持指定分控面板发起对讲)
        /// </summary>
        /// <param name="from">发起对讲终端ID和分控面板号</param>
        /// <param name="target">对讲目标终端ID和分控面板号</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_Start_Talk")]
        public static extern bool ITCAPI_Start_Talk(ref CallAddr from, ref CallAddr target);
        /// <summary>
        /// 控制终端结束对讲
        /// </summary>
        /// <param name="tid">对讲终端ID</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_Stop_Talk")]
        public static extern bool ITCAPI_Stop_Talk(uint tid);
        /// <summary>
        /// 控制终端接听对讲
        /// </summary>
        /// <param name="tid">振铃终端ID</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_Accept_Call")]
        public static extern bool ITCAPI_Accept_Call(uint tid);
        /// <summary>
        /// 停止电脑对远程终端的反向监听
        /// </summary>
        /// <param name="tid">需要监听的终端ID</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_PCListenStart")]
        public static extern bool ITCAPI_PCListenStop(uint tid);
        /// <summary>
        /// 打开电脑对远程终端的反向监听
        /// </summary>
        /// <param name="tid">需要监听的终端ID</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_PCListenStart")]
        public static extern bool ITCAPI_PCListenStart(uint tid);

        /// <summary>
        /// 控制终端发起寻呼( 1. 目前2.3.12.D06版本只支持终端主机发起寻呼，不支持指定分控面板发起寻呼；2. 目前2.3.12.D06版本只支持对单个目标终端发起寻呼，不支持呼叫多个目标终端)
        /// </summary>
        /// <param name="from">发起寻呼终端ID和分控面板号</param>
        /// <param name="target">寻呼目标终端ID和分控面板号</param>
        /// <param name="target_number">寻呼目标个数</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        [DllImport(@"DLL\ITCDLL\ITCAPI_I.DLL", EntryPoint = "ITCAPI_Start_Speech")]
        public static extern bool ITCAPI_Start_Speech(ref CallAddr from, IntPtr target, int target_number);

    }
}
