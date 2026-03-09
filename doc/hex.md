# HEX 编码

SharpDevLib 提供了 16 进制格式的编码与解码功能。

## 编码

##### 字节数组编码为16进制字符串

```csharp
var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
var hexString = bytes.HexStringEncode();
Console.WriteLine(hexString);
//48656c6c6f
```

## 解码

##### 16进制字符串解码为字节数组

```csharp
var hexString = "48656c6c6f";
var bytes = hexString.HexStringDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

## 相关文档

- [Base64 编码](base64.md)
- [UTF-8 编码](utf8.md)
- [编码](../README.md#编码)
