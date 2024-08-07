using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// Enumerable空断言扩展
/// </summary>
[BelongDirectory("NullCheck")]
public static class EnumerableNullCheck
{
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
}
