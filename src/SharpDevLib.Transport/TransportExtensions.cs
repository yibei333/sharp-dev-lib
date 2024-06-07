using System.Net;
using System.Net.NetworkInformation;

namespace SharpDevLib.Transport;

/// <summary>
/// 传输扩展
/// </summary>
public static class TransportExtensions
{
    /// <summary>
    /// 获取可用的Tcp端口
    /// </summary>
    /// <param name="min">最小端口</param>
    /// <param name="max">最大端口</param>
    /// <returns>可用端口,如果为-1则表示获取失败</returns>
    public static int GetAvailableTcpPort(int min = IPEndPoint.MinPort, int max = IPEndPoint.MaxPort) => GetAvailableUdpPort(1, min, max);

    /// <summary>
    /// 获取可用的Udp端口
    /// </summary>
    /// <param name="min">最小端口</param>
    /// <param name="max">最大端口</param>
    /// <returns>可用端口,如果为-1则表示获取失败</returns>
    public static int GetAvailableUdpPort(int min = IPEndPoint.MinPort, int max = IPEndPoint.MaxPort) => GetAvailableUdpPort(2, min, max);

    static int GetAvailableUdpPort(int type, int min = IPEndPoint.MinPort, int max = IPEndPoint.MaxPort)
    {
        if (min < IPEndPoint.MinPort || min > IPEndPoint.MaxPort) return -1;
        if (max < IPEndPoint.MinPort || max > IPEndPoint.MaxPort) return -1;
        if (min > max) return -1;

        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var ipPoints = type == 1 ? ipGlobalProperties.GetActiveTcpListeners() : ipGlobalProperties.GetActiveUdpListeners();
        var endPoints = ipPoints.Where(x => x.Port >= min && x.Port <= max).ToList();
        for (int i = min; i <= max; i++)
        {
            if (endPoints.All(x => x.Port != i)) return i;
        }
        return -1;
    }
}
