namespace SharpDevLib;

/// <summary>
/// Tcp客户端状态变更事件参数
/// </summary>
/// <remarks>
/// 实例化Tcp客户端状态变更事件参数
/// </remarks>
/// <param name="client">客户端</param>
/// <param name="before">之前状态</param>
/// <param name="current">当前状态</param>
public class TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpClientStates Before { get; set; } = before;

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpClientStates Current { get; set; } = current;
}