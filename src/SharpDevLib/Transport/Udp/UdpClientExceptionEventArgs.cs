namespace SharpDevLib;

/// <summary>
/// UDP客户端异常事件参数
/// </summary>
/// <param name="client">UDP客户端实例</param>
/// <param name="exception">发生的异常</param>
public class UdpClientExceptionEventArgs(UdpClient client, Exception exception) : UdpClientEventArgs(client)
{
    /// <summary>
    /// 发生的异常对象
    /// </summary>
    public Exception Exception { get; } = exception;
}