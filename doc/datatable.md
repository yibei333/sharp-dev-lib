# DataTable - 数据表操作

##### 定义数据类型

```csharp
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
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
//基础示例
var table = users.ToDataTable();
Console.WriteLine(table.Serialize());
//Id|Name|Age|IsActive|CreateTime
//1|张三|25|True|2026/3/8 21:12:58
//2|李四|30|False|2026/3/7 21:12:58
//3|王五|28|True|2026/3/6 21:12:58

//导出需要的列
var table1 = users.ToDataTable(["Id", "Name", "CreateTime"]);
Console.WriteLine(table1.Serialize());
//Id|Name|CreateTime
//1|张三|2026/3/13 21:03:55
//2|李四|2026/3/12 21:03:55
//3|王五|2026/3/11 21:03:55

//自定义列名和值
ListToTableMapping.DefaultNameConventer = (name) => name.GetTranslate();
var table2 = users.ToDataTable(
[
    new ListToTableMapping("Id"),
    new ListToTableMapping("Name"),
    new ListToTableMapping("CreateTime")
    {
        NameConverter=(name)=>$"自定义列名_{name}*",
        ValueConverter=(value,user)=>(user as User)?.CreateTime.ToTimeString()
    },
]);
Console.WriteLine(table2.Serialize());
//标识|姓名|自定义列名_CreateTime*
//1|张三|2026-03-13 21:05:43
//2|李四|2026-03-12 21:05:43
//3|王五|2026-03-11 21:05:43

static class I18NService
{
    static readonly Dictionary<string, string> _translates = new()
    {
        {"Id","标识"},
        {"Name","姓名"},
        {"CreateTime","创建时间"},
    };

    public static string GetTranslate(this string key) => _translates.TryGetValue(key, out var value) ? value : key;
}
```

##### 将 DataTable 转换为列表

```csharp
//基础示例
var table = users.ToDataTable();
var list = table.ToList<User>();
Console.WriteLine(list.Serialize(new JsonOption { FormatJson = true }));
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

//导出需要的列
var list1 = table.ToList<AnotherUser>(["Id", "Name"]);
Console.WriteLine(list1.Serialize(new JsonOption { FormatJson = true }));
//[
//  {
//    "Id": 1,
//    "Name": "张三",
//    "Info": null
//  },
//  {
//    "Id": 2,
//    "Name": "李四",
//    "Info": null
//  },
//  {
//    "Id": 3,
//    "Name": "王五",
//    "Info": null
//  }
//]

//自定义名称/值
//可以单次设置
//TableToListMapping.DefaultNameConventer;
//TableToListMapping.DefaultValueConverter;
var list2 = table.ToList<AnotherUser>(
[
    new TableToListMapping("Id"),
    new TableToListMapping("Name"),
    new TableToListMapping("Age")
    {
        NameConverter=(name)=>"Info",
        ValueConverter=(value,row)=>$"个人信息,年龄:{row["Age"]},创建时间:{row["CreateTime"]}"
    }
]);
Console.WriteLine(list2.Serialize(new JsonOption { FormatJson = true }));
//[
//  {
//    "Id": 1,
//    "Name": "张三",
//    "Info": "个人信息,年龄:25,创建时间:2026/3/13 21:21:54"
//  },
//  {
//    "Id": 2,
//    "Name": "李四",
//    "Info": "个人信息,年龄:30,创建时间:2026/3/12 21:21:54"
//  },
//  {
//    "Id": 3,
//    "Name": "王五",
//    "Info": "个人信息,年龄:28,创建时间:2026/3/11 21:21:54"
//  }
//]

public class AnotherUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Info { get; set; }
}
```

## 相关文档
- [基础扩展](../README.md#基础扩展)