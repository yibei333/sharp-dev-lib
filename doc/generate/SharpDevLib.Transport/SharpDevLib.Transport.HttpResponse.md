###### [主页](./Index.md "主页")
## HttpResponse 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**派生** : [HttpResponse](./SharpDevLib.Transport.HttpResponse.1.md "HttpResponse")\<T\>
``` csharp
public class HttpResponse : Object
```
**注释**
*http响应*

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Url](./SharpDevLib.Transport.HttpResponse.Url.md "Url")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|请求地址|
|[IsSuccess](./SharpDevLib.Transport.HttpResponse.IsSuccess.md "IsSuccess")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|请求是否成功|
|[Code](./SharpDevLib.Transport.HttpResponse.Code.md "Code")|[HttpStatusCode](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode "HttpStatusCode")|`否`|http状态码|
|[Message](./SharpDevLib.Transport.HttpResponse.Message.md "Message")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|消息|
|[Headers](./SharpDevLib.Transport.HttpResponse.Headers.md "Headers")|[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>\>|`否`|响应头|
|[Cookies](./SharpDevLib.Transport.HttpResponse.Cookies.md "Cookies")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Cookie](https://learn.microsoft.com/en-us/dotnet/api/system.net.cookie "Cookie")\>|`否`|cookie集合|
|[RetryCount](./SharpDevLib.Transport.HttpResponse.RetryCount.md "RetryCount")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|重试次数|
|[ProcessCount](./SharpDevLib.Transport.HttpResponse.ProcessCount.md "ProcessCount")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|处理次数|
|[LastTimeConsuming](./SharpDevLib.Transport.HttpResponse.LastTimeConsuming.md "LastTimeConsuming")|[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan "TimeSpan")|`否`|最后一次耗时|
|[TotalTimeConsuming](./SharpDevLib.Transport.HttpResponse.TotalTimeConsuming.md "TotalTimeConsuming")|[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan "TimeSpan")|`否`|总耗时|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Failed(String url, HttpStatusCode code, String message)](./SharpDevLib.Transport.HttpResponse.Failed.String.HttpStatusCode.String.md "Failed(String url, HttpStatusCode code, String message)")|[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")|`public`|`是`|url:请求地址<br>code:http状态码<br>message:消息|
|[Succeed(String url, String message)](./SharpDevLib.Transport.HttpResponse.Succeed.String.String.md "Succeed(String url, String message)")|[HttpResponse](./SharpDevLib.Transport.HttpResponse.md "HttpResponse")|`public`|`是`|url:请求地址<br>message:消息|
|[ToString()](./SharpDevLib.Transport.HttpResponse.ToString.md "ToString()")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

