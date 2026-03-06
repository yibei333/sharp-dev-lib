namespace SharpDevLib;

/// <summary>
/// UDP客户端数据事件参数
/// </summary>
/// <param name="client">UDP客户端实例</param>
/// <param name="bytes">接收到的字节数组</param>
public class UdpClientDataEventArgs(UdpClient client, byte[] bytes) : UdpClientEventArgs(client)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}