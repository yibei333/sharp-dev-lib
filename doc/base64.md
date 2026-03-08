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

##### 中文编码

```csharp
var str = "你好";
var bytes = str.Utf8Decode();
var base64 = bytes.Base64Encode();
Console.WriteLine(base64);
//5L2g5aW9
```

##### 图片编码

```csharp
var imageBytes = File.ReadAllBytes("image.png");
var base64 = imageBytes.Base64Encode();
Console.WriteLine(base64);
//iVBORw0KGgo...
```

##### 空字节数组编码

```csharp
var bytes = Array.Empty<byte>();
var base64 = bytes.Base64Encode();
Console.WriteLine(base64);
//(空字符串)
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

##### 中文解码

```csharp
var base64 = "5L2g5aW9";
var bytes = base64.Base64Decode();
var str = bytes.Utf8Encode();
Console.WriteLine(str);
//你好
```

##### 图片解码

```csharp
var base64 = "iVBORw0KGgo...";
var bytes = base64.Base64Decode();
File.WriteAllBytes("image.png", bytes);
```

##### 空字符串解码

```csharp
var base64 = "";
var bytes = base64.Base64Decode();
Console.WriteLine(bytes.Length);
//0
```

## 实际应用

##### 数据传输

```csharp
// 序列化对象
var user = new { Name = "Alice", Age = 25 };
var json = user.Serialize();
var bytes = json.Utf8Decode();
var base64 = bytes.Base64Encode();

// 传输base64字符串...
Send(base64);

// 接收并反序列化
var receivedBase64 = Receive();
var bytes = receivedBase64.Base64Decode();
var json = bytes.Utf8Encode();
var user = json.DeSerialize<UserDto>();
```

##### 图片存储

```csharp
// 读取图片并转为Base64
var imagePath = "photo.jpg";
var imageBytes = File.ReadAllBytes(imagePath);
var base64 = imageBytes.Base64Encode();

// 存储到数据库
SaveImageToDatabase(base64);

// 从数据库读取并保存
var base64 = LoadImageFromDatabase();
var imageBytes = base64.Base64Decode();
File.WriteAllBytes("restored.jpg", imageBytes);
```

##### API参数传递

```csharp
// 构造API请求
var data = new { Key = "value" };
var json = data.Serialize();
var bytes = json.Utf8Decode();
var base64 = bytes.Base64Encode();

// 发送请求
var response = await HttpClient.PostAsJsonAsync("/api/endpoint", new { Data = base64 });
```

##### 加密数据传输

```csharp
// 加密数据
var plaintext = "敏感数据";
var bytes = plaintext.Utf8Decode();
var encrypted = Encrypt(bytes);
var base64 = encrypted.Base64Encode();

// 传输...

// 解密数据
var base64 = Receive();
var encrypted = base64.Base64Decode();
var decrypted = Decrypt(encrypted);
var plaintext = decrypted.Utf8Encode();
```

## 相关文档

- [UTF-8 编码](utf8.md)
- [Base64Url 编码](base64url.md)
- [编码](../README.md#编码)
