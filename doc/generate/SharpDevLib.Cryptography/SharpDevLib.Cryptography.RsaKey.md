###### [主页](./Index.md "主页")

## RsaKey 类

### 定义

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class RsaKey : Object
```

**注释**

*RSA密钥对扩展*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[WrapLineWith64Char(this String keyBody)](./SharpDevLib.Cryptography.RsaKey.WrapLineWith64Char.thisString.md "WrapLineWith64Char(this String keyBody)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|keyBody:不带头尾的key|
|[RemoveWrapLineAndTrim(this String keyBody)](./SharpDevLib.Cryptography.RsaKey.RemoveWrapLineAndTrim.thisString.md "RemoveWrapLineAndTrim(this String keyBody)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|keyBody:不带头尾的key|
|[ImportPem(this RSA rsa, String pem, Byte[] password)](./SharpDevLib.Cryptography.RsaKey.ImportPem.thisRSA.String.Byte.md "ImportPem(this RSA rsa, String pem, Byte[] password)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|rsa:rsa algorithm<br>pem:Pem格式的密钥,支持的格式如下:<br>(1)PKCS#1私钥<br>(2)受密码保护的PKCS#1私钥<br>(3)PKCS#1公钥<br>(4)PKCS#8私钥<br>(5)受密码保护的PKCS#8私钥<br>(6)X.509SubjectPublicKey<br>password:密码（仅当PEM格式为受密码保护的私钥时适用）|
|[ExportPem(this RSA rsa, PemType pemType, Byte[] password, String encryptPkcs1PrivateKeyAlogorithm)](./SharpDevLib.Cryptography.RsaKey.ExportPem.thisRSA.PemType.Byte.String.md "ExportPem(this RSA rsa, PemType pemType, Byte[] password, String encryptPkcs1PrivateKeyAlogorithm)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|rsa:rsa algorithm<br>pemType:PEM格式类型<br>password:密码(仅当PEM格式为受密码保护的私钥时适用)<br>encryptPkcs1PrivateKeyAlogorithm:加密算法(仅当PEM格式为受密码保护的PKCS#1私钥时适用),受支持的算法如下:<br>(1)AES-256-CBC<br>(2)DES-EDE3-CBC|
|[IsKeyPairMatch(String privatePem, String publicPem)](./SharpDevLib.Cryptography.RsaKey.IsKeyPairMatch.String.String.md "IsKeyPairMatch(String privatePem, String publicPem)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|privatePem:PEM格式的私钥<br>publicPem:PEM格式的公钥|
|[GetKeyInfo(String key, Byte[] password)](./SharpDevLib.Cryptography.RsaKey.GetKeyInfo.String.Byte.md "GetKeyInfo(String key, Byte[] password)")|[RsaKeyInfo](./SharpDevLib.Cryptography.RsaKeyInfo.md "RsaKeyInfo")|`public`|`是`|key:PEM格式的密钥<br>password:密码（仅当PEM格式为受密码保护的私钥时适用）|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


