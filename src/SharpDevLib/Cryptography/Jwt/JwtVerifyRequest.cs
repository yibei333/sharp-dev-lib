using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// JWT验证请求基类，用于构建JWT验证请求
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
    /// JWT签名算法类型
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// 要验证的JWT令牌字符串
    /// </summary>
    public string Token { get; }

    internal string Key { get; }

    internal RSASignaturePadding? Padding { get; }
}