using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输默认接收适配器
/// </summary>
/// <remarks>每次按照最大64KB字节获取数据，存在粘包问题，适用于简单场景</remarks>
public class TcpDefaultAdapter : ITcpAdapter
{
    /// <summary>
    /// 发送数据到套接字
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    public void Send(Socket socket, byte[] bytes)
    {
        socket.Send(bytes);
    }

    /// <summary>
    /// 套接字开始接收数据
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bufferSize">缓存区大小</param>
    /// <param name="asyncCallback">回调</param>
    public void BeginReceive(Socket socket, int bufferSize, AsyncCallback asyncCallback)
    {
        var buffer = new byte[bufferSize];
        socket.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, asyncCallback, buffer);
    }

    /// <summary>
    /// 从套接字接收数据
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="result">异步结果</param>
    /// <returns>接收到的字节数组</returns>
    public byte[] EndReceive(Socket socket, IAsyncResult result)
    {
        var buffer = (byte[])result.AsyncState;
        var length = socket.EndReceive(result);
        return [.. buffer.Take(length)];
    }
}