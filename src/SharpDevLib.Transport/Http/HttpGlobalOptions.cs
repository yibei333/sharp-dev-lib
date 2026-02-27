namespace SharpDevLib.Transport;

/// <summary>
/// http全局设置
/// </summary>
public static class HttpGlobalOptions
{
    /// <summary>
    /// 基址
    /// </summary>
    public static string? BaseUrl { get; set; }

    /// <summary>
    /// 超时时间
    /// </summary>
    public static TimeSpan? TimeOut { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public static int? RetryCount { get; set; }

    /// <summary>
    /// 接收数据回调
    /// </summary>
    public static Action<HttpProgress>? OnReceiveProgress { get; set; }

    /// <summary>
    /// 传入数据回调
    /// </summary>
    public static Action<HttpProgress>? OnSendProgress { get; set; }
}

internal class HttpGlobalSettingsOptions
{
    /// <summary>
    /// 基址
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// 超时时间
    /// </summary>
    public int? TimeOut { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int? RetryCount { get; set; }
}