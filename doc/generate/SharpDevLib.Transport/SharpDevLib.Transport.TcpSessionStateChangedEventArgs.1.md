###### [主页](./Index.md "主页")

## TcpSessionStateChangedEventArgs\<TSessionMetadata\> 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [TcpSessionEventArgs](./SharpDevLib.Transport.TcpSessionEventArgs.1.md "TcpSessionEventArgs")\<TSessionMetadata\>

``` csharp
public class TcpSessionStateChangedEventArgs<TSessionMetadata> : TcpSessionEventArgs<TSessionMetadata>
```

**注释**

*Tcp会话状态变更事件参数*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TSessionMetadata|会话元数据类型|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[TcpSessionStateChangedEventArgs(TcpSession\<TSessionMetadata\> session, TcpSessionStates before, TcpSessionStates current)](./SharpDevLib.Transport.TcpSessionStateChangedEventArgs.1.ctor.TcpSessionStateChangedEventArgs.TcpSession.TSessionMetadata.TcpSessionStates.TcpSessionStates.md "TcpSessionStateChangedEventArgs(TcpSession<TSessionMetadata> session, TcpSessionStates before, TcpSessionStates current)")|实例化Tcp会话状态变更事件参数|session:会话<br>before:之前状态<br>current:当前状态|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Before](./SharpDevLib.Transport.TcpSessionStateChangedEventArgs.1.Before.md "Before")|[TcpSessionStates](./SharpDevLib.Transport.TcpSessionStates.md "TcpSessionStates")|`否`|之前状态|
|[Current](./SharpDevLib.Transport.TcpSessionStateChangedEventArgs.1.Current.md "Current")|[TcpSessionStates](./SharpDevLib.Transport.TcpSessionStates.md "TcpSessionStates")|`否`|当前状态|
|[Session](./SharpDevLib.Transport.TcpSessionEventArgs.1.Session.md "Session")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[TcpSessionEventArgs](./SharpDevLib.Transport.TcpSessionEventArgs.1.md "TcpSessionEventArgs")\<TSessionMetadata\>)*|[TcpSession](./SharpDevLib.Transport.TcpSession.1.md "TcpSession")\<TSessionMetadata\>|`否`|会话|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


