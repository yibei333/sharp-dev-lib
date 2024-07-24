namespace SharpDevLib.Transport;

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