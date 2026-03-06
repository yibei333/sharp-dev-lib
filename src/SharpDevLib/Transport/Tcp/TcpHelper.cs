using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.NetworkInformation;

namespace SharpDevLib;

/// <summary>
/// Tcp扩展
/// </summary>
public static class TcpHelper
{
    public static ILogger? Logger { get; set; } = new SimpleConsoleLogger(nameof(TcpHelper));

    /// <summary>
    /// 获取可用的Tcp端口
    /// </summary>
    /// <param name="min">最小端口</param>
    /// <param name="max">最大端口</param>
    /// <returns>可用端口,如果为-1则表示获取失败</returns>
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
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    public static TcpClient CreateClient(IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(remoteAdress, remotePort, adapterType);

    /// <summary>
    /// 创建Tcp客户端
    /// </summary>
    /// <param name="localAdress">本地地址</param>
    /// <param name="localPort">本地端口</param>
    /// <param name="remoteAdress">远程地址</param>
    /// <param name="remotePort">远程端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>Tcp客户端</returns>
    public static TcpClient CreateClient(IPAddress localAdress, int localPort, IPAddress remoteAdress, int remotePort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(localAdress, localPort, remoteAdress, remotePort, adapterType);

    /// <summary>
    /// 创建Tcp监听器
    /// </summary>
    /// <typeparam name="TSessionMetadata">会话元数据类型(可以用来绑定会话的身份信息)</typeparam>
    /// <param name="address">地址</param>
    /// <param name="port">端口</param>
    /// <param name="initSessionMetadata">初始化会话元数据</param>
    /// <param name="adapterType">接收数据适配器类型</param>
    /// <returns>Tcp监听器</returns>
    public static TcpListener<TSessionMetadata> CreateListener<TSessionMetadata>(IPAddress address, int port, Func<TSessionMetadata> initSessionMetadata, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, initSessionMetadata, adapterType);

    /// <summary>
    /// 创建Tcp监听器
    /// </summary>
    /// <param name="address">地址</param>
    /// <param name="port">端口</param>
    /// <param name="adapterType">接收数据适配器类型</param>
    /// <returns>Tcp监听器</returns>
    public static TcpListener CreateListener(IPAddress address, int port, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, adapterType);
}
