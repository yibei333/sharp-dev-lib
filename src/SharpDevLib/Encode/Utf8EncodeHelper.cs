using System.Text;

namespace SharpDevLib;

/// <summary>
/// UTF8编码和解码扩展，提供UTF8格式的编码与解码功能
/// </summary>
public static class Utf8EncodeHelper
{
    /// <summary>
    /// 将字节数组解码为UTF8字符串
    /// </summary>
    /// <param name="bytes">要解码的字节数组</param>
    /// <returns>解码后的UTF8字符串</returns>
    public static string Utf8Encode(this byte[] bytes) => Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// 将字符串编码为UTF8字节数组
    /// </summary>
    /// <param name="str">要编码的字符串</param>
    /// <returns>UTF8编码的字节数组</returns>
    public static byte[] Utf8Decode(this string str) => Encoding.UTF8.GetBytes(str);
}
