namespace SharpDevLib.Standard;

/// <summary>
/// 集合扩展
/// </summary>
public static class EnumerableExtension
{
    /// <summary>
    /// 根据对象的值（不是引用）去重
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <returns>去重的集合</returns>
    public static IEnumerable<T?>? DistinctByObjectValue<T>(this IEnumerable<T?>? source) where T : class
    {
        if (source.IsNullOrEmpty()) return source;
        return source.Distinct(new ObjectValueComparer<T>());
    }
}

internal class ObjectValueComparer<T> : IEqualityComparer<T?> where T : class
{
    public bool Equals(T? x, T? y)
    {
        if (x is null && y is null) return true;
        return x.Serialize() == y.Serialize();
    }

    public int GetHashCode(T? obj)
    {
        return obj is null ? -1 : 1;
    }
}