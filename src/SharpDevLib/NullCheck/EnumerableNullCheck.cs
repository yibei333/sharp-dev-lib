using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// 可枚举对象空断言扩展类
/// 为<see cref="IEnumerable{T}"/>类型提供便捷的空值判断扩展方法
/// </summary>
public static class EnumerableNullCheck
{
    /// <summary>
    /// 断言一个可枚举对象是否为 null 或者长度为 0
    /// </summary>
    /// <typeparam name="T">可枚举对象元素类型</typeparam>
    /// <param name="source">需要断言的可枚举对象</param>
    /// <returns>如果可枚举对象为 null 或长度为 0 返回 true,否则返回 false</returns>
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source is null || source.Count() <= 0;

    /// <summary>
    /// 断言一个可枚举对象是否不为 null 并且长度大于 0
    /// </summary>
    /// <typeparam name="T">可枚举对象元素类型</typeparam>
    /// <param name="source">需要断言的可枚举对象</param>
    /// <returns>如果可枚举对象不为 null 且长度大于 0 返回 true,否则返回 false</returns>
    public static bool NotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? source) => source is not null && source.Count() > 0;
}
