using System.Reflection;
using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// 树形结构项
/// </summary>
/// <typeparam name="TMetaData">元数据类型</typeparam>
[BelongDirectory("Tree")]
public class TreeItem<TMetaData> where TMetaData : class
{
    [JsonConstructor]
    internal TreeItem(TMetaData metaData, List<TreeItem<TMetaData>> children)
    {
        MetaData = metaData;
        Children = children;
    }

    internal TreeItem(TMetaData metaData, TreeBuildOption<TMetaData>? option)
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
    internal TreeBuildOption<TMetaData> Option { get; } = new TreeBuildOption<TMetaData>();

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
    public List<TreeItem<TMetaData>> Children { get; internal set; } = [];

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
                throw new InvalidOperationException($"cycle reference detected:'{string.Join("->", ids)}'");
            }
            current = current.Parent;
        }
    }
}