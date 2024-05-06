using System.Text;
using System.Web;

namespace SharpDevLib.Standard;

/// <summary>
/// 编码扩展
/// </summary>
public static class EncodeExtension
{
    #region UTF8
    /// <summary>
    /// 将字节数组转换为UTF8编码的字符串
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>UTF8编码的字符串</returns>
    public static string ToUtf8String(this byte[]? bytes) => bytes.IsNullOrEmpty() ? string.Empty : Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// 将字符串转换为UTF8编码的字节数组
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>UTF8编码的字节数组</returns>
    public static byte[]? ToUtf8Bytes(this string? str) => str.IsNullOrWhiteSpace() ? null : Encoding.UTF8.GetBytes(str);
    #endregion

    #region HexString
    /// <summary>
    /// 将字节数组转换为16进制字符串
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>16进制字符串</returns>
    public static string ToHexString(this byte[]? bytes)
    {
        if (bytes.IsNullOrEmpty()) return string.Empty;
        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// 将16进制字符串转换为字节数组
    /// </summary>
    /// <param name="hexString">16进制字符串</param>
    /// <returns>原始字节数组</returns>
    public static byte[]? FromHexString(this string? hexString)
    {
        if (hexString.IsNullOrWhiteSpace()) return null;
        if ((hexString.Length % 2) != 0) throw new FormatException($"'{hexString}' is not a valid hex string");
        var list = new List<byte>();

        for (int i = 0; i < hexString.Length / 2; i++)
        {
            list.Add(Convert.ToByte(string.Join("", hexString.Skip(i * 2).Take(2)), 16));
        }
        return list.ToArray();
    }
    #endregion

    #region Base64
    /// <summary>
    /// base64编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>base64字符串</returns>
    public static string Base64Encode(this byte[]? bytes) => bytes.IsNullOrEmpty() ? string.Empty : Convert.ToBase64String(bytes);

    /// <summary>
    /// base64解码
    /// </summary>
    /// <param name="base64EncodedString">base64字符串</param>
    /// <returns>原始的字节数组</returns>
    public static byte[]? Base64Decode(this string? base64EncodedString) => base64EncodedString.IsNullOrWhiteSpace() ? null : Convert.FromBase64String(base64EncodedString);
    #endregion

    #region Url
    /// <summary>
    /// url编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>编码后的字符串</returns>
    public static string UrlEncode(this byte[]? bytes) => bytes.IsNullOrEmpty() ? string.Empty : HttpUtility.UrlEncode(bytes);

    /// <summary>
    /// url解码
    /// </summary>
    /// <param name="urlEncodedString">url编码的字符串</param>
    /// <returns>原始字符串</returns>
    public static byte[]? UrlDecode(this string? urlEncodedString) => urlEncodedString.IsNullOrWhiteSpace() ? null : HttpUtility.UrlDecodeToBytes(urlEncodedString);
    #endregion

    #region Base64Url
    /// <summary>
    /// base64 url编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>base64 url编码后的字符串</returns>
    public static string Base64UrlEncode(this byte[]? bytes) => bytes.IsNullOrEmpty() ? string.Empty : bytes.Base64Encode().Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');

    /// <summary>
    /// base64 url解码
    /// </summary>
    /// <param name="base64UrlEncodedString">base64 url编码的字符串</param>
    /// <returns>原始字节数组</returns>
    public static byte[]? Base64UrlDecode(this string? base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNullOrWhiteSpace()) return null;
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
