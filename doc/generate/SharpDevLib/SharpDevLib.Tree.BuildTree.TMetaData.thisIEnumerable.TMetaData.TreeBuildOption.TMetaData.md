###### [主页](./Index.md "主页")

#### BuildTree\<TMetaData\>(this IEnumerable\<TMetaData\> items, TreeBuildOption\<TMetaData\> option) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [Tree](./SharpDevLib.Tree.md "Tree")

``` csharp
public static List<TreeItem<TMetaData>> BuildTree<TMetaData>(this IEnumerable<TMetaData> items, TreeBuildOption<TMetaData> option)
    where TMetaData : class
```

**注释**

*将集合构建树形结构集合*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|TMetaData|元数据类型|`class`|




**返回类型** : [List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>


**参数**

|名称|类型|注释|
|---|---|---|
|items|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<TMetaData\>|集合|
|option|[TreeBuildOption](./SharpDevLib.TreeBuildOption.1.md "TreeBuildOption")\<TMetaData\>|选项|


