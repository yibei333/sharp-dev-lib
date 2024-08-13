###### [主页](./Index.md "主页")
#### DistinctByObjectValue\<T\>(this IEnumerable\<T\> source) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [EnumerableExtension](./SharpDevLib.EnumerableExtension.md "EnumerableExtension")
``` csharp
public static IEnumerable<T> DistinctByObjectValue<T>(this IEnumerable<T> source)
    where T : class
```
**注释**
*根据对象的值（不是引用）去重*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|T|泛型类型|`class`|


**返回类型** : [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>

**参数**
|名称|类型|注释|
|---|---|---|
|source|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|集合|

