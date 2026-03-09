# Excel - Excel 操作

提供 Excel 文件的读取、写入、加密和解密功能。

##### 实例

```csharp
using System.Data;
using SharpDevLib;

//数据准备
var dataTable1 = new List<IdNameDto<int>>
{
    new(1,"张三"),
    new(2,"李四"),
}.ToDataTable();
dataTable1.TableName="自定义表名";
var dataTable2 = new List<IdNameDto<int>>
{
    new(1,"张三"),
    new(2,"李四"),
    new(3,"王五"),
}.ToDataTable();
var dataSet = new DataSet();
dataSet.Tables.Add(dataTable1);
dataSet.Tables.Add(dataTable2);

//写入DataTable
using var stream1 = new MemoryStream();
ExcelHelper.Write(dataTable1, stream1);
stream1.SaveToFile("1.xlsx");

//读取DataTable
using var stream2 = new FileInfo("1.xlsx").OpenRead();
var table = ExcelHelper.ReadTable(stream2);
Console.WriteLine(table.Serialize());
//Name|Id
//张三|1
//李四|2

//写入DataSet
using var stream3 = new MemoryStream();
ExcelHelper.Write(dataSet, stream3);
stream3.SaveToFile("2.xlsx");

//读取DataSet
using var stream4 = new FileInfo("2.xlsx").OpenRead();
var set1 = ExcelHelper.ReadSet(stream4);
Console.WriteLine(set1.Tables[0].Serialize());
//Name|Id
//张三|1
//李四|2
Console.WriteLine(set1.Tables[1].Serialize());
//Name|Id
//张三|1
//李四|2
//王五|3

//自定义列名，读写都支持
using var stream5 = new FileInfo("2.xlsx").OpenRead();
var set2 = ExcelHelper.ReadSet(stream5, ["姓名", "标识"], ["SomeName", "UserId"]);
Console.WriteLine(set2.Tables[0].Serialize());
//姓名|标识
//张三|1
//李四|2
Console.WriteLine(set2.Tables[1].Serialize());
//SomeName|UserId
//张三|1
//李四|2
//王五|3

//添加密码保护
using var stream6 = new FileInfo("2.xlsx").OpenRead();
using var stream7 = new MemoryStream();
ExcelHelper.Encrypt(stream6,stream7,"foo");
stream7.SaveToFile("encrypted.xlsx");

//去除密码保护
using var stream8 = new FileInfo("encrypted.xlsx").OpenRead();
using var stream9 = new FileStream("decrypted.xlsx",FileMode.OpenOrCreate,FileAccess.ReadWrite);
ExcelHelper.Decrypt(stream8,stream9,"foo");
```

## 相关文档
- [Spreadsheet](spreadsheet.md)
- [基础扩展](../README.md#基础扩展)