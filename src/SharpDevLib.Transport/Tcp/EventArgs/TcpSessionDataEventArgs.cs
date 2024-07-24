namespace SharpDevLib.Transport;

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