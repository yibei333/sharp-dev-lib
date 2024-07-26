using System.Text;

namespace SharpDevLib;

/// <summary>
/// Utf8编码扩展
/// </summary>
internal static class Utf8
{
    /// <summary>
    /// 将字节数组转换为UTF8编码的字符串
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>UTF8编码的字符串</returns>
    public static string Utf8Encode(this byte[] bytes) => Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// 将字符串转换为UTF8编码的字节数组
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>UTF8编码的字节数组</returns>
    public static byte[] Utf8Decode(this string str) => Encoding.UTF8.GetBytes(str);
}
