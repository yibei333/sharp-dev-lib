###### [主页](./Index.md "主页")
## StringExtension 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class StringExtension : Object
```
**注释**
*字符串扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[TrimStart(this String source, String target)](./SharpDevLib.StringExtension.TrimStart.thisString.String.md "TrimStart(this String source, String target)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|source:字符串<br>target:要删除的前置字符串|
|[TrimEnd(this String source, String target)](./SharpDevLib.StringExtension.TrimEnd.thisString.String.md "TrimEnd(this String source, String target)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|source:字符串<br>target:要删除的后置字符串|
|[ToGuid(this String str, Boolean throwException)](./SharpDevLib.StringExtension.ToGuid.thisString.Boolean.md "ToGuid(this String str, Boolean throwException)")|[Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid "Guid")|`public`|`是`|str:字符串<br>throwException:如果转换失败是否抛出异常,如果位false则返回Guid.Empty|
|[SplitToGuidList(this String str, Char separator, Boolean removeEmptyEntries, Boolean throwException, Boolean distinct)](./SharpDevLib.StringExtension.SplitToGuidList.thisString.Char.Boolean.Boolean.Boolean.md "SplitToGuidList(this String str, Char separator, Boolean removeEmptyEntries, Boolean throwException, Boolean distinct)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Guid](https://learn.microsoft.com/en-us/dotnet/api/system.guid "Guid")\>|`public`|`是`|str:字符串<br>separator:分隔符<br>removeEmptyEntries:是否删除空白<br>throwException:如果转换失败是否抛出异常,如果位false则返回Guid.Empty<br>distinct:是否去重|
|[SplitToList(this String str, Char separator, Boolean removeEmptyEntries, Boolean distinct)](./SharpDevLib.StringExtension.SplitToList.thisString.Char.Boolean.Boolean.md "SplitToList(this String str, Char separator, Boolean removeEmptyEntries, Boolean distinct)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`public`|`是`|str:字符串<br>separator:分隔符<br>removeEmptyEntries:是否删除空白<br>distinct:是否去重|
|[ToBoolean(this String str)](./SharpDevLib.StringExtension.ToBoolean.thisString.md "ToBoolean(this String str)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|str:字符串|
|[Escape(this String str)](./SharpDevLib.StringExtension.Escape.thisString.md "Escape(this String str)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|str:字符串|
|[RemoveEscape(this String str)](./SharpDevLib.StringExtension.RemoveEscape.thisString.md "RemoveEscape(this String str)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|str:字符串|
|[RemoveLineBreak(this String str)](./SharpDevLib.StringExtension.RemoveLineBreak.thisString.md "RemoveLineBreak(this String str)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|str:字符串|
|[RemoveSpace(this String str)](./SharpDevLib.StringExtension.RemoveSpace.thisString.md "RemoveSpace(this String str)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|str:字符串|
|[GetUrlRelativePath(this String sourcePath, String targetPath)](./SharpDevLib.StringExtension.GetUrlRelativePath.thisString.String.md "GetUrlRelativePath(this String sourcePath, String targetPath)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|sourcePath:源路径<br>targetPath:目标路径|
|[GetUrlCommonPrefix(this String url1, String url2)](./SharpDevLib.StringExtension.GetUrlCommonPrefix.thisString.String.md "GetUrlCommonPrefix(this String url1, String url2)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|url1:地址1<br>url2:地址2|
|[GetCommonPrefix(this String str1, String str2)](./SharpDevLib.StringExtension.GetCommonPrefix.thisString.String.md "GetCommonPrefix(this String str1, String str2)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|str1:字符串1<br>str2:字符串2|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

