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
        if (source.IsNullOrEmpty()) return (false, null);
        var items = source.Select(x => new { Id = id(x), ParentId = parentId(x) });
        if (items.Any(x => x.Id is null)) throw new InvalidDataException("列表中的Id不能为null");

        List<TId> visited = [];

        foreach (var item in items)
        {
            if (item.ParentId is null)
            {
                visited.Add(item.Id);
                continue;
            }

            List<TId> pIds = [];
            var currentParentId = item.ParentId;
            while (true)
            {
                if (visited.Any(x => x!.Equals(currentParentId)))
                {
                    visited.AddRange(pIds);
                    break;
                }
                else
                {
                    pIds.Add(currentParentId!);
                    if (pIds.Any(x => x!.Equals(item.Id)))
                    {
                        pIds.Insert(0, item.Id);
                        return (true, string.Join("->", pIds));
                    }
                    var next = items.FirstOrDefault(x => x.Id!.Equals(currentParentId));
                    if (next is null) break;
                    currentParentId = next.ParentId;
                }
            }
            pIds.Insert(0, item.Id);
            visited.AddRange(pIds);
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
        var childrenProperty = properties.GetProperty(type, childrenPropertyName);
        var sortProperty = sortPropertyName.IsNullOrWhiteSpace() ? null : properties.GetProperty(type, sortPropertyName);
        if (childrenProperty.PropertyType != typeof(List<T>)) throw new InvalidDataException($"'{childrenPropertyName}'属性类型应该为List<{type.Name}>");

        var items = source.Select(x => new TreeItem<T>(x, idProperty, parentIdProperty, sortProperty));
        var repeateData = items.GroupBy(x => x.Id).Where(x => x.Count() > 1);
        if (repeateData.Any()) throw new InvalidDataException($"检测到重复数据:{string.Join(",", repeateData.Select(x => x.Key))}");
        var parents = items.Where(x => x.ParentId is null || items.All(y => y.Id != x.ParentId));
        if (sortProperty is not null)
        {
            if (descending) parents = parents.OrderByDescending(x => x.SortValue);
            else parents = parents.OrderBy(x => x.SortValue);
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