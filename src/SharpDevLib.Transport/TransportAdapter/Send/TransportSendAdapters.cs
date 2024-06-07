namespace SharpDevLib.Transport;

/// <summary>
/// 内置的传输发送适配器
/// </summary>
public static class TransportSendAdapters
{
    /// <summary>
    /// 传输固定头发送适配器(每次发送消息在前四个字节中放入字节的长度总和,没有粘包问题)
    /// </summary>
    public static ITransportSendAdapter FixedHeader = new TransportFixedHeaderSendAdapter();

    /// <summary>
    /// 传输默认发送适配器
    /// </summary>
    public static ITransportSendAdapter Default = new TransportDefaultSendAdapter();
}