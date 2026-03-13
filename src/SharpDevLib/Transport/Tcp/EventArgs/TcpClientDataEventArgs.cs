namespace SharpDevLib;

/// <summary>
/// TCP客户端数据事件参数
/// </summary>
/// <param name="client">TCP客户端示例</param>
/// <param name="bytes">接收到的字节数组</param>
public class TcpClientDataEventArgs(TcpClient client, byte[] bytes) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; } = bytes;
}