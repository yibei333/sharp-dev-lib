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