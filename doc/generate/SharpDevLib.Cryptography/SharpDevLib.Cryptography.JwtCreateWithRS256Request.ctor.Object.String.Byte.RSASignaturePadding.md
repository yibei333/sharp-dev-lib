###### [主页](./Index.md "主页")

#### JwtCreateWithRS256Request(Object payload, String pemKey, Byte[] keyPassword, RSASignaturePadding padding) 构造函数

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [JwtCreateWithRS256Request](./SharpDevLib.Cryptography.JwtCreateWithRS256Request.md "JwtCreateWithRS256Request")

``` csharp
public JwtCreateWithRS256Request(Object payload, String pemKey, Byte[] keyPassword, RSASignaturePadding padding)
```
**注释**

*实例化请求模型*


**参数**

|名称|类型|注释|
|---|---|---|
|payload|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|载荷|
|pemKey|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|PEM格式的私钥|
|keyPassword|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|私钥密码,仅当私钥是受密码保护是适用|
|padding|[RSASignaturePadding](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsasignaturepadding "RSASignaturePadding")|RSA签名Padding|


