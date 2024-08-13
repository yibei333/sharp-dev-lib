###### [主页](./Index.md "主页")
#### Create\<TSessionMetadata\>(IPAddress address, Int32 port, Func\<TSessionMetadata\> initSessionMetadata, TransportAdapterType adapterType) 方法
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [TcpListenerFactory](./SharpDevLib.Transport.TcpListenerFactory.md "TcpListenerFactory")
``` csharp
public virtual TcpListener<TSessionMetadata> Create<TSessionMetadata>(IPAddress address, Int32 port, Func<TSessionMetadata> initSessionMetadata, TransportAdapterType adapterType)
```
**注释**
*创建Tcp监听器*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TSessionMetadata|会话元数据类型(可以用来绑定会话的身份信息)|-|


**返回类型** : [TcpListener](./SharpDevLib.Transport.TcpListener.1.md "TcpListener")\<TSessionMetadata\>

**参数**
|名称|类型|注释|
|---|---|---|
|address|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|地址|
|port|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|端口|
|initSessionMetadata|[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-1 "Func")\<TSessionMetadata\>|初始化会话元数据|
|adapterType|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|接收数据适配器类型|

