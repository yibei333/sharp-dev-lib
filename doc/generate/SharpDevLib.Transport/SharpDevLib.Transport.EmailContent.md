###### [主页](./Index.md "主页")

## EmailContent 类

### 定义

**程序集** : [SharpDevLib.Transport.dll](./SharpDevLib.Transport.assembly.md "SharpDevLib.Transport.dll")

**命名空间** : [SharpDevLib.Transport](./SharpDevLib.Transport.namespace.md "SharpDevLib.Transport")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public class EmailContent : Object
```

**注释**

*邮箱内容*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[EmailContent(IEnumerable\<String\> receivers, String subject, String body)](./SharpDevLib.Transport.EmailContent.ctor.IEnumerable.String.String.String.md "EmailContent(IEnumerable<String> receivers, String subject, String body)")|实例化邮箱内容|receivers:收件人<br>subject:标题<br>body:主体|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Receivers](./SharpDevLib.Transport.EmailContent.Receivers.md "Receivers")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|收件人|
|[CC](./SharpDevLib.Transport.EmailContent.CC.md "CC")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|抄送人|
|[BCC](./SharpDevLib.Transport.EmailContent.BCC.md "BCC")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|密送人|
|[Repliers](./SharpDevLib.Transport.EmailContent.Repliers.md "Repliers")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|回复人|
|[Subject](./SharpDevLib.Transport.EmailContent.Subject.md "Subject")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|标题|
|[Body](./SharpDevLib.Transport.EmailContent.Body.md "Body")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|主体|
|[Attachments](./SharpDevLib.Transport.EmailContent.Attachments.md "Attachments")|[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 "IEnumerable")\<[EmailAttachment](./SharpDevLib.Transport.EmailAttachment.md "EmailAttachment")\>|`否`|附件|
|[Priority](./SharpDevLib.Transport.EmailContent.Priority.md "Priority")|[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 "Nullable")\<[MailPriority](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailpriority "MailPriority")\>|`否`|优先级|
|[BodyEncoding](./SharpDevLib.Transport.EmailContent.BodyEncoding.md "BodyEncoding")|[Encoding](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding "Encoding")|`否`|主体编码|
|[HeaderEncoding](./SharpDevLib.Transport.EmailContent.HeaderEncoding.md "HeaderEncoding")|[Encoding](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding "Encoding")|`否`|头部编码|
|[IsHtml](./SharpDevLib.Transport.EmailContent.IsHtml.md "IsHtml")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否是Html内容|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


