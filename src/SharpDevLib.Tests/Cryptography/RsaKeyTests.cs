using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Cryptography;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SharpDevLib.Tests.Cryptography;

[TestClass]
public class RsaKeyTests
{
    #region Data
    const string password = "foo";
    static readonly byte[] passwordBytes = password.ToUtf8Bytes();
    static readonly string Pkcs1PrivateKey = GetKey(nameof(Pkcs1PrivateKey));
    static readonly string Pkcs8PrivateKey = GetKey(nameof(Pkcs8PrivateKey));
    static readonly string PublicKey = GetKey(nameof(PublicKey));
    static readonly string X509PublicKey = GetKey(nameof(X509PublicKey));
    const string d = "96869b760e3f38cc1e88034d439bc3655022c401070b9b2b9a17af8f8a8ee738f11cf501926fc302fb39fdbdd5f8ae0ff975deb4ae29b72c66ebeb9ab67ece59f9b684633dba55a0e19eb646e17fa05036cfabda4dadd3e99f884d0e1edde5bd66338b7b1f1451277943dd3db80223d11e791d19d2df58889a84e77d73bf5b10658196da657a23e5b61f0b8870a30d69b9a4ab2aaf3e4f80b213780d1d67ff9ae8ceab2e669b463141e25857d3442609de745742d07c240df1e0c224894644b31800aebe116a2aac0714cc8f27479cdbf563d40a24564055775e15dc37f1c7b8323e35d4b73208146426b343f0eddfcb365224a313e9e998dcf24b5f312b4591";
    const string exponent = "010001";


