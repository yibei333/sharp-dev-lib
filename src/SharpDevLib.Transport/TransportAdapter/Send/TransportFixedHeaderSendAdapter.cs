using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Transport;

/// <summary>
/// 传输固定头发送适配器(每次发送消息在前四个字节中放入字节的长度总和,没有粘包问题)
/// </summary>
public class TransportFixedHeaderSendAdapter : ITransportSendAdapter
{
    const int maxLength = (int)(1.9 * 1024 * 1024 * 1024);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    /// <exception cref="NotSupportedException">当字节长度超过2040109465时引发异常</exception>
    public void Send(Socket socket, byte[] bytes)
    {
        if (bytes.Length > maxLength) throw new NotSupportedException("data is too long,just cut the data to transfer");

        var header = BitConverter.GetBytes(bytes.Length);
        var data = header.Concat(bytes).ToArray();
        socket.Send(data);
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="remoteAddress">远程地址</param>
    /// <param name="remoteAddressPort">远程端口</param>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    /// <exception cref="NotSupportedException">当字节长度超过2040109465时引发异常</exception>
    public void SendTo(Socket socket, IPAddress remoteAddress, int remoteAddressPort, byte[] bytes)
    {
        if (bytes.Length > maxLength) throw new NotSupportedException("data is too long,just cut the data to transfer");

        var header = BitConverter.GetBytes(bytes.Length);
        var data = header.Concat(bytes).ToArray();
        socket.SendTo(data, new IPEndPoint(remoteAddress, remoteAddressPort));
    }
}