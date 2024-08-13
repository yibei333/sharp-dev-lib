###### [主页](./Index.md "主页")

## TransportDefaultSendAdapter 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**实现** : [ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")

``` csharp
public class TransportDefaultSendAdapter : Object, ITransportSendAdapter
```

**注释**

*传输默认发送适配器*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[TransportDefaultSendAdapter()](./SharpDevLib.Transport.TransportDefaultSendAdapter.ctor.md "TransportDefaultSendAdapter()")|默认构造函数|-|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Send(Socket socket, Byte[] bytes)](./SharpDevLib.Transport.TransportDefaultSendAdapter.Send.Socket.Byte.md "Send(Socket socket, Byte[] bytes)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|socket:套接字<br>bytes:字节数组|
|[SendTo(Socket socket, IPAddress remoteAddress, Int32 remoteAddressPort, Byte[] bytes)](./SharpDevLib.Transport.TransportDefaultSendAdapter.SendTo.Socket.IPAddress.Int32.Byte.md "SendTo(Socket socket, IPAddress remoteAddress, Int32 remoteAddressPort, Byte[] bytes)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|socket:套接字<br>remoteAddress:远程地址<br>remoteAddressPort:远程端口<br>bytes:字节数组|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


