###### [主页](./Index.md "主页")
## ITransportSendAdapter 接口
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**派生** : [TransportDefaultSendAdapter](./SharpDevLib.Transport.TransportDefaultSendAdapter.md "TransportDefaultSendAdapter"), [TransportFixedHeaderSendAdapter](./SharpDevLib.Transport.TransportFixedHeaderSendAdapter.md "TransportFixedHeaderSendAdapter")
``` csharp
public interface ITransportSendAdapter
```
**注释**
*传输发送适配器*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Send(Socket socket, Byte[] bytes)](./SharpDevLib.Transport.ITransportSendAdapter.Send.Socket.Byte.md "Send(Socket socket, Byte[] bytes)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|socket:套接字<br>bytes:字节数组|
|[SendTo(Socket socket, IPAddress remoteAddress, Int32 remoteAddressPort, Byte[] bytes)](./SharpDevLib.Transport.ITransportSendAdapter.SendTo.Socket.IPAddress.Int32.Byte.md "SendTo(Socket socket, IPAddress remoteAddress, Int32 remoteAddressPort, Byte[] bytes)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|socket:套接字<br>remoteAddress:远程地址<br>remoteAddressPort:远程端口<br>bytes:字节数组|

