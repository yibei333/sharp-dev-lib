namespace SharpDevLib;

/// <summary>
/// 时间扩展，提供时间戳转换和时间格式化功能
/// </summary>
public static class TimeHelper
{
    /// <summary>
    /// UTC时间起始点，即1970年1月1日00:00:00 UTC
    /// </summary>
    public static DateTime UtcStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 将DateTime转换为UTC时间戳（毫秒）
    /// </summary>
    /// <param name="time">要转换的时间</param>
    /// <returns>UTC时间戳（从1970年1月1日起的毫秒数）</returns>
    public static long ToUtcTimestamp(this DateTime time) => (long)(time.ToUniversalTime() - UtcStartTime).TotalMilliseconds;

    /// <summary>
    /// 将UTC时间戳转换为DateTime
    /// </summary>
    /// <param name="utcTimestamp">UTC时间戳（从1970年1月1日起的毫秒数）</param>
    /// <returns>转换后的DateTime对象</returns>
    public static DateTime ToUtcTime(this long utcTimestamp) => UtcStartTime.AddMilliseconds(utcTimestamp);

    /// <summary>
    /// 将DateTime格式化为指定格式的字符串
    /// </summary>
    /// <param name="time">要格式化的时间</param>
    /// <param name="format">日期时间格式字符串，默认为"yyyy-MM-dd HH:mm:ss"</param>
    /// <returns>格式化后的时间字符串</returns>
    public static string ToTimeString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss") => time.ToString(format);
}
