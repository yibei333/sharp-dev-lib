using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// UDP客户端
/// </summary>
public class UdpClient : IDisposable
{
    bool _isDisposed;
    const int maxLength = 65507;//这是UDP协议的极限

    internal UdpClient(int bufferSize)
    {
        if (bufferSize <= 4) throw new Exception("bufferSize需要大于4");
        BufferSize = bufferSize;
        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp)
        {
            SendBufferSize = BufferSize,
            ReceiveBufferSize = BufferSize
        };
    }

    internal UdpClient(IPAddress localAdress, int localPort, int bufferSize)
    {
        if (bufferSize <= 4) throw new Exception("bufferSize需要大于4");
        BufferSize = bufferSize;
        LocalAdress = localAdress;
        LocalPort = localPort;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp)
        {
            SendBufferSize = BufferSize,
            ReceiveBufferSize = BufferSize
        };
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

    async void NotifyReceived(byte[] bytes, IPEndPoint? remoteEndPoint)
    {
        await Task.Run(() =>
        {
            Received?.Invoke(this, new UdpClientDataEventArgs(this, bytes) { RemoteEndPoint = remoteEndPoint?.Port == 0 ? null : remoteEndPoint });
        });
    }

    async void NotifySended(byte[] bytes, IPEndPoint? remoteEndPoint)
    {
        await Task.Run(() =>
        {
            Sended?.Invoke(this, new UdpClientDataEventArgs(this, bytes) { RemoteEndPoint = remoteEndPoint?.Port == 0 ? null : remoteEndPoint });
        });
    }

    async void NotifyError(Exception ex, IPEndPoint? remoteEndPoint)
    {
        await Task.Run(() =>
        {
            Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex) { RemoteEndPoint = remoteEndPoint?.Port == 0 ? null : remoteEndPoint });
        });
    }

    /// <summary>
    /// 开始异步接收数据
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public void StartReceive(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken?.IsCancellationRequested ?? false) return;
        if (_isDisposed) return;

        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        try
        {
            var buffer = new byte[BufferSize];
            Socket.BeginReceiveFrom(buffer, 0, BufferSize, SocketFlags.None, ref remoteEndPoint, ReceiveCallback, buffer);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            NotifyError(ex, remoteEndPoint as IPEndPoint);
        }
    }

    void ReceiveCallback(IAsyncResult result)
    {
        EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        try
        {
            var buffer = (byte[])result.AsyncState;
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var length = Socket.EndReceiveFrom(result, ref remoteEndPoint);
            var bytes = buffer.Take(length).ToArray();
            if (bytes.IsNullOrEmpty()) return;
            NotifyReceived(bytes, remoteEndPoint as IPEndPoint);
            var nextBuffer = new byte[BufferSize];
            Socket.BeginReceiveFrom(nextBuffer, 0, BufferSize, SocketFlags.None, ref endPoint, ReceiveCallback, nextBuffer);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            NotifyError(ex, endPoint as IPEndPoint);
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
        var remoteEndPoint = new IPEndPoint(remoteAdress, remotePort);
        try
        {
            if (_isDisposed) throw new ObjectDisposedException("无法访问已释放的UDP客户端");
            if (bytes.Length > maxLength) throw new NotSupportedException($"数据长度超出限制{maxLength},请分段传输数据");
            Socket.SendTo(bytes, remoteEndPoint);
            NotifySended(bytes, remoteEndPoint);
        }
        catch (Exception ex)
        {
            NotifyError(ex, remoteEndPoint);
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
