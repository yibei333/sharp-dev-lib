###### [主页](./Index.md "主页")

## SymmetricAlgorithmExtension 类

### 定义

**程序集** : [SharpDevLib.Cryptography.dll](./SharpDevLib.Cryptography.assembly.md "SharpDevLib.Cryptography.dll")

**命名空间** : [SharpDevLib.Cryptography](./SharpDevLib.Cryptography.namespace.md "SharpDevLib.Cryptography")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class SymmetricAlgorithmExtension : Object
```

**注释**

*对称加密算法扩展*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Decrypt(this SymmetricAlgorithm algorithm, Byte[] data)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.Decrypt.thisSymmetricAlgorithm.Byte.md "Decrypt(this SymmetricAlgorithm algorithm, Byte[] data)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`是`|algorithm:对称算法<br>data:已加密的字节数组|
|[Decrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.Decrypt.thisSymmetricAlgorithm.Stream.Stream.md "Decrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|algorithm:对称算法<br>inputStream:已加密的流<br>outputStream:解密的流|
|[Encrypt(this SymmetricAlgorithm algorithm, Byte[] data)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.Encrypt.thisSymmetricAlgorithm.Byte.md "Encrypt(this SymmetricAlgorithm algorithm, Byte[] data)")|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|`public`|`是`|algorithm:对称算法<br>data:需要加密的字节数组|
|[Encrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.Encrypt.thisSymmetricAlgorithm.Stream.Stream.md "Encrypt(this SymmetricAlgorithm algorithm, Stream inputStream, Stream outputStream)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|algorithm:对称算法<br>inputStream:需要加密的流<br>outputStream:加密的流|
|[SetKey(this SymmetricAlgorithm algorithm, Byte[] key)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.SetKey.thisSymmetricAlgorithm.Byte.md "SetKey(this SymmetricAlgorithm algorithm, Byte[] key)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|algorithm:对称算法<br>key:密钥|
|[SetIV(this SymmetricAlgorithm algorithm, Byte[] iv)](./SharpDevLib.Cryptography.SymmetricAlgorithmExtension.SetIV.thisSymmetricAlgorithm.Byte.md "SetIV(this SymmetricAlgorithm algorithm, Byte[] iv)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|algorithm:对称算法<br>iv:初始化向量|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


