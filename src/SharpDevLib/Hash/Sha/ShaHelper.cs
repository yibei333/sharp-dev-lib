using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// Sha哈希扩展
/// </summary>
public static class ShaHelper
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
