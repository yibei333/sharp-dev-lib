using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// RSA密钥信息类，包含密钥的类型、长度、加密状态和参数信息
/// </summary>
public class RsaKeyInfo
{
    internal RsaKeyInfo(PemType type, int keySize, bool isPrivate, bool isEncrypted, RSAParameters? parameters)
    {
        Type = type;
        IsPrivate = isPrivate;
        IsEncrypted = isEncrypted;
        KeySize = keySize;
        Parameters = parameters is null ? null : new RsaKeyParameters(parameters.Value);
    }

    /// <summary>
    /// PEM密钥类型
    /// </summary>
    public PemType Type { get; }

    /// <summary>
    /// 是否为私钥，true表示私钥，false表示公钥
    /// </summary>
    public bool IsPrivate { get; }

    /// <summary>
    /// 是否受密码保护，true表示需要密码才能使用密钥
    /// </summary>
    public bool IsEncrypted { get; }

    /// <summary>
    /// RSA密钥长度（比特数），如2048、4096等
    /// </summary>
    public int KeySize { get; set; }

    /// <summary>
    /// RSA密钥参数详情，包含Modulus、Exponent等参数的16进制表示
    /// </summary>
    public RsaKeyParameters? Parameters { get; }
}