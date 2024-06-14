using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using SharpDevLib.Cryptography;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Cryptography;

[TestClass]
public class RsaKeyTests
{
    #region Data
    const string password = "foo";
    static readonly byte[] passwordBytes = password.ToUtf8Bytes();

    internal enum PemType
    {
        Pkcs1PrivateKey,
        AESEncryptedPkcs1PrivateKey,
        TrippleDESEncryptedPkcs1PrivateKey,
        Pkcs8PrivateKey,
        EncryptedPkcs8PrivateKey,
        PublicKey,
        X509PublicKey,
    }

    static readonly Dictionary<PemType, string> keys = new()
    {
        { PemType.Pkcs1PrivateKey,GetKey(PemType.Pkcs1PrivateKey) },
        { PemType.AESEncryptedPkcs1PrivateKey,GetKey(PemType.AESEncryptedPkcs1PrivateKey) },
        { PemType.TrippleDESEncryptedPkcs1PrivateKey,GetKey(PemType.TrippleDESEncryptedPkcs1PrivateKey) },
        { PemType.Pkcs8PrivateKey,GetKey(PemType.Pkcs8PrivateKey) },
        { PemType.EncryptedPkcs8PrivateKey,GetKey(PemType.EncryptedPkcs8PrivateKey) },
        { PemType.PublicKey,GetKey(PemType.PublicKey) },
        { PemType.X509PublicKey,GetKey(PemType.X509PublicKey) },
    };

    static string GetKey(PemType type)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/CryptoGraphy/{type}.txt");
        var text = File.ReadAllText(path);
        return text;
    }
    #endregion

    [TestMethod]
    public void ImportPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPkcs1PrivateKeyPem(keys[PemType.Pkcs1PrivateKey]);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void ImportAESEncryptedPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportEncryptedPkcs1PrivateKeyPem(keys[PemType.AESEncryptedPkcs1PrivateKey], passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void ImportTrippleDESEncryptedPkcs1PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportEncryptedPkcs1PrivateKeyPem(keys[PemType.TrippleDESEncryptedPkcs1PrivateKey], passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void Tet()
    {
        var a=CSharp_easy_RSA_PEM.Crypto.DecodeRsaPrivateKey(keys[PemType.TrippleDESEncryptedPkcs1PrivateKey], password);
        var exportedPem = a.ExportRSAPrivateKeyPem().Trim();

        using var rsa = RSA.Create();
        rsa.ImportEncryptedPkcs1PrivateKeyPem(keys[PemType.TrippleDESEncryptedPkcs1PrivateKey], passwordBytes);

        Console.WriteLine(exportedPem);
    }
}
