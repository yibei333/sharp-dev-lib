# 枚举操作

SharpDevLib 提供了枚举类型的转换和获取功能。

``` csharp
enum Status
{
    Active = 1,
    Inactive = 2,
    Pending = 3
}
```

## 枚举转换

##### 整型转枚举

```csharp
int intValue = 1;
var status = intValue.ToEnum<Status>();
Console.WriteLine(status);
//Active
```

##### 字符串转枚举

```csharp
var stringValue = "Active";
var status = stringValue.ToEnum<Status>();
Console.WriteLine(status);
//Active
```

##### 字符串转枚举（忽略大小写）

```csharp
var stringValue = "active";
var status = stringValue.ToEnum<Status>(ignoreCase: true);
Console.WriteLine(status);
//Active
```

## 获取枚举信息

##### 获取枚举字典

```csharp
var dictionary = EnumHelper.GetDictionary<Status>();
foreach (var kvp in dictionary)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
//Active: 1
//Inactive: 2
//Pending: 3
```

##### 获取枚举键值对

```csharp
var keyValues = EnumHelper.GetKeyValues<Status>();
foreach (var kvp in keyValues)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
//Active: 1
//Inactive: 2
//Pending: 3
```

## 相关文档

- [字符串操作](string.md)
- [反射操作](reflection.md)
- [基础扩展](../README.md#基础扩展)
