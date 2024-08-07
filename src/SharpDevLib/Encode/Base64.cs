namespace SharpDevLib;

/// <summary>
/// Base64编码扩展
/// </summary>
[BelongDirectory("Encode")]
public static class Base64
{
    /// <summary>
    /// base64编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>base64字符串</returns>
    public static string Base64Encode(this byte[] bytes) => Convert.ToBase64String(bytes);

    /// <summary>
    /// base64解码
    /// </summary>
    /// <param name="base64EncodedString">base64字符串</param>
    /// <returns>原始的字节数组</returns>
    public static byte[] Base64Decode(this string base64EncodedString) => Convert.FromBase64String(base64EncodedString);
}
