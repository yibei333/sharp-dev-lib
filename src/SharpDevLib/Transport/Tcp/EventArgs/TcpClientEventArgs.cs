namespace SharpDevLib;

/// <summary>
/// TCP客户端事件参数
/// </summary>
/// <param name="client">TCP客户端示例</param>
public class TcpClientEventArgs(TcpClient client)
{
    /// <summary>
    /// TCP客户端示例
    /// </summary>
    public TcpClient Client { get; } = client;
}