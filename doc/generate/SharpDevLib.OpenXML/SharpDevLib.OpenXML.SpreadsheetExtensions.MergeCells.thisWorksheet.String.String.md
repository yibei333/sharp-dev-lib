###### [主页](./Index.md "主页")

#### MergeCells(this Worksheet worksheet, String cellReference1, String cellReference2) 方法

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**所属类型** : [SpreadsheetExtensions](./SharpDevLib.OpenXML.SpreadsheetExtensions.md "SpreadsheetExtensions")

``` csharp
public static MergeCell MergeCells(this Worksheet worksheet, String cellReference1, String cellReference2)
```

**注释**

*合并单元格,只保存左上角的单元格(如果值为空,则根据RowIndex,ColumnIndex排序找到一个有值的单元格赋值),其余单元格删除*



**返回类型** : [MergeCell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.mergecell "MergeCell")


**参数**

|名称|类型|注释|
|---|---|---|
|worksheet|[Worksheet](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.worksheet "Worksheet")|工作表格|
|cellReference1|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|第一个单元格|
|cellReference2|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|第二个单元格|


