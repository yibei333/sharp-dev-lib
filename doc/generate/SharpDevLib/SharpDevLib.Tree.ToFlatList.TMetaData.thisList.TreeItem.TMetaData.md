###### [主页](./Index.md "主页")
#### ToFlatList\<TMetaData\>(this List\<TreeItem\<TMetaData\>\> tree) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [Tree](./SharpDevLib.Tree.md "Tree")
``` csharp
public static List<TreeItem<TMetaData>> ToFlatList<TMetaData>(this List<TreeItem<TMetaData>> tree)
    where TMetaData : class
```
**注释**
*将树形结构转换为平级结构*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TMetaData|元数据类型|`class`|


**返回类型** : [List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>

**参数**
|名称|类型|注释|
|---|---|---|
|tree|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|树形结构|

