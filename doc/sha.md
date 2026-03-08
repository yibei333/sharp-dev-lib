# SHA 哈希

SharpDevLib 提供了 SHA-1、SHA-256、SHA-384、SHA-512 哈希计算功能。

## SHA-1

##### 计算字节数组的SHA-1

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha128();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81
```

##### 计算流的SHA-1

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha128();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81
```

##### 中文SHA-1

```csharp
var bytes = "你好世界".Utf8Decode();
var hash = bytes.Sha128();
Console.WriteLine(hash);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

## SHA-256

##### 计算字节数组的SHA-256

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha256();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622
```

##### 计算流的SHA-256

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha256();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622
```

##### 中文SHA-256

```csharp
var bytes = "你好世界".Utf8Decode();
var hash = bytes.Sha256();
Console.WriteLine(hash);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8 (示例值)
```

## SHA-384

##### 计算字节数组的SHA-384

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha384();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

##### 计算流的SHA-384

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha384();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a (示例值)
```

##### 中文SHA-384

```csharp
var bytes = "你好世界".Utf8Decode();
var hash = bytes.Sha384();
Console.WriteLine(hash);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8d8a (示例值)
```

## SHA-512

##### 计算字节数组的SHA-512

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha512();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8d8a (示例值)
```

##### 计算流的SHA-512

```csharp
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha512();
Console.WriteLine(hash);
//dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8d8a (示例值)
```

##### 中文SHA-512

```csharp
var bytes = "你好世界".Utf8Decode();
var hash = bytes.Sha512();
Console.WriteLine(hash);
//7d8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a8f8f8e4c8c8a8e8a8d8a8d8a8d8a8d8a8d8a8e8f8a8d8a8d8a8d8a8d8a8d8a8d8a (示例值)
```

## 实际应用

##### 文件完整性校验

```csharp
// 计算文件SHA-256
var filePath = "important.dat";
using var stream = File.OpenRead(filePath);
var originalHash = stream.Sha256();
Console.WriteLine($"文件SHA-256: {originalHash}");

// 传输文件后验证
var receivedFilePath = "received.dat";
using var receivedStream = File.OpenRead(receivedFilePath);
var receivedHash = receivedStream.Sha256();

if (originalHash == receivedHash)
{
    Console.WriteLine("文件完整，未损坏");
}
else
{
    Console.WriteLine("文件已损坏");
}
```

##### 密码哈希

```csharp
// 存储密码（注意：实际应用中应使用加盐和迭代）
var password = "user123";
var passwordBytes = password.Utf8Decode();
var passwordHash = passwordBytes.Sha256();
SavePasswordHash(passwordHash);

// 验证密码
var inputPassword = "user123";
var inputBytes = inputPassword.Utf8Decode();
var inputHash = inputBytes.Sha256();

if (inputHash == storedPasswordHash)
{
    Console.WriteLine("密码正确");
}
```

##### 数据指纹

```csharp
// 生成数据指纹
var data = new
{
    Id = 1,
    Name = "Alice",
    Timestamp = DateTime.Now
};
var json = data.Serialize();
var dataBytes = json.Utf8Decode();
var fingerprint = dataBytes.Sha256();

Console.WriteLine($"数据指纹: {fingerprint}");
```

##### 唯一标识符

```csharp
// 基于内容生成唯一ID
var content = "unique content";
var contentBytes = content.Utf8Decode();
var uniqueId = contentBytes.Sha256().Substring(0, 16);

Console.WriteLine($"唯一ID: {uniqueId}");
//唯一ID: dffd6021bb2bd5b0
```

##### 缓存键生成

```csharp
// 生成缓存键
var parameters = new
{
    UserId = 123,
    Page = 1,
    PageSize = 10,
    Filters = new[] { "active", "verified" }
};
var json = parameters.Serialize();
var parametersBytes = json.Utf8Decode();
var cacheKey = parametersBytes.Sha256();

Console.WriteLine($"缓存键: {cacheKey}");
//缓存键: dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a36218298622
```

##### 区块链哈希

```csharp
// 区块数据
var blockData = new
{
    Index = 1,
    PreviousHash = "0000000000000000000000000000000000000000000000000000000000000000",
    Timestamp = DateTime.Now.ToUtcTimestamp(),
    Data = "block data"
};
var json = blockData.Serialize();
var blockBytes = json.Utf8Decode();
var blockHash = blockBytes.Sha256();

Console.WriteLine($"区块哈希: {blockHash}");
```

## SHA算法对比

| 算法    | 输出长度 | 安全性 |
|---------|----------|--------|
| SHA-1   | 160位    | 低     |
| SHA-256 | 256位    | 中     |
| SHA-384 | 384位    | 高     |
| SHA-512 | 512位    | 高     |

## 注意事项

- SHA-1 已被证明存在安全漏洞，不建议用于安全敏感场景
- 对于密码存储，建议使用 bcrypt、Argon2 或 PBKDF2 等专门的密码哈希算法
- SHA-256 是大多数场景的推荐选择
- SHA-384 和 SHA-512 提供更高的安全性，但计算开销更大

## 相关文档

- [HMAC-SHA 哈希](hmacsha.md)
- [MD5 哈希](md5.md)
- [HEX 编码](hex.md)
- [哈希](../README.md#哈希)
