namespace SharpDevLib;

/// <summary>
/// Base64编码和解码扩展，提供Base64格式的编码与解码功能
/// </summary>
public static class Base64EncodeHelper
{
    /// <summary>
    /// 将字节数组编码为Base64字符串
    /// </summary>
    /// <param name="bytes">要编码的字节数组</param>
    /// <returns>Base64编码后的字符串</returns>
    public static string Base64Encode(this byte[] bytes) => Convert.ToBase64String(bytes);

    /// <summary>
    /// 将Base64字符串解码为原始字节数组
    /// </summary>
    /// <param name="base64EncodedString">Base64编码的字符串</param>
    /// <returns>解码后的原始字节数组</returns>
    public static byte[] Base64Decode(this string base64EncodedString) => Convert.FromBase64String(base64EncodedString);
}
