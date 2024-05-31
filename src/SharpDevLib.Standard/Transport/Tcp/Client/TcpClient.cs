using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp客户端
/// </summary>
public class TcpClient : IDisposable
{
    TcpClientStates _state = 0;

    internal TcpClient(IServiceProvider? serviceProvider, IPAddress remoteAdress, int remotePort, TcpAdapterType adapterType = TcpAdapterType.Default)
    {
        ServiceProvider = serviceProvider;
        RemoteAdress = remoteAdress;
        RemotePort = remotePort;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        State = TcpClientStates.Created;
    }

    internal TcpClient(IServiceProvider? serviceProvider, IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TcpAdapterType adapterType = TcpAdapterType.Default)
    {
        ServiceProvider = serviceProvider;
        LocalAdress = localAdress;
        LocalPort = localPort;
        RemoteAdress = remoteAdress;
        RemotePort = remotePort;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        State = TcpClientStates.Created;
    }

    /// <summary>
    /// 套接字
    /// </summary>
    public Socket Socket { get; private set; }

    /// <summary>
    /// 状态
    /// </summary>
    public TcpClientStates State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            var before = _state;
            _state = value;
            StateChanged?.Invoke(this, new TcpClientStateChangedEventArgs(this, before, _state));
        }
    }

    /// <summary>
    /// 状态变更回调事件
    /// </summary>
    public event EventHandler<TcpClientStateChangedEventArgs>? StateChanged;

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
    /// 远程地址
    /// </summary>
    public IPAddress RemoteAdress { get; }

    /// <summary>
    /// 远程端口
    /// </summary>
    public int RemotePort { get; }

    /// <summary>
    /// 收发适配器类型
    /// </summary>
    public TcpAdapterType AdapterType { get; }

    /// <summary>
    /// 接收数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)
    /// </summary>
    public ITcpReceiveAdapter? ReceiveAdapter { get; set; }

    /// <summary>
    /// 发送数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)
    /// </summary>
    public ITcpSendAdapter? SendAdapter { get; set; }

    /// <summary>
    /// 接收事件
    /// </summary>
    public event EventHandler<TcpClientDataEventArgs>? Received;

    /// <summary>
    /// 发送事件
    /// </summary>
    public event EventHandler<TcpClientDataEventArgs>? Sended;

    /// <summary>
    /// 异常事件
    /// </summary>
    public event EventHandler<TcpClientExceptionEventArgs>? Error;

    /// <summary>
    /// 开始连接和接收
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>task</returns>
    public async Task ConnectAndReceiveAsync(CancellationToken? cancellationToken = null)
    {
        await Task.Run(() =>
        {
            if (State == TcpClientStates.Connected) return;

            if (State == TcpClientStates.Closed)
            {
                Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                State = TcpClientStates.Created;
            }

            if (LocalPort is not null && LocalPort > 0 && LocalPort < 256) Socket.Bind(new IPEndPoint(LocalAdress ?? IPAddress.None, LocalPort.Value));
            Socket.Connect(new IPEndPoint(RemoteAdress, RemotePort));
            State = TcpClientStates.Connected;
            Receive(cancellationToken);
        }, cancellationToken ?? CancellationToken.None);
    }

    void Receive(CancellationToken? cancellationToken = null)
    {
        while (true)
        {
            if (cancellationToken?.IsCancellationRequested ?? false)
            {
                Close();
                break;
            }

            if (State != TcpClientStates.Created || !Socket.Connected) break;

            try
            {
                var bytes = AdapterType.GetReceiveAdapter(ReceiveAdapter).Receive(Socket);
                if (bytes.IsNullOrEmpty())
                {
                    Close();
                    break;
                }
                Received?.Invoke(this, new TcpClientDataEventArgs(this, bytes));
            }
            catch (SocketException ex)
            {
                Close();
                Error?.Invoke(this, new TcpClientExceptionEventArgs(this, ex));
                break;
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, new TcpClientExceptionEventArgs(this, ex));
            }
        }
    }

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="bytes">字节数组</param>
    public void Send(byte[] bytes)
    {
        try
        {
            if (State != TcpClientStates.Connected || !Socket.Connected) throw new Exception("can not access a closed tcp client");
            AdapterType.GetSendAdapter(SendAdapter).Send(Socket, bytes);
            Sended?.Invoke(this, new TcpClientDataEventArgs(this, bytes));
        }
        catch (SocketException ex)
        {
            Close();
            Error?.Invoke(this, new TcpClientExceptionEventArgs(this, ex));
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new TcpClientExceptionEventArgs(this, ex));
            throw ex;
        }
    }

    /// <summary>
    /// 关闭并释放连接
    /// </summary>
    public void Close()
    {
        Socket.Close();
        State = TcpClientStates.Closed;
    }

    /// <summary>
    /// dispose
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}
