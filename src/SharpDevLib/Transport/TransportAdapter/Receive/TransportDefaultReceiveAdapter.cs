using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输默认接收适配器
/// </summary>
/// <remarks>每次按照最大64KB字节获取数据，存在粘包问题，适用于简单场景</remarks>
public class TransportDefaultReceiveAdapter : ITransportReceiveAdapter
{
    const int size = 64 * 1024;

    /// <summary>
    /// 接收数据（TCP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>接收到的字节数组</returns>
    public byte[] Receive(Socket socket)
    {
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        return [.. buffer.Take(length)];
    }

    /// <summary>
    /// 接收数据（UDP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程端点</param>
    /// <returns>接收到的字节数组</returns>
    public byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint)
    {
        var buffer = new byte[size];
        var length = socket.ReceiveFrom(buffer, ref remoteEndPoint);
        return [.. buffer.Take(length)];
    }
}