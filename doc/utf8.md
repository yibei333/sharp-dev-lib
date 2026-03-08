# UTF-8 编码

SharpDevLib 提供了 UTF-8 格式的编码与解码功能。

## 编码

##### 字符串编码为UTF-8字节数组

```csharp
var str = "Hello, 世界";
var bytes = str.Utf8Decode();
Console.WriteLine(bytes.Length);
//12 (每个中文字符占3个字节)
```

##### 中文编码

```csharp
var str = "你好世界";
var bytes = str.Utf8Decode();
Console.WriteLine(bytes.Length);
//12
```

##### 空字符串编码

```csharp
var str = "";
var bytes = str.Utf8Decode();
Console.WriteLine(bytes.Length);
//0
```

## 解码

##### UTF-8字节数组解码为字符串

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111, 44, 32 };
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello, 
```

##### 中文解码

```csharp
var bytes = new byte[] { 228, 189, 160, 229, 165, 189 };
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//你好
```

##### 混合解码

```csharp
var bytes = new byte[] { 72, 101, 108, 108, 111, 44, 32, 228, 189, 160 };
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//Hello, 你
```

##### 空字节数组解码

```csharp
var bytes = Array.Empty<byte>();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//(空字符串)
```

## 实际应用

##### 文件读写

```csharp
// 读取文件
var fileBytes = File.ReadAllBytes("data.txt");
var content = fileBytes.Utf8Encode();
Console.WriteLine(content);

// 写入文件
var content = "Hello, 世界";
var fileBytes = content.Utf8Decode();
File.WriteAllBytes("output.txt", fileBytes);
```

##### 网络传输

```csharp
// 发送数据
var message = "Hello, 世界";
var bytes = message.Utf8Decode();
// 发送bytes到网络...

// 接收数据
var receivedBytes = ReceiveFromNetwork();
var message = receivedBytes.Utf8Encode();
```

##### 数据库存储

```csharp
// 存储
var text = "重要数据";
var bytes = text.Utf8Decode();
SaveToDatabase(bytes);

// 读取
var bytes = LoadFromDatabase();
var text = bytes.Utf8Encode();
```

## 相关文档

- [Base64 编码](base64.md)
- [HEX 编码](hex.md)
- [编码](../README.md#编码)
