using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 使用RSA SHA256算法创建JWT请求模型
/// </summary>
public class JwtCreateWithRS256Request : JwtCreateRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="payload">载荷</param>
    /// <param name="pemKey">PEM格式的私钥</param>
    /// <param name="keyPassword">私钥密码,仅当私钥是受密码保护是适用</param>
    /// <param name="padding">RSA签名Padding</param>
    public JwtCreateWithRS256Request(object payload, string pemKey, byte[]? keyPassword = null, RSASignaturePadding? padding = null) : base(JwtAlgorithm.RS256, payload, pemKey, keyPassword, padding)
    {
        var pemObject = PemObject.Read(pemKey);
        if (pemObject.PemType == PemType.PublicKey || pemObject.PemType == PemType.X509SubjectPublicKey) throw new ArgumentException("pemKey parameter should be private key type");
        if (pemObject.PemType == PemType.EncryptedPkcs1PrivateKey || pemObject.PemType == PemType.EncryptedPkcs8PrivateKey)
        {
            if (keyPassword.IsNullOrEmpty()) throw new ArgumentException("keyPassword parameter required");
        }
    }
}