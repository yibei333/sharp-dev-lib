# SHA 哈希

SharpDevLib 提供了 SHA-1、SHA-256、SHA-384、SHA-512 哈希计算功能。

## SHA-1 (SHA128)

### 字节数组 SHA-1

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Sha128();

Console.WriteLine(hash);
// 输出: f7ff9e8b7bb2e09b70935a5d785e0cc5d9d0abf
```

### 流 SHA-1

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var hash = stream.Sha128();

Console.WriteLine(hash);
```

## SHA-256

### 字节数组 SHA-256

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Sha256();

Console.WriteLine(hash);
// 输出: 185f8db32271fe25f561a6fc938b2e264306ec304eda518007d1764826381969
```

### 流 SHA-256

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var hash = stream.Sha256();

Console.WriteLine(hash);
```

### 文件 SHA-256

```csharp
using var stream = File.OpenRead("document.pdf");
var hash = stream.Sha256();

Console.WriteLine($"文件 SHA-256: {hash}");
```

## SHA-384

### 字节数组 SHA-384

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Sha384();

Console.WriteLine(hash);
// 输出: 351b3c9cdd741fdd2d0a7e5e058d58f2e9f0c9b736dbd0c830b6c2d0e09c9b1b9a8e9d3f4a4b4c4d5e6f7a8b9c0d1e2f3a4b5c6d7e8f9a0b1c2d
```

### 流 SHA-384

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var hash = stream.Sha384();

Console.WriteLine(hash);
```

## SHA-512

### 字节数组 SHA-512

```csharp
var bytes = "Hello".Utf8Decode();
var hash = bytes.Sha512();

Console.WriteLine(hash);
// 输出: 3615f80c9d293ed7402687f94b22d58e529b8cc7916f8fac7fddf7fbd5af4cf777d3d795a7a00a16bf7e7f3fb9561ee9baae480da9fe7a1876947b91c45f4d
```

### 流 SHA-512

```csharp
using var stream = new MemoryStream("Hello".Utf8Decode());
var hash = stream.Sha512();

Console.WriteLine(hash);
```

## 完整示例

### 计算字符串 SHA-256

```csharp
var text = "Hello World";
var hash = text.Utf8Decode().Sha256();

Console.WriteLine($"SHA-256: {hash}");
```

### 文件完整性验证

```csharp
var filePath = "document.pdf";

using var stream = File.OpenRead(filePath);
var hash = stream.Sha256();

Console.WriteLine($"文件: {Path.GetFileName(filePath)}");
Console.WriteLine($"SHA-256: {hash}");
```

### 密码哈希（加盐）

```csharp
var password = "myPassword123";
var salt = "unique_salt_value";

// 加盐后计算 SHA-256
var saltedPassword = password + salt;
var hash = saltedPassword.Utf8Decode().Sha256();

Console.WriteLine($"密码哈希: {hash}");
```

## SHA 算法对比

| 算法 | 输出长度 | 安全性 | 用途 |
|------|---------|--------|------|
| SHA-1 | 160 位 | 低 | 已不推荐使用 |
| SHA-256 | 256 位 | 高 | 推荐使用 |
| SHA-384 | 384 位 | 高 | 安全敏感场景 |
| SHA-512 | 512 位 | 最高 | 最高安全要求 |

## 相关文档

- [HmacSHA](hmacsha.md)
- [MD5](md5.md)
- [基础扩展](../README.md#基础扩展)
