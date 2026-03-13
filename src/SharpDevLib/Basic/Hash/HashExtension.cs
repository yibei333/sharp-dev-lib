using System.Security.Cryptography;

namespace SharpDevLib;

internal static class HashExtension
{
    public static HashAlgorithm GetHashAlgorithm(string algorithmName)
    {
        return algorithmName switch
        {
            nameof(MD5) => MD5.Create(),
            nameof(SHA1) => SHA1.Create(),
            nameof(SHA256) => SHA256.Create(),
            nameof(SHA384) => SHA384.Create(),
            nameof(SHA512) => SHA512.Create(),
            _ => throw new NotSupportedException()
        };
    }

    public static string Hash(string algorithmName, byte[] bytes)
    {
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(bytes).HexStringEncode();
    }

    public static string Hash(string algorithmName, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(stream).HexStringEncode();
    }

    public static HashAlgorithm GetHMacHashAlgorithm(string algorithmName, byte[] secret)
    {
        return algorithmName switch
        {
            nameof(HMACMD5) => new HMACMD5(secret),
            nameof(HMACSHA1) => new HMACSHA1(secret),
            nameof(HMACSHA256) => new HMACSHA256(secret),
            nameof(HMACSHA384) => new HMACSHA384(secret),
            nameof(HMACSHA512) => new HMACSHA512(secret),
            _ => throw new NotSupportedException()
        };
    }

    public static string HMacHash(string algorithmName, byte[] secret, byte[] bytes)
    {
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(bytes).HexStringEncode();
    }

    public static string HMacHash(string algorithmName, byte[] secret, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(stream).HexStringEncode();
    }
}
