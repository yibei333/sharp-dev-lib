namespace SharpDevLib;

/// <summary>
/// Base64 URL编码和解码扩展，提供Base64 URL安全格式的编码与解码功能
/// <para>Base64 URL编码替换了标准Base64中的+和/字符，使其可以安全地在URL中使用</para>
/// </summary>
public static class Base64UrlEncodeHelper
{
    /// <summary>
    /// 将字节数组编码为Base64 URL安全字符串
    /// <para>编码规则：</para>
    /// <para>1.将+替换为-</para>
    /// <para>2.将/替换为_</para>
    /// <para>3.移除末尾的=填充字符</para>
    /// </summary>
    /// <param name="bytes">要编码的字节数组</param>
    /// <returns>Base64 URL安全编码后的字符串</returns>
    public static string Base64UrlEncode(this byte[] bytes) => bytes.Base64Encode().Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');

    /// <summary>
    /// 将Base64 URL安全字符串解码为原始字节数组
    /// <para>解码规则：</para>
    /// <para>1.将-替换为+</para>
    /// <para>2.将_替换为/</para>
    /// <para>3.根据长度补充=填充字符</para>
    /// </summary>
    /// <param name="base64UrlEncodedString">Base64 URL安全编码的字符串</param>
    /// <returns>解码后的原始字节数组</returns>
    /// <exception cref="InvalidDataException">当字符串长度不符合Base64 URL格式或解码失败时抛出</exception>
    public static byte[] Base64UrlDecode(this string base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNullOrWhiteSpace()) return [];
        base64UrlEncodedString = base64UrlEncodedString.Replace('-', '+').Replace('_', '/');
        var lengthFormat = base64UrlEncodedString.Length % 4;
        base64UrlEncodedString += lengthFormat switch
        {
            1 => throw new InvalidDataException("illegal base64url encoded string."),
            2 => "==",
            3 => "=",
            _ => string.Empty
        };
        return Convert.FromBase64String(base64UrlEncodedString);
    }
}
