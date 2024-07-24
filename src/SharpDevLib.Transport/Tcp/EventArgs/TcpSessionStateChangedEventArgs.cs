namespace SharpDevLib.Transport;

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