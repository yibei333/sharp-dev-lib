using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp监听器
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型(可以用来绑定会话的身份信息)</typeparam>
public class TcpListener<TSessionMetadata> : IDisposable
{
    TcpListnerStates _state = 0;
    readonly List<TcpSession<TSessionMetadata>> _sessions;
    static readonly object _lock = new();

    internal TcpListener(IPAddress iPAddress, int port, IServiceProvider? serviceProvider, TransportAdapterType adapterType = TransportAdapterType.Default)
    {
        IPAddress = iPAddress;
        Port = port;
        ServiceProvider = serviceProvider;
        AdapterType = adapterType;
        Logger = ServiceProvider?.GetService<ILogger<TcpListener>>();

        Socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        Socket.Bind(new IPEndPoint(iPAddress, port));
        State = TcpListnerStates.Created;

        _sessions = new();
        Sessions = new ReadOnlyCollection<TcpSession<TSessionMetadata>>(_sessions);
    }

    ILogger<TcpListener>? Logger { get; }

    /// <summary>
    /// 套接字
    /// </summary>
    public Socket Socket { get; }

    /// <summary>
    /// 接收数据适配器类型
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
    /// 状态
    /// </summary>
    public TcpListnerStates State
    {
        get => _state;
        private set
        {
            if (_state == value) return;
            var before = _state;
            _state = value;
            StateChanged?.Invoke(this, new TcpListenerStateChangedEventArgs(before, _state));
        }
    }

    /// <summary>
    /// 地址
    /// </summary>
    public IPAddress IPAddress { get; }

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// ServiceProvider
    /// </summary>
    public IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 会话集合
    /// </summary>
    public IReadOnlyCollection<TcpSession<TSessionMetadata>> Sessions { get; }

    /// <summary>
    /// 状态变更回调事件
    /// </summary>
    public event EventHandler<TcpListenerStateChangedEventArgs>? StateChanged;

    /// <summary>
    /// 添加了会话回调事件
    /// </summary>
    public event EventHandler<TcpSessionEventArgs<TSessionMetadata>>? SessionAdded;

    /// <summary>
    /// 删除了会话回调事件
    /// </summary>
    public event EventHandler<TcpSessionEventArgs<TSessionMetadata>>? SessionRemoved;

    /// <summary>
    /// 开始监听
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>task</returns>
    public async Task ListenAsync(CancellationToken? cancellationToken = null)
    {
        if (State == TcpListnerStates.Listening) return;
        if (State == TcpListnerStates.Closed) throw new ObjectDisposedException(this.GetTypeName(), "can not access a closed tcp listener");

        await Task.Run(() =>
        {
            try
            {
                Socket.Listen(int.MaxValue);
                State = TcpListnerStates.Listening;
                Accept(cancellationToken);
            }
            catch (Exception ex)
            {
                LogError(ex);
                Close();
            }
        }, cancellationToken ?? CancellationToken.None);
    }

    void Accept(CancellationToken? cancellationToken)
    {
        while (true)
        {
            if (State != TcpListnerStates.Listening) break;
            if (cancellationToken?.IsCancellationRequested ?? false)
            {
                Close();
                break;
            }

            try
            {
                var socket = Socket.Accept();
                var session = new TcpSession<TSessionMetadata>(this, socket);
                _sessions.Add(session);
                SessionAdded?.Invoke(this, new TcpSessionEventArgs<TSessionMetadata>(session));
                try
                {
                    Task.Run(session.Receive);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
            catch
            {
                Close();
                break;
            }
        }
    }

    internal void RemoveSession(TcpSession<TSessionMetadata> session)
    {
        if (!_sessions.Contains(session)) return;
        lock (_lock)
        {
            if (!_sessions.Contains(session)) return;
            _sessions.Remove(session);
            SessionRemoved?.Invoke(this, new TcpSessionEventArgs<TSessionMetadata>(session));
        }
    }

    /// <summary>
    /// 关闭监听
    /// </summary>
    public void Close()
    {
        if (State == TcpListnerStates.Closed) return;
        while (Sessions.Any()) Sessions.First().Close();
        Socket.Dispose();
        State = TcpListnerStates.Closed;
    }

    /// <summary>
    /// dispose
    /// </summary>
    public void Dispose()
    {
        Close();
    }

    void LogError(Exception exception)
    {
        Logger?.LogError(exception, exception.Message);
        Debug.WriteLine(exception.Message);
        Debug.WriteLine(exception?.StackTrace);
    }
}