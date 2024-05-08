using System.Text;
using System.Web;

namespace SharpDevLib;

/// <summary>
/// encode util
/// </summary>
public static class EncodeUtil
{
    #region Base64
    /// <summary>
    /// base64 encode a object(use serialize)
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="obj">object value</param>
    /// <returns>base64</returns>
    public static string Base64Encode<T>(this T? obj) where T : class => obj?.Serialize().Base64Encode() ?? string.Empty;

    /// <summary>
    /// base64 encode byte array
    /// </summary>
    /// <param name="bytes">byte array</param>
    /// <returns>base64</returns>
    public static string Base64Encode(this byte[]? bytes) => bytes.IsNull() ? string.Empty : Convert.ToBase64String(bytes);

    /// <summary>
    /// base64 encode a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <returns>base64</returns>
    public static string Base64Encode(this string? str)
    {
        if (str.IsNull()) return string.Empty;
        return Encoding.UTF8.GetBytes(str).Base64Encode();
    }

    /// <summary>
    /// base64 decode a string
    /// </summary>
    /// <param name="base64EncodedString">base64 encoded string</param>
    /// <returns>decoded value</returns>
    public static string Base64Decode(this string? base64EncodedString)
    {
        if (base64EncodedString.IsNull()) return string.Empty;
        var bytes = Convert.FromBase64String(base64EncodedString);
        return Encoding.UTF8.GetString(bytes);
    }
    #endregion

    #region Url
    /// <summary>
    /// url encode a string
    /// </summary>
    /// <param name="str">string value</param>
    /// <returns>url encode string</returns>
    public static string UrlEncode(this string? str)
    {
        if (str.IsNull()) return string.Empty;
        return HttpUtility.UrlEncode(str);
    }

    /// <summary>
    /// url decode a string
    /// </summary>
    /// <param name="urlEncodedString">url encoded value</param>
    /// <returns>origin string</returns>
    public static string UrlDecode(this string? urlEncodedString)
    {
        if (urlEncodedString.IsNull()) return string.Empty;
        return HttpUtility.UrlDecode(urlEncodedString);
    }
    #endregion

    #region Base64Url
    /// <summary>
    /// base64 url encode a string
    /// </summary>
    /// <param name="str">string to encode</param>
    /// <returns>base64 url encoded string</returns>
    public static string Base64UrlEncode(this string? str)
    {
        if (str.IsNull()) return string.Empty;
        return str.Base64Encode().Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');
    }

    /// <summary>
    /// base64 url encode byte array
    /// </summary>
    /// <param name="bytes">byte array to encode</param>
    /// <returns>base64 url encoded string</returns>
    public static string Base64UrlEncode(this byte[]? bytes) => bytes.IsNull() ? string.Empty : bytes.Base64Encode().Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');

    /// <summary>
    /// base64 url decode a string
    /// </summary>
    /// <param name="base64UrlEncodedString">base64 url encoded value</param>
    /// <returns>origin string</returns>
    /// <exception cref="FormatException"></exception>
    public static string Base64UrlDecode(this string? base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNull()) return string.Empty;
        var bytes = base64UrlEncodedString.Base64UrlDecodeBytes();
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// base64 url decode a string
    /// </summary>
    /// <param name="base64UrlEncodedString">base64 url encoded value</param>
    /// <returns>origin bytes</returns>
    /// <exception cref="FormatException"></exception>
    public static byte[] Base64UrlDecodeBytes(this string? base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNull()) return Array.Empty<byte>();
        base64UrlEncodedString = base64UrlEncodedString.Replace('-', '+').Replace('_', '/');
        var lengthFormat = base64UrlEncodedString.Length % 4;
        base64UrlEncodedString += lengthFormat switch
        {
            1 => throw new FormatException("illegal base64url encoded string."),
            2 => "==",
            3 => "=",
            _ => string.Empty
        };
        return Convert.FromBase64String(base64UrlEncodedString);
    }
    #endregion
}
