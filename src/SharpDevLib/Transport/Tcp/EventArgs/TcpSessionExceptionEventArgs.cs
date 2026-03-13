namespace SharpDevLib;

/// <summary>
/// TCP会话异常事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <param name="session">TCP会话示例</param>
/// <param name="exception">发生的异常</param>
public class TcpSessionExceptionEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, Exception exception) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 发生的异常对象
    /// </summary>
    public Exception Exception { get; } = exception;
}