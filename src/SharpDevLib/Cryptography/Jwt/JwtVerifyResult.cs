namespace SharpDevLib;

/// <summary>
/// JWT验证结果类，包含验证状态和JWT各部分信息
/// </summary>
public class JwtVerifyResult
{
    internal JwtVerifyResult(bool isVerified)
    {
        IsVerified = isVerified;
    }

    internal JwtVerifyResult(bool isVerified, JwtAlgorithm algorithm, string? header, string? payload, string? signature)
    {
        IsVerified = isVerified;
        Header = header;
        Algorithm = algorithm;
        Payload = payload;
        Signature = signature;
    }

    /// <summary>
    /// 验证是否通过，true表示验证成功，false表示验证失败
    /// </summary>
    public bool IsVerified { get; }

    /// <summary>
    /// JWT使用的签名算法类型
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// JWT头部部分的Base64Url解码字符串
    /// </summary>
    public string? Header { get; }

    /// <summary>
    /// JWT载荷部分的Base64Url解码字符串
    /// </summary>
    public string? Payload { get; }

    /// <summary>
    /// JWT签名部分的Base64Url解码字符串
    /// </summary>
    public string? Signature { get; }
}
