using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// HTTP响应，封装HTTP请求的响应信息
/// </summary>
public class HttpResponseModel//改名为HttpResponseModel,防止和Microsoft.AspNetCore.Http.HttpResponse命名冲突
{
    internal HttpResponseModel(HttpRequestModel request, HttpResponseMessage? httpResponseMessage, string? errorMessage, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming)
    {
        _httpResponseMessage = httpResponseMessage;
        Code = _httpResponseMessage?.StatusCode ?? HttpStatusCode.Unused;
        IsSuccess = _httpResponseMessage?.IsSuccessStatusCode ?? false;
        Request = request;
        ErrorMessage = errorMessage;
        if (_httpResponseMessage is null && errorMessage.IsNullOrWhiteSpace()) ErrorMessage = "无响应";
        RetryCount = retryCount;
        LastTimeConsuming = lastTimeConsuming;
        TotalTimeConsuming = totalTimeConsuming;
    }

    /// <summary>
    /// 获取对应的HTTP请求
    /// </summary>
    HttpRequestModel Request { get; }

    readonly HttpResponseMessage? _httpResponseMessage;

    /// <summary>
    /// 获取HTTP响应消息
    /// </summary>
    /// <exception cref="Exception">当无法获取响应消息时抛出异常</exception>
    HttpResponseMessage HttpResponseMessage => _httpResponseMessage ?? throw new Exception("无法获取响应消息,可能是请求任务在完成前被取消");

    /// <summary>
    /// 获取请求是否成功
    /// </summary>
    /// <value>当HTTP状态码为2xx时返回true，否则返回false</value>
    public bool IsSuccess { get; }

    /// <summary>
    /// 获取HTTP状态码
    /// </summary>
    public HttpStatusCode Code { get; }

    /// <summary>
    /// 获取错误消息
    /// </summary>
    /// <value>当请求失败时包含错误描述信息</value>
    public string? ErrorMessage { get; }

    /// <summary>
    /// 获取重试次数
    /// </summary>
    /// <value>请求失败后的重试次数</value>
    public int RetryCount { get; }

    /// <summary>
    /// 获取处理次数（重试次数+1）
    /// </summary>
    public int ProcessCount => RetryCount + 1;

    /// <summary>
    /// 获取最后一次请求的耗时
    /// </summary>
    public TimeSpan LastTimeConsuming { get; }

