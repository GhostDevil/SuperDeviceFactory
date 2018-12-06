using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperFramework.SuperSocket;
using System.Net;

namespace SuperDeviceFactory.HuaYiAnalysis
{
    /// <summary>
    /// 华亿智能分析服务
    /// </summary>
    public class ServerManage
    {
         SuperFramework.SuperSocket.TCP.TCPAsyncSocketHelper asynTcp = null;
        //SuperFramework.SuperSocket.TCP.TCPAsynSocketServer server = null;
        /// <summary>
        /// 数据处理事件委托
        /// </summary>
        public delegate void StateEventHandle(string msg);
        /// <summary>
        /// 数据处理事件
        /// </summary>
        public event StateEventHandle StateEvent;
        /// <summary>
        /// 数据处理事件委托
        /// </summary>
        public delegate void MsgEventHandle();
        /// <summary>
        /// 数据处理事件
        /// </summary>
        public event MsgEventHandle MsgEvent;
        /// <summary>
        /// 通讯地址
        /// </summary>
        string address = "";
        /// <summary>
        /// 通讯端口
        /// </summary>
        int point=0;

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="ip">服务ip地址</param>
        /// <param name="port">通讯端口</param>
        public ServerManage(string ip, int port)
        {
            address=ip;
            point = port;
            asynTcp = new SuperFramework.SuperSocket.TCP.TCPAsyncSocketHelper(ip, port,m_pIsAsServer:true);
            //server = new SuperFramework.SuperSocket.TCP.TCPAsynSocketServer(ip, port);
        }

        
        /// <summary>
        /// 开启服务
        /// </summary>
        /// <returns></returns>
        public bool StartServer()
        {
            asynTcp.AsyncDataAcceptedEvent += AsynTcp_AsyncDataAcceptedEvent;
            asynTcp.AsyncSocketAcceptEvent += AsynTcp_AsyncSocketAcceptEvent;
            asynTcp.AsyncSocketClosedEvent += AsynTcp_AsyncSocketClosedEvent;
            MsgEvent?.Invoke();
            StateEvent?.Invoke(string.Format("服务端： {0}:{1} 开始监听！！！", address == "" ? "LocalHost" : address, point.ToString()));
            return asynTcp.AsyncOpen();
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="m_pSocket"></param>
        private void AsynTcp_AsyncSocketClosedEvent(SuperFramework.SuperSocket.TCP.TCPAsyncSocketHelper m_pSocket)
        {
            StateEvent?.Invoke(string.Format("客户端： {0}:{1} 下线！！！", ((IPEndPoint)(m_pSocket.LinkObject.RemoteEndPoint)).Address.ToString(), ((IPEndPoint)(m_pSocket.LinkObject.RemoteEndPoint)).Port.ToString()));
        }
        /// <summary>
        /// 连接接收数据
        /// </summary>
        /// <param name="m_pSocket"></param>
        private void AsynTcp_AsyncSocketAcceptEvent(SuperFramework.SuperSocket.TCP.TCPAsyncSocketHelper m_pSocket)
        {
            StateEvent?.Invoke(string.Format("客户端： {0}:{1} 上线！！！", ((IPEndPoint)(m_pSocket.LinkObject.RemoteEndPoint)).Address.ToString(), ((IPEndPoint)(m_pSocket.LinkObject.RemoteEndPoint)).Port.ToString()));

        }
        /// <summary>
        /// 数据接收完成
        /// </summary>
        /// <param name="m_pSocket"></param>
        /// <param name="m_pDatagram"></param>
        private void AsynTcp_AsyncDataAcceptedEvent(SuperFramework.SuperSocket.TCP.TCPAsyncSocketHelper m_pSocket, byte[] m_pDatagram)
        {
            HYAlarmData.MsgStrut msg= HYAlarmAnalysis.GetObjectData(m_pDatagram);
            if (msg.DataId == "")
                return;
            HYAlarmAnalysis.AnalysisAlarmData(address,msg);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServer()
        {
            asynTcp.AsyncClose();
            StateEvent?.Invoke(string.Format("服务户端： {0}:{1} 停止监听！！！", address == "" ? "LocalHost" : address, point.ToString()));
        }
    }
}
