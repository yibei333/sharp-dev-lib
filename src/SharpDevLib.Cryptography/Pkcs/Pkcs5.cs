using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// Pkcs5,rfc8018
/// </summary>
internal static class Pkcs5
{
    /// <summary>
    /// PBKDF1
    /// </summary>
    /// <param name="hashAlgorithm">hashAlgorithm</param>
    /// <param name="password">password</param>
    /// <param name="salt">salt</param>
    /// <param name="iterationCount">iterationCount</param>
    /// <param name="dkLen">dkLen</param>
    /// <returns>derived key</returns>
    public static byte[] PBKDF1(HashAlgorithm hashAlgorithm, byte[] password, byte[] salt, int iterationCount, int dkLen)
    {
        VerifyPBKDF1Parameters(hashAlgorithm, iterationCount, dkLen, out var hLen);
        var hash = new byte[hLen];

        //T_1 = Hash (P || S) ,
        //T_2 = Hash (T_1) ,
        //...
        //T_c = Hash (T_{c-1}) ,
        //DK = T_c<0..dkLen-1>
        for (int i = 0; i < iterationCount; i++)
        {
            if (i == 0) hash = hashAlgorithm.ComputeHash(password.Concat(salt).ToArray());
            else hash = hashAlgorithm.ComputeHash(hash.Concat(password).Concat(salt).ToArray());
        }
        return hash.Take(dkLen).ToArray();
    }

    static void VerifyPBKDF1Parameters(HashAlgorithm hashAlgorithm, int iterationCount, int dkLen, out int hLen)
    {
        if (iterationCount < 1) throw new ArgumentException($"iteration count({iterationCount}) error");

        if (hashAlgorithm is MD5)
        {
            hLen = 16;
            if (dkLen > hLen) throw new Exception("derived key too long");
            return;
        }
        else if (hashAlgorithm is SHA1)
        {
            hLen = 20;
            if (dkLen > hLen) throw new Exception("derived key too long");
            return;
        }
        //have no md2 implemention
        else throw new Exception($"algorithm only support:{nameof(MD5)},{nameof(SHA1)}");
    }

    /// <summary>
    /// PBKDF2
    /// </summary>
    /// <param name="hashAlgorithm">hashAlgorithm</param>
    /// <param name="salt">salt</param>
    /// <param name="iterationCount">iterationCount</param>
    /// <param name="dkLen">dkLen</param>
    /// <returns>derived key</returns>
    public static byte[] PBKDF2(HashAlgorithm hashAlgorithm, byte[] salt, int iterationCount, int dkLen)
    {
        VerifyPBKDF2Parameters(hashAlgorithm, iterationCount, dkLen, out var hLen);

        var key = new byte[dkLen];
        var l = (int)Math.Ceiling(dkLen * 1.0 / hLen);
        var r = dkLen - (l - 1) * hLen;

        for (int i = 1; i <= l; i++)
        {
            var tempKey = hashAlgorithm.PBKDF2PRF(salt, iterationCount, i, hLen);
            tempKey.CopyTo(key, (i - 1) * hLen, r == hLen ? hLen : r);
        }

        return key;
    }

    static void VerifyPBKDF2Parameters(HashAlgorithm hashAlgorithm, int iterationCount, int dkLen, out int hLen)
    {
        if (iterationCount < 1) throw new ArgumentException($"iteration count({iterationCount}) error");

        if (hashAlgorithm is HMACSHA1) hLen = 16;
        else if (hashAlgorithm is HMACSHA256) hLen = 32;
        else if (hashAlgorithm is HMACSHA384) hLen = 48;
        else if (hashAlgorithm is HMACSHA512) hLen = 64;
        else throw new Exception($"algorithm only support:{nameof(HMACSHA1)},{nameof(HMACSHA256)},{nameof(HMACSHA384)},{nameof(HMACSHA512)}");
        if (dkLen > ((Math.Pow(2, 32) - 1) * hLen)) throw new Exception("derived key too long");
    }

    static byte[] PBKDF2PRF(this HashAlgorithm hashAlgorithm, byte[] salt, int iterationCount, int i, int hLen)
    {
        var key = new byte[hLen];
        var lastKey = new byte[hLen];

        for (int j = 0; j < iterationCount; j++)
        {
            if (j == 0)
            {
                lastKey = hashAlgorithm.ComputeHash(salt.Concat(BitConverter.GetBytes(i).Reverse()).ToArray());
                lastKey.CopyTo(key, 0, key.Length);
            }
            else
            {
                lastKey = hashAlgorithm.ComputeHash(lastKey);
                key.Xor(lastKey);
            }
        }
        return key;
    }

    static void CopyTo(this byte[] source, byte[] target, int targetStartIndex, int targetLength)
    {
        for (int i = 0; i < targetLength; i++)
        {
            target[targetStartIndex + i] = source[i];
        }
    }

    static void Xor(this byte[] source, byte[] target)
    {
        for (int i = 0; i < source.Length; i++)
        {
            source[i] ^= target[i];
        }
    }
}
