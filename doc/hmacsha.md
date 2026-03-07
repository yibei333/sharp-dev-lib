# HMAC-SHA 哈希

SharpDevLib 提供了 HMAC-SHA-1、HMAC-SHA-256、HMAC-SHA-384、HMAC-SHA-512 哈希计算功能，用于消息认证。

## HMAC-SHA-1 (HmacSha128)

### 字节数组 HMAC-SHA-1

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacSha128(secret);

Console.WriteLine(hash);
```

### 流 HMAC-SHA-1

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacSha128(secret);

Console.WriteLine(hash);
```

## HMAC-SHA-256

### 字节数组 HMAC-SHA-256

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacSha256(secret);

Console.WriteLine(hash);
```

### 流 HMAC-SHA-256

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacSha256(secret);

Console.WriteLine(hash);
```

### 文件 HMAC-SHA-256

```csharp
using var stream = File.OpenRead("document.pdf");
var secret = "shared_secret".Utf8Decode();
var hash = stream.HmacSha256(secret);

Console.WriteLine($"文件 HMAC-SHA-256: {hash}");
```

## HMAC-SHA-384

### 字节数组 HMAC-SHA-384

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacSha384(secret);

Console.WriteLine(hash);
```

### 流 HMAC-SHA-384

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacSha384(secret);

Console.WriteLine(hash);
```

## HMAC-SHA-512

### 字节数组 HMAC-SHA-512

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacSha512(secret);

Console.WriteLine(hash);
```

### 流 HMAC-SHA-512

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacSha512(secret);

Console.WriteLine(hash);
```

## 完整示例

### JWT Token 签名

```csharp
var header = new { alg = "HS256", typ = "JWT" };
var payload = new { sub = "1234567890", name = "张三", exp = 1699123456 };

var headerBase64 = header.Serialize().Utf8Decode().Base64UrlEncode();
var payloadBase64 = payload.Serialize().Utf8Decode().Base64UrlEncode();

var signatureData = $"{headerBase64}.{payloadBase64}";
var signature = signatureData.Utf8Decode().HmacSha256("secret".Utf8Decode());

var token = $"{headerBase64}.{payloadBase64}.{signature}";
Console.WriteLine($"JWT Token: {token}");
```

### API 签名验证

```csharp
var apiKey = "your_api_key";
var timestamp = DateTime.UtcNow.ToUtcTimestamp().ToString();
var data = "request_data";

// 生成签名
var signatureString = $"{apiKey}{timestamp}{data}";
var signature = signatureString.Utf8Decode().HmacSha256("secret".Utf8Decode());

Console.WriteLine($"签名: {signature}");

// 验证签名
var receivedSignature = "received_signature_from_api";
var computedSignature = signatureString.Utf8Decode().HmacSha256("secret".Utf8Decode());

if (computedSignature == receivedSignature)
{
    Console.WriteLine("签名验证通过");
}
```

### 文件完整性验证

```csharp
var filePath = "document.pdf";
var secret = "verification_secret".Utf8Decode();

// 保存文件时计算 HMAC
using var stream = File.OpenRead(filePath);
var originalHmac = stream.HmacSha256(secret);

Console.WriteLine($"原始 HMAC: {originalHmac}");

// 传输后验证
using var receivedStream = File.OpenRead("received.pdf");
var receivedHmac = receivedStream.HmacSha256(secret);

if (originalHmac == receivedHmac)
{
    Console.WriteLine("文件验证通过，完整无损");
}
else
{
    Console.WriteLine("文件已损坏或被篡改");
}
```

## HMAC-SHA 算法对比

| 算法 | 输出长度 | 安全性 | 推荐用途 |
|------|---------|--------|----------|
| HMAC-SHA-1 | 160 位 | 低 | 不推荐新项目使用 |
| HMAC-SHA-256 | 256 位 | 高 | 推荐使用 |
| HMAC-SHA-384 | 384 位 | 高 | 安全敏感场景 |
| HMAC-SHA-512 | 512 位 | 最高 | 最高安全要求 |

## 相关文档

- [SHA](sha.md)
- [HmacMD5](hmacmd5.md)
- [基础扩展](../README.md#基础扩展)
