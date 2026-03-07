using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// MD5哈希扩展，提供MD5和HMAC-MD5哈希计算功能
/// </summary>
public static class Md5Helper
{
    /// <summary>
    /// 计算字节数组的MD5哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="length">哈希输出长度，默认为32位16进制字符串</param>
    /// <returns>MD5哈希值的16进制字符串</returns>
    public static string Md5(this byte[] bytes, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        var hexString = HashExtension.Hash(nameof(MD5), bytes);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 计算流的MD5哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="length">哈希输出长度，默认为32位16进制字符串</param>
    /// <returns>MD5哈希值的16进制字符串</returns>
    public static string Md5(this Stream stream, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        var hexString = HashExtension.Hash(nameof(MD5), stream);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 使用密钥计算字节数组的HMAC-MD5哈希值
    /// </summary>
    /// <param name="bytes">要计算哈希的字节数组</param>
    /// <param name="secret">HMAC密钥</param>
    /// <param name="length">哈希输出长度，默认为32位16进制字符串</param>
    /// <returns>HMAC-MD5哈希值的16进制字符串</returns>
    /// <exception cref="InvalidOperationException">当密钥长度超过64字节时引发异常</exception>
    public static string HmacMd5(this byte[] bytes, byte[] secret, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HashExtension.HMacHash(nameof(HMACMD5), secret, bytes);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 使用密钥计算流的HMAC-MD5哈希值
    /// </summary>
    /// <param name="stream">要计算哈希的流</param>
    /// <param name="secret">HMAC密钥</param>
    /// <param name="length">哈希输出长度，默认为32位16进制字符串</param>
    /// <returns>HMAC-MD5哈希值的16进制字符串</returns>
    /// <exception cref="InvalidOperationException">当密钥长度超过64字节时引发异常</exception>
    public static string HmacMd5(this Stream stream, byte[] secret, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HashExtension.HMacHash(nameof(HMACMD5), secret, stream);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }
}
