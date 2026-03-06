using System.Net;
using System.Net.NetworkInformation;

namespace SharpDevLib;

/// <summary>
/// Udp扩展
/// </summary>
public static class UdpHelper
{
    /// <summary>
    /// 获取可用的UDP端口
    /// </summary>
    /// <param name="min">最小端口</param>
    /// <param name="max">最大端口</param>
    /// <returns>可用端口，如果为-1则表示范围内无可用端口</returns>
    public static int GetAvailableUdpPort(int min = IPEndPoint.MinPort, int max = IPEndPoint.MaxPort)
    {
        if (min < IPEndPoint.MinPort || min > IPEndPoint.MaxPort) return -1;
        if (max < IPEndPoint.MinPort || max > IPEndPoint.MaxPort) return -1;
        if (min > max) return -1;

        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var ipPoints = ipGlobalProperties.GetActiveUdpListeners();
        var endPoints = ipPoints.Where(x => x.Port >= min && x.Port <= max).ToList();
        for (int i = min; i <= max; i++)
        {
            if (endPoints.All(x => x.Port != i)) return i;
        }
        return -1;
    }

    /// <summary>
    /// 创建UDP客户端
    /// </summary>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>UDP客户端</returns>
    public static UdpClient CreateClient(TransportAdapterType adapterType = TransportAdapterType.Default) => new(adapterType);

    /// <summary>
    /// 创建UDP客户端（指定本地绑定地址）
    /// </summary>
    /// <param name="localAdress">本地绑定IP地址</param>
    /// <param name="localPort">本地绑定端口</param>
    /// <param name="adapterType">收发适配器类型</param>
    /// <returns>UDP客户端</returns>
    public static UdpClient CreateClient(IPAddress localAdress, int localPort, TransportAdapterType adapterType = TransportAdapterType.Default) => new(localAdress, localPort, adapterType);
}
