namespace SharpDevLib.Transport;

/// <summary>
/// Tcp客户端事件参数
/// </summary>
public class TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp会话事件
    /// </summary>
    /// <param name="client">客户端</param>
    public TcpClientEventArgs(TcpClient client)
    {
        Client = client;
    }

    /// <summary>
    /// 客户端
    /// </summary>
    public TcpClient Client { get; }
}