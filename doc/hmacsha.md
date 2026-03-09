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
//1f44504fd184b6ce8d337f587b793e032a42f895
```

##### 计算流的HMAC-SHA1

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha128(secret);
Console.WriteLine(hmac);
//1f44504fd184b6ce8d337f587b793e032a42f895
```

## HMAC-SHA256

##### 计算字节数组的HMAC-SHA256

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha256(secret);
Console.WriteLine(hmac);
//b0442e3b5164e2743348706e5fb284e056a386eaf8740d4f2f936fd5857bf149
```

##### 计算流的HMAC-SHA256

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha256(secret);
Console.WriteLine(hmac);
//b0442e3b5164e2743348706e5fb284e056a386eaf8740d4f2f936fd5857bf149
```

## HMAC-SHA384

##### 计算字节数组的HMAC-SHA384

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha384(secret);
Console.WriteLine(hmac);
//834e7c33e8445ca60bc8bb4d7bd6d064a46e7adac12238c3489fe6910e5caf04f4515eac1d595921fc252a41d8a4dfb0
```

##### 计算流的HMAC-SHA384

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha384(secret);
Console.WriteLine(hmac);
//834e7c33e8445ca60bc8bb4d7bd6d064a46e7adac12238c3489fe6910e5caf04f4515eac1d595921fc252a41d8a4dfb0
```

## HMAC-SHA512

##### 计算字节数组的HMAC-SHA512

```csharp
var data = "Hello, World".Utf8Decode();
var secret = "secret-key".Utf8Decode();
var hmac = data.HmacSha512(secret);
Console.WriteLine(hmac);
//5e3e7e03557a69d78c7b22ebce34456baff937fe32f70638a3b03e78f250aa34cc2f9223a542748ca947814cff3468e2d844930f9b3d9d55474e4e6751fefe51
```

##### 计算流的HMAC-SHA512

```csharp

"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var secret = "secret-key".Utf8Decode();
var hmac = stream.HmacSha512(secret);
Console.WriteLine(hmac);
//5e3e7e03557a69d78c7b22ebce34456baff937fe32f70638a3b03e78f250aa34cc2f9223a542748ca947814cff3468e2d844930f9b3d9d55474e4e6751fefe51
```

## 相关文档

- [SHA 哈希](sha.md)
- [MD5 哈希](md5.md)
- [HEX 编码](hex.md)
- [哈希](../README.md#哈希)
