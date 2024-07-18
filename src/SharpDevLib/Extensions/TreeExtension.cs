using System.Reflection;
using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// 树形结构项
/// </summary>
/// <typeparam name="TMetaData">元数据类型</typeparam>
public class TreeItem<TMetaData> where TMetaData : class
{
    [JsonConstructor]
    internal TreeItem(TMetaData metaData, List<TreeItem<TMetaData>> children)
    {
        MetaData = metaData;
        Children = children;
    }

    internal TreeItem(TMetaData metaData, BuildTreeOption<TMetaData>? option)
    {
        MetaData = metaData;
        if (option is not null) Option = option;

        Id = ToStringValue(Option.IdProperty, metaData, true);
        ParentId = ToStringValue(Option.ParentIdProperty, metaData);
        if (Option.TreeItemSortProperty is not null) SortValue = Option.MetaDataSortProperty?.GetValue(metaData);
    }

    internal string? Id { get; }
    internal string? ParentId { get; }
    internal object? SortValue { get; }
    internal BuildTreeOption<TMetaData> Option { get; } = new BuildTreeOption<TMetaData>();

    /// <summary>
    /// 元数据
    /// </summary>
    [JsonPropertyOrder(0)]
    public TMetaData MetaData { get; }

    /// <summary>
    /// 父项
    /// </summary>
    [JsonIgnore]
    public TreeItem<TMetaData>? Parent { get; private set; }

    /// <summary>
    /// 层级
    /// </summary>
    [JsonPropertyOrder(1)]
    public int Level => (Parent?.Level ?? 0) + 1;

    /// <summary>
    /// 子项
    /// </summary>
    [JsonPropertyOrder(2)]
    public List<TreeItem<TMetaData>> Children { get; internal set; } = new();

    /// <summary>
    /// 转换为元数据集合
    /// </summary>
    /// <returns>元数据集合</returns>
    public List<TMetaData> ToMetaDataList()
    {
        var list = new List<TMetaData> { MetaData };
        Children?.ForEach(x => list.AddRange(x.ToMetaDataList()));
        return list;
    }

    /// <summary>
    /// 转换为平级集合
    /// </summary>
    /// <returns>平级集合</returns>
    public List<TreeItem<TMetaData>> ToFlatList()
    {
        var list = new List<TreeItem<TMetaData>> { this };
        Children?.ForEach(x => list.AddRange(x.ToFlatList()));
        return list;
    }

    /// <summary>
    /// 设置父项
    /// </summary>
    /// <param name="parent">父项</param>
    public void SetParent(TreeItem<TMetaData> parent) => SetParent(parent, true);

    string? ToStringValue(PropertyInfo propertyInfo, TMetaData metaData, bool required = false)
    {
        var data = propertyInfo.GetValue(metaData);
        string? result = null;
        if (data is not null)
        {
            if (propertyInfo.PropertyType.IsClass)
            {
                if (propertyInfo.PropertyType == typeof(string)) result = data.ToString();
                else result = data.Serialize();
            }
            else result = data.ToString();
        }
        if (result.IsNullOrWhiteSpace() && required) throw new NullReferenceException($"property '{propertyInfo.Name}' should not be null or empty");
        return result;
    }

    internal void SetParent(TreeItem<TMetaData> parent, bool sort)
    {
        Parent?.Children?.Remove(this);
        Parent = parent;
        if (Parent is not null)
        {
            EnsureNotCycle(Parent);
            Parent.Children.Add(this);
            if (sort) Parent.Children = Parent.Children.SortTree();
        }
    }

    void EnsureNotCycle(TreeItem<TMetaData> parent)
    {
        var current = parent;
        var ids = new List<object?> { Id };
        while (current is not null)
        {
            ids.Add(current.Id);
            if (current.Equals(this))
            {
                throw new InvalidOperationException($"cycle reference detected:'{(string.Join("->", ids))}'");
            }
            current = current.Parent;
        }
    }
}

/// <summary>
/// 构建属性结构选项
/// </summary>
public class BuildTreeOption<TMetaData> where TMetaData : class
{
    string _sortPropertyName = string.Empty;
    readonly Type _type = typeof(TMetaData);
    readonly Dictionary<string, PropertyInfo> _cache = new();

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
                MetaDataSortProperty = (_type.GetProperty(_sortPropertyName) ?? throw new ArgumentException($"unable to find property '{_sortPropertyName}' of type '{_type.FullName}'"));
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
            var property = (_type.GetProperty(IdPropertyName) ?? throw new ArgumentException($"unable to find property '{IdPropertyName}' of type '{_type.FullName}'"));
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
            var property = (_type.GetProperty(ParentIdPropertyName) ?? throw new ArgumentException($"unable to find property '{ParentIdPropertyName}' of type '{_type.FullName}'"));
            _cache[ParentIdPropertyName] = property;
            return property;
        }
    }
    internal PropertyInfo? MetaDataSortProperty { get; private set; }
    internal PropertyInfo? TreeItemSortProperty { get; private set; }
}

/// <summary>
/// 树形结构扩展
/// </summary>
public static class Tree
{
    /// <summary>
    /// 将集合构建树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="items">集合</param>
    /// <param name="option">选项</param>
    /// <returns>树形结构集合</returns>
    public static List<TreeItem<TMetaData>> BuildTree<TMetaData>(this IEnumerable<TMetaData> items, BuildTreeOption<TMetaData>? option = null) where TMetaData : class
    {
        var list = items.Select(x => new TreeItem<TMetaData>(x, option)).ToList();
        var repeated = list.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        if (repeated.Any()) throw new InvalidDataException($"repeat id detected:'{(string.Join(",", repeated))}'");
        foreach (var item in list)
        {
            if (item.ParentId is not null) item.SetParent(list.FirstOrDefault(x => x.Id == item.ParentId), false);
        }
        return list.Where(x => x.Parent is null).ToList().SortTree();
    }

    /// <summary>
    /// 反序列化json为树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="treeJson">json</param>
    /// <param name="option">选项</param>
    /// <returns>树形结构集合</returns>
    public static List<TreeItem<TMetaData>> DeSerializeTree<TMetaData>(this string treeJson, BuildTreeOption<TMetaData>? option = null) where TMetaData : class
    {
        var items = treeJson.DeSerialize<List<TreeItem<TMetaData>>>();
        return items.ToMetaDataList().BuildTree(option);
    }

    /// <summary>
    /// 将树形结构转换为元数据集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="tree">树形结构</param>
    /// <returns>元数据集合</returns>
    public static List<TMetaData> ToMetaDataList<TMetaData>(this List<TreeItem<TMetaData>> tree) where TMetaData : class
    {
        return tree.SelectMany(x => x.ToMetaDataList()).ToList();
    }

    /// <summary>
    /// 将树形结构转换为平级结构
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="tree">树形结构</param>
    /// <returns>平级结构</returns>
    public static List<TreeItem<TMetaData>> ToFlatList<TMetaData>(this List<TreeItem<TMetaData>> tree) where TMetaData : class
    {
        return tree.SelectMany(x => x.ToFlatList()).ToList();
    }

    internal static List<TreeItem<TMetaData>> SortTree<TMetaData>(this List<TreeItem<TMetaData>> items) where TMetaData : class
    {
        items.ForEach(child => child.Children = child.Children.SortTree());
        if (items.Count <= 1) return items;
        var firstItem = items.FirstOrDefault();
        if (firstItem.Option.TreeItemSortProperty is null) return items;
        return items.OrderByDynamic(firstItem.Option.TreeItemSortProperty, firstItem.Option.Descending).ToList();
    }
}
