using System.Net;

namespace SharpDevLib;

/// <summary>
/// HTTP请求
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
    /// 实例化HTTP请求
    /// </summary>
    /// <param name="url">请求URL地址</param>
    public HttpRequest(string url)
    {
        Url = url;
        RequestUrl = url;
    }

    /// <summary>
    /// 实例化HTTP请求（JSON格式）
    /// </summary>
    /// <param name="url">请求URL地址</param>
    /// <param name="json">JSON格式的请求体</param>
    public HttpRequest(string url, string json)
    {
        Url = url;
        RequestUrl = url;
        Json = json;
    }

    /// <summary>
    /// HTTP配置
    /// </summary>
    public HttpConfig? Config { get; set; }

    /// <summary>
    /// 请求URL地址
    /// </summary>
    public string Url { get; }

    internal string RequestUrl { get; set; }

    /// <summary>
    /// 请求参数字典
    /// </summary>
    /// <remarks>用于GET/DELETE请求的查询参数，或POST/PUT请求的表单参数</remarks>
    public Dictionary<string, string?>? Parameters { get; set; }

    /// <summary>
    /// JSON格式的请求体
    /// </summary>
    public string? Json { get; }

    /// <summary>
    /// 表单文件列表
    /// </summary>
    public List<HttpFormFile>? Files { get; set; }

    /// <summary>
    /// HTTP请求头字典
    /// </summary>
    public Dictionary<string, string[]>? Headers { get; set; }

    /// <summary>
    /// HTTP请求Cookie列表
    /// </summary>
    public List<Cookie>? Cookies { get; set; }

    /// <summary>
    /// 添加请求参数
    /// </summary>
    /// <param name="key">参数键</param>
    /// <param name="value">参数值</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public HttpRequest AddParameter(string key, string? value)
    {
        Parameters ??= [];
        Parameters[key] = value;
        return this;
    }

    /// <summary>
    /// 添加HTTP请求头
    /// </summary>
    /// <param name="key">请求头名称</param>
    /// <param name="value">请求头值数组</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public HttpRequest AddHeader(string key, string[] value)
    {
        Headers ??= [];
        Headers[key] = value;
        return this;
    }

    /// <summary>
    /// 添加Cookie
    /// </summary>
    /// <param name="cookie">Cookie对象</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public HttpRequest AddCookie(Cookie cookie)
    {
        Cookies ??= [];
        Cookies.Add(cookie);
        return this;
    }

    /// <summary>
    /// 添加表单文件
    /// </summary>
    /// <param name="file">表单文件对象</param>
    /// <returns>当前请求对象，支持链式调用</returns>
    public HttpRequest AddFile(HttpFormFile file)
    {
        Files ??= [];
        Files.Add(file);
        return this;
    }
}