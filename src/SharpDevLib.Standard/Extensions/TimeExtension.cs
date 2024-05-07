namespace SharpDevLib.Standard;

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
    /// convert a long timestamp to datetime
    /// </summary>
    public static DateTime ToUtcTime(this long timeStamp) => UtcStartTime.AddMilliseconds(timeStamp);

    /// <summary>
    /// convert a long timestamp to a format time string
    /// </summary>
    public static string ToUtcTimeString(this long timeStamp, string format = "yyyy-MM-dd HH:mm:ss") => timeStamp.ToUtcTime().ToString(format);

    /// <summary>
    /// convert a long timestamp to a format time string
    /// </summary>
    public static string ToLocalTimeString(this long timeStamp, string format = "yyyy-MM-dd HH:mm:ss") => timeStamp.ToUtcTime().ToLocalTime().ToString(format);

    /// <summary>
    /// convert a datetime to a format string
    /// </summary>
    public static string ToTimeString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss") =>time.ToString(format);
}
