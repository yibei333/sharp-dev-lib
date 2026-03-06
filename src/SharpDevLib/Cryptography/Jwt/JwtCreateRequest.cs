using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// JWT创建请求基类，用于构建JWT创建请求
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
    /// JWT签名算法类型
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// JWT载荷数据，将被序列化为JSON字符串
    /// </summary>
    public object Payload { get; }

    internal string Key { get; }

    internal byte[]? KeyPassword { get; }

    internal RSASignaturePadding? Padding { get; }
}