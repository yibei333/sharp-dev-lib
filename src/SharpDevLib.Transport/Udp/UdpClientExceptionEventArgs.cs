namespace SharpDevLib.Transport;

/// <summary>
/// Udp客户端异常事件参数
/// </summary>
public class UdpClientExceptionEventArgs : UdpClientEventArgs
{
    /// <summary>
    /// 实例化Udp客户端异常事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="exception">异常</param>
    public UdpClientExceptionEventArgs(UdpClient client, Exception exception) : base(client)
    {
        Exception = exception;
    }

    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; }
}