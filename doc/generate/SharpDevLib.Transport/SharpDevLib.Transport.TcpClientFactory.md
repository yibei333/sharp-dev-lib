###### [主页](./Index.md "主页")
## TcpClientFactory 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**实现** : [ITcpClientFactory](./SharpDevLib.Transport.ITcpClientFactory.md "ITcpClientFactory")
``` csharp
public class TcpClientFactory : Object, ITcpClientFactory
```
**注释**
*Tcp客户端创建工厂*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[TcpClientFactory(IServiceProvider serviceProvider)](./SharpDevLib.Transport.TcpClientFactory.ctor.IServiceProvider.md "TcpClientFactory(IServiceProvider serviceProvider)")|实例化Tcp客户端创建工厂|serviceProvider:serviceProvider(用于获取Logger)|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create(IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)](./SharpDevLib.Transport.TcpClientFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)")|[TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")|`public`|`否`|remoteAdress:远程地址<br>remotePort:远程端口<br>adapterType:收发适配器类型|
|[Create(IPAddress localAdress, Int32 localPort, IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)](./SharpDevLib.Transport.TcpClientFactory.Create.IPAddress.Int32.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress localAdress, Int32 localPort, IPAddress remoteAdress, Int32 remotePort, TransportAdapterType adapterType)")|[TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")|`public`|`否`|localAdress:本地地址<br>localPort:本地端口<br>remoteAdress:远程地址<br>remotePort:远程端口<br>adapterType:收发适配器类型|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

