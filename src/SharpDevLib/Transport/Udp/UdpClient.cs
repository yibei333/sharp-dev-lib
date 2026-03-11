using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// UDP客户端
/// </summary>
public class UdpClient : IDisposable
{
    bool _isDisposed;
    const int maxLength = (int)(1.9 * 1024 * 1024 * 1024);

    internal UdpClient(int bufferSize)
    {
        if (bufferSize <= 4) throw new Exception("bufferSize需要大于4");
        BufferSize = bufferSize;
        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
    }

    internal UdpClient(IPAddress localAdress, int localPort, int bufferSize)
    {
        if (bufferSize <= 4) throw new Exception("bufferSize需要大于4");
        BufferSize = bufferSize;
        LocalAdress = localAdress;
        LocalPort = localPort;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        Socket.Bind(new IPEndPoint(localAdress, localPort));
    }

    /// <summary>
    /// 缓冲区大小
    /// </summary>
    public int BufferSize { get; }

    /// <summary>
    /// 套接字
    /// </summary>
    public Socket Socket { get; private set; }

    /// <summary>
    /// 本地绑定IP地址
    /// </summary>
    public IPAddress? LocalAdress { get; }

    /// <summary>
    /// 本地端口
    /// </summary>
    public int? LocalPort { get; }

    /// <summary>
    /// 接收到数据事件
    /// </summary>
    public event EventHandler<UdpClientDataEventArgs>? Received;

    /// <summary>
    /// 数据发送完成事件
    /// </summary>
    public event EventHandler<UdpClientDataEventArgs>? Sended;

    /// <summary>
    /// 发生异常事件
    /// </summary>
    public event EventHandler<UdpClientExceptionEventArgs>? Error;

    /// <summary>
    /// 开始异步接收数据
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public void StartReceive(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken?.IsCancellationRequested ?? false) return;
        if (_isDisposed) return;

        try
        {
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var buffer = new byte[BufferSize];
            Socket.BeginReceiveFrom(buffer, 0, BufferSize, SocketFlags.None, ref remoteEndPoint, ReceiveCallback, buffer);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex));
        }
    }

    void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            var buffer = (byte[])result.AsyncState;
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var length = Socket.EndReceiveFrom(result, ref remoteEndPoint);
            var bytes = buffer.Take(length).ToArray();
            if (bytes.IsNullOrEmpty()) return;
            Received?.Invoke(this, new UdpClientDataEventArgs(this, bytes) { RemoteEndPoint = remoteEndPoint });

            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            var nextBuffer = new byte[BufferSize];
            Socket.BeginReceiveFrom(nextBuffer, 0, BufferSize, SocketFlags.None, ref endPoint, ReceiveCallback, nextBuffer);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex));

            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var buffer = new byte[BufferSize];
            Socket.BeginReceiveFrom(buffer, 0, BufferSize, SocketFlags.None, ref remoteEndPoint, ReceiveCallback, buffer);
        }
    }

    /// <summary>
    /// 发送数据到指定远程端点
    /// </summary>
    /// <param name="remoteAdress">远程IP地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="bytes">要发送的字节数组</param>
    /// <param name="throwIfException">发送失败是否抛出异常，默认false，可订阅Error事件</param>
    public void Send(IPAddress remoteAdress, int remotePort, byte[] bytes, bool throwIfException = false)
    {
        try
        {
            if (_isDisposed) throw new ObjectDisposedException("无法访问已释放的UDP客户端");
            if (bytes.Length > BufferSize) throw new NotSupportedException($"数据长度超出限制{maxLength},请分段传输数据");
            Socket.SendTo(bytes, new IPEndPoint(remoteAdress, remotePort));
            Sended?.Invoke(this, new UdpClientDataEventArgs(this, bytes));
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex) { RemoteEndPoint = new IPEndPoint(remotePort, remotePort) });
            if (throwIfException) throw ex;
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _isDisposed = true;
        Socket.Dispose();
    }
}
