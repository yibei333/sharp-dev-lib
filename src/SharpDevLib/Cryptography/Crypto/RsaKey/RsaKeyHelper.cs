using SharpDevLib.Cryptography.Internal.OpenSSL;
using SharpDevLib.Cryptography.Internal.Pkcs;
using SharpDevLib.Cryptography.Pem;
using System.Security.Cryptography;

namespace SharpDevLib;

/// <summary>
/// RSA密钥对扩展，提供RSA密钥的导入、导出、匹配验证和密钥信息获取功能
/// </summary>
public static class RsaKeyHelper
{
    /// <summary>
    /// 将密钥体内容按64个字符换行
    /// </summary>
    /// <param name="keyBody">不带头尾标记的密钥体</param>
    /// <returns>格式化后的密钥字符串</returns>
    public static string WrapLineWith64Char(string keyBody) => PemObject.WrapLineWith64Char(keyBody);

    /// <summary>
    /// 删除密钥体中的换行符并去除首尾空白
    /// </summary>
    /// <param name="keyBody">不带头尾标记的密钥体</param>
    /// <returns>去除换行和空白后的密钥字符串</returns>
    public static string RemoveWrapLineAndTrim(string keyBody) => PemObject.RemoveWrapLineAndTrim(keyBody);

    /// <summary>
    /// 导入PEM格式的RSA密钥
    /// </summary>
    /// <param name="rsa">RSA算法实例</param>
    /// <param name="pem">PEM格式的密钥，支持的格式如下：
    /// <para>(1)PKCS#1私钥</para> 
    /// <para>(2)受密码保护的PKCS#1私钥</para> 
    /// <para>(3)PKCS#1公钥</para> 
    /// <para>(4)PKCS#8私钥</para> 
    /// <para>(5)受密码保护的PKCS#8私钥</para> 
    /// <para>(6)X.509 SubjectPublicKey</para> 
    /// </param>
    /// <param name="password">密码（仅当PEM格式为受密码保护的私钥时适用）</param>
    /// <exception cref="NotSupportedException">当解析出的PemType为UnKnown时引发异常</exception>
    /// <exception cref="ArgumentNullException">当pem为受密码保护且password参数为空时引发异常</exception>
    /// <exception cref="NotImplementedException">X509Certificate和X509CertificateSigningRequest格式未实现</exception>
    public static void ImportPem(this RSA rsa, string pem, byte[]? password = null)
    {
        var pemObject = PemObject.Read(pem);
        if (pemObject.PemType == PemType.UnKnown) throw new NotSupportedException("不支持的PEM格式");
        if (pemObject.PemType == PemType.Pkcs1PrivateKey)
        {
            rsa.ImportPkcs1PrivateKeyPem(pem);
        }
        else if (pemObject.PemType == PemType.EncryptedPkcs1PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            rsa.ImportEncryptedPkcs1PrivateKeyPem(pem, password);
        }
        else if (pemObject.PemType == PemType.PublicKey)
        {
            rsa.ImportPublicKeyPem(pem);
        }
        else if (pemObject.PemType == PemType.Pkcs8PrivateKey)
        {
            rsa.ImportPkcs8PrivateKeyPem(pem);
        }
        else if (pemObject.PemType == PemType.EncryptedPkcs8PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            rsa.ImportEncryptedPkcs8PrivateKeyPem(pem, password);
        }
        else if (pemObject.PemType == PemType.X509SubjectPublicKey)
        {
            rsa.ImportX509SubjectPublicKeyPem(pem);
        }
        else throw new NotImplementedException();
    }

