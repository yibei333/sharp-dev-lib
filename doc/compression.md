# Compression - 压缩

提供文件和目录的压缩功能。

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

### CompressionLevel

压缩级别枚举，控制压缩速度和压缩率的权衡。

| 值 | 说明 |
| --- | --- |
| Normal | 正常压缩级别，平衡压缩速度和压缩率 |
| Fastest | 最快压缩级别，压缩速度最快但压缩率较低 |
| MinimumSize | 最小尺寸压缩级别，压缩率最高但压缩速度最慢 |

## 类

### CompressRequest

压缩请求，用于配置文件或目录的压缩操作。

#### 构造函数

```csharp
public CompressRequest(List<string> sourcePaths, string targetPath)
```

| 参数 | 说明 |
| --- | --- |
| sourcePaths | 要压缩的文件或目录路径集合 |
| targetPath | 保存压缩文件的目标路径 |

#### 属性

| 属性 | 类型 | 默认值 | 说明 |
| --- | --- | --- | --- |
| SourcePaths | List\<string\> | 构造函数参数 | 要压缩的文件或目录路径集合 |
| TargetPath | string | 构造函数参数 | 保存压缩文件的目标路径 |
| IncludeSourceDiretory | bool | false | 是否在压缩时包含目录的名称结构 |
| Level | CompressionLevel | Normal | 压缩级别 |
| Password | string? | null | 压缩文件的密码（如果需要加密） |
| CancellationToken | CancellationToken? | null | 取消令牌，用于取消长时间运行的压缩操作 |
| OnProgress | Action\<CompressionProgressArgs\>? | null | 进度变化回调函数，用于接收压缩进度更新 |

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

### CompressAsync

异步压缩文件或目录。

#### 方法签名

```csharp
public static Task CompressAsync(this CompressRequest request)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | CompressRequest | 压缩请求配置 |

#### 返回值

表示异步压缩任务的 Task。

## 示例

### 基本压缩

```csharp
// 压缩单个文件
var request = new CompressRequest(["data.txt"], "data.zip");
await request.CompressAsync();
```

### 压缩多个文件

```csharp
// 压缩多个文件
var request = new CompressRequest(
    ["data1.txt", "data2.txt", "data3.txt"],
    "files.zip"
);
await request.CompressAsync();
```

### 压缩目录

```csharp
// 压缩目录
var request = new CompressRequest(["./myfolder"], "folder.zip");
await request.CompressAsync();
```

### 压缩多个目录

```csharp
// 压缩多个目录
var request = new CompressRequest(
    ["./folder1", "./folder2", "./folder3"],
    "folders.zip"
);
await request.CompressAsync();
```

### 混合压缩文件和目录

```csharp
// 同时压缩文件和目录
var request = new CompressRequest(
    ["./data.txt", "./folder1", "./data2.txt"],
    "mixed.zip"
);
await request.CompressAsync();
```

### 设置压缩级别

```csharp
// 最快压缩
var request1 = new CompressRequest(["data.txt"], "data_fast.zip")
{
    Level = CompressionLevel.Fastest
};
await request1.CompressAsync();

// 最小尺寸压缩
var request2 = new CompressRequest(["data.txt"], "data_small.zip")
{
    Level = CompressionLevel.MinimumSize
};
await request2.CompressAsync();

// 正常压缩（默认）
var request3 = new CompressRequest(["data.txt"], "data_normal.zip")
{
    Level = CompressionLevel.Normal
};
await request3.CompressAsync();
```

### 包含目录结构

```csharp
// 包含目录结构
var request = new CompressRequest(["./myfolder"], "folder.zip")
{
    IncludeSourceDiretory = true
};
await request.CompressAsync();
```

### 监听压缩进度

```csharp
var request = new CompressRequest(["./largefile"], "largefile.zip")
{
    OnProgress = args =>
    {
        Console.WriteLine($"正在处理: {args.CurrentName}");
        Console.WriteLine($"进度: {args.ProgressText}");
        Console.WriteLine($"已处理: {args.Trasnsfed} / {args.Total}");
    }
};
await request.CompressAsync();
```

### 使用取消令牌

```csharp
var cts = new CancellationTokenSource();
var request = new CompressRequest(["./largefile"], "largefile.zip")
{
    CancellationToken = cts.Token
};

// 异步压缩
var task = request.CompressAsync();

// 5秒后取消
await Task.Delay(5000);
cts.Cancel();

try
{
    await task;
}
catch (OperationCanceledException)
{
    Console.WriteLine("压缩已取消");
}
```

### 压缩为不同格式

```csharp
// ZIP 格式
var zipRequest = new CompressRequest(["data.txt"], "data.zip");
await zipRequest.CompressAsync();

// TAR 格式
var tarRequest = new CompressRequest(["data.txt"], "data.tar");
await tarRequest.CompressAsync();

// TAR.GZ 格式
var tarGzRequest = new CompressRequest(["data.txt"], "data.tar.gz");
await tarGzRequest.CompressAsync();

// BZ2 格式
var bz2Request = new CompressRequest(["data.txt"], "data.tar.bz2");
await bz2Request.CompressAsync();
```

### 综合示例

```csharp
var cts = new CancellationTokenSource();
var request = new CompressRequest(
    ["./data1.txt", "./data2.txt", "./folder1"],
    "backup.zip"
)
{
    IncludeSourceDiretory = true,
    Level = CompressionLevel.Normal,
    CancellationToken = cts.Token,
    OnProgress = args =>
    {
        Console.WriteLine($"[{args.ProgressText}] {args.CurrentName}");
    }
};

Console.WriteLine("开始压缩...");
await request.CompressAsync();
Console.WriteLine("压缩完成！");
```

## 特性

- 支持压缩单个文件或多个文件
- 支持压缩单个目录或多个目录
- 支持混合压缩文件和目录
- 支持多种压缩格式（ZIP、TAR、GZ、BZ2 等）
- 支持三种压缩级别（正常、最快、最小尺寸）
- 支持包含或不包含目录结构
- 支持密码加密
- 支持取消操作
- 支持进度回调
- 异步操作，不阻塞主线程
