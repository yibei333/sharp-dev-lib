using System.Text.RegularExpressions;

namespace SharpDevLib;

/// <summary>
/// 字符串扩展，提供字符串处理、转换和验证功能
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// 删除字符串前缀
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="target">要删除的前缀字符串</param>
    /// <returns>删除前缀后的字符串</returns>
    public static string TrimStart(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source;
        if (source.StartsWith(target)) return source.Substring(source.IndexOf(target) + target.Length);
        return source;
    }

    /// <summary>
    /// 删除字符串后缀
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="target">要删除的后缀字符串</param>
    /// <returns>删除后缀后的字符串</returns>
    public static string TrimEnd(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source;
        if (source.EndsWith(target)) return source.Substring(0, source.IndexOf(target));
        return source;
    }

    /// <summary>
    /// 将字符串转换为Guid
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="throwException">转换失败时是否抛出异常，为false时返回Guid.Empty</param>
    /// <returns>转换后的Guid值</returns>
    /// <exception cref="InvalidCastException">当转换失败且throwException为true时引发异常</exception>
    public static Guid ToGuid(this string? str, bool throwException = false)
    {
        var success = Guid.TryParse(str, out var guid);
        if (throwException && !success) throw new InvalidCastException($"无法将值\"{str}\"转换为Guid");
        return success ? guid : Guid.Empty;
    }

    /// <summary>
    /// 将字符串分割为Guid集合
    /// </summary>
    /// <param name="str">要分割的字符串</param>
    /// <param name="separator">分隔符</param>
    /// <param name="removeEmptyEntries">是否删除空项，默认为true</param>
    /// <param name="throwException">转换失败时是否抛出异常，为false时返回Guid.Empty</param>
    /// <param name="distinct">是否去重，默认为true</param>
    /// <returns>Guid集合</returns>
    /// <exception cref="ArgumentException">当separator参数为'-'时引发异常</exception>
    /// <exception cref="InvalidCastException">当转换失败且throwException为true时引发异常</exception>
    public static List<Guid> SplitToGuidList(this string? str, char separator = ',', bool removeEmptyEntries = true, bool throwException = false, bool distinct = true) => str.SplitToGuidList([separator], removeEmptyEntries, throwException, distinct);

    /// <summary>
    /// 将字符串分割为Guid集合
    /// </summary>
    /// <param name="str">要分割的字符串</param>
    /// <param name="separators">分隔符集合</param>
    /// <param name="removeEmptyEntries">是否删除空项，默认为true</param>
    /// <param name="throwException">转换失败时是否抛出异常，为false时返回Guid.Empty</param>
    /// <param name="distinct">是否去重，默认为true</param>
    /// <returns>Guid集合</returns>
    /// <exception cref="ArgumentException">当separator参数为'-'时引发异常</exception>
    /// <exception cref="InvalidCastException">当转换失败且throwException为true时引发异常</exception>
    public static List<Guid> SplitToGuidList(this string? str, char[] separators, bool removeEmptyEntries = true, bool throwException = false, bool distinct = true)
    {
        if (str.IsNullOrWhiteSpace()) return [];
        if (separators.Contains('-')) throw new ArgumentException("分隔符不能为'-'");
        var list = str.Split(separators, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None).Select(x =>
        {
            var success = Guid.TryParse(x, out var y);
            if (throwException && !success) throw new InvalidCastException($"无法将值\"{x}\"转换为Guid");
            return success ? y : Guid.Empty;
        }).ToList();
        if (distinct) list = [.. list.Distinct()];
        return list;
    }

    /// <summary>
    /// 将字符串分割为字符串集合
    /// </summary>
    /// <param name="str">要分割的字符串</param>
    /// <param name="separator">分隔符</param>
    /// <param name="removeEmptyEntries">是否删除空项，默认为true</param>
    /// <param name="distinct">是否去重，默认为true</param>
    /// <returns>字符串集合</returns>
    /// <exception cref="ArgumentException">当separator参数为'-'时引发异常</exception>
    public static List<string> SplitToList(this string? str, char separator = ',', bool removeEmptyEntries = true, bool distinct = true) => str.SplitToList([separator], removeEmptyEntries, distinct);

    /// <summary>
    /// 将字符串分割为字符串集合
    /// </summary>
    /// <param name="str">要分割的字符串</param>
    /// <param name="separators">分隔符集合</param>
    /// <param name="removeEmptyEntries">是否删除空项，默认为true</param>
    /// <param name="distinct">是否去重，默认为true</param>
    /// <returns>字符串集合</returns>
    public static List<string> SplitToList(this string? str, char[] separators, bool removeEmptyEntries = true, bool distinct = true)
    {
        if (str.IsNullOrWhiteSpace()) return [];
        var list = str.Split(separators, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None).ToList();
        if (distinct) list = [.. list.Distinct()];
        return list;
    }

    /// <summary>
    /// 将字符串转换为bool值，仅当字符串为'true'时（忽略大小写）返回true，其余为false
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的bool值</returns>
    public static bool ToBoolean(this string? str)
    {
        return "true".Equals(str, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 将字符串转换为int值，转换失败时抛出异常
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的int值</returns>
    /// <exception cref="InvalidCastException">当转换失败时引发异常</exception>
    public static int ToIntThrow(this string? str)
    {
        if (!int.TryParse(str, out var value)) throw new InvalidCastException();
        return value;
    }

    /// <summary>
    /// 将字符串转换为int值，转换失败时返回默认值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="defaultValue">转换失败时的默认返回值，默认为0</param>
    /// <returns>转换后的int值</returns>
    public static int ToInt(this string? str, int defaultValue = 0)
    {
        return int.TryParse(str, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// 将字符串转换为decimal值，转换失败时抛出异常
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的decimal值</returns>
    /// <exception cref="InvalidCastException">当转换失败时引发异常</exception>
    public static decimal ToDecimalThrow(this string? str)
    {
        if (!decimal.TryParse(str, out var value)) throw new InvalidCastException();
        return value;
    }

    /// <summary>
    /// 将字符串转换为decimal值，转换失败时返回默认值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="defaultValue">转换失败时的默认返回值，默认为0</param>
    /// <returns>转换后的decimal值</returns>
    public static decimal ToDecimal(this string? str, int defaultValue = 0)
    {
        return decimal.TryParse(str, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// 将字符串转换为double值，转换失败时抛出异常
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的double值</returns>
    /// <exception cref="InvalidCastException">当转换失败时引发异常</exception>
    public static double ToDoubleThrow(this string? str)
    {
        if (!double.TryParse(str, out var value)) throw new InvalidCastException();
        return value;
    }

    /// <summary>
    /// 将字符串转换为double值，转换失败时返回默认值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="defaultValue">转换失败时的默认返回值，默认为0</param>
    /// <returns>转换后的double值</returns>
    public static double ToDouble(this string? str, int defaultValue = 0)
    {
        return double.TryParse(str, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// 字符串转义
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>字符串</returns>
    public static string Escape(this string str) => str.Replace("\\", "\\\\").Replace("\"", "\\\"");

    /// <summary>
    /// 字符串去除转义
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>字符串</returns>
    public static string RemoveEscape(this string str) => str.Replace("\\\"", "\"").Replace("\\\\", "\\");

    /// <summary>
    /// 将输入字符串中的任何转义字符进行转换
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>还原字符串</returns>
    public static string RegexUnescape(this string str) => Regex.Unescape(str);

    /// <summary>
    /// 字符串删除换行
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>字符串</returns>
    public static string RemoveLineBreak(this string str) => str.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

    /// <summary>
    /// 字符串删除空格
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>字符串</returns>
    public static string RemoveSpace(this string str) => str.Replace(" ", "");
}
