# UTF-8 编码

SharpDevLib 提供了 UTF-8 格式的编码与解码功能。

## 编码

##### 字符串编码为UTF-8字节数组

```csharp

var str = "Hello, 世界";
var bytes = str.Utf8Decode();
Console.WriteLine(bytes.Length);
//13 (每个中文字符占3个字节)```
```

## 解码

##### UTF-8字节数组解码为字符串

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111, 44, 32 };
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello, 
```

## 相关文档

- [Base64 编码](base64.md)
- [HEX 编码](hex.md)
- [编码](../README.md#编码)
