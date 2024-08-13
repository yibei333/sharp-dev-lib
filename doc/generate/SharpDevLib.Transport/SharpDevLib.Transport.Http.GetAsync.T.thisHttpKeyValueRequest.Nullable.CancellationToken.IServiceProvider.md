###### [主页](./Index.md "主页")

#### GetAsync\<T\>(this HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken, IServiceProvider serviceProvider) 方法

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**所属类型** : [Http](./SharpDevLib.Transport.Http.md "Http")

``` csharp
[System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(SharpDevLib.Transport.Http+<GetAsync>d__2`1))]
[System.Diagnostics.DebuggerStepThroughAttribute()]
public static Task<HttpResponse<T>> GetAsync<T>(this HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken, IServiceProvider serviceProvider)
```

**注释**

*http get请求*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|返回类型,如果T为byte[]且数据较大建议用GetStreamAsync方法|-|




**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>


**参数**

|名称|类型|注释|
|---|---|---|
|request|[HttpKeyValueRequest](./SharpDevLib.Transport.HttpKeyValueRequest.md "HttpKeyValueRequest")|请求|
|cancellationToken|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken "CancellationToken")\>|cancellationToken|
|serviceProvider|[IServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider "IServiceProvider")|serviceProvider(获取ILogger和全局配置用)|


