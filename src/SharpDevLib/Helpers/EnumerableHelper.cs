using System.Linq.Expressions;
using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 集合扩展
/// </summary>
public static class EnumerableHelper
{
    /// <summary>
    /// 添加项,方便链式调用
    /// </summary>
    /// <typeparam name="TKey">Key泛型类型</typeparam>
    /// <typeparam name="TValue">Value泛型类型</typeparam>
    /// <param name="source">KeyValuePair集合</param>
    /// <returns>字典</returns>
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) => source.ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// 添加项,方便链式调用
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="item">项</param>
    /// <returns>原集合</returns>
    public static List<T> AddItem<T>(this List<T> source, T item)
    {
        source.Add(item);
        return source;
    }

    /// <summary>
    /// 删除项,方便链式调用
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="item">项</param>
    /// <returns>原集合</returns>
    public static List<T> RemoveItem<T>(this List<T> source, T item)
    {
        source.Remove(item);
        return source;
    }

    /// <summary>
    /// 添加项,方便链式调用
    /// </summary>
    /// <typeparam name="TKey">Key泛型类型</typeparam>
    /// <typeparam name="TValue">Value泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="key">key</param>
    /// <param name="value">value</param>
    /// <returns>原字典</returns>
    public static Dictionary<TKey, TValue> AddItem<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)

    {
        source.Add(key, value);
        return source;
    }

    /// <summary>
    /// 删除项,方便链式调用
    /// </summary>
    /// <typeparam name="TKey">Key泛型类型</typeparam>
    /// <typeparam name="TValue">Value泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="key">key</param>
    /// <returns>原字典</returns>
    public static Dictionary<TKey, TValue> RemoveItem<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
    {
        source.Remove(key);
        return source;
    }

    /// <summary>
    /// 循环
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="action">action</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source.IsNullOrEmpty()) return;
        foreach (var item in source) action(item);
    }

    /// <summary>
    /// 根据对象的值（不是引用）去重
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="source">集合</param>
    /// <returns>去重的集合</returns>
    public static IEnumerable<T?> DistinctByObjectValue<T>(this IEnumerable<T?> source) where T : class => source.Distinct(new ObjectValueComparer<T>());

    /// <summary>
    /// 根据属性名称排序
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="query">query</param>
    /// <param name="sortPropertyName">排序属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns>IEnumerable</returns>
    public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string sortPropertyName, bool descending = false) where T : class => query.AsQueryable().OrderByDynamic(typeof(T).GetProperty(sortPropertyName), descending).AsEnumerable();

    /// <summary>
    /// 根据属性名称排序
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="query">query</param>
    /// <param name="sortPropertyName">排序属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns>IQueryable</returns>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortPropertyName, bool descending = false) where T : class => query.OrderByDynamic(typeof(T).GetProperty(sortPropertyName), descending);

    internal static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, PropertyInfo sortProperty, bool descending = false) where T : class => query.AsQueryable().OrderByDynamic(sortProperty, descending).AsEnumerable();

    internal static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, PropertyInfo sortProperty, bool descending = false) where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        string command = descending ? "OrderByDescending" : "OrderBy";
        var propertyAccess = Expression.MakeMemberAccess(parameter, sortProperty);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, [typeof(T), sortProperty.PropertyType], query.Expression, orderByExpression);
        return query.Provider.CreateQuery<T>(resultExpression);
    }

    internal class ObjectValueComparer<T> : IEqualityComparer<T?> where T : class
    {
        public bool Equals(T? x, T? y)
        {
            if (x is null && y is null) return true;
            return x?.Serialize() == y?.Serialize();
        }

        public int GetHashCode(T? obj)
        {
            return obj is null ? -1 : 1;
        }
    }
}