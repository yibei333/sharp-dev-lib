using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// JWT验证请求基类，用于构建JWT验证请求
/// </summary>
internal class JwtVerifyRequest(JwtAlgorithm algorithm, string token, string key, RSASignaturePadding? padding)
{
    /// <summary>
    /// JWT签名算法类型
    /// </summary>
    public JwtAlgorithm Algorithm { get; } = algorithm;

    /// <summary>
    /// 要验证的JWT令牌字符串
    /// </summary>
    public string Token { get; } = token;

    internal string Key { get; } = key;

    internal RSASignaturePadding? Padding { get; } = padding;
}