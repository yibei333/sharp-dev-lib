###### [主页](./Index.md "主页")
## HttpMultiPartFormDataRequest 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest") ↣ [HttpRequest](./SharpDevLib.Transport.HttpRequest.1.md "HttpRequest")\<[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>\>
``` csharp
public class HttpMultiPartFormDataRequest : HttpRequest<Dictionary<String, String>>
```
**注释**
*multipart/form-data表单请求*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[HttpMultiPartFormDataRequest(String url)](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.ctor.String.md "HttpMultiPartFormDataRequest(String url)")|实例化表单请求|url:请求地址|
|[HttpMultiPartFormDataRequest(String url, Dictionary\<String, String\> parameters)](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.ctor.String.Dictionary.String.String.md "HttpMultiPartFormDataRequest(String url, Dictionary<String, String> parameters)")|实例化表单请求|url:请求地址<br>parameters:请求表单参数|
|[HttpMultiPartFormDataRequest(String url, HttpFormFile[] files)](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.ctor.String.HttpFormFile.md "HttpMultiPartFormDataRequest(String url, HttpFormFile[] files)")|实例化表单请求|url:请求地址<br>files:文件集合|
|[HttpMultiPartFormDataRequest(String url, Dictionary\<String, String\> parameters, HttpFormFile[] files)](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.ctor.String.Dictionary.String.String.HttpFormFile.md "HttpMultiPartFormDataRequest(String url, Dictionary<String, String> parameters, HttpFormFile[] files)")|实例化表单请求|url:请求地址<br>parameters:请求表单参数<br>files:文件集合|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Files](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.Files.md "Files")|[HttpFormFile\[\]](https://learn.microsoft.com/en-us/dotnet/api/sharpdevlib.transport.httpformfile[] "HttpFormFile\[\]")|`否`|文件集合|
|[Parameters](./SharpDevLib.Transport.HttpRequest.1.Parameters.md "Parameters")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.1.md "HttpRequest")\<[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>\>)*|[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|请求参数|
|[TimeOut](./SharpDevLib.Transport.HttpRequest.TimeOut.md "TimeOut")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan "TimeSpan")\>|`否`|超时时间|
|[RetryCount](./SharpDevLib.Transport.HttpRequest.RetryCount.md "RetryCount")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>|`否`|重试次数|
|[Url](./SharpDevLib.Transport.HttpRequest.Url.md "Url")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|请求地址|
|[Headers](./SharpDevLib.Transport.HttpRequest.Headers.md "Headers")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 "Dictionary")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [String\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.string[] "String\[\]")\>|`否`|请求头|
|[Cookies](./SharpDevLib.Transport.HttpRequest.Cookies.md "Cookies")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[Cookie](https://learn.microsoft.com/en-us/dotnet/api/system.net.cookie "Cookie")\>|`否`|请求Cookie|
|[OnReceiveProgress](./SharpDevLib.Transport.HttpRequest.OnReceiveProgress.md "OnReceiveProgress")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Action](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 "Action")\<[HttpProgress](./SharpDevLib.Transport.HttpProgress.md "HttpProgress")\>|`否`|接收数据回调|
|[OnSendProgress](./SharpDevLib.Transport.HttpRequest.OnSendProgress.md "OnSendProgress")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Action](https://learn.microsoft.com/en-us/dotnet/api/system.action-1 "Action")\<[HttpProgress](./SharpDevLib.Transport.HttpProgress.md "HttpProgress")\>|`否`|传入数据回调|
|[UseEdgeUserAgent](./SharpDevLib.Transport.HttpRequest.UseEdgeUserAgent.md "UseEdgeUserAgent")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[HttpRequest](./SharpDevLib.Transport.HttpRequest.md "HttpRequest"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|使用edge的ua(默认为true)|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ToString()](./SharpDevLib.Transport.HttpMultiPartFormDataRequest.ToString.md "ToString()")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

