###### [主页](./Index.md "主页")

#### SetIV(this SymmetricAlgorithm algorithm, Byte[] iv) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [SymmetricAlgorithmExtension](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.md "SymmetricAlgorithmExtension")

``` csharp
public static Void SetIV(this SymmetricAlgorithm algorithm, Byte[] iv)
```

**注释**

*设置对称算法的初始化向量,自动截取或补全密钥长度,步骤如下*

* 1.设对称算法需要的初始化向量的长度为L

* 2.如果密钥长度等于L,直接返回

* 3.如果密钥长度小于L,返回[iv,(补上(L-iv.Length)个0字节)]

* 4.返回iv[...L]



**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")


**参数**

|名称|类型|注释|
|---|---|---|
|algorithm|[SymmetricAlgorithm](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.symmetricalgorithm "SymmetricAlgorithm")|对称算法|
|iv|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|初始化向量|


