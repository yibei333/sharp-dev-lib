###### [主页](./Index.md "主页")

#### GenerateSelfSignedCert(String privateKey, X509CertificateSigningRequest csr, Byte[] serialNumber, Int32 days, List\<X509Extension\> extensions, String friendlyName) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [X509](./SharpDevLib.Cryptography.X509.md "X509")

``` csharp
public static X509Certificate2 GenerateSelfSignedCert(String privateKey, X509CertificateSigningRequest csr, Byte[] serialNumber, Int32 days, List<X509Extension> extensions, String friendlyName)
```

**注释**

*生成自签名证书*



**返回类型** : [X509Certificate2](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2 "X509Certificate2")


**参数**

|名称|类型|注释|
|---|---|---|
|privateKey|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|PEM格式的私钥|
|csr|[X509CertificateSigningRequest](./SharpDevLib.Cryptography.X509CertificateSigningRequest.md "X509CertificateSigningRequest")|证书签名请求|
|serialNumber|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|序列号|
|days|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|过期天数|
|extensions|[List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 "List")\<[X509Extension](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509extension "X509Extension")\>|扩展|
|friendlyName|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|友好名称|


