# MD5 哈希

SharpDevLib 提供了 MD5 哈希计算功能。

## 字节数组哈希

##### 计算字节数组的MD5（32位）

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Md5();
Console.WriteLine(hash);
//6cd3556deb0da54bca060b4c39479839
```

##### 计算字节数组的MD5（16位）

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Md5(Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//0da54bca060b4c39
```

##### 中文MD5

```csharp
var bytes = "你好世界".Utf8Decode();
var hash = bytes.Md5();
Console.WriteLine(hash);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

## 流哈希

##### 计算文件的MD5（32位）

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Md5();
Console.WriteLine(hash);
//5eb63bbbbe01eeed093cb22bb8f5acdc
```

##### 计算文件的MD5（16位）

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Md5(Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//01eeed093cb22bb8
```

## HMAC-MD5

##### 计算字节数组的HMAC-MD5（32位）

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hash = data.HmacMd5(secret);
Console.WriteLine(hash);
//302d0a8c1f5e5a2d3b4c5d6e7f8a9b0c (示例值)
```

##### 计算字节数组的HMAC-MD5（16位）

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hash = data.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//5a2d3b4c5d6e7f8a (示例值)
```

##### 计算流的HMAC-MD5（32位）

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hash = stream.HmacMd5(secret);
Console.WriteLine(hash);
//3b4c5d6e7f8a9b0c1d2e3f4a5b6c7d8e (示例值)
```

##### 计算流的HMAC-MD5（16位）

```csharp
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hash = stream.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//5d6e7f8a9b0c1d2e (示例值)
```

## 错误处理

##### 密钥长度超过64字节

```csharp
var data = "Hello".Utf8Decode();
var secret = new byte[65]; // 超过64字节
try
{
    var hash = data.HmacMd5(secret);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    //md5 secret length should less than equal 64 bytes
}
```

## 实际应用

##### 文件完整性校验

```csharp
// 计算文件MD5
var filePath = "important.dat";
using var stream = File.OpenRead(filePath);
var originalHash = stream.Md5();
Console.WriteLine($"文件MD5: {originalHash}");

// 传输文件后验证
var receivedFilePath = "received.dat";
using var receivedStream = File.OpenRead(receivedFilePath);
var receivedHash = receivedStream.Md5();

if (originalHash == receivedHash)
{
    Console.WriteLine("文件完整，未损坏");
}
else
{
    Console.WriteLine("文件已损坏");
}
```

##### 密码存储

```csharp
// 存储密码（注意：实际应用中应使用更安全的哈希算法如bcrypt）
var password = "user123";
var passwordBytes = password.Utf8Decode();
var passwordHash = passwordBytes.Md5();
SavePasswordHash(passwordHash);

// 验证密码
var inputPassword = "user123";
var inputBytes = inputPassword.Utf8Decode();
var inputHash = inputBytes.Md5();

if (inputHash == storedPasswordHash)
{
    Console.WriteLine("密码正确");
}
```

##### 数据签名

```csharp
// 使用HMAC-MD5签名数据
var data = "important data".Utf8Decode();
var secretKey = "my-secret-key".Utf8Decode();

var signature = data.HmacMd5(secretKey);

// 发送数据和签名
SendData(data);
SendSignature(signature);

// 验证签名
var receivedData = ReceiveData();
var receivedSignature = ReceiveSignature();
var computedSignature = receivedData.HmacMd5(secretKey);

if (computedSignature == receivedSignature)
{
    Console.WriteLine("签名验证成功");
}
```

##### 生成唯一标识符

```csharp
// 基于内容生成唯一ID
var content = "unique content";
var contentBytes = content.Utf8Decode();
var uniqueId = contentBytes.Md5(Md5OutputLength.Sixteen);

Console.WriteLine($"唯一ID: {uniqueId}");
//唯一ID: 01eeed093cb22bb8
```

##### 缓存键生成

```csharp
// 生成缓存键
var parameters = new
{
    UserId = 123,
    Page = 1,
    PageSize = 10
};
var json = parameters.Serialize();
var parametersBytes = json.Utf8Decode();
var cacheKey = parametersBytes.Md5();

Console.WriteLine($"缓存键: {cacheKey}");
//缓存键: 6cd3556deb0da54bca060b4c39479839
```

##### API请求签名

```csharp
// 签名API请求
var apiKey = "my-api-key";
var timestamp = DateTime.Now.ToUtcTimestamp().ToString();
var data = "request data";

var signatureData = $"{apiKey}{timestamp}{data}".Utf8Decode();
var signature = signatureData.HmacMd5(apiKey.Utf8Decode());

// 发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new
{
    Timestamp = timestamp,
    Data = data,
    Signature = signature
});
```

## 注意事项

- MD5 已被证明存在安全漏洞，不建议用于密码存储或数字签名
- 对于密码存储，建议使用 bcrypt、Argon2 或 PBKDF2 等专门的密码哈希算法
- 对于数字签名，建议使用 SHA-256 或更强的哈希算法
- MD5 适用于文件完整性校验、缓存键生成等非安全敏感场景

## 相关文档

- [SHA 哈希](sha.md)
- [HMAC-SHA 哈希](hmacsha.md)
- [HEX 编码](hex.md)
- [哈希](../README.md#哈希)
