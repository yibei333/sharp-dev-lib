###### [主页](./Index.md "主页")
#### SplitToGuidList(this String str, Char separator, Boolean removeEmptyEntries, Boolean throwException, Boolean distinct) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [StringExtension](./SharpDevLib.StringExtension.md "StringExtension")
``` csharp
public static List<Guid> SplitToGuidList(this String str, Char separator, Boolean removeEmptyEntries, Boolean throwException, Boolean distinct)
```
**注释**
*将字符串转分割为Guid集合*

**返回类型** : [List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid "Guid")\>

**参数**
|名称|类型|注释|
|---|---|---|
|str|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|字符串|
|separator|[Char](https://learn.microsoft.com/en-us/dotnet/api/system.char "Char")|分隔符|
|removeEmptyEntries|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否删除空白|
|throwException|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|如果转换失败是否抛出异常,如果位false则返回Guid.Empty|
|distinct|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否去重|

