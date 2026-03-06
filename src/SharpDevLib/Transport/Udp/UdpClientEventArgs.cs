using System.Net;

namespace SharpDevLib;

/// <summary>
/// Udp客户端事件参数
/// </summary>
/// <remarks>
/// 实例化Udp会话事件
/// </remarks>
/// <param name="client">客户端</param>
public class UdpClientEventArgs(UdpClient client)
{
    /// <summary>
    /// 客户端
    /// </summary>
    public UdpClient Client { get; } = client;

    /// <summary>
    /// 远程终结点
    /// </summary>
    public EndPoint? RemoteEndPoint { get; internal set; }
}