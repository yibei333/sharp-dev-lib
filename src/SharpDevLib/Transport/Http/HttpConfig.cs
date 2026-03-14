using Microsoft.Extensions.Logging;

namespace SharpDevLib;

/// <summary>
/// http全局设置
/// </summary>
public class HttpConfig
{
    /// <summary>
    /// 完成请求选项
    /// </summary>
    public HttpCompletionOption HttpCompletionOption { get; set; } = HttpCompletionOption.ResponseHeadersRead;

    /// <summary>
    /// 日志记录器
    /// </summary>
    public ILogger? Logger { get; set; }

    /// <summary>
    /// 基础地址
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// 请求超时时间
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// 请求失败时的重试次数
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// User-Agent请求头
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 接收数据进度回调事件,需要响应头中包含Content-Length,一般适用于下载文件
    /// </summary>
    public Action<HttpProgress>? OnReceiveProgress { get; set; }

    /// <summary>
    /// 发送数据进度回调事件,需要请求头中包含Content-Length,一般适用于上传文件
    /// </summary>
    public Action<HttpProgress>? OnSendProgress { get; set; }
}