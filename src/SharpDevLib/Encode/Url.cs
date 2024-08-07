using System.Web;

namespace SharpDevLib;

/// <summary>
/// Url编码扩展
/// </summary>
[BelongDirectory("Encode")]
public static class Url
{
    /// <summary>
    /// url编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>编码后的字符串</returns>
    public static string UrlEncode(this byte[] bytes) => HttpUtility.UrlEncode(bytes);

    /// <summary>
    /// url解码
    /// </summary>
    /// <param name="urlEncodedString">url编码的字符串</param>
    /// <returns>原始字符串</returns>
    public static byte[] UrlDecode(this string urlEncodedString) => HttpUtility.UrlDecodeToBytes(urlEncodedString);
}
