###### [主页](./Index.md "主页")

## CompressOption 类

### 定义

**程序集** : [SharpDevLib.Compression.dll](./SharpDevLib.Compression.assembly.md "SharpDevLib.Compression.dll")

**命名空间** : [SharpDevLib.Compression](./SharpDevLib.Compression.namespace.md "SharpDevLib.Compression")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [CompressionOption](./SharpDevLib.Compression.CompressionOption.md "CompressionOption")

``` csharp
public class CompressOption : CompressionOption
```

**注释**

*压缩选项*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[CompressOption(List\<String\> sourcePaths, String targetPath)](./SharpDevLib.Compression.CompressOption.ctor.List.String.String.md "CompressOption(List<String> sourcePaths, String targetPath)")|实例化压缩选项|sourcePaths:路径集合,可以是目录也可以是文件路径<br>targetPath:保存目标路径|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[SourcePaths](./SharpDevLib.Compression.CompressOption.SourcePaths.md "SourcePaths")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|路径集合,可以是目录也可以是文件路径|
|[IncludeSourceDiretory](./SharpDevLib.Compression.CompressOption.IncludeSourceDiretory.md "IncludeSourceDiretory")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|如果SourcePath中的是目录,是否要包含目录的名称结构,默认为false|
|[Level](./SharpDevLib.Compression.CompressOption.Level.md "Level")|[CompressionLevel](./SharpDevLib.Compression.CompressionLevel.md "CompressionLevel")|`否`|压缩级别|
|[Format](./SharpDevLib.Compression.CompressOption.Format.md "Format")|[CompressionFormat](./SharpDevLib.Compression.CompressionFormat.md "CompressionFormat")|`否`|压缩文件格式|
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


