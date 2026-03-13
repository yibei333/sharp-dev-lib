using System.Net;

namespace SharpDevLib;

/// <summary>
/// URL编码和解码扩展，提供URL格式的编码与解码功能
/// </summary>
public static class UrlEncodeHelper
{
    /// <summary>
    /// URL编码
    /// </summary>
    /// <param name="url">要编码的字符串</param>
    /// <returns>URL编码后的字符串</returns>
    public static string UrlEncode(this string url) => WebUtility.UrlEncode(url);

    /// <summary>
    /// URL解码
    /// </summary>
    /// <param name="urlEncodedString">URL编码的字符串</param>
    /// <returns>解码后的原始字符串</returns>
    public static string UrlDecode(this string urlEncodedString) => WebUtility.UrlDecode(urlEncodedString);
}
