###### [主页](./Index.md "主页")
## Jwt 类
### 定义
**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")
**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class Jwt : Object
```
**注释**
*jwt扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Create(this JwtCreateWithHMACSHA256Request request)](./SharpDevLib.Cryptography.Jwt.Create.thisJwtCreateWithHMACSHA256Request.md "Create(this JwtCreateWithHMACSHA256Request request)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|request:使用HMACSHA256算法创建JWT请求模型|
|[Create(this JwtCreateWithRS256Request request)](./SharpDevLib.Cryptography.Jwt.Create.thisJwtCreateWithRS256Request.md "Create(this JwtCreateWithRS256Request request)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|request:使用RSA SHA256算法创建JWT请求模型|
|[Verify(this JwtVerifyWithHMACSHA256Request request)](./SharpDevLib.Cryptography.Jwt.Verify.thisJwtVerifyWithHMACSHA256Request.md "Verify(this JwtVerifyWithHMACSHA256Request request)")|[JwtVerifyResult](./SharpDevLib.Cryptography.JwtVerifyResult.md "JwtVerifyResult")|`public`|`是`|request:使用HMACSHA256算法验证JWT请求模型|
|[Verify(this JwtVerifyWithRS256Request request)](./SharpDevLib.Cryptography.Jwt.Verify.thisJwtVerifyWithRS256Request.md "Verify(this JwtVerifyWithRS256Request request)")|[JwtVerifyResult](./SharpDevLib.Cryptography.JwtVerifyResult.md "JwtVerifyResult")|`public`|`是`|request:使用RSA SHA256算法验证JWT请求模型|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

