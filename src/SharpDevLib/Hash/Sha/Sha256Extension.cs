using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// Sha256哈希扩展
/// </summary>
[BelongDirectory("Hash/Sha")]
public static class Sha256Extension
{
    /// <summary>
    /// 字节数组Sha256哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>Sha256哈希值</returns>
    public static string Sha256(this byte[] bytes) => HashExtension.Hash(nameof(SHA256), bytes);

    /// <summary>
    /// 流Sha256哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>Sha256哈希值</returns>
    public static string Sha256(this Stream stream) => HashExtension.Hash(nameof(SHA256), stream);

    /// <summary>
    /// 字节数组HmacSha256哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha256哈希值</returns>
    public static string HmacSha256(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA256), secret, bytes);

    /// <summary>
    /// 流HmacSha256哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha256哈希值</returns>
    public static string HmacSha256(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA256), secret, stream);
}
