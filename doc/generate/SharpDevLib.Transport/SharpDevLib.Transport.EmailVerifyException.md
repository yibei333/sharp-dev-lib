###### [主页](./Index.md "主页")
## EmailVerifyException 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception")
**实现** : [ISerializable](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable "ISerializable")
``` csharp
public class EmailVerifyException : Exception, ISerializable
```
**注释**
*邮件验证异常*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[EmailVerifyException(String errorMessage)](./SharpDevLib.Transport.EmailVerifyException.ctor.String.md "EmailVerifyException(String errorMessage)")|实例化邮件验证异常|errorMessage:error message|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|TargetSite&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[MethodBase](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.methodbase "MethodBase")|`否`|-|
|Message&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|-|
|Data&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[IDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.idictionary "IDictionary")|`否`|-|
|InnerException&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception")|`否`|-|
|HelpLink&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|-|
|Source&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|-|
|HResult&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|-|
|StackTrace&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|-|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetBaseException()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception")|`public`|`否`|-|
|GetObjectData(SerializationInfo info, StreamingContext context)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

### 事件
|名称|事件处理类型|Accessor|注释|
|---|---|---|---|
|SerializeObjectState&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception"))*|[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1 "EventHandler")\<[SafeSerializationEventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.safeserializationeventargs "SafeSerializationEventArgs")\>|`protected`|-|

