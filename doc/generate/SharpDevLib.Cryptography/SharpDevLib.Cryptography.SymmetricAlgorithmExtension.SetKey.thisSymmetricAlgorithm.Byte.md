###### [主页](./Index.md "主页")

#### SetKey(this SymmetricAlgorithm algorithm, Byte[] key) 方法

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**所属类型** : [SymmetricAlgorithmExtension](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.md "SymmetricAlgorithmExtension")

``` csharp
public static Void SetKey(this SymmetricAlgorithm algorithm, Byte[] key)
```

**注释**

*设置对称算法的密钥和密钥长度,自动截取或补全密钥长度,步骤如下*

* 1.循环允许的密钥长度[L1,L2,...,Ln]

* 2.如果密钥长度等于Ln,直接返回密钥

* 3.如果密钥长度小于Ln,返回[key,(补上足够的0字节)]

* 4.如果不是最后一次循环,则进入下个循环,否则返回key[...Ln]



**返回类型** : [Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")


**参数**

|名称|类型|注释|
|---|---|---|
|algorithm|[SymmetricAlgorithm](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.symmetricalgorithm "SymmetricAlgorithm")|对称算法|
|key|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|密钥|


