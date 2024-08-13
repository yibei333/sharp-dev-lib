###### [主页](./Index.md "主页")

## UdpClientExceptionEventArgs 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [UdpClientEventArgs](./SharpDevLib.Transport.UdpClientEventArgs.md "UdpClientEventArgs")

``` csharp
public class UdpClientExceptionEventArgs : UdpClientEventArgs
```

**注释**

*Udp客户端异常事件参数*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[UdpClientExceptionEventArgs(UdpClient client, Exception exception)](./SharpDevLib.Transport.UdpClientExceptionEventArgs.ctor.UdpClient.Exception.md "UdpClientExceptionEventArgs(UdpClient client, Exception exception)")|实例化Udp客户端异常事件参数|client:客户端<br>exception:异常|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Exception](./SharpDevLib.Transport.UdpClientExceptionEventArgs.Exception.md "Exception")|[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception")|`否`|异常|
|[Client](./SharpDevLib.Transport.UdpClientEventArgs.Client.md "Client")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[UdpClientEventArgs](./SharpDevLib.Transport.UdpClientEventArgs.md "UdpClientEventArgs"))*|[UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")|`否`|客户端|
|[RemoteEndPoint](./SharpDevLib.Transport.UdpClientEventArgs.RemoteEndPoint.md "RemoteEndPoint")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[UdpClientEventArgs](./SharpDevLib.Transport.UdpClientEventArgs.md "UdpClientEventArgs"))*|[EndPoint](https://learn.microsoft.com/en-us/dotnet/api/system.net.endpoint "EndPoint")|`否`|远程终结点|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


