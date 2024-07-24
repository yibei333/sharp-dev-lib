using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 使用RSA SHA256算法验证JWT请求模型
/// </summary>
public class JwtVerifyWithRS256Request : JwtVerifyRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="token">token</param>
    /// <param name="pemKey">PEM格式的公钥</param>
    /// <param name="padding">RSA签名Padding</param>
    public JwtVerifyWithRS256Request(string token, string pemKey, RSASignaturePadding? padding = null) : base(JwtAlgorithm.RS256, token, pemKey, padding)
    {
        var pemObject = PemObject.Read(pemKey);
        if (pemObject.PemType != PemType.PublicKey && pemObject.PemType != PemType.X509SubjectPublicKey) throw new ArgumentException("pemKey parameter should be public key type");
    }
}