###### [主页](./Index.md "主页")
## ITcpClientFactory 接口
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**派生** : [TcpClientFactory](./SharpDevLib.Transport.TcpClientFactory.md "TcpClientFactory")
``` csharp
public interface ITcpClientFactory
```
**注释**
*Tcp客户端创建工厂*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create(IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)](./SharpDevLib.Transport.ITcpClientFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)")|[TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")|`public`|`否`|remoteAdress:远程地址<br>remotePort:远程端口<br>adapterType:收发适配器类型|
|[Create(IPAddress localAdress, Int32 localPort, IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)](./SharpDevLib.Transport.ITcpClientFactory.Create.IPAddress.Int32.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress localAdress, Int32 localPort, IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)")|[TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")|`public`|`否`|localAdress:本地地址<br>localPort:本地端口<br>remoteAdress:远程地址<br>remotePort:远程端口<br>adapterType:收发适配器类型|

