###### [主页](./Index.md "主页")
#### SaveToFileAsync(this Stream stream, String filePath, Nullable\<CancellationToken\> cancellationToken, Boolean throwIfFileExist) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [FileExtension](./SharpDevLib.FileExtension.md "FileExtension")
``` csharp
[System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(SharpDevLib.FileExtension+<SaveToFileAsync>d__11))]
[System.Diagnostics.DebuggerStepThroughAttribute()]
public static Task SaveToFileAsync(this Stream stream, String filePath, Nullable<CancellationToken> cancellationToken, Boolean throwIfFileExist)
```
**注释**
*将流保存到文件中*

**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")

**参数**
|名称|类型|注释|
|---|---|---|
|stream|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|流|
|filePath|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|文件路径|
|cancellationToken|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")\>|cancellationToken|
|throwIfFileExist|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|当文件已存在时是否抛出异常,true-抛出异常,false-覆盖|

