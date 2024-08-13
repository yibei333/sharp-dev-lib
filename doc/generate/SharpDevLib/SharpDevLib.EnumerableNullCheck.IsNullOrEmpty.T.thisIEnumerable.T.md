###### [主页](./Index.md "主页")

#### IsNullOrEmpty\<T\>(this IEnumerable\<T\> source) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [EnumerableNullCheck](./SharpDevLib.EnumerableNullCheck.md "EnumerableNullCheck")

``` csharp
public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> source)
```

**注释**

*断言一个可枚举对象是否为Null或者长度为0*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|需要断言的可枚举对象反省类型|-|




**返回类型** : [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")


**参数**

|名称|类型|注释|
|---|---|---|
|source|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<T\>|需要断言的可枚举对象|


