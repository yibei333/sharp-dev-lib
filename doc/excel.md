# Excel - Excel 操作

提供 Excel 文件的读取、写入、加密和解密功能。

## 类

### ExcelHelper

Excel 帮助类，提供 Excel 文件的操作功能。

## 扩展方法

### ReadExcel

读取 Excel 文件并返回 DataTable。

#### 方法签名

```csharp
public static DataTable ReadExcel(string filePath, string sheetName = null, bool hasHeader = true)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| filePath | string | - | Excel 文件路径 |
| sheetName | string | null | 工作表名称，为 null 时读取第一个工作表 |
| hasHeader | bool | true | 是否包含标题行 |

#### 返回值

包含 Excel 数据的 DataTable。

### WriteExcel

将 DataTable 写入 Excel 文件。

#### 方法签名

```csharp
public static void WriteExcel(string filePath, DataTable data, string sheetName = "Sheet1", bool hasHeader = true)
```

#### 参数

| 参数 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| filePath | string | - | Excel 文件路径 |
| data | DataTable | - | 要写入的数据表 |
| sheetName | string | Sheet1 | 工作表名称 |
| hasHeader | bool | true | 是否写入标题行 |

### EncryptExcel

加密 Excel 文件。

#### 方法签名

```csharp
public static void EncryptExcel(string sourceFilePath, string targetFilePath, string password)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sourceFilePath | string | 源 Excel 文件路径 |
| targetFilePath | string | 目标加密文件路径 |
| password | string | 加密密码 |

### DecryptExcel

解密 Excel 文件。

#### 方法签名

```csharp
public static void DecryptExcel(string sourceFilePath, string targetFilePath, string password)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sourceFilePath | string | 加密的 Excel 文件路径 |
| targetFilePath | string | 目标解密文件路径 |
| password | string | 解密密码 |

## 示例

### 读取 Excel 文件

```csharp
// 读取第一个工作表
var data = ExcelHelper.ReadExcel("data.xlsx");

// 读取指定工作表
var data = ExcelHelper.ReadExcel("data.xlsx", "Sheet2");

// 不包含标题行
var data = ExcelHelper.ReadExcel("data.xlsx", hasHeader: false);
```

### 写入 Excel 文件

```csharp
var table = new DataTable();
table.Columns.Add("Name");
table.Columns.Add("Age");
table.Rows.Add("张三", 25);
table.Rows.Add("李四", 30);

// 写入 Excel
ExcelHelper.WriteExcel("output.xlsx", table);

// 指定工作表名称
ExcelHelper.WriteExcel("output.xlsx", table, "Users");
```

### 加密 Excel 文件

```csharp
// 加密 Excel 文件
ExcelHelper.EncryptExcel("data.xlsx", "encrypted.xlsx", "password123");
```

### 解密 Excel 文件

```csharp
// 解密 Excel 文件
ExcelHelper.DecryptExcel("encrypted.xlsx", "decrypted.xlsx", "password123");
```

## 特性

- 支持 Excel 文件的读取和写入
- 支持指定工作表名称
- 支持包含或不包含标题行
- 支持 Excel 文件的加密和解密
- 使用密码保护 Excel 文件
