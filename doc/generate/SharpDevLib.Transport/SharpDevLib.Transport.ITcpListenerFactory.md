###### [主页](./Index.md "主页")

## ITcpListenerFactory 接口

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**派生** : [TcpListenerFactory](./SharpDevLib.Transport.TcpListenerFactory.md "TcpListenerFactory")

``` csharp
public interface ITcpListenerFactory
```

**注释**

*Tcp监听器创建工厂*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create\<TSessionMetadata\>(IPAddress address, Int32 port, Func\<TSessionMetadata\> initSessionMetadata, TransportAdapterType adapterType)](./SharpDevLib.Transport.ITcpListenerFactory.Create.TSessionMetadata.IPAddress.Int32.Func.TSessionMetadata.TransportAdapterType.md "Create<TSessionMetadata>(IPAddress address, Int32 port, Func<TSessionMetadata> initSessionMetadata, TransportAdapterType adapterType)")|[TcpListener](./SharpDevLib.Transport.TcpListener.1.md "TcpListener")\<TSessionMetadata\>|`public`|`否`|address:地址<br>port:端口<br>initSessionMetadata:初始化会话元数据<br>adapterType:接收数据适配器类型|
|[Create(IPAddress address, Int32 port, TransportAdapterType adapterType)](./SharpDevLib.Transport.ITcpListenerFactory.Create.IPAddress.Int32.TransportAdapterType.md "Create(IPAddress address, Int32 port, TransportAdapterType adapterType)")|[TcpListener](./SharpDevLib.Transport.TcpListener.md "TcpListener")|`public`|`否`|address:地址<br>port:端口<br>adapterType:接收数据适配器类型|


