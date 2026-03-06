using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// RSA密钥参数类，存储RSA密钥的详细参数信息（16进制字符串表示）
/// </summary>
public class RsaKeyParameters
{
    internal RsaKeyParameters(RSAParameters parameters)
    {
        Modulus = parameters.Modulus.HexStringEncode();
        Exponent = parameters.Exponent.HexStringEncode();
        D = parameters.D.IsNullOrEmpty() ? null : parameters.D.HexStringEncode();
        P = parameters.D.IsNullOrEmpty() ? null : parameters.P.HexStringEncode();
        DP = parameters.D.IsNullOrEmpty() ? null : parameters.DP.HexStringEncode();
        DQ = parameters.D.IsNullOrEmpty() ? null : parameters.DQ.HexStringEncode();
        InverseQ = parameters.D.IsNullOrEmpty() ? null : parameters.InverseQ.HexStringEncode();
    }

    /// <summary>
    /// 模数（Modulus），RSA公钥和私钥共有的参数
    /// </summary>
    public string Modulus { get; }

    /// <summary>
    /// 公钥指数（Exponent），通常为65537（0x010001）
    /// </summary>
    public string Exponent { get; }

    /// <summary>
    /// 私钥指数（D），仅私钥包含此参数
    /// </summary>
    public string? D { get; }

    /// <summary>
    /// 质数因子P，仅私钥包含此参数
    /// </summary>
    public string? P { get; }

    /// <summary>
    /// CRT参数DP，仅私钥包含此参数，用于中国剩余定理优化
    /// </summary>
    public string? DP { get; }

    /// <summary>
    /// CRT参数DQ，仅私钥包含此参数，用于中国剩余定理优化
    /// </summary>
    public string? DQ { get; }

    /// <summary>
    /// CRT参数InverseQ，仅私钥包含此参数，用于中国剩余定理优化
    /// </summary>
    public string? InverseQ { get; }
}