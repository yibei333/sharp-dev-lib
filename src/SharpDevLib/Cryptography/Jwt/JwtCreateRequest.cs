using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// JWT创建请求基类，用于构建JWT创建请求
/// </summary>
internal class JwtCreateRequest(JwtAlgorithm algorithm, object payload, string key, byte[]? keyPassword, RSASignaturePadding? padding)
{
    /// <summary>
    /// JWT签名算法类型
    /// </summary>
    public JwtAlgorithm Algorithm { get; } = algorithm;

    /// <summary>
    /// JWT载荷数据，将被序列化为JSON字符串
    /// </summary>
    public object Payload { get; } = payload;

    internal string Key { get; } = key;

    internal byte[]? KeyPassword { get; } = keyPassword;

    internal RSASignaturePadding? Padding { get; } = padding;
}