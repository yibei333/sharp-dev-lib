# 空值断言

SharpDevLib 提供了`字符串、Guid、集合`的空值断扩展方法。

##### 字符串

```csharp
List<string?> values = ["", null, " ", "foo"];
values.ForEach(str =>
{
    Console.WriteLine(str.IsNullOrEmpty());
    Console.WriteLine(str.IsNullOrWhiteSpace());
    Console.WriteLine(str.NotNullOrEmpty());
    Console.WriteLine(str.NotNullOrWhiteSpace());
});
```

##### Guid
```csharp
List<Guid?> values = [
    null,
    Guid.Parse("00000000-0000-0000-0000-000000000000"),
    Guid.Parse("44806b68-ea98-4af8-8062-78a5e34be746")
];
values.ForEach(id =>
{
    Console.WriteLine(id.IsNullOrEmpty());
    Console.WriteLine(id.NotNullOrEmpty());
});
```

##### 集合
```csharp
List<IEnumerable<int>?> values = [
    null,
    [],
    [1,2]
];
values.ForEach(collection =>
{
    Console.WriteLine(collection.IsNullOrEmpty());
    Console.WriteLine(collection.NotNullOrEmpty());
});
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
