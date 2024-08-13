###### [主页](./Index.md "主页")

## EmailOptions 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public class EmailOptions : Object
```

**注释**

*邮件配置*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[EmailOptions()](./SharpDevLib.Transport.EmailOptions.ctor.md "EmailOptions()")|默认构造函数|-|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Host](./SharpDevLib.Transport.EmailOptions.Host.md "Host")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|主机(一般为smtp.xx.com)|
|[Port](./SharpDevLib.Transport.EmailOptions.Port.md "Port")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|端口(一般为25,587,465)|
|[UseSSL](./SharpDevLib.Transport.EmailOptions.UseSSL.md "UseSSL")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否使用ssl|
|[Sender](./SharpDevLib.Transport.EmailOptions.Sender.md "Sender")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|发件人地址|
|[SenderPassword](./SharpDevLib.Transport.EmailOptions.SenderPassword.md "SenderPassword")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|发件人密码(有些邮箱为单独的授权码)|
|[SenderDisplayName](./SharpDevLib.Transport.EmailOptions.SenderDisplayName.md "SenderDisplayName")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|发件人显示名称|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


