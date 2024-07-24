namespace SharpDevLib.Transport.Tcp.EventArgs;

/// <summary>
/// Tcp客户端异常事件参数
/// </summary>
public class TcpClientExceptionEventArgs : TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp客户端异常事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="exception">异常</param>
    public TcpClientExceptionEventArgs(TcpClient client, Exception exception) : base(client)
    {
        Exception = exception;
    }

    /// <summary>
    /// 异常
    /// </summary>
    public Exception Exception { get; }
}