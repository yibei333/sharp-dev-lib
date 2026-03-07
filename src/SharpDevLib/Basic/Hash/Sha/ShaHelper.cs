using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// SHA哈希扩展，提供SHA-1、SHA-256、SHA-384、SHA-512及其HMAC哈希计算功能
/// </summary>
public static class ShaHelper
{
    /// <summary>
    /// 计算字节数组的SHA-1哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <returns>SHA-1哈希值的16进制字符串</returns>
    public static string Sha128(this byte[] bytes) => HashExtension.Hash(nameof(SHA1), bytes);

    /// <summary>
    /// 计算流的SHA-1哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <returns>SHA-1哈希值的16进制字符串</returns>
    public static string Sha128(this Stream stream) => HashExtension.Hash(nameof(SHA1), stream);

    /// <summary>
    /// 使用密钥计算字节数组的HMAC-SHA1哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA1哈希值的16进制字符串</returns>
    public static string HmacSha128(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA1), secret, bytes);

    /// <summary>
    /// 使用密钥计算流的HMAC-SHA1哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA1哈希值的16进制字符串</returns>
    public static string HmacSha128(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA1), secret, stream);

    /// <summary>
    /// 计算字节数组的SHA-256哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <returns>SHA-256哈希值的16进制字符串</returns>
    public static string Sha256(this byte[] bytes) => HashExtension.Hash(nameof(SHA256), bytes);

    /// <summary>
    /// 计算流的SHA-256哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <returns>SHA-256哈希值的16进制字符串</returns>
    public static string Sha256(this Stream stream) => HashExtension.Hash(nameof(SHA256), stream);

    /// <summary>
    /// 使用密钥计算字节数组的HMAC-SHA256哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA256哈希值的16进制字符串</returns>
    public static string HmacSha256(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA256), secret, bytes);

    /// <summary>
    /// 使用密钥计算流的HMAC-SHA256哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA256哈希值的16进制字符串</returns>
    public static string HmacSha256(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA256), secret, stream);

    /// <summary>
    /// 计算字节数组的SHA-384哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <returns>SHA-384哈希值的16进制字符串</returns>
    public static string Sha384(this byte[] bytes) => HashExtension.Hash(nameof(SHA384), bytes);

    /// <summary>
    /// 计算流的SHA-384哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <returns>SHA-384哈希值的16进制字符串</returns>
    public static string Sha384(this Stream stream) => HashExtension.Hash(nameof(SHA384), stream);

    /// <summary>
    /// 使用密钥计算字节数组的HMAC-SHA384哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA384哈希值的16进制字符串</returns>
    public static string HmacSha384(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA384), secret, bytes);

    /// <summary>
    /// 使用密钥计算流的HMAC-SHA384哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA384哈希值的16进制字符串</returns>
    public static string HmacSha384(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA384), secret, stream);

    /// <summary>
    /// 计算字节数组的SHA-512哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <returns>SHA-512哈希值的16进制字符串</returns>
    public static string Sha512(this byte[] bytes) => HashExtension.Hash(nameof(SHA512), bytes);

    /// <summary>
    /// 计算流的SHA-512哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <returns>SHA-512哈希值的16进制字符串</returns>
    public static string Sha512(this Stream stream) => HashExtension.Hash(nameof(SHA512), stream);

    /// <summary>
    /// 使用密钥计算字节数组的HMAC-SHA512哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA512哈希值的16进制字符串</returns>
    public static string HmacSha512(this byte[] bytes, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA512), secret, bytes);

    /// <summary>
    /// 使用密钥计算流的HMAC-SHA512哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="secret">HMAC密钥</param>
    /// <returns>HMAC-SHA512哈希值的16进制字符串</returns>
    public static string HmacSha512(this Stream stream, byte[] secret) => HashExtension.HMacHash(nameof(HMACSHA512), secret, stream);
}
