namespace SharpDevLib.Standard;

/// <summary>
/// 内置的传输接收适配器
/// </summary>
public static class TransportReceiveAdapters
{
    /// <summary>
    /// 传输固定头接收适配器(每次发送消息需要在前四个字节中放入字节的长度总和,没有粘包问题)
    /// </summary>
    public static ITransportReceiveAdapter FixedHeader = new TransportFixedHeaderReceiveAdapter();

    /// <summary>
    /// 传输默认接收适配器(每次按照最大64KB字节获取数据,有粘包问题)
    /// </summary>
    public static ITransportReceiveAdapter Default = new TransportDefaultReceiveAdapter();
}