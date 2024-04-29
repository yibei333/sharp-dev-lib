using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib;

/// <summary>
/// enumerable util
/// </summary>
public static class EnumerableUtil
{
    /// <summary>
    /// distinct enumerable by object value(not reference)
    /// </summary>
    /// <typeparam name="T">enumerable type</typeparam>
    /// <param name="source">enumerable value</param>
    /// <returns>distincted value</returns>
    public static IEnumerable<T> DistinctObject<T>(this IEnumerable<T>? source) where T : class {
        if(source.IsEmpty()) return Enumerable.Empty<T>();
        return source.Distinct(new ObjectValueComparer<T>());
    }
}

internal class ObjectValueComparer<T> : IEqualityComparer<T> where T : class
{
    public bool Equals(T x, T y)
    {
        if (x.IsNull() && y.IsNull()) return true;
        return x.Serialize() == y.Serialize();
    }

    public int GetHashCode(T obj)
    {
        return obj.GetHashCode();
    }
}