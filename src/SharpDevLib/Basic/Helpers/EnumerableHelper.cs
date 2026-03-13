using System.Linq.Expressions;
using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 集合扩展，提供集合操作和动态排序等功能
/// </summary>
public static class EnumerableHelper
{
    /// <summary>
    /// 向列表添加项并返回原列表，支持链式调用
    /// </summary>
    /// <typeparam name="T">列表项类型</typeparam>
    /// <param name="source">源列表</param>
    /// <param name="item">要添加的项</param>
    /// <returns>添加项后的原列表，用于链式调用</returns>
    public static List<T> AddItem<T>(this List<T> source, T item)
    {
        source.Add(item);
        return source;
    }

    /// <summary>
    /// 从列表中删除指定项并返回原列表，支持链式调用
    /// </summary>
    /// <typeparam name="T">列表项类型</typeparam>
    /// <param name="source">源列表</param>
    /// <param name="item">要删除的项</param>
    /// <returns>删除项后的原列表，用于链式调用</returns>
    public static List<T> RemoveItem<T>(this List<T> source, T item)
    {
        source.Remove(item);
        return source;
    }

    /// <summary>
    /// 向字典添加键值对并返回原字典，支持链式调用
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="source">源字典</param>
    /// <param name="key">要添加的键</param>
    /// <param name="value">要添加的值</param>
    /// <returns>添加键值对后的原字典，用于链式调用</returns>
    /// <exception cref="ArgumentException">当字典中已存在相同键时抛出</exception>
    public static Dictionary<TKey, TValue> AddItem<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)

    {
        source.Add(key, value);
        return source;
    }

    /// <summary>
    /// 从字典中删除指定键并返回原字典，支持链式调用
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="source">源字典</param>
    /// <param name="key">要删除的键</param>
    /// <returns>删除键后的原字典，用于链式调用</returns>
    public static Dictionary<TKey, TValue> RemoveItem<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
    {
        source.Remove(key);
        return source;
    }

    /// <summary>
    /// 对集合中的每个元素执行指定操作
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">源集合</param>
    /// <param name="action">要对每个元素执行的操作</param>
    public static void ForEach<T>(this IEnumerable<T>? source, Action<T> action)
    {
        if (source is null) return;
        foreach (var item in source) action(item);
    }

    /// <summary>
    /// 对集合中的每个元素执行指定操作，包含元素索引
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">源集合</param>
    /// <param name="action">要对每个元素执行的操作，第一个参数为元素索引，第二个参数为元素</param>
    public static void ForEach<T>(this IEnumerable<T>? source, Action<int, T> action)
    {
        if (source is null) return;
        var index = 0;
        foreach (var item in source) action(index++, item);
    }

    /// <summary>
    /// 根据对象的值（而非引用）对集合进行去重
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">源集合</param>
    /// <returns>去重后的集合</returns>
    public static IEnumerable<T?> DistinctByObjectValue<T>(this IEnumerable<T?> source) where T : class => source.Distinct(new ObjectValueComparer<T>());

    /// <summary>
    /// 根据属性名称对集合进行动态排序
    /// </summary>
    /// <typeparam name="T">集合元素类型，必须为引用类型</typeparam>
    /// <param name="query">源集合</param>
    /// <param name="sortPropertyName">排序属性的名称</param>
    /// <param name="descending">是否降序排序，默认为false（升序）</param>
    /// <returns>排序后的集合</returns>
    /// <exception cref="ArgumentNullException">当属性名称为null或找不到属性时抛出</exception>
    public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string sortPropertyName, bool descending = false) where T : class => query.AsQueryable().OrderByDynamic(typeof(T).GetProperty(sortPropertyName), descending).AsEnumerable();

    /// <summary>
    /// 根据属性名称对查询进行动态排序
    /// </summary>
    /// <typeparam name="T">集合元素类型，必须为引用类型</typeparam>
    /// <param name="query">源查询</param>
    /// <param name="sortPropertyName">排序属性的名称</param>
    /// <param name="descending">是否降序排序，默认为false（升序）</param>
    /// <returns>排序后的查询</returns>
    /// <exception cref="ArgumentNullException">当属性名称为null或找不到属性时抛出</exception>
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
        static readonly PropertyInfo[] _properties = [.. typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead)];

        public bool Equals(T? x, T? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            foreach (var prop in _properties)
            {
                var valX = prop.GetValue(x);
                var valY = prop.GetValue(y);
                if (!Equals(valX, valY)) return false;
            }
            return true;
        }

        public int GetHashCode(T? obj)
        {
            if (obj is null) return 0;
            var hashCode = new HashCode();
            foreach (var prop in _properties)
            {
                hashCode.Add(prop.GetValue(obj));
            }
            return hashCode.ToHashCode();
        }
    }
}