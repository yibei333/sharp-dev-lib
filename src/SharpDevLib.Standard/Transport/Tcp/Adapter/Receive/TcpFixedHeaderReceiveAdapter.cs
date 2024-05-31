using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp固定头接收适配器(每次接收前四个字节作为数据长度,没有粘包问题)
/// </summary>
public class TcpFixedHeaderReceiveAdapter : ITcpReceiveAdapter
{
    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>字节数组</returns>
    public byte[] Receive(Socket socket)
    {
        var sizeBuffer = new byte[4];
        var sizeLength = socket.Receive(sizeBuffer);
        if (sizeLength != sizeBuffer.Length) throw new InvalidDataException("fix header length should be 4");

        var size = BitConverter.ToInt32(sizeBuffer, 0);
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        if (length != buffer.Length) throw new InvalidDataException($"client should use fix header adapter");
        return buffer;
    }
}