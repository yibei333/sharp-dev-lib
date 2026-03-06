namespace SharpDevLib;

/// <summary>
/// Tcp客户端事件参数
/// </summary>
/// <remarks>
/// 实例化Tcp会话事件
/// </remarks>
/// <param name="client">客户端</param>
public class TcpClientEventArgs(TcpClient client)
{
    /// <summary>
    /// 客户端
    /// </summary>
    public TcpClient Client { get; } = client;
}