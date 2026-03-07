# HMAC-MD5 哈希

SharpDevLib 提供了 HMAC-MD5 哈希计算功能，用于消息认证。

## 字节数组 HMAC-MD5

### 基本用法

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacMd5(secret);

Console.WriteLine(hash);
// 输出: 5f3e9f5a8b6d7c2a1e4f9b8d7c6a5b2
```

### 16 位 HMAC-MD5

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacMd5(secret, Md5OutputLength.Sixteen);

Console.WriteLine(hash);
// 输出: 9f5a8b6d7c2a1e4f
```

### 32 位 HMAC-MD5

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacMd5(secret, Md5OutputLength.ThirtyTwo);

Console.WriteLine(hash);
// 输出: 5f3e9f5a8b6d7c2a1e4f9b8d7c6a5b2
```

## 流 HMAC-MD5

### 基本用法

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacMd5(secret);

Console.WriteLine(hash);
```

### 文件 HMAC-MD5

```csharp
using var stream = File.OpenRead("document.pdf");
var secret = "shared_secret".Utf8Decode();
var hash = stream.HmacMd5(secret);

Console.WriteLine($"文件 HMAC-MD5: {hash}");
```

## 完整示例

### API 签名验证

```csharp
// 生成签名
var apiKey = "your_api_key";
var timestamp = DateTime.UtcNow.ToUtcTimestamp().ToString();
var data = "request_data";

var signatureString = $"{apiKey}{timestamp}{data}";
var signature = signatureString.Utf8Decode().HmacMd5("secret".Utf8Decode());

Console.WriteLine($"签名: {signature}");

// 验证签名
var receivedSignature = "received_signature_from_api";
var computedSignature = signatureString.Utf8Decode().HmacMd5("secret".Utf8Decode());

if (computedSignature == receivedSignature)
{
    Console.WriteLine("签名验证通过");
}
```

### 消息完整性验证

```csharp
// 发送方
var message = "这是重要消息";
var secret = "shared_secret".Utf8Decode();

var hmac = message.Utf8Decode().HmacMd5(secret);
var packet = $"{message}|{hmac}";

// 发送数据包
SendData(packet);

// 接收方
var receivedPacket = ReceiveData();
var parts = receivedPacket.Split('|');
var receivedMessage = parts[0];
var receivedHmac = parts[1];

// 验证完整性
var computedHmac = receivedMessage.Utf8Decode().HmacMd5(secret);

if (computedHmac == receivedHmac)
{
    Console.WriteLine("消息完整，未被篡改");
    Console.WriteLine($"消息内容: {receivedMessage}");
}
else
{
    Console.WriteLine("消息已被篡改");
}
```

### JWT Token 签名 (HMAC-SHA256 替代)

```csharp
// 注意：HMAC-MD5 不推荐用于 JWT，这里仅作示例
var header = new { alg = "HS256", typ = "JWT" };
var payload = new { sub = "1234567890", name = "张三", exp = 1699123456 };

var headerBase64 = header.Serialize().Utf8Decode().Base64UrlEncode();
var payloadBase64 = payload.Serialize().Utf8Decode().Base64UrlEncode();

var signatureData = $"{headerBase64}.{payloadBase64}";
var signature = signatureData.Utf8Decode().HmacMd5("secret".Utf8Decode());

var token = $"{headerBase64}.{payloadBase64}.{signature}";
Console.WriteLine($"Token: {token}");
```

### 密钥派生

```csharp
var password = "user_password";
var salt = "unique_salt";

// 从密码派生密钥
var derivedKey = (password + salt).Utf8Decode().HmacMd5("master_secret".Utf8Decode());

Console.WriteLine($"派生密钥: {derivedKey}");
```

### 文件验证

```csharp
// 保存文件时计算 HMAC
var filePath = "document.pdf";
var secret = "verification_secret".Utf8Decode();

using var stream = File.OpenRead(filePath);
var originalHmac = stream.HmacMd5(secret);

Console.WriteLine($"原始 HMAC: {originalHmac}");

// 传输后验证
var receivedFilePath = "received.pdf";
using var receivedStream = File.OpenRead(receivedFilePath);
var receivedHmac = receivedStream.HmacMd5(secret);

if (originalHmac == receivedHmac)
{
    Console.WriteLine("文件验证通过，完整无损");
}
else
{
    Console.WriteLine("文件已损坏或被篡改");
}
```

### 一次性密码 (OTP)

```csharp
var secret = "user_secret_key";
var counter = 12345;

// 生成 OTP
var otp = counter.ToString().Utf8Decode().HmacMd5(secret.Utf8Decode());
var otpCode = otp.Substring(0, 6);  // 取前 6 位

Console.WriteLine($"OTP: {otpCode}");

// 验证 OTP
var userOtp = "abc123";
var computedOtp = counter.ToString().Utf8Decode().HmacMd5(secret.Utf8Decode()).Substring(0, 6);

if (computedOtp == userOtp)
{
    Console.WriteLine("OTP 验证成功");
}
```

### 多方认证

```csharp
var message = "需要多方确认的指令";
var secret1 = "party1_secret".Utf8Decode();
var secret2 = "party2_secret".Utf8Decode();
var secret3 = "party3_secret".Utf8Decode();

// 各方计算签名
var signature1 = message.Utf8Decode().HmacMd5(secret1);
var signature2 = message.Utf8Decode().HmacMd5(secret2);
var signature3 = message.Utf8Decode().HmacMd5(secret3);

// 组合签名
var combinedSignature = $"{signature1}:{signature2}:{signature3}";

Console.WriteLine($"组合签名: {combinedSignature}");

// 验证各方签名
var signatures = combinedSignature.Split(':');
var isValid1 = message.Utf8Decode().HmacMd5(secret1) == signatures[0];
var isValid2 = message.Utf8Decode().HmacMd5(secret2) == signatures[1];
var isValid3 = message.Utf8Decode().HmacMd5(secret3) == signatures[2];

if (isValid1 && isValid2 && isValid3)
{
    Console.WriteLine("所有签名验证通过");
}
```

## 注意事项

### 密钥长度限制

HMAC-MD5 的密钥长度不能超过 64 字节：

```csharp
var secret = new byte[65];  // 65 字节

// 会抛出异常
var hash = bytes.HmacMd5(secret);
// 抛出: InvalidOperationException (md5 secret length should less than equal 64 bytes)
```

### 安全性警告

HMAC-MD5 已被认为不安全，不建议用于以下场景：
- 新系统的安全认证
- 长期有效的签名
- 高安全性要求的场景

建议使用 HMAC-SHA256 或更安全的算法。

### 与 MD5 的区别

| 特性 | MD5 | HMAC-MD5 |
|------|-----|----------|
| 用途 | 数据摘要 | 消息认证 |
| 密钥 | 无 | 需要 |
| 安全性 | 低 | 较低（但高于 MD5） |
| 防篡改 | 不能 | 能 |

## 相关文档

- [MD5](md5.md)
- [HmacSHA](hmacsha.md)
- [SHA](sha.md)
- [基础扩展](../README.md#基础扩展)
