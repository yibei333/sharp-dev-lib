namespace SharpDevLib.Transport;

/// <summary>
/// Udp客户端数据事件参数
/// </summary>
public class UdpClientDataEventArgs : UdpClientEventArgs
{
    /// <summary>
    /// 实例化Udp客户端数据事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="bytes">字节数组</param>
    public UdpClientDataEventArgs(UdpClient client, byte[] bytes) : base(client)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; }
}