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