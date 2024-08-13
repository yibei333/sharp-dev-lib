###### [主页](./Index.md "主页")
## DeCompressFormatNotSupportedException 类
### 定义
**程序集** : [SharpDevLib.Compression.dll](./SharpDevLib.Compression.assembly.md "SharpDevLib.Compression.dll")
**命名空间** : [SharpDevLib.Compression](./SharpDevLib.Compression.namespace.md "SharpDevLib.Compression")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [Exception](https://learn.microsoft.com/en-us/dotnet/api/system.exception "Exception") ↣ [SystemException](https://learn.microsoft.com/en-us/dotnet/api/system.systemexception "SystemException") ↣ [NotSupportedException](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception "NotSupportedException")
**实现** : [ISerializable](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable "ISerializable")
``` csharp
public class DeCompressFormatNotSupportedException : NotSupportedException, ISerializable
```
**注释**
*解压不支持的格式异常*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[DeCompressFormatNotSupportedException(String extension)](./SharpDevLib.Compression.DeCompressFormatNotSupportedException.ctor.String.md "DeCompressFormatNotSupportedException(String extension)")|实例化异常|extension:文件扩展名|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[SupportedFormats](./SharpDevLib.Compression.DeCompressFormatNotSupportedException.SupportedFormats.md "SupportedFormats")|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|支持的格式|
|[Extension](./SharpDevLib.Compression.DeCompressFormatNotSupportedException.Extension.md "Extension")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|文件扩展名|
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

