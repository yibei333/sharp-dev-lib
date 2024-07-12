using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 验证jwt请求模型
/// </summary>
public abstract class JwtVerifyRequest
{
    internal JwtVerifyRequest(JwtAlgorithm algorithm, string token, string key, RSASignaturePadding? padding)
    {
        Algorithm = algorithm;
        Token = token;
        Key = key;
        Padding = padding;
    }


    /// <summary>
    /// 算法
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// token
    /// </summary>
    public string Token { get; }

    internal string Key { get; }

    internal RSASignaturePadding? Padding { get; }
}

/// <summary>
/// 使用HMACSHA256算法验证JWT请求模型
/// </summary>
public class JwtVerifyWithHMACSHA256Request : JwtVerifyRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="token">token</param>
    /// <param name="secret">密钥</param>
    public JwtVerifyWithHMACSHA256Request(string token, byte[] secret) : base(JwtAlgorithm.HS256, token, secret.ToHexString(), null)
    {

    }
}

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