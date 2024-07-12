namespace SharpDevLib.Cryptography;

/// <summary>
/// PEM格式类型
/// </summary>
public enum PemType
{
    /// <summary>
    /// 未知格式
    /// </summary>
    UnKnown,
    /// <summary>
    /// PKCS#1私钥
    /// </summary>
    Pkcs1PrivateKey,
    /// <summary>
    /// 受密码保护的PKCS#1私钥
    /// </summary>
    EncryptedPkcs1PrivateKey,
    /// <summary>
    /// PKCS#8私钥
    /// </summary>
    Pkcs8PrivateKey,
    /// <summary>
    /// 受密码保护的PKCS#8私钥
    /// </summary>
    EncryptedPkcs8PrivateKey,
    /// <summary>
    /// PKCS#1公钥
    /// </summary>
    PublicKey,
    /// <summary>
    /// X.509SubjectPublicKey
    /// </summary>
    X509SubjectPublicKey,
    /// <summary>
    /// X.509 Certificate
    /// </summary>
    X509Certificate,
}
