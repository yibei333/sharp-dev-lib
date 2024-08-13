###### [主页](./Index.md "主页")
#### ToEnum\<TEnum\>(this String stringValue, Boolean ignoreCase) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [EnumExtension](./SharpDevLib.EnumExtension.md "EnumExtension")
``` csharp
public static TEnum ToEnum<TEnum>(this String stringValue, Boolean ignoreCase)
    where TEnum : struct
```
**注释**
*将字符串转换为指定的枚举*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TEnum|指定的枚举类型|`struct`|


**返回类型** : TEnum

**参数**
|名称|类型|注释|
|---|---|---|
|stringValue|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|字符串|
|ignoreCase|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否忽略大小写|

