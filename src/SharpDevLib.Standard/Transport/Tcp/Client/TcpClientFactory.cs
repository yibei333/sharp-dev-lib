using System.Net;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp客户端创建工厂
/// </summary>
public class TcpClientFactory : ITcpClientFactory
{
    /// <summary>
    /// 实例化Tcp客户端创建工厂
    /// </summary>
    /// <param name="serviceProvider">serviceProvider(用于获取Logger)</param>
    public TcpClientFactory(IServiceProvider? serviceProvider = null)
    {
        ServiceProvider = serviceProvider;
    }

    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    public TcpClient Create(IPAddress remoteAdress, int remotePort, TcpAdapterType adapterType = TcpAdapterType.Default) => new(ServiceProvider, remoteAdress, remotePort, adapterType);

    /// <summary>
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="localAdress">本地地址</param>
    /// <param name="localPort">本地端口</param>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    public TcpClient Create(IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TcpAdapterType adapterType = TcpAdapterType.Default) => new(ServiceProvider, localAdress, localPort, remoteAdress, remotePort, adapterType);
}
