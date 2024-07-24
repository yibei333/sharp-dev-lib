using System.Security.Cryptography;

namespace SharpDevLib.Hash;

internal static class HashExtension
{
    public static HashAlgorithm GetHashAlgorithm(string algorithmName)
    {
        if (algorithmName.Equals(nameof(MD5))) return MD5.Create();
        if (algorithmName.Equals(nameof(SHA1))) return SHA1.Create();
        if (algorithmName.Equals(nameof(SHA256))) return SHA256.Create();
        if (algorithmName.Equals(nameof(SHA384))) return SHA384.Create();
        if (algorithmName.Equals(nameof(SHA512))) return SHA512.Create();
        throw new NotSupportedException();
    }

    public static string Hash(string algorithmName, byte[] bytes)
    {
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(bytes).ToHexString();
    }

    public static string Hash(string algorithmName, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHashAlgorithm(algorithmName);
        return algorithm.ComputeHash(stream).ToHexString();
    }

    public static HashAlgorithm GetHMacHashAlgorithm(string algorithmName, byte[] secret)
    {
        if (algorithmName.Equals(nameof(HMACMD5))) return new HMACMD5(secret);
        if (algorithmName.Equals(nameof(HMACSHA1))) return new HMACSHA1(secret);
        if (algorithmName.Equals(nameof(HMACSHA256))) return new HMACSHA256(secret);
        if (algorithmName.Equals(nameof(HMACSHA384))) return new HMACSHA384(secret);
        if (algorithmName.Equals(nameof(HMACSHA512))) return new HMACSHA512(secret);
        throw new NotSupportedException();
    }

    public static string HMacHash(string algorithmName, byte[] secret, byte[] bytes)
    {
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(bytes).ToHexString();
    }

    public static string HMacHash(string algorithmName, byte[] secret, Stream stream)
    {
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        using var algorithm = GetHMacHashAlgorithm(algorithmName, secret);
        return algorithm.ComputeHash(stream).ToHexString();
    }
}
