###### [主页](./Index.md "主页")
## Tree 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class Tree : Object
```
**注释**
*树形结构扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[BuildTree\<TMetaData\>(this IEnumerable\<TMetaData\> items, TreeBuildOption\<TMetaData\> option)](./SharpDevLib.Tree.BuildTree.TMetaData.thisIEnumerable.TMetaData.TreeBuildOption.TMetaData.md "BuildTree<TMetaData>(this IEnumerable<TMetaData> items, TreeBuildOption<TMetaData> option)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|`public`|`是`|items:集合<br>option:选项|
|[DeSerializeTree\<TMetaData\>(this String treeJson, TreeBuildOption\<TMetaData\> option)](./SharpDevLib.Tree.DeSerializeTree.TMetaData.thisString.TreeBuildOption.TMetaData.md "DeSerializeTree<TMetaData>(this String treeJson, TreeBuildOption<TMetaData> option)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|`public`|`是`|treeJson:json<br>option:选项|
|[ToMetaDataList\<TMetaData\>(this List\<TreeItem\<TMetaData\>\> tree)](./SharpDevLib.Tree.ToMetaDataList.TMetaData.thisList.TreeItem.TMetaData.md "ToMetaDataList<TMetaData>(this List<TreeItem<TMetaData>> tree)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<TMetaData\>|`public`|`是`|tree:树形结构|
|[ToFlatList\<TMetaData\>(this List\<TreeItem\<TMetaData\>\> tree)](./SharpDevLib.Tree.ToFlatList.TMetaData.thisList.TreeItem.TMetaData.md "ToFlatList<TMetaData>(this List<TreeItem<TMetaData>> tree)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|`public`|`是`|tree:树形结构|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

