using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp默认发送适配器
/// </summary>
public class TcpDefaultSendAdapter : ITcpSendAdapter
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
}