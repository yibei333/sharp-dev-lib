using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib.Standard;

/// <summary>
/// 空断言扩展
/// </summary>
public static class NullCheckExtension
{
    #region string
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
    public static bool NotNullOrEmpty([NotNullWhen(false)] this string? str) => !string.IsNullOrEmpty(str);

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
    public static bool NotNullOrWhiteSpace([NotNullWhen(false)] this string? str) => !string.IsNullOrWhiteSpace(str);
    #endregion

    #region guid
    /// <summary>
    /// 断言一个guid是否为'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否为'00000000-0000-0000-0000-000000000000'</returns>
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

    /// <summary>
    /// 断言一个guid是否不为'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否不为'00000000-0000-0000-0000-000000000000'</returns>
    public static bool NotEmpty(this Guid guid) => guid != Guid.Empty;

    /// <summary>
    /// 断言一个guid是否为null或者'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否为null或者'00000000-0000-0000-0000-000000000000'</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? guid) => guid is null || guid == Guid.Empty;

    /// <summary>
    /// 断言一个guid是否不为null且不等于'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否不为null且不等于'00000000-0000-0000-0000-000000000000'</returns>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this Guid? guid) => guid is not null && guid != Guid.Empty;
    #endregion

    #region enumerable
    /// <summary>
    /// 断言一个可枚举对象是否为Null或者长度为0
    /// </summary>
    /// <typeparam name="T">需要断言的可枚举对象反省类型</typeparam>
    /// <param name="source">需要断言的可枚举对象</param>
    /// <returns>可枚举对象是否为Null或者长度为0</returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source is null || source.Count() <= 0;

    /// <summary>
    /// 断言一个可枚举对象是否不为Null并且长度大于0
    /// </summary>
    /// <typeparam name="T">需要断言的可枚举对象反省类型</typeparam>
    /// <param name="source">需要断言的可枚举对象</param>
    /// <returns>可枚举对象是否不为Null并且长度大于0</returns>
    public static bool NotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? source) => source is not null && source.Count() > 0;
    #endregion
}
