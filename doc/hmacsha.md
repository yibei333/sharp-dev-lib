# HMAC-SHA 哈希

SharpDevLib 提供了基于密钥的 HMAC-SHA 哈希计算功能。

HMAC（Hash-based Message Authentication Code）是一种基于密钥的消息认证码，支持 SHA-1、SHA-256、SHA-384、SHA-512 哈希算法。

## HMAC-SHA1

##### 计算字节数组的HMAC-SHA1

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha128(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81 (示例值)
```

##### 计算流的HMAC-SHA1

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha128(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81 (示例值)
```

## HMAC-SHA256

##### 计算字节数组的HMAC-SHA256

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha256(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622 (示例值)
```

##### 计算流的HMAC-SHA256

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha256(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622 (示例值)
```

##### 中文数据HMAC-SHA256

```csharp
var data = "你好世界".Utf8Decode();
var secret = "密钥".Utf8Decode();
var hmac = data.HmacSha256(secret);
Console.WriteLine(hmac);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8 (示例值)
```

## HMAC-SHA384

##### 计算字节数组的HMAC-SHA384

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha384(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

##### 计算流的HMAC-SHA384

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha384(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

## HMAC-SHA512

##### 计算字节数组的HMAC-SHA512

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha512(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

##### 计算流的HMAC-SHA512

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha512(secret);
Console.WriteLine(hmac);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

## 实际应用

##### API签名验证

```csharp
// 服务端生成签名
var apiKey = "my-api-key";
var timestamp = DateTime.Now.ToUtcTimestamp().ToString();
var data = "request data";

var signatureData = $"{apiKey}{timestamp}{data}".Utf8Decode();
var secret = apiKey.Utf8Decode();
var signature = signatureData.HmacSha256(secret);

// 客户端发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new
{
    Timestamp = timestamp,
    Data = data,
    Signature = signature
});

// 服务端验证签名
var receivedSignature = "received-signature";
var computedSignature = signatureData.HmacSha256(secret);

if (computedSignature == receivedSignature)
{
    Console.WriteLine("签名验证成功");
}
```

##### JWT Token签名

```csharp
// JWT Header
var header = new { alg = "HS256", typ = "JWT" };
var headerJson = header.Serialize();
var headerBytes = headerJson.Utf8Decode();
var headerBase64Url = headerBytes.Base64UrlEncode();

// JWT Payload
var payload = new { sub = "1234567890", name = "John Doe", exp = 1516239022 };
var payloadJson = payload.Serialize();
var payloadBytes = payloadJson.Utf8Decode();
var payloadBase64Url = payloadBytes.Base64UrlEncode();

// JWT Signature
var secretKey = "your-256-bit-secret".Utf8Decode();
var signatureData = $"{headerBase64Url}.{payloadBase64Url}".Utf8Decode();
var signature = signatureData.HmacSha256(secretKey).HexStringEncode();

// 组合Token
var token = $"{headerBase64Url}.{payloadBase64Url}.{signature}";
Console.WriteLine(token);
```

##### 消息完整性验证

```csharp
// 发送方
var message = "重要消息";
var secretKey = "shared-secret".Utf8Decode();
var messageBytes = message.Utf8Decode();
var hmac = messageBytes.HmacSha256(secretKey);

SendMessage(message);
SendHmac(hmac);

// 接收方
var receivedMessage = ReceiveMessage();
var receivedHmac = ReceiveHmac();
var computedHmac = receivedMessage.Utf8Decode().HmacSha256(secretKey);

if (computedHmac == receivedHmac)
{
    Console.WriteLine("消息完整性验证成功");
}
else
{
    Console.WriteLine("消息已被篡改");
}
```

##### Token生成

```csharp
// 生成临时Token
var userId = "user123";
var secretKey = "token-secret".Utf8Decode();
var timestamp = DateTime.Now.ToUtcTimestamp().ToString();

var tokenData = $"{userId}:{timestamp}".Utf8Decode();
var token = tokenData.HmacSha256(secretKey).Substring(0, 32);

Console.WriteLine($"Token: {token}");
//Token: dffd6021bb2bd5b0af676290809ec3a5
```

##### WebSocket消息认证

```csharp
// WebSocket握手认证
var sessionId = "session-123";
var secretKey = "websocket-secret".Utf8Decode();
var challenge = "random-challenge";

var authData = $"{sessionId}:{challenge}".Utf8Decode();
var response = authData.HmacSha256(secretKey);

// 发送认证响应
await WebSocket.SendAsync(response.Utf8Encode());
```

##### 文件传输验证

```csharp
// 发送方
var filePath = "important.dat";
using var stream = File.OpenRead(filePath);
var secretKey = "file-transfer-secret".Utf8Decode();
var fileHmac = stream.HmacSha256(secretKey);

SendFile(filePath);
SendHmac(fileHmac);

// 接收方
var receivedFilePath = "received.dat";
using var receivedStream = File.OpenRead(receivedFilePath);
var computedHmac = receivedStream.HmacSha256(secretKey);

if (computedHmac == receivedHmac)
{
    Console.WriteLine("文件传输验证成功");
}
```

##### 请求参数签名

```csharp
// 对请求参数进行排序和签名
var parameters = new Dictionary<string, string>
{
    ["name"] = "Alice",
    ["email"] = "alice@example.com",
    ["timestamp"] = DateTime.Now.ToUtcTimestamp().ToString()
};

var sortedParams = parameters.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}={kvp.Value}");
var paramStr = string.Join("&", sortedParams);
var secretKey = "api-secret".Utf8Decode();
var signature = paramStr.Utf8Decode().HmacSha256(secretKey);

// 发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new
{
    Parameters = parameters,
    Signature = signature
});
```

##### 支付回调签名验证

```csharp
// 验证支付平台回调签名
var callbackData = new
{
    OrderId = "12345",
    Amount = 100.00,
    Status = "success"
};
var callbackJson = callbackData.Serialize();
var secretKey = "payment-platform-secret".Utf8Decode();
var computedSignature = callbackJson.Utf8Decode().HmacSha256(secretKey);

var receivedSignature = "received-signature-from-payment-platform";

if (computedSignature == receivedSignature)
{
    Console.WriteLine("支付回调签名验证成功");
    ProcessPaymentCallback(callbackData);
}
```

## 密钥管理

##### 使用固定密钥

```csharp
var secretKey = "my-fixed-secret-key".Utf8Decode();
var hmac = data.HmacSha256(secretKey);
```

##### 使用环境变量密钥

```csharp
var secretKey = Environment.GetEnvironmentVariable("HMAC_SECRET_KEY").Utf8Decode();
var hmac = data.HmacSha256(secretKey);
```

##### 使用配置文件密钥

```csharp
var config = LoadConfiguration();
var secretKey = config.HmacSecretKey.Utf8Decode();
var hmac = data.HmacSha256(secretKey);
```

## HMAC算法对比

| 算法        | 输出长度 | 安全性 | 推荐场景 |
|-------------|----------|--------|----------|
| HMAC-SHA1   | 160位    | 低     | 不推荐使用 |
| HMAC-SHA256 | 256位    | 中     | 大多数场景 |
| HMAC-SHA384 | 384位    | 高     | 高安全性要求 |
| HMAC-SHA512 | 512位    | 高     | 最高安全性要求 |

## 注意事项

- HMAC-SHA1 安全性较低，不建议用于新系统
- HMAC-SHA256 是大多数场景的推荐选择
- 密钥应该保密，不应该泄露给未授权方
- 建议定期更换密钥以提高安全性
- 对于JWT Token，推荐使用 HMAC-SHA256 或更强算法

## 相关文档

- [SHA 哈希](sha.md)
- [MD5 哈希](md5.md)
- [HEX 编码](hex.md)
- [哈希](../README.md#哈希)
