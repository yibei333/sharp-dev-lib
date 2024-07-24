using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// 字符串空断言扩展
/// </summary>
public static class StringNullCheck
{
    /// <summary>
    /// 断言一个字符串是否为null或者空字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>字符串是否为null或者空字符串</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// 断言一个字符串是否不为null且不为空字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>字符串是否不为null且不为空字符串</returns>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);

    /// <summary>
    /// 断言一个字符串是否为null或者空白字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>字符串是否为null或者空白字符串</returns>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 断言一个字符串是否不为null且不为空白字符串
    /// </summary>
    /// <param name="str">需要断言的字符串</param>
    /// <returns>字符串是否不为null且不为空白字符串</returns>
    public static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);
}
