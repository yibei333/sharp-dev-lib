using System.Text;

namespace SharpDevLib;

/// <summary>
/// 字符串扩展
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 删除前置字符串,自动处理前后的空白字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="target">要删除的前置字符串</param>
    /// <returns>字符串</returns>
    public static string TrimStart(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
        source = source.Trim();
        target = target.Trim();
        if (source.StartsWith(target)) return source.Substring(source.IndexOf(target) + target.Length);
        return source;
    }

    /// <summary>
    /// 删除后置字符串,自动处理前后的空白字符串
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="target">要删除的后置字符串</param>
    /// <returns>字符串</returns>
    public static string TrimEnd(this string source, string target)
    {
        if (source.IsNullOrWhiteSpace() || target.IsNullOrWhiteSpace()) return source.Trim();
        source = source.Trim();
        target = target.Trim();
        if (source.EndsWith(target)) return source.Substring(0, source.IndexOf(target));
        return source;
    }

    /// <summary>
    /// 字符串转换位Guid
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="throwException">如果转换失败是否抛出异常,如果位false则返回Guid.Empty</param>
    /// <returns>Guid</returns>
    /// <exception cref="InvalidCastException">当转换失败时引发异常</exception>
    public static Guid ToGuid(this string? str, bool throwException = false)
    {
        var success = Guid.TryParse(str, out var guid);
        if (throwException && !success) throw new InvalidCastException($"can not convert value \"{str}\" to guid");
        return success ? guid : Guid.Empty;
    }

    /// <summary>
    /// 将字符串转分割为Guid集合
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="separator">分隔符</param>
    /// <param name="removeEmptyEntries">是否删除空白</param>
    /// <param name="throwException">如果转换失败是否抛出异常,如果位false则返回Guid.Empty</param>
    /// <param name="distinct">是否去重</param>
    /// <returns>Guid集合</returns>
    /// <exception cref="ArgumentException">当separator参数为'-'时引发异常</exception>
    /// <exception cref="InvalidCastException">当转换失败且throwException为true时引发异常</exception>
    public static List<Guid> SplitToGuidList(this string? str, char separator = ',', bool removeEmptyEntries = true, bool throwException = false, bool distinct = true)
    {
        if (str.IsNullOrWhiteSpace()) return new List<Guid>();
        if (separator == '-') throw new ArgumentException("separator can not be '-'");
        var list = str.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None).Select(x =>
        {
            var success = Guid.TryParse(x, out var y);
            if (throwException && !success) throw new InvalidCastException($"can not convert value \"{x}\" to guid");
            return success ? y : Guid.Empty;
        }).ToList();
        if (distinct) list = list.Distinct().ToList();
        return list;
    }

    /// <summary>
    /// 将字符串分割为集合
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="separator">分隔符</param>
    /// <param name="removeEmptyEntries">是否删除空白</param>
    /// <param name="distinct">是否去重</param>
    /// <returns>Guid集合</returns>
    /// <exception cref="ArgumentException">当separator参数为'-'时引发异常</exception>
    public static List<string> SplitToList(this string? str, char separator = ',', bool removeEmptyEntries = true, bool distinct = true)
    {
        if (str.IsNullOrWhiteSpace()) return new List<string>();
        if (separator == '-') throw new ArgumentException("separator can not be '-'");
        var list = str.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None).ToList();
        if (distinct) list = list.Distinct().ToList();
        return list;
    }

    /// <summary>
    /// 将字符串转换为bool值,仅当字符串为'true'时(忽略大小写)返回true,其余为false
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>bool值</returns>
    public static bool ToBoolean(this string? str)
    {
        return "true".Equals(str, StringComparison.OrdinalIgnoreCase);
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

    /// <summary>
    /// 获取相对路径
    /// </summary>
    /// <param name="sourcePath">源路径</param>
    /// <param name="targetPath">目标路径</param>
    /// <returns>相对路径</returns>
    /// <exception cref="InvalidDataException">当源路径和目标路径相同时引发异常</exception>
    public static string GetUrlRelativePath(this string sourcePath, string targetPath)
    {
        sourcePath = sourcePath.FormatPath();
        targetPath = targetPath.FormatPath();
        if (sourcePath.Equals(targetPath)) throw new InvalidDataException();

        var commonPrefix = GetUrlCommonPrefix(sourcePath, targetPath);
        sourcePath = sourcePath.TrimStart(commonPrefix).TrimStart('/');
        targetPath = targetPath.TrimStart(commonPrefix).TrimStart('/');

        var sourceCount = sourcePath.Split('/').Count();
        if (sourceCount == 1) return $"./{targetPath}";
        else
        {
            var builder = new StringBuilder();
            for (int i = 0; i < sourceCount - 1; i++)
            {
                builder.Append("../");
            }
            builder.Append(targetPath);
            return builder.ToString();
        }
    }

    /// <summary>
    /// 获取地址相同的前缀
    /// </summary>
    /// <param name="url1">地址1</param>
    /// <param name="url2">地址2</param>
    /// <returns>相同的前缀</returns>
    public static string GetUrlCommonPrefix(this string url1, string url2)
    {
        url1 = url1.FormatPath();
        url2 = url2.FormatPath();
        var array1 = url1.SplitToList('/');
        var array2 = url2.SplitToList('/');
        int minCount = Math.Min(array1.Count, array2.Count);
        int i;
        for (i = 0; i < minCount; i++)
        {
            if (array1[i] != array2[i])
            {
                break;
            }
        }
        return string.Join("/", array1.Take(i));
    }

    /// <summary>
    /// 获取字符串相同的前缀
    /// </summary>
    /// <param name="str1">字符串1</param>
    /// <param name="str2">字符串2</param>
    /// <returns>相同的前缀</returns>
    public static string GetCommonPrefix(this string str1, string str2)
    {
        int length = Math.Min(str1.Length, str2.Length);
        int i;
        for (i = 0; i < length; i++)
        {
            if (str1[i] != str2[i])
            {
                break;
            }
        }
        return str1.Substring(0, i);
    }
}
