using System.Net;

namespace SharpDevLib.Transport;

/// <summary>
/// Tcp客户端创建工厂
/// </summary>
public interface ITcpClientFactory
{
    /// <summary>
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    TcpClient Create(IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default);

    /// <summary>
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="localAdress">本地地址</param>
    /// <param name="localPort">本地端口</param>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    TcpClient Create(IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default);
}
