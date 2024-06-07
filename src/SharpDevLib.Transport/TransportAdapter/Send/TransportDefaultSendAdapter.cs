using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Transport;

/// <summary>
/// 传输默认发送适配器
/// </summary>
public class TransportDefaultSendAdapter : ITransportSendAdapter
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    public void Send(Socket socket, byte[] bytes)
    {
        socket.Send(bytes);
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="remoteAddress">远程地址</param>
    /// <param name="remoteAddressPort">远程端口</param>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    public void SendTo(Socket socket, IPAddress remoteAddress, int remoteAddressPort, byte[] bytes)
    {
        socket.SendTo(bytes, new IPEndPoint(remoteAddress, remoteAddressPort));
    }
}