using SharpDevLib;

namespace SharpDevLib;

/// <summary>
/// 树形结构扩展，提供树形结构的构建、序列化和转换功能
/// </summary>
public static class TreeHelper
{
    /// <summary>
    /// 将平级集合构建为树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="items">要构建为树形结构的平级集合</param>
    /// <param name="option">树形结构构建选项，用于指定ID属性名、父ID属性名和排序方式</param>
    /// <returns>树形结构集合，包含所有根节点及其子节点</returns>
    /// <exception cref="InvalidDataException">当集合中存在重复ID或检测到循环引用时引发异常</exception>
    public static List<TreeItem<TMetaData>> BuildTree<TMetaData>(this IEnumerable<TMetaData> items, TreeOption? option = null) where TMetaData : class
    {
        var list = items.Where(x => x is not null).Select(x => new TreeItem<TMetaData>(x, option)).ToList();
        var repeated = list.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        if (repeated.Any()) throw new InvalidDataException($"检测到重复ID:'{string.Join(",", repeated)}'");
        foreach (var item in list)
        {
            if (item.ParentId is not null) item.SetParent(list.FirstOrDefault(x => x.Id == item.ParentId));
        }
        var treeOption = option ?? TreeOption.Default;
        return list.Where(x => x.Parent is null).ToList().SortTree(treeOption?.Descending ?? false);
    }

    /// <summary>
    /// 反序列化JSON字符串为树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="treeJson">包含树形结构的JSON字符串</param>
    /// <param name="option">树形结构构建选项</param>
    /// <returns>树形结构集合</returns>
    public static List<TreeItem<TMetaData>> DeSerializeTree<TMetaData>(this string treeJson, TreeOption? option = null) where TMetaData : class
    {
        var items = treeJson.DeSerialize<List<TreeItem<TMetaData>>>();
        return items.ToMetaDataList().BuildTree(option);
    }

    /// <summary>
    /// 将树形结构转换为元数据平级集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="tree">树形结构集合</param>
    /// <returns>包含所有节点的元数据平级集合，保持树形结构中的层级顺序</returns>
    public static List<TMetaData> ToMetaDataList<TMetaData>(this List<TreeItem<TMetaData>> tree) where TMetaData : class
    {
        return [.. tree.SelectMany(x => x.ToMetaDataList())];
    }

    /// <summary>
    /// 将树形结构转换为平级树项集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="tree">树形结构集合</param>
    /// <returns>包含所有树项的平级集合，保持树形结构中的层级顺序</returns>
    public static List<TreeItem<TMetaData>> ToFlatList<TMetaData>(this List<TreeItem<TMetaData>> tree) where TMetaData : class
    {
        return [.. tree.SelectMany(x => x.ToFlatList())];
    }

    internal static List<TreeItem<TMetaData>> SortTree<TMetaData>(this List<TreeItem<TMetaData>> items, bool descending) where TMetaData : class
    {
        var sortProperty = items.FirstOrDefault()?.SortProperty;
        if (sortProperty is null) return items;
        items.ForEach(child => child.Children = child.Children.SortTree(descending));
        if (items.Count <= 1) return items;
        var firstItem = items.FirstOrDefault();
        return [.. items.OrderByDynamic(sortProperty, descending)];
    }
}
