using System.Security.Cryptography;

namespace SharpDevLib.Hash.Sha;

/// <summary>
/// Sha512哈希扩展
/// </summary>
public static class Sha512Extension
{
    /// <summary>
    /// 字节数组Sha512哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>Sha512哈希值</returns>
    public static string Sha512(this byte[] bytes) => HashExtension.Hash(nameof(SHA512), bytes);

    /// <summary>
    /// 流Sha512哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>Sha512哈希值</returns>
    public static string Sha512(this Stream stream) => HashExtension.Hash(nameof(SHA512), stream);

    /// <summary>
    /// 字节数组HmacSha512哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha512哈希值</returns>
    public static string HmacSha512(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA512), secret, bytes);

    /// <summary>
    /// 流HmacSha512哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>HmacSha512哈希值</returns>
    public static string HmacSha512(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA512), secret, stream);
}
