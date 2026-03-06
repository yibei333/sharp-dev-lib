namespace SharpDevLib;

/// <summary>
/// TCP客户端状态变更事件参数
/// </summary>
/// <param name="client">TCP客户端实例</param>
/// <param name="before">变更前的状态</param>
/// <param name="current">变更后的状态</param>
public class TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 变更前的状态
    /// </summary>
    public TcpClientStates Before { get; set; } = before;

    /// <summary>
    /// 变更后的状态
    /// </summary>
    public TcpClientStates Current { get; set; } = current;
}