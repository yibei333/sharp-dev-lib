###### [主页](./Index.md "主页")
## TcpSession\<TMetadata\> 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**实现** : [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable "IDisposable")
``` csharp
public class TcpSession<TMetadata> : Object, IDisposable
```
**注释**
*Tcp会话*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TMetadata|元数据|-|


### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Socket](./SharpDevLib.Transport.TcpSession.1.Socket.md "Socket")|[Socket](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket "Socket")|`否`|套接字|
|[Listener](./SharpDevLib.Transport.TcpSession.1.Listener.md "Listener")|[TcpListener](./SharpDevLib.Transport.TcpListener.1.md "TcpListener")\<TMetadata\>|`否`|所属监听器|
|[Metadata](./SharpDevLib.Transport.TcpSession.1.Metadata.md "Metadata")|TMetadata|`否`|元数据|
|[State](./SharpDevLib.Transport.TcpSession.1.State.md "State")|[TcpSessionStates](./SharpDevLib.Transport.TcpSessionStates.md "TcpSessionStates")|`否`|状态|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Send(Byte[] bytes, Boolean throwIfException)](./SharpDevLib.Transport.TcpSession.1.Send.Byte.Boolean.md "Send(Byte[] bytes, Boolean throwIfException)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|bytes:字节数组<br>throwIfException:发送失败是否抛出异常,默认false,可以订阅Error事件|
|[Close()](./SharpDevLib.Transport.TcpSession.1.Close.md "Close()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|[Dispose()](./SharpDevLib.Transport.TcpSession.1.Dispose.md "Dispose()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

### 事件
|名称|事件处理类型|Accessor|注释|
|---|---|---|---|
|[StateChanged](./SharpDevLib.Transport.TcpSession.1.StateChanged.md "StateChanged")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionStateChangedEventArgs](./SharpDevLib.Transport.TcpSessionStateChangedEventArgs.1.md "TcpSessionStateChangedEventArgs")\<TMetadata\>\>|`public`|状态变更回调事件|
|[Received](./SharpDevLib.Transport.TcpSession.1.Received.md "Received")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionDataEventArgs](./SharpDevLib.Transport.TcpSessionDataEventArgs.1.md "TcpSessionDataEventArgs")\<TMetadata\>\>|`public`|接收事件|
|[Sended](./SharpDevLib.Transport.TcpSession.1.Sended.md "Sended")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionDataEventArgs](./SharpDevLib.Transport.TcpSessionDataEventArgs.1.md "TcpSessionDataEventArgs")\<TMetadata\>\>|`public`|发送事件|
|[Error](./SharpDevLib.Transport.TcpSession.1.Error.md "Error")|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionExceptionEventArgs](./SharpDevLib.Transport.TcpSessionExceptionEventArgs.1.md "TcpSessionExceptionEventArgs")\<TMetadata\>\>|`public`|异常事件|

