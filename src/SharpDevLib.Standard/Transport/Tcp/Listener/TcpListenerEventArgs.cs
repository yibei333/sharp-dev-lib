namespace SharpDevLib.Standard;

/// <summary>
/// Tcp监听器状态变更事件参数
/// </summary>
public class TcpListenerStateChangedEventArgs
{
    /// <summary>
    /// 实例化Tcp监听器状态变更事件参数
    /// </summary>
    /// <param name="before">之前状态</param>
    /// <param name="current">当前状态</param>
    public TcpListenerStateChangedEventArgs(TcpListnerStates before, TcpListnerStates current)
    {
        Before = before;
        Current = current;
    }

    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpListnerStates Before { get; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpListnerStates Current { get; }
}

/// <summary>
/// Tcp会话事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
public class TcpSessionEventArgs<TSessionMetadata>
{
    /// <summary>
    /// 实例化Tcp会话事件
    /// </summary>
    /// <param name="session"></param>
    public TcpSessionEventArgs(TcpSession<TSessionMetadata> session)
    {
        Session = session;
    }

    /// <summary>
    /// 会话
    /// </summary>
    public TcpSession<TSessionMetadata> Session { get; }
}

/// <summary>
/// Tcp会话状态变更事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
public class TcpSessionStateChangedEventArgs<TSessionMetadata> : TcpSessionEventArgs<TSessionMetadata>
{
    /// <summary>
    /// 实例化Tcp会话状态变更事件参数
    /// </summary>
    /// <param name="session">会话</param>
    /// <param name="before">之前状态</param>
    /// <param name="current">当前状态</param>
    public TcpSessionStateChangedEventArgs(TcpSession<TSessionMetadata> session, TcpSessionStates before, TcpSessionStates current) : base(session)
    {
        Before = before;
        Current = current;
    }

    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpSessionStates Before { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpSessionStates Current { get; set; }
}

/// <summary>
/// Tcp会话数据事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">Tcp会话元数据类型</typeparam>
public class TcpSessionDataEventArgs<TSessionMetadata> : TcpSessionEventArgs<TSessionMetadata>
{
    /// <summary>
    /// 实例化Tcp接收事件参数
    /// </summary>
    /// <param name="session">会话</param>
    /// <param name="bytes">字节数组</param>
    public TcpSessionDataEventArgs(TcpSession<TSessionMetadata> session, byte[] bytes) : base(session)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; }
}

/// <summary>
/// Tcp会话异常事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">Tcp会话元数据类型</typeparam>
public class TcpSessionExceptionEventArgs<TSessionMetadata> : TcpSessionEventArgs<TSessionMetadata>
{
    /// <summary>
    /// 实例化Tcp会话异常事件参数
    /// </summary>
    /// <param name="session">会话</param>
    /// <param name="exception">异常</param>
    public TcpSessionExceptionEventArgs(TcpSession<TSessionMetadata> session, Exception exception) : base(session)
    {
        Exception = exception;
    }

    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; }
}