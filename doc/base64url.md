# Base64Url 编码

SharpDevLib 提供了 Base64Url 格式的编码与解码功能，适用于 URL 安全的场景。

## 编码

### Base64UrlEncode

```csharp
// 将字节数组编码为 Base64Url 字符串
var bytes = "Hello World".Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

Console.WriteLine(base64Url);
// 输出: SGVsbG8gV29ybGQ=
```

## 解码

### Base64UrlDecode

```csharp
// 将 Base64Url 字符串解码为原始字节数组
var base64Url = "SGVsbG8gV29ybGQ=";
var bytes = base64Url.Base64UrlDecode();

var text = bytes.Utf8Encode();
Console.WriteLine(text);
// 输出: Hello World
```

## Base64Url vs Base64

### 主要区别

Base64Url 对 Base64 进行了以下修改，使其适合在 URL 中使用：

| 特性 | Base64 | Base64Url |
|------|--------|-----------|
| `+` 替换为 | 不变 | `-` |
| `/` 替换为 | 不变 | `_` |
| `=` 填充 | 保留 | 通常移除 |

### 对比示例

```csharp
var bytes = "Hello World!@#$%".Utf8Decode();

// 标准 Base64
var base64 = bytes.Base64Encode();
Console.WriteLine($"Base64: {base64}");
// 输出: SGVsbG8gV29ybGQhQCMkJQ==

// Base64Url
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine($"Base64Url: {base64Url}");
// 输出: SGVsbG8gV29ybGQhQCMkJQ==
```

## URL 安全使用

### URL 参数

```csharp
var data = "user:123456";
var bytes = data.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

// 在 URL 中使用
var url = $"https://example.com/api?token={base64Url}";
Console.WriteLine(url);
// 输出: https://example.com/api?token=dXNlcjoxMjM0NTY=
```

### 路径参数

```csharp
var id = "user:123456";
var bytes = id.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

// 在 URL 路径中使用
var url = $"https://example.com/users/{base64Url}";
Console.WriteLine(url);
// 输出: https://example.com/users/dXNlcjoxMjM0NTY=
```

## JWT Token

### JWT Payload

```csharp
var payload = new
{
    sub = "1234567890",
    name = "张三",
    exp = 1699123456
};

var json = payload.Serialize();
var bytes = json.Utf8Decode();
var base64Url = bytes.Base64UrlEncode();

Console.WriteLine($"JWT Payload: {base64Url}");
// 输出: eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IuWbveWKoCIsImV4cCI6MTY5OTEyMzQ1Nn0=
```

### 完整 JWT 示例

```csharp
var header = new { alg = "HS256", typ = "JWT" };
var payload = new { sub = "1234567890", name = "张三", exp = 1699123456 };

var headerBase64Url = header.Serialize().Utf8Decode().Base64UrlEncode();
var payloadBase64Url = payload.Serialize().Utf8Decode().Base64UrlEncode();

var signature = ComputeSignature(headerBase64Url, payloadBase64Url, secret);
var jwt = $"{headerBase64Url}.{payloadBase64Url}.{signature}";

Console.WriteLine($"JWT: {jwt}");
```

## 特殊字符处理

### 包含 `+` 和 `/` 的数据

```csharp
var data = "data+with/special@chars";
var bytes = data.Utf8Decode();

// 标准 Base64 (包含 + 和 /)
var base64 = bytes.Base64Encode();
Console.WriteLine($"Base64: {base64}");
// 可能输出: ZGF0YSt3aXRoL3NwZWNpYWxAY2hhcnM=

// Base64Url (替换 + 和 /)
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine($"Base64Url: {base64Url}");
// 可能输出: ZGF0YSt3aXRoL3NwZWNpYWxAY2hhcnM=
```

## 完整示例

### URL Token 生成

```csharp
// 生成 URL 安全的 token
var userId = "user:123456";
var timestamp = DateTime.UtcNow.ToUtcTimestamp();

var tokenData = $"{userId}:{timestamp}";
var bytes = tokenData.Utf8Decode();
var token = bytes.Base64UrlEncode();

var url = $"https://example.com/verify?token={token}";
Console.WriteLine($"验证 URL: {url}");
```

### URL Token 验证

```csharp
// 从 URL 解析 token
var url = "https://example.com/verify?token=dXNlcjoxMjM0NTY6MTY5OTEyMzQ1Njk=";
var token = ExtractTokenFromUrl(url);

var bytes = token.Base64UrlDecode();
var tokenData = bytes.Utf8Encode();

var parts = tokenData.Split(':');
var userId = parts[0];
var timestamp = long.Parse(parts[1]);

Console.WriteLine($"用户 ID: {userId}");
Console.WriteLine($"时间戳: {timestamp}");
```

### 短链接生成

```csharp
var originalUrl = "https://example.com/articles/very-long-article-title-here";

// 生成短链接 ID
var bytes = originalUrl.Utf8Decode();
var shortId = bytes.Base64UrlEncode().Substring(0, 8);

var shortUrl = $"https://short.example.com/{shortId}";
Console.WriteLine($"短链接: {shortUrl}");
```

## 注意事项

### URL 编码

虽然 Base64Url 已经去除了 `+` 和 `/`，但在某些情况下仍需要 URL 编码：

```csharp
var base64Url = bytes.Base64UrlEncode();

// 在 URL 中使用（可能需要 URL 编码）
var encodedUrl = HttpUtility.UrlEncode(base64Url);
var url = $"https://example.com/api?data={encodedUrl}";
```

### 填充处理

```csharp
var bytes = new byte[] { 0x41 };  // 'A'

// 标准 Base64
var base64 = bytes.Base64Encode();
Console.WriteLine($"Base64: {base64}");
// 输出: QQ==

// Base64Url (可能移除填充)
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine($"Base64Url: {base64Url}");
// 输出可能为: QQ== 或 QQ
```

## 相关文档

- [Base64](base64.md)
- [Url](url.md)
- [基础扩展](../README.md#基础扩展)
