# UTF-8 编码

SharpDevLib 提供了 UTF-8 格式的编码与解码功能。

## 编码

### Utf8Decode

```csharp
// 将字符串编码为 UTF-8 字节数组
var bytes = "Hello World".Utf8Decode();

// 输出字节数组
Console.WriteLine(BitConverter.ToString(bytes));
// 示例输出: 48-65-6C-6C-6F-20-57-6F-72-6C-64
```

## 解码

### Utf8Encode

```csharp
// 将字节数组解码为 UTF-8 字符串
var bytes = new byte[] { 72, 101, 108, 108, 111 };
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: Hello
```

## 中文编码

### 编码中文

```csharp
// 编码中文字符串
var text = "你好世界";
var bytes = text.Utf8Decode();

Console.WriteLine($"字节长度: {bytes.Length}");
// 示例输出: 字节长度: 12 (每个中文字符 3 字节)
```

### 解码中文

```csharp
// 解码中文字节数组
var bytes = new byte[] { 0xE4, 0xBD, 0xA0, 0xE5, 0xA5, 0xBD };
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: 你好
```

## 混合内容

### 编码混合内容

```csharp
var text = "Hello 你好 World 世界";
var bytes = text.Utf8Decode();

var decoded = bytes.Utf8Encode();
Console.WriteLine(decoded);
// 输出: Hello 你好 World 世界
```

## 完整示例

### 字符串与字节数组转换

```csharp
var text = "Hello 你好";

// 编码为字节数组
var bytes = text.Utf8Decode();
Console.WriteLine($"编码后的字节数: {BitConverter.ToString(bytes)}");

// 解码为字符串
var decoded = bytes.Utf8Encode();
Console.WriteLine($"解码后的文本: {decoded}");
```

### 文件读写

```csharp
// 写入 UTF-8 文本文件
var text = "Hello 你好";
text.Utf8Decode().SaveToFile("output.txt");

// 读取 UTF-8 文本文件
var content = "input.txt".ReadFileBytes();
var decoded = content.Utf8Encode();
Console.WriteLine(decoded);
```

### 网络传输

```csharp
// 发送 UTF-8 编码的消息
var message = "Hello 你好";
var bytes = message.Utf8Decode();
await networkStream.WriteAsync(bytes, 0, bytes.Length);

// 接收 UTF-8 编码的消息
var buffer = new byte[1024];
var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
var received = buffer.Take(bytesRead).ToArray().Utf8Encode();
Console.WriteLine($"收到消息: {received}");
```

## BOM 处理

### UTF-8 BOM

UTF-8 的 BOM (Byte Order Mark) 为 `EF BB BF` 三个字节。

```csharp
// 带 BOM 的字节数组
var bytesWithBom = new byte[] { 0xEF, 0xBB, 0xBF, 0x48, 0x65, 0x6C, 0x6C, 0x6F };
var text = bytesWithBom.Utf8Encode();

Console.WriteLine(text);
// 输出: Hello (BOM 在解码时被正确处理)
```

## 相关文档

- [Base64](base64.md)
- [Url](url.md)
- [Hex](hex.md)
- [基础扩展](../README.md#基础扩展)
