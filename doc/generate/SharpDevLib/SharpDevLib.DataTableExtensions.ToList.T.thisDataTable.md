###### [主页](./Index.md "主页")

#### ToList\<T\>(this DataTable table) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [DataTableExtensions](./SharpDevLib.DataTableExtensions.md "DataTableExtensions")

``` csharp
public static List<T> ToList<T>(this DataTable table)
    where T : class
```

**注释**

*将DataTable转换为列表,以下元素属性赋值将被忽略:*

* 1.非公共属性

* 2.属性不可写

* 3.属性类型为Class(string除外)

* 4.被new关键字覆盖的基类属性

* 5.属性名称不存在



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|列表元素类型|`class`|




**返回类型** : [List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<T\>


**参数**

|名称|类型|注释|
|---|---|---|
|table|[DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable "DataTable")|DataTable|


