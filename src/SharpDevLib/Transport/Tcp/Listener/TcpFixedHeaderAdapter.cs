using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输固定头接收适配器
/// </summary>
/// <remarks>每次接收前四个字节作为数据长度，没有粘包问题</remarks>
public class TcpFixedHeaderAdapter : ITcpAdapter
{
    /// <summary>
    /// 发送数据到套接字
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    public void Send(Socket socket, byte[] bytes)
    {
        var header = BitConverter.GetBytes(bytes.Length);
        var data = header.Concat(bytes).ToArray();
        socket.Send(data);
    }

    /// <summary>
    /// 套接字开始接收数据
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bufferSize">缓存区大小</param>
    /// <param name="asyncCallback">回调</param>
    public void BeginReceive(Socket socket, int bufferSize, AsyncCallback asyncCallback)
    {
        var sizeBuffer = new byte[4];
        socket.BeginReceive(sizeBuffer, 0, 4, SocketFlags.None, (r) =>
        {
            var b = (byte[])r.AsyncState;
            var l = socket.EndReceive(r);
            if (l != b.Length) throw new InvalidDataException("固定头长度应为4字节");
            var size = BitConverter.ToInt32(sizeBuffer, 0);
            var buffer = new byte[size];
            socket.BeginReceive(buffer, 0, size, SocketFlags.None, asyncCallback, buffer);
        }, sizeBuffer);
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