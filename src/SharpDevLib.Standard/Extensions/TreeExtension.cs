using System.Reflection;

namespace SharpDevLib.Standard;

/// <summary>
/// 树形结构项
/// </summary>
/// <typeparam name="TMetaData">元数据类型</typeparam>
public class TreeItem<TMetaData> where TMetaData : class
{
    TreeItem<TMetaData>? _parent;

    internal TreeItem(TMetaData metaData, PropertyInfo idProperty, PropertyInfo parentIdProperty, PropertyInfo? orderProperty, bool descending)
    {
        MetaData = metaData;
        OrderProperty = orderProperty;
        Descending = descending;
        Id = idProperty.GetValue(metaData);
        ParentId = idProperty.GetValue(parentIdProperty);
        if (orderProperty is not null) OrderValue = idProperty.GetValue(orderProperty);
        Descending = descending;
    }

    internal object Id { get; }
    internal object? ParentId { get; }
    internal object? OrderValue { get; }
    PropertyInfo? OrderProperty { get; }
    bool Descending { get; }

    /// <summary>
    /// 元数据
    /// </summary>
    public TMetaData MetaData { get; }

    /// <summary>
    /// 父项
    /// </summary>
    public TreeItem<TMetaData>? Parent
    {
        get => _parent;
        set
        {
            _parent?.Children?.Remove(this);
            _parent = value;
            if (_parent is not null)
            {
                EnsureNotCycle(_parent);
                _parent.Children ??= new List<TreeItem<TMetaData>>();
                _parent.Children.Add(this);
                if (OrderProperty is not null)
                {
                    _parent.Children.OrderByDynamic("OrderValue", Descending);
                }
            }
        }
    }

    /// <summary>
    /// 层级
    /// </summary>
    public int Level => (Parent?.Level ?? 0) + 1;

    /// <summary>
    /// 子项
    /// </summary>
    public List<TreeItem<TMetaData>>? Children { get; private set; }

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

    void EnsureNotCycle(TreeItem<TMetaData> parent)
    {
        var current = parent;
        var ids = new List<object> { Id };
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
/// 树形结构扩展
/// </summary>
public static class TreeExtension
{
    /// <summary>
    /// 将集合构建树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="items">集合</param>
    /// <param name="idPropertyName">Id属性名称</param>
    /// <param name="parentIdPropertyName">父Id属性名称</param>
    /// <param name="orderPropertyName">排序属性名称</param>
    /// <param name="descending">是否降序</param>
    /// <returns>树形结构集合</returns>
    public static List<TreeItem<TMetaData>> BuildTree<TMetaData>(this IEnumerable<TMetaData> items, string idPropertyName = "Id", string parentIdPropertyName = "ParentId", string orderPropertyName = "", bool descending = false) where TMetaData : class
    {
        var type = typeof(TMetaData);
        PropertyInfo idProperty = type.GetProperty(idPropertyName) ?? throw new ArgumentException($"unable to find property '{idPropertyName}' of type '{type.FullName}'");
        PropertyInfo parentIdProperty = type.GetProperty(parentIdPropertyName) ?? throw new ArgumentException($"unable to find property '{parentIdPropertyName}' of type '{type.FullName}'");
        PropertyInfo? orderProperty = orderPropertyName.IsNullOrWhiteSpace() ? null : (type.GetProperty(parentIdPropertyName) ?? throw new ArgumentException($"unable to find property '{parentIdPropertyName}' of type '{type.FullName}'"));
        var list = items.Select(x => new TreeItem<TMetaData>(x, idProperty, parentIdProperty, orderProperty, descending));
        var repeated = list.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        if (repeated.Any()) throw new InvalidDataException($"repeat id detected:'{(string.Join(",", list))}'");
        foreach (var item in list)
        {
            if (item.ParentId is not null)
            {
                var parentItem = list.FirstOrDefault(x => x.Id == item.ParentId);
                item.Parent = parentItem;
            }
        }
        return list.Where(x => x.Parent is null).ToList();
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
}
