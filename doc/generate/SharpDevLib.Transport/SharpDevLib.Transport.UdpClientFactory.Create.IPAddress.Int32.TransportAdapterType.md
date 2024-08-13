###### [主页](./Index.md "主页")
#### Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType) 方法
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [UdpClientFactory](./SharpDevLib.Transport.UdpClientFactory.md "UdpClientFactory")
``` csharp
public virtual UdpClient Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType)
```
**注释**
*创建Udp客户端*

**返回类型** : [UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")

**参数**
|名称|类型|注释|
|---|---|---|
|localAdress|[IPAddress](https://learn.microsoft.com/en-us/dotnet/api/system.net.ipaddress "IPAddress")|本地地址|
|localPort|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|本地端口|
|adapterType|[TransportAdapterType](./SharpDevLib.Transport.TransportAdapterType.md "TransportAdapterType")|收发适配器类型|

