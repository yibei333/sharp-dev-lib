namespace SharpDevLib;

/// <summary>
/// 时间扩展
/// </summary>
public static class TimeExtension
{
    /// <summary>
    /// UTC开始时间
    /// </summary>
    public static DateTime UtcStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 将时间转换为UTC时间戳
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns>UTC时间戳</returns>
    public static long ToUtcTimestamp(this DateTime time) => (long)(time.ToUniversalTime() - UtcStartTime).TotalMilliseconds;

    /// <summary>
    /// 将UTC时间戳转换为时间
    /// </summary>
    /// <param name="utcTimeStamp">UTC时间戳</param>
    /// <returns>时间</returns>
    public static DateTime ToUtcTime(this long utcTimeStamp) => UtcStartTime.AddMilliseconds(utcTimeStamp);

    /// <summary>
    /// 将时间转换为格式化的字符串
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="format">格式</param>
    /// <returns>格式化的字符串</returns>
    public static string ToTimeString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss") => time.ToString(format);
}
