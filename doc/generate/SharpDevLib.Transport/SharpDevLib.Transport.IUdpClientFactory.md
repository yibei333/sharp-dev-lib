###### [主页](./Index.md "主页")

## IUdpClientFactory 接口

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**派生** : [UdpClientFactory](./SharpDevLib.Transport.UdpClientFactory.md "UdpClientFactory")

``` csharp
public interface IUdpClientFactory
```

**注释**

*Udp客户端创建工厂*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create(TransportAdapterType adapterType)](./SharpDevLib.Transport.IUdpClientFactory.Create.TransportAdapterType.md "Create(TransportAdapterType adapterType)")|[UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")|`public`|`否`|adapterType:收发适配器类型|
|[Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType)](./SharpDevLib.Transport.IUdpClientFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType)")|[UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")|`public`|`否`|localAdress:本地地址<br>localPort:本地端口<br>adapterType:收发适配器类型|


