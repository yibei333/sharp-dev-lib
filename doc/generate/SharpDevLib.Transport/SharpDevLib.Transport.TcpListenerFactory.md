###### [主页](./Index.md "主页")
## TcpListenerFactory 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**实现** : [ITcpListenerFactory](./SharpDevLib.Transport.ITcpListenerFactory.md "ITcpListenerFactory")
``` csharp
public class TcpListenerFactory : Object, ITcpListenerFactory
```
**注释**
*Tcp监听器创建工厂*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[TcpListenerFactory(IServiceProvider serviceProvider)](./SharpDevLib.Transport.TcpListenerFactory.ctor.IServiceProvider.md "TcpListenerFactory(IServiceProvider serviceProvider)")|实例化Tcp监听器创建工厂|serviceProvider:serviceProvider(用于获取Logger)|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create\<TSessionMetadata\>(IPAddress address, Int32 port, Func\<TSessionMetadata\> initSessionMetadata, TransportAdapterType adapterType)](./SharpDevLib.Transport.TcpListenerFactory.Create.TSessionMetadata.IPAddress.Int32.Func.TSessionMetadata.TransportAdapterType.md "Create<TSessionMetadata>(IPAddress address, Int32 port, Func<TSessionMetadata> initSessionMetadata, TransportAdapterType adapterType)")|[TcpListener](./SharpDevLib.Transport.TcpListener.1.md "TcpListener")\<TSessionMetadata\>|`public`|`否`|address:地址<br>port:端口<br>initSessionMetadata:初始化会话元数据<br>adapterType:接收数据适配器类型|
|[Create(IPAddress address, Int32 port, TransportAdapterType adapterType)](./SharpDevLib.Transport.TcpListenerFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress address, Int32 port, TransportAdapterType adapterType)")|[TcpListener](./SharpDevLib.Transport.TcpListener.md "TcpListener")|`public`|`否`|address:地址<br>port:端口<br>adapterType:接收数据适配器类型|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

