using System.Reflection;
using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// 树形结构项，表示树形结构中的单个节点
/// </summary>
/// <typeparam name="TMetaData">元数据类型，存储节点的自定义数据</typeparam>
public class TreeItem<TMetaData> where TMetaData : class
{
    [JsonConstructor]
    internal TreeItem(TMetaData metaData, List<TreeItem<TMetaData>> children)
    {
        MetaData = metaData;
        Children = children;
    }

    internal TreeItem(TMetaData metaData, TreeOption? option = null)
    {
        option ??= TreeOption.Default ?? new TreeOption();
        MetaData = metaData;
        var type = typeof(TMetaData);
        Id = ToStringValue(option.GetIdProperty(type), metaData, true);
        ParentId = ToStringValue(option.GetParentIdProperty(type), metaData);
        if (option.SortPropertyName.NotNullOrWhiteSpace())
        {
            SortProperty = this.GetType().GetProperty(nameof(SortValue));
            SortValue = option.GetSortProperty(type)?.GetValue(metaData);
        }
    }

    internal string? Id { get; }
    internal string? ParentId { get; }
    internal object? SortValue { get; }
    internal PropertyInfo? SortProperty { get; }

    /// <summary>
    /// 获取节点的元数据，存储节点的自定义信息
    /// </summary>
    [JsonPropertyOrder(0)]
    public TMetaData MetaData { get; }

    /// <summary>
    /// 获取父节点，如果没有父节点则为null
    /// </summary>
    [JsonIgnore]
    public TreeItem<TMetaData>? Parent { get; private set; }

    /// <summary>
    /// 获取节点在树形结构中的层级，根节点层级为1
    /// </summary>
    [JsonPropertyOrder(1)]
    public int Level => (Parent?.Level ?? 0) + 1;

    /// <summary>
    /// 获取或设置子节点集合
    /// </summary>
    [JsonPropertyOrder(2)]
    public List<TreeItem<TMetaData>> Children { get; internal set; } = [];

    /// <summary>
    /// 将当前节点及其所有子节点转换为元数据平级集合
    /// </summary>
    /// <returns>包含当前节点及其所有子孙节点的元数据集合</returns>
    public List<TMetaData> ToMetaDataList()
    {
        var list = new List<TMetaData> { MetaData };
        Children?.ForEach(x => list.AddRange(x.ToMetaDataList()));
        return list;
    }

    /// <summary>
    /// 将当前节点及其所有子节点转换为树项平级集合
    /// </summary>
    /// <returns>包含当前节点及其所有子孙节点的树项集合</returns>
    public List<TreeItem<TMetaData>> ToFlatList()
    {
        var list = new List<TreeItem<TMetaData>> { this };
        Children?.ForEach(x => list.AddRange(x.ToFlatList()));
        return list;
    }

    /// <summary>
    /// 设置当前节点的父节点
    /// </summary>
    /// <param name="parent">要设置的父节点，为null时将当前节点设置为根节点</param>
    /// <exception cref="InvalidOperationException">当设置父节点会导致循环引用时引发异常</exception>
    public void SetParent(TreeItem<TMetaData>? parent)
    {
        Parent?.Children?.Remove(this);
        Parent = parent;
        if (Parent is not null)
        {
            EnsureNotCycle(Parent);
            Parent.Children.Add(this);
        }
    }

    /// <summary>
    /// 向当前节点添加子节点
    /// </summary>
    /// <param name="child">要添加的子节点</param>
    /// <exception cref="InvalidOperationException">当添加子节点会导致循环引用时引发异常</exception>
    public void AddChild(TreeItem<TMetaData> child)
    {
        child.SetParent(this);
    }

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