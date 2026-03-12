using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// TCP监听器
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型（可以用来绑定会话的身份信息）</typeparam>
public class TcpListener<TSessionMetadata> : IDisposable
{
    TcpListnerStates _state = 0;
    readonly List<TcpSession<TSessionMetadata>> _sessions;
    static readonly object _lock = new();

    internal TcpListener(IPAddress iPAddress, int port, int bufferSize, ITcpAdapter? adapter)
    {
        if (bufferSize <= 4) throw new Exception("bufferSize需要大于4");
        BufferSize = bufferSize;
        IPAddress = iPAddress;
        Port = port;
        Adapter = adapter ?? TcpAdapters.Default;

        Socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        {
            SendBufferSize = bufferSize,
            ReceiveBufferSize = bufferSize,
        };
        Socket.Bind(new IPEndPoint(iPAddress, port));
        State = TcpListnerStates.Created;

        _sessions = [];
        Sessions = new ReadOnlyCollection<TcpSession<TSessionMetadata>>(_sessions);
    }

    /// <summary>
    /// 缓冲区大小
    /// </summary>
    public int BufferSize { get; }

    /// <summary>
    /// 底层套接字
    /// </summary>
    public Socket Socket { get; }

    /// <summary>
    /// 发送/接收数据适配器
    /// </summary>
    public ITcpAdapter Adapter { get; set; }

    /// <summary>
    /// 监听器状态
    /// </summary>
    public TcpListnerStates State
    {
        get => _state;
        private set
        {
            if (_state == value) return;
            var before = _state;
            _state = value;
            NotifyStageChanged(before);
        }
    }

    /// <summary>
    /// 监听IP地址
    /// </summary>
    public IPAddress IPAddress { get; }

    /// <summary>
    /// 监听端口
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// 当前所有会话的只读集合
    /// </summary>
    public IReadOnlyCollection<TcpSession<TSessionMetadata>> Sessions { get; }

    /// <summary>
    /// 状态变更事件
    /// </summary>
    public event EventHandler<TcpListenerStateChangedEventArgs>? StateChanged;

    /// <summary>
    /// 添加新会话事件
    /// </summary>
    public event EventHandler<TcpSessionEventArgs<TSessionMetadata>>? SessionAdded;

    /// <summary>
    /// 移除会话事件
    /// </summary>
    public event EventHandler<TcpSessionEventArgs<TSessionMetadata>>? SessionRemoved;

    async void NotifyStageChanged(TcpListnerStates before)
    {
        await Task.Run(() =>
        {
            StateChanged?.Invoke(this, new TcpListenerStateChangedEventArgs(before, _state));
        });
    }

    async void NotifySessionAdded(TcpSession<TSessionMetadata> session)
    {
        await Task.Run(() =>
        {
            SessionAdded?.Invoke(this, new TcpSessionEventArgs<TSessionMetadata>(session));
        });
    }

    async void NotifySessionRemoved(TcpSession<TSessionMetadata> session)
    {
        await Task.Run(() =>
        {
            SessionRemoved?.Invoke(this, new TcpSessionEventArgs<TSessionMetadata>(session));
        });
    }

    /// <summary>
    /// 开始监听连接
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="Exception">监听器已关闭时引发异常</exception>
    public void StartListen(CancellationToken? cancellationToken = null)
    {
        if (State == TcpListnerStates.Listening) return;
        if (State == TcpListnerStates.Closed) throw new Exception("无法访问已关闭的TCP监听器");

        try
        {
            Socket.Listen(int.MaxValue);
            State = TcpListnerStates.Listening;
            Socket.BeginAccept(AcceptCallback, cancellationToken);
        }
        catch (Exception ex)
        {
            TcpHelper.Logger?.LogError(ex, ex.Message);
            Close();
        }
    }

    void AcceptCallback(IAsyncResult result)
    {
        if (State != TcpListnerStates.Listening) return;
        var cancellationToken = (CancellationToken?)result.AsyncState;
        if (cancellationToken?.IsCancellationRequested ?? false)
        {
            Close();
            return;
        }

        try
        {
            var socket = Socket.EndAccept(result);
            socket.SendBufferSize = BufferSize;
            socket.ReceiveBufferSize = BufferSize;
            var session = new TcpSession<TSessionMetadata>(this, socket);
            _sessions.Add(session);
            NotifySessionAdded(session);
            session.Receive();
            Socket.BeginAccept(AcceptCallback, cancellationToken);
        }
        catch
        {
            Close();
        }
    }

    internal void RemoveSession(TcpSession<TSessionMetadata> session)
    {
        if (!_sessions.Contains(session)) return;
        lock (_lock)
        {
            if (!_sessions.Contains(session)) return;
            _sessions.Remove(session);
            NotifySessionRemoved(session);
        }
    }

    /// <summary>
    /// 关闭监听器并释放所有会话
    /// </summary>
    public void Close()
    {
        if (State == TcpListnerStates.Closed) return;
        while (Sessions.Any()) Sessions.First().Close();
        Socket.Dispose();
        State = TcpListnerStates.Closed;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}

/// <summary>
/// TCP监听器（非泛型版本）
/// </summary>
public class TcpListener : TcpListener<int>
{
    internal TcpListener(IPAddress iPAddress, int port, int bufferSize, ITcpAdapter? adapter) : base(iPAddress, port, bufferSize, adapter)
    {
    }
}