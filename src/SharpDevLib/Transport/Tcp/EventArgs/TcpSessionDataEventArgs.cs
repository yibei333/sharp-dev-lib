namespace SharpDevLib;

/// <summary>
/// TCP会话数据事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">会话元数据类型</typeparam>
/// <param name="session">TCP会话示例</param>
/// <param name="bytes">接收到的字节数组</param>
public class TcpSessionDataEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, byte[] bytes) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}