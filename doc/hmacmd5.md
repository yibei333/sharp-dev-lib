# HMAC-MD5 哈希

SharpDevLib 提供了基于密钥的 HMAC-MD5 哈希计算功能。

HMAC（Hash-based Message Authentication Code）是一种基于密钥的消息认证码，使用 MD5 哈希算法计算。

## 字节数组HMAC

##### 计算字节数组的HMAC-MD5（32位）

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacMd5(secret);
Console.WriteLine(hmac);
//302d0a8c1f5e5a2d3b4c5d6e7f8a9b0c (示例值)
```

##### 计算字节数组的HMAC-MD5（16位）

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hmac);
//5a2d3b4c5d6e7f8a (示例值)
```

##### 中文数据HMAC

```csharp
var data = "你好世界".Utf8Decode();
var secret = "密钥".Utf8Decode();
var hmac = data.HmacMd5(secret);
Console.WriteLine(hmac);
//8f9e2d4c5a6b1c3d4e5f6a7b8c9d0e1f (示例值)
```

## 流HMAC

##### 计算流的HMAC-MD5（32位）

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacMd5(secret);
Console.WriteLine(hmac);
//3b4c5d6e7f8a9b0c1d2e3f4a5b6c7d8e (示例值)
```

##### 计算流的HMAC-MD5（16位）

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hmac);
//5d6e7f8a9b0c1d2e (示例值)
```

##### 大文件HMAC

```csharp
using var stream = new FileStream("large-file.bin", FileMode.Open);
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacMd5();
Console.WriteLine($"大文件HMAC: {hmac}");
```

## 错误处理

##### 密钥长度超过64字节

```csharp
var data = "Hello".Utf8Decode();
var secret = new byte[65]; // 超过64字节
try
{
    var hmac = data.HmacMd5(secret);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    //md5 secret length should less than equal 64 bytes
}
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
var signature = signatureData.HmacMd5(secret);

// 客户端发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new
{
    Timestamp = timestamp,
    Data = data,
    Signature = signature
});

// 服务端验证签名
var receivedSignature = "received-signature";
var computedSignature = signatureData.HmacMd5(secret);

if (computedSignature == receivedSignature)
{
    Console.WriteLine("签名验证成功");
}
```

##### 消息完整性验证

```csharp
// 发送方
var message = "重要消息";
var secretKey = "shared-secret".Utf8Decode();
var messageBytes = message.Utf8Decode();
var hmac = messageBytes.HmacMd5(secretKey);

SendMessage(message);
SendHmac(hmac);

// 接收方
var receivedMessage = ReceiveMessage();
var receivedHmac = ReceiveHmac();
var computedHmac = receivedMessage.Utf8Decode().HmacMd5(secretKey);

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
var token = tokenData.HmacMd5(secretKey, Md5OutputLength.Sixteen);

Console.WriteLine($"Token: {token}");
//Token: 5a2d3b4c5d6e7f8a
```

##### WebSocket消息认证

```csharp
// WebSocket握手认证
var sessionId = "session-123";
var secretKey = "websocket-secret".Utf8Decode();
var challenge = "random-challenge";

var authData = $"{sessionId}:{challenge}".Utf8Decode();
var response = authData.HmacMd5(secretKey);

// 发送认证响应
await WebSocket.SendAsync(response.Utf8Encode());
```

##### 文件传输验证

```csharp
// 发送方
var filePath = "important.dat";
using var stream = File.OpenRead(filePath);
var secretKey = "file-transfer-secret".Utf8Decode();
var fileHmac = stream.HmacMd5(secretKey);

SendFile(filePath);
SendHmac(fileHmac);

// 接收方
var receivedFilePath = "received.dat";
using var receivedStream = File.OpenRead(receivedFilePath);
var computedHmac = receivedStream.HmacMd5(secretKey);

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
var signature = paramStr.Utf8Decode().HmacMd5(secretKey);

// 发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new
{
    Parameters = parameters,
    Signature = signature
});
```

##### 防重放攻击

```csharp
// 添加时间戳和nonce防止重放攻击
var request = new
{
    Timestamp = DateTime.Now.ToUtcTimestamp(),
    Nonce = Guid.NewGuid().ToString(),
    Data = "sensitive data"
};

var requestStr = request.Serialize();
var secretKey = "anti-replay-secret".Utf8Decode();
var signature = requestStr.Utf8Decode().HmacMd5(secretKey);

// 验证请求
var receivedRequest = ReceiveRequest();
var computedSignature = receivedRequest.Serialize().Utf8Decode().HmacMd5(secretKey);

// 检查时间戳和nonce是否已使用
if (IsTimestampValid(receivedRequest.Timestamp) && !IsNonceUsed(receivedRequest.Nonce))
{
    if (computedSignature == receivedRequest.Signature)
    {
        Console.WriteLine("请求验证成功");
    }
}
```

## 密钥管理

##### 使用固定密钥

```csharp
var secretKey = "my-fixed-secret-key".Utf8Decode();
var hmac = data.HmacMd5(secretKey);
```

##### 使用环境变量密钥

```csharp
var secretKey = Environment.GetEnvironmentVariable("HMAC_SECRET_KEY").Utf8Decode();
var hmac = data.HmacMd5(secretKey);
```

##### 使用配置文件密钥

```csharp
var config = LoadConfiguration();
var secretKey = config.HmacSecretKey.Utf8Decode();
var hmac = data.HmacMd5(secretKey);
```

## 注意事项

- HMAC-MD5 使用 MD5 哈希算法，安全性较低，不建议用于安全敏感场景
- 对于高安全性要求，建议使用 HMAC-SHA256 或 HMAC-SHA512
- 密钥长度不应超过64字节
- 密钥应该保密，不应该泄露给未授权方
- 建议定期更换密钥以提高安全性

## 相关文档

- [MD5 哈希](md5.md)
- [HMAC-SHA 哈希](hmacsha.md)
- [SHA 哈希](sha.md)
- [哈希](../README.md#哈希)
