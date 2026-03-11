using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// TCP会话
/// </summary>
/// <typeparam name="TMetadata">会话元数据类型</typeparam>
public class TcpSession<TMetadata> : IDisposable
{
    TcpSessionStates _state = 0;
    bool _isDisposed;

    internal TcpSession(TcpListener<TMetadata> listener, Socket socket)
    {
        Listener = listener;
        Socket = socket;
        State = TcpSessionStates.Connected;
        ReceiveAdapter = Listener.AdapterType.GetReceiveAdapter(listener.ReceiveAdapter);
        SendAdapter = Listener.AdapterType.GetSendAdapter(listener.SendAdapter);
    }

    ITransportReceiveAdapter ReceiveAdapter { get; }
    ITransportSendAdapter SendAdapter { get; }

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
    public TMetadata? Metadata { get; }

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
            StateChanged?.Invoke(this, new TcpSessionStateChangedEventArgs<TMetadata>(this, before, _state));
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

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="bytes">要发送的字节数组</param>
    /// <param name="throwIfException">发送失败是否抛出异常，默认false，可订阅Error事件</param>
    public void Send(byte[] bytes, bool throwIfException = false)
    {
        try
        {
            if (State != TcpSessionStates.Connected || !Socket.Connected) throw new Exception("无法访问已关闭的TCP会话");
            SendAdapter.Send(Socket, bytes);
            Sended?.Invoke(this, new TcpSessionDataEventArgs<TMetadata>(this, bytes));
        }
        catch (SocketException ex)
        {
            Close();
            Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetadata>(this, ex));
            if (throwIfException) throw ex;
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetadata>(this, ex));
            if (throwIfException) throw ex;
        }
    }

    internal void Receive()
    {
        while (true)
        {
            if (State != TcpSessionStates.Connected || !Socket.Connected) break;
            if (_isDisposed) break;

            try
            {
                var bytes = ReceiveAdapter.Receive(Socket);
                if (bytes.IsNullOrEmpty())
                {
                    Close();
                    break;
                }
                Received?.Invoke(this, new TcpSessionDataEventArgs<TMetadata>(this, bytes));
            }
            catch (SocketException ex)
            {
                Close();
                Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetadata>(this, ex));
                break;
            }
            catch (Exception ex)
            {
                if (_isDisposed) break;
                Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetadata>(this, ex));
            }
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