using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Transport;

/// <summary>
/// 传输接收适配器
/// </summary>
public interface ITransportReceiveAdapter
{
    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>字节数组</returns>
    byte[] Receive(Socket socket);

    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程终结点</param>
    /// <returns>字节数组</returns>
    byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint);
}