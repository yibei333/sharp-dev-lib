###### [主页](./Index.md "主页")
## TcpListener\<TSessionMetadata\> 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**实现** : [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable "IDisposable")
**派生** : [TcpListener](./SharpDevLib.Transport.TcpListener.md "TcpListener")
``` csharp
public class TcpListener<TSessionMetadata> : Object, IDisposable
```
**注释**
*Tcp监听器*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TSessionMetadata|会话元数据类型(可以用来绑定会话的身份信息)|-|


### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Socket](./SharpDevLib.Transport.TcpListener.1.Socket.md "Socket")|[Socket](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket "Socket")|`否`|套接字|
|[AdapterType](./SharpDevLib.Transport.TcpListener.1.AdapterType.md "AdapterType")|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|`否`|接收数据适配器类型|
|[ReceiveAdapter](./SharpDevLib.Transport.TcpListener.1.ReceiveAdapter.md "ReceiveAdapter")|[ITransportReceiveAdapter](./SharpDevLib.Transport.ITransportReceiveAdapter.md "ITransportReceiveAdapter")|`否`|接收数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)|
|[SendAdapter](./SharpDevLib.Transport.TcpListener.1.SendAdapter.md "SendAdapter")|[ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")|`否`|发送数据适配器(仅当AdapterType=TcpAdapterType.Custom时有用)|
|[State](./SharpDevLib.Transport.TcpListener.1.State.md "State")|[TcpListnerStates](./SharpDevLib.Transport.TcpListnerStates.md "TcpListnerStates")|`否`|状态|
|[IPAddress](./SharpDevLib.Transport.TcpListener.1.IPAddress.md "IPAddress")|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|`否`|地址|
|[Port](./SharpDevLib.Transport.TcpListener.1.Port.md "Port")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|端口|
|[InitSessionMetadata](./SharpDevLib.Transport.TcpListener.1.InitSessionMetadata.md "InitSessionMetadata")|[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 "Func")\<TSessionMetadata\>|`否`|初始化会话元数据|
|[ServiceProvider](./SharpDevLib.Transport.TcpListener.1.ServiceProvider.md "ServiceProvider")|[IServiceProvider](https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider "IServiceProvider")|`否`|ServiceProvider|
|[Sessions](./SharpDevLib.Transport.TcpListener.1.Sessions.md "Sessions")|[IReadOnlyCollection](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1 "IReadOnlyCollection")\<[TcpSession](./SharpDevLib.Transport.TcpSession.1.md "TcpSession")\<TSessionMetadata\>\>|`否`|会话集合|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[ListenAsync(Nullable\<CancellationToken\> cancellationToken)](./SharpDevLib.Transport.TcpListener.1.ListenAsync.Nullable.CancellationToken.md "ListenAsync(Nullable<CancellationToken> cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task "Task")|`public`|`否`|cancellationToken:cancellationToken|
|[Close()](./SharpDevLib.Transport.TcpListener.1.Close.md "Close()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|[Dispose()](./SharpDevLib.Transport.TcpListener.1.Dispose.md "Dispose()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

### 事件
|名称|事件处理类型|Accessor|注释|
|---|---|---|---|
|[StateChanged](./SharpDevLib.Transport.TcpListener.1.StateChanged.md "StateChanged")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpListenerStateChangedEventArgs](./SharpDevLib.Transport.TcpListenerStateChangedEventArgs.md "TcpListenerStateChangedEventArgs")\>|`public`|状态变更回调事件|
|[SessionAdded](./SharpDevLib.Transport.TcpListener.1.SessionAdded.md "SessionAdded")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionEventArgs](./SharpDevLib.Transport.TcpSessionEventArgs.1.md "TcpSessionEventArgs")\<TSessionMetadata\>\>|`public`|添加了会话回调事件|
|[SessionRemoved](./SharpDevLib.Transport.TcpListener.1.SessionRemoved.md "SessionRemoved")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionEventArgs](./SharpDevLib.Transport.TcpSessionEventArgs.1.md "TcpSessionEventArgs")\<TSessionMetadata\>\>|`public`|删除了会话回调事件|
