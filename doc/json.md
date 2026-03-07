# JSON 操作

SharpDevLib 提供了基于 `System.Text.Json` 的 JSON 序列化和反序列化扩展。

## 序列化

##### 简单示例

```csharp
var user = new { Name = "张三", Age = 25 };
var json = user.Serialize();
Console.WriteLine(json);
// {"Age":25,"Name":"张三"}
```

##### 完整示例 

```csharp
var user = new { Name = "张三", Age = 25 };

//全局配置，只需要设置一次
JsonOption.Default = new JsonOption
{
    //反序列化只用到这三个属性，其余属性将影响序列化
    OrderByNameProperty = true,
    FormatJson = true,
    NameFormat = JsonNameFormat.CamelCaseLower
};
//或者每次传入
//var json = user.Serialize(new JsonOption { FormatJson = true });

var json = user.Serialize();
Console.WriteLine(json);
//{
//  "age": 25,
//  "name": "张三"
//}
```

## 反序列化

##### 简单示例

```csharp
var json = "{\"Name\":\"张三\"}";
var user = json.DeSerialize<NameDto>();
Console.WriteLine(user.Name);
//张三
```

##### 完整示例

```csharp
var json = "{\"Name\":\"张三\"}";

//全局配置，只需要设置一次
JsonOption.Default = new JsonOption
{
    //反序列化只用到此配置，其余属性将影响序列化
    CaseInsensitive = true
};
//或者每次传入
//var user = json.DeSerialize<NameDto>(new JsonOption { CaseInsensitive = false });

var user = json.DeSerialize<NameDto>();
Console.WriteLine(user.Name);
//张三

var notJson = "hello";
//var not = notJson.DeSerialize<NameDto>();将引发异常
var canDeserialize = notJson.TryDeSerialize<NameDto>(out _);
Console.WriteLine(canDeserialize);
//false
```

## JSON 格式化

##### 格式化 JSON

```csharp
var compactJson = "{\"Name\":\"张三\",\"Age\":25}";
var formattedJson = compactJson.FormatJson(true);
Console.WriteLine(formattedJson);
//{
```

##### 压缩 JSON

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
