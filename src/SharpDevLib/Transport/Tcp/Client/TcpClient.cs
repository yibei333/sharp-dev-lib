using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// TCP客户端
/// </summary>
public class TcpClient : IDisposable
{
    TcpClientStates _state = 0;

    internal TcpClient(IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default)
    {
        RemoteAdress = remoteAdress;
        RemotePort = remotePort;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        State = TcpClientStates.Created;
    }

    internal TcpClient(IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default)
    {
        LocalAdress = localAdress;
        LocalPort = localPort;
        RemoteAdress = remoteAdress;
        RemotePort = remotePort;
        AdapterType = adapterType;

        Socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        State = TcpClientStates.Created;
    }

    /// <summary>
    /// 底层套接字
    /// </summary>
    public Socket Socket { get; private set; }

    /// <summary>
    /// 客户端状态
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
    /// 状态变更事件
    /// </summary>
    public event EventHandler<TcpClientStateChangedEventArgs>? StateChanged;

    /// <summary>
    /// 本地绑定IP地址
    /// </summary>
    public IPAddress? LocalAdress { get; }

    /// <summary>
    /// 本地绑定端口
    /// </summary>
    public int? LocalPort { get; }

    /// <summary>
    /// 远程服务器IP地址
    /// </summary>
    public IPAddress RemoteAdress { get; }

    /// <summary>
    /// 远程服务器端口
    /// </summary>
    public int RemotePort { get; }

    /// <summary>
    /// 收发适配器类型
    /// </summary>
    public TransportAdapterType AdapterType { get; }

    /// <summary>
    /// 接收数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)
    /// </summary>
    public ITransportReceiveAdapter? ReceiveAdapter { get; set; }

    /// <summary>
    /// 发送数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)
    /// </summary>
    public ITransportSendAdapter? SendAdapter { get; set; }

    /// <summary>
    /// 接收到数据事件
    /// </summary>
    public event EventHandler<TcpClientDataEventArgs>? Received;

    /// <summary>
    /// 数据发送完成事件
    /// </summary>
    public event EventHandler<TcpClientDataEventArgs>? Sended;

    /// <summary>
    /// 发生异常事件
    /// </summary>
    public event EventHandler<TcpClientExceptionEventArgs>? Error;

    /// <summary>
    /// 连接到服务器并开始接收数据
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
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

            if (State != TcpClientStates.Connected || !Socket.Connected) break;

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
    /// 发送数据
    /// </summary>
    /// <param name="bytes">要发送的字节数组</param>
    /// <param name="throwIfException">发送失败是否抛出异常，默认false，可订阅Error事件</param>
    public void Send(byte[] bytes, bool throwIfException = false)
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
            if (throwIfException) throw ex;
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new TcpClientExceptionEventArgs(this, ex));
            if (throwIfException) throw ex;
        }
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        Socket.Close();
        State = TcpClientStates.Closed;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}
