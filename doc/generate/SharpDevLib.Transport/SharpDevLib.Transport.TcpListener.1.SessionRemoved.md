###### [主页](./Index.md "主页")
# SessionRemoved 事件
## 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [TcpListener](./SharpDevLib.Transport.TcpListener.1.md "TcpListener")\<TSessionMetadata\>
``` csharp
public event EventHandler<TcpSessionEventArgs<TSessionMetadata>> SessionRemoved;
```
**注释**
*删除了会话回调事件*

**事件处理器类型** : [EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpSessionEventArgs](./SharpDevLib.Transport.TcpSessionEventArgs.1.md "TcpSessionEventArgs")\<TSessionMetadata\>\>
