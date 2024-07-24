using System.Security.Cryptography;

namespace SharpDevLib.Hash.Sha;

/// <summary>
/// Sha128哈希扩展
/// </summary>
public static class Sha128Extension
{
    /// <summary>
    /// 字节数组Sha128哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>Sha128哈希值</returns>
    public static string Sha128(this byte[] bytes) => HashExtension.Hash(nameof(SHA1), bytes);

    /// <summary>
    /// 流Sha128哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>Sha128哈希值</returns>
    public static string Sha128(this Stream stream) => HashExtension.Hash(nameof(SHA1), stream);

    /// <summary>
    /// 字节数组HmacSha128哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha128哈希值</returns>
    public static string HmacSha128(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA1), secret, bytes);

    /// <summary>
    /// 流HmacSha128哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha128哈希值</returns>
    public static string HmacSha128(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA1), secret, stream);
}
