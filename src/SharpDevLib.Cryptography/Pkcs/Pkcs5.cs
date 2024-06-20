using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

//rfc8018
internal class Pkcs5
{
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
}
