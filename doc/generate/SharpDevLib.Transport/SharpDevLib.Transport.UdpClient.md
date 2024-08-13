###### [主页](./Index.md "主页")
## UdpClient 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**实现** : [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable "IDisposable")
``` csharp
public class UdpClient : Object, IDisposable
```
**注释**
*Udp客户端*

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Socket](./SharpDevLib.Transport.UdpClient.Socket.md "Socket")|[Socket](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket "Socket")|`否`|套接字|
|[ServiceProvider](./SharpDevLib.Transport.UdpClient.ServiceProvider.md "ServiceProvider")|[IServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider "IServiceProvider")|`否`|ServiceProvider|
|[LocalAdress](./SharpDevLib.Transport.UdpClient.LocalAdress.md "LocalAdress")|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|`否`|本地地址|
|[LocalPort](./SharpDevLib.Transport.UdpClient.LocalPort.md "LocalPort")|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>|`否`|本地端口|
|[AdapterType](./SharpDevLib.Transport.UdpClient.AdapterType.md "AdapterType")|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|`否`|收发适配器类型|
|[ReceiveAdapter](./SharpDevLib.Transport.UdpClient.ReceiveAdapter.md "ReceiveAdapter")|[ITransportReceiveAdapter](./SharpDevLib.Transport.ITransportReceiveAdapter.md "ITransportReceiveAdapter")|`否`|接收数据适配器(仅当AdapterType=UdpAdapterType.Custom时有用)|
|[SendAdapter](./SharpDevLib.Transport.UdpClient.SendAdapter.md "SendAdapter")|[ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")|`否`|发送数据适配器(仅当AdapterType=UdpAdapterType.Custom时有用)|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ReceiveAsync(Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.UdpClient.ReceiveAsync.Nullable.CancellationToken.md "ReceiveAsync(Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`否`|cancellationToken:cancellationToken|
|[Send(IPAddress remoteAdress, Int32 remotePort, Byte[] bytes, Boolean throwIfException)](./SharpDevLib.Transport.UdpClient.Send.IPAddress.Int32.Byte.Boolean.md "Send(IPAddress remoteAdress, Int32 remotePort, Byte[] bytes, Boolean throwIfException)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|remoteAdress:远程地址<br>remotePort:远程端口<br>bytes:字节数组<br>throwIfException:发送失败是否抛出异常,默认false,可以订阅Error事件|
|[Dispose()](./SharpDevLib.Transport.UdpClient.Dispose.md "Dispose()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

### 事件
|名称|事件处理类型|Accessor|注释|
|---|---|---|---|
|[Received](./SharpDevLib.Transport.UdpClient.Received.md "Received")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[UdpClientDataEventArgs](./SharpDevLib.Transport.UdpClientDataEventArgs.md "UdpClientDataEventArgs")\>|`public`|接收事件|
|[Sended](./SharpDevLib.Transport.UdpClient.Sended.md "Sended")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[UdpClientDataEventArgs](./SharpDevLib.Transport.UdpClientDataEventArgs.md "UdpClientDataEventArgs")\>|`public`|发送事件|
|[Error](./SharpDevLib.Transport.UdpClient.Error.md "Error")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[UdpClientExceptionEventArgs](./SharpDevLib.Transport.UdpClientExceptionEventArgs.md "UdpClientExceptionEventArgs")\>|`public`|异常事件|

