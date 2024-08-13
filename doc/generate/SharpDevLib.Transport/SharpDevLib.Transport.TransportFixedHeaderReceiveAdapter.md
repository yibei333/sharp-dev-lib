###### [主页](./Index.md "主页")

## TransportFixedHeaderReceiveAdapter 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**实现** : [ITransportReceiveAdapter](./SharpDevLib.Transport.ITransportReceiveAdapter.md "ITransportReceiveAdapter")

``` csharp
public class TransportFixedHeaderReceiveAdapter : Object, ITransportReceiveAdapter
```

**注释**

*传输固定头接收适配器(每次接收前四个字节作为数据长度,没有粘包问题)*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[TransportFixedHeaderReceiveAdapter()](./SharpDevLib.Transport.TransportFixedHeaderReceiveAdapter.ctor.md "TransportFixedHeaderReceiveAdapter()")|默认构造函数|-|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Receive(Socket socket)](./SharpDevLib.Transport.TransportFixedHeaderReceiveAdapter.Receive.Socket.md "Receive(Socket socket)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`否`|socket:套接字|
|[ReceiveFrom(Socket socket, EndPoint& remoteEndPoint)](./SharpDevLib.Transport.TransportFixedHeaderReceiveAdapter.ReceiveFrom.Socket.EndPoint&.md "ReceiveFrom(Socket socket, EndPoint& remoteEndPoint)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`否`|socket:-<br>remoteEndPoint:-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


