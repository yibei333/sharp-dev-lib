###### [主页](./Index.md "主页")

#### SendAsync(this EmailOptions options, EmailContent content, Nullable\<CancellationToken\> cancellationToken) 方法

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**所属类型** : [Email](./SharpDevLib.Transport.Email.md "Email")

``` csharp
[System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(SharpDevLib.Transport.Email+<SendAsync>d__2))]
[System.Diagnostics.DebuggerStepThroughAttribute()]
public static Task SendAsync(this EmailOptions options, EmailContent content, Nullable<CancellationToken> cancellationToken)
```

**注释**

*发送邮件*



**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")


**参数**

|名称|类型|注释|
|---|---|---|
|options|[EmailOptions](./SharpDevLib.Transport.EmailOptions.md "EmailOptions")|配置|
|content|[EmailContent](./SharpDevLib.Transport.EmailContent.md "EmailContent")|内容|
|cancellationToken|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")\>|cancellationToken|


