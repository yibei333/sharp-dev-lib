###### [主页](./Index.md "主页")

## TransportSendAdapters 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class TransportSendAdapters : Object
```

**注释**

*内置的传输发送适配器*


### 字段

|名称|类型|是否静态|注释|
|---|---|---|---|
|[FixedHeader](./SharpDevLib.Transport.TransportSendAdapters.FixedHeader.md "FixedHeader")|[ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")|`是`|传输固定头发送适配器(每次发送消息在前四个字节中放入字节的长度总和,没有粘包问题)|
|[Default](./SharpDevLib.Transport.TransportSendAdapters.Default.md "Default")|[ITransportSendAdapter](./SharpDevLib.Transport.ITransportSendAdapter.md "ITransportSendAdapter")|`是`|传输默认发送适配器|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


