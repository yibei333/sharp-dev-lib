# MD5 哈希

SharpDevLib 提供了 MD5 哈希计算功能。

## 字节数组 MD5

### 基本用法

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Md5();

Console.WriteLine(hash);
// 输出: 8b1a9953c4611296a827abf8c47804d7
```

### 16 位 MD5

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Md5(Md5OutputLength.Sixteen);

Console.WriteLine(hash);
// 输出: 53c4611296a827ab
```

### 32 位 MD5

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Md5(Md5OutputLength.ThirtyTwo);

Console.WriteLine(hash);
// 输出: 8b1a9953c4611296a827abf8c47804d7
```

## 流 MD5

### 基本用法

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var hash = stream.Md5();

Console.WriteLine(hash);
// 输出: 8b1a9953c4611296a827abf8c47804d7
```

### 文件 MD5

```csharp
using var stream = File.OpenRead("document.pdf");
var hash = stream.Md5();

Console.WriteLine($"文件 MD5: {hash}");
```

## HMAC-MD5

### 字节数组 HMAC-MD5

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacMd5(secret);

Console.WriteLine(hash);
// 输出: 5f3e9f5a8b6d7c2a1e4f9b8d7c6a5b2
```

### 流 HMAC-MD5

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var secret = "secret".Utf8Decode();
var hash = stream.HmacMd5(secret);

Console.WriteLine(hash);
```

### 16 位 HMAC-MD5

```csharp
var bytes = "Hello".Utf8Decode();
var secret = "secret".Utf8Decode();
var hash = bytes.HmacMd5(secret, Md5OutputLength.Sixteen);

Console.WriteLine(hash);
```

## 完整示例

### 计算字符串 MD5

```csharp
var text = "Hello World";
var hash = text.Utf8Decode().Md5();

Console.WriteLine($"MD5: {hash}");
// 输出: ed076287532e86365e841e92bfc50d8c
```

### 计算文件 MD5

```csharp
var filePath = "document.pdf";

using var stream = File.OpenRead(filePath);
var hash = stream.Md5();

Console.WriteLine($"文件: {Path.GetFileName(filePath)}");
Console.WriteLine($"MD5: {hash}");
```

### 文件完整性验证

```csharp
// 保存原始文件 MD5
var originalHash = "document.pdf".ReadFileBytes().Md5();
Console.WriteLine($"原始 MD5: {originalHash}");

// 传输后验证
var receivedHash = "received.pdf".ReadFileBytes().Md5();
Console.WriteLine($"接收 MD5: {receivedHash}");

if (originalHash == receivedHash)
{
    Console.WriteLine("文件完整");
}
else
{
    Console.WriteLine("文件已损坏");
}
```

### 密码哈希

```csharp
var password = "myPassword123";
var hash = password.Utf8Decode().Md5();

// 保存哈希值
Console.WriteLine($"密码哈希: {hash}");

// 验证密码
var inputPassword = "myPassword123";
var inputHash = inputPassword.Utf8Decode().Md5();

if (hash == inputHash)
{
    Console.WriteLine("密码正确");
}
```

### API 签名

```csharp
var apiKey = "your_api_key";
var timestamp = DateTime.UtcNow.ToUtcTimestamp().ToString();
var data = "request_data";

// 生成签名
var signatureString = $"{apiKey}{timestamp}{data}";
var signature = signatureString.Utf8Decode().Md5();

// 发送请求
var request = new HttpRequest("https://api.example.com/data")
{
    Parameters = new Dictionary<string, string?>
    {
        { "timestamp", timestamp },
        { "signature", signature }
    }
};
```

### HMAC-MD5 签名

```csharp
var secret = "my_secret_key".Utf8Decode();
var message = "Hello World";

// 生成 HMAC-MD5
var hash = message.Utf8Decode().HmacMd5(secret);
Console.WriteLine($"HMAC-MD5: {hash}");

// 验证签名
var receivedMessage = "Hello World";
var receivedHash = receivedMessage.Utf8Decode().HmacMd5(secret);

if (hash == receivedHash)
{
    Console.WriteLine("签名验证通过");
}
```

### 去重标识

```csharp
// 为大量数据生成唯一标识
var data = new List<string>
{
    "张三",
    "李四",
    "张三",  // 重复
    "王五"
};

var hashes = data.Select(d => d.Utf8Decode().Md5()).Distinct();
Console.WriteLine($"原始数量: {data.Count}, 去重后: {hashes.Count()}");
// 输出: 原始数量: 4, 去重后: 3
```

### 批量文件哈希

```csharp
var files = Directory.GetFiles(@"C:\Documents", "*.pdf");

foreach (var file in files)
{
    using var stream = File.OpenRead(file);
    var hash = stream.Md5();

    Console.WriteLine($"{Path.GetFileName(file)}: {hash}");
}
```

## 注意事项

### MD5 输出长度

| 类型 | 长度 | 示例 |
|------|------|------|
| `Md5OutputLength.ThirtyTwo` | 32 字符 | `8b1a9953c4611296a827abf8c47804d7` |
| `Md5OutputLength.Sixteen` | 16 字符 | `53c4611296a827ab` |

### HMAC 密钥限制

```csharp
var secret = new byte[65];  // 65 字节，超过限制

// 会抛出异常
var hash = bytes.HmacMd5(secret);
// 抛出: InvalidOperationException (md5 secret length should less than equal 64 bytes)
```

### 安全性警告

MD5 已被认为不安全，不建议用于以下场景：
- 密码存储
- 数字签名
- 安全认证

建议使用 SHA-256 或更安全的哈希算法。

## 相关文档

- [HmacMD5](hmacmd5.md)
- [SHA](sha.md)
- [HmacSHA](hmacsha.md)
- [基础扩展](../README.md#基础扩展)
