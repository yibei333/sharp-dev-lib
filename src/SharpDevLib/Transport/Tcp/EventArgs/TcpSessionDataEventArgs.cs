namespace SharpDevLib;

/// <summary>
/// Tcp会话数据事件参数
/// </summary>
/// <typeparam name="TSessionMetadata">Tcp会话元数据类型</typeparam>
/// <remarks>
/// 实例化Tcp接收事件参数
/// </remarks>
/// <param name="session">会话</param>
/// <param name="bytes">字节数组</param>
public class TcpSessionDataEventArgs<TSessionMetadata>(TcpSession<TSessionMetadata> session, byte[] bytes) : TcpSessionEventArgs<TSessionMetadata>(session)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}