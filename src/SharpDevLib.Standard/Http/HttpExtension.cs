using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Standard;

/// <summary>
/// http扩展
/// </summary>
public static class HttpExtension
{
    ///// <summary>
    ///// add http service
    ///// </summary>
    ///// <param name="services">service collection</param>
    ///// <param name="configuration">configuration</param>
    ///// <returns>service collection</returns>
    //public static IServiceCollection AddHttp(this IServiceCollection services, IConfiguration? configuration = null)
    //{
    //    if (configuration.NotNull())
    //    {
    //        services.Configure<HttpGlobalSettings>(configuration!.GetSection("HttpService"));
    //    }
    //    services.AddTransient<IHttpService, HttpService>();
    //    return services;
    //}

    public static async Task<HttpResponse> GetAsync(this HttpKeyValueRequest request)
    {
        return await new HttpService().GetAsync(request);
    }
}