    /// <summary>
    /// 导出PEM格式的RSA密钥
    /// </summary>
    /// <param name="rsa">RSA算法实例</param>
    /// <param name="pemType">PEM格式类型</param>
    /// <param name="password">密码（仅当PEM格式为受密码保护的私钥时适用）</param>
    /// <param name="encryptPkcs1PrivateKeyAlogorithm">加密算法（仅当PEM格式为受密码保护的PKCS#1私钥时适用），受支持的算法如下：
    /// <para>(1)AES-256-CBC</para> 
    /// <para>(2)DES-EDE3-CBC</para> 
    /// </param>
    /// <returns>PEM格式的密钥字符串</returns>
    /// <exception cref="NotSupportedException">当pemType为UnKnown时引发异常</exception>
    /// <exception cref="ArgumentNullException">当pemType为受密码保护且password参数为空时引发异常</exception>
    /// <exception cref="NotImplementedException">X509Certificate和X509CertificateSigningRequest格式未实现</exception>
    public static string ExportPem(this RSA rsa, PemType pemType, byte[]? password = null, string encryptPkcs1PrivateKeyAlogorithm = "AES-256-CBC")
    {
        if (pemType == PemType.UnKnown) throw new NotSupportedException("不支持的PEM格式");

        if (pemType == PemType.Pkcs1PrivateKey)
        {
            return rsa.ExportPkcs1PrivateKeyPem();
        }
        else if (pemType == PemType.EncryptedPkcs1PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            return rsa.ExportEncryptedPkcs1PrivateKeyPem(password, encryptPkcs1PrivateKeyAlogorithm);
        }
        else if (pemType == PemType.PublicKey)
        {
            return rsa.ExportPublicKeyPem();
        }
        else if (pemType == PemType.Pkcs8PrivateKey)
        {
            return rsa.ExportPkcs8PrivateKeyPem();
        }
        else if (pemType == PemType.EncryptedPkcs8PrivateKey)
        {
            if (password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(password));
            return rsa.ExportEncryptedPkcs8PrivateKeyPem(password);
        }
        else if (pemType == PemType.X509SubjectPublicKey)
        {
            return rsa.ExportX509SubjectPublicKeyPem();
        }
        else throw new NotImplementedException();
    }

    /// <summary>
    /// 验证私钥和公钥是否匹配
    /// </summary>
    /// <param name="privatePem">PEM格式的私钥</param>
    /// <param name="publicPem">PEM格式的公钥</param>
    /// <returns>如果密钥对匹配返回true，否则返回false</returns>
    public static bool IsKeyPairMatch(string privatePem, string publicPem)
    {
        try
        {
            var pemObject = PemObject.Read(publicPem);
            if (pemObject.PemType != PemType.PublicKey && pemObject.PemType != PemType.X509SubjectPublicKey) return false;

            using var rsa = RSA.Create();
            rsa.ImportPem(privatePem);
            var exportPublicKey = rsa.ExportPem(pemObject.PemType);
            return publicPem.RemoveLineBreak() == exportPublicKey.RemoveLineBreak();
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获取RSA密钥的详细信息
    /// </summary>
    /// <param name="key">PEM格式的密钥</param>
    /// <param name="password">密码（仅当PEM格式为受密码保护的私钥时适用）</param>
    /// <returns>RSA密钥信息对象</returns>
    public static RsaKeyInfo GetKeyInfo(string key, byte[]? password = null)
    {
        var pemObject = PemObject.Read(key);
        var isEncrypted = pemObject.PemType == PemType.EncryptedPkcs1PrivateKey || pemObject.PemType == PemType.EncryptedPkcs8PrivateKey;
        var isPrivate = pemObject.PemType != PemType.PublicKey && pemObject.PemType != PemType.X509SubjectPublicKey;
        if (isEncrypted && password.IsNullOrEmpty()) return new RsaKeyInfo(pemObject.PemType, 0, isPrivate, isEncrypted, null);

        using var rsa = RSA.Create();
        rsa.ImportPem(key, password);
        return new RsaKeyInfo(pemObject.PemType, rsa.KeySize, isPrivate, isEncrypted, rsa.ExportParameters(isPrivate));
    }

    #region Pkcs1
    static string ExportPkcs1PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.Pkcs1PrivateKey);
        return pemObject.Write();
    }

    static string ExportEncryptedPkcs1PrivateKeyPem(this RSA rsa, byte[] password, string algorithm = "AES-256-CBC")
    {
        var iv = GenterateRandomIVByAlgorithm(algorithm);
        var fields = new PemHeaderFields("Proc-Type: 4,ENCRYPTED", $"DEK-Info: {algorithm},{iv.HexStringEncode()}");
        var derivedKey = OpenSSLRsa.DeriveKey(fields, password);
        var data = Pkcs1.EncodePrivateKey(rsa.ExportParameters(true));
        var bytes = EncryptPkcs1Key(fields, data, derivedKey);
        var pemObject = new PemObject(PemStatics.RsaPkcs1PrivateStart, fields, Convert.ToBase64String(bytes), PemStatics.RsaPkcs1PrivateEnd, PemType.EncryptedPkcs1PrivateKey);
        return pemObject.Write();
    }

