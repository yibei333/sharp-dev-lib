using System.Net;

namespace SharpDevLib;

/// <summary>
/// UDP客户端事件参数
/// </summary>
/// <param name="client">UDP客户端实例</param>
public class UdpClientEventArgs(UdpClient client)
{
    /// <summary>
    /// UDP客户端实例
    /// </summary>
    public UdpClient Client { get; } = client;

    /// <summary>
    /// 远程端点
    /// </summary>
    public IPEndPoint? RemoteEndPoint { get; internal set; }
}