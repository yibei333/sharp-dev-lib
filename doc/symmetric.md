# Symmetric - 对称加密

提供对称加密功能。

##### 以AES为例,其他对称加密算法类似

```csharp
using SharpDevLib;
using System.Security.Cryptography;

//AES
var key = "0123456789ABCDEF0123456789ABCDEF".Utf8Decode();
var iv = "0123456789ABCDEF".Utf8Decode();
using var aes = Aes.Create();
aes.Mode = CipherMode.CBC;
aes.Padding = PaddingMode.PKCS7;
//设置对称算法的密钥，自动截取或补全密钥长度以符合算法要求
//处理步骤：
//1.循环检查允许的密钥长度[L1, L2, ..., Ln]
//2.如果密钥长度等于Li，直接使用该密钥
//3.如果密钥长度小于Li，返回[key + 补零字节]
//4.如果不是最后一次循环，则进入下一轮，否则返回key的前Li个字节
//支持算法：AES(16/24/32字节)、DES(8字节)、TripleDES(16/24字节)
aes.SetKeyAutoPad(key);
//设置对称算法的初始化向量(IV)，自动截取或补全长度以符合算法要求
//处理步骤
//1.设对称算法需要的初始化向量长度为L
//2.如果IV长度等于L，直接使用该IV
//3.如果IV长度小于L，返回[iv + 补零字节]
//4.如果IV长度大于L，返回iv的前L个字节
//支持算法：AES(16字节)、DES(8字节)、TripleDES(8字节)
aes.SetIVAutoPad(iv);

//AES加密字节数组
var encrypted = aes.Encrypt("Hello World".Utf8Decode());
Console.WriteLine(encrypted.Base64Encode());
//Dsi9m/4Yh2O9it41IrxzMw==

//AES解密字节数组
var decrypted = aes.Decrypt("Dsi9m/4Yh2O9it41IrxzMw==".Base64Decode());
Console.WriteLine(decrypted.Utf8Encode());
//Hello World

//AES加密流
"Hello World".Utf8Decode().SaveToFile("source.txt");
using var stream1 = new FileInfo("source.txt").OpenRead();
using var stream2 = new MemoryStream();
aes.Encrypt(stream1, stream2);
stream2.SaveToFile("encrypted.txt");
//Ƚ���c���5"�s3（示例）

//AES解密流
using var stream3 = new FileInfo("encrypted.txt").OpenRead();
using var stream4 = new MemoryStream();
aes.Decrypt(stream3, stream4);
stream4.SaveToFile("decrypted.txt");
Console.WriteLine(File.ReadAllText("decrypted.txt"));
//Hello World
```

## 相关文档
- [RSA密钥](rsakey.md)
- [JWT](jwt.md)
- [X.509证书](x509.md)
- [加解密](../README.md#加解密)
