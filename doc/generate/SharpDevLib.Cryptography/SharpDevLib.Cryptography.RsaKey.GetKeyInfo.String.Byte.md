###### [主页](./Index.md "主页")

#### GetKeyInfo(String key, Byte[] password) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [RsaKey](./SharpDevLib.Cryptography.RsaKey.md "RsaKey")

``` csharp
public static RsaKeyInfo GetKeyInfo(String key, Byte[] password)
```

**注释**

*获取密钥信息*



**返回类型** : [RsaKeyInfo](./SharpDevLib.Cryptography.RsaKeyInfo.md "RsaKeyInfo")


**参数**

|名称|类型|注释|
|---|---|---|
|key|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|PEM格式的密钥|
|password|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|密码（仅当PEM格式为受密码保护的私钥时适用）|


