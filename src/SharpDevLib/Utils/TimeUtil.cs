namespace SharpDevLib;

/// <summary>
/// time convert utils
/// </summary>
public static class TimeUtil
{

    /// <summary>
    /// utc start Time
    /// </summary>
    public static DateTime UtcStartTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// convert a datetime to utc timestamp
    /// </summary>
    /// <param name="time">the time to covert</param>
    /// <returns>utc timestamp</returns>
    public static long ToUtcTimestamp(this DateTime time) => (long)((time.ToUniversalTime() - UtcStartTime).TotalMilliseconds);

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
    public static string ToTimeString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss") => time.ToString(format);
}
