###### [主页](./Index.md "主页")
## CellReference 类
### 定义
**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")
**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public class CellReference : Object
```
**注释**
*单元格引用*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[CellReference(UInt32 rowIndex, String columnName)](./SharpDevLib.OpenXML.CellReference.ctor.UInt32.String.md "CellReference(UInt32 rowIndex, String columnName)")|实例化单元格引用|rowIndex:行号,以1开始<br>columnName:列明,如A,B,C|
|[CellReference(UInt32 rowIndex, UInt32 columnIndex)](./SharpDevLib.OpenXML.CellReference.ctor.UInt32.UInt32.md "CellReference(UInt32 rowIndex, UInt32 columnIndex)")|实例化单元格引用|rowIndex:行号,以1开始<br>columnIndex:列号,以1开始|
|[CellReference(String reference)](./SharpDevLib.OpenXML.CellReference.ctor.String.md "CellReference(String reference)")|实例化单元格引用|reference:引用,如A1,B2|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[RowIndex](./SharpDevLib.OpenXML.CellReference.RowIndex.md "RowIndex")|[UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32 "UInt32")|`否`|行号,以1开始|
|[ColumnIndex](./SharpDevLib.OpenXML.CellReference.ColumnIndex.md "ColumnIndex")|[UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32 "UInt32")|`否`|列号,以1开始|
|[ColumnName](./SharpDevLib.OpenXML.CellReference.ColumnName.md "ColumnName")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|列明,如A,B,C|
|[Reference](./SharpDevLib.OpenXML.CellReference.Reference.md "Reference")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|引用,如A1,B2|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

