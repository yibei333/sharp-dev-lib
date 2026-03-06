using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 树形结构构建选项
/// </summary>
/// <param name="idPropertyName">Id属性名称</param>
/// <param name="parentIdPropertyName">父Id属性名称</param>
public class TreeOption(string idPropertyName = "Id", string parentIdPropertyName = "ParentId")
{
    readonly Dictionary<string, PropertyInfo> _cache = [];

    public static TreeOption? Default { get; set; } = new TreeOption();

    /// <summary>
    /// Id属性名称
    /// </summary>
    public string IdPropertyName { get; } = idPropertyName;

    /// <summary>
    /// 父Id属性名称
    /// </summary>
    public string ParentIdPropertyName { get; } = parentIdPropertyName;

    /// <summary>
    /// 排序属性名称
    /// </summary>
    public string? SortPropertyName { get; set; }

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool Descending { get; set; }

    internal PropertyInfo GetIdProperty(Type metaDataType)
    {
        if (_cache.ContainsKey(IdPropertyName)) return _cache[IdPropertyName];

        if (IdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"id property name required");
        var property = metaDataType.GetProperty(IdPropertyName) ?? throw new ArgumentException($"unable to find property '{IdPropertyName}' of type '{metaDataType.FullName}'");
        _cache[IdPropertyName] = property;
        return property;
    }

    internal PropertyInfo GetParentIdProperty(Type metaDataType)
    {
        if (_cache.ContainsKey(ParentIdPropertyName)) return _cache[ParentIdPropertyName];

        if (ParentIdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"parent id property name required");
        var property = metaDataType.GetProperty(ParentIdPropertyName) ?? throw new ArgumentException($"unable to find property '{ParentIdPropertyName}' of type '{metaDataType.FullName}'");
        _cache[ParentIdPropertyName] = property;
        return property;
    }
}