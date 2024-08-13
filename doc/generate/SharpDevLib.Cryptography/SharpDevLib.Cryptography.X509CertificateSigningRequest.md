###### [主页](./Index.md "主页")

## X509CertificateSigningRequest 类

### 定义

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public class X509CertificateSigningRequest : Object
```

**注释**

*证书签名请求*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[X509CertificateSigningRequest(String subject, String privateKey)](./SharpDevLib.Cryptography.X509CertificateSigningRequest.ctor.String.String.md "X509CertificateSigningRequest(String subject, String privateKey)")|实例化证书签名请求|subject:subject<br>privateKey:私钥,当前仅支持RSA|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Subject](./SharpDevLib.Cryptography.X509CertificateSigningRequest.Subject.md "Subject")|[X500DistinguishedName](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x500distinguishedname "X500DistinguishedName")|`否`|subject|
|[PublicKey](./SharpDevLib.Cryptography.X509CertificateSigningRequest.PublicKey.md "PublicKey")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`否`|公钥|
|[Signature](./SharpDevLib.Cryptography.X509CertificateSigningRequest.Signature.md "Signature")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`否`|签名|
|[CertificationRequestInfo](./SharpDevLib.Cryptography.X509CertificateSigningRequest.CertificationRequestInfo.md "CertificationRequestInfo")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`否`|证书请求信息|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Import(String pemText)](./SharpDevLib.Cryptography.X509CertificateSigningRequest.Import.String.md "Import(String pemText)")|[X509CertificateSigningRequest](./SharpDevLib.Cryptography.X509CertificateSigningRequest.md "X509CertificateSigningRequest")|`public`|`是`|pemText:pem格式的请求|
|[Export()](./SharpDevLib.Cryptography.X509CertificateSigningRequest.Export.md "Export()")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|[GenerateCert(String issuerPrivateKey, X500DistinguishedName issuer, Byte[] serialNumber, Int32 days, List\<X509Extension\> extensions, String friendlyName)](./SharpDevLib.Cryptography.X509CertificateSigningRequest.GenerateCert.String.X500DistinguishedName.Byte.Int32.List.X509Extension.String.md "GenerateCert(String issuerPrivateKey, X500DistinguishedName issuer, Byte[] serialNumber, Int32 days, List<X509Extension> extensions, String friendlyName)")|[X509Certificate2](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2 "X509Certificate2")|`public`|`否`|issuerPrivateKey:颁发者私钥<br>issuer:颁发者<br>serialNumber:序列号<br>days:过期天数<br>extensions:扩展集合<br>friendlyName:友好名称|
|[GenerateSelfSignedCert(String privateKey, Byte[] serialNumber, Int32 days, List\<X509Extension\> extensions, String friendlyName)](./SharpDevLib.Cryptography.X509CertificateSigningRequest.GenerateSelfSignedCert.String.Byte.Int32.List.X509Extension.String.md "GenerateSelfSignedCert(String privateKey, Byte[] serialNumber, Int32 days, List<X509Extension> extensions, String friendlyName)")|[X509Certificate2](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2 "X509Certificate2")|`public`|`否`|privateKey:私钥<br>serialNumber:序列号<br>days:过期天数<br>extensions:扩展集合<br>friendlyName:友好名称|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


