namespace SharpDevLib.Cryptography;

/// <summary>
/// jwt验签结果
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
    /// 验证是否通过
    /// </summary>
    public bool IsVerified { get; }

    /// <summary>
    /// 算法
    /// </summary>
    public JwtAlgorithm Algorithm { get; }

    /// <summary>
    /// jwt头
    /// </summary>
    public string? Header { get; }

    /// <summary>
    /// jwt载荷
    /// </summary>
    public string? Payload { get; }

    /// <summary>
    /// 签名
    /// </summary>
    public string? Signature { get; }
}
