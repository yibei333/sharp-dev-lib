namespace SharpDevLib.Standard;

/// <summary>
/// Tcp监听器状态
/// </summary>
public enum TcpListnerStates
{
    /// <summary>
    /// 已创建
    /// </summary>
    Created = 1,
    /// <summary>
    /// 监听中
    /// </summary>
    Listening,
    /// <summary>
    /// 已关闭
    /// </summary>
    Closed,
}

/// <summary>
/// Tcp会话状态
/// </summary>
public enum TcpSessionStates
{
    /// <summary>
    /// 已连接
    /// </summary>
    Connected = 1,
    /// <summary>
    /// 已关闭
    /// </summary>
    Closed,
}
