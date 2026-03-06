using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输默认发送适配器
/// </summary>
/// <remarks>直接发送数据，存在粘包问题，适用于简单场景</remarks>
public class TransportDefaultSendAdapter : ITransportSendAdapter
{
    /// <summary>
    /// 发送数据（TCP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    public void Send(Socket socket, byte[] bytes)
    {
        socket.Send(bytes);
    }

    /// <summary>
    /// 发送数据（UDP使用）
    /// </summary>
    /// <param name="remoteAddress">远程IP地址</param>
    /// <param name="remoteAddressPort">远程端口</param>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    public void SendTo(Socket socket, IPAddress remoteAddress, int remoteAddressPort, byte[] bytes)
    {
        socket.SendTo(bytes, new IPEndPoint(remoteAddress, remoteAddressPort));
    }
}