using SharpDevLib;

namespace SharpDevLib;

/// <summary>
/// 树形结构扩展
/// </summary>
[BelongDirectory("Tree")]
public static class Tree
{
    /// <summary>
    /// 将集合构建树形结构集合
    /// </summary>
    /// <typeparam name="TMetaData">元数据类型</typeparam>
    /// <param name="items">集合</param>
    /// <param name="option">选项</param>
    /// <returns>树形结构集合</returns>
    /// <exception cref="InvalidDataException">当出现循环引用时引发异常</exception>
    public static List<TreeItem<TMetaData>> BuildTree<TMetaData>(this IEnumerable<TMetaData> items, TreeBuildOption<TMetaData>? option = null) where TMetaData : class
    {
        var list = items.Select(x => new TreeItem<TMetaData>(x, option)).ToList();
        var repeated = list.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        if (repeated.Any()) throw new InvalidDataException($"repeat id detected:'{string.Join(",", repeated)}'");
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
    public static List<TreeItem<TMetaData>> DeSerializeTree<TMetaData>(this string treeJson, TreeBuildOption<TMetaData>? option = null) where TMetaData : class
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
