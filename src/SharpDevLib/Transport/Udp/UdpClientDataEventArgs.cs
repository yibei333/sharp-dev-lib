namespace SharpDevLib;

/// <summary>
/// Udp客户端数据事件参数
/// </summary>
/// <remarks>
/// 实例化Udp客户端数据事件参数
/// </remarks>
/// <param name="client">客户端</param>
/// <param name="bytes">字节数组</param>
public class UdpClientDataEventArgs(UdpClient client, byte[] bytes) : UdpClientEventArgs(client)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}