namespace SharpDevLib.Transport;

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