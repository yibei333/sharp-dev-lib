using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// 使用RSA SHA256算法验证JWT的请求模型
/// </summary>
public class JwtVerifyWithRS256Request : JwtVerifyRequest
{
    /// <summary>
    /// 初始化请求模型实例
    /// </summary>
    /// <param name="token">要验证的JWT令牌字符串</param>
    /// <param name="pemKey">PEM格式的RSA公钥</param>
    /// <param name="padding">RSA签名填充方式，默认使用Pkcs1</param>
    /// <exception cref="ArgumentException">当提供的密钥不是公钥类型时抛出</exception>
    public JwtVerifyWithRS256Request(string token, string pemKey, RSASignaturePadding? padding = null) : base(JwtAlgorithm.RS256, token, pemKey, padding)
    {
        var pemObject = PemObject.Read(pemKey);
        if (pemObject.PemType != PemType.PublicKey && pemObject.PemType != PemType.X509SubjectPublicKey) throw new ArgumentException("pemKey parameter should be public key type");
    }
}