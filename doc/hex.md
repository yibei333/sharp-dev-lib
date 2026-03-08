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

##### 中文字符编码

```csharp
var str = "你好";
var bytes = str.Utf8Decode();
var hexString = bytes.HexStringEncode();
Console.WriteLine(hexString);
//e4bda0e5a5bd
```

##### 空字节数组编码

```csharp
var bytes = Array.Empty<byte>();
var hexString = bytes.HexStringEncode();
Console.WriteLine(hexString);
//(空字符串)
```

##### 单个字节编码

```csharp
var bytes = new byte[] { 0xFF };
var hexString = bytes.HexStringEncode();
Console.WriteLine(hexString);
//ff
```

##### 零值编码

```csharp
var bytes = new byte[] { 0x00, 0x00, 0x00 };
var hexString = bytes.HexStringEncode();
Console.WriteLine(hexString);
//000000
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

##### 中文解码

```csharp
var hexString = "e4bda0e5a5bd";
var bytes = hexString.HexStringDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//你好
```

##### 混合大小写解码

```csharp
var hexString = "48656C6C6F";
var bytes = hexString.HexStringDecode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello
```

##### 空字符串解码

```csharp
var hexString = "";
var bytes = hexString.HexStringDecode();
Console.WriteLine(bytes.Length);
//0
```

## 错误处理

##### 奇数长度错误

```csharp
var hexString = "48656c"; // 长度为7（奇数）
try
{
    var bytes = hexString.HexStringDecode();
}
catch (InvalidDataException ex)
{
    Console.WriteLine(ex.Message);
    //'48656c' is not a valid hex string
}
```

## 实际应用

##### 哈希值表示

```csharp
// MD5哈希
var data = "Hello, World";
var bytes = data.Utf8Decode();
var hash = bytes.Md5();
Console.WriteLine(hash);
//5eb63bbbbe01eeed093cb22bb8f5acdc
```

##### 颜色值转换

```csharp
// 颜色RGB转HEX
var r = (byte)255;
var g = (byte)128;
var b = (byte)64;
var rgbBytes = new[] { r, g, b };
var hexColor = rgbBytes.HexStringEncode();
Console.WriteLine(hexColor);
//ff8040
```

```csharp
// HEX转颜色RGB
var hexColor = "ff8040";
var rgbBytes = hexColor.HexStringDecode();
Console.WriteLine($"R: {rgbBytes[0]}, G: {rgbBytes[1]}, B: {rgbBytes[2]}");
//R: 255, G: 128, B: 64
```

##### MAC地址表示

```csharp
// MAC地址转HEX
var macBytes = new byte[] { 0x00, 0x1A, 0x2B, 0x3C, 0x4D, 0x5E };
var hexMac = string.Join(":", macBytes.Select(b => b.ToString("X2")));
Console.WriteLine(hexMac);
//00:1A:2B:3C:4D:5E
```

##### 二进制数据可视化

```csharp
// 可视化二进制数据
var binaryData = new byte[] { 0x01, 0x02, 0x03, 0xFF, 0xFE, 0xFD };
var hexRepresentation = binaryData.HexStringEncode();
Console.WriteLine(hexRepresentation);
//010203fffefd
```

##### 协议调试

```csharp
// 调试网络协议数据包
var packetData = new byte[] { 0xAA, 0x55, 0x01, 0x00, 0x02, 0x03 };
var hexPacket = packetData.HexStringEncode();
Console.WriteLine($"Packet: {hexPacket}");
//Packet: aa5501000203
```

##### 加密密钥表示

```csharp
// AES密钥
var keyBytes = new byte[16];
Random.Shared.NextBytes(keyBytes);
var hexKey = keyBytes.HexStringEncode();
Console.WriteLine($"AES Key: {hexKey}");
//AES Key: 7b8f9e2d4c5a6b1c3d4e5f6a7b8c9d0
```

##### 数据校验

```csharp
// 计算数据的HEX校验和
var data = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
var checksum = data.Sum(b => b).ToString("X2");
Console.WriteLine($"Checksum: {checksum}");
//Checksum: 0F
```

##### 数据库存储

```csharp
// 存储二进制数据为HEX字符串
var binaryData = File.ReadAllBytes("data.bin");
var hexData = binaryData.HexStringEncode();
SaveToDatabase(hexData);

// 从数据库读取HEX字符串并转换为二进制数据
var hexData = LoadFromDatabase();
var binaryData = hexData.HexStringDecode();
File.WriteAllBytes("restored.bin", binaryData);
```

## 常见格式

##### 大小写格式

```csharp
var bytes = new byte[] { 0xFF, 0xAA, 0x55 };
var hexLower = bytes.HexStringEncode();
var hexUpper = hexLower.ToUpper();

Console.WriteLine(hexLower);
//ffaa55

Console.WriteLine(hexUpper);
//FFAA55
```

##### 分隔符格式

```csharp
var bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };
var hexString = bytes.HexStringEncode();
var formatted = string.Join(" ", Enumerable.Range(0, hexString.Length / 2)
    .Select(i => hexString.Substring(i * 2, 2)));

Console.WriteLine(formatted);
//01 02 03 04
```

## 相关文档

- [Base64 编码](base64.md)
- [UTF-8 编码](utf8.md)
- [编码](../README.md#编码)
