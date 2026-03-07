# HEX 编码

SharpDevLib 提供了十六进制（Hex）格式的编码与解码功能。

## 编码

### HexStringEncode

```csharp
// 将字节数组编码为十六进制字符串
var bytes = "Hello".Utf8Decode();
var hex = bytes.HexStringEncode();

Console.WriteLine(hex);
// 输出: 48656c6c6f
```

## 解码

### HexStringDecode

```csharp
// 将十六进制字符串解码为原始字节数组
var hex = "48656c6c6f";
var bytes = hex.HexStringDecode();

var text = bytes.Utf8Encode();
Console.WriteLine(text);
// 输出: Hello
```

## 大小写

### 小写十六进制

```csharp
var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
var hex = bytes.HexStringEncode();

Console.WriteLine(hex);
// 输出: 48656c6c6f (小写)
```

### 大小写不敏感

```csharp
// 小写十六进制
var hexLower = "48656c6c6f";
var bytes1 = hexLower.HexStringDecode();
var text1 = bytes1.Utf8Encode();
Console.WriteLine(text1);
// 输出: Hello

// 大写十六进制
var hexUpper = "48656C6C6F";
var bytes2 = hexUpper.HexStringDecode();
var text2 = bytes2.Utf8Encode();
Console.WriteLine(text2);
// 输出: Hello
```

## 中文编码

### 编码中文

```csharp
var text = "你好";
var bytes = text.Utf8Decode();
var hex = bytes.HexStringEncode();

Console.WriteLine(hex);
// 输出: e4bda0e5a5bd
```

### 解码中文

```csharp
var hex = "e4bda0e5a5bd";
var bytes = hex.HexStringDecode();
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: 你好
```

## 二进制数据

### 编码二进制数据

```csharp
var bytes = new byte[] { 0x00, 0xFF, 0x0A, 0xF0, 0x15 };
var hex = bytes.HexStringEncode();

Console.WriteLine(hex);
// 输出: 00ff0af015
```

### 解码二进制数据

```csharp
var hex = "00ff0af015";
var bytes = hex.HexStringDecode();

Console.WriteLine(BitConverter.ToString(bytes));
// 输出: 00-FF-0A-F0-15
```

## 完整示例

### 字符串与十六进制转换

```csharp
// 字符串转十六进制
var text = "Hello 你好";
var bytes = text.Utf8Decode();
var hex = bytes.HexStringEncode();
Console.WriteLine($"十六进制: {hex}");

// 十六进制转字符串
var decodedBytes = hex.HexStringDecode();
var decodedText = decodedBytes.Utf8Encode();
Console.WriteLine($"解码: {decodedText}");
```

### 格式化输出

```csharp
var data = "Hello World 你好";
var bytes = data.Utf8Decode();

// 格式化输出为每两个字符一个空格
var hex = bytes.HexStringEncode();
var formatted = string.Join(" ", Enumerable.Range(0, hex.Length / 2)
    .Select(i => hex.Substring(i * 2, 2)));

Console.WriteLine(formatted);
// 输出: 48 65 6c 6c 6f 20 57 6f 72 6c 64 20 e4 bd a0 e5 a5 bd
```

### 每字节单独处理

```csharp
var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

foreach (var b in bytes)
{
    var hex = new[] { b }.HexStringEncode();
    Console.WriteLine($"0x{b:X2} -> {hex}");
}
// 输出:
// 0x48 -> 48
// 0x65 -> 65
// 0x6C -> 6c
// 0x6C -> 6c
// 0x6F -> 6f
```

### 验证校验和

```csharp
// 计算数据的校验和
var data = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
var checksum = data.Aggregate((sum, b) => (byte)((sum + b) % 256));

// 转换为十六进制
var hexChecksum = new[] { checksum }.HexStringEncode();
Console.WriteLine($"校验和: 0x{hexChecksum}");
// 输出: 校验和: 0x0f
```

### 颜色值转换

```csharp
// RGB 颜色值
var r = 255;
var g = 128;
var b = 64;

var rgbBytes = new[] { (byte)r, (byte)g, (byte)b };
var hexColor = rgbBytes.HexStringEncode();

Console.WriteLine($"RGB({r}, {g}, {b}) -> #{hexColor}");
// 输出: RGB(255, 128, 64) -> #ff8040
```

## 常见用途

### MAC 地址

```csharp
var macBytes = new byte[] { 0x00, 0x1A, 0x2B, 0x3C, 0x4D, 0x5E };
var macHex = macBytes.HexStringEncode();
var formattedMac = string.Join(":", Enumerable.Range(0, macHex.Length / 2)
    .Select(i => macHex.Substring(i * 2, 2)));

Console.WriteLine($"MAC 地址: {formattedMac}");
// 输出: MAC 地址: 00:1a:2b:3c:4d:5e
```

### 查看二进制数据

```csharp
var fileBytes = "data.bin".ReadFileBytes();

// 显示前 16 字节的十六进制
var previewBytes = fileBytes.Take(16).ToArray();
var hex = previewBytes.HexStringEncode();

Console.WriteLine($"前 16 字节: {hex}");
```

### 数据校验

```csharp
// 生成数据的十六进制摘要
var data = "重要数据".Utf8Decode();
var hex = data.HexStringEncode();

// 传输数据
TransmitData(data);

// 接收方验证
var received = ReceiveData();
var receivedHex = received.HexStringEncode();

if (hex == receivedHex)
{
    Console.WriteLine("数据验证通过");
}
else
{
    Console.WriteLine("数据验证失败");
}
```

## 注意事项

### 十六进制格式

- 有效的十六进制字符：`0-9`, `a-f`, `A-F`
- 每个字节由 2 个十六进制字符表示
- 长度必须是偶数

### 无效输入

```csharp
// 长度为奇数
var hex = "486";  // 无效

// 包含非十六进制字符
var hex = "48656c6cg";  // 'g' 不是有效的十六进制字符

// 这些情况会抛出异常
```

### 字节顺序

十六进制字符串的字节顺序与原始字节数组一致：

```csharp
var bytes = new byte[] { 0x48, 0x65 };
var hex = bytes.HexStringEncode();
// 输出: 4865 (不是 6548)
```

## 相关文档

- [Base64](base64.md)
- [Utf8](utf8.md)
- [基础扩展](../README.md#基础扩展)
