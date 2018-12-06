using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static ITCDevice.ITCAPI;
using static ITCDevice.ITCStruct;

namespace ITCDevice
{
    /// <summary>
    /// <para>作者：痞子少爷</para>
    /// <para>日期：2016-09-01</para>
    /// <para>说明：ITC 操作</para>
    /// </summary>
    public static class ITCOperate
    {
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
        public static int FilePlayStartServer(string pProgramList, string pTermList, int Grade, int CycMode, int CycCount, int CycTime)
        {
            return ITCAPI_FilePlayStartServer(pProgramList, pTermList, Grade, CycMode, CycCount, CycTime);
        }
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
        public static int FilePlayStartLocal(string fileList, string pTermList, int Grade, int CycMode, int CycCount, int CycTime)
        {
            return ITCAPI_FilePlayStartLocal(fileList, pTermList, Grade, CycMode, CycCount, CycTime);
        }
        /// <summary>
        /// 创建文件播放
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="CycMode">播放模式</param>
        /// <param name="term">终端id</param>
        /// <returns>大于0: 返回广播会话ID -1：会话创建失败</returns>
        public static int FilePlayStart(string file, int CycMode, params uint[] term)
        {
            PlayFile f = new PlayFile() { fid = 0, fvol = 10, fname = file };
            PlayFile[] FList = new PlayFile[] { f };
            int cnt = term.Length;
            return ITCAPI.FilePlayStart(ref FList, 1, term, cnt, 500, CycMode, 0, 0);
        }
        /// <summary>
        /// 播放控制
        /// </summary>
        /// <param name="id">终端id</param>
        /// <param name="con">控制模式</param>
        /// <param name="pos">跳转模式</param>
        /// <returns></returns>
        public static bool FilePlayControl(int id,PlayControl con,int pos=0)
        {
            return FilePlayCtrl(id, (int)con, pos);
        }
        /// <summary>
        /// 获取系统中所有终端的ID清单
        /// </summary>
        /// <returns>返回终端id列表</returns>
        unsafe public static uint[] GetAllID()
        {
            IntPtr pArray = Marshal.AllocHGlobal(0);
            int count = ITCAPI_GetTermList(pArray, 0);
            if (count > 0)
            {
                pArray = Marshal.ReAllocHGlobal(pArray, new IntPtr(count * 4));
                int ret = ITCAPI_GetTermList(pArray, count);        // 获取终端ID清单
                uint[] t = new uint[ret];

                if (ret > 0)
                {
                    uint* pId = (uint*)pArray.ToPointer();
                    for (int i = 0; i < ret; i++)
                    {
                        t[i] = pId[i];
                    }
                }
                Marshal.FreeHGlobal(pArray);
                return t;
            }
            return null;
        }
        /// <summary>
        /// 获取系统中所有终端详情
        /// </summary>
        /// <param name="ids">输出终端id</param>
        /// <returns>返回终端属性</returns>
        unsafe public static List<TermAttr> GetAllDeviceInfo(ref uint[] ids)
        {
            List<TermAttr> ta = new List<TermAttr>();
            IntPtr pArray = Marshal.AllocHGlobal(0);
            int count = ITCAPI_GetTermList(pArray, 0);
            if (count > 0)
            {
                pArray = Marshal.ReAllocHGlobal(pArray, new IntPtr(count * 4));
                int ret = ITCAPI_GetTermList(pArray, count);        // 获取终端ID清单
                uint[] t = new uint[ret];

                if (ret > 0)
                {
                    uint* pId = (uint*)pArray.ToPointer();
                    for (int i = 0; i < ret; i++)
                    {
                        t[i] = pId[i];
                        uint tid = pId[i];
                        //termIds += tid.ToString();
                        //termIds += ",";

                        //演示获取终端详细信息
                        long structSize = Marshal.SizeOf(typeof(TermAttr));
                        IntPtr pTermStatus = Marshal.AllocHGlobal((int)new IntPtr(structSize));
                        if (ITCAPI_GetTermStatus(tid, pTermStatus))
                        {
                            TermAttr termAttr = (TermAttr)Marshal.PtrToStructure(pTermStatus, typeof(TermAttr));
                            ta.Add(termAttr);
                        }
                        Marshal.FreeHGlobal(pTermStatus);
                    }
                }
                Marshal.FreeHGlobal(pArray);
                return ta;
            }
            return null;
        }
        /// <summary>
        /// 控制终端发起寻呼( 1. 目前2.3.12.D06版本只支持终端主机发起寻呼，不支持指定分控面板发起寻呼；2. 目前2.3.12.D06版本只支持对单个目标终端发起寻呼，不支持呼叫多个目标终端)
        /// </summary>
        /// <param name="from">发起寻呼终端ID和分控面板号</param>
        /// <param name="to">寻呼目标终端ID和分控面板号</param>
        /// <param name="target_number">寻呼目标个数</param>
        /// <returns>成功：返回TRUE  失败：返回FALSE</returns>
        unsafe public static bool StartSpeech(uint from, ref uint[] to, int target_number)
        {
            CallAddr pFrom = new CallAddr() { tid = from, box_id = 0 };
            int structSize = Marshal.SizeOf(typeof(CallAddr));
            IntPtr pArray = Marshal.AllocHGlobal((int)new IntPtr(structSize) * target_number);
            CallAddr* pTo = (CallAddr*)pArray.ToPointer();

            for (int i = 0; i < target_number; i++)
            {
                pTo[i].tid = to[i];
                pTo[i].box_id = 0;
            }

            bool bret = ITCAPI_Start_Speech(ref pFrom, pArray, target_number);
            Marshal.FreeHGlobal(pArray);
            return bret;
        }
    }
}
