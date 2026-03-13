namespace SharpDevLib;

/// <summary>
/// TCP会话事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <param name="session">TCP会话示例</param>
public class TcpSessionEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session)
{
    /// <summary>
    /// TCP会话示例
    /// </summary>
    public TcpSession<TSessionMetadata> Session { get; } = session;
}