using System.Net;

namespace SharpDevLib.Transport;

/// <summary>
/// Udp客户端事件参数
/// </summary>
public class UdpClientEventArgs
{
    /// <summary>
    /// 实例化Udp会话事件
    /// </summary>
    /// <param name="client">客户端</param>
    public UdpClientEventArgs(UdpClient client)
    {
        Client = client;
    }

    /// <summary>
    /// 客户端
    /// </summary>
    public UdpClient Client { get; }

    /// <summary>
    /// 远程终结点
    /// </summary>
    public EndPoint? RemoteEndPoint { get; internal set; }
}

/// <summary>
/// Udp客户端数据事件参数
/// </summary>
public class UdpClientDataEventArgs : UdpClientEventArgs
{
    /// <summary>
    /// 实例化Udp客户端数据事件参数
    /// </summary>
    /// <param name="client">客户端</param>
    /// <param name="bytes">字节数组</param>
    public UdpClientDataEventArgs(UdpClient client, byte[] bytes) : base(client)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[] Bytes { get; }
}

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