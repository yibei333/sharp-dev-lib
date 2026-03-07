# 枚举操作

SharpDevLib 提供了枚举类型的转换和信息获取功能。

## 类型转换

### 整型转枚举

```csharp
// 转换为枚举
var status = 1.ToEnum<UserStatus>();
// 结果: UserStatus.Disabled

// 如果值不在枚举定义中则抛出异常
var status = 99.ToEnum<UserStatus>();
// 抛出: InvalidDataException
```

### 字符串转枚举

```csharp
// 转换为枚举（忽略大小写）
var status = "Active".ToEnum<UserStatus>();
// 结果: UserStatus.Active

var status = "active".ToEnum<UserStatus>(ignoreCase: true);
// 结果: UserStatus.Active

// 区分大小写
var status = "Active".ToEnum<UserStatus>(ignoreCase: false);
// 结果: UserStatus.Active
```

## 获取枚举信息

### GetDictionary

```csharp
// 获取枚举字典
var dict = EnumHelper.GetDictionary<UserStatus>();
// 结果: { "Active": 0, "Disabled": 1 }

foreach (var item in dict)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}
```

### GetKeyValues

```csharp
// 获取枚举键值对集合
var keyValues = EnumHelper.GetKeyValues<UserStatus>();

foreach (var kv in keyValues)
{
    Console.WriteLine($"{kv.Key}: {kv.Value}");
}
```

## 完整示例

```csharp
public enum UserStatus
{
    Active = 0,
    Disabled = 1,
    Pending = 2
}

// 从整型转换
var status1 = 0.ToEnum<UserStatus>();  // UserStatus.Active
var status2 = 1.ToEnum<UserStatus>();  // UserStatus.Disabled

// 从字符串转换
var status3 = "Pending".ToEnum<UserStatus>();  // UserStatus.Pending
var status4 = "pending".ToEnum<UserStatus>(ignoreCase: true);  // UserStatus.Pending

// 获取所有枚举值
var dictionary = EnumHelper.GetDictionary<UserStatus>();
foreach (var item in dictionary)
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}
```

## 相关文档

- [字符串](string.md)
- [集合](collection.md)
- [基础扩展](../README.md#基础扩展)
