namespace SharpDevLib.Transport;

/// <summary>
/// Tcp客户端状态变更事件参数
/// </summary>
public class TcpClientStateChangedEventArgs : TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp客户端状态变更事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="before">之前状态</param>
    /// <param name="current">当前状态</param>
    public TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current) : base(client)
    {
        Before = before;
        Current = current;
    }

    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpClientStates Before { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpClientStates Current { get; set; }
}