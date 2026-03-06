using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// 字符串空断言扩展类
/// 为 string 类型提供便捷的空值和空白字符判断扩展方法
/// </summary>
public static class StringNullCheck
{
    /// <summary>
    /// 断言一个字符串是否为 null 或者空字符串 (string.Empty 或 "")
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>如果字符串为 null 或空字符串返回 true,否则返回 false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// 断言一个字符串是否不为 null 且不为空字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>如果字符串不为 null 且不为空字符串返回 true,否则返回 false</returns>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);

    /// <summary>
    /// 断言一个字符串是否为 null 或者空白字符串 (仅包含空白字符或为空)
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>如果字符串为 null 或空白字符串返回 true,否则返回 false</returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 断言一个字符串是否不为 null 且不为空白字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>如果字符串不为 null 且不为空白字符串返回 true,否则返回 false</returns>
    public static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);
}
