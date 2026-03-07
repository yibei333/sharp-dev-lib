# 文件操作

SharpDevLib 提供了文件读写、路径处理等扩展方法。

## 保存文件

### 保存字节数组

```csharp
// 保存字节数组到文件
var bytes = "Hello World".Utf8Decode();
bytes.SaveToFile("output.txt");
```

### 保存流到文件

```csharp
// 保存流到文件
using var stream = new MemoryStream(bytes);
stream.SaveToFile("output.txt");
```

## 读取文件

### 读取文件文本

```csharp
// 读取文件文本
var content = "input.txt".ReadFileText();
```

### 读取文件字节

```csharp
// 读取文件字节
var bytes = "input.txt".ReadFileBytes();
```

## 文件路径处理

### GetFileExtension

```csharp
// 获取文件扩展名（带点）
var ext = "file.txt".GetFileExtension(includePoint: true);
// 结果: ".txt"

// 获取文件扩展名（不带点）
var ext = "file.txt".GetFileExtension(includePoint: false);
// 结果: "txt"

var ext = "/path/to/file.tar.gz".GetFileExtension(includePoint: true);
// 结果: ".gz"
```

### GetFileName

```csharp
// 获取文件名（带扩展名）
var name = "/path/to/file.txt".GetFileName(includeExtension: true);
// 结果: "file.txt"

// 获取文件名（不带扩展名）
var name = "/path/to/file.txt".GetFileName(includeExtension: false);
// 结果: "file"

var name = "C:\\path\\to\\file.txt".GetFileName(includeExtension: true);
// 结果: "file.txt"
```

### GetFileDirectory

```csharp
// 获取文件目录
var directory = "/path/to/file.txt".GetFileDirectory();
// 结果: "/path/to"

var directory = "C:\\path\\to\\file.txt".GetFileDirectory();
// 结果: "C:\\path\\to"

var directory = "file.txt".GetFileDirectory();
// 结果: ""
```

## 删除文件

### RemoveFileIfExist

```csharp
// 删除文件（如果存在）
"file.txt".RemoveFileIfExist();

// 如果文件不存在也不会抛出异常
"nonexistent.txt".RemoveFileIfExist();
```

## 文件哈希

### FileMd5

```csharp
// 计算文件的 MD5
var hash = "file.txt".FileMd5();

// 16 位 MD5
var hash = "file.txt".FileMd5(Md5OutputLength.Sixteen);
```

## 完整示例

### 读取、处理、写入文件

```csharp
// 读取文件
var content = "input.txt".ReadFileText();

// 处理内容
var processed = content.ToUpper();

// 写入文件
processed.Utf8Decode().SaveToFile("output.txt");
```

### 文件备份

```csharp
// 读取原文件
var content = "data.txt".ReadFileBytes();

// 计算哈希
var hash = "data.txt".FileMd5();
Console.WriteLine($"文件哈希: {hash}");

// 保存备份
content.SaveToFile($"data_{DateTime.Now:yyyyMMddHHmmss}.txt");
```

## 相关文档

- [进程](process.md)
- [基础扩展](../README.md#基础扩展)
