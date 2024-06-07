namespace SharpDevLib.Transport;

/// <summary>
/// Tcp客户端事件参数
/// </summary>
public class TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp会话事件
    /// </summary>
    /// <param name="client">客户端</param>
    public TcpClientEventArgs(TcpClient client)
    {
        Client = client;
    }

    /// <summary>
    /// 客户端
    /// </summary>
    public TcpClient Client { get; }
}

/// <summary>
/// Tcp客户端状态变更事件参数
/// </summary>
public class TcpClientStateChangedEventArgs : TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp客户端状态变更事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="before">之前状态</param>
    /// <param name="current">当前状态</param>
    public TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current) : base(client)
    {
        Before = before;
        Current = current;
    }

    /// <summary>
    /// 之前状态
    /// </summary>
    public TcpClientStates Before { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    public TcpClientStates Current { get; set; }
}

/// <summary>
/// Tcp客户端数据事件参数
/// </summary>
public class TcpClientDataEventArgs : TcpClientEventArgs
{
    /// <summary>
    /// 实例化Tcp客户端数据事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="bytes">字节数组</param>
    public TcpClientDataEventArgs(TcpClient client, byte[] bytes) : base(client)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; }
}

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