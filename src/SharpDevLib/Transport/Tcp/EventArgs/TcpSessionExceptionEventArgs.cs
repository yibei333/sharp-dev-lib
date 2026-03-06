namespace SharpDevLib;

/// <summary>
/// Tcp会话异常事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">Tcp会话元数据类型</typeparam>
/// <remarks>
/// 实例化Tcp会话异常事件参数
/// </remarks>
/// <param name="session">会话</param>
/// <param name="exception">异常</param>
public class TcpSessionExceptionEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, Exception exception) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; } = exception;
}