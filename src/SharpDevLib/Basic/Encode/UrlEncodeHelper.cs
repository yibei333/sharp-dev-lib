using System.Web;

namespace SharpDevLib;

/// <summary>
/// URL编码和解码扩展，提供URL格式的编码与解码功能
/// </summary>
public static class UrlEncodeHelper
{
    /// <summary>
    /// 将字节数组编码为URL编码字符串
    /// </summary>
    /// <param name="bytes">要编码的字节数组</param>
    /// <returns>URL编码后的字符串</returns>
    public static string UrlEncode(this byte[] bytes) => HttpUtility.UrlEncode(bytes);

    /// <summary>
    /// 将URL编码字符串解码为原始字节数组
    /// </summary>
    /// <param name="urlEncodedString">URL编码的字符串</param>
    /// <returns>解码后的原始字节数组</returns>
    public static byte[] UrlDecode(this string urlEncodedString) => HttpUtility.UrlDecodeToBytes(urlEncodedString);
}
