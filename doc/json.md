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
    //序列化只用到FormatJson和NameFormat，其余属性将影响反序列化
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
var formattedJson = compactJson.FormatJson();
Console.WriteLine(formattedJson);
//{
//  "Name": "张三",
//  "Age": 25
//}
```

##### 压缩 JSON

```csharp
var formattedJson = @"{
  ""Name"": ""张三"",
  ""Age"": 25
}";

var compressedJson = formattedJson.CompressJson();
Console.WriteLine(compressedJson);
//{"Name":"张三","Age":25}
```

## 相关文档

- [字符串](string.md)
- [基础扩展](../README.md#基础扩展)
