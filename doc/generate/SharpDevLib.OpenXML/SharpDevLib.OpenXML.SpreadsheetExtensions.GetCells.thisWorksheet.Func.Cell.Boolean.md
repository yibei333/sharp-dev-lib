###### [主页](./Index.md "主页")

#### GetCells(this Worksheet worksheet, Func\<Cell, Boolean\> condition) 方法

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**所属类型** : [SpreadsheetExtensions](./SharpDevLib.OpenXML.SpreadsheetExtensions.md "SpreadsheetExtensions")

``` csharp
public static IEnumerable<Cell> GetCells(this Worksheet worksheet, Func<Cell, Boolean> condition)
```

**注释**

*获取单元格集合*



**返回类型** : [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell")\>


**参数**

|名称|类型|注释|
|---|---|---|
|worksheet|[Worksheet](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.worksheet "Worksheet")|工作表格|
|condition|[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 "Func")\<[Cell](https://learn.microsoft.com/en-us/dotnet/api/documentformat.openxml.spreadsheet.cell "Cell"), [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")\>|查询条件|


