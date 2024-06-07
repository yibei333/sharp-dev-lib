using System.Security.Cryptography;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// md5长度
/// </summary>
public enum MD5Length
{
    /// <summary>
    /// 16位
    /// </summary>
    Sixteen,
    /// <summary>
    /// 32位
    /// </summary>
    ThirtyTwo
}

/// <summary>
/// 哈希扩展
/// </summary>
public static class HashExtension
{
    #region MD5
    /// <summary>
    /// 字节数组Md5哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this byte[] bytes, MD5Length length = MD5Length.ThirtyTwo)
    {
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(bytes);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流Md5哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this Stream stream, MD5Length length = MD5Length.ThirtyTwo)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(stream);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 字节数组Md5哈希(HMAC)
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this byte[] bytes, string secret, MD5Length length = MD5Length.ThirtyTwo)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        if (key.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64");
        using var md5 = new HMACMD5(key);
        var hashBytes = md5.ComputeHash(bytes);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流Md5哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this Stream stream, string secret, MD5Length length = MD5Length.ThirtyTwo)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        if (key.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64");
        using var md5 = new HMACMD5(key);
        var hashBytes = md5.ComputeHash(stream);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }
    #endregion

    #region SHA128
    /// <summary>
    /// 字节数组SHA128哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this byte[] bytes)
    {
        using var sha = SHA1.Create();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA128哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this Stream stream)
    {
        using var sha = SHA1.Create();
        return sha.ComputeHash(stream).ToHexString();
    }

    /// <summary>
    /// 字节数组SHA128哈希(HMAC)
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this byte[] bytes, string secret)
    {
        using var sha = new HMACSHA1();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA128哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this Stream stream, string secret)
    {
        using var sha = new HMACSHA1();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(stream).ToHexString();
    }
    #endregion

    #region SHA256
    /// <summary>
    /// 字节数组SHA256哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this byte[] bytes)
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA256哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this Stream stream)
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(stream).ToHexString();
    }

    /// <summary>
    /// 字节数组SHA256哈希(HMAC)
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this byte[] bytes, string secret)
    {
        using var sha = new HMACSHA256();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA256哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this Stream stream, string secret)
    {
        using var sha = new HMACSHA256();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(stream).ToHexString();
    }
    #endregion

    #region SHA384
    /// <summary>
    /// 字节数组SHA384哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this byte[] bytes)
    {
        using var sha = SHA384.Create();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA384哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this Stream stream)
    {
        using var sha = SHA384.Create();
        return sha.ComputeHash(stream).ToHexString();
    }

    /// <summary>
    /// 字节数组SHA384哈希(HMAC)
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this byte[] bytes, string secret)
    {
        using var sha = new HMACSHA384();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA384哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this Stream stream, string secret)
    {
        using var sha = new HMACSHA384();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(stream).ToHexString();
    }
    #endregion

    #region SHA512
    /// <summary>
    /// 字节数组SHA512哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this byte[] bytes)
    {
        using var sha = SHA512.Create();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA512哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this Stream stream)
    {
        using var sha = SHA512.Create();
        return sha.ComputeHash(stream).ToHexString();
    }

    /// <summary>
    /// 字节数组SHA512哈希(HMAC)
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this byte[] bytes, string secret)
    {
        using var sha = new HMACSHA512();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(bytes).ToHexString();
    }

    /// <summary>
    /// 流SHA512哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this Stream stream, string secret)
    {
        using var sha = new HMACSHA512();
        sha.Key = secret.ToUtf8Bytes();
        return sha.ComputeHash(stream).ToHexString();
    }
    #endregion
}
