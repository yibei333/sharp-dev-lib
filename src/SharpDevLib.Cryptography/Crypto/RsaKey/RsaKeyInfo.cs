using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// RSA密钥信息
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
    /// 类型
    /// </summary>
    public PemType Type { get; }

    /// <summary>
    /// 是否是私钥
    /// </summary>
    public bool IsPrivate { get; }

    /// <summary>
    /// 是否受密码保护
    /// </summary>
    public bool IsEncrypted { get; }

    /// <summary>
    /// 密钥长度
    /// </summary>
    public int KeySize { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public RsaKeyParameters? Parameters { get; }
}