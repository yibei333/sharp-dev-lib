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

    /// <summary>
    /// http get请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> GetAsync(this HttpKeyValueRequest request)
    {
        return await new HttpService().GetAsync(request);
    }
}