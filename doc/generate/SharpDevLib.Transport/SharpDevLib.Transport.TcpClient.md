###### [主页](./Index.md "主页")

## TcpClient 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**实现** : [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable "IDisposable")

``` csharp
public class TcpClient : Object, IDisposable
```

**注释**

*Tcp客户端*


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Socket](./SharpDevLib.Transport.TcpClient.Socket.md "Socket")|[Socket](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket "Socket")|`否`|套接字|
|[State](./SharpDevLib.Transport.TcpClient.State.md "State")|[TcpClientStates](./SharpDevLib.Transport.TcpClientStates.md "TcpClientStates")|`否`|状态|
|[ServiceProvider](./SharpDevLib.Transport.TcpClient.ServiceProvider.md "ServiceProvider")|[IServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider "IServiceProvider")|`否`|ServiceProvider|
|[LocalAdress](./SharpDevLib.Transport.TcpClient.LocalAdress.md "LocalAdress")|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|`否`|本地地址|
|[LocalPort](./SharpDevLib.Transport.TcpClient.LocalPort.md "LocalPort")|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>|`否`|本地端口|
|[RemoteAdress](./SharpDevLib.Transport.TcpClient.RemoteAdress.md "RemoteAdress")|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|`否`|远程地址|
|[RemotePort](./SharpDevLib.Transport.TcpClient.RemotePort.md "RemotePort")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|远程端口|
|[AdapterType](./SharpDevLib.Transport.TcpClient.AdapterType.md "AdapterType")|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|`否`|收发适配器类型|
|[ReceiveAdapter](./SharpDevLib.Transport.TcpClient.ReceiveAdapter.md "ReceiveAdapter")|[ITransportReceiveAdapter](./SharpDevLib.Transport.ITransportReceiveAdapter.md "ITransportReceiveAdapter")|`否`|接收数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)|
|[SendAdapter](./SharpDevLib.Transport.TcpClient.SendAdapter.md "SendAdapter")|[ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")|`否`|发送数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ConnectAndReceiveAsync(Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.TcpClient.ConnectAndReceiveAsync.Nullable.CancellationToken.md "ConnectAndReceiveAsync(Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`否`|cancellationToken:cancellationToken|
|[Send(Byte[] bytes, Boolean throwIfException)](./SharpDevLib.Transport.TcpClient.Send.Byte.Boolean.md "Send(Byte[] bytes, Boolean throwIfException)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|bytes:字节数组<br>throwIfException:发送失败是否抛出异常,默认false,可以订阅Error事件|
|[Close()](./SharpDevLib.Transport.TcpClient.Close.md "Close()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|[Dispose()](./SharpDevLib.Transport.TcpClient.Dispose.md "Dispose()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


### 事件

|名称|事件处理类型|Accessor|注释|
|---|---|---|---|
|[StateChanged](./SharpDevLib.Transport.TcpClient.StateChanged.md "StateChanged")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpClientStateChangedEventArgs](./SharpDevLib.Transport.TcpClientStateChangedEventArgs.md "TcpClientStateChangedEventArgs")\>|`public`|状态变更回调事件|
|[Received](./SharpDevLib.Transport.TcpClient.Received.md "Received")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpClientDataEventArgs](./SharpDevLib.Transport.TcpClientDataEventArgs.md "TcpClientDataEventArgs")\>|`public`|接收事件|
|[Sended](./SharpDevLib.Transport.TcpClient.Sended.md "Sended")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpClientDataEventArgs](./SharpDevLib.Transport.TcpClientDataEventArgs.md "TcpClientDataEventArgs")\>|`public`|发送事件|
|[Error](./SharpDevLib.Transport.TcpClient.Error.md "Error")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpClientExceptionEventArgs](./SharpDevLib.Transport.TcpClientExceptionEventArgs.md "TcpClientExceptionEventArgs")\>|`public`|异常事件|


