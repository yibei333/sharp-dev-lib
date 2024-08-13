###### [主页](./Index.md "主页")
#### SavePfx(this X509Certificate2 certificate, String path, String privateKey, String password) 方法
**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")
**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")
**所属类型** : [X509](./SharpDevLib.Cryptography.X509.md "X509")
``` csharp
public static Void SavePfx(this X509Certificate2 certificate, String path, String privateKey, String password)
```
**注释**
*保存为pfx格式到文件*

**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")

**参数**
|名称|类型|注释|
|---|---|---|
|certificate|[X509Certificate2](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2 "X509Certificate2")|证书|
|path|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|文件路径|
|privateKey|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|PEM格式的私钥|
|password|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|密码|
