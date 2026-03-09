# Base64Url 编码

SharpDevLib 提供了 Base64 URL 安全格式的编码与解码功能。

Base64 URL 编码替换了标准 Base64 中的 `+` 和 `/` 字符，并移除填充字符 `=`，使其可以安全地在 URL 中使用。

## 编码规则

1. 将 `+` 替换为 `-`
2. 将 `/` 替换为 `_`
3. 移除末尾的 `=` 填充字符

## 编码

##### 字节数组编码为Base64Url字符串

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111 };
var base64Url = bytes.Base64UrlEncode();
Console.WriteLine(base64Url);
//SGVsbG8
```

##### 标准Base64对比

```csharp
var bytes = new byte[] { 0xFF, 0xFF, 0xFF };
var base64 = bytes.Base64Encode();
var base64Url = bytes.Base64UrlEncode();

Console.WriteLine($"标准Base64: {base64}");
//标准Base64: ////

Console.WriteLine($"Base64Url: {base64Url}");
//Base64Url: ____
```

## 解码

##### Base64Url字符串解码为字节数组

```csharp
var base64Url = "SGVsbG8";
var bytes = base64Url.Base64UrlDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

## 相关文档

- [Base64 编码](base64.md)
- [URL 编码](url.md)
- [编码](../README.md#编码)
