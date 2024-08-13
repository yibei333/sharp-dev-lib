###### [主页](./Index.md "主页")
## DeCompressOption 类
### 定义
**程序集** : [SharpDevLib.Compression.dll](./SharpDevLib.Compression.assembly.md "SharpDevLib.Compression.dll")
**命名空间** : [SharpDevLib.Compression](./SharpDevLib.Compression.namespace.md "SharpDevLib.Compression")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [CompressionOption](./SharpDevLib.Compression.CompressionOption.md "CompressionOption")
``` csharp
public class DeCompressOption : CompressionOption
```
**注释**
*解压选项*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[DeCompressOption(String sourceFile, String targetPath)](./SharpDevLib.Compression.DeCompressOption.ctor.String.String.md "DeCompressOption(String sourceFile, String targetPath)")|实例化解压选项|sourceFile:压缩文件路径<br>targetPath:要解压到的目标路径|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[SourceFile](./SharpDevLib.Compression.DeCompressOption.SourceFile.md "SourceFile")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|压缩文件路径|
|[Format](./SharpDevLib.Compression.DeCompressOption.Format.md "Format")|[CompressionFormat](./SharpDevLib.Compression.CompressionFormat.md "CompressionFormat")|`否`|解压文件格式|
|[TargetPath](./SharpDevLib.Compression.CompressionOption.TargetPath.md "TargetPath")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[CompressionOption](./SharpDevLib.Compression.CompressionOption.md "CompressionOption"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|保存目标路径|
|[Password](./SharpDevLib.Compression.CompressionOption.Password.md "Password")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[CompressionOption](./SharpDevLib.Compression.CompressionOption.md "CompressionOption"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|密码|
|[OnProgress](./SharpDevLib.Compression.CompressionOption.OnProgress.md "OnProgress")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[CompressionOption](./SharpDevLib.Compression.CompressionOption.md "CompressionOption"))*|[Action](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 "Action")\<[CompressionProgressArgs](./SharpDevLib.Compression.CompressionProgressArgs.md "CompressionProgressArgs")\>|`否`|进度变化回调|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

