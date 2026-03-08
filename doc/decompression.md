# DeCompression - 解压缩

提供压缩文件的解压功能。

## 枚举

### CompressionFormat

压缩文件格式枚举。

| 值 | 说明 | 扩展名 |
| --- | --- | --- |
| UnKnown | 未知格式 | - |
| Zip | ZIP 格式 | .zip |
| Rar | RAR 格式 | .rar |
| SevenZip | 7-Zip 格式 | .7z |
| Tar | TAR 格式 | .tar |
| Gz | GZIP 格式 | .tgz, .tar.gz |
| Xz | XZ 格式 | .tar.xz |
| Bz2 | BZIP2 格式 | .bz2 |

## 类

### DeCompressRequest

解压请求，用于配置压缩文件的解压操作。

#### 构造函数

```csharp
public DeCompressRequest(string sourceFile, string targetPath)
```

| 参数 | 说明 |
| --- | --- |
| sourceFile | 要解压的压缩文件路径 |
| targetPath | 解压文件保存的目标路径 |

#### 属性

| 属性 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| SourceFile | string | 构造函数参数 | 要解压的压缩文件路径 |
| TargetPath | string | 构造函数参数 | 解压文件保存的目标路径 |
| Format | CompressionFormat | 自动推断 | 根据源文件自动推断的解压文件格式 |
| Password | string? | null | 压缩文件的密码（如果需要解密） |
| CancellationToken | CancellationToken? | null | 取消令牌，用于取消长时间运行的解压操作 |
| OnProgress | Action\<CompressionProgressArgs\>? | null | 进度变化回调函数，用于接收解压进度更新 |

### CompressionProgressArgs

压缩/解压进度参数，用于报告压缩或解压操作的进度信息。

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| CurrentName | string? | 当前正在处理的文件名称 |
| Total | double | 需要处理的总字节数 |
| Trasnsfed | double | 已处理的字节数 |
| Progress | double | 进度百分比，范围从 0 到 100 |
| ProgressText | string | 进度文本，包含百分号 |

## 扩展方法

### DeCompressAsync

异步解压文件。

#### 方法签名

```csharp
public static Task DeCompressAsync(this DeCompressRequest request)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | DeCompressRequest | 解压请求配置 |

#### 返回值

表示异步解压任务的 Task。

## 示例

### 基本解压

```csharp
// 解压 ZIP 文件
var request = new DeCompressRequest("data.zip", "./output");
await request.DeCompressAsync();
```

### 解压不同格式的文件

```csharp
// 解压 ZIP 格式
var zipRequest = new DeCompressRequest("data.zip", "./output");
await zipRequest.DeCompressAsync();

// 解压 TAR 格式
var tarRequest = new DeCompressRequest("data.tar", "./output");
await tarRequest.DeCompressAsync();

// 解压 TAR.GZ 格式
var tarGzRequest = new DeCompressRequest("data.tar.gz", "./output");
await tarGzRequest.DeCompressAsync();

// 解压 BZ2 格式
var bz2Request = new DeCompressRequest("data.tar.bz2", "./output");
await bz2Request.DeCompressAsync();
```

### 解压加密文件

```csharp
// 解压带密码的 ZIP 文件
var request = new DeCompressRequest("encrypted.zip", "./output")
{
    Password = "my_password"
};
await request.DeCompressAsync();
```

### 监听解压进度

```csharp
var request = new DeCompressRequest("largefile.zip", "./output")
{
    OnProgress = args =>
    {
        Console.WriteLine($"正在解压: {args.CurrentName}");
        Console.WriteLine($"进度: {args.ProgressText}");
        Console.WriteLine($"已处理: {args.Trasnsfed} / {args.Total}");
    }
};
await request.DeCompressAsync();
```

### 使用取消令牌

```csharp
var cts = new CancellationTokenSource();
var request = new DeCompressRequest("largefile.zip", "./output")
{
    CancellationToken = cts.Token
};

// 异步解压
var task = request.DeCompressAsync();

// 5秒后取消
await Task.Delay(5000);
cts.Cancel();

try
{
    await task;
}
catch (OperationCanceledException)
{
    Console.WriteLine("解压已取消");
}
```

### 解压到指定目录

```csharp
// 解压到新目录
var request = new DeCompressRequest("data.zip", "./myfolder/output");
await request.DeCompressAsync();
```

### 批量解压

```csharp
// 解压多个文件
var files = new[] { "file1.zip", "file2.zip", "file3.zip" };
foreach (var file in files)
{
    var request = new DeCompressRequest(file, $"./output/{Path.GetFileNameWithoutExtension(file)}");
    await request.DeCompressAsync();
}
```

### 综合示例

```csharp
var cts = new CancellationTokenSource();
var request = new DeCompressRequest("backup.zip", "./restore")
{
    Password = "password123",
    CancellationToken = cts.Token,
    OnProgress = args =>
    {
        Console.WriteLine($"[{args.ProgressText}] {args.CurrentName}");
    }
};

Console.WriteLine("开始解压...");
await request.DeCompressAsync();
Console.WriteLine("解压完成！");
```

### 错误处理

```csharp
try
{
    var request = new DeCompressRequest("invalid.zip", "./output");
    await request.DeCompressAsync();
}
catch (FileNotFoundException)
{
    Console.WriteLine("文件不存在");
}
catch (InvalidDataException)
{
    Console.WriteLine("文件格式不正确");
}
catch (Exception ex)
{
    Console.WriteLine($"解压失败: {ex.Message}");
}
```

## 特性

- 支持多种压缩格式（ZIP、TAR、GZ、BZ2 等）
- 支持密码解密
- 支持取消操作
- 支持进度回调
- 自动识别压缩文件格式
- 异步操作，不阻塞主线程
- 可以指定解压目标目录
