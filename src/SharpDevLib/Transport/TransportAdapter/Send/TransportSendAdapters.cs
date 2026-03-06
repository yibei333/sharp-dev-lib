namespace SharpDevLib;

/// <summary>
/// 内置的传输发送适配器
/// </summary>
public static class TransportSendAdapters
{
    /// <summary>
    /// 固定头发送适配器（前四个字节为数据长度，无粘包问题）
    /// </summary>
    public static ITransportSendAdapter FixedHeader = new TransportFixedHeaderSendAdapter();

    /// <summary>
    /// 默认发送适配器（直接发送数据，有粘包问题）
    /// </summary>
    public static ITransportSendAdapter Default = new TransportDefaultSendAdapter();
}