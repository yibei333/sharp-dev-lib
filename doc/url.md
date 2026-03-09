# URL 编码

SharpDevLib 提供了 URL 格式的编码与解码功能。

## 编码

##### 字节数组编码为URL编码字符串

```csharp
var bytes = "Hello World".Utf8Decode();
var urlEncoded = bytes.UrlEncode();
Console.WriteLine(urlEncoded);
//Hello+World
```

## 解码

##### URL编码字符串解码为字节数组

```csharp
var urlEncoded = "Hello+World";
var bytes = urlEncoded.UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello World
```

## 常见编码字符

| 原字符 | URL编码 |
|--------|---------|
| 空格    | `+`     |
| `!`     | `%21`   |
| `@`     | `%40`   |
| `#`     | `%23`   |
| `$`     | `%24`   |
| `%`     | `%25`   |
| `&`     | `%26`   |
| `=`     | `%3D`   |
| `?`     | `%3F`   |

## 相关文档

- [UTF-8 编码](utf8.md)
- [Base64Url 编码](base64url.md)
- [编码](../README.md#编码)
