###### [主页](./Index.md "主页")

## SpreadsheetExtensions 类

### 定义

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class SpreadsheetExtensions : Object
```

**注释**

*电子表格扩展*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[GetWorksheet(this WorkbookPart workbookPart, String tableName)](./SharpDevLib.OpenXML.SpreadsheetExtensions.GetWorksheet.thisWorkbookPart.String.md "GetWorksheet(this WorkbookPart workbookPart, String tableName)")|[Worksheet](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.worksheet "Worksheet")|`public`|`是`|workbookPart:工作簿部件<br>tableName:表明,如sheet1|
|[GetSharedStringTable(this WorkbookPart workbookPart)](./SharpDevLib.OpenXML.SpreadsheetExtensions.GetSharedStringTable.thisWorkbookPart.md "GetSharedStringTable(this WorkbookPart workbookPart)")|[SharedStringTable](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.sharedstringtable "SharedStringTable")|`public`|`是`|workbookPart:工作簿部件|
|[SetSharedStringItem(this SharedStringTable sharedStringTable, String text)](./SharpDevLib.OpenXML.SpreadsheetExtensions.SetSharedStringItem.thisSharedStringTable.String.md "SetSharedStringItem(this SharedStringTable sharedStringTable, String text)")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`是`|sharedStringTable:SharedStringTable<br>text:字符串|
|[GetCells(this Worksheet worksheet, Func\<Cell, Boolean\> condition)](./SharpDevLib.OpenXML.SpreadsheetExtensions.GetCells.thisWorksheet.Func.Cell.Boolean.md "GetCells(this Worksheet worksheet, Func<Cell, Boolean> condition)")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell")\>|`public`|`是`|worksheet:工作表格<br>condition:查询条件|
|[DeleteRow(this Worksheet worksheet, UInt32 rowIndex)](./SharpDevLib.OpenXML.SpreadsheetExtensions.DeleteRow.thisWorksheet.UInt32.md "DeleteRow(this Worksheet worksheet, UInt32 rowIndex)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheet:工作表格<br>rowIndex:行号|
|[InsertRow(this Worksheet worksheet, UInt32 rowIndex)](./SharpDevLib.OpenXML.SpreadsheetExtensions.InsertRow.thisWorksheet.UInt32.md "InsertRow(this Worksheet worksheet, UInt32 rowIndex)")|[Row](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.row "Row")|`public`|`是`|worksheet:工作表格<br>rowIndex:行号|
|[InsertColumn(this Worksheet worksheet, String columnName, Boolean insertColumnCells)](./SharpDevLib.OpenXML.SpreadsheetExtensions.InsertColumn.thisWorksheet.String.Boolean.md "InsertColumn(this Worksheet worksheet, String columnName, Boolean insertColumnCells)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheet:工作表格<br>columnName:列明,如A,B,C<br>insertColumnCells:是否要插入单元格|
|[DeleteColumn(this Worksheet worksheet, String columnName)](./SharpDevLib.OpenXML.SpreadsheetExtensions.DeleteColumn.thisWorksheet.String.md "DeleteColumn(this Worksheet worksheet, String columnName)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheet:工作表格<br>columnName:列明,如A,B,C|
|[GetValue(this Cell cell, IEnumerable\<SharedStringItem\> sharedStringItems)](./SharpDevLib.OpenXML.SpreadsheetExtensions.GetValue.thisCell.IEnumerable.SharedStringItem.md "GetValue(this Cell cell, IEnumerable<SharedStringItem> sharedStringItems)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|cell:单元格<br>sharedStringItems:共享字符串集合|
|[SetValue(this Cell cell, Object value, SharedStringTable sharedStringTable)](./SharpDevLib.OpenXML.SpreadsheetExtensions.SetValue.thisCell.Object.SharedStringTable.md "SetValue(this Cell cell, Object value, SharedStringTable sharedStringTable)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|cell:单元格<br>value:值<br>sharedStringTable:SharedStringTable|
|[MergeCells(this Worksheet worksheet, String cellReference1, String cellReference2)](./SharpDevLib.OpenXML.SpreadsheetExtensions.MergeCells.thisWorksheet.String.String.md "MergeCells(this Worksheet worksheet, String cellReference1, String cellReference2)")|[MergeCell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.mergecell "MergeCell")|`public`|`是`|worksheet:工作表格<br>cellReference1:第一个单元格<br>cellReference2:第二个单元格|
|[CreateCellIfNotExist(this Worksheet worksheet, String cellReference)](./SharpDevLib.OpenXML.SpreadsheetExtensions.CreateCellIfNotExist.thisWorksheet.String.md "CreateCellIfNotExist(this Worksheet worksheet, String cellReference)")|[Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell")|`public`|`是`|worksheet:工作表格<br>cellReference:单元格地址|
|[AutoColumnWidth(this SpreadsheetDocument doc)](./SharpDevLib.OpenXML.SpreadsheetExtensions.AutoColumnWidth.thisSpreadsheetDocument.md "AutoColumnWidth(this SpreadsheetDocument doc)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|doc:文档|
|[AutoColumnWidth(this WorksheetPart worksheetPart)](./SharpDevLib.OpenXML.SpreadsheetExtensions.AutoColumnWidth.thisWorksheetPart.md "AutoColumnWidth(this WorksheetPart worksheetPart)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheetPart:工作表格配件|
|[CreateStyle(this WorkbookPart workbookPart, CellStyle style)](./SharpDevLib.OpenXML.SpreadsheetExtensions.CreateStyle.thisWorkbookPart.CellStyle.md "CreateStyle(this WorkbookPart workbookPart, CellStyle style)")|[UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32 "UInt32")|`public`|`是`|workbookPart:工作簿部件<br>style:样式|
|[CreateStyle(this WorkbookPart workbookPart, CellFormat cellFormat)](./SharpDevLib.OpenXML.SpreadsheetExtensions.CreateStyle.thisWorkbookPart.CellFormat.md "CreateStyle(this WorkbookPart workbookPart, CellFormat cellFormat)")|[UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32 "UInt32")|`public`|`是`|workbookPart:工作簿部件<br>cellFormat:单元格格式,更灵活|
|[UseStyle(this MergeCell mergeCell, CellStyle style)](./SharpDevLib.OpenXML.SpreadsheetExtensions.UseStyle.thisMergeCell.CellStyle.md "UseStyle(this MergeCell mergeCell, CellStyle style)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|mergeCell:合并单元格<br>style:样式|
|[GetCells(this MergeCell mergeCell)](./SharpDevLib.OpenXML.SpreadsheetExtensions.GetCells.thisMergeCell.md "GetCells(this MergeCell mergeCell)")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell")\>|`public`|`是`|mergeCell:合并单元格|
|[SetComment(this Cell cell, String author, String comment)](./SharpDevLib.OpenXML.SpreadsheetExtensions.SetComment.thisCell.String.String.md "SetComment(this Cell cell, String author, String comment)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|cell:单元格<br>author:批注者<br>comment:批注|
|[AddBackground(this WorksheetPart worksheetPart, Stream background, String contentType)](./SharpDevLib.OpenXML.SpreadsheetExtensions.AddBackground.thisWorksheetPart.Stream.String.md "AddBackground(this WorksheetPart worksheetPart, Stream background, String contentType)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheetPart:工作表格部件<br>background:背景图文件流<br>contentType:content type|
|[RemoveBackground(this WorksheetPart worksheetPart)](./SharpDevLib.OpenXML.SpreadsheetExtensions.RemoveBackground.thisWorksheetPart.md "RemoveBackground(this WorksheetPart worksheetPart)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|worksheetPart:工作表格部件|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


