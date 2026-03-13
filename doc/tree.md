# 树形结构

SharpDevLib 提供了检查循环引用和构造树形结构功能

##### 检查循环引用
``` csharp
using SharpDevLib;

var users = new List<User>
{
    new() { Id=1,Name="foo",ParentId=3 },
    new() { Id=2,Name="bar",ParentId=1 },
    new() { Id=3,Name="baz",ParentId=2 },
};
var (result, ids) = users.HasCycleReference(x => x.Id, x => x.ParentId);
Console.WriteLine($"{result},{ids}");
//True,1->3->2->1

class User
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Name { get; set; }
    public List<User>? Children { get; set; }
}
```

##### 构造树形结构

``` csharp
using SharpDevLib;

var users = new List<User>
{
    new() { Id=1,Name="foo" },
    new() { Id=2,Name="bar",ParentId=1 },
    new() { Id=3,Name="baz",ParentId=2 },
    new() { Id=4,Name="qux" },
};
var items = users.BuildTree();
Console.WriteLine(items.Serialize(new JsonOption { FormatJson = true }));
//[
//  {
//    "Id": 1,
//    "ParentId": null,
//    "Name": "foo",
//    "Children": [
//      {
//        "Id": 2,
//        "ParentId": 1,
//        "Name": "bar",
//        "Children": [
//          {
//            "Id": 3,
//            "ParentId": 2,
//            "Name": "baz",
//            "Children": null
//          }
//        ]
//      }
//    ]
//  },
//  {
//    "Id": 4,
//    "ParentId": null,
//    "Name": "qux",
//    "Children": null
//  }
//]

class User
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Name { get; set; }
    public List<User>? Children { get; set; }
}
```

## 相关文档

- [基础扩展](../README.md#基础扩展)