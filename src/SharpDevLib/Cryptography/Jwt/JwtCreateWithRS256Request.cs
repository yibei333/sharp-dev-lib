using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// 使用RSA SHA256算法创建JWT的请求模型
/// </summary>
public class JwtCreateWithRS256Request : JwtCreateRequest
{
    /// <summary>
    /// 初始化请求模型实例
    /// </summary>
    /// <param name="payload">JWT负载数据，将被序列化为JSON字符串</param>
    /// <param name="pemKey">PEM格式的RSA私钥</param>
    /// <param name="keyPassword">私钥密码，仅当私钥受密码保护时需要提供</param>
    /// <param name="padding">RSA签名填充方式，默认使用Pkcs1</param>
    /// <exception cref="ArgumentException">当提供的密钥不是私钥类型时抛出</exception>
    /// <exception cref="ArgumentException">当私钥受密码保护但未提供密码时抛出</exception>
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