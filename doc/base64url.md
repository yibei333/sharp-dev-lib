# Base64Url 编码

SharpDevLib 提供了 Base64 URL 安全格式的编码与解码功能。

Base64 URL 编码替换了标准 Base64 中的 `+` 和 `/` 字符，并移除填充字符 `=`，使其可以安全地在 URL 中使用。

## 编码规则

1. 将 `+` 替换为 `-`
2. 将 `/` 替换为 `_`
3. 移除末尾的 `=` 填充字符

## 编码

##### 字节数组编码为Base64Url字符串

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111 };
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine(base64Url);
//SGVsbG8
```

##### 标准Base64对比

```csharp
var bytes = new byte[] { 0xFF, 0xFF, 0xFF };
var base64 = bytes.Base64Encode();
var base64Url = bytes.Base64UrlEncode();

Console.WriteLine($"标准Base64: {base64}");
///////

Console.WriteLine($"Base64Url: {base64Url}");
//____
```

##### 中文编码

```csharp
var str = "你好";
var bytes = str.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine(base64Url);
//5L2g5aW9
```

##### 空字节数组编码

```csharp
var bytes = Array.Empty<byte>();
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine(base64Url);
//(空字符串)
```

## 解码

##### Base64Url字符串解码为字节数组

```csharp
var base64Url = "SGVsbG8";
var bytes = base64Url.Base64UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

##### 自动补全填充字符

```csharp
var base64Url = "SGVsbG8"; // 长度为6，需要补全为8
var bytes = base64Url.Base64UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

##### Base64Url解码（带特殊字符）

```csharp
var base64Url = "SGVsbG8gV29ybGQ"; // 包含空格（会被替换为+）
var bytes = base64Url.Base64UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello World
```

##### 空字符串解码

```csharp
var base64Url = "";
var bytes = base64Url.Base64UrlDecode();
Console.WriteLine(bytes.Length);
//0
```

## 错误处理

##### 无效长度

```csharp
var base64Url = "SGVsbG8="; // 长度为7（模4余1）
try
{
    var bytes = base64Url.Base64UrlDecode();
}
catch (InvalidDataException ex)
{
    Console.WriteLine(ex.Message);
    //illegal base64url encoded string.
}
```

## 实际应用

##### JWT Token编码

```csharp
// JWT Header
var header = new { alg = "HS256", typ = "JWT" };
var headerJson = header.Serialize();
var headerBytes = headerJson.Utf8Decode();
var headerBase64Url = headerBytes.Base64UrlEncode();

// JWT Payload
var payload = new { sub = "1234567890", name = "John Doe" };
var payloadJson = payload.Serialize();
var payloadBytes = payloadJson.Utf8Decode();
var payloadBase64Url = payloadBytes.Base64UrlEncode();

// 组合Token
var token = $"{headerBase64Url}.{payloadBase64Url}.{signature}";
Console.WriteLine(token);
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIn0
```

##### URL参数传递

```csharp
// 构造URL安全的参数
var data = new { Key = "value" };
var json = data.Serialize();
var bytes = json.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

var url = $"https://example.com/api?data={base64Url}";
Console.WriteLine(url);
//https://example.com/api?data=eyJrZXkiOiJ2YWx1ZSJ9
```

##### API认证

```csharp
// 生成API Key
var clientId = "my-client-id";
var clientSecret = "my-secret-key";
var credentials = $"{clientId}:{clientSecret}";
var bytes = credentials.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

// 使用Base64Url作为Authorization header
var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64Url}");
```

##### Cookie存储

```csharp
// 序列化用户信息并存储到Cookie
var userInfo = new { UserId = 123, Username = "Alice" };
var json = userInfo.Serialize();
var bytes = json.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

// 设置Cookie
Response.Cookies.Append("UserInfo", base64Url);

// 读取Cookie
var base64Url = Request.Cookies["UserInfo"];
var bytes = base64Url.Base64UrlDecode();
var json = bytes.Utf8Encode();
var userInfo = json.DeSerialize<UserInfoDto>();
```

## 与标准Base64对比

##### 标准Base64

```csharp
var bytes = new byte[] { 0x00, 0x01, 0x02 };
var base64 = bytes.Base64Encode();
Console.WriteLine(base64);
//AAEC
```

##### Base64Url

```csharp
var bytes = new byte[] { 0x00, 0x01, 0x02 };
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine(base64Url);
//AAEC
```

##### 包含+和/的情况

```csharp
var bytes = new byte[] { 0xFB, 0xFF, 0xFF };
var base64 = bytes.Base64Encode();
var base64Url = bytes.Base64UrlEncode();

Console.WriteLine($"标准Base64: {base64}");
////_8

Console.WriteLine($"Base64Url: {base64Url}");
//_-8
```

## 相关文档

- [Base64 编码](base64.md)
- [URL 编码](url.md)
- [编码](../README.md#编码)
