using System.Security.Cryptography;

namespace SharpDevLib.Hash.Sha;

/// <summary>
/// Sha384哈希扩展
/// </summary>
public static class Sha384Extension
{
    /// <summary>
    /// 字节数组Sha384哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>Sha384哈希值</returns>
    public static string Sha384(this byte[] bytes) => HashExtension.Hash(nameof(SHA384), bytes);

    /// <summary>
    /// 流Sha384哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>Sha384哈希值</returns>
    public static string Sha384(this Stream stream) => HashExtension.Hash(nameof(SHA384), stream);

    /// <summary>
    /// 字节数组HmacSha384哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha384哈希值</returns>
    public static string HmacSha384(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA384), secret, bytes);

    /// <summary>
    /// 流HmacSha384哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha384哈希值</returns>
    public static string HmacSha384(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA384), secret, stream);
}
