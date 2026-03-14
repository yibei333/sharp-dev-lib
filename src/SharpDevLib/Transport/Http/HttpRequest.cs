using System.Net;

namespace SharpDevLib;

/// <summary>
/// HTTP请求
/// </summary>
/// <remarks>
/// 示例化HTTP请求
/// </remarks>
/// <param name="url">请求URL地址</param>
public class HttpRequest(string url)
{
    internal HttpRequestMessage? Message { get; set; }

    /// <summary>
    /// 请求URL地址
    /// </summary>
    public string Url { get; } = url;

    /// <summary>
    /// 客户端Id
    /// </summary>
    public string? ClientId { get; internal set; }

    /// <summary>
    /// 请求参数字典
    /// </summary>
    /// <remarks>用于GET/DELETE请求的查询参数，或POST/PUT请求的表单参数</remarks>
    public Dictionary<string, string?>? Parameters { get; internal set; }

    /// <summary>
    /// JSON格式的请求体
    /// </summary>
    public string? Json { get; internal set; }

    /// <summary>
    /// 表单文件列表
    /// </summary>
    public List<HttpFormFile>? Files { get; internal set; }

    /// <summary>
    /// HTTP请求头字典
    /// </summary>
    public Dictionary<string, string[]>? Headers { get; internal set; }

    /// <summary>
    /// HTTP请求Cookie列表
    /// </summary>
    public List<Cookie>? Cookies { get; internal set; }
}

/// <summary>
/// HTTP请求扩展
/// </summary>
public static class HttpRequestExtension
{
    /// <summary>
    /// 使用HTTP客户端Id
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="clientId">HTTP客户端Id</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest UseClientId(this HttpRequest request, string clientId)
    {
        request.ClientId = clientId;
        return request;
    }

    /// <summary>
    /// 添加JSON参数
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="json">JSON参数</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddJson(this HttpRequest request, string json)
    {
        request.Json = json;
        return request;
    }

    /// <summary>
    /// 添加请求参数
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="key">参数键</param>
    /// <param name="value">参数值</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddParameter(this HttpRequest request, string key, string? value)
    {
        request.Parameters ??= [];
        request.Parameters[key] = value;
        return request;
    }

    /// <summary>
    /// 添加请求参数集合
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="parameters">参数集合</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddParameters(this HttpRequest request, Dictionary<string, string?> parameters)
    {
        if (request.Parameters.IsNullOrEmpty()) request.Parameters = parameters;
        else
        {
            parameters.ForEach(item => request.Parameters.Add(item.Key, item.Value));
        }
        return request;
    }

    /// <summary>
    /// 添加HTTP请求头
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="key">请求头名称</param>
    /// <param name="value">请求头值数组</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddHeader(this HttpRequest request, string key, string[] value)
    {
        request.Headers ??= [];
        request.Headers[key] = value;
        return request;
    }

    /// <summary>
    /// 添加HTTP请求头集合
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="headers">请求头集合</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddHeaders(this HttpRequest request, Dictionary<string, string[]> headers)
    {
        if (request.Headers.IsNullOrEmpty()) request.Headers = headers;
        else
        {
            headers.ForEach(item => request.Headers.Add(item.Key, item.Value));
        }
        return request;
    }

    /// <summary>
    /// 添加Cookie,一般不会调用,内置了CookieContainer会自动处理Cookie
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cookie">Cookie对象</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddCookie(this HttpRequest request, Cookie cookie)
    {
        request.Cookies ??= [];
        request.Cookies.Add(cookie);
        return request;
    }

    /// <summary>
    /// 添加Cookie集合,一般不会调用,内置了CookieContainer会自动处理Cookie
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cookies">Cookie对象集合</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddCookies(this HttpRequest request, List<Cookie> cookies)
    {
        if (request.Cookies.IsNullOrEmpty()) request.Cookies = cookies;
        else
        {
            cookies.ForEach(item => request.Cookies.Add(item));
        }
        return request;
    }

    /// <summary>
    /// 添加表单文件
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="file">表单文件对象</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddFile(this HttpRequest request, HttpFormFile file)
    {
        request.Files ??= [];
        request.Files.Add(file);
        return request;
    }

    /// <summary>
    /// 添加表单文件集合
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="files">表单文件对象集合</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public static HttpRequest AddFiles(this HttpRequest request, List<HttpFormFile> files)
    {
        if (request.Files.IsNullOrEmpty()) request.Files = files;
        else
        {
            files.ForEach(item => request.Files.Add(item));
        }
        return request;
    }
}