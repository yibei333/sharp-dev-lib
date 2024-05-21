using System.Net;

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
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> GetAsync(this HttpKeyValueRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).GetAsync(request, cancellationToken);
    }

    /// <summary>
    /// http get请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> GetAsync<T>(this HttpKeyValueRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).GetAsync<T>(request, cancellationToken);
    }

    /// <summary>
    /// http get请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<Stream> GetStreamAsync(this HttpKeyValueRequest request, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).GetStreamAsync(request);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> PostAsync(this HttpJsonRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync(request, cancellationToken);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> PostAsync<T>(this HttpJsonRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync<T>(request, cancellationToken);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> PostAsync(this HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync(request, cancellationToken);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> PostAsync<T>(this HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync<T>(request, cancellationToken);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> PostAsync(this HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync(request, cancellationToken);
    }

    /// <summary>
    /// http post请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> PostAsync<T>(this HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PostAsync<T>(request, cancellationToken);
    }

    /// <summary>
    /// http put请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> PutAsync(this HttpJsonRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PutAsync(request, cancellationToken);
    }

    /// <summary>
    /// http put请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> PutAsync<T>(this HttpJsonRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).PutAsync<T>(request, cancellationToken);
    }

    /// <summary>
    /// http delete请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse> DeleteAsync(this HttpKeyValueRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).DeleteAsync(request, cancellationToken);
    }

    /// <summary>
    /// http delete请求
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="serviceProvider">serviceProvider(获取ILogger用)</param>
    /// <returns>http响应</returns>
    public static async Task<HttpResponse<T>> DeleteAsync<T>(this HttpKeyValueRequest request, CancellationToken? cancellationToken = null, IServiceProvider? serviceProvider = null)
    {
        return await new HttpService(serviceProvider).DeleteAsync<T>(request, cancellationToken);
    }

    internal static Cookie? ParseCookie(this string cookieString, string host)
    {
        if (cookieString.IsNullOrEmpty()) return null;

        var array = cookieString.Split(';');
        if (array.IsNullOrEmpty()) return null;

        var nameValue = array[0].Trim().ParseCookieValue();
        var cookie = new Cookie(nameValue.Key, nameValue.Value);

        for (int i = 1; i < array.Length; i++)
        {
            var keyValue = array[i].ParseCookieValue();
            if (keyValue.Key.Equals("Domain", StringComparison.InvariantCultureIgnoreCase)) cookie.Domain = keyValue.Value ?? host;
            else if (keyValue.Key.Equals("Expires", StringComparison.InvariantCultureIgnoreCase)) cookie.Expires = DateTime.TryParse(keyValue.Value, out var time) ? time : DateTime.Now;
            else if (keyValue.Key.Equals("Max-Age", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!array.Any(x => x.Contains("Expires"))) cookie.Expires = TimeExtension.UtcStartTime.AddSeconds(int.TryParse(keyValue.Value, out var seconds) ? seconds : 0);
            }
            else if (keyValue.Key.Equals("HttpOnly", StringComparison.InvariantCultureIgnoreCase)) cookie.HttpOnly = true;
            else if (keyValue.Key.Equals("Path", StringComparison.InvariantCultureIgnoreCase)) cookie.Path = keyValue.Value ?? "/";
            else if (keyValue.Key.Equals("Secure", StringComparison.InvariantCultureIgnoreCase)) cookie.Secure = true;
        }

        return cookie;
    }

    internal static KeyValuePair<string, string?> ParseCookieValue(this string valuePair)
    {
        var index = valuePair.IndexOf('=');
        if (index < 0) return new KeyValuePair<string, string?>(valuePair, null);

        var name = valuePair.Substring(0, index);
        var value = valuePair.Substring(index + 1);
        return new KeyValuePair<string, string?>(name, value);
    }
}