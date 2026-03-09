# Compression - 压缩

提供文件和目录的压缩功能。
支持的格式有`.zip,.tar,.tgz,.gz,.bz2`

##### 简单示例

```csharp
"Hello,World".Utf8Decode().SaveToFile("data.txt");
var request = new CompressRequest(["data.txt"], "data.zip");
await request.CompressAsync();
```

##### 完整示例

```csharp
"Hello,World".Utf8Decode().SaveToFile("data1.txt");
"./data2.txt".CreateFileIfNotExist();
"./folder1".CreateDirectoryIfNotExist();
"./folder1/data3.txt".CreateFileIfNotExist();

var cts = new CancellationTokenSource();
//cts.CancelAfter(TimeSpan.FromSeconds(5));
var request = new CompressRequest
(
    ["./data1.txt", "./data2.txt", "./folder1"],//可以是文件，也可以是文件夹
    "backup.zip"//.zip,.tar,.tgz,.gz,.bz2
)
{
    Password = "foo",//添加密码保护,只支持zip格式,其余格式将引发异常
    IncludeSourceDiretory = true,//是否保持文件夹的结果，如果为false，将自动将该文件夹中的所有文件变为平级
    Level = CompressionLevel.Normal,
    CancellationToken = cts.Token,
    OnProgress = args => Console.WriteLine($"[{args.ProgressText}] {args.CurrentName}")
    //[100%] ./data1.txt
};

await request.CompressAsync();
```

## 相关文档
- [解压缩](decompression.md)
- [基础](../README.md#基础)
