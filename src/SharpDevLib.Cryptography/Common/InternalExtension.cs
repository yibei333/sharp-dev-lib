using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SharpDevLib.Cryptography;

internal static class InternalExtension
{
    internal static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    internal static bool NotNullOrEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);

    internal static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    internal static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);

    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source is null || source.Count() <= 0;

    public static bool NotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? source) => source is not null && source.Count() > 0;

    internal static byte[] FromHexString(this string hexString)
    {
        if (hexString.IsNullOrWhiteSpace()) return Array.Empty<byte>();
        if ((hexString.Length % 2) != 0) throw new FormatException($"'{hexString}' is not a valid hex string");
        var list = new List<byte>();

        for (int i = 0; i < hexString.Length / 2; i++)
        {
            list.Add(Convert.ToByte(string.Join("", hexString.Skip(i * 2).Take(2)), 16));
        }
        return list.ToArray();
    }

    const string LFTerminator = "\n";
    internal static void AppendLineWithLFTerminator(this StringBuilder builder, string? content)
    {
        if (content.NotNullOrWhiteSpace()) builder.Append(content);
        builder.Append(LFTerminator);
    }

    internal static void AppendLineWithLFTerminator(this StringBuilder builder)
    {
        builder.Append(LFTerminator);
    }

    internal static string ToHexString(this byte[] bytes)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    internal static string Base64UrlEncode(this byte[] bytes) => Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').TrimEnd('=').TrimEnd('=');

    internal static byte[] Base64UrlDecode(this string base64UrlEncodedString)
    {
        if (base64UrlEncodedString.IsNullOrWhiteSpace()) return Array.Empty<byte>();
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

    internal static byte[] ToUtf8Bytes(this string str) => Encoding.UTF8.GetBytes(str);

    internal static string ToUtf8String(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
}