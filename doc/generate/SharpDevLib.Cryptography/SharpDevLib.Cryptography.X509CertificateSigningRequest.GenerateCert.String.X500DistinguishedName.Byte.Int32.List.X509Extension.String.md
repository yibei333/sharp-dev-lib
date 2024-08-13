###### [主页](./Index.md "主页")
#### GenerateCert(String issuerPrivateKey, X500DistinguishedName issuer, Byte[] serialNumber, Int32 days, List\<X509Extension\> extensions, String friendlyName) 方法
**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")
**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")
**所属类型** : [X509CertificateSigningRequest](./SharpDevLib.Cryptography.X509CertificateSigningRequest.md "X509CertificateSigningRequest")
``` csharp
public X509Certificate2 GenerateCert(String issuerPrivateKey, X500DistinguishedName issuer, Byte[] serialNumber, Int32 days, List<X509Extension> extensions, String friendlyName)
```
**注释**
*生成证书*

**返回类型** : [X509Certificate2](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2 "X509Certificate2")

**参数**
|名称|类型|注释|
|---|---|---|
|issuerPrivateKey|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|颁发者私钥|
|issuer|[X500DistinguishedName](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x500distinguishedname "X500DistinguishedName")|颁发者|
|serialNumber|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|序列号|
|days|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|过期天数|
|extensions|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[X509Extension](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509extension "X509Extension")\>|扩展集合|
|friendlyName|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|友好名称|

