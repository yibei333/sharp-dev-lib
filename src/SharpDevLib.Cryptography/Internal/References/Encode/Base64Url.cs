namespace SharpDevLib;

/// <summary>
/// Base64Url编码扩展
/// </summary>
internal static class Base64Url
{
    /// <summary>
    /// base64 url编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>base64 url编码后的字符串</returns>
    public static string Base64UrlEncode(this byte[] bytes) => bytes.Base64Encode().Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');

    /// <summary>
    /// base64 url解码
    /// </summary>
    /// <param name="base64UrlEncodedString">base64 url编码的字符串</param>
    /// <returns>原始字节数组</returns>
    /// <exception cref="InvalidDataException">当转换失败时引发异常</exception>
    public static byte[] Base64UrlDecode(this string base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNullOrWhiteSpace()) return Array.Empty<byte>();
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
