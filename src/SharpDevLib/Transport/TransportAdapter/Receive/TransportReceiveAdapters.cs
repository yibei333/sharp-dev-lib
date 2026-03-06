namespace SharpDevLib;

/// <summary>
/// 内置的传输接收适配器
/// </summary>
public static class TransportReceiveAdapters
{
    /// <summary>
    /// 固定头接收适配器（前四个字节为数据长度，无粘包问题）
    /// </summary>
    public static ITransportReceiveAdapter FixedHeader = new TransportFixedHeaderReceiveAdapter();

    /// <summary>
    /// 默认接收适配器（每次按照最大64KB字节获取数据，有粘包问题）
    /// </summary>
    public static ITransportReceiveAdapter Default = new TransportDefaultReceiveAdapter();
}