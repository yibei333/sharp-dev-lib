using System.Net;

namespace SharpDevLib.Transport;

/// <summary>
/// Udp客户端创建工厂
/// </summary>
public class UdpClientFactory : IUdpClientFactory
{
    /// <summary>
    /// 实例化Udp客户端创建工厂
    /// </summary>
    /// <param name="serviceProvider">serviceProvider(用于获取Logger)</param>
    public UdpClientFactory(IServiceProvider? serviceProvider = null)
    {
        ServiceProvider = serviceProvider;
    }

    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 创建Udp客户端
    /// </summary>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Udp客户端</returns>
    public UdpClient Create(TransportAdapterType adapterType = TransportAdapterType.Default) => new(ServiceProvider, adapterType);

    /// <summary>
    /// 创建Udp客户端
    /// </summary>
    /// <param name="localAdress">本地地址</param>
    /// <param name="localPort">本地端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Udp客户端</returns>
    public UdpClient Create(IPAddress localAdress, int localPort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(ServiceProvider, localAdress, localPort, adapterType);
}
