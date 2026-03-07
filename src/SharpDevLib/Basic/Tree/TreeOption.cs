using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 树形结构构建选项，用于自定义树形结构的构建行为
/// </summary>
/// <param name="idPropertyName">标识节点的属性名称，默认为"Id"</param>
/// <param name="parentIdPropertyName">标识父节点的属性名称，默认为"ParentId"</param>
public class TreeOption(string idPropertyName = "Id", string parentIdPropertyName = "ParentId")
{
    readonly Dictionary<string, PropertyInfo> _cache = [];

    /// <summary>
    /// 获取或设置默认的树形结构构建选项
    /// </summary>
    public static TreeOption? Default { get; set; } = new TreeOption();

    /// <summary>
    /// 获取标识节点的属性名称
    /// </summary>
    public string IdPropertyName { get; } = idPropertyName;

    /// <summary>
    /// 获取标识父节点的属性名称
    /// </summary>
    public string ParentIdPropertyName { get; } = parentIdPropertyName;

    /// <summary>
    /// 获取或设置用于排序的属性名称，如果为null则不进行排序
    /// </summary>
    public string? SortPropertyName { get; set; }

    /// <summary>
    /// 获取或设置是否按降序排序，默认为false
    /// </summary>
    public bool Descending { get; set; }

    internal PropertyInfo GetIdProperty(Type metaDataType)
    {
        if (_cache.ContainsKey(IdPropertyName)) return _cache[IdPropertyName];

        if (IdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"ID属性名称不能为空");
        var property = metaDataType.GetProperty(IdPropertyName) ?? throw new ArgumentException($"无法在类型'{metaDataType.FullName}'中找到属性'{IdPropertyName}'");
        _cache[IdPropertyName] = property;
        return property;
    }

    internal PropertyInfo GetParentIdProperty(Type metaDataType)
    {
        if (_cache.ContainsKey(ParentIdPropertyName)) return _cache[ParentIdPropertyName];

        if (ParentIdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"父ID属性名称不能为空");
        var property = metaDataType.GetProperty(ParentIdPropertyName) ?? throw new ArgumentException($"无法在类型'{metaDataType.FullName}'中找到属性'{ParentIdPropertyName}'");
        _cache[ParentIdPropertyName] = property;
        return property;
    }

    internal PropertyInfo? GetSortProperty(Type metaDataType)
    {
        if (SortPropertyName.IsNullOrWhiteSpace()) return null;
        if (_cache.ContainsKey(SortPropertyName)) return _cache[SortPropertyName];
        var property = metaDataType.GetProperty(SortPropertyName) ?? throw new ArgumentException($"无法在类型'{metaDataType.FullName}'中找到属性'{SortPropertyName}'");
        _cache[SortPropertyName] = property;
        return property;
    }
}