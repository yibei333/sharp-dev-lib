namespace SharpDevLib.Standard;

/// <summary>
/// Tcp客户端状态
/// </summary>
public enum TcpClientStates
{
    /// <summary>
    /// 已创建
    /// </summary>
    Created = 1,
    /// <summary>
    /// 已连接
    /// </summary>
    Connected,
    /// <summary>
    /// 已关闭
    /// </summary>
    Closed,
}
