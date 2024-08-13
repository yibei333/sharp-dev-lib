###### [主页](./Index.md "主页")

## ITransportReceiveAdapter 接口

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**派生** : [TransportDefaultReceiveAdapter](./SharpDevLib.Transport.TransportDefaultReceiveAdapter.md "TransportDefaultReceiveAdapter"), [TransportFixedHeaderReceiveAdapter](./SharpDevLib.Transport.TransportFixedHeaderReceiveAdapter.md "TransportFixedHeaderReceiveAdapter")

``` csharp
public interface ITransportReceiveAdapter
```

**注释**

*传输接收适配器*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Receive(Socket socket)](./SharpDevLib.Transport.ITransportReceiveAdapter.Receive.Socket.md "Receive(Socket socket)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`否`|socket:套接字|
|[ReceiveFrom(Socket socket, EndPoint& remoteEndPoint)](./SharpDevLib.Transport.ITransportReceiveAdapter.ReceiveFrom.Socket.EndPoint&.md "ReceiveFrom(Socket socket, EndPoint& remoteEndPoint)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`否`|socket:-<br>remoteEndPoint:-|


