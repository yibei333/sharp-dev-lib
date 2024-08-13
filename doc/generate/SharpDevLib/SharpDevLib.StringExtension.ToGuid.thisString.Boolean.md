###### [主页](./Index.md "主页")
#### ToGuid(this String str, Boolean throwException) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [StringExtension](./SharpDevLib.StringExtension.md "StringExtension")
``` csharp
public static Guid ToGuid(this String str, Boolean throwException)
```
**注释**
*字符串转换位Guid*

**返回类型** : [Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid "Guid")

**参数**
|名称|类型|注释|
|---|---|---|
|str|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|字符串|
|throwException|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|如果转换失败是否抛出异常,如果位false则返回Guid.Empty|

