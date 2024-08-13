###### [主页](./Index.md "主页")
## HttpResponse\<T\> 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")
``` csharp
public class HttpResponse<T> : HttpResponse
```
**注释**
*http响应*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|T|响应数据类型|-|


### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Data](./SharpDevLib.Transport.HttpResponse.1.Data.md "Data")|T|`否`|数据|
|[Url](./SharpDevLib.Transport.HttpResponse.Url.md "Url")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|请求地址|
|[IsSuccess](./SharpDevLib.Transport.HttpResponse.IsSuccess.md "IsSuccess")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|请求是否成功|
|[Code](./SharpDevLib.Transport.HttpResponse.Code.md "Code")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[HttpStatusCode](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode "HttpStatusCode")|`否`|http状态码|
|[Message](./SharpDevLib.Transport.HttpResponse.Message.md "Message")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|消息|
|[Headers](./SharpDevLib.Transport.HttpResponse.Headers.md "Headers")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>\>|`否`|响应头|
|[Cookies](./SharpDevLib.Transport.HttpResponse.Cookies.md "Cookies")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Cookie](https://learn.microsoft.com/en-us/dotnet/api/system.net.cookie "Cookie")\>|`否`|cookie集合|
|[RetryCount](./SharpDevLib.Transport.HttpResponse.RetryCount.md "RetryCount")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|重试次数|
|[ProcessCount](./SharpDevLib.Transport.HttpResponse.ProcessCount.md "ProcessCount")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|处理次数|
|[LastTimeConsuming](./SharpDevLib.Transport.HttpResponse.LastTimeConsuming.md "LastTimeConsuming")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan "TimeSpan")|`否`|最后一次耗时|
|[TotalTimeConsuming](./SharpDevLib.Transport.HttpResponse.TotalTimeConsuming.md "TotalTimeConsuming")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse"))*|[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan "TimeSpan")|`否`|总耗时|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ToString()](./SharpDevLib.Transport.HttpResponse.1.ToString.md "ToString()")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|[Failed(String url, HttpStatusCode code, String message)](./SharpDevLib.Transport.HttpResponse.1.Failed.String.HttpStatusCode.String.md "Failed(String url, HttpStatusCode code, String message)")|[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>|`public`|`是`|url:请求地址<br>code:http状态码<br>message:消息|
|[Succeed(String url, T data, String message)](./SharpDevLib.Transport.HttpResponse.1.Succeed.String.T.String.md "Succeed(String url, T data, String message)")|[HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>|`public`|`是`|url:请求地址<br>data:数据<br>message:消息|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

