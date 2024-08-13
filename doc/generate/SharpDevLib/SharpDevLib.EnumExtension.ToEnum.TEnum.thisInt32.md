###### [主页](./Index.md "主页")

#### ToEnum\<TEnum\>(this Int32 intValue) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [EnumExtension](./SharpDevLib.EnumExtension.md "EnumExtension")

``` csharp
public static TEnum ToEnum<TEnum>(this Int32 intValue)
    where TEnum : struct
```

**注释**

*将整型值转换为指定的枚举*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|TEnum|指定的枚举类型|`struct`|




**返回类型** : TEnum


**参数**

|名称|类型|注释|
|---|---|---|
|intValue|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|整型值|


