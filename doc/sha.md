# SHA 哈希

SharpDevLib 提供了 SHA-1、SHA-256、SHA-384、SHA-512 哈希计算功能。

## SHA-1

##### 计算字节数组的SHA-1

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha128();
Console.WriteLine(hash);
//907d14fb3af2b0d4f18c2d46abe8aedce17367bd
```

##### 计算流的SHA-1

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha128();
Console.WriteLine(hash);
//907d14fb3af2b0d4f18c2d46abe8aedce17367bd
```

## SHA-256

##### 计算字节数组的SHA-256

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha256();
Console.WriteLine(hash);
//03675ac53ff9cd1535ccc7dfcdfa2c458c5218371f418dc136f2d19ac1fbe8a5
```

##### 计算流的SHA-256

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha256();
Console.WriteLine(hash);
//03675ac53ff9cd1535ccc7dfcdfa2c458c5218371f418dc136f2d19ac1fbe8a5
```

## SHA-384

##### 计算字节数组的SHA-384

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha384();
Console.WriteLine(hash);
//c7b9b2dbbdd22b8a3a4ad011e326d536f603a404e55af47196a24db6513d877706d122b35d5b45daeb6674f9527e9659
```

##### 计算流的SHA-384

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha384();
Console.WriteLine(hash);
//c7b9b2dbbdd22b8a3a4ad011e326d536f603a404e55af47196a24db6513d877706d122b35d5b45daeb6674f9527e9659
```

## SHA-512

##### 计算字节数组的SHA-512

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Sha512();
Console.WriteLine(hash);
//45546d4d71407e82ecda31eba5bf74b65bc092b0436a2409a6b615c1f78fdb2d3da371758f07a65b5d2b3ee8fa9ea0c772dd1eff884c4c77d4290177b002ccdc
```

##### 计算流的SHA-512

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Sha512();
Console.WriteLine(hash);
//45546d4d71407e82ecda31eba5bf74b65bc092b0436a2409a6b615c1f78fdb2d3da371758f07a65b5d2b3ee8fa9ea0c772dd1eff884c4c77d4290177b002ccdc
```

## 相关文档

- [HMAC-SHA 哈希](hmacsha.md)
- [MD5 哈希](md5.md)
- [HEX 编码](hex.md)
- [哈希](../README.md#哈希)
