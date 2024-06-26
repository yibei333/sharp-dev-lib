﻿using SharpDevLib.Cryptography.OpenSSL;
using System.Diagnostics;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

/// <summary>
/// RSA密钥对扩展
/// </summary>
public static class RsaKeyExtension
{
    #region Common
    /// <summary>
    /// 导入Pem格式的密钥
    /// </summary>
    /// <param name="rsa">rsa algorithm</param>
    /// <param name="pem">Pem格式的密钥,支持的格式如下:
    /// <para>(1)PKCS#1私钥</para> 
    /// <para>(2)受密码保护的PKCS#1私钥</para> 
    /// <para>(3)PKCS#1公钥</para> 
    /// <para>(4)PKCS#8私钥</para> 
    /// <para>(5)受密码保护的PKCS#8私钥</para> 
    /// <para>(6)X.509SubjectPublicKey</para> 
    /// </param>
    /// <param name="password">密码（仅当PEM格式为受密码保护的私钥时适用）</param>
    public static void ImportPem(this RSA rsa, string pem, byte[]? password = null)
    {
        var pemObject = PemObject.Read(pem);
        if (pemObject.PemType == RsaPemType.UnKnown) throw new NotSupportedException("not supported pem format");
        if (pemObject.PemType == RsaPemType.Pkcs1PrivateKey)
        {
            rsa.ImportPkcs1PrivateKeyPem(pem);
        }
        else if (pemObject.PemType == RsaPemType.EncryptedPkcs1PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            rsa.ImportEncryptedPkcs1PrivateKeyPem(pem, password);
        }
        else if (pemObject.PemType == RsaPemType.PublicKey)
        {
            rsa.ImportPublicKeyPem(pem);
        }
        else if (pemObject.PemType == RsaPemType.Pkcs8PrivateKey)
        {
            rsa.ImportPkcs8PrivateKeyPem(pem);
        }
        else if (pemObject.PemType == RsaPemType.EncryptedPkcs8PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            rsa.ImportEncryptedPkcs8PrivateKeyPem(pem, password);
        }
        else if (pemObject.PemType == RsaPemType.X509SubjectPublicKey)
        {
            rsa.ImportX509SubjectPublicKeyPem(pem);
        }
        else throw new NotImplementedException();
    }

    /// <summary>
    /// 导出PEM格式的密钥
    /// </summary>
    /// <param name="rsa">rsa algorithm</param>
    /// <param name="pemType">PEM格式类型</param>
    /// <param name="password">密码(仅当PEM格式为受密码保护的私钥时适用)</param>
    /// <param name="encryptPkcs1PrivateKeyAlogorithm">加密算法(仅当PEM格式为受密码保护的PKCS#1私钥时适用),受支持的算法如下:
    /// <para>(1)AES-256-CBC</para> 
    /// <para>(2)DES-EDE3-CBC</para> 
    /// </param>
    /// <returns>PEM格式的密钥</returns>
    public static string ExportPem(this RSA rsa, RsaPemType pemType, byte[]? password = null, string encryptPkcs1PrivateKeyAlogorithm = "AES-256-CBC")
    {
        if (pemType == RsaPemType.UnKnown) throw new NotSupportedException("not supported pem format");

        if (pemType == RsaPemType.Pkcs1PrivateKey)
        {
            return rsa.ExportPkcs1PrivateKeyPem();
        }
        else if (pemType == RsaPemType.EncryptedPkcs1PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            return rsa.ExportEncryptedPkcs1PrivateKeyPem(password, encryptPkcs1PrivateKeyAlogorithm);
        }
        else if (pemType == RsaPemType.PublicKey)
        {
            return rsa.ExportPublicKeyPem();
        }
        else if (pemType == RsaPemType.Pkcs8PrivateKey)
        {
            return rsa.ExportPkcs8PrivateKeyPem();
        }
        else if (pemType == RsaPemType.EncryptedPkcs8PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            return rsa.ExportEncryptedPkcs8PrivateKeyPem(password);
        }
        else if (pemType == RsaPemType.X509SubjectPublicKey)
        {
            return rsa.ExportX509SubjectPublicKeyPem();
        }
        else throw new NotImplementedException();
    }

    /// <summary>
    /// 密钥对是否匹配
    /// </summary>
    /// <param name="privatePem">PEM格式的私钥</param>
    /// <param name="publicPem">PEM格式的公钥</param>
    /// <returns>是否匹配</returns>
    public static bool IsKeyPairMatch(string privatePem, string publicPem)
    {
        try
        {
            var pemObject = PemObject.Read(publicPem);
            if (pemObject.PemType != RsaPemType.PublicKey && pemObject.PemType != RsaPemType.X509SubjectPublicKey) return false;

            using var rsa = RSA.Create();
            rsa.ImportPem(privatePem);
            var exportPublicKey = rsa.ExportPem(pemObject.PemType);
            return publicPem == exportPublicKey;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
            return false;
        }
    }

