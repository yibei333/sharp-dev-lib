# URL 编码

SharpDevLib 提供了 URL 格式的编码与解码功能。

## 编码

### UrlEncode

```csharp
// 将字节数组编码为 URL 编码字符串
var bytes = "Hello World".Utf8Decode();
var urlEncoded = bytes.UrlEncode();

Console.WriteLine(urlEncoded);
// 输出: Hello+World
```

## 解码

### UrlDecode

```csharp
// 将 URL 编码字符串解码为原始字节数组
var urlEncoded = "Hello+World";
var bytes = urlEncoded.UrlDecode();

var text = bytes.Utf8Encode();
Console.WriteLine(text);
// 输出: Hello World
```

## 特殊字符编码

### 编码特殊字符

```csharp
var text = "Hello World!@#$%";
var bytes = text.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

Console.WriteLine(urlEncoded);
// 输出: Hello+World%21%40%23%24%25
```

### 解码特殊字符

```csharp
var urlEncoded = "Hello+World%21%40%23";
var bytes = urlEncoded.UrlDecode();
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: Hello World!@#
```

## 中文编码

### 编码中文

```csharp
var text = "你好世界";
var bytes = text.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

Console.WriteLine(urlEncoded);
// 输出: %E4%BD%A0%E5%A5%BD%E4%B8%96%E7%95%8C
```

### 解码中文

```csharp
var urlEncoded = "%E4%BD%A0%E5%A5%BD";
var bytes = urlEncoded.UrlDecode();
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: 你好
```

## URL 参数

### 编码 URL 参数

```csharp
var query = "name=张三&age=25&city=北京";
var bytes = query.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

var url = $"https://example.com/api?{urlEncoded}";
Console.WriteLine(url);
// 输出: https://example.com/api?name%3D%E5%BC%A0%E4%B8%89%26age%3D25%26city%3D%E5%8C%97%E4%BA%AC
```

### 解码 URL 参数

```csharp
var urlEncoded = "name%3D%E5%BC%A0%E4%B8%89%26age%3D25";
var bytes = urlEncoded.UrlDecode();
var query = bytes.Utf8Encode();

Console.WriteLine(query);
// 输出: name=张三&age=25
```

## 完整示例

### 构建 URL 查询字符串

```csharp
var parameters = new Dictionary<string, string>
{
    ["name"] = "张三",
    ["age"] = "25",
    ["city"] = "北京"
};

var queryString = string.Join("&", parameters.Select(kvp =>
{
    var key = kvp.Key.Utf8Decode().UrlEncode().Utf8Encode();
    var value = kvp.Value.Utf8Decode().UrlEncode().Utf8Encode();
    return $"{key}={value}";
}));

var url = $"https://example.com/api?{queryString}";
Console.WriteLine(url);
// 输出: https://example.com/api?name=%E5%BC%A0%E4%B8%89&age=25&city=%E5%8C%97%E4%BA%AC
```

### 解析 URL 查询字符串

```csharp
var queryString = "name=%E5%BC%A0%E4%B8%89&age=25&city=%E5%8C%97%E4%BA%AC";
var parameters = queryString.Split('&')
    .Select(param =>
    {
        var parts = param.Split('=');
        var key = parts[0].Utf8Decode().UrlDecode().Utf8Encode();
        var value = parts[1].Utf8Decode().UrlDecode().Utf8Encode();
        return new { Key = key, Value = value };
    })
    .ToDictionary(x => x.Key, x => x.Value);

foreach (var kvp in parameters)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
// 输出:
// name: 张三
// age: 25
// city: 北京
```

### URL 短链接

```csharp
var originalUrl = "https://example.com/articles/article-title-here";
var bytes = originalUrl.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

var shortUrl = $"https://short.example.com/{urlEncoded}";
Console.WriteLine(shortUrl);
```

### 处理用户输入

```csharp
var userInput = "张三 <script>alert('xss')</script>";
var bytes = userInput.Utf8Decode();
var urlEncoded = bytes.UrlEncode();

// 安全地在 URL 中使用
var url = $"https://example.com/search?q={urlEncoded}";
Console.WriteLine(url);
```

## 常见字符编码

### 特殊字符

| 字符 | URL 编码 |
|------|----------|
| 空格 | `+` 或 `%20` |
| `!` | `%21` |
| `@` | `%40` |
| `#` | `%23` |
| `$` | `%24` |
| `%` | `%25` |
| `&` | `%26` |
| `=` | `%3D` |
| `?` | `%3F` |
| `/` | `%2F` |
| `:` | `%3A` |
| `;` | `%3B` |

### 保留字符

以下字符在 URL 中有特殊含义，需要编码：

- `:` - 协议分隔符
- `/` - 路径分隔符
- `?` - 查询字符串开始
- `#` - 片段标识符
- `&` - 参数分隔符
- `=` - 参数赋值
- `+` - 空格（在查询字符串中）
- `%` - 编码前缀

## 注意事项

### 空格编码

```csharp
// 空格可以编码为 + 或 %20
var bytes = "Hello World".Utf8Decode();
var urlEncoded = bytes.UrlEncode();

// 可能输出: Hello+World 或 Hello%20World
```

### 大小写

URL 编码通常使用大写的十六进制：

```csharp
// 虽然小写也有效，但通常使用大写
// %E4%BD%A0 (推荐)
// %e4%bd%a0 (也可用)
```

### 完整 URL

注意不要对整个 URL 进行编码，只对需要编码的部分进行编码：

```csharp
// 错误：对整个 URL 编码
var fullUrl = "https://example.com/api?name=张三";
var encodedUrl = fullUrl.Utf8Decode().UrlEncode();
// 这会破坏 URL 结构

// 正确：只对参数值编码
var baseUrl = "https://example.com/api?name=";
var parameterValue = "张三".Utf8Decode().UrlEncode();
var url = $"{baseUrl}{parameterValue}";
```

## 相关文档

- [Base64Url](base64url.md)
- [Utf8](utf8.md)
- [基础扩展](../README.md#基础扩展)
