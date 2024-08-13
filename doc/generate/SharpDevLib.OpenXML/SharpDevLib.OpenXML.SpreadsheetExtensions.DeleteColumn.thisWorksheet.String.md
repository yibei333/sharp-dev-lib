###### [主页](./Index.md "主页")
#### DeleteColumn(this Worksheet worksheet, String columnName) 方法
**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")
**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")
**所属类型** : [SpreadsheetExtensions](./SharpDevLib.OpenXML.SpreadsheetExtensions.md "SpreadsheetExtensions")
``` csharp
public static Void DeleteColumn(this Worksheet worksheet, String columnName)
```
**注释**
*删除行(如果该行有合并单元格,则会产生不可预期的效果)*

**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")

**参数**
|名称|类型|注释|
|---|---|---|
|worksheet|[Worksheet](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.worksheet "Worksheet")|工作表格|
|columnName|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|列明,如A,B,C|
