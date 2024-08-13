###### [主页](./Index.md "主页")

## TcpClientStateChangedEventArgs 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [TcpClientEventArgs](./SharpDevLib.Transport.TcpClientEventArgs.md "TcpClientEventArgs")

``` csharp
public class TcpClientStateChangedEventArgs : TcpClientEventArgs
```

**注释**

*Tcp客户端状态变更事件参数*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current)](./SharpDevLib.Transport.TcpClientStateChangedEventArgs.ctor.TcpClient.TcpClientStates.TcpClientStates.md "TcpClientStateChangedEventArgs(TcpClient client, TcpClientStates before, TcpClientStates current)")|实例化Tcp客户端状态变更事件参数|client:客户端<br>before:之前状态<br>current:当前状态|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Before](./SharpDevLib.Transport.TcpClientStateChangedEventArgs.Before.md "Before")|[TcpClientStates](./SharpDevLib.Transport.TcpClientStates.md "TcpClientStates")|`否`|之前状态|
|[Current](./SharpDevLib.Transport.TcpClientStateChangedEventArgs.Current.md "Current")|[TcpClientStates](./SharpDevLib.Transport.TcpClientStates.md "TcpClientStates")|`否`|当前状态|
|[Client](./SharpDevLib.Transport.TcpClientEventArgs.Client.md "Client")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[TcpClientEventArgs](./SharpDevLib.Transport.TcpClientEventArgs.md "TcpClientEventArgs"))*|[TcpClient](./SharpDevLib.Transport.TcpClient.md "TcpClient")|`否`|客户端|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


