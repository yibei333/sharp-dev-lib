using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// hash util
/// </summary>
public static class HashUtil
{
    #region HexString
    /// <summary>
    /// get hex string from byte array
    /// </summary>
    /// <param name="bytes">byte array</param>
    /// <returns>hex string</returns>
    public static string ToHexString(this byte[]? bytes)
    {
        if (bytes.IsEmpty()) return string.Empty;
        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// convert hex string to byte array
    /// </summary>
    /// <param name="hexString">hex string</param>
    /// <returns>byte array</returns>
    public static byte[] FromHexString(this string? hexString)
    {
        if ((hexString?.Length % 2) != 0) throw new Exception($"is not a valid hex string");
        if (hexString.IsNull()) return Array.Empty<byte>();
        var list = new List<byte>();

        for (int i = 0; i < hexString.Length / 2; i++)
        {
            list.Add(Convert.ToByte(string.Join("", hexString.Skip(i * 2).Take(2)), 16));
        }
        return list.ToArray();
    }
    #endregion

    #region Common
    /// <summary>
    /// sha compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha hash</returns>
    private static string SHAHash<T>(this T? obj, HashAlgorithm algorithm, WordCase? wordCase = null) where T : class
    {
        if (obj.IsNull()) return string.Empty;
        return obj.Serialize().SHAHash(algorithm, wordCase);
    }

    /// <summary>
    /// sha compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha hash</returns>
    private static string SHAHash(this string? str, HashAlgorithm algorithm, WordCase? wordCase = null)
    {
        if (str.IsNull()) return string.Empty;
        return Encoding.UTF8.GetBytes(str).SHAHash(algorithm, wordCase);
    }

    /// <summary>
    /// sha compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha hash</returns>
    private static string SHAHash(this byte[]? bytes, HashAlgorithm algorithm, WordCase? wordCase = null)
    {
        if (bytes.IsEmpty()) return string.Empty;
        var hashBytes = algorithm.ComputeHash(bytes);
        algorithm.Dispose();
        var hexString = hashBytes.ToHexString();
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }

    /// <summary>
    /// sha compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha hash</returns>
    private static string FileSHAHash(this Stream? stream, HashAlgorithm algorithm, WordCase? wordCase = null)
    {
        if (stream.IsNull() || stream.Length <= 0) return string.Empty;
        using var sha = SHA384.Create();
        var bytes = algorithm.ComputeHash(stream);
        algorithm.Dispose();
        var hexString = bytes.ToHexString();
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }

    /// <summary>
    /// sha compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha hash</returns>
    /// <exception cref="FileNotFoundException"></exception>
    private static string FileSHAHash(this string? filePath, HashAlgorithm algorithm, WordCase? wordCase = null)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);
        using var stream = File.OpenRead(filePath);
        return stream.FileSHAHash(algorithm, wordCase);
    }

    /// <summary>
    /// hmacsha compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="secret">hash secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha hash</returns>
    private static string HMACSHAHash<T>(this T? obj, KeyedHashAlgorithm algorithm, string secret, WordCase? wordCase = null) where T : class
    {
        if (obj.IsNull()) return string.Empty;
        return obj.Serialize().HMACSHAHash(algorithm, secret, wordCase);
    }

    /// <summary>
    /// hmacsha compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha hash</returns>
    private static string HMACSHAHash(this string? str, KeyedHashAlgorithm algorithm, string secret, WordCase? wordCase = null)
    {
        if (str.IsNull()) return string.Empty;
        return Encoding.UTF8.GetBytes(str).HMACSHAHash(algorithm, secret, wordCase);
    }

    /// <summary>
    /// hmacsha compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="algorithm">concrete algorithm</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha hash</returns>
    private static string HMACSHAHash(this byte[]? bytes, KeyedHashAlgorithm algorithm, string secret, WordCase? wordCase = null)
    {
        if (bytes.IsEmpty()) return string.Empty;
        algorithm.Key = secret.IsNull() ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(secret);
        var hashBytes = algorithm.ComputeHash(bytes);
        algorithm.Dispose();
        var hexString = hashBytes.ToHexString();
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }
    #endregion

    #region MD5
    /// <summary>
    /// md5 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>md5 hash</returns>
    public static string MD5Hash<T>(this T? obj, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null) where T : class
    {
        if (obj.IsNull()) return string.Empty;
        return obj.Serialize().MD5Hash(length, wordCase);
    }

    /// <summary>
    /// md5 compute a string
    /// </summary>
    /// <param name="str">string to compute</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>md5 hash</returns>
    public static string MD5Hash(this string? str, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (str.IsNull()) return string.Empty;
        return Encoding.UTF8.GetBytes(str).MD5Hash(length, wordCase);
    }

    /// <summary>
    /// md5 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>md5 hash</returns>
    public static string MD5Hash(this byte[]? bytes, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (bytes.IsEmpty()) return string.Empty;
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(bytes);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }

    /// <summary>
    /// md5 compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>md5 hash</returns>
    public static string FileMD5Hash(this Stream? stream, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (stream.IsNull() || stream.Length <= 0) return string.Empty;
        stream.Seek(0, SeekOrigin.Begin);
        using var md5 = MD5.Create();
        var bytes = md5.ComputeHash(stream);
        var hexString = bytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }

    /// <summary>
    /// md5 compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>md5 hash</returns>
    public static string FileMD5Hash(this string? filePath, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);
        using var stream = File.OpenRead(filePath);
        return stream.FileMD5Hash(length, wordCase);
    }
    #endregion

    #region HMACMD5
    /// <summary>
    /// hmacmd5 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmacmd5 hash</returns>
    public static string HMACMD5Hash<T>(this T? obj, string secret, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null) where T : class
    {
        if (obj.IsNull()) return string.Empty;
        return obj.Serialize().HMACMD5Hash(secret, length, wordCase);
    }

    /// <summary>
    /// hmacmd5 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="secret">hamc secret</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmacmd5 hash</returns>
    public static string HMACMD5Hash(this string? str, string secret, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (str.IsNull()) return string.Empty;
        return Encoding.UTF8.GetBytes(str).HMACMD5Hash(secret, length, wordCase);
    }

    /// <summary>
    /// hmacmd5 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compoute</param>
    /// <param name="secret">hamc secret</param>
    /// <param name="length">result length</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmacmd5 hash</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string HMACMD5Hash(this byte[]? bytes, string secret, MD5Length length = MD5Length.ThirtyTwo, WordCase? wordCase = null)
    {
        if (bytes.IsEmpty()) return string.Empty;
        var key = secret.IsNull() ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(secret);
        if (key.Length > 64) throw new InvalidOperationException("md5 secret length can't more than 64");
        using var md5 = new HMACMD5(key);
        var hashBytes = md5.ComputeHash(bytes);
        var hexString = hashBytes.ToHexString();
        if (length == MD5Length.Sixteen) hexString = hexString.Substring(8, 16);
        if (wordCase.IsNull()) return hexString;
        if (wordCase == WordCase.UpperCase) return hexString.ToUpper();
        return hexString.ToLower();
    }
    #endregion

    #region SHA128
    /// <summary>
    /// sha128 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha128 hash</returns>
    public static string SHA128Hash<T>(this T? obj, WordCase? wordCase = null) where T : class => obj.SHAHash(SHA1.Create(), wordCase);

    /// <summary>
    /// sha128 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha128 hash</returns>
    public static string SHA128Hash(this string? str, WordCase? wordCase = null) => str.SHAHash(SHA1.Create(), wordCase);

    /// <summary>
    /// sha128 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha128 hash</returns>
    public static string SHA128Hash(this byte[]? bytes, WordCase? wordCase = null) => bytes.SHAHash(SHA1.Create(), wordCase);

    /// <summary>
    /// sha128 compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha128 hash</returns>
    public static string FileSHA128Hash(this Stream? stream, WordCase? wordCase = null) => stream.FileSHAHash(SHA1.Create(), wordCase);

    /// <summary>
    /// sha128 compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha128 hash</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string FileSHA128Hash(this string? filePath, WordCase? wordCase = null) => filePath.FileSHAHash(SHA1.Create(), wordCase);
    #endregion

    #region HMACSHA128
    /// <summary>
    /// hmacsha128 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="secret">hash secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha128 hash</returns>
    public static string HMACSHA128Hash<T>(this T? obj, string secret, WordCase? wordCase = null) where T : class => obj.HMACSHAHash(new HMACSHA1(), secret, wordCase);

    /// <summary>
    /// hmacsha128 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha128 hash</returns>
    public static string HMACSHA128Hash(this string? str, string secret, WordCase? wordCase = null) => str.HMACSHAHash(new HMACSHA1(), secret, wordCase);

    /// <summary>
    /// hmacsha128 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha128 hash</returns>
    public static string HMACSHA128Hash(this byte[]? bytes, string secret, WordCase? wordCase = null) => bytes.HMACSHAHash(new HMACSHA1(), secret, wordCase);
    #endregion

    #region SHA256
    /// <summary>
    /// sha256 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha256 hash</returns>
    public static string SHA256Hash<T>(this T? obj, WordCase? wordCase = null) where T : class => obj.SHAHash(SHA256.Create(), wordCase);

    /// <summary>
    /// sha256 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha256 hash</returns>
    public static string SHA256Hash(this string? str, WordCase? wordCase = null) => str.SHAHash(SHA256.Create(), wordCase);

    /// <summary>
    /// sha256 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha256 hash</returns>
    public static string SHA256Hash(this byte[]? bytes, WordCase? wordCase = null) => bytes.SHAHash(SHA256.Create(), wordCase);

    /// <summary>
    /// sha256 compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha256 hash</returns>
    public static string FileSHA256Hash(this Stream? stream, WordCase? wordCase = null) => stream.FileSHAHash(SHA256.Create(), wordCase);

    /// <summary>
    /// sha256 compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha256 hash</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string FileSHA256Hash(this string? filePath, WordCase? wordCase = null) => filePath.FileSHAHash(SHA256.Create(), wordCase);
    #endregion

    #region HMACSHA256
    /// <summary>
    /// hmacsha256 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="secret">hash secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha256 hash</returns>
    public static string HMACSHA256Hash<T>(this T? obj, string secret, WordCase? wordCase = null) where T : class => obj.HMACSHAHash(new HMACSHA256(), secret, wordCase);

    /// <summary>
    /// hmacsha256 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha256 hash</returns>
    public static string HMACSHA256Hash(this string? str, string secret, WordCase? wordCase = null) => str.HMACSHAHash(new HMACSHA256(), secret, wordCase);

    /// <summary>
    /// hmacsha256 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha256 hash</returns>
    public static string HMACSHA256Hash(this byte[]? bytes, string secret, WordCase? wordCase = null) => bytes.HMACSHAHash(new HMACSHA256(), secret, wordCase);
    #endregion

    #region SHA384
    /// <summary>
    /// sha384 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha384 hash</returns>
    public static string SHA384Hash<T>(this T? obj, WordCase? wordCase = null) where T : class => obj.SHAHash(SHA384.Create(), wordCase);

    /// <summary>
    /// sha384 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha384 hash</returns>
    public static string SHA384Hash(this string? str, WordCase? wordCase = null) => str.SHAHash(SHA384.Create(), wordCase);

    /// <summary>
    /// sha384 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha384 hash</returns>
    public static string SHA384Hash(this byte[]? bytes, WordCase? wordCase = null) => bytes.SHAHash(SHA384.Create(), wordCase);

    /// <summary>
    /// sha384 compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha384 hash</returns>
    public static string FileSHA384Hash(this Stream? stream, WordCase? wordCase = null) => stream.FileSHAHash(SHA384.Create(), wordCase);

    /// <summary>
    /// sha384 compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha384 hash</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string FileSHA384Hash(this string? filePath, WordCase? wordCase = null) => filePath.FileSHAHash(SHA384.Create(), wordCase);
    #endregion

    #region HMACSHA384
    /// <summary>
    /// hmacsha384 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="secret">hash secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha384 hash</returns>
    public static string HMACSHA384Hash<T>(this T? obj, string secret, WordCase? wordCase = null) where T : class => obj.HMACSHAHash(new HMACSHA384(), secret, wordCase);

    /// <summary>
    /// hmacsha384 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha384 hash</returns>
    public static string HMACSHA384Hash(this string? str, string secret, WordCase? wordCase = null) => str.HMACSHAHash(new HMACSHA384(), secret, wordCase);

    /// <summary>
    /// hmacsha384 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha384 hash</returns>
    public static string HMACSHA384Hash(this byte[]? bytes, string secret, WordCase? wordCase = null) => bytes.HMACSHAHash(new HMACSHA384(), secret, wordCase);
    #endregion

    #region SHA512
    /// <summary>
    /// sha512 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha512 hash</returns>
    public static string SHA512Hash<T>(this T? obj, WordCase? wordCase = null) where T : class => obj.SHAHash(SHA512.Create(), wordCase);

    /// <summary>
    /// sha512 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha512 hash</returns>
    public static string SHA512Hash(this string? str, WordCase? wordCase = null) => str.SHAHash(SHA512.Create(), wordCase);

    /// <summary>
    /// sha512 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha512 hash</returns>
    public static string SHA512Hash(this byte[]? bytes, WordCase? wordCase = null) => bytes.SHAHash(SHA512.Create(), wordCase);

    /// <summary>
    /// sha512 compute a file
    /// </summary>
    /// <param name="stream">file stream</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha512 hash</returns>
    public static string FileSHA512Hash(this Stream? stream, WordCase? wordCase = null) => stream.FileSHAHash(SHA512.Create(), wordCase);

    /// <summary>
    /// sha512 compute a file
    /// </summary>
    /// <param name="filePath">file path</param>
    /// <param name="wordCase">word case</param>
    /// <returns>sha512 hash</returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static string FileSHA512Hash(this string? filePath, WordCase? wordCase = null) => filePath.FileSHAHash(SHA512.Create(), wordCase);
    #endregion

    #region HMACSHA512
    /// <summary>
    /// hmacsha512 compute a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object to compute</param>
    /// <param name="secret">hash secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha512 hash</returns>
    public static string HMACSHA512Hash<T>(this T? obj, string secret, WordCase? wordCase = null) where T : class => obj.HMACSHAHash(new HMACSHA512(), secret, wordCase);

    /// <summary>
    /// hmacsha512 compute a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha512 hash</returns>
    public static string HMACSHA512Hash(this string? str, string secret, WordCase? wordCase = null) => str.HMACSHAHash(new HMACSHA512(), secret, wordCase);

    /// <summary>
    /// hmacsha512 compute a byte array
    /// </summary>
    /// <param name="bytes">byte array to compute</param>
    /// <param name="secret">hmac secret</param>
    /// <param name="wordCase">word case</param>
    /// <returns>hmac sha512 hash</returns>
    public static string HMACSHA512Hash(this byte[]? bytes, string secret, WordCase? wordCase = null) => bytes.HMACSHAHash(new HMACSHA512(), secret, wordCase);
    #endregion
}
