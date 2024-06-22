using SharpDevLib.Cryptography.OpenSSL;
using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

public static class RsaKeyExtension
{
    #region Pkcs1
    public static string ExportPkcs1PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaPkcs1PrivateKey);
        return pemObject.Write();
    }

    public static string ExportEncryptedPkcs1PrivateKeyPem(this RSA rsa, byte[] password, string algorithm = "AES-256-CBC")
    {
        var iv = GenterateRandomIVByAlgorithm(algorithm);
        var fields = new PemHeaderFields("Proc-Type: 4,ENCRYPTED", $"DEK-Info: {algorithm},{iv.ToHexString()}");
        var derivedKey = OpenSSLRsa.DeriveKey(fields, password);
        var data = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
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
        var parameter = Pkcs1.DecodePrivateKey(bytes);
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
    public static string InternalExportPkcs8PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs8.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs8PrivateEnd, PemType.RsaPkcs8PrivateKey);
        return pemObject.Write();
    }

    public static string InternalExportEncryptedPkcs8PrivateKeyPem(this RSA rsa, byte[] password)
    {
        var bytes = Pkcs8.EncodeEncryptedPrivateKey(rsa.ExportParameters(true),password);
        var pemObject = new PemObject(PemStatics.RsaEncryptedPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaEncryptedPkcs8PrivateEnd, PemType.RsaEncryptedPkcs8PrivateKey);
        return pemObject.Write();
    }

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

        var key = Pkcs8.DecodeEncryptedPrivateKeyInfo(bytes, password);
        ImportPkcs8PrivateKeyPem(rsa, key);
    }

    static void ImportPkcs8PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        var key = Pkcs8.DecodePrivateKeyInfo(bytes);
        rsa.ImportPkcs1PrivateKeyPem(key);
    }
    #endregion
}
