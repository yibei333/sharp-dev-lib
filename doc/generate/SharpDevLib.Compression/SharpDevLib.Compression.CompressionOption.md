###### [主页](./Index.md "主页")
## CompressionOption 类
### 定义
**程序集** : [SharpDevLib.Compression.dll](./SharpDevLib.Compression.assembly.md "SharpDevLib.Compression.dll")
**命名空间** : [SharpDevLib.Compression](./SharpDevLib.Compression.namespace.md "SharpDevLib.Compression")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**派生** : [CompressOption](./SharpDevLib.Compression.CompressOption.md "CompressOption"), [DeCompressOption](./SharpDevLib.Compression.DeCompressOption.md "DeCompressOption")
``` csharp
public abstract class CompressionOption : Object
```
**注释**
*压缩/解压选项*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[CompressionOption(String targetPath)](./SharpDevLib.Compression.CompressionOption.ctor.String.md "CompressionOption(String targetPath)")|实例化压缩/解压选项|targetPath:目标路径|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[TargetPath](./SharpDevLib.Compression.CompressionOption.TargetPath.md "TargetPath")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|保存目标路径|
|[Password](./SharpDevLib.Compression.CompressionOption.Password.md "Password")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|密码|
|[OnProgress](./SharpDevLib.Compression.CompressionOption.OnProgress.md "OnProgress")|[Action](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 "Action")\<[CompressionProgressArgs](./SharpDevLib.Compression.CompressionProgressArgs.md "CompressionProgressArgs")\>|`否`|进度变化回调|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

