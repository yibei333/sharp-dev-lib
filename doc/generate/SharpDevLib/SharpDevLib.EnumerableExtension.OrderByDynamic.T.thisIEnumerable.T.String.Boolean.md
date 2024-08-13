###### [主页](./Index.md "主页")

#### OrderByDynamic\<T\>(this IEnumerable\<T\> query, String sortPropertyName, Boolean descending) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [EnumerableExtension](./SharpDevLib.EnumerableExtension.md "EnumerableExtension")

``` csharp
public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, String sortPropertyName, Boolean descending)
    where T : class
```

**注释**

*根据属性名称排序*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|泛型类型|`class`|




**返回类型** : [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>


**参数**

|名称|类型|注释|
|---|---|---|
|query|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|query|
|sortPropertyName|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|排序属性名称|
|descending|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否降序|


