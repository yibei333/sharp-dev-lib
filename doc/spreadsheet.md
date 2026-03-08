# Spreadsheet - 电子表格操作

提供电子表格（Spreadsheet）的详细操作功能。

## 类

### SpreadsheetHelper

电子表格帮助类，提供更详细的电子表格操作功能。

## 扩展方法

### CreateSpreadsheet

创建新的电子表格文件。

#### 方法签名

```csharp
public static SpreadsheetDocument CreateSpreadsheet(string filePath)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| filePath | string | 文件路径 |

#### 返回值

SpreadsheetDocument 对象。

### OpenSpreadsheet

打开现有的电子表格文件。

#### 方法签名

```csharp
public static SpreadsheetDocument OpenSpreadsheet(string filePath, bool isEditable = false)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| filePath | string | - | 文件路径 |
| isEditable | bool | false | 是否可编辑 |

#### 返回值

SpreadsheetDocument 对象。

### AddWorksheet

添加工作表。

#### 方法签名

```csharp
public static Worksheet AddWorksheet(this SpreadsheetDocument spreadsheet, string sheetName)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| spreadsheet | SpreadsheetDocument | 电子表格文档 |
| sheetName | string | 工作表名称 |

#### 返回值

Worksheet 对象。

### WriteCell

写入单元格数据。

#### 方法签名

```csharp
public static void WriteCell(this Worksheet worksheet, string cellAddress, object value)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| worksheet | Worksheet | 工作表 |
| cellAddress | string | 单元格地址，如 A1 |
| value | object | 单元格值 |

### ReadCell

读取单元格数据。

#### 方法签名

```csharp
public static object ReadCell(this Worksheet worksheet, string cellAddress)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| worksheet | Worksheet | 工作表 |
| cellAddress | string | 单元格地址，如 A1 |

#### 返回值

单元格值。

## 示例

### 创建电子表格

```csharp
// 创建新的电子表格
var spreadsheet = SpreadsheetHelper.CreateSpreadsheet("new.xlsx");
var worksheet = spreadsheet.AddWorksheet("Sheet1");
worksheet.WriteCell("A1", "姓名");
worksheet.WriteCell("B1", "年龄");
worksheet.WriteCell("A2", "张三");
worksheet.WriteCell("B2", 25);
```

### 打开电子表格

```csharp
// 以只读方式打开
var spreadsheet = SpreadsheetHelper.OpenSpreadsheet("data.xlsx");

// 以可编辑方式打开
var spreadsheet = SpreadsheetHelper.OpenSpreadsheet("data.xlsx", true);
```

### 读取单元格

```csharp
var spreadsheet = SpreadsheetHelper.OpenSpreadsheet("data.xlsx");
var worksheet = spreadsheet.GetWorksheet("Sheet1");
var value = worksheet.ReadCell("A1");
Console.WriteLine(value); // 输出: 姓名
```

## 特性

- 提供更底层的电子表格操作功能
- 支持创建和打开电子表格文件
- 支持添加工作表
- 支持读写单元格数据
- 支持可编辑模式
