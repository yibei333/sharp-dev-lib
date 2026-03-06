namespace SharpDevLib;

/// <summary>
/// Tcp监听器状态变更事件参数
/// </summary>
/// <remarks>
/// 实例化Tcp监听器状态变更事件参数
/// </remarks>
/// <param name="before">之前状态</param>
/// <param name="current">当前状态</param>
public class TcpListenerStateChangedEventArgs(TcpListnerStates before, TcpListnerStates current)
{
    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpListnerStates Before { get; } = before;

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpListnerStates Current { get; } = current;
}