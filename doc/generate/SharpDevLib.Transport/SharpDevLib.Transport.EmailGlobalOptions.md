###### [主页](./Index.md "主页")
## EmailGlobalOptions 类
### 定义
**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")
**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class EmailGlobalOptions : Object
```
**注释**
*全局邮件配置*

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Host](./SharpDevLib.Transport.EmailGlobalOptions.Host.md "Host")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|主机(一般为smtp.xx.com)|
|[Port](./SharpDevLib.Transport.EmailGlobalOptions.Port.md "Port")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`是`|端口(一般为25,587,465)|
|[UseSSL](./SharpDevLib.Transport.EmailGlobalOptions.UseSSL.md "UseSSL")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`是`|是否使用ssl|
|[Sender](./SharpDevLib.Transport.EmailGlobalOptions.Sender.md "Sender")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|发件人地址|
|[SenderPassword](./SharpDevLib.Transport.EmailGlobalOptions.SenderPassword.md "SenderPassword")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|发件人密码(有些邮箱为单独的授权码)|
|[SenderDisplayName](./SharpDevLib.Transport.EmailGlobalOptions.SenderDisplayName.md "SenderDisplayName")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|发件人显示名称|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

