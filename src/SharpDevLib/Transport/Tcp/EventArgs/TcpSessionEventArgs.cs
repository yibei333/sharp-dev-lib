namespace SharpDevLib;

/// <summary>
/// Tcp会话事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <remarks>
/// 实例化Tcp会话事件
/// </remarks>
/// <param name="session"></param>
public class TcpSessionEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session)
{
    /// <summary>
    /// 会话
    /// </summary>
    public TcpSession<TSessionMetadata> Session { get; } = session;
}