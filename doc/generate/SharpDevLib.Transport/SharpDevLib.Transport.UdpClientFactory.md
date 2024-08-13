###### [主页](./Index.md "主页")

## UdpClientFactory 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**实现** : [IUdpClientFactory](./SharpDevLib.Transport.IUdpClientFactory.md "IUdpClientFactory")

``` csharp
public class UdpClientFactory : Object, IUdpClientFactory
```

**注释**

*Udp客户端创建工厂*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[UdpClientFactory(IServiceProvider serviceProvider)](./SharpDevLib.Transport.UdpClientFactory.ctor.IServiceProvider.md "UdpClientFactory(IServiceProvider serviceProvider)")|实例化Udp客户端创建工厂|serviceProvider:serviceProvider(用于获取Logger)|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create(TransportAdapterType adapterType)](./SharpDevLib.Transport.UdpClientFactory.Create.TransportAdapterType.md "Create(TransportAdapterType adapterType)")|[UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")|`public`|`否`|adapterType:收发适配器类型|
|[Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType)](./SharpDevLib.Transport.UdpClientFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress localAdress, Int32 localPort, TransportAdapterType adapterType)")|[UdpClient](./SharpDevLib.Transport.UdpClient.md "UdpClient")|`public`|`否`|localAdress:本地地址<br>localPort:本地端口<br>adapterType:收发适配器类型|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


