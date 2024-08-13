###### [主页](./Index.md "主页")
#### Create(IPAddress address, Int32 port, TransportAdapterType adapterType) 方法
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [ITcpListenerFactory](./SharpDevLib.Transport.ITcpListenerFactory.md "ITcpListenerFactory")
``` csharp
public virtual abstract TcpListener Create(IPAddress address, Int32 port, TransportAdapterType adapterType)
```
**注释**
*创建Tcp监听器*

**返回类型** : [TcpListener](./SharpDevLib.Transport.TcpListener.md "TcpListener")

**参数**
|名称|类型|注释|
|---|---|---|
|address|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|地址|
|port|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|端口|
|adapterType|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|接收数据适配器类型|

