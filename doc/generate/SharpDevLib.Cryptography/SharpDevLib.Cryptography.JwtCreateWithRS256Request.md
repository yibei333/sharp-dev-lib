###### [主页](./Index.md "主页")
## JwtCreateWithRS256Request 类
### 定义
**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")
**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [JwtCreateRequest](./SharpDevLib.Cryptography.JwtCreateRequest.md "JwtCreateRequest")
``` csharp
public class JwtCreateWithRS256Request : JwtCreateRequest
```
**注释**
*使用RSA SHA256算法创建JWT请求模型*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[JwtCreateWithRS256Request(Object payload, String pemKey, Byte[] keyPassword, RSASignaturePadding padding)](./SharpDevLib.Cryptography.JwtCreateWithRS256Request.ctor.Object.String.Byte.RSASignaturePadding.md "JwtCreateWithRS256Request(Object payload, String pemKey, Byte[] keyPassword, RSASignaturePadding padding)")|实例化请求模型|payload:载荷<br>pemKey:PEM格式的私钥<br>keyPassword:私钥密码,仅当私钥是受密码保护是适用<br>padding:RSA签名Padding|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Algorithm](./SharpDevLib.Cryptography.JwtCreateRequest.Algorithm.md "Algorithm")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[JwtCreateRequest](./SharpDevLib.Cryptography.JwtCreateRequest.md "JwtCreateRequest"))*|[JwtAlgorithm](./SharpDevLib.Cryptography.JwtAlgorithm.md "JwtAlgorithm")|`否`|算法|
|[Payload](./SharpDevLib.Cryptography.JwtCreateRequest.Payload.md "Payload")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[JwtCreateRequest](./SharpDevLib.Cryptography.JwtCreateRequest.md "JwtCreateRequest"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`否`|jwt载荷|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

