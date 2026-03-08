# 文件操作

SharpDevLib 提供了丰富的文件操作扩展方法。

## 文件大小格式化

##### 格式化为可读大小

```csharp
var size = 1024L;
var formatted = size.FormatFileSize();
Console.WriteLine(formatted);
//1.00 KB
```

## 路径处理

##### 格式化路径

```csharp
var path = "/path/to//file";
var formatted = path.FormatPath();
Console.WriteLine(formatted);
///path/to/file
```

##### 获取文件扩展名

```csharp
var path = "/path/to/file.txt";
var extension = path.GetFileExtension();
Console.WriteLine(extension);
//.txt
```

##### 获取文件名（不含扩展名）

```csharp
var path = "/path/to/file.txt";
var fileName = path.GetFileNameWithoutExtension();
Console.WriteLine(fileName);
//file
```

## MIME类型

##### 获取MIME类型

```csharp
var extension = ".pdf";
var mimeType = extension.GetMimeType();
Console.WriteLine(mimeType);
//application/pdf
```

##### 批量获取MIME类型

```csharp
var extensions = new[] { ".pdf", ".jpg", ".mp4" };
var mimeTypes = extensions.GetMimeTypes();
foreach (var mimeType in mimeTypes)
{
    Console.WriteLine(mimeType);
}
//application/pdf
//image/jpeg
//video/mp4
```

## 文件读写

##### 读取文件所有文本

```csharp
var path = "/path/to/file.txt";
var content = path.ReadAllText();
Console.WriteLine(content);
```

##### 写入文本到文件

```csharp
var path = "/path/to/file.txt";
var content = "Hello, World!";
path.WriteAllText(content);
```

##### 追加文本到文件

```csharp
var path = "/path/to/file.txt";
var content = "New line";
path.AppendAllText(content);
```

## 文件操作

##### 复制文件

```csharp
var sourcePath = "/path/to/source.txt";
var destinationPath = "/path/to/destination.txt";
sourcePath.CopyFile(destinationPath, overwrite: true);
```

##### 移动文件

```csharp
var sourcePath = "/path/to/source.txt";
var destinationPath = "/path/to/destination.txt";
sourcePath.MoveFile(destinationPath);
```

##### 删除文件

```csharp
var path = "/path/to/file.txt";
path.DeleteFile();
```

## 目录操作

##### 创建目录

```csharp
var path = "/path/to/new/directory";
path.CreateDirectory();
```

##### 删除目录

```csharp
var path = "/path/to/directory";
path.DeleteDirectory(recursive: true);
```

##### 检查目录是否存在

```csharp
var path = "/path/to/directory";
var exists = path.DirectoryExists();
Console.WriteLine(exists);
//True
```

##### 检查文件是否存在

```csharp
var path = "/path/to/file.txt";
var exists = path.FileExists();
Console.WriteLine(exists);
//True
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
