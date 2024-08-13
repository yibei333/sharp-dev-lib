###### [主页](./Index.md "主页")

#### SendAsync(EmailContent content, Nullable\<CancellationToken\> cancellationToken) 方法

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**所属类型** : [IEmailService](./SharpDevLib.Transport.IEmailService.md "IEmailService")

``` csharp
public virtual abstract Task SendAsync(EmailContent content, Nullable<CancellationToken> cancellationToken)
```

**注释**

*发送邮件*



**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")


**参数**

|名称|类型|注释|
|---|---|---|
|content|[EmailContent](./SharpDevLib.Transport.EmailContent.md "EmailContent")|内容|
|cancellationToken|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")\>|cancellationToken|


