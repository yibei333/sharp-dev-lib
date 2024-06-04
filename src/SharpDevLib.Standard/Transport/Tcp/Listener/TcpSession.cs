using System.Net.Sockets;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp会话
/// </summary>
/// <typeparam name="TMetaData">元数据</typeparam>
public class TcpSession<TMetaData> : IDisposable
{
    TcpSessionStates _state = 0;

    internal TcpSession(TcpListener<TMetaData> listener, Socket socket)
    {
        Listener = listener;
        Socket = socket;
        State = TcpSessionStates.Connected;
        ReceiveAdapter = Listener.AdapterType.GetReceiveAdapter(listener.ReceiveAdapter);
        SendAdapter = Listener.AdapterType.GetSendAdapter(listener.SendAdapter);
    }

    ITcpReceiveAdapter ReceiveAdapter { get; }
    ITcpSendAdapter SendAdapter { get; }

    /// <summary>
    /// 套接字
    /// </summary>
    public Socket Socket { get; }

    /// <summary>
    /// 所属监听器
    /// </summary>
    public TcpListener<TMetaData> Listener { get; }

    /// <summary>
    /// 元数据
    /// </summary>
    public TMetaData? MetaData { get; }

    /// <summary>
    /// 状态
    /// </summary>
    public TcpSessionStates State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            var before = _state;
            _state = value;
            StateChanged?.Invoke(this, new TcpSessionStateChangedEventArgs<TMetaData>(this, before, _state));
        }
    }

    /// <summary>
    /// 状态变更回调事件
    /// </summary>
    public event EventHandler<TcpSessionStateChangedEventArgs<TMetaData>>? StateChanged;

    /// <summary>
    /// 接收事件
    /// </summary>
    public event EventHandler<TcpSessionDataEventArgs<TMetaData>>? Received;

    /// <summary>
    /// 发送事件
    /// </summary>
    public event EventHandler<TcpSessionDataEventArgs<TMetaData>>? Sended;

    /// <summary>
    /// 异常事件
    /// </summary>
    public event EventHandler<TcpSessionExceptionEventArgs<TMetaData>>? Error;

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="throwIfException">发送失败是否抛出异常,默认false,可以订阅Error事件</param>
    public void Send(byte[] bytes, bool throwIfException = false)
    {
        try
        {
            if (State != TcpSessionStates.Connected || !Socket.Connected) throw new Exception("can not access a closed tcp session");
            SendAdapter.Send(Socket, bytes);
            Sended?.Invoke(this, new TcpSessionDataEventArgs<TMetaData>(this, bytes));
        }
        catch (SocketException ex)
        {
            Close();
            Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetaData>(this, ex));
            if (throwIfException) throw ex;
        }
        catch (Exception ex)
        {
            Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetaData>(this, ex));
            if (throwIfException) throw ex;
        }
    }

    internal void Receive()
    {
        while (true)
        {
            if (State != TcpSessionStates.Connected || !Socket.Connected) break;

            try
            {
                var bytes = ReceiveAdapter.Receive(Socket);
                if (bytes.IsNullOrEmpty())
                {
                    Close();
                    break;
                }
                Received?.Invoke(this, new TcpSessionDataEventArgs<TMetaData>(this, bytes));
            }
            catch (SocketException ex)
            {
                Close();
                Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetaData>(this, ex));
                break;
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, new TcpSessionExceptionEventArgs<TMetaData>(this, ex));
            }
        }
    }

    /// <summary>
    /// 关闭并释放连接
    /// </summary>
    public void Close()
    {
        Socket.Close();
        State = TcpSessionStates.Closed;
        Listener.RemoveSession(this);
    }

    /// <summary>
    /// dispose
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}