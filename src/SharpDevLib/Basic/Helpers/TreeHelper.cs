using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 树形结构Helper
/// </summary>
public static class TreeHelper
{
    /// <summary>
    /// 检测列表中是否有循环引用
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TId">Id类型</typeparam>
    /// <param name="source">列表</param>
    /// <param name="id">获取Id</param>
    /// <param name="parentId">获取ParentId</param>
    /// <returns>返回是否包含循环引用及具体的Id</returns>
    public static (bool, string?) HasCycleReference<T, TId>(this IEnumerable<T> source, Func<T, TId> id, Func<T, TId?> parentId)
    {
        var type = typeof(TId);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"TId当前只支持值类型和string类型");
        if (source.IsNullOrEmpty()) return (false, null);
        Dictionary<TId, TId?> items = [];
        source.ForEach(x =>
        {
            items.Add(id(x) ?? throw new InvalidDataException("列表中的Id不能为null"), parentId(x));
        });
        HashSet<TId> visited = [];

        foreach (var item in items)
        {
            if (item.Value is null)
            {
                visited.Add(item.Key);
                continue;
            }

            HashSet<TId> pIds = [];
            var currentParentId = item.Value;
            while (true)
            {
                if (visited.Contains(currentParentId)) break;
                else
                {
                    pIds.Add(currentParentId!);
                    visited.Add(currentParentId!);
                    if (pIds.Contains(item.Key))
                    {
                        return (true, $"{item.Key}->" + string.Join("->", pIds));
                    }
                    if (!items.TryGetValue(currentParentId!, out var nextItem) || nextItem is null) break;
                    currentParentId = nextItem;
                }
            }
            visited.Add(item.Key);
        }
        return (false, null);
    }

    /// <summary>
    /// 构建树形结构,循环引用节点将不会出现在结果中,可以先调用TreeHelper.HasCycleReference方法确保没有循环引用
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="source">不带树形结构的列表</param>
    /// <param name="idPropertyName">Id属性名称,默认为Id</param>
    /// <param name="parentIdPropertyName">父Id属性名称,默认为ParentId</param>
    /// <param name="childrenPropertyName">子容器属性名称,默认为Children</param>
    /// <param name="sortPropertyName">排序属性名称,为空不排序</param>
    /// <param name="descending">是否降序,默认False,仅当设置了sortPropertyName时有用</param>
    /// <param name="setNullWhenHasNoChildren">没有子节点是否设置容器为null,默认为true
    /// <para>true-如果没有子节点,则设置容器为null</para>
    /// <para>false-如果没有子节点,则设置子容器为空集合</para>
    /// </param>
    /// <returns>属性结构集合</returns>
    public static List<T> BuildTree<T>
    (
        this IEnumerable<T> source,
        string idPropertyName = "Id",
        string parentIdPropertyName = "ParentId",
        string childrenPropertyName = "Children",
        string sortPropertyName = "",
        bool descending = false,
        bool setNullWhenHasNoChildren = true
    ) where T : class
    {
        if (source.IsNullOrEmpty()) return [.. source];
        var type = typeof(T);
        var index = 0;
        var properties = type
            .GetProperties()
            .Where(x => x.CanRead && x.CanWrite)
            .Select(x => new { Property = x, Index = index++ })
            .GroupBy(x => x.Property.Name)//handle property with new keyword
            .Select(x => x.OrderBy(x => x.Index).First().Property);
        var idProperty = properties.GetProperty(type, idPropertyName);
        var parentIdProperty = properties.GetProperty(type, parentIdPropertyName);
        if ((!idProperty.PropertyType.IsValueType && idProperty.PropertyType != typeof(string)) || (!parentIdProperty.PropertyType.IsValueType && parentIdProperty.PropertyType != typeof(string))) throw new Exception($"'{idPropertyName}'和'{parentIdPropertyName}'当前只支持值类型和string类型");
        var childrenProperty = properties.GetProperty(type, childrenPropertyName);
        var sortProperty = sortPropertyName.IsNullOrWhiteSpace() ? null : properties.GetProperty(type, sortPropertyName);
        if (childrenProperty.PropertyType != typeof(List<T>)) throw new InvalidDataException($"'{childrenPropertyName}'属性类型应该为List<{type.Name}>");

        var items = source.Select(x => new TreeItem<T>(x, idProperty, parentIdProperty, sortProperty));
        var repeateData = items.GroupBy(x => x.Id).Where(x => x.Count() > 1);
        if (repeateData.Any()) throw new InvalidDataException($"检测到重复数据:{string.Join(",", repeateData.Select(x => x.Key))}");
        var idSet = new HashSet<object>(items.Select(item => item.Id));
        var parents = items.Where(item => item.ParentId is null || !idSet.Contains(item.ParentId)).ToList();
        if (sortProperty is not null)
        {
            if (descending) parents = [.. parents.OrderByDescending(x => x.SortValue)];
            else parents = [.. parents.OrderBy(x => x.SortValue)];
        }
        parents.ForEach(x => RecursiveBuild(items, x, childrenProperty, sortProperty, descending, setNullWhenHasNoChildren));
        return [.. parents.Select(x => x.MetaData)];
    }

    static void RecursiveBuild<T>(IEnumerable<TreeItem<T>> source, TreeItem<T> current, PropertyInfo childrentProperty, PropertyInfo? sortProperty, bool descending, bool setNullWhenHasNoChildren)
    {
        var children = source.Where(x => x.ParentId == current.Id);
        if (children.NotNullOrEmpty())
        {
            var childrentList = new List<T>();
            if (sortProperty is not null)
            {
                if (descending) children = children.OrderByDescending(x => x.SortValue);
                else children = children.OrderBy(x => x.SortValue);
            }
            children.ForEach(x =>
            {
                RecursiveBuild(source, x, childrentProperty, sortProperty, descending, setNullWhenHasNoChildren);
                childrentList.Add(x.MetaData);
            });
            childrentProperty.SetValue(current.MetaData, childrentList);
        }
        else
        {
            var childrentList = setNullWhenHasNoChildren ? null : new List<T>();
            childrentProperty.SetValue(current.MetaData, childrentList);
        }
    }

    static PropertyInfo GetProperty(this IEnumerable<PropertyInfo> properties, Type type, string propertyName)
    {
        return properties.FirstOrDefault(x => x.Name == propertyName) ?? throw new InvalidDataException($"在类型'{type.FullName}'中找不到属性'{propertyName}',确认可读/可写");
    }

    class TreeItem<T>
    {
        public TreeItem(T metaData, PropertyInfo idProperty, PropertyInfo parentIdProperty, PropertyInfo? sortProperty)
        {
            Id = idProperty.GetValue(metaData)?.ToString() ?? throw new InvalidDataException($"{idProperty.Name}不支持null值");
            if (Id.ToString().IsNullOrWhiteSpace()) throw new InvalidDataException($"{idProperty.Name}不支持空字符串");
            ParentId = parentIdProperty.GetValue(metaData)?.ToString();
            if (sortProperty is not null) SortValue = sortProperty.GetValue(metaData);
            MetaData = metaData;
        }

        public string Id { get; set; }
        public string? ParentId { get; set; }
        public object? SortValue { get; set; }
        public T MetaData { get; set; }
    }
}