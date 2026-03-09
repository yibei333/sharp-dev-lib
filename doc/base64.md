# Base64 编码

SharpDevLib 提供了 Base64 格式的编码与解码功能。

## 编码

##### 字节数组编码为Base64字符串

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111 };
var base64 = bytes.Base64Encode();
Console.WriteLine(base64);
//SGVsbG8=
```

## 解码

##### Base64字符串解码为字节数组

```csharp
var base64 = "SGVsbG8=";
var bytes = base64.Base64Decode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

## 相关文档

- [UTF-8 编码](utf8.md)
- [Base64Url 编码](base64url.md)
- [编码](../README.md#编码)
