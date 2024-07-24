using SharpDevLib.Cryptography.Internal.Pkcs;
using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography.Internal.OpenSSL;

internal class OpenSSLRsa
{
    //https://www.openssl.org/docs/man1.1.1/man3/PEM_write_RSAPrivateKey.html
    //https://www.openssl.org/docs/manmaster/man3/EVP_BytesToKey.html
    public static byte[] DeriveKey(PemHeaderFields fields, byte[] password)
    {
        var hashAlgorithm = MD5.Create();
        int dkLen = GetDkLength(fields);
        int hLen = 16;//md5 digist length
        var salt = fields.DEKInfoIVBytes.Take(8).ToArray();//The salt parameter is used as a salt in the derivation: it should point to an 8 byte buffer or NULL if no salt is used

        //If the total key and IV length is less than the digest length and MD5 is used
        //then the derivation algorithm is compatible with PKCS#5 v1.5(PKCS#5 v2.0 PBKDF1 is compatible with the key derivation process in PKCS #5 v1.5)
        //otherwise a non standard extension is used to derive the extra data.
        if (dkLen < hLen && salt.Length < hLen)
        {
            return Pkcs5.PBKDF1(hashAlgorithm, password, salt, 1, dkLen);
        }
        else
        {
            //The key and IV is derived by concatenating D_1, D_2, etc until enough data is available for the key and IV. D_i is defined as:
            //D_i = HASH^count(D_(i-1) || data || salt)
            //where || denotes concatenation, D_0 is empty, HASH is the digest algorithm in use, HASH^1(data) is simply HASH(data), HASH^2(data) is HASH(HASH(data)) and so on.

            var roundingUp = (int)Math.Ceiling(dkLen * 1.0 / hLen);
            var derivedKey = new byte[roundingUp * hLen];
            var hash = new byte[hLen];

            for (int i = 0; i < roundingUp; i++)
            {
                if (i == 0) hash = hashAlgorithm.ComputeHash(password.Concat(salt).ToArray());
                else hash = hashAlgorithm.ComputeHash(hash.Concat(password).Concat(salt).ToArray());
                hash.CopyTo(derivedKey, i * hLen);
            }
            return derivedKey.Take(dkLen).ToArray();
        }
    }

    static int GetDkLength(PemHeaderFields fields)
    {
        if (fields.DEKInfoAlgorithmFileds![0].Equals(nameof(Aes), StringComparison.OrdinalIgnoreCase))
        {
            return int.Parse(fields.DEKInfoAlgorithmFileds[1]) / 8;
        }
        else if (fields.DEKInfoAlgorithmFileds![0].Equals(nameof(DES), StringComparison.OrdinalIgnoreCase))
        {
            if (fields.DEKInfoAlgorithmFileds[1].Equals("EDE3", StringComparison.OrdinalIgnoreCase)) return 24;
            else return 8;
        }
        throw new NotSupportedException();
    }
}
