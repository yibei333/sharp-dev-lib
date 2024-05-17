namespace SharpDevLib.Standard;

/// <summary>
/// http全局设置
/// </summary>
public static class HttpGlobalSettings
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
}
