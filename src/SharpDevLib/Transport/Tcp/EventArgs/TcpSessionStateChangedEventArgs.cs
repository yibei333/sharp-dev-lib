namespace SharpDevLib;

/// <summary>
/// TCP会话状态变更事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <param name="session">TCP会话示例</param>
/// <param name="before">变更前的状态</param>
/// <param name="current">变更后的状态</param>
public class TcpSessionStateChangedEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, TcpSessionStates before, TcpSessionStates current) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 变更前的状态
    /// </summary>
    public TcpSessionStates Before { get; set; } = before;

    /// <summary>
    /// 变更后的状态
    /// </summary>
    public TcpSessionStates Current { get; set; } = current;
}