###### [主页](./Index.md "主页")

#### ToDataTable\<T\>(this IEnumerable\<T\> source) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [DataTableExtensions](./SharpDevLib.DataTableExtensions.md "DataTableExtensions")

``` csharp
public static DataTable ToDataTable<T>(this IEnumerable<T> source)
    where T : class
```

**注释**

*将集合转换为DataTable,以下元素属性将被忽略:*

* 1.属性不可读

* 2.属性类型为Class(string除外)

* 3.被new关键字覆盖的基类属性



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|集合元素类型|`class`|




**返回类型** : [DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable "DataTable")


**参数**

|名称|类型|注释|
|---|---|---|
|source|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|集合|


