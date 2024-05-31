using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp发送适配器
/// </summary>
public interface ITcpSendAdapter
{
    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">字节数组</param>
    void Send(Socket socket, byte[] bytes);
}