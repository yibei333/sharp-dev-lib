using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp默认接收适配器(每次按照最大64KB字节获取数据,有粘包问题)
/// </summary>
public class TcpDefaultReceiveAdapter : ITcpReceiveAdapter
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
}