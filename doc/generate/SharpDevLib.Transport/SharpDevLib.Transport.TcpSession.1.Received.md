###### [主页](./Index.md "主页")
# Received 事件
## 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [TcpSession](./SharpDevLib.Transport.TcpSession.1.md "TcpSession")\<TMetadata\>
``` csharp
public event EventHandler<TcpSessionDataEventArgs<TMetadata>> Received;
```
**注释**
*接收事件*

**事件处理器类型** : [EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionDataEventArgs](./SharpDevLib.Transport.TcpSessionDataEventArgs.1.md "TcpSessionDataEventArgs")\<TMetadata\>\>
