using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Transport;

/// <summary>
/// Udp扩展
/// </summary>
public static class UdpExtensions
{
    /// <summary>
    /// 添加Udp服务
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddUdpService(this IServiceCollection services)
    {
        services.AddSingleton<IUdpClientFactory, UdpClientFactory>();
        return services;
    }
}
