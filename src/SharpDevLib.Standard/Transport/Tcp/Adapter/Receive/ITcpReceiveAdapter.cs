using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp接收适配器
/// </summary>
public interface ITcpReceiveAdapter
{
    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>字节数组</returns>
    byte[] Receive(Socket socket);
}