namespace SharpDevLib;

/// <summary>
/// TCP监听器状态变更事件参数
/// </summary>
/// <param name="before">变更前的状态</param>
/// <param name="current">变更后的状态</param>
public class TcpListenerStateChangedEventArgs(TcpListnerStates before, TcpListnerStates current)
{
    /// <summary>
    /// 变更前的状态
    /// </summary>
    public TcpListnerStates Before { get; } = before;

    /// <summary>
    /// 变更后的状态
    /// </summary>
    public TcpListnerStates Current { get; } = current;
}