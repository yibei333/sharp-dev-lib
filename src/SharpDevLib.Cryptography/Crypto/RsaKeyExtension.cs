using SharpDevLib.Cryptography.OpenSSL;
using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

public static class RsaKeyExtension
{
    #region Pkcs1
    public static string ExportPkcs1PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.Encode(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaPkcs1PrivateKey);
        return pemObject.Write();
    }

    public static string ExportEncryptedPkcs1PrivateKeyPem(this RSA rsa, byte[] password, string algorithm = "AES-256-CBC")
    {
        var iv = GenterateRandomIVByAlgorithm(algorithm);
        var fields = new PemHeaderFields("Proc-Type: 4,ENCRYPTED", $"DEK-Info: {algorithm},{iv.ToHexString()}");
        var derivedKey = OpenSSLRsa.DeriveKey(fields, password);
        var data = Pkcs1.Encode(rsa.ExportParameters(true));
        var bytes = EncryptPkcs1Key(fields, data, derivedKey);
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, fields, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaEncryptedPkcs1PrivateKey);
        return pemObject.Write();
    }

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
        if (pemObject.HeaderFields is null || pemObject.HeaderFields.DEKInfoAlgorithmFileds.IsNullOrEmpty()) throw new InvalidDataException("error DEK-INFO");

        var derivedKey = OpenSSLRsa.DeriveKey(pemObject.HeaderFields, password);
        var decryptedKey = DecryptPkcs1Key(pemObject.HeaderFields, Convert.FromBase64String(pemObject.Body), derivedKey);
        rsa.ImportPkcs1PrivateKeyPem(decryptedKey);
    }

    static byte[] GenterateRandomIVByAlgorithm(string algorithm)
    {
        int ivLength;
        if (algorithm == "AES-256-CBC") ivLength = 16;
        else if (algorithm == "DES-EDE3-CBC") ivLength = 8;
        else throw new NotSupportedException($"algorithm '{algorithm}' not supported yet");

        var iv = new byte[ivLength];
        new Random().NextBytes(iv);
        return iv;
    }

    static void ImportPkcs1PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        var parameter = Pkcs1.Decode(bytes);
        rsa.ImportParameters(parameter);
    }

    static byte[] DecryptPkcs1Key(PemHeaderFields fields, byte[] data, byte[] key)
    {
        using var algorithm = GetSymmetricAlgorithmByFields(fields);
        using var transform = algorithm.CreateDecryptor(key, fields.DEKInfoIVBytes);
        return transform.TransformFinalBlock(data, 0, data.Length);
    }

    static byte[] EncryptPkcs1Key(PemHeaderFields fields, byte[] data, byte[] key)
    {
        using var algorithm = GetSymmetricAlgorithmByFields(fields);
        using var transform = algorithm.CreateEncryptor(key, fields.DEKInfoIVBytes);
        return transform.TransformFinalBlock(data, 0, data.Length);
    }

    static SymmetricAlgorithm GetSymmetricAlgorithmByFields(PemHeaderFields fields)
    {
        SymmetricAlgorithm algorithm;
        if (fields.DEKInfoAlgorithmFileds![0].Equals(nameof(Aes), StringComparison.OrdinalIgnoreCase))
        {
            var aes = Aes.Create();
            aes.KeySize = int.Parse(fields.DEKInfoAlgorithmFileds[1]);
            aes.Mode = (CipherMode)Enum.Parse(typeof(CipherMode), fields.DEKInfoAlgorithmFileds[2]);
            algorithm = aes;
        }
        else if (fields.DEKInfoAlgorithmFileds[0].Equals(nameof(DES), StringComparison.OrdinalIgnoreCase) && fields.DEKInfoAlgorithmFileds[1].Equals("EDE3", StringComparison.OrdinalIgnoreCase))
        {
            var tripleDES = TripleDES.Create();
            tripleDES.Mode = (CipherMode)Enum.Parse(typeof(CipherMode), fields.DEKInfoAlgorithmFileds[2]);
            algorithm = tripleDES;
        }
        else throw new NotSupportedException($"algorithm '{fields.DEKInfoAlgorithm}' not supported yet");
        return algorithm;
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
