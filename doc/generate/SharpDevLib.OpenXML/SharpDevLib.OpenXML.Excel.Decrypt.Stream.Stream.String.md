###### [主页](./Index.md "主页")
#### Decrypt(Stream inputStream, Stream outputStream, String password) 方法
**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")
**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")
**所属类型** : [Excel](./SharpDevLib.OpenXML.Excel.md "Excel")
``` csharp
public static Void Decrypt(Stream inputStream, Stream outputStream, String password)
```
**注释**
*去除excel密码保护*

**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")

**参数**
|名称|类型|注释|
|---|---|---|
|inputStream|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|密码保护的excel文件流|
|outputStream|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|去除密码的excel文件流|
|password|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|密码|

