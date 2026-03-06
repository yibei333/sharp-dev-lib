namespace SharpDevLib;

/// <summary>
/// Tcp会话状态变更事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <remarks>
/// 实例化Tcp会话状态变更事件参数
/// </remarks>
/// <param name="session">会话</param>
/// <param name="before">之前状态</param>
/// <param name="current">当前状态</param>
public class TcpSessionStateChangedEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, TcpSessionStates before, TcpSessionStates current) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpSessionStates Before { get; set; } = before;

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpSessionStates Current { get; set; } = current;
}