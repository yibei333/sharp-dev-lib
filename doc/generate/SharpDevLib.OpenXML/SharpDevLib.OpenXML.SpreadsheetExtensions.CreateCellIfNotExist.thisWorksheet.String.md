###### [主页](./Index.md "主页")

#### CreateCellIfNotExist(this Worksheet worksheet, String cellReference) 方法

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**所属类型** : [SpreadsheetExtensions](./SharpDevLib.OpenXML.SpreadsheetExtensions.md "SpreadsheetExtensions")

``` csharp
public static Cell CreateCellIfNotExist(this Worksheet worksheet, String cellReference)
```

**注释**

*如果单元格不存在则创建*



**返回类型** : [Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell")


**参数**

|名称|类型|注释|
|---|---|---|
|worksheet|[Worksheet](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.worksheet "Worksheet")|工作表格|
|cellReference|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|单元格地址|


