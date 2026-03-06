namespace SharpDevLib;

/// <summary>
/// Tcp客户端数据事件参数
/// </summary>
/// <remarks>
/// 实例化Tcp客户端数据事件参数
/// </remarks>
/// <param name="client">客户端</param>
/// <param name="bytes">字节数组</param>
public class TcpClientDataEventArgs(TcpClient client, byte[] bytes) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}