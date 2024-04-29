namespace SharpDevLib;

/// <summary>
/// string util
/// </summary>
public static class StringUtil
{
    /// <summary>
    /// trim start by a string
    /// </summary>
    /// <param name="source">source string</param>
    /// <param name="target">target string</param>
    /// <returns>string result</returns>
    public static string TrimStart(this string source, string target)
    {
        if (source.IsEmpty() || target.IsEmpty()) return source;
        source = source.Trim();
        target = target.Trim();
        if (source.StartsWith(target)) return source[target.Length..];
        return source;
    }

    /// <summary>
    /// trim end by a string
    /// </summary>
    /// <param name="source">source string</param>
    /// <param name="target">target string</param>
    /// <returns>string result</returns>
    public static string TrimEnd(this string source, string target)
    {
        if (source.IsEmpty() || target.IsEmpty()) return source;
        source = source.Trim();
        target = target.Trim();
        if (source.EndsWith(target)) return source[..^target.Length];
        return source;
    }
}
