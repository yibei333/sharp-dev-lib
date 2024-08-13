###### [主页](./Index.md "主页")
#### GetAsync(HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken) 方法
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [IHttpService](./SharpDevLib.Transport.IHttpService.md "IHttpService")
``` csharp
public virtual abstract Task<HttpResponse> GetAsync(HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken)
```
**注释**
*get请求*

**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>

**参数**
|名称|类型|注释|
|---|---|---|
|request|[HttpKeyValueRequest](./SharpDevLib.Transport.HttpKeyValueRequest.md "HttpKeyValueRequest")|请求|
|cancellationToken|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")\>|cancllation token|

