using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Udp客户端
/// </summary>
public class UdpClient : IDisposable
{
    bool _isDisposed;

    internal UdpClient(IServiceProvider? serviceProvider, TransportAdapterType adapterType = TransportAdapterType.Default)
    {
        ServiceProvider = serviceProvider;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
    }

    internal UdpClient(IServiceProvider? serviceProvider, IPAddress localAdress, int localPort, TransportAdapterType adapterType = TransportAdapterType.Default)
    {
        ServiceProvider = serviceProvider;
        LocalAdress = localAdress;
        LocalPort = localPort;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        Socket.Bind(new IPEndPoint(localAdress, localPort));
    }

    /// <summary>
    /// 套接字
    /// </summary>
    public Socket Socket { get; private set; }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 本地地址
    /// </summary>
    public IPAddress? LocalAdress { get; }

    /// <summary>
    /// 本地端口
    /// </summary>
    public int? LocalPort { get; }

    /// <summary>
    /// 收发适配器类型
    /// </summary>
    public TransportAdapterType AdapterType { get; }

    /// <summary>
    /// 接收数据适配器(仅当AdapterType=UdpAdapterType.Custom时有用)
    /// </summary>
    public ITransportReceiveAdapter? ReceiveAdapter { get; set; }

    /// <summary>
    /// 发送数据适配器(仅当AdapterType=UdpAdapterType.Custom时有用)
    /// </summary>
    public ITransportSendAdapter? SendAdapter { get; set; }

    /// <summary>
    /// 接收事件
    /// </summary>
    public event EventHandler<UdpClientDataEventArgs>? Received;

    /// <summary>
    /// 发送事件
    /// </summary>
    public event EventHandler<UdpClientDataEventArgs>? Sended;

    /// <summary>
    /// 异常事件
    /// </summary>
    public event EventHandler<UdpClientExceptionEventArgs>? Error;

    /// <summary>
    /// 开始接收
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>task</returns>
    public async Task ReceiveAsync(CancellationToken? cancellationToken = null)
    {
        await Task.Run(() =>
        {
            while (true)
            {
                if (cancellationToken?.IsCancellationRequested ?? false) break;
                if (_isDisposed) break;

                try
                {
                    EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    var bytes = AdapterType.GetReceiveAdapter(ReceiveAdapter).ReceiveFrom(Socket, ref remoteEndPoint);
                    if (bytes.IsNullOrEmpty()) break;
                    Received?.Invoke(this, new UdpClientDataEventArgs(this, bytes) { RemoteEndPoint = remoteEndPoint });
                }
                catch (Exception ex)
                {
                    if (_isDisposed) break;
                    Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex));
                }
            }
        }, cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="bytes">字节数组</param>
    /// <param name="throwIfException">发送失败是否抛出异常,默认false,可以订阅Error事件</param>
    public void Send(IPAddress remoteAdress, int remotePort, byte[] bytes, bool throwIfException = false)
    {
        try
        {
            if (_isDisposed) throw new ObjectDisposedException("can not access a disposed udp client");
            AdapterType.GetSendAdapter(SendAdapter).SendTo(Socket, remoteAdress, remotePort, bytes);
            Sended?.Invoke(this, new UdpClientDataEventArgs(this, bytes));
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new UdpClientExceptionEventArgs(this, ex) { RemoteEndPoint = new IPEndPoint(remotePort, remotePort) });
            if (throwIfException) throw ex;
        }
    }

    /// <summary>
    /// dispose
    /// </summary>
    public void Dispose()
    {
        _isDisposed = true;
        Socket.Dispose();
    }
}
