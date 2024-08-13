###### [主页](./Index.md "主页")

## IHttpService 接口

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

``` csharp
public interface IHttpService
```

**注释**

*http服务抽象*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[GetAsync\<T\>(HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.GetAsync.T.HttpKeyValueRequest.Nullable.CancellationToken.md "GetAsync<T>(HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[GetAsync(HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.GetAsync.HttpKeyValueRequest.Nullable.CancellationToken.md "GetAsync(HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[GetStreamAsync(HttpKeyValueRequest request)](./SharpDevLib.Transport.IHttpService.GetStreamAsync.HttpKeyValueRequest.md "GetStreamAsync(HttpKeyValueRequest request)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")\>|`public`|`否`|request:请求|
|[PostAsync\<T\>(HttpJsonRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.T.HttpJsonRequest.Nullable.CancellationToken.md "PostAsync<T>(HttpJsonRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PostAsync(HttpJsonRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.HttpJsonRequest.Nullable.CancellationToken.md "PostAsync(HttpJsonRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PostAsync\<T\>(HttpMultiPartFormDataRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.T.HttpMultiPartFormDataRequest.Nullable.CancellationToken.md "PostAsync<T>(HttpMultiPartFormDataRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PostAsync(HttpMultiPartFormDataRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.HttpMultiPartFormDataRequest.Nullable.CancellationToken.md "PostAsync(HttpMultiPartFormDataRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PostAsync\<T\>(HttpUrlEncodedFormRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.T.HttpUrlEncodedFormRequest.Nullable.CancellationToken.md "PostAsync<T>(HttpUrlEncodedFormRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PostAsync(HttpUrlEncodedFormRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PostAsync.HttpUrlEncodedFormRequest.Nullable.CancellationToken.md "PostAsync(HttpUrlEncodedFormRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PutAsync\<T\>(HttpJsonRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PutAsync.T.HttpJsonRequest.Nullable.CancellationToken.md "PutAsync<T>(HttpJsonRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[PutAsync(HttpJsonRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.PutAsync.HttpJsonRequest.Nullable.CancellationToken.md "PutAsync(HttpJsonRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[DeleteAsync\<T\>(HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.DeleteAsync.T.HttpKeyValueRequest.Nullable.CancellationToken.md "DeleteAsync<T>(HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|
|[DeleteAsync(HttpKeyValueRequest request, Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.IHttpService.DeleteAsync.HttpKeyValueRequest.Nullable.CancellationToken.md "DeleteAsync(HttpKeyValueRequest request, Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")\>|`public`|`否`|request:请求<br>cancellationToken:cancllation token|


