namespace SharpDevLib;

/// <summary>
/// TCP客户端异常事件参数
/// </summary>
/// <param name="client">TCP客户端示例</param>
/// <param name="exception">发生的异常</param>
public class TcpClientExceptionEventArgs(TcpClient client, Exception exception) : TcpClientEventArgs(client)
{
    /// <summary>
    /// 发生的异常对象
    /// </summary>
    public Exception Exception { get; } = exception;
}