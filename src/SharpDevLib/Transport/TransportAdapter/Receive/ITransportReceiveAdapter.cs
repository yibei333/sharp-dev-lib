using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输接收适配器接口
/// </summary>
/// <remarks>用于自定义TCP/UDP数据的接收逻辑，处理粘包问题等场景</remarks>
public interface ITransportReceiveAdapter
{
    /// <summary>
    /// 从套接字接收数据（TCP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>接收到的字节数组</returns>
    byte[] Receive(Socket socket);

    /// <summary>
    /// 从套接字接收数据并获取远程端点（UDP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程端点</param>
    /// <returns>接收到的字节数组</returns>
    byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint);
}