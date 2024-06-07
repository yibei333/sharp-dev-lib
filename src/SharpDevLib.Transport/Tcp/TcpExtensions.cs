using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Transport;

/// <summary>
/// Tcp扩展
/// </summary>
public static class TcpExtensions
{
    /// <summary>
    /// 添加Tcp服务
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddTcp(this IServiceCollection services)
    {
        services.AddSingleton<ITcpListenerFactory, TcpListenerFactory>();
        services.AddSingleton<ITcpClientFactory, TcpClientFactory>();
        return services;
    }
}
