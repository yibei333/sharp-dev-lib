using System.Net;

namespace SharpDevLib;

/// <summary>
/// http请求
/// </summary>
public class HttpRequest
{
    HttpRequestMessage? _httpRequestMessage;

    internal HttpRequestMessage HttpRequestMessage
    {
        get => _httpRequestMessage ?? throw new Exception("can not get request message,may be task cancled before request");
        set => _httpRequestMessage = value;
    }

    /// <summary>
    /// 实例化http请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpRequest(string url)
    {
        Url = url;
        RequestUrl = url;
    }

    /// <summary>
    /// 实例化http请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="json">json</param>
    public HttpRequest(string url, string json)
    {
        Url = url;
        RequestUrl = url;
        Json = json;
    }

    /// <summary>
    /// 配置
    /// </summary>
    public HttpConfig? Config { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string Url { get; }

    internal string RequestUrl { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public Dictionary<string, string?>? Parameters { get; set; }

    /// <summary>
    /// json参数
    /// </summary>
    public string? Json { get; }

    /// <summary>
    /// 文件
    /// </summary>
    public List<HttpFormFile>? Files { get; set; }

    /// <summary>
    /// 请求头
    /// </summary>
    public Dictionary<string, string[]>? Headers { get; set; }

    /// <summary>
    /// 请求Cookie
    /// </summary>
    public List<Cookie>? Cookies { get; set; }

    /// <summary>
    /// 添加参数
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    /// <returns>Request</returns>
    public HttpRequest AddParameter(string key, string? value)
    {
        Parameters ??= [];
        Parameters[key] = value;
        return this;
    }

    /// <summary>
    /// 添加头
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    /// <returns>Request</returns>
    public HttpRequest AddHeader(string key, string[] value)
    {
        Headers ??= [];
        Headers[key] = value;
        return this;
    }

    /// <summary>
    /// 添加cookie
    /// </summary>
    /// <param name="cookie">cookie</param>
    /// <returns>Request</returns>
    public HttpRequest AddCookie(Cookie cookie)
    {
        Cookies ??= [];
        Cookies.Add(cookie);
        return this;
    }

    /// <summary>
    /// 添加file
    /// </summary>
    /// <param name="file">file</param>
    /// <returns>Request</returns>
    public HttpRequest AddFile(HttpFormFile file)
    {
        Files ??= [];
        Files.Add(file);
        return this;
    }
}