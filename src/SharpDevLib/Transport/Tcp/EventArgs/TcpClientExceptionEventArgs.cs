namespace SharpDevLib;

/// <summary>
/// Tcp客户端异常事件参数
/// </summary>
/// <remarks>
/// 实例化Tcp客户端异常事件参数
/// </remarks>
/// <param name="client">客户端</param>
/// <param name="exception">异常</param>
public class TcpClientExceptionEventArgs(TcpClient client, Exception exception) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; } = exception;
}