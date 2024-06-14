using SharpDevLib.Cryptography.References;

namespace SharpDevLib.Cryptography;

internal static class InternalExtension
{
    internal static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    internal static bool NotNullOrEmpty([NotNullWhen(false)] this string? str) => !string.IsNullOrEmpty(str);

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
}