    static string GetKey(string type)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/CryptoGraphy/RsaKeys/{type}.txt");
        var text = File.ReadAllText(path).Replace("\r\n", "\n");
        return text;
    }
    #endregion

    #region Pkcs1
    [TestMethod]
    public void ExportPkcs1PrivateKeyTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var pem = rsa.ExportPem(RsaPemType.Pkcs1PrivateKey);
        Console.WriteLine(pem);
        Assert.AreEqual(Pkcs1PrivateKey, pem);
    }

    [TestMethod]
    public void ExportAESEncryptedPkcs1PrivateKeyTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var encryptedPkcs1Pem = rsa.ExportPem(RsaPemType.EncryptedPkcs1PrivateKey, passwordBytes);
        Console.WriteLine(encryptedPkcs1Pem);
        Assert.AreNotEqual(Pkcs1PrivateKey, encryptedPkcs1Pem);

        using var newRsa = RSA.Create();
        newRsa.ImportPem(encryptedPkcs1Pem, passwordBytes);
        var exportedPem = newRsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ExportTrippleDESEncryptedPkcs1PrivateKeyTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var encryptedPkcs1Pem = rsa.ExportPem(RsaPemType.EncryptedPkcs1PrivateKey, passwordBytes, "DES-EDE3-CBC");
        Console.WriteLine(encryptedPkcs1Pem);
        Assert.AreNotEqual(Pkcs1PrivateKey, encryptedPkcs1Pem);

        using var newRsa = RSA.Create();
        newRsa.ImportPem(encryptedPkcs1Pem, passwordBytes);
        var exportedPem = newRsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ImportPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(Pkcs1PrivateKey);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ImportAESEncryptedPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(GetKey("AESEncryptedPkcs1PrivateKey"), passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ImportTrippleDESEncryptedPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(GetKey("TrippleDESEncryptedPkcs1PrivateKey"), passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }
    #endregion

    #region Pkcs8
    [TestMethod]
    public void ExportPkcs8PrivateKeyTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var pem = rsa.ExportPem(RsaPemType.Pkcs8PrivateKey);
        Console.WriteLine(pem);
        Assert.AreEqual(Pkcs8PrivateKey, pem);
    }

    [TestMethod]
    public void ExportEncryptedPkcs8PrivateKeyTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        string encryptedPkcs8Pem = rsa.ExportPem(RsaPemType.EncryptedPkcs8PrivateKey, passwordBytes);
        Console.WriteLine(encryptedPkcs8Pem);
        Assert.AreNotEqual(Pkcs1PrivateKey, encryptedPkcs8Pem);

        using var newRsa = RSA.Create();
        newRsa.ImportFromEncryptedPem(encryptedPkcs8Pem, passwordBytes);
        var exportedPem = newRsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ImportPkcs8PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(Pkcs8PrivateKey);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }

    [TestMethod]
    public void ImportEncryptedPkcs8PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(GetKey("EncryptedPkcs8PrivateKey"), passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(Pkcs1PrivateKey, exportedPem);
    }
    #endregion

    #region PublicKey
    [TestMethod]
    public void ExportPublicKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var pem = rsa.ExportPem(RsaPemType.PublicKey);
        Console.WriteLine(pem);
        Assert.AreEqual(PublicKey, pem);
    }

    [TestMethod]
    public void ExportX509SubjectPublicKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(Pkcs1PrivateKey);
        var pem = rsa.ExportPem(RsaPemType.X509SubjectPublicKey);
        Console.WriteLine(pem);
        Assert.AreEqual(X509PublicKey, pem);
    }

    [TestMethod]
    public void ImportPublicKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(PublicKey);
        var exportedPem = rsa.ExportRSAPublicKeyPem().Trim();
        Assert.AreEqual(PublicKey, exportedPem);
    }

    [TestMethod]
    public void ImportX509SubjectPublicKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(X509PublicKey);
        var exportedPem = rsa.ExportRSAPublicKeyPem().Trim();
        Assert.AreEqual(PublicKey, exportedPem);
    }
    #endregion

    #region Convert
    [TestMethod]
    public void IsKeyPairMatchTest()
    {
        Assert.IsTrue(RsaKeyExtension.IsKeyPairMatch(Pkcs1PrivateKey, PublicKey));
        Assert.IsTrue(RsaKeyExtension.IsKeyPairMatch(Pkcs8PrivateKey, PublicKey));
        Assert.IsTrue(RsaKeyExtension.IsKeyPairMatch(Pkcs1PrivateKey, X509PublicKey));
        Assert.IsTrue(RsaKeyExtension.IsKeyPairMatch(Pkcs8PrivateKey, X509PublicKey));
        Assert.IsFalse(RsaKeyExtension.IsKeyPairMatch(Pkcs1PrivateKey, Pkcs8PrivateKey));
    }

    [TestMethod]
    [DataRow("Pkcs1PrivateKey", false, RsaPemType.Pkcs1PrivateKey, true, false, 2048)]
    [DataRow("AESEncryptedPkcs1PrivateKey", true, RsaPemType.EncryptedPkcs1PrivateKey, true, true, 2048)]
    [DataRow("AESEncryptedPkcs1PrivateKey", false, RsaPemType.EncryptedPkcs1PrivateKey, true, true, 0)]
    [DataRow("TrippleDESEncryptedPkcs1PrivateKey", true, RsaPemType.EncryptedPkcs1PrivateKey, true, true, 2048)]
    [DataRow("Pkcs8PrivateKey", false, RsaPemType.Pkcs8PrivateKey, true, false, 2048)]
    [DataRow("EncryptedPkcs8PrivateKey", true, RsaPemType.EncryptedPkcs8PrivateKey, true, true, 2048)]
    [DataRow("PublicKey", false, RsaPemType.PublicKey, false, false, 2048)]
    [DataRow("X509PublicKey", false, RsaPemType.X509SubjectPublicKey, false, false, 2048)]
    public void GetKeyInfoTest(string keyName, bool requirePassword, RsaPemType type, bool isPrivate, bool isEncrypted, int keySize)
    {
        var key = GetKey(keyName);
        var info = RsaKeyExtension.GetKeyInfo(key, requirePassword ? passwordBytes : null);
        Assert.AreEqual(type, info.Type);
        Assert.AreEqual(isPrivate, info.IsPrivate);
        Assert.AreEqual(isEncrypted, info.IsEncrypted);
        Assert.AreEqual(keySize, info.KeySize);

        if (isEncrypted && !requirePassword)
        {
            Assert.IsNull(info.Parameters);
        }
        else
        {
            if (isPrivate) Assert.AreEqual(d, info.Parameters?.D);
            Assert.AreEqual(exponent, info.Parameters?.Exponent);
        }
    }
    #endregion
}
