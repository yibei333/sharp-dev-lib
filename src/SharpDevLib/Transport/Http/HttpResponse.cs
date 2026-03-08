using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// HTTP响应，封装HTTP请求的响应信息
/// </summary>
public class HttpResponse(HttpRequest request, HttpResponseMessage? httpResponseMessage, string? errorMessage, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming)
{
    /// <summary>
    /// 获取对应的HTTP请求
    /// </summary>
    HttpRequest Request { get; } = request;

    readonly HttpResponseMessage? _httpResponseMessage = httpResponseMessage;

    /// <summary>
    /// 获取HTTP响应消息
    /// </summary>
    /// <exception cref="Exception">当无法获取响应消息时抛出异常</exception>
    public HttpResponseMessage HttpResponseMessage => _httpResponseMessage ?? throw new Exception("无法获取响应消息,可能是请求任务在完成前被取消");

    /// <summary>
    /// 获取请求是否成功
    /// </summary>
    /// <value>当HTTP状态码为2xx时返回true，否则返回false</value>
    public bool IsSuccess { get; } = httpResponseMessage?.IsSuccessStatusCode ?? false;

    /// <summary>
    /// 获取HTTP状态码
    /// </summary>
    public HttpStatusCode Code { get; } = httpResponseMessage?.StatusCode ?? HttpStatusCode.Unused;

    /// <summary>
    /// 获取错误消息
    /// </summary>
    /// <value>当请求失败时包含错误描述信息</value>
    public string? ErrorMessage { get; } = errorMessage;

    /// <summary>
    /// 获取重试次数
    /// </summary>
    /// <value>请求失败后的重试次数</value>
    public int RetryCount { get; } = retryCount;

    /// <summary>
    /// 获取处理次数（重试次数+1）
    /// </summary>
    public int ProcessCount => RetryCount + 1;

    /// <summary>
    /// 获取最后一次请求的耗时
    /// </summary>
    public TimeSpan LastTimeConsuming { get; } = lastTimeConsuming;

    /// <summary>
    /// 获取总耗时
    /// </summary>
    /// <value>包含所有重试在内的总耗时</value>
    public TimeSpan TotalTimeConsuming { get; } = totalTimeConsuming;

    /// <summary>
    /// 获取响应头信息
    /// </summary>
    /// <returns>响应头字典，键为头名称，值为头值数组</returns>
    public Dictionary<string, string[]>? GetResponseHeaders() => HttpResponseMessage.Headers?.Select(x => new KeyValuePair<string, string[]>(x.Key, [.. x.Value])).ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// 获取响应Cookie集合
    /// </summary>
    /// <returns>Cookie列表，如果不存在Set-Cookie头则返回null</returns>
    public List<Cookie?>? GetResponseCookies()
    {
        var headers = GetResponseHeaders();
        if (headers.IsNullOrEmpty() || !headers.ContainsKey("Set-Cookie")) return null;
        var host = new Uri(Request.Url).Host;
        return [.. headers["Set-Cookie"].Select(x => ParseCookie(x, host))];
    }

    /// <summary>
    /// 确保响应状态码表示成功，否则抛出异常
    /// </summary>
    /// <returns>当前响应对象</returns>
    /// <exception cref="Exception">当响应不成功时抛出异常</exception>
    public HttpResponse EnsureSuccessStatusCode()
    {
        if (!IsSuccess)
        {
            (Request.Config ?? HttpConfig.Default).Logger?.LogInformation(ToString());
            throw new Exception("HTTP请求失败,请检查响应状态码和错误信息");
        }
        return this;
    }

