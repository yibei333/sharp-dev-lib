###### [主页](./Index.md "主页")
#### OrderByDynamic\<T\>(this IQueryable\<T\> query, String sortPropertyName, Boolean descending) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [EnumerableExtension](./SharpDevLib.EnumerableExtension.md "EnumerableExtension")
``` csharp
public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, String sortPropertyName, Boolean descending)
    where T : class
```
**注释**
*根据属性名称排序*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|T|泛型类型|`class`|


**返回类型** : [IQueryable](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1 "IQueryable")\<T\>

**参数**
|名称|类型|注释|
|---|---|---|
|query|[IQueryable](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1 "IQueryable")\<T\>|query|
|sortPropertyName|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|排序属性名称|
|descending|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否降序|

