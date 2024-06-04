using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// 传输默认接收适配器(每次按照最大64KB字节获取数据,有粘包问题)
/// </summary>
public class TransportDefaultReceiveAdapter : ITransportReceiveAdapter
{
    const int size = 64 * 1024;

    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>字节数组</returns>
    public byte[] Receive(Socket socket)
    {
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        return buffer.Take(length).ToArray();
    }

    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程终结点</param>
    /// <returns>字节数组</returns>
    public byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint)
    {
        var buffer = new byte[size];
        var length = socket.ReceiveFrom(buffer, ref remoteEndPoint);
        return buffer.Take(length).ToArray();
    }
}