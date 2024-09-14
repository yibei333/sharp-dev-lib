###### [主页](./Index.md "主页")

#### ImportPem(this RSA rsa, String pem, Byte[] password) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [RsaKey](./SharpDevLib.Cryptography.RsaKey.md "RsaKey")

``` csharp
public static Void ImportPem(this RSA rsa, String pem, Byte[] password)
```

**注释**

*导入Pem格式的密钥*



**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")


**参数**

|名称|类型|注释|
|---|---|---|
|rsa|[RSA](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa "RSA")|rsa algorithm|
|pem|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|Pem格式的密钥,支持的格式如下:<br>(1)PKCS#1私钥<br>(2)受密码保护的PKCS#1私钥<br>(3)PKCS#1公钥<br>(4)PKCS#8私钥<br>(5)受密码保护的PKCS#8私钥<br>(6)X.509SubjectPublicKey|
|password|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|密码（仅当PEM格式为受密码保护的私钥时适用）|


