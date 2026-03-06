namespace SharpDevLib;

/// <summary>
/// Udp客户端异常事件参数
/// </summary>
/// <remarks>
/// 实例化Udp客户端异常事件参数
/// </remarks>
/// <param name="client">客户端</param>
/// <param name="exception">异常</param>
public class UdpClientExceptionEventArgs(UdpClient client, Exception exception) : UdpClientEventArgs(client)
{
    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; } = exception;
}