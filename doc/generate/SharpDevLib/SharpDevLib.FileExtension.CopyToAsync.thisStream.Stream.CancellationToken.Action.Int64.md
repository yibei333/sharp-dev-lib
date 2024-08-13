###### [主页](./Index.md "主页")

#### CopyToAsync(this Stream source, Stream target, CancellationToken cancellationToken, Action\<Int64\> transfered) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [FileExtension](./SharpDevLib.FileExtension.md "FileExtension")

``` csharp
[System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(SharpDevLib.FileExtension+<CopyToAsync>d__28))]
[System.Diagnostics.DebuggerStepThroughAttribute()]
public static Task CopyToAsync(this Stream source, Stream target, CancellationToken cancellationToken, Action<Int64> transfered)
```

**注释**

*拷贝流*



**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")


**参数**

|名称|类型|注释|
|---|---|---|
|source|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|原始流|
|target|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|目标流|
|cancellationToken|[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")|cancellationToken|
|transfered|[Action](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 "Action")\<[Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64 "Int64")\>|传输回调(本次传输字节数)|


