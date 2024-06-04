using System.Net;

namespace SharpDevLib.Standard;

/// <summary>
/// Udp客户端创建工厂
/// </summary>
public interface IUdpClientFactory
{
    /// <summary>
    /// 创建Udp客户端
    /// </summary>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Udp客户端</returns>
    UdpClient Create(TransportAdapterType adapterType = TransportAdapterType.Default);

    /// <summary>
    /// 创建Udp客户端
    /// </summary>
    /// <param name="localAdress">本地地址</param>
    /// <param name="localPort">本地端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Udp客户端</returns>
    UdpClient Create(IPAddress localAdress, int localPort, TransportAdapterType adapterType = TransportAdapterType.Default);
}
