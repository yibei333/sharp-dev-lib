# JWT - JSON Web Token

提供 JWT 的生成和验证功能。

#### 实例

```csharp
using System.Security.Cryptography;
using SharpDevLib;

var payload = new
{
    id = 1,
    name = "张三"
};

//使用HMACSHA256算法创建JWT
var token1 = new JwtCreateWithHMACSHA256Request(payload, "foo".Utf8Decode()).Create();
Console.WriteLine(token1);
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwibmFtZSI6Ilx1NUYyMFx1NEUwOSJ9.nGBAztIkApT3NdQZowM0qgnrqN12zIdSPFobYanQKiQ

//使用HMACSHA256算法验证JWT
var result1 = new JwtVerifyWithHMACSHA256Request(token1, "foo".Utf8Decode()).Verify();
Console.WriteLine(result1.Serialize(new JsonOption { FormatJson = true }));
//{
//  "IsVerified": true,
//  "Algorithm": 1,
//  "Header": "{\"alg\":\"HS256\",\"typ\":\"JWT\"}",
//  "Payload": "{\"id\":1,\"name\":\"\\u5F20\\u4E09\"}",
//  "Signature": "nGBAztIkApT3NdQZowM0qgnrqN12zIdSPFobYanQKiQ"
//}
Console.WriteLine(Regex.Unescape(result1.Payload!));
//{"id":1,"name":"张三"}

//使用RSA SHA256算法创建JWT
using var rsa=RSA.Create();
var privateKey=rsa.ExportPem(PemType.Pkcs1PrivateKey);
var publicKey=rsa.ExportPem(PemType.X509SubjectPublicKey);
var token2 = new JwtCreateWithRS256Request(payload,privateKey).Create();
Console.WriteLine(token2);
//eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwibmFtZSI6Ilx1NUYyMFx1NEUwOSJ9.mIG1wDNBGQdkS2mqKQe1SFojHW4zTbaAj3rY3gbF5jm559DJbXVWSg0HWhaejeBB7NMxU0aYMcuYmi44tvajFj9GQNLjAqWYz0VxwMGA98Imsrj9I82C-i12xh63FZ2_TyfMBPfQVuEM0xIUGxANLd8c0ovxLR_QxrPrXVoy5bvcpi_0Unr20y-0poE5nOJExuj93YVHFHNspMT1nZdxgDb0MaafDBQ90cRJ-j-k_ErRnBJMMV3D3_QThW79NUG_j2DjWsrg0m5n0VX8OFLCpfZNyKKnWG_7WpBAMNXCC5Ph4upxGYFjawE-aviudPBc7uvueVMqeTHcnQZoe1IXPw

//使用HMACSHA256算法验证JWT
var result2 = new JwtVerifyWithRS256Request(token2, publicKey).Verify();
Console.WriteLine(result2.Serialize(new JsonOption { FormatJson = true }));
//{
//  "IsVerified": true,
//  "Algorithm": 2,
//  "Header": "{\"alg\":\"RS256\",\"typ\":\"JWT\"}",
//  "Payload": "{\"id\":1,\"name\":\"\\u5F20\\u4E09\"}",
//  "Signature": "mIG1wDNBGQdkS2mqKQe1SFojHW4zTbaAj3rY3gbF5jm559DJbXVWSg0HWhaejeBB7NMxU0aYMcuYmi44tvajFj9GQNLjAqWYz0VxwMGA98Imsrj9I82C-i12xh63FZ2_TyfMBPfQVuEM0xIUGxANLd8c0ovxLR_QxrPrXVoy5bvcpi_0Unr20y-0poE5nOJExuj93YVHFHNspMT1nZdxgDb0MaafDBQ90cRJ-j-k_ErRnBJMMV3D3_QThW79NUG_j2DjWsrg0m5n0VX8OFLCpfZNyKKnWG_7WpBAMNXCC5Ph4upxGYFjawE-aviudPBc7uvueVMqeTHcnQZoe1IXPw"
//}
Console.WriteLine(Regex.Unescape(result2.Payload!));
//{"id":1,"name":"张三"}
```

## 相关文档
- [对称加密](symmetric.md)
- [RSA密钥](rsakey.md)
- [X.509证书](x509.md)
- [加解密](../README.md#加解密)