###### [主页](./Index.md "主页")
# StateChanged 事件
## 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**所属类型** : [TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")
``` csharp
public event EventHandler<TcpClientStateChangedEventArgs> StateChanged;
```
**注释**
*状态变更回调事件*

**事件处理器类型** : [EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[TcpClientStateChangedEventArgs](./SharpDevLib.Transport.TcpClientStateChangedEventArgs.md "TcpClientStateChangedEventArgs")\>