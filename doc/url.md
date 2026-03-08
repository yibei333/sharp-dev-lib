# URL 编码

SharpDevLib 提供了 URL 格式的编码与解码功能。

## 编码

##### 字节数组编码为URL编码字符串

```csharp
var bytes = "Hello World".Utf8Decode();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//Hello+World
```

##### 中文编码

```csharp
var bytes = "你好世界".Utf8Decode();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//%e4%bd%a0%e5%a5%bd%e4%b8%96%e7%95%8c
```

##### 特殊字符编码

```csharp
var bytes = "hello@example.com".Utf8Decode();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//hello%40example.com
```

##### 空格编码

```csharp
var bytes = "Hello World Test".Utf8Decode();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//Hello+World+Test
```

##### 空字节数组编码

```csharp
var bytes = Array.Empty<byte>();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//(空字符串)
```

## 解码

##### URL编码字符串解码为字节数组

```csharp
var urlEncoded = "Hello+World";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello World
```

##### 中文解码

```csharp
var urlEncoded = "%e4%bd%a0%e5%a5%bd";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//你好
```

##### 特殊字符解码

```csharp
var urlEncoded = "hello%40example.com";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//hello@example.com
```

##### 空格解码

```csharp
var urlEncoded = "Hello+World+Test";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello World Test
```

##### 空字符串解码

```csharp
var urlEncoded = "";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//(空字符串)
```

## 实际应用

##### 构造URL查询参数

```csharp
// 构造查询参数
var query = new
{
    name = "张三",
    email = "zhangsan@example.com",
    city = "北京"
};
var json = query.Serialize();
var bytes = json.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

var url = $"https://example.com/search?query={urlEncoded}";
Console.WriteLine(url);
//https://example.com/search?query=%7b%22name%22%3a%22%e5%bc%a0%e4%b8%89%22%2c%22email%22%3a%22zhangsan%40example.com%22%2c%22city%22%3a%22%e5%8c%97%e4%ba%ac%22%7d
```

##### 处理表单数据

```csharp
// 编码表单数据
var formData = new Dictionary<string, string>
{
    ["username"] = "user123",
    ["password"] = "pass@word",
    ["location"] = "上海"
};

var encodedPairs = new List<string>();
foreach (var kvp in formData)
{
    var keyBytes = kvp.Key.Utf8Decode();
    var valueBytes = kvp.Value.Utf8Decode();
    encodedPairs.Add($"{keyBytes.UrlEncode()}={valueBytes.UrlEncode()}");
}

var formDataString = string.Join("&", encodedPairs);
Console.WriteLine(formDataString);
//username=user123&password=pass%40word&location=%e4%b8%8a%e6%b5%b7
```

##### 解析URL参数

```csharp
// 解码URL参数
var urlEncoded = "name=%e5%bc%a0%e4%b8%89&email=zhangsan%40example.com";
var bytes = urlEncoded.UrlDecode();
var decoded = bytes.Utf8Encode();

// 解析参数
var pairs = decoded.Split('&');
foreach (var pair in pairs)
{
    var keyValue = pair.Split('=');
    var key = Uri.UnescapeDataString(keyValue[0]);
    var value = Uri.UnescapeDataString(keyValue[1]);
    Console.WriteLine($"{key}: {value}");
}
//name: 张三
//email: zhangsan@example.com
```

##### API请求

```csharp
// 构造API请求URL
var baseUrl = "https://api.example.com/users";
var parameters = new Dictionary<string, string>
{
    ["name"] = "Alice",
    ["email"] = "alice@example.com",
    ["page"] = "1"
};

var encodedParams = parameters
    .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")
    .ToArray();

var url = $"{baseUrl}?{string.Join("&", encodedParams)}";
Console.WriteLine(url);
//https://api.example.com/users?name=Alice&email=alice%40example.com&page=1
```

##### 处理文件名

```csharp
// 编码文件名用于URL
var fileName = "我的文档.pdf";
var fileNameBytes = fileName.Utf8Decode();
var urlEncodedFileName = fileNameBytes.UrlEncode();

var fileUrl = $"https://example.com/files/{urlEncodedFileName}";
Console.WriteLine(fileUrl);
//https://example.com/files/%e6%88%91%e7%9a%84%e6%96%87%e6%a1%a3.pdf
```

## 常见编码字符

| 原字符 | URL编码 |
|--------|---------|
| 空格    | `+`     |
| `!`     | `%21`   |
| `@`     | `%40`   |
| `#`     | `%23`   |
| `$`     | `%24`   |
| `%`     | `%25`   |
| `&`     | `%26`   |
| `=`     | `%3D`   |
| `?`     | `%3F`   |

## 相关文档

- [UTF-8 编码](utf8.md)
- [Base64Url 编码](base64url.md)
- [编码](../README.md#编码)