    /// <summary>
    /// 获取密钥信息
    /// </summary>
    /// <param name="key">PEM格式的密钥</param>
    /// <param name="password">密码（仅当PEM格式为受密码保护的私钥时适用）</param>
    /// <returns>密钥信息</returns>
    public static RsaKeyInfo GetKeyInfo(string key, byte[]? password = null)
    {
        var pemObject = PemObject.Read(key);
        var isEncrypted = pemObject.PemType == RsaPemType.EncryptedPkcs1PrivateKey || pemObject.PemType == RsaPemType.EncryptedPkcs8PrivateKey;
        var isPrivate = pemObject.PemType != RsaPemType.PublicKey && pemObject.PemType != RsaPemType.X509SubjectPublicKey;
        if (isEncrypted && password.IsNullOrEmpty()) return new RsaKeyInfo(pemObject.PemType, 0, isPrivate, isEncrypted, null);

        using var rsa = RSA.Create();
        rsa.ImportPem(key, password);
        return new RsaKeyInfo(pemObject.PemType, rsa.KeySize, isPrivate, isEncrypted, rsa.ExportParameters(isPrivate));
    }
    #endregion

    #region Pkcs1
    private static string ExportPkcs1PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, RsaPemType.Pkcs1PrivateKey);
        return pemObject.Write();
    }

    private static string ExportEncryptedPkcs1PrivateKeyPem(this RSA rsa, byte[] password, string algorithm = "AES-256-CBC")
    {
        var iv = GenterateRandomIVByAlgorithm(algorithm);
        var fields = new PemHeaderFields("Proc-Type: 4,ENCRYPTED", $"DEK-Info: {algorithm},{iv.ToHexString()}");
        var derivedKey = OpenSSLRsa.DeriveKey(fields, password);
        var data = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
        var bytes = EncryptPkcs1Key(fields, data, derivedKey);
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, fields, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, RsaPemType.EncryptedPkcs1PrivateKey);
        return pemObject.Write();
    }

    private static void ImportPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != RsaPemType.Pkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs1 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        rsa.ImportPkcs1PrivateKeyPem(bytes);
    }

    private static void ImportEncryptedPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem, byte[] password)
    {
        if (password.IsNullOrEmpty()) throw new Exception("password required");
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != RsaPemType.EncryptedPkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs1 private key");
        if (pemObject.HeaderFields is null || pemObject.HeaderFields.DEKInfoAlgorithmFileds.IsNullOrEmpty()) throw new InvalidDataException("error DEK-INFO");

        var derivedKey = OpenSSLRsa.DeriveKey(pemObject.HeaderFields, password);
        var decryptedKey = DecryptPkcs1Key(pemObject.HeaderFields, Convert.FromBase64String(pemObject.Body), derivedKey);
        rsa.ImportPkcs1PrivateKeyPem(decryptedKey);
    }

    private static byte[] GenterateRandomIVByAlgorithm(string algorithm)
    {
        int ivLength;
        if (algorithm == "AES-256-CBC") ivLength = 16;
        else if (algorithm == "DES-EDE3-CBC") ivLength = 8;
        else throw new NotSupportedException($"algorithm '{algorithm}' not supported yet");

        var iv = new byte[ivLength];
        new Random().NextBytes(iv);
        return iv;
    }

    private static void ImportPkcs1PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        var parameter = Pkcs1.DecodePrivateKey(bytes);
        rsa.ImportParameters(parameter);
    }

    private static byte[] DecryptPkcs1Key(PemHeaderFields fields, byte[] data, byte[] key)
    {
        using var algorithm = GetSymmetricAlgorithmByFields(fields);
        using var transform = algorithm.CreateDecryptor(key, fields.DEKInfoIVBytes);
        return transform.TransformFinalBlock(data, 0, data.Length);
    }

    private static byte[] EncryptPkcs1Key(PemHeaderFields fields, byte[] data, byte[] key)
    {
        using var algorithm = GetSymmetricAlgorithmByFields(fields);
        using var transform = algorithm.CreateEncryptor(key, fields.DEKInfoIVBytes);
        return transform.TransformFinalBlock(data, 0, data.Length);
    }

    private static SymmetricAlgorithm GetSymmetricAlgorithmByFields(PemHeaderFields fields)
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
    private static string ExportPkcs8PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs8.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs8PrivateEnd, RsaPemType.Pkcs8PrivateKey);
        return pemObject.Write();
    }

    private static string ExportEncryptedPkcs8PrivateKeyPem(this RSA rsa, byte[] password)
    {
        var bytes = Pkcs8.EncodeEncryptedPrivateKey(rsa.ExportParameters(true), password);
        var pemObject = new PemObject(PemStatics.RsaEncryptedPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaEncryptedPkcs8PrivateEnd, RsaPemType.EncryptedPkcs8PrivateKey);
        return pemObject.Write();
    }

    private static void ImportPkcs8PrivateKeyPem(this RSA rsa, string pkcs8PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs8PrivateKeyPem);
        if (pemObject.PemType != RsaPemType.Pkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        ImportPkcs8PrivateKeyPem(rsa, bytes);
    }

    private static void ImportEncryptedPkcs8PrivateKeyPem(this RSA rsa, string encryptedPkcs8PrivateKeyPem, byte[] password)
    {
        var pemObject = PemObject.Read(encryptedPkcs8PrivateKeyPem);
        if (pemObject.PemType != RsaPemType.EncryptedPkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        var key = Pkcs8.DecodeEncryptedPrivateKeyInfo(bytes, password);
        ImportPkcs8PrivateKeyPem(rsa, key);
    }

    private static void ImportPkcs8PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        var key = Pkcs8.DecodePrivateKeyInfo(bytes);
        rsa.ImportPkcs1PrivateKeyPem(key);
    }
    #endregion

    #region Public Key
    private static string ExportPublicKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.EncodePublicKey(rsa.ExportParameters(false));
        var pemObject = new PemObject(PemStatics.RsaPublicStart, Convert.ToBase64String(bytes), PemStatics.RsaPublicEnd, RsaPemType.PublicKey);
        return pemObject.Write();
    }

    private static string ExportX509SubjectPublicKeyPem(this RSA rsa)
    {
        var bytes = X509.EncodeSubjectPublicKeyInfo(rsa.ExportParameters(false));
        var pemObject = new PemObject(PemStatics.RsaX509SubjectPublicStart, Convert.ToBase64String(bytes), PemStatics.RsaX509SubjectPublicEnd, RsaPemType.X509SubjectPublicKey);
        return pemObject.Write();
    }

    private static void ImportPublicKeyPem(this RSA rsa, string publicKeyPem)
    {
        var pemObject = PemObject.Read(publicKeyPem);
        if (pemObject.PemType != RsaPemType.PublicKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not rsa public key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        var parameters = Pkcs1.DecodePublicKey(bytes);
        rsa.ImportParameters(parameters);
    }

    private static void ImportX509SubjectPublicKeyPem(this RSA rsa, string x509SubjectPublicKeyPem)
    {
        var pemObject = PemObject.Read(x509SubjectPublicKeyPem);
        if (pemObject.PemType != RsaPemType.X509SubjectPublicKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not x509 subject public key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        var parameters = X509.DecodeSubjectPublicInfo(bytes);
        rsa.ImportParameters(parameters);
    }
    #endregion
}

/// <summary>
/// RSA密钥信息
/// </summary>
public class RsaKeyInfo
{
    internal RsaKeyInfo(RsaPemType type, int keySize, bool isPrivate, bool isEncrypted, RSAParameters? parameters)
    {
        Type = type;
        IsPrivate = isPrivate;
        IsEncrypted = isEncrypted;
        KeySize = keySize;
        Parameters = parameters is null ? null : new RsaKeyParameters(parameters.Value);
    }

    /// <summary>
    /// 类型
    /// </summary>
    public RsaPemType Type { get; }

    /// <summary>
    /// 是否是私钥
    /// </summary>
    public bool IsPrivate { get; }

    /// <summary>
    /// 是否受密码保护
    /// </summary>
    public bool IsEncrypted { get; }

    /// <summary>
    /// 密钥长度
    /// </summary>
    public int KeySize { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public RsaKeyParameters? Parameters { get; }
}

/// <summary>
/// RSA密钥参数
/// </summary>
public class RsaKeyParameters
{
    internal RsaKeyParameters(RSAParameters parameters)
    {
        Modulus = parameters.Modulus.ToHexString();
        Exponent = parameters.Exponent.ToHexString();
        D = parameters.D.IsNullOrEmpty() ? null : parameters.D.ToHexString();
        P = parameters.D.IsNullOrEmpty() ? null : parameters.P.ToHexString();
        DP = parameters.D.IsNullOrEmpty() ? null : parameters.DP.ToHexString();
        DQ = parameters.D.IsNullOrEmpty() ? null : parameters.DQ.ToHexString();
        InverseQ = parameters.D.IsNullOrEmpty() ? null : parameters.InverseQ.ToHexString();
    }

    /// <summary>
    /// Modulus参数
    /// </summary>
    public string Modulus { get; }

    /// <summary>
    /// Exponent参数
    /// </summary>
    public string Exponent { get; }

    /// <summary>
    /// D参数
    /// </summary>
    public string? D { get; }

    /// <summary>
    /// P参数
    /// </summary>
    public string? P { get; }

    /// <summary>
    /// DP参数
    /// </summary>
    public string? DP { get; }

    /// <summary>
    /// DQ参数
    /// </summary>
    public string? DQ { get; }

    /// <summary>
    /// InverseQ参数
    /// </summary>
    public string? InverseQ { get; }
}