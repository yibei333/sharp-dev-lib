using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Logging;
using SharpDevLib;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpDevLib;

/// <summary>
/// http响应
/// </summary>
public class HttpResponse(HttpRequest request, HttpResponseMessage? httpResponseMessage, string? errorMessage, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming)
{
    HttpRequest Request { get; } = request;

    readonly HttpResponseMessage? _httpResponseMessage = httpResponseMessage;

    public HttpResponseMessage HttpResponseMessage => _httpResponseMessage ?? throw new Exception("can not get response message,may be task cancled before request");

    /// <summary>
    /// 请求是否成功
    /// </summary>
    public bool IsSuccess { get; } = httpResponseMessage?.IsSuccessStatusCode ?? false;

    /// <summary>
    /// http状态码
    /// </summary>
    public HttpStatusCode Code { get; } = httpResponseMessage?.StatusCode ?? HttpStatusCode.Unused;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMessage { get; } = errorMessage;

    /// <summary>
    /// 重试次数
    /// </summary>
    public int RetryCount { get; } = retryCount;

    /// <summary>
    /// 处理次数
    /// </summary>
    public int ProcessCount => RetryCount + 1;

    /// <summary>
    /// 最后一次耗时
    /// </summary>
    public TimeSpan LastTimeConsuming { get; } = lastTimeConsuming;

    /// <summary>
    /// 总耗时
    /// </summary>
    public TimeSpan TotalTimeConsuming { get; } = totalTimeConsuming;

    public Dictionary<string, string[]>? GetResponseHeaders() => HttpResponseMessage.Headers?.Select(x => new KeyValuePair<string, string[]>(x.Key, [.. x.Value])).ToDictionary();

    public List<Cookie?>? GetResponseCookies()
    {
        var headers = GetResponseHeaders();
        if (headers.IsNullOrEmpty() || !headers.ContainsKey("Set-Cookie")) return null;
        var host = new Uri(Request.Url).Host;
        return [.. headers["Set-Cookie"].Select(x => ParseCookie(x, host))];
    }

    public HttpResponse EnsureSuccessStatusCode()
    {
        if (!IsSuccess)
        {
            (Request.Config ?? HttpConfig.Default).Logger?.LogInformation(ToString());
            throw new Exception("请求失败");
        }
        return this;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        BuildRequestInfo(builder, Request);
        BuildResponseInfo(builder, this);
        return builder.ToString();
    }

    public async Task<T> GetResponseDataAsync<T>()
    {
        var content = HttpResponseMessage.Content;
        T? data = default;
        if (content is not null)
        {
            var type = typeof(T);
            if (type == typeof(byte[]))
            {
                data = (T)Convert.ChangeType(content.ReadAsByteArrayAsync().Result, type);
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
        if (requestContentType == "application/json")
        {
            builder.AppendLine($"json:");
            builder.AppendLine(request.Json);
        }
        else if (requestContentType == "multipart/form-data")
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
        else if (requestContentType == "application/x-www-form-urlencoded")
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
        else throw new NotImplementedException($"content-type:{requestContentType} not supported yet");
    }

    static void BuildResponseInfo(StringBuilder builder, HttpResponse response)
    {
        builder.AppendLine($"****response****");
        builder.AppendLine($"code:{response.Code}");
        builder.AppendLine($"error message:{response.ErrorMessage}");
        if (response.HttpResponseMessage.Content is null) builder.AppendLine("no response");
        else
        {
            if (response.HttpResponseMessage.Content.Headers?.ContentType?.ToString() == "application/json")
            {
                builder.AppendLine($"reply:{response.HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
            }
        }
    }
}
