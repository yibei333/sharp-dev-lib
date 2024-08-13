###### [主页](./Index.md "主页")

## TcpSessionEventArgs\<TSessionMetadata\> 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**派生** : [TcpSessionDataEventArgs](./SharpDevLib.Transport.TcpSessionDataEventArgs.1.md "TcpSessionDataEventArgs")\<TSessionMetadata\>, [TcpSessionExceptionEventArgs](./SharpDevLib.Transport.TcpSessionExceptionEventArgs.1.md "TcpSessionExceptionEventArgs")\<TSessionMetadata\>, [TcpSessionStateChangedEventArgs](./SharpDevLib.Transport.TcpSessionStateChangedEventArgs.1.md "TcpSessionStateChangedEventArgs")\<TSessionMetadata\>

``` csharp
public class TcpSessionEventArgs<TSessionMetadata> : Object
```

**注释**

*Tcp会话事件参数*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TSessionMetadata|会话元数据类型|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[TcpSessionEventArgs(TcpSession\<TSessionMetadata\> session)](./SharpDevLib.Transport.TcpSessionEventArgs.1.ctor.TcpSessionEventArgs.TcpSession.TSessionMetadata.md "TcpSessionEventArgs(TcpSession<TSessionMetadata> session)")|实例化Tcp会话事件|session:-|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Session](./SharpDevLib.Transport.TcpSessionEventArgs.1.Session.md "Session")|[TcpSession](./SharpDevLib.Transport.TcpSession.1.md "TcpSession")\<TSessionMetadata\>|`否`|会话|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


