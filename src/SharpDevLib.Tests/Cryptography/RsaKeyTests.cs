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

    static string GetKey(string type)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/CryptoGraphy/{type}.txt");
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
        Assert.AreEqual(GetKey("X509PublicKey"), pem);
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
        rsa.ImportPem(GetKey("X509PublicKey"));
        var exportedPem = rsa.ExportRSAPublicKeyPem().Trim();
        Assert.AreEqual(PublicKey, exportedPem);
    }
    #endregion
}
