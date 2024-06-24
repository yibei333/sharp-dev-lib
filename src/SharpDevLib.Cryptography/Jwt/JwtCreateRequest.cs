using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 创建jwt请求模型
/// </summary>
public abstract class JwtCreateRequest
{
    internal JwtCreateRequest(JwtAlgorithm algorithm, object payload, string key, byte[]? keyPassword, RSASignaturePadding? padding)
    {
        Algorithm = algorithm;
        Payload = payload;
        Key = key;
        KeyPassword = keyPassword;
        Padding = padding;
    }


    /// <summary>
    /// 算法
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// jwt载荷
    /// </summary>
    public object Payload { get; }

    internal string Key { get; }

    internal byte[]? KeyPassword { get; }

    internal RSASignaturePadding? Padding { get; }
}

/// <summary>
/// 使用HMACSHA256算法创建JWT请求模型
/// </summary>
public class JwtCreateWithHMACSHA256Request : JwtCreateRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="payload">载荷</param>
    /// <param name="secret">密钥</param>
    public JwtCreateWithHMACSHA256Request(object payload, byte[] secret) : base(JwtAlgorithm.HS256, payload, secret.ToHexString(), null, null)
    {

    }
}

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
        if (pemObject.PemType == RsaPemType.PublicKey || pemObject.PemType == RsaPemType.X509SubjectPublicKey) throw new ArgumentException("pemKey parameter should be private key type");
        if (pemObject.PemType == RsaPemType.EncryptedPkcs1PrivateKey || pemObject.PemType == RsaPemType.EncryptedPkcs8PrivateKey)
        {
            if (keyPassword.IsNullOrEmpty()) throw new ArgumentException("keyPassword parameter required");
        }
    }
}