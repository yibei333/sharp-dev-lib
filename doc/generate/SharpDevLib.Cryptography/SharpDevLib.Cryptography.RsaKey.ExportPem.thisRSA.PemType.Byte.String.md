###### [主页](./Index.md "主页")

#### ExportPem(this RSA rsa, PemType pemType, Byte[] password, String encryptPkcs1PrivateKeyAlogorithm) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [RsaKey](./SharpDevLib.Cryptography.RsaKey.md "RsaKey")

``` csharp
public static String ExportPem(this RSA rsa, PemType pemType, Byte[] password, String encryptPkcs1PrivateKeyAlogorithm)
```

**注释**

*导出PEM格式的密钥*



**返回类型** : [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")


**参数**

|名称|类型|注释|
|---|---|---|
|rsa|[RSA](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa "RSA")|rsa algorithm|
|pemType|[PemType](./SharpDevLib.Cryptography.PemType.md "PemType")|PEM格式类型|
|password|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|密码(仅当PEM格式为受密码保护的私钥时适用)|
|encryptPkcs1PrivateKeyAlogorithm|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|加密算法(仅当PEM格式为受密码保护的PKCS#1私钥时适用),受支持的算法如下:
            (1)AES-256-CBC(2)DES-EDE3-CBC|


