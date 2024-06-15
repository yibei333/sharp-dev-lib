using System.Security.Cryptography;
using System.Formats.Asn1;

namespace SharpDevLib.Cryptography;

public static class RsaKeyExtension
{
    #region Pkcs1
    public static void ImportPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != PemType.RsaPkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs1 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        rsa.ImportPkcs1PrivateKeyPem(bytes);
    }

    public static void ImportEncryptedPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem, byte[] password)
    {
        if (password.IsNullOrEmpty()) throw new Exception("password required");
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != PemType.RsaEncryptedPkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs1 private key");
        if (pemObject.Header.ProcType != "Proc-Type: 4,ENCRYPTED") throw new NotSupportedException($"Proc-Type with '{pemObject.Header.ProcType}' not supported yet");
        var dekInfo = pemObject.Header.DEKInfo?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
        if (dekInfo.Count != 2) throw new InvalidDataException("error DEKInfo");

        var ciphertext = Convert.FromBase64String(pemObject.Body);
        var algorithm = dekInfo.First().Split(':').Last().Trim();

        //rfc2898
        //https://stackoverflow.com/questions/77239925/decrypt-the-encrypted-private-key-in-pkcs-1-format
        if (algorithm == "AES-256-CBC")
        {
            var iv = dekInfo.Last().Trim().FromHexString();
            var salt = iv.Take(8).ToArray();
            var md5Key = password.Concat(salt).ToArray();
            using var md5 = MD5.Create();
            var hash1 = md5.ComputeHash(md5Key);
            var hash2 = md5.ComputeHash(hash1.Concat(md5Key).ToArray());//length not enough,hash again
            var key = hash1.Concat(hash2).ToArray();

            using var aes = Aes.Create();
            using var transform = aes.CreateDecryptor(key, iv);
            var decrypted = transform.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            rsa.ImportPkcs1PrivateKeyPem(decrypted);
        }
        else if (algorithm == "DES-EDE3-CBC")
        {
            var iv = dekInfo.Last().Trim().FromHexString();
            var salt = iv.Take(8).ToArray();
            var md5Key = password.Concat(salt).ToArray();
            using var md5 = MD5.Create();
            var hash1 = md5.ComputeHash(md5Key);
            var hash2 = md5.ComputeHash(hash1.Concat(md5Key).ToArray());//length not enough,hash again
            var key = hash1.Concat(hash2).Take(24).ToArray();

            using var tripleDES = TripleDES.Create();
            using var transform = tripleDES.CreateDecryptor(key, iv);
            var decrypted = transform.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            rsa.ImportPkcs1PrivateKeyPem(decrypted);
        }
        else
        {
            throw new NotSupportedException($"DEKInfo '{algorithm}' not supported yet");
        }
    }

    static void ImportPkcs1PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        //rfc8017
        var reader = new AsnReader(bytes, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence(Asn1Tag.Sequence);
        _ = sequence.ReadInteger();//version
        var module = sequence.ReadIntegerValue();
        var publicExponent = sequence.ReadIntegerValue();
        var privateExponent = sequence.ReadIntegerValue();
        var p = sequence.ReadIntegerValue();
        var q = sequence.ReadIntegerValue();
        var dp = sequence.ReadIntegerValue();
        var dq = sequence.ReadIntegerValue();
        var inverseQ = sequence.ReadIntegerValue();

        var parameter = new RSAParameters
        {
            Modulus = module,
            Exponent = publicExponent,
            D = privateExponent,
            P = p,
            Q = q,
            DP = dp,
            DQ = dq,
            InverseQ = inverseQ,
        };
        rsa.ImportParameters(parameter);
    }

    static byte[] ReadIntegerValue(this AsnReader reader)
    {
        var array = reader.ReadInteger().ToByteArray().Reverse();
        if (array.First() == 0x00) array = array.Skip(1);
        return array.ToArray();
    }

    #endregion
}
