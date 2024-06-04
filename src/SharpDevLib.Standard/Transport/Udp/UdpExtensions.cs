using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Standard;

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
    public static IServiceCollection AddUdp(this IServiceCollection services)
    {
        services.AddSingleton<IUdpClientFactory, UdpClientFactory>();
        return services;
    }
}
