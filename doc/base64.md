# Base64 编码

SharpDevLib 提供了 Base64 格式的编码与解码功能。

## 编码

### Base64Encode

```csharp
// 将字节数组编码为 Base64 字符串
var bytes = "Hello World".Utf8Decode();
var base64 = bytes.Base64Encode();

Console.WriteLine(base64);
// 输出: SGVsbG8gV29ybGQ=
```

## 解码

### Base64Decode

```csharp
// 将 Base64 字符串解码为原始字节数组
var base64 = "SGVsbG8gV29ybGQ=";
var bytes = base64.Base64Decode();

var text = bytes.Utf8Encode();
Console.WriteLine(text);
// 输出: Hello World
```

## 中文编码

### 编码中文

```csharp
// 编码中文字符串
var text = "你好世界";
var bytes = text.Utf8Decode();
var base64 = bytes.Base64Encode();

Console.WriteLine(base64);
// 输出: 5L2g5aW95LiW55WM
```

### 解码中文

```csharp
// 解码中文字符串
var base64 = "5L2g5aW95LiW55WM";
var bytes = base64.Base64Decode();
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: 你好世界
```

## 混合内容

### 编码混合内容

```csharp
var text = "Hello 你好 World 世界";
var bytes = text.Utf8Decode();
var base64 = bytes.Base64Encode();

Console.WriteLine(base64);
// 输出: SGVsbG8gIOS9oOWlvSBUb3JsZCDliJXlpY4=
```

### 解码混合内容

```csharp
var base64 = "SGVsbG8gIOS9oOWlvSBUb3JsZCDliJXlpY4=";
var bytes = base64.Base64Decode();
var text = bytes.Utf8Encode();

Console.WriteLine(text);
// 输出: Hello 你好 World 世界
```

## 二进制数据

### 编码二进制数据

```csharp
// 编码二进制数据
var bytes = new byte[] { 0x00, 0x01, 0x02, 0xFF, 0xFE, 0xFD };
var base64 = bytes.Base64Encode();

Console.WriteLine(base64);
// 输出: AAEC/v79
```

### 解码二进制数据

```csharp
// 解码二进制数据
var base64 = "AAEC/v79";
var bytes = base64.Base64Decode();

Console.WriteLine(BitConverter.ToString(bytes));
// 输出: 00-01-02-FF-FE-FD
```

## 文件操作

### 编码文件

```csharp
// 将文件编码为 Base64
var fileBytes = File.ReadAllBytes("document.pdf");
var base64 = fileBytes.Base64Encode();

// 保存 Base64 到文本文件
base64.Utf8Decode().SaveToFile("document.base64");
```

### 解码文件

```csharp
// 从 Base64 恢复文件
var base64 = "document.base64".ReadFileText();
var bytes = base64.Base64Decode();

// 保存为文件
bytes.SaveToFile("document_restored.pdf");
```

## 完整示例

### 字符串与 Base64 转换

```csharp
// 字符串转 Base64
var text = "Hello 你好";
var base64 = text.Utf8Decode().Base64Encode();
Console.WriteLine($"Base64: {base64}");

// Base64 转字符串
var decoded = base64.Base64Decode().Utf8Encode();
Console.WriteLine($"解码: {decoded}");
```

### 图片转 Base64

```csharp
// 读取图片文件
var imageBytes = File.ReadAllBytes("image.png");

// 编码为 Base64
var base64 = imageBytes.Base64Encode();

// 创建 data URI (用于 HTML)
var dataUri = $"data:image/png;base64,{base64}";
Console.WriteLine(dataUri);
```

### Base64 数据传输

```csharp
// 发送 Base64 编码的数据
var data = "敏感数据";
var base64 = data.Utf8Decode().Base64Encode();
await SendDataAsync(base64);

// 接收 Base64 编码的数据
var received = await ReceiveDataAsync();
var bytes = received.Base64Decode();
var decoded = bytes.Utf8Encode();
Console.WriteLine($"接收到的数据: {decoded}");
```

### Base64 文本存储

```csharp
// 将二进制数据存储为文本
var binaryData = new byte[] { /* 二进制数据 */ };
var base64 = binaryData.Base64Encode();
base64.Utf8Decode().SaveToFile("data.txt");

// 从文本恢复二进制数据
var storedBase64 = "data.txt".ReadFileText();
var restoredData = storedBase64.Base64Decode();
```

## 注意事项

### 编码规则

- Base64 编码将 3 个字节转换为 4 个字符
- 不足 3 字节时使用 `=` 填充
- Base64 字符集：`A-Z`, `a-z`, `0-9`, `+`, `/`

### 大小写敏感

Base64 是大小写敏感的：

```csharp
var base64 = "SGVsbG8=";  // 大写

var bytes = base64.Base64Decode();
// 正确解码

var base64 = "sgvsbgo=";  // 小写
var bytes = base64.Base64Decode();
// 解码结果不同
```

### 填充字符

```csharp
// 1 字节 -> 2 字符 + 2 个 =
var bytes1 = new byte[] { 0x41 };
var base64 = bytes1.Base64Encode();
// 输出: QQ==

// 2 字节 -> 3 字符 + 1 个 =
var bytes2 = new byte[] { 0x41, 0x42 };
var base64 = bytes2.Base64Encode();
// 输出: QUI=

// 3 字节 -> 4 字符
var bytes3 = new byte[] { 0x41, 0x42, 0x43 };
var base64 = bytes3.Base64Encode();
// 输出: QUJD
```

## 相关文档

- [Base64Url](base64url.md)
- [Utf8](utf8.md)
- [Hex](hex.md)
- [基础扩展](../README.md#基础扩展)
