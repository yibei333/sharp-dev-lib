using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp固定头发送适配器(每次发送消息在前四个字节中放入字节的长度总和,没有粘包问题)
/// </summary>
public class TcpFixedHeaderSendAdapter : ITcpSendAdapter
{
    const int maxLength = (int)(1.9 * 1024 * 1024 * 1024);

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    public void Send(Socket socket, byte[] bytes)
    {
        if (bytes.Length > maxLength) throw new NotSupportedException("data is too long,just cut the data to transfer");

        var header = BitConverter.GetBytes(bytes.Length);
        var data = header.Concat(bytes).ToArray();
        socket.Send(data);
    }
}