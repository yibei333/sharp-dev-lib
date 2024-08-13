###### [主页](./Index.md "主页")
## TreeItem\<TMetaData\> 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public class TreeItem<TMetaData> : Object
    where TMetaData : class
```
**注释**
*树形结构项*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TMetaData|元数据类型|`class`|


### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[MetaData](./SharpDevLib.TreeItem.1.MetaData.md "MetaData")|TMetaData|`否`|元数据|
|[Parent](./SharpDevLib.TreeItem.1.Parent.md "Parent")|[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>|`否`|父项|
|[Level](./SharpDevLib.TreeItem.1.Level.md "Level")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|层级|
|[Children](./SharpDevLib.TreeItem.1.Children.md "Children")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|`否`|子项|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ToMetaDataList()](./SharpDevLib.TreeItem.1.ToMetaDataList.md "ToMetaDataList()")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<TMetaData\>|`public`|`否`|-|
|[ToFlatList()](./SharpDevLib.TreeItem.1.ToFlatList.md "ToFlatList()")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[TreeItem](./SharpDevLib.TreeItem.1.md "TreeItem")\<TMetaData\>\>|`public`|`否`|-|
|[SetParent(TreeItem\<TMetaData\> parent)](./SharpDevLib.TreeItem.1.SetParent.TreeItem.TMetaData.md "SetParent(TreeItem<TMetaData> parent)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|parent:父项|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

