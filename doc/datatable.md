# DataTable - 数据表操作

##### 定义数据类型

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateTime { get; set; }
}

var users = new List<User>
{
    new() { Id = 1, Name = "张三", Age = 25, IsActive = true, CreateTime = DateTime.Now },
    new() { Id = 2, Name = "李四", Age = 30, IsActive = false, CreateTime = DateTime.Now.AddDays(-1) },
    new() { Id = 3, Name = "王五", Age = 28, IsActive = true, CreateTime = DateTime.Now.AddDays(-2) }
};
```

##### 序列化 DataTable

```csharp
var table = users.ToDataTable();
var text=table.Serialize(x=>(x["Id"]?.ToString()?.ToInt()??0)>1,1);
Console.WriteLine(text);
//Id|Name|Age|IsActive|CreateTime
//2|李四|30|False|2026/3/7 21:12:58
```

##### 将集合转换为 DataTable

```csharp
var table = users.ToDataTable();
Console.WriteLine(table.Serialize());
//Id|Name|Age|IsActive|CreateTime
//1|张三|25|True|2026/3/8 21:12:58
//2|李四|30|False|2026/3/7 21:12:58
//3|王五|28|True|2026/3/6 21:12:58
```

##### 将 DataTable 转换为列表

```csharp
var table = users.ToDataTable();
var result = table.ToList<User>();
Console.WriteLine(result.Serialize(new JsonOption { FormatJson = true }));
//[
//  {
//    "Id": 1,
//    "Name": "张三",
//    "Age": 25,
//    "IsActive": true,
//    "CreateTime": "2026-03-08T21:14:13.6054849"
//  },
//  {
//    "Id": 2,
//    "Name": "李四",
//    "Age": 30,
//    "IsActive": false,
//    "CreateTime": "2026-03-07T21:14:13.6099975"
//  },
//  {
//    "Id": 3,
//    "Name": "王五",
//    "Age": 28,
//    "IsActive": true,
//    "CreateTime": "2026-03-06T21:14:13.6100067"
//  }
//]
```

##### 使用 Transfer 方法转换 DataTable

```csharp
var table = users.ToDataTable();
var transferred = table.Transfer(
    new DataTableTransferColumn("Id"),
    new DataTableTransferColumn("Name")
    {
        NameConverter = (name) => "姓名"
    },
    new DataTableTransferColumn("Age")
    {
        IsRequired = true,
        NameConverter = (sourceName) => $"复杂信息",
        TargetType = typeof(string),
        ValueConverter = (sourceValue, row) => $"年龄:{row["Age"]},创建时间:{row["CreateTime"]}"
    }
);
Console.WriteLine(transferred.Serialize());
//Id|姓名|* 复杂信息
//1|张三|年龄:25,创建时间:2026/3/8 21:09:25
//2|李四|年龄:30,创建时间:2026/3/7 21:09:25
//3|王五|年龄:28,创建时间:2026/3/6 21:09:25
```

## 相关文档
- [基础扩展](../README.md#基础扩展)