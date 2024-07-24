using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Transport;

/// <summary>
/// 传输固定头接收适配器(每次接收前四个字节作为数据长度,没有粘包问题)
/// </summary>
public class TransportFixedHeaderReceiveAdapter : ITransportReceiveAdapter
{
    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>字节数组</returns>
    /// <exception cref="InvalidDataException">获取头部字节长度失败时引发异常</exception>
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

    /// <summary>
    /// 接收
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程终结点</param>
    /// <returns>字节数组</returns>
    /// <exception cref="InvalidDataException">获取头部字节长度失败时引发异常</exception>
    public byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint)
    {
        var sizeBuffer = new byte[4];
        var sizeLength = socket.ReceiveFrom(sizeBuffer, ref remoteEndPoint);
        if (sizeLength != sizeBuffer.Length) throw new InvalidDataException("fix header length should be 4");

        var size = BitConverter.ToInt32(sizeBuffer, 0);
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        if (length != buffer.Length) throw new InvalidDataException($"client should use fix header adapter");
        return buffer;
    }
}