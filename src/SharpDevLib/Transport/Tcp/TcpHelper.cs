using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.NetworkInformation;

namespace SharpDevLib;

/// <summary>
/// TCP扩展，提供TCP客户端和监听器的创建以及端口管理功能
/// </summary>
public static class TcpHelper
{
    /// <summary>
    /// 获取或设置日志记录器
    /// </summary>
    public static ILogger? Logger { get; set; } = new SimpleConsoleLogger(nameof(TcpHelper));

    /// <summary>
    /// 获取可用的TCP端口
    /// </summary>
    /// <param name="min">最小端口，默认为IPEndPoint.MinPort</param>
    /// <param name="max">最大端口，默认为IPEndPoint.MaxPort</param>
    /// <returns>可用端口，如果为-1则表示范围内无可用端口</returns>
    public static int GetAvailableTcpPort(int min = IPEndPoint.MinPort, int max = IPEndPoint.MaxPort)
    {
        if (min < IPEndPoint.MinPort || min > IPEndPoint.MaxPort) return -1;
        if (max < IPEndPoint.MinPort || max > IPEndPoint.MaxPort) return -1;
        if (min > max) return -1;

        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var ipPoints = ipGlobalProperties.GetActiveTcpListeners();
        var endPoints = ipPoints.Where(x => x.Port >= min && x.Port <= max).ToList();
        for (int i = min; i <= max; i++)
        {
            if (endPoints.All(x => x.Port != i)) return i;
        }
        return -1;
    }

    /// <summary>
    /// 创建TCP客户端
    /// </summary>
    /// <param name="remoteAdress">远程服务器IP地址</param>
    /// <param name="remotePort">远程服务器端口</param>
    /// <param name="adapterType">收发适配器类型，默认为Default</param>
    /// <returns>TCP客户端</returns>
    public static TcpClient CreateClient(IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(remoteAdress, remotePort, adapterType);

    /// <summary>
    /// 创建TCP客户端（指定本地和远程地址）
    /// </summary>
    /// <param name="localAdress">本地绑定IP地址</param>
    /// <param name="localPort">本地绑定端口</param>
    /// <param name="remoteAdress">远程服务器IP地址</param>
    /// <param name="remotePort">远程服务器端口</param>
    /// <param name="adapterType">收发适配器类型，默认为Default</param>
    /// <returns>TCP客户端</returns>
    public static TcpClient CreateClient(IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(localAdress, localPort, remoteAdress, remotePort, adapterType);

    /// <summary>
    /// 创建TCP监听器（泛型版本，支持会话元数据）
    /// </summary>
    /// <typeparam name="TSessionMetadata">会话元数据类型（可以用来绑定会话的身份信息）</typeparam>
    /// <param name="address">监听IP地址</param>
    /// <param name="port">监听端口</param>
    /// <param name="adapterType">接收数据适配器类型，默认为Default</param>
    /// <returns>TCP监听器</returns>
    public static TcpListener<TSessionMetadata> CreateListener<TSessionMetadata>(IPAddress address, int port, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, adapterType);

    /// <summary>
    /// 创建TCP监听器（非泛型版本）
    /// </summary>
    /// <param name="address">监听IP地址</param>
    /// <param name="port">监听端口</param>
    /// <param name="adapterType">接收数据适配器类型，默认为Default</param>
    /// <returns>TCP监听器</returns>
    public static TcpListener CreateListener(IPAddress address, int port, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, adapterType);
}
