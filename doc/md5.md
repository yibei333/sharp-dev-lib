# MD5 哈希

SharpDevLib 提供了 MD5 哈希计算功能。

## 字节数组哈希

##### 计算字节数组的MD5（32位）

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Md5();
Console.WriteLine(hash);
//82bb413746aee42f89dea2b59614f9ef
```

##### 计算字节数组的MD5（16位）

```csharp
var bytes = "Hello, World".Utf8Decode();
var hash = bytes.Md5(Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//46aee42f89dea2b5
```

## 流哈希

##### 计算文件的MD5（32位）

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Md5();
Console.WriteLine(hash);
//82bb413746aee42f89dea2b59614f9ef
```

##### 计算文件的MD5（16位）

```csharp
"Hello, World".Utf8Decode().SaveToFile("data.txt");
using var stream = File.OpenRead("data.txt");
var hash = stream.Md5(Md5OutputLength.Sixteen);
Console.WriteLine(hash);
//46aee42f89dea2b5
```

## 相关文档

- [HMAC-MD5 哈希](hmacmd5.md)
- [SHA 哈希](sha.md)
- [HMAC-SHA 哈希](hmacsha.md)
- [哈希](../README.md#哈希)
