###### [主页](./Index.md "主页")

## FileExtension 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class FileExtension : Object
```

**注释**

*文件扩展*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[GetFileExtension(this String filePath, Boolean includePoint)](./SharpDevLib.FileExtension.GetFileExtension.thisString.Boolean.md "GetFileExtension(this String filePath, Boolean includePoint)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|filePath:文件路径,文件名也可以<br>includePoint:是否包含"."|
|[GetFileName(this String filePath, Boolean includeExtension)](./SharpDevLib.FileExtension.GetFileName.thisString.Boolean.md "GetFileName(this String filePath, Boolean includeExtension)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|filePath:文件路径<br>includeExtension:是否包含扩展名|
|[SaveToFile(this Byte[] bytes, String filePath, Boolean throwIfFileExist)](./SharpDevLib.FileExtension.SaveToFile.thisByte.String.Boolean.md "SaveToFile(this Byte[] bytes, String filePath, Boolean throwIfFileExist)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|bytes:字节数组<br>filePath:文件路径<br>throwIfFileExist:当文件已存在时是否抛出异常,true-抛出异常,false-覆盖|
|[SaveToFileAsync(this Byte[] bytes, String filePath, Nullable\<CancellationToken\> cancellationToken, Boolean throwIfFileExist)](./SharpDevLib.FileExtension.SaveToFileAsync.thisByte.String.Nullable.CancellationToken.Boolean.md "SaveToFileAsync(this Byte[] bytes, String filePath, Nullable<CancellationToken> cancellationToken, Boolean throwIfFileExist)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`是`|bytes:字节数组<br>filePath:文件路径<br>cancellationToken:cancellationToken<br>throwIfFileExist:当文件已存在时是否抛出异常,true-抛出异常,false-覆盖|
|[SaveToFile(this Stream stream, String filePath, Boolean throwIfFileExist)](./SharpDevLib.FileExtension.SaveToFile.thisStream.String.Boolean.md "SaveToFile(this Stream stream, String filePath, Boolean throwIfFileExist)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|stream:流<br>filePath:文件路径<br>throwIfFileExist:当文件已存在时是否抛出异常,true-抛出异常,false-覆盖|
|[SaveToFileAsync(this Stream stream, String filePath, Nullable\<CancellationToken\> cancellationToken, Boolean throwIfFileExist)](./SharpDevLib.FileExtension.SaveToFileAsync.thisStream.String.Nullable.CancellationToken.Boolean.md "SaveToFileAsync(this Stream stream, String filePath, Nullable<CancellationToken> cancellationToken, Boolean throwIfFileExist)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`是`|stream:流<br>filePath:文件路径<br>cancellationToken:cancellationToken<br>throwIfFileExist:当文件已存在时是否抛出异常,true-抛出异常,false-覆盖|
|[ThrowIfDirectoryNotExist(this String directory)](./SharpDevLib.FileExtension.ThrowIfDirectoryNotExist.thisString.md "ThrowIfDirectoryNotExist(this String directory)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|directory:文件夹路径|
|[ThrowIfDirectoryNotExist(this DirectoryInfo directory)](./SharpDevLib.FileExtension.ThrowIfDirectoryNotExist.thisDirectoryInfo.md "ThrowIfDirectoryNotExist(this DirectoryInfo directory)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|directory:文件夹路径|
|[ThrowIfFileNotExist(this String filePath)](./SharpDevLib.FileExtension.ThrowIfFileNotExist.thisString.md "ThrowIfFileNotExist(this String filePath)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|filePath:文件路径|
|[ThrowIfFileNotExist(this FileInfo fileInfo)](./SharpDevLib.FileExtension.ThrowIfFileNotExist.thisFileInfo.md "ThrowIfFileNotExist(this FileInfo fileInfo)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|fileInfo:文件信息|
|[CreateDirectoryIfNotExist(this String directory)](./SharpDevLib.FileExtension.CreateDirectoryIfNotExist.thisString.md "CreateDirectoryIfNotExist(this String directory)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|directory:文件夹路径|
|[CreateDirectoryIfNotExist(this DirectoryInfo directory)](./SharpDevLib.FileExtension.CreateDirectoryIfNotExist.thisDirectoryInfo.md "CreateDirectoryIfNotExist(this DirectoryInfo directory)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|directory:文件夹路径|
|[CreateFileIfNotExist(this String filePath)](./SharpDevLib.FileExtension.CreateFileIfNotExist.thisString.md "CreateFileIfNotExist(this String filePath)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|filePath:文件路径|
|[CreateFileIfNotExist(this FileInfo fileInfo)](./SharpDevLib.FileExtension.CreateFileIfNotExist.thisFileInfo.md "CreateFileIfNotExist(this FileInfo fileInfo)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|fileInfo:文件信息|
|[ToFileSizeString(this Int64 size)](./SharpDevLib.FileExtension.ToFileSizeString.thisInt64.md "ToFileSizeString(this Int64 size)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|size:文件字节长度|
|[GetMimeType(this String filePathOrName)](./SharpDevLib.FileExtension.GetMimeType.thisString.md "GetMimeType(this String filePathOrName)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|filePathOrName:文件名或路径|
|[CombinePath(this String leftPath, String rightPath)](./SharpDevLib.FileExtension.CombinePath.thisString.String.md "CombinePath(this String leftPath, String rightPath)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|leftPath:左边路径<br>rightPath:右边路径|
|[FormatPath(this String path)](./SharpDevLib.FileExtension.FormatPath.thisString.md "FormatPath(this String path)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|path:路径|
|[FileBase64Encode(this String filePath)](./SharpDevLib.FileExtension.FileBase64Encode.thisString.md "FileBase64Encode(this String filePath)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|filePath:文件路径|
|[FileBase64Decode(this String base64FileString)](./SharpDevLib.FileExtension.FileBase64Decode.thisString.md "FileBase64Decode(this String base64FileString)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`是`|base64FileString:base64格式字符串|
|[RemoveFileIfExist(this String path)](./SharpDevLib.FileExtension.RemoveFileIfExist.thisString.md "RemoveFileIfExist(this String path)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|path:文件路径|
|[OpenOrCreate(this FileInfo fileInfo)](./SharpDevLib.FileExtension.OpenOrCreate.thisFileInfo.md "OpenOrCreate(this FileInfo fileInfo)")|[FileStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream "FileStream")|`public`|`是`|fileInfo:文件信息|
|[CopyToAsync(this Stream source, Stream target, CancellationToken cancellationToken, Action\<Int64\> transfered)](./SharpDevLib.FileExtension.CopyToAsync.thisStream.Stream.CancellationToken.Action.Int64.md "CopyToAsync(this Stream source, Stream target, CancellationToken cancellationToken, Action<Int64> transfered)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`是`|source:原始流<br>target:目标流<br>cancellationToken:cancellationToken<br>transfered:传输回调(本次传输字节数)|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


