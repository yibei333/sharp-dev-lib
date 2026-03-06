using Microsoft.Extensions.Logging;

namespace SharpDevLib;

/// <summary>
/// http全局设置
/// </summary>
public class HttpConfig
{
    /// <summary>
    /// 默认
    /// </summary>
    public static HttpConfig Default { get; set; } = new()
    {
        Logger = new ConsoleLogger(nameof(HttpHelper)),
        UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0"
    };

    /// <summary>
    /// 日志
    /// </summary>
    public ILogger? Logger { get; set; }

    /// <summary>
    /// 超时时间
    /// </summary>
    public TimeSpan? TimeOut { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// UA
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 接收数据回调
    /// </summary>
    public Action<HttpProgress>? OnReceiveProgress { get; set; }

    /// <summary>
    /// 传入数据回调
    /// </summary>
    public Action<HttpProgress>? OnSendProgress { get; set; }
}