namespace SharpDevLib.Transport;

/// <summary>
/// Tcp监听器状态变更事件参数
/// </summary>
public class TcpListenerStateChangedEventArgs
{
    /// <summary>
    /// 实例化Tcp监听器状态变更事件参数
    /// </summary>
    /// <param name="before">之前状态</param>
    /// <param name="current">当前状态</param>
    public TcpListenerStateChangedEventArgs(TcpListnerStates before, TcpListnerStates current)
    {
        Before = before;
        Current = current;
    }

    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpListnerStates Before { get; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpListnerStates Current { get; }
}