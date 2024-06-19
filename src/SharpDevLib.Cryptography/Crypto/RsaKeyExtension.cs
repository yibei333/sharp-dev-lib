using System.Formats.Asn1;
using System.Security.Cryptography;

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
        var parameter = Pkcs1.Decode(bytes);
        rsa.ImportParameters(parameter);
    }

    public static string ExportPkcs1PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.Encode(rsa.ExportParameters(true));
        var pemObject = new PemObject(new PemHeader(PemStatics.RsaPkcs1PrivateStart, null, null), Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaPkcs1PrivateKey);
        return pemObject.Write();
    }
    #endregion

    #region Pkcs8
    public static void ImportPkcs8PrivateKeyPem(this RSA rsa, string pkcs8PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs8PrivateKeyPem);
        if (pemObject.PemType != PemType.RsaPkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        ImportPkcs8PrivateKeyPem(rsa, bytes);
    }

    public static void ImportEncryptedPkcs8PrivateKeyPem(this RSA rsa, string encryptedPkcs8PrivateKeyPem, byte[] password)
    {
        var pemObject = PemObject.Read(encryptedPkcs8PrivateKeyPem);
        if (pemObject.PemType != PemType.RsaEncryptedPkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        var reader = new AsnReader(bytes, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence(Asn1Tag.Sequence);
        var parameterSequence = sequence.ReadSequence(Asn1Tag.Sequence);
        var encryptedData = sequence.ReadOctetString();

        var pkcs5pbes2Identifier = parameterSequence.ReadObjectIdentifier();//pkcs5 pbes2
        if (pkcs5pbes2Identifier != "1.2.840.113549.1.5.13") throw new NotSupportedException();
        var pbes2Sequence = parameterSequence.ReadSequence();

        var hashSequence = pbes2Sequence.ReadSequence(Asn1Tag.Sequence);
        var hashIdentifier = hashSequence.ReadObjectIdentifier();//pkcs5 PBKDF2
        if (hashIdentifier != "1.2.840.113549.1.5.12") throw new NotSupportedException();

        var pbkdf2ParameterSequence = hashSequence.ReadSequence();
        var salt = pbkdf2ParameterSequence.ReadOctetString();
        var iterationCount = pbkdf2ParameterSequence.ReadInteger();
        //var keyLength = pbkdf2ParameterSequence.TryReadInt32(out var length) ? length : 32;
        var prfSequence = pbkdf2ParameterSequence.ReadSequence();
        var prfIdentifier = prfSequence.ReadObjectIdentifier();
        if (prfIdentifier == "1.2.840.113549.2.9")//rfc4231,hmacWithSHA256
        {

        }
        else throw new NotSupportedException();


        var symmetricSequence = pbes2Sequence.ReadSequence();
        var symmetricIdentifier = symmetricSequence.ReadObjectIdentifier();
        if (symmetricIdentifier == "2.16.840.1.101.3.4.1.42")//rfc3565,id-aes256-CBC
        {

        }
        else throw new NotSupportedException();
        var iv = symmetricSequence.ReadOctetString();

        //get key
        var key = GetKey(password, salt, iterationCount);

        //aes decrypt
        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        var transform = aes.CreateDecryptor(key, iv);
        var decrypted = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        ImportPkcs8PrivateKeyPem(rsa, decrypted);
    }

    static byte[] GetKey(byte[] password, byte[] salt, System.Numerics.BigInteger iterationCount)
    {
        using var sha = new HMACSHA256(password);
        var key = sha.ComputeHash(salt.Concat(new byte[] { 0, 0, 0, 1 }).ToArray());
        var initKey = new byte[key.Length];
        key.CopyTo(initKey, 0);
        for (int i = 1; i < iterationCount; i++)
        {
            key = sha.ComputeHash(key);
            for (int j = 0; j < initKey.Length; j++)
            {
                initKey[j] ^= key[j];
            }
        }

        return initKey;
    }

    static void ImportPkcs8PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        //rfc5208
        var reader = new AsnReader(bytes, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence(Asn1Tag.Sequence);
        var version = sequence.ReadInteger();//version
        if (version != 0) throw new NotImplementedException($"only support rfc5208 structure");
        var identifierSequence = sequence.ReadSequence(Asn1Tag.Sequence);
        var identifier = identifierSequence.ReadObjectIdentifier();//rsa identifier,rfc8017
        if (identifier != "1.2.840.113549.1.1.1") throw new NotImplementedException($"identifier '{identifier}' not supported yet");
        var pkcs1Key = sequence.ReadOctetString();
        rsa.ImportPkcs1PrivateKeyPem(pkcs1Key);
    }
    #endregion
}
