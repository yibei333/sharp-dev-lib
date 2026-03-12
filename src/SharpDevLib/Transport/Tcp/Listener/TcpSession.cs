using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// TCP会话
/// </summary>
/// <typeparam name="TMetadata">会话元数据类型</typeparam>
public class TcpSession<TMetadata> : IDisposable
{
    const int maxLength = (int)(1.9 * 1024 * 1024 * 1024);

    TcpSessionStates _state = 0;
    bool _isDisposed;

    internal TcpSession(TcpListener<TMetadata> listener, Socket socket)
    {
        Listener = listener;
        Socket = socket;
        State = TcpSessionStates.Connected;
    }

    /// <summary>
    /// 底层套接字
    /// </summary>
    public Socket Socket { get; }

    /// <summary>
    /// 所属的TCP监听器
    /// </summary>
    public TcpListener<TMetadata> Listener { get; }

    /// <summary>
    /// 会话元数据
    /// </summary>
    public TMetadata? Metadata { get; set; }

    /// <summary>
    /// 会话状态
    /// </summary>
    public TcpSessionStates State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            var before = _state;
            _state = value;
            NotifyStateChanged(before);
        }
    }

    /// <summary>
    /// 状态变更事件
    /// </summary>
    public event EventHandler<TcpSessionStateChangedEventArgs<TMetadata>>? StateChanged;

    /// <summary>
    /// 接收到数据事件
    /// </summary>
    public event EventHandler<TcpSessionDataEventArgs<TMetadata>>? Received;

    /// <summary>
    /// 数据发送完成事件
    /// </summary>
    public event EventHandler<TcpSessionDataEventArgs<TMetadata>>? Sended;

    /// <summary>
    /// 发生异常事件
    /// </summary>
    public event EventHandler<TcpSessionExceptionEventArgs<TMetadata>>? Error;

    async void NotifyStateChanged(TcpSessionStates before)
    {
        await Task.Run(() =>
        {
            StateChanged?.Invoke(this, new TcpSessionStateChangedEventArgs<TMetadata>(this, before, _state));
        });
    }

    async void NotifyReceived(byte[] bytes)
    {
        await Task.Run(() =>
        {
            Received?.Invoke(this, new TcpSessionDataEventArgs<TMetadata>(this, bytes));
        });
    }

    async void NotifySended(byte[] bytes)
    {
        await Task.Run(() =>
        {
            Sended?.Invoke(this, new TcpSessionDataEventArgs<TMetadata>(this, bytes));
        });
    }

    async void NotifyError(Exception ex)
    {
        await Task.Run(() =>
        {
            Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetadata>(this, ex));
        });
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
            if (bytes.Length > maxLength) throw new NotSupportedException($"数据长度超出限制{maxLength},请分段传输数据");
            if (State != TcpSessionStates.Connected || !Socket.Connected) throw new Exception("无法访问已关闭的TCP会话");
            Listener.Adapter.Send(Socket, bytes);
            NotifySended(bytes);
        }
        catch (SocketException ex)
        {
            Close();
            NotifyError(ex);
            if (throwIfException) throw ex;
        }
        catch (Exception ex)
        {
            NotifyError(ex);
            if (throwIfException) throw ex;
        }
    }

    internal void Receive()
    {
        if (State != TcpSessionStates.Connected || !Socket.Connected) return;
        if (_isDisposed) return;
        try
        {
            Listener.Adapter.BeginReceive(Socket, Listener.BufferSize, ReceiveCallback);
        }
        catch (SocketException ex)
        {
            Close();
            NotifyError(ex);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            NotifyError(ex);
        }
    }

    void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            var bytes = Listener.Adapter.EndReceive(Socket, result);
            if (bytes.IsNullOrEmpty())
            {
                Close();
                return;
            }
            NotifyReceived(bytes);
            Listener.Adapter.BeginReceive(Socket, Listener.BufferSize, ReceiveCallback);
        }
        catch (SocketException ex)
        {
            Close();
            NotifyError(ex);
        }
        catch (Exception ex)
        {
            if (_isDisposed) return;
            NotifyError(ex);
            Listener.Adapter.BeginReceive(Socket, Listener.BufferSize, ReceiveCallback);
        }
    }

    /// <summary>
    /// 关闭连接并从监听器中移除
    /// </summary>
    public void Close()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        Socket.Close();
        State = TcpSessionStates.Closed;
        Listener.RemoveSession(this);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}