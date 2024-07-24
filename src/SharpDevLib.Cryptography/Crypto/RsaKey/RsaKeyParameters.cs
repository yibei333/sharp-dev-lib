using SharpDevLib.Cryptography.Internal.References;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// RSA密钥参数
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
    /// Modulus参数
    /// </summary>
    public string Modulus { get; }

    /// <summary>
    /// Exponent参数
    /// </summary>
    public string Exponent { get; }

    /// <summary>
    /// D参数
    /// </summary>
    public string? D { get; }

    /// <summary>
    /// P参数
    /// </summary>
    public string? P { get; }

    /// <summary>
    /// DP参数
    /// </summary>
    public string? DP { get; }

    /// <summary>
    /// DQ参数
    /// </summary>
    public string? DQ { get; }

    /// <summary>
    /// InverseQ参数
    /// </summary>
    public string? InverseQ { get; }
}