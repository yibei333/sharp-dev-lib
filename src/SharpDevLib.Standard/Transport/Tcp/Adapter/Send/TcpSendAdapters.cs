namespace SharpDevLib.Standard;

/// <summary>
/// 内置的Tcp发送适配器
/// </summary>
public static class TcpSendAdapters
{
    /// <summary>
    /// Tcp固定头发送适配器(每次发送消息在前四个字节中放入字节的长度总和,没有粘包问题)
    /// </summary>
    public static ITcpSendAdapter FixedHeader = new TcpFixedHeaderSendAdapter();

    /// <summary>
    /// Tcp默认发送适配器
    /// </summary>
    public static ITcpSendAdapter Default = new TcpDefaultSendAdapter();
}