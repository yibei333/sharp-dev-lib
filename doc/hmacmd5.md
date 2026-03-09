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
//7569b78ea12037bf735da2239a399db4
```

##### 计算字节数组的HMAC-MD5（16位）

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hmac);
//a12037bf735da223
```

## 流HMAC

##### 计算流的HMAC-MD5（32位）

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacMd5(secret);
Console.WriteLine(hmac);
//7569b78ea12037bf735da2239a399db4
```

##### 计算流的HMAC-MD5（16位）

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacMd5(secret, Md5OutputLength.Sixteen);
Console.WriteLine(hmac);
//a12037bf735da223
```

## 相关文档

- [MD5 哈希](md5.md)
- [HMAC-SHA 哈希](hmacsha.md)
- [SHA 哈希](sha.md)
- [哈希](../README.md#哈希)
