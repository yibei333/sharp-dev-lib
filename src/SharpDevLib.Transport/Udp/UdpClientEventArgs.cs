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