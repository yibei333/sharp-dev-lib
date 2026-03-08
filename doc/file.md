# 文件操作

SharpDevLib 提供了丰富的文件操作扩展方法，包括文件路径处理、文件读写、MIME类型识别等功能。

## 路径处理

##### 获取文件扩展名

```csharp
var filePath = "/path/to/document.pdf";
var extension = filePath.GetFileExtension();
Console.WriteLine(extension);
//.pdf
```

##### 获取文件扩展名（不包含点号）

```csharp
var filePath = "/path/to/document.pdf";
var extension = filePath.GetFileExtension(includePoint: false);
Console.WriteLine(extension);
//pdf
```

##### 获取文件名

```csharp
var filePath = "/path/to/document.pdf";
var fileName = filePath.GetFileName();
Console.WriteLine(fileName);
//document.pdf
```

##### 获取文件名（不包含扩展名）

```csharp
var filePath = "/path/to/document.pdf";
var fileName = filePath.GetFileName(includeExtension: false);
Console.WriteLine(fileName);
//document
```

##### 获取文件所在文件夹

```csharp
var filePath = "/path/to/document.pdf";
var directory = filePath.GetFileDirectory();
Console.WriteLine(directory);
///path/to
```

##### 合并路径

```csharp
var leftPath = "/path/to";
var rightPath = "file.txt";
var combinedPath = leftPath.CombinePath(rightPath);
Console.WriteLine(combinedPath);
///path/to/file.txt
```

##### 格式化路径

```csharp
var path = "C:\\Users\\Name\\file.txt";
var formatted = path.FormatPath();
Console.WriteLine(formatted);
//C:/Users/Name/file.txt
```

## 文件读写

##### 字节数组保存到文件

```csharp
var bytes = "Hello, World".Utf8Decode();
bytes.SaveToFile("output.txt");
```

##### 字节数组保存到文件（文件存在时抛出异常）

```csharp
var bytes = "Hello, World".Utf8Decode();
bytes.SaveToFile("output.txt", throwIfFileExist: true);
// 如果文件已存在，抛出 InvalidOperationException
```

##### 异步保存字节数组到文件

```csharp
var bytes = "Hello, World".Utf8Decode();
var cts = new CancellationTokenSource();
await bytes.SaveToFileAsync("output.txt", cts.Token);
```

##### 流保存到文件

```csharp
using var stream = new MemoryStream("Hello, World".Utf8Decode());
stream.SaveToFile("output.txt");
```

##### 流保存到文件（文件存在时抛出异常）

```csharp
using var stream = new MemoryStream("Hello, World".Utf8Decode());
stream.SaveToFile("output.txt", throwIfFileExist: true);
// 如果文件已存在，抛出 InvalidOperationException
```

##### 异步保存流到文件

```csharp
using var stream = new MemoryStream("Hello, World".Utf8Decode());
var cts = new CancellationTokenSource();
await stream.SaveToFileAsync("output.txt", cts.Token);
```

##### 异步拷贝流（带进度回调）

```csharp
using var sourceStream = File.OpenRead("source.txt");
using var targetStream = File.Create("target.txt");

long totalBytes = 0;
await sourceStream.CopyToAsync(targetStream, CancellationToken.None, bytes =>
{
    totalBytes += bytes;
    Console.WriteLine($"已传输: {totalBytes} 字节");
});
```

##### 打开或创建文件流

```csharp
var fileInfo = new FileInfo("data.txt");
using var stream = fileInfo.OpenOrCreate();
// 使用stream进行读写操作
```

## 文件大小格式化

##### 格式化字节大小

```csharp
Console.WriteLine(104L.ToFileSizeString());
//1KB

Console.WriteLine(512L.ToFileSizeString());
//512Byte

Console.WriteLine(2048L.ToFileSizeString());
//2KB

Console.WriteLine(1048576L.ToFileSizeString());
//1MB

Console.WriteLine(1073741824L.ToFileSizeString());
//1GB

Console.WriteLine(1099511627776L.ToFileSizeString());
//1TB

Console.WriteLine(1125899906842624L.ToFileSizeString());
//1PB
```

## MIME类型

##### 获取文件MIME类型

```csharp
var filePath = "document.pdf";
var mimeType = filePath.GetMimeType();
Console.WriteLine(mimeType);
//application/pdf
```

## Base64文件编码

##### 文件转换为Base64字符串

```csharp
"hello".Utf8Decode().SaveToFile("source.txt");
var filePath = "source.txt";
var base64String = filePath.FileBase64Encode();
Console.WriteLine(base64String);
//data:text/plain;base64,aGVsbG8=
```

##### Base64字符串转换为字节数组

```csharp
var base64String = "data:text/plain;base64,aGVsbG8=";
var bytes = base64String.FileBase64Decode();
Console.WriteLine(bytes.Utf8Encode());
//hello
```

## 文件和目录操作

##### 创建目录（如果不存在）

```csharp
var directoryPath = "/path/to/new/directory";
directoryPath.CreateDirectoryIfNotExist();
```

##### 创建文件（如果不存在）

```csharp
var filePath = "/path/to/new/file.txt";
filePath.CreateFileIfNotExist();
```

##### 删除目录（如果存在）

```csharp
var directoryPath = "/path/to/directory";
directoryPath.RemoveDirectoryIfExist();
```

##### 删除文件（如果存在）

```csharp
var filePath = "/path/to/file.txt";
filePath.RemoveFileIfExist();
```

## 异常抛出

##### 目录不存在时抛出异常

```csharp
var directoryPath = "/path/to/nonexistent/directory";
directoryPath.ThrowIfDirectoryNotExist();
// 抛出 Exception: directory '/path/to/nonexistent/directory' not exist
```

##### 文件不存在时抛出异常

```csharp
var filePath = "/path/to/nonexistent/file.txt";
filePath.ThrowIfFileNotExist();
// 抛出 FileNotFoundException
```

## 常见MIME类型

| 扩展名 | MIME类型 |
|---------|----------|
| .txt | text/plain |
| .html | text/html |
| .css | text/css |
| .js | application/javascript |
| .json | application/json |
| .pdf | application/pdf |
| .zip | application/zip |
| .jpg | image/jpeg |
| .png | image/png |
| .gif | image/gif |
| .svg | image/svg+xml |
| .mp3 | audio/mpeg |
| .mp4 | video/mp4 |
| .avi | video/x-msvideo |

## 相关文档

- [字符串操作](string.md)
- [UTF-8 编码](utf8.md)
- [Base64 编码](base64.md)
- [基础扩展](../README.md#基础扩展)
