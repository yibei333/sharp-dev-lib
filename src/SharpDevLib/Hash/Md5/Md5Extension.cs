using System.Security.Cryptography;

namespace SharpDevLib.Hash.Md5;

/// <summary>
/// MD5哈希扩展
/// </summary>
public static class Md5Extension
{
    /// <summary>
    /// 字节数组Md5哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string Md5(this byte[] bytes, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        var hexString = HashExtension.Hash(nameof(MD5), bytes);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流Md5哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string Md5(this Stream stream, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        var hexString = HashExtension.Hash(nameof(MD5), stream);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 字节数组HmacMd5哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>HmacMd5哈希值</returns>
    public static string HmacMd5(this byte[] bytes, byte[] secret, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HashExtension.HMacHash(nameof(HMACMD5), secret, bytes);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流HmacMd5哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>HmacMd5哈希值</returns>
    public static string HmacMd5(this Stream stream, byte[] secret, Md5OutputLength length = Md5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HashExtension.HMacHash(nameof(HMACMD5), secret, stream);
        if (length == Md5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }
}
