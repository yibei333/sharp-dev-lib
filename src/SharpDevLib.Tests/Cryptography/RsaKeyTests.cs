﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

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
        var text = File.ReadAllText(path).Replace("\r\n", "\n");
        return text;
    }
    #endregion

    [TestMethod]
    public void ExportPkcs1PrivateKey()
    {
        using var rsa = RSA.Create();
        rsa.ImportFromPem(keys[PemType.Pkcs1PrivateKey]);
        var pem = rsa.ExportPkcs1PrivateKeyPem();
        Console.WriteLine(pem);
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], pem);
    }

    [TestMethod]
    public void ExportAESEncryptedPkcs1PrivateKey()
    {
        using var rsa = RSA.Create();
        rsa.ImportPkcs1PrivateKeyPem(keys[PemType.Pkcs1PrivateKey]);
        var encryptedPkcs1Pem = rsa.ExportEncryptedPkcs1PrivateKeyPem(passwordBytes);
        Console.WriteLine(encryptedPkcs1Pem);
        Assert.AreNotEqual(keys[PemType.Pkcs1PrivateKey], encryptedPkcs1Pem);

        using var newRsa = RSA.Create();
        newRsa.ImportEncryptedPkcs1PrivateKeyPem(encryptedPkcs1Pem, passwordBytes);
        var exportedPem = newRsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void ExportTrippleDESEncryptedPkcs1PrivateKey()
    {
        using var rsa = RSA.Create();
        rsa.ImportPkcs1PrivateKeyPem(keys[PemType.Pkcs1PrivateKey]);
        var encryptedPkcs1Pem = rsa.ExportEncryptedPkcs1PrivateKeyPem(passwordBytes, "DES-EDE3-CBC");
        Console.WriteLine(encryptedPkcs1Pem);
        Assert.AreNotEqual(keys[PemType.Pkcs1PrivateKey], encryptedPkcs1Pem);

        using var newRsa = RSA.Create();
        newRsa.ImportEncryptedPkcs1PrivateKeyPem(encryptedPkcs1Pem, passwordBytes);
        var exportedPem = newRsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

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
    public void ImportPkcs8PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKeyPem(keys[PemType.Pkcs8PrivateKey]);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void ImportEncryptedPkcs8PrivateKeyPemTest()
    {
        using var rsa = RSA.Create();
        rsa.ImportEncryptedPkcs8PrivateKeyPem(keys[PemType.EncryptedPkcs8PrivateKey], passwordBytes);
        var exportedPem = rsa.ExportRSAPrivateKeyPem().Trim();
        Assert.AreEqual(keys[PemType.Pkcs1PrivateKey], exportedPem);
    }

    [TestMethod]
    public void Test()
    {
        Console.WriteLine(string.Join(" ", BitConverter.GetBytes(1)));
        Console.WriteLine(new HMACMD5().InputBlockSize / 8);
        Console.WriteLine(new HMACMD5().OutputBlockSize / 8);

        Console.WriteLine(SHA1.Create().InputBlockSize / 8);
        Console.WriteLine(SHA1.Create().OutputBlockSize / 8);

        Console.WriteLine(SHA256.Create().InputBlockSize / 8);
        Console.WriteLine(SHA256.Create().OutputBlockSize / 8);
    }
}