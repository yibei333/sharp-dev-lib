using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 树形结构构建选项
/// </summary>
/// <typeparam name="TMetaData">元数据类型</typeparam>
[BelongDirectory("Tree")]
public class TreeBuildOption<TMetaData> where TMetaData : class
{
    string _sortPropertyName = string.Empty;
    readonly Type _type = typeof(TMetaData);
    readonly Dictionary<string, PropertyInfo> _cache = [];

    /// <summary>
    /// Id属性名称
    /// </summary>
    public string IdPropertyName { get; set; } = "Id";

    /// <summary>
    /// 父Id属性名称
    /// </summary>
    public string ParentIdPropertyName { get; set; } = "ParentId";

    /// <summary>
    /// 排序属性名称
    /// </summary>
    public string SortPropertyName
    {
        get => _sortPropertyName;
        set
        {
            _sortPropertyName = value;
            if (_sortPropertyName.NotNullOrWhiteSpace())
            {
                MetaDataSortProperty = _type.GetProperty(_sortPropertyName) ?? throw new ArgumentException($"unable to find property '{_sortPropertyName}' of type '{_type.FullName}'");
                TreeItemSortProperty = typeof(TreeItem<TMetaData>).GetProperty(nameof(TreeItem<TMetaData>.SortValue), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }
        }
    }

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool Descending { get; set; }

    internal PropertyInfo IdProperty
    {
        get
        {
            if (_cache.ContainsKey(IdPropertyName)) return _cache[IdPropertyName];

            if (IdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"id property name required");
            var property = _type.GetProperty(IdPropertyName) ?? throw new ArgumentException($"unable to find property '{IdPropertyName}' of type '{_type.FullName}'");
            _cache[IdPropertyName] = property;
            return property;
        }
    }
    internal PropertyInfo ParentIdProperty
    {
        get
        {
            if (_cache.ContainsKey(ParentIdPropertyName)) return _cache[ParentIdPropertyName];

            if (ParentIdPropertyName.IsNullOrWhiteSpace()) throw new NullReferenceException($"parent id property name required");
            var property = _type.GetProperty(ParentIdPropertyName) ?? throw new ArgumentException($"unable to find property '{ParentIdPropertyName}' of type '{_type.FullName}'");
            _cache[ParentIdPropertyName] = property;
            return property;
        }
    }
    internal PropertyInfo? MetaDataSortProperty { get; private set; }
    internal PropertyInfo? TreeItemSortProperty { get; private set; }
}