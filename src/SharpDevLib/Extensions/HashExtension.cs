using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// md5输出长度
/// </summary>
public enum MD5OutputLength
{
    /// <summary>
    /// 16个字符
    /// </summary>
    Sixteen,
    /// <summary>
    /// 32个字符
    /// </summary>
    ThirtyTwo
}

/// <summary>
/// 哈希扩展
/// </summary>
public static class HashExtension
{
    #region Common
    static HashAlgorithm GetHashAlgorithm(string algorithmName)
    {
        if (algorithmName.Equals(nameof(MD5))) return MD5.Create();
        if (algorithmName.Equals(nameof(SHA1))) return SHA1.Create();
        if (algorithmName.Equals(nameof(SHA256))) return SHA256.Create();
        if (algorithmName.Equals(nameof(SHA384))) return SHA384.Create();
        if (algorithmName.Equals(nameof(SHA512))) return SHA512.Create();
        throw new NotSupportedException();
    }

    static string Hash(string algorithmName, byte[] bytes)
    {
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(bytes).ToHexString();
    }

    static string Hash(string algorithmName, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(stream).ToHexString();
    }

    static HashAlgorithm GetHMacHashAlgorithm(string algorithmName, byte[] secret)
    {
        if (algorithmName.Equals(nameof(HMACMD5))) return new HMACMD5(secret);
        if (algorithmName.Equals(nameof(HMACSHA1))) return new HMACSHA1(secret);
        if (algorithmName.Equals(nameof(HMACSHA256))) return new HMACSHA256(secret);
        if (algorithmName.Equals(nameof(HMACSHA384))) return new HMACSHA384(secret);
        if (algorithmName.Equals(nameof(HMACSHA512))) return new HMACSHA512(secret);
        throw new NotSupportedException();
    }

    static string HMacHash(string algorithmName, byte[] secret, byte[] bytes)
    {
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(bytes).ToHexString();
    }

    static string HMacHash(string algorithmName, byte[] secret, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(stream).ToHexString();
    }
    #endregion

    #region MD5
    /// <summary>
    /// 字节数组Md5哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this byte[] bytes, MD5OutputLength length = MD5OutputLength.ThirtyTwo)
    {
        var hexString = Hash(nameof(MD5), bytes);
        if (length == MD5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流Md5哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="length">长度</param>
    /// <returns>md5哈希值</returns>
    public static string MD5Hash(this Stream stream, MD5OutputLength length = MD5OutputLength.ThirtyTwo)
    {
        var hexString = Hash(nameof(MD5), stream);
        if (length == MD5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }
    #endregion

    #region SHA128
    /// <summary>
    /// 字节数组SHA128哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this byte[] bytes) => Hash(nameof(SHA1), bytes);

    /// <summary>
    /// 流SHA128哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA128哈希值</returns>
    public static string SHA128Hash(this Stream stream) => Hash(nameof(SHA1), stream);
    #endregion

    #region SHA256
    /// <summary>
    /// 字节数组SHA256哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this byte[] bytes) => Hash(nameof(SHA256), bytes);

    /// <summary>
    /// 流SHA256哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA256哈希值</returns>
    public static string SHA256Hash(this Stream stream) => Hash(nameof(SHA256), stream);
    #endregion

    #region SHA384
    /// <summary>
    /// 字节数组SHA384哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this byte[] bytes) => Hash(nameof(SHA384), bytes);

    /// <summary>
    /// 流SHA384哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA384哈希值</returns>
    public static string SHA384Hash(this Stream stream) => Hash(nameof(SHA384), stream);
    #endregion

    #region SHA512
    /// <summary>
    /// 字节数组SHA512哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this byte[] bytes) => Hash(nameof(SHA512), bytes);

    /// <summary>
    /// 流SHA512哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>SHA512哈希值</returns>
    public static string SHA512Hash(this Stream stream) => Hash(nameof(SHA512), stream);
    #endregion

    #region HMACMD5
    /// <summary>
    /// 字节数组HMACMd5哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>HMACMd5哈希值</returns>
    public static string HMACMD5Hash(this byte[] bytes, byte[] secret, MD5OutputLength length = MD5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HMacHash(nameof(HMACMD5), secret, bytes);
        if (length == MD5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }

    /// <summary>
    /// 流HMACMd5哈希
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <param name="length">长度</param>
    /// <returns>HMACMd5哈希值</returns>
    public static string HMACMD5Hash(this Stream stream, byte[] secret, MD5OutputLength length = MD5OutputLength.ThirtyTwo)
    {
        if (secret.Length > 64) throw new InvalidOperationException("md5 secret length should less than equal 64 bytes");
        var hexString = HMacHash(nameof(HMACMD5), secret, stream);
        if (length == MD5OutputLength.Sixteen) hexString = hexString.Substring(8, 16);
        return hexString;
    }
    #endregion

    #region HMACSHA128
    /// <summary>
    /// 字节数组HMACSHA128哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HMACSHA128哈希值</returns>
    public static string HMACSHA128Hash(this byte[] bytes, byte[] secret) => HMacHash(nameof(HMACSHA1), secret, bytes);

    /// <summary>
    /// 流SHA128哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA128哈希值</returns>
    public static string HMACSHA128Hash(this Stream stream, byte[] secret) => HMacHash(nameof(HMACSHA1), secret, stream);
    #endregion

    #region HMACSHA256
    /// <summary>
    /// 字节数组HMACSHA256哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HMACSHA256哈希值</returns>
    public static string HMACSHA256Hash(this byte[] bytes, byte[] secret) => HMacHash(nameof(HMACSHA256), secret, bytes);

    /// <summary>
    /// 流SHA256哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA256哈希值</returns>
    public static string HMACSHA256Hash(this Stream stream, byte[] secret) => HMacHash(nameof(HMACSHA256), secret, stream);
    #endregion

    #region HMACSHA384
    /// <summary>
    /// 字节数组HMACSHA384哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HMACSHA384哈希值</returns>
    public static string HMACSHA384Hash(this byte[] bytes, byte[] secret) => HMacHash(nameof(HMACSHA384), secret, bytes);

    /// <summary>
    /// 流SHA384哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA384哈希值</returns>
    public static string HMACSHA384Hash(this Stream stream, byte[] secret) => HMacHash(nameof(HMACSHA384), secret, stream);
    #endregion

    #region HMACSHA512
    /// <summary>
    /// 字节数组HMACSHA512哈希
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="secret">密钥</param>
    /// <returns>HMACSHA512哈希值</returns>
    public static string HMACSHA512Hash(this byte[] bytes, byte[] secret) => HMacHash(nameof(HMACSHA512), secret, bytes);

    /// <summary>
    /// 流SHA512哈希(HMAC)
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="secret">密钥</param>
    /// <returns>SHA512哈希值</returns>
    public static string HMACSHA512Hash(this Stream stream, byte[] secret) => HMacHash(nameof(HMACSHA512), secret, stream);
    #endregion
}