    /// <summary>
    /// 返回响应的字符串表示，包含请求和响应的详细信息
    /// </summary>
    /// <returns>请求和响应的详细信息字符串，用于日志记录</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        BuildRequestInfo(builder, Request);
        BuildResponseInfo(builder, this);
        return builder.ToString();
    }

    /// <summary>
    /// 异步获取响应数据并反序列化为指定类型
    /// </summary>
    /// <typeparam name="T">目标数据类型</typeparam>
    /// <returns>反序列化后的响应数据</returns>
    public async Task<T> GetResponseDataAsync<T>()
    {
        var content = HttpResponseMessage.Content;
        T? data = default;
        if (content is not null)
        {
            var type = typeof(T);
            if (type == typeof(byte[]))
            {
                data = (T)Convert.ChangeType(await content.ReadAsByteArrayAsync(), type);
            }
            else
            {
                var responseText = await content.ReadAsStringAsync();
                if (responseText.NotNullOrWhiteSpace())
                {
                    if (type.IsClass)
                    {
                        if (type == typeof(string)) data = (T)Convert.ChangeType(responseText, type);
                        else
                        {
                            var obj = responseText.DeSerialize(type);
                            if (obj is not null) data = (T)obj;
                        }
                    }
                    else
                    {
                        data = (T)Convert.ChangeType(responseText, type);
                    }
                }
            }
        }
        return data!;
    }

    static Cookie? ParseCookie(string? cookieString, string host)
    {
        if (cookieString.IsNullOrEmpty()) return null;

        var array = cookieString.Split(';');
        if (array.IsNullOrEmpty()) return null;

        var nameValue = ParseCookieValue(array[0].Trim());
        var cookie = new Cookie(nameValue.Key, nameValue.Value);

        for (int i = 1; i < array.Length; i++)
        {
            var keyValue = ParseCookieValue(array[i]);
            if (keyValue.Key.Equals("Domain", StringComparison.InvariantCultureIgnoreCase)) cookie.Domain = keyValue.Value ?? host;
            else if (keyValue.Key.Equals("Expires", StringComparison.InvariantCultureIgnoreCase)) cookie.Expires = DateTime.TryParse(keyValue.Value, out var time) ? time : DateTime.Now;
            else if (keyValue.Key.Equals("Max-Age", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!array.Any(x => x.Contains("Expires"))) cookie.Expires = TimeHelper.UtcStartTime.AddSeconds(int.TryParse(keyValue.Value, out var seconds) ? seconds : 0);
            }
            else if (keyValue.Key.Equals("HttpOnly", StringComparison.InvariantCultureIgnoreCase)) cookie.HttpOnly = true;
            else if (keyValue.Key.Equals("Path", StringComparison.InvariantCultureIgnoreCase)) cookie.Path = keyValue.Value ?? "/";
            else if (keyValue.Key.Equals("Secure", StringComparison.InvariantCultureIgnoreCase)) cookie.Secure = true;
        }

        return cookie;
    }

    static KeyValuePair<string, string?> ParseCookieValue(string valuePair)
    {
        var index = valuePair.IndexOf('=');
        if (index < 0) return new KeyValuePair<string, string?>(valuePair, null);

        var name = valuePair.Substring(0, index);
        var value = valuePair.Substring(index + 1);
        return new KeyValuePair<string, string?>(name, value);
    }

    static void BuildRequestInfo(StringBuilder builder, HttpRequest request)
    {
        builder.AppendLine($"****request****");
        builder.AppendLine($"url:{request.RequestUrl}");
        builder.AppendLine($"method:{request.HttpRequestMessage.Method}");
        if (request.Headers.NotNullOrEmpty())
        {
            builder.AppendLine("headers:");
            foreach (var item in request.Headers)
            {
                builder.AppendLine($"{item.Key}:{string.Join(",", item.Value)}");
            }
        }

        var requestContentType = request.HttpRequestMessage.Content?.Headers?.ContentType?.ToString();
        if (requestContentType.IsNullOrWhiteSpace()) return;
        builder.AppendLine($"content-type:{requestContentType}");
        if (requestContentType.Contains("application/json"))
        {
            builder.AppendLine($"json:");
            builder.AppendLine(request.Json);
        }
        else if (requestContentType.Contains("multipart/form-data"))
        {
            builder.AppendLine($"form:");
            if (request.Parameters.NotNullOrEmpty())
            {
                foreach (var item in request.Parameters)
                {
                    builder.AppendLine($"{item.Key}:{item.Value}");
                }
            }
            if (request.Files.NotNullOrEmpty())
            {
                foreach (var item in request.Files)
                {
                    builder.AppendLine($"{item.ParameterName}:{item.FileName}");
                }
            }
        }
        else if (requestContentType.Contains("application/x-www-form-urlencoded"))
        {
            builder.AppendLine($"form:");
            if (request.Parameters.NotNullOrEmpty())
            {
                foreach (var item in request.Parameters)
                {
                    builder.AppendLine($"{item.Key}:{item.Value}");
                }
            }
        }
        else throw new NotImplementedException($"暂不支持的内容类型:{requestContentType}");
    }

    static void BuildResponseInfo(StringBuilder builder, HttpResponse response)
    {
        builder.AppendLine($"****response****");
        builder.AppendLine($"code:{response.Code}");
        if (response.ErrorMessage.NotNullOrWhiteSpace()) builder.AppendLine($"error message:{response.ErrorMessage}");
        if (response.HttpResponseMessage.Content is null) builder.AppendLine("no response");
        else
        {
            if (response.HttpResponseMessage.Content.Headers?.ContentType?.ToString().Contains("application/json") ?? false)
            {
                builder.AppendLine("reply:");
                builder.AppendLine(response.HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
}
