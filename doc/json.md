# JSON 操作

SharpDevLib 提供了基于 `System.Text.Json` 的 JSON 序列化和反序列化扩展，支持格式化、压缩、自定义配置等功能。

## 序列化

### 基本序列化

```csharp
var user = new User { Name = "张三", Age = 25 };

// 基本序列化
var json = user.Serialize();
// 结果: {"Name":"张三","Age":25}
```

### 格式化序列化

```csharp
var user = new User { Name = "张三", Age = 25 };

// 格式化序列化
var json = user.Serialize(new JsonOption { FormatJson = true });
// 结果:
// {
//   "Name": "张三",
//   "Age": 25
// }
```

### 自定义配置序列化

```csharp
var json = user.Serialize(new JsonOption
{
    FormatJson = true,
    IgnoreNullValues = true,
    WriteIndented = true
});
```

### 尝试序列化

```csharp
// 尝试序列化（不抛出异常）
var success = user.TrySerialize(out string json);
if (success)
{
    Console.WriteLine(json);
}
```

## 反序列化

### 基本反序列化

```csharp
var json = "{\"Name\":\"张三\",\"Age\":25}";

// 反序列化为对象
var user = json.DeSerialize<User>();
```

### 反序列化为指定类型

```csharp
var json = "{\"Name\":\"张三\",\"Age\":25}";

// 反序列化为指定类型
var user = json.DeSerialize(typeof(User)) as User;
```

### 自定义配置反序列化

```csharp
var user = json.DeSerialize<User>(new JsonOption
{
    PropertyNameCaseInsensitive = true
});
```

### 尝试反序列化

```csharp
// 尝试反序列化（不抛出异常）
var success = json.TryDeSerialize(out User user);
if (success)
{
    Console.WriteLine(user.Name);
}
```

## JSON 格式化

### 格式化 JSON

```csharp
var compactJson = "{\"Name\":\"张三\",\"Age\":25}";

// 格式化 JSON
var formattedJson = compactJson.FormatJson();

// 格式化 JSON（不排序属性）
var formattedJson = compactJson.FormatJson(orderByNameProperty: false);
```

### 压缩 JSON

```csharp
var formattedJson = @"{
  ""Name"": ""张三"",
  ""Age"": 25
}";

// 压缩 JSON
var compressedJson = formattedJson.CompressJson();

// 压缩 JSON（不排序属性）
var compressedJson = formattedJson.CompressJson(orderByNameProperty: false);
```

## JsonOption 配置

### 基本配置

```csharp
var option = new JsonOption
{
    FormatJson = true,              // 格式化输出
    IgnoreNullValues = true,         // 忽略 null 值
    WriteIndented = true,            // 缩进输出
    PropertyNameCaseInsensitive = true  // 属性名不区分大小写
};
```

### 预定义配置

```csharp
// 默认配置
var json = user.Serialize(JsonOption.Default);

// 默认格式化配置
var json = user.Serialize(JsonOption.DefaultFormatJson);

// 默认压缩配置
var json = user.Serialize(JsonOption.DefaultCompressJson);
```

## 相关文档

- [字符串](string.md)
- [基础扩展](../README.md#基础扩展)
