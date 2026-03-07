using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输固定头接收适配器
/// </summary>
/// <remarks>每次接收前四个字节作为数据长度，没有粘包问题</remarks>
public class TransportFixedHeaderReceiveAdapter : ITransportReceiveAdapter
{
    /// <summary>
    /// 接收数据（TCP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <returns>接收到的字节数组</returns>
    /// <exception cref="InvalidDataException">获取头部字节长度失败时引发异常</exception>
    public byte[] Receive(Socket socket)
    {
        var sizeBuffer = new byte[4];
        var sizeLength = socket.Receive(sizeBuffer);
        if (sizeLength != sizeBuffer.Length) throw new InvalidDataException("固定头长度应为4字节");

        var size = BitConverter.ToInt32(sizeBuffer, 0);
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        if (length != buffer.Length) throw new InvalidDataException("客户端应使用固定头适配器");
        return buffer;
    }

    /// <summary>
    /// 接收数据（UDP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteEndPoint">远程端点</param>
    /// <returns>接收到的字节数组</returns>
    /// <exception cref="InvalidDataException">获取头部字节长度失败时引发异常</exception>
    public byte[] ReceiveFrom(Socket socket, ref EndPoint remoteEndPoint)
    {
        var sizeBuffer = new byte[4];
        var sizeLength = socket.ReceiveFrom(sizeBuffer, ref remoteEndPoint);
        if (sizeLength != sizeBuffer.Length) throw new InvalidDataException("固定头长度应为4字节");

        var size = BitConverter.ToInt32(sizeBuffer, 0);
        var buffer = new byte[size];
        var length = socket.Receive(buffer);
        if (length != buffer.Length) throw new InvalidDataException("客户端应使用固定头适配器");
        return buffer;
    }
}