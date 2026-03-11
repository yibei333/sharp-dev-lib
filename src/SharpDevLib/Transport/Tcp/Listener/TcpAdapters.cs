namespace SharpDevLib;

/// <summary>
/// 内置的传输接收适配器
/// </summary>
public static class TcpAdapters
{
    /// <summary>
    /// 固定头接收适配器（前四个字节为数据长度，无粘包问题）
    /// </summary>
    public static ITcpAdapter FixedHeader { get; } = new TcpFixedHeaderAdapter();

    /// <summary>
    /// 默认接收适配器（有粘包问题）
    /// </summary>
    public static ITcpAdapter Default { get; } = new TcpDefaultAdapter();
}