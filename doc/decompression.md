# DeCompression - 解压缩

提供压缩文件的解压功能。
支持的格式有`.zip,.rar,.7z,.tar,.tgz,.gz,.xz,.bz2`

##### 简单示例

```csharp
// 解压 ZIP 文件
var request = new DeCompressRequest("data.zip", "./output");
await request.DeCompressAsync();
```

##### 完整示例

```csharp
var request = new DeCompressRequest("backup.zip", "./output")
{
    Password="foo",//rar,7z,zip支持密码
    OnProgress=(p)=>Console.WriteLine(p.ProgressText),
};
await request.DeCompressAsync();
```

## 相关文档
- [压缩](compression.md)
- [基础](../README.md#基础)