    /// <summary>
    /// 获取总耗时
    /// </summary>
    /// <value>包含所有重试在内的总耗时</value>
    public TimeSpan TotalTimeConsuming { get; }

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
    /// 读取字符串
    /// </summary>
    /// <returns>字符串</returns>
    public async Task<string> ReadAsStringAsync()
    {
        return await HttpResponseMessage.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// 读取字节数组
    /// </summary>
    /// <returns>字节数组</returns>
    public async Task<byte[]> ReadAsByteArrayAsync()
    {
        return await HttpResponseMessage.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// 读取流
    /// </summary>
    /// <returns>流</returns>
    public async Task<Stream> ReadAsStreamAsync()
    {
        var sourceStream = await HttpResponseMessage.Content.ReadAsStreamAsync();
        var progress = new HttpProgress { RequestUrl = Request.Message?.RequestUri.ToString() ?? string.Empty, Total = HttpResponseMessage.Content?.Headers?.ContentLength ?? 0 };
        var onProgress = HttpClientFactory.GetClient(Request.ClientId).Config.OnReceiveProgress;
        var lastProgress = progress.Progress;
        return new HttpProgressStream(sourceStream, (p) =>
        {
            progress.Transfered = p;
            if (progress.Progress == 100 || (progress.Progress - lastProgress) > 5)
            {
                onProgress?.Invoke(progress);
                lastProgress = progress.Progress;
            }
        });
    }

    /// <summary>
    /// 确保响应状态码表示成功，否则抛出异常
    /// </summary>
    /// <returns>当前响应对象</returns>
    /// <exception cref="Exception">当响应不成功时抛出异常</exception>
    public HttpResponseModel EnsureSuccessStatusCode()
    {
        if (!IsSuccess)
        {
            HttpClientFactory.GetClient(Request.ClientId).Config.Logger?.LogInformation(ToString());
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
        BuildRequestInfo(builder);
        BuildResponseInfo(builder);
        return builder.ToString();
    }

    /// <summary>
    /// 异步获取响应数据并反序列化为指定类型
    /// </summary>
    /// <typeparam name="T">目标数据类型</typeparam>
    /// <returns>反序列化后的响应数据</returns>
    public async Task<T> GetResponseDataAsync<T>(JsonOption? option = null)
    {
        var content = HttpResponseMessage!.Content;
        T? data = default;
        if (content is not null)
        {
            var type = typeof(T);
            if (type == typeof(byte[]))
            {
                data = (T)Convert.ChangeType(await ReadAsByteArrayAsync(), type);
            }
            else
            {
                var responseText = await ReadAsStringAsync();
                if (responseText.NotNullOrWhiteSpace())
                {
                    if (type.IsClass)
                    {
                        if (type == typeof(string)) data = (T)Convert.ChangeType(responseText, type);
                        else
                        {
                            var obj = responseText.DeSerialize(type, option);
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

    void BuildRequestInfo(StringBuilder builder)
    {
        builder.AppendLine($"****request****");
        builder.AppendLine($"clientId:{Request.ClientId}");
        builder.AppendLine($"url:{Request.Message?.RequestUri}");
        builder.AppendLine($"method:{Request.Message?.Method}");

        var headers = new List<KeyValuePair<string, List<string>>>();
        Request.Message?.Headers.ForEach((_, y) =>
        {
            headers.Add(new KeyValuePair<string, List<string>>(y.Key, [.. y.Value]));
        });
        Request.Message?.Content?.Headers.ForEach((_, y) =>
        {
            headers.Add(new KeyValuePair<string, List<string>>(y.Key, [.. y.Value]));
        });
        if (Request.Message?.RequestUri?.ToString().NotNullOrWhiteSpace() ?? false)
        {
            var clientInfo = HttpClientFactory.GetClient(Request.ClientId);
            var cookies = clientInfo.ClientHandler.CookieContainer.GetCookies(Request.Message.RequestUri);
            if (cookies.Count > 0)
            {
                if (!headers.Any(x => x.Key.Equals("Cookie")))
                {
                    headers.Add(new KeyValuePair<string, List<string>>("Cookie", [""]));
                }
                var cookie = headers.First(x => x.Key.Equals("Cookie"));
                var cookieValue = cookie.Value.First();
                foreach (Cookie item in clientInfo.ClientHandler.CookieContainer.GetCookies(Request.Message.RequestUri))
                {
                    cookieValue += $"{item.Name}={item.Value};";
                }
                cookie.Value.Clear();
                cookie.Value.Add(cookieValue);
            }
        }
        if (headers.NotNullOrEmpty())
        {
            builder.AppendLine("headers:");
            foreach (var item in headers)
            {
                builder.AppendLine($"{item.Key}:{string.Join(",", item.Value)}");
            }
        }

        var requestContentType = Request.Message?.Content?.Headers?.ContentType?.ToString();
        if (requestContentType.IsNullOrWhiteSpace()) return;
        builder.AppendLine($"content-type:{requestContentType}");
        if (requestContentType.Contains("application/json"))
        {
            builder.AppendLine($"json:");
            builder.AppendLine(Request.Json);
        }
        else if (requestContentType.Contains("multipart/form-data"))
        {
            builder.AppendLine($"form:");
            if (Request.Parameters.NotNullOrEmpty())
            {
                foreach (var item in Request.Parameters)
                {
                    builder.AppendLine($"{item.Key}:{item.Value}");
                }
            }
            if (Request.Files.NotNullOrEmpty())
            {
                foreach (var item in Request.Files)
                {
                    builder.AppendLine($"{item.ParameterName}:{item.FileName}");
                }
            }
        }
        else if (requestContentType.Contains("application/x-www-form-urlencoded"))
        {
            builder.AppendLine($"form:");
            if (Request.Parameters.NotNullOrEmpty())
            {
                foreach (var item in Request.Parameters)
                {
                    builder.AppendLine($"{item.Key}:{item.Value}");
                }
            }
        }
        else throw new NotImplementedException($"暂不支持的内容类型:{requestContentType}");
    }

    void BuildResponseInfo(StringBuilder builder)
    {
        builder.AppendLine($"****response****");
        builder.AppendLine($"code:{Code}");
        var headers = new List<KeyValuePair<string, List<string>>>();
        _httpResponseMessage?.Headers.ForEach((_, y) =>
        {
            headers.Add(new KeyValuePair<string, List<string>>(y.Key, [.. y.Value]));
        });
        _httpResponseMessage?.Content?.Headers.ForEach((_, y) =>
        {
            headers.Add(new KeyValuePair<string, List<string>>(y.Key, [.. y.Value]));
        });
        if (headers.NotNullOrEmpty())
        {
            builder.AppendLine("headers:");
            foreach (var item in headers)
            {
                builder.AppendLine($"{item.Key}:{string.Join(",", item.Value)}");
            }
        }

        if (ErrorMessage.NotNullOrWhiteSpace()) builder.AppendLine($"error message:{ErrorMessage}");
        if (HttpResponseMessage?.Content is not null)
        {
            var contentType = HttpResponseMessage.Content.Headers?.ContentType?.ToString() ?? string.Empty;
            if (contentType.Contains("application/json") || contentType.Contains("text/plain"))
            {
                builder.AppendLine("reply:");
                builder.AppendLine(HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult().RegexUnescape());
            }
        }
    }
}
