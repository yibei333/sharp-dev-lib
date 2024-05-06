namespace SharpDevLib.Standard;

/// <summary>
/// string util
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// trim start by a string
    /// </summary>
    /// <param name="source">source string</param>
    /// <param name="target">target string</param>
    /// <returns>string result</returns>
    public static string TrimStart(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
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
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
        source = source.Trim();
        target = target.Trim();
        if (source.EndsWith(target)) return source[..^target.Length];
        return source;
    }

    /// <summary>
    /// convert string to guid
    /// </summary>
    /// <param name="str">string to convert</param>
    /// <param name="throwException">indicate when convert failed will throw exception</param>
    /// <returns>guid</returns>
    /// <exception cref="InvalidCastException">if [throwException=true] and convert fail</exception>
    public static Guid ToGuid(this string? str, bool throwException = false)
    {
        var success = Guid.TryParse(str, out var guid);
        if (throwException && !success) throw new InvalidCastException($"can not convert value \"{str}\" to guid");
        return success ? guid : Guid.Empty;
    }

    /// <summary>
    /// convert string to a guid list(ignore parse error and except empty guid and distinct value)
    /// </summary>
    /// <param name="str">string to convert</param>
    /// <param name="separator">default is ','</param>
    /// <returns>a list of guid</returns>
    /// <exception cref="ArgumentException">if separator is '-'</exception>
    public static List<Guid> ToGuidList(this string? str, char separator = ',')
    {
        if (str is null) return new List<Guid>();
        if (separator == '-') throw new ArgumentException("separator can not be '-'");
        return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(x => Guid.TryParse(x, out var y) ? y : Guid.Empty).Where(x => x != Guid.Empty).Distinct().ToList();
    }

    //todo:split to array

    /// <summary>
    /// convert string to boolean
    /// </summary>
    /// <param name="str">string to convert</param>
    /// <returns>bool</returns>
    public static bool ToBoolean(this string? str)
    {
        if (str is null) return false;
        return bool.TryParse(str, out var res) && res;
    }
}