    static void ImportPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != PemType.Pkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs1 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        rsa.ImportPkcs1PrivateKeyPem(bytes);
    }

    static void ImportEncryptedPkcs1PrivateKeyPem(this RSA rsa, string pkcs1PrivateKeyPem, byte[] password)
    {
        if (password.IsNullOrEmpty()) throw new Exception("密码不能为空");
        var pemObject = PemObject.Read(pkcs1PrivateKeyPem);
        if (pemObject.PemType != PemType.EncryptedPkcs1PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs1 private key");
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
        else throw new NotSupportedException($"暂不支持的算法: '{algorithm}'");

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
        else throw new NotSupportedException($"暂不支持的算法: '{fields.DEKInfoAlgorithm}'");
        return algorithm;
    }
    #endregion

    #region Pkcs8
    static string ExportPkcs8PrivateKeyPem(this RSA rsa)
    {
        var bytes = Pkcs8.EncodePrivateKey(rsa.ExportParameters(true));
        var pemObject = new PemObject(PemStatics.RsaPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaPkcs8PrivateEnd, PemType.Pkcs8PrivateKey);
        return pemObject.Write();
    }

    static string ExportEncryptedPkcs8PrivateKeyPem(this RSA rsa, byte[] password)
    {
        var privateKey = Pkcs8.EncodePrivateKey(rsa.ExportParameters(true));
        var bytes = Pkcs8.EncodeEncryptedPrivateKey(privateKey, password);
        var pemObject = new PemObject(PemStatics.RsaEncryptedPkcs8PrivateStart, Convert.ToBase64String(bytes), PemStatics.RsaEncryptedPkcs8PrivateEnd, PemType.EncryptedPkcs8PrivateKey);
        return pemObject.Write();
    }

    static void ImportPkcs8PrivateKeyPem(this RSA rsa, string pkcs8PrivateKeyPem)
    {
        var pemObject = PemObject.Read(pkcs8PrivateKeyPem);
        if (pemObject.PemType != PemType.Pkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        rsa.ImportPkcs8PrivateKeyPem(bytes);
    }

    static void ImportEncryptedPkcs8PrivateKeyPem(this RSA rsa, string encryptedPkcs8PrivateKeyPem, byte[] password)
    {
        var pemObject = PemObject.Read(encryptedPkcs8PrivateKeyPem);
        if (pemObject.PemType != PemType.EncryptedPkcs8PrivateKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not encrypted pkcs8 private key");
        var bytes = Convert.FromBase64String(pemObject.Body);

        var key = Pkcs8.DecodeEncryptedPrivateKeyInfo(bytes, password);
        rsa.ImportPkcs8PrivateKeyPem(key);
    }

    static void ImportPkcs8PrivateKeyPem(this RSA rsa, byte[] bytes)
    {
        var key = Pkcs8.DecodePrivateKeyInfo(bytes);
        rsa.ImportPkcs1PrivateKeyPem(key);
    }
    #endregion

    #region Public Key
    static string ExportPublicKeyPem(this RSA rsa)
    {
        var bytes = Pkcs1.EncodePublicKey(rsa.ExportParameters(false));
        var pemObject = new PemObject(PemStatics.RsaPublicStart, Convert.ToBase64String(bytes), PemStatics.RsaPublicEnd, PemType.PublicKey);
        return pemObject.Write();
    }

    static string ExportX509SubjectPublicKeyPem(this RSA rsa)
    {
        var bytes = X509Helper.EncodeSubjectPublicKeyInfo(rsa.ExportParameters(false));
        var pemObject = new PemObject(PemStatics.RsaX509SubjectPublicStart, Convert.ToBase64String(bytes), PemStatics.RsaX509SubjectPublicEnd, PemType.X509SubjectPublicKey);
        return pemObject.Write();
    }

    static void ImportPublicKeyPem(this RSA rsa, string publicKeyPem)
    {
        var pemObject = PemObject.Read(publicKeyPem);
        if (pemObject.PemType != PemType.PublicKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not rsa public key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        var parameters = Pkcs1.DecodePublicKey(bytes);
        rsa.ImportParameters(parameters);
    }

    static void ImportX509SubjectPublicKeyPem(this RSA rsa, string x509SubjectPublicKeyPem)
    {
        var pemObject = PemObject.Read(x509SubjectPublicKeyPem);
        if (pemObject.PemType != PemType.X509SubjectPublicKey) throw new InvalidDataException($"key type({pemObject.PemType}) is not x509 subject public key");
        var bytes = Convert.FromBase64String(pemObject.Body);
        var parameters = X509Helper.DecodeSubjectPublicInfo(bytes);
        rsa.ImportParameters(parameters);
    }
    #endregion
}