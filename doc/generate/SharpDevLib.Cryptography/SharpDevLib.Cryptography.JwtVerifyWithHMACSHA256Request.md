###### [主页](./Index.md "主页")

## JwtVerifyWithHMACSHA256Request 类

### 定义

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [JwtVerifyRequest](./SharpDevLib.Cryptography.JwtVerifyRequest.md "JwtVerifyRequest")

``` csharp
public class JwtVerifyWithHMACSHA256Request : JwtVerifyRequest
```

**注释**

*使用HMACSHA256算法验证JWT请求模型*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[JwtVerifyWithHMACSHA256Request(String token, Byte[] secret)](./SharpDevLib.Cryptography.JwtVerifyWithHMACSHA256Request.ctor.String.Byte.md "JwtVerifyWithHMACSHA256Request(String token, Byte[] secret)")|实例化请求模型|token:token<br>secret:密钥|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Algorithm](./SharpDevLib.Cryptography.JwtVerifyRequest.Algorithm.md "Algorithm")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[JwtVerifyRequest](./SharpDevLib.Cryptography.JwtVerifyRequest.md "JwtVerifyRequest"))*|[JwtAlgorithm](./SharpDevLib.Cryptography.JwtAlgorithm.md "JwtAlgorithm")|`否`|算法|
|[Token](./SharpDevLib.Cryptography.JwtVerifyRequest.Token.md "Token")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[JwtVerifyRequest](./SharpDevLib.Cryptography.JwtVerifyRequest.md "JwtVerifyRequest"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|token|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


