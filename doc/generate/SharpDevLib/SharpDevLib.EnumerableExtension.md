###### [主页](./Index.md "主页")
## EnumerableExtension 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class EnumerableExtension : Object
```
**注释**
*集合扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ForEach\<T\>(this IEnumerable\<T\> source, Action\<T\> action)](./SharpDevLib.EnumerableExtension.ForEach.T.thisIEnumerable.T.Action.T.md "ForEach<T>(this IEnumerable<T> source, Action<T> action)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|source:集合<br>action:action|
|[DistinctByObjectValue\<T\>(this IEnumerable\<T\> source)](./SharpDevLib.EnumerableExtension.DistinctByObjectValue.T.thisIEnumerable.T.md "DistinctByObjectValue<T>(this IEnumerable<T> source)")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|`public`|`是`|source:集合|
|[OrderByDynamic\<T\>(this IEnumerable\<T\> query, String sortPropertyName, Boolean descending)](./SharpDevLib.EnumerableExtension.OrderByDynamic.T.thisIEnumerable.T.String.Boolean.md "OrderByDynamic<T>(this IEnumerable<T> query, String sortPropertyName, Boolean descending)")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|`public`|`是`|query:query<br>sortPropertyName:排序属性名称<br>descending:是否降序|
|[OrderByDynamic\<T\>(this IQueryable\<T\> query, String sortPropertyName, Boolean descending)](./SharpDevLib.EnumerableExtension.OrderByDynamic.T.thisIQueryable.T.String.Boolean.md "OrderByDynamic<T>(this IQueryable<T> query, String sortPropertyName, Boolean descending)")|[IQueryable](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1 "IQueryable")\<T\>|`public`|`是`|query:query<br>sortPropertyName:排序属性名称<br>descending:是否降序|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

