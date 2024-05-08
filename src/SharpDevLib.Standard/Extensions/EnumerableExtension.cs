using System.Linq.Expressions;

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
    public static IEnumerable<T?> DistinctByObjectValue<T>(this IEnumerable<T?> source) where T : class => source.Distinct(new ObjectValueComparer<T>());

    /// <summary>
    /// 根据属性名称排序
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="query">query</param>
    /// <param name="sortPropertyName">排序属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns>IQueryable</returns>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortPropertyName, bool descending = false) where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        string command = descending ? "OrderByDescending" : "OrderBy";
        var property = typeof(T).GetProperty(sortPropertyName);
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));
        return query.Provider.CreateQuery<T>(resultExpression);
    }

    /// <summary>
    /// 根据属性名称排序
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="query">query</param>
    /// <param name="sortPropertyName">排序属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns>IEnumerable</returns>
    public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string sortPropertyName, bool descending = false) where T : class=>query.AsQueryable().OrderByDynamic(sortPropertyName, descending).AsEnumerable();
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