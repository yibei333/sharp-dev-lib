using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Cryptography;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SharpDevLib.Tests.Cryptography;

[TestClass]
public class SymmetricAlgorithmExtensionTests
{
    const string plainText = "foo";
    static readonly byte[] key = "1234567".ToUtf8Bytes();
    static readonly byte[] iv = "12345678".ToUtf8Bytes();
    const string plainFileHash = "2c26b46b68ffc68ff99b453c1d30413413422d706483bfa0f98a5e886266e7ae";
    const string aesFileHash = "3ccf2755b13f7635337bd8b54fb8d80c3e306751c7bbeb052baaeb7a318bbc1e";
    const string desFileHash = "c00b686b22e9de4bb1a09259b26c6df60f80762c3e2186d04e3b57c2028b775b";
    const string trippleDesFileash = "c338cf4e2e9f6efd7cdd432db707f77f0490e910952a6d383a55809287aa261b";

    static SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmName)
    {
        if (algorithmName.Equals("AES", StringComparison.OrdinalIgnoreCase)) return Aes.Create();
        if (algorithmName.Equals("DES", StringComparison.OrdinalIgnoreCase)) return DES.Create();
        if (algorithmName.Equals("3DES", StringComparison.OrdinalIgnoreCase)) return TripleDES.Create();
        throw new NotImplementedException();
    }

    [TestMethod]
    [DataRow("aes", "XnZb0CukLb8xgQ+GXD/3MQ==")]
    [DataRow("des", "IExA3pG3FH8=")]
    [DataRow("3des", "9fRHJoEPRqw=")]
    public void EncryptTest(string algorithmName, string expected)
    {
        using var algorithm = GetSymmetricAlgorithm(algorithmName);
        algorithm.Mode = CipherMode.CBC;
        algorithm.Padding = PaddingMode.PKCS7;
        algorithm.SetKey(key);
        algorithm.SetIV(iv);
        var encrypted = algorithm.Encrypt(plainText.ToUtf8Bytes());
        Assert.AreEqual(expected, encrypted.Base64Encode());
    }

    [TestMethod]
    [DataRow("aes", "XnZb0CukLb8xgQ+GXD/3MQ==")]
    [DataRow("des", "IExA3pG3FH8=")]
    [DataRow("3des", "9fRHJoEPRqw=")]
    public void DecryptTest(string algorithmName, string encrypted)
    {
        using var algorithm = GetSymmetricAlgorithm(algorithmName);
        algorithm.SetKey(key);
        algorithm.SetIV(iv);
        var decrypted = algorithm.Decrypt(encrypted.Base64Decode());
        Assert.AreEqual(plainText, decrypted.ToUtf8String());
    }

    [TestMethod]
    [DataRow("AES", aesFileHash)]
    [DataRow("DES", desFileHash)]
    [DataRow("3DES", trippleDesFileash)]
    public void EncryptFileTest(string algorithmName, string expected)
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Foo.txt");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Symmetric/Output/Foo-{algorithmName}.txt");
        targetPath.RemoveFileIfExist();
        new FileInfo(targetPath).DirectoryName?.EnsureDirectoryExist();
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        using var algorithm = GetSymmetricAlgorithm(algorithmName);
        algorithm.Mode = CipherMode.CBC;
        algorithm.Padding = PaddingMode.PKCS7;
        algorithm.SetKey(key);
        algorithm.SetIV(iv);
        algorithm.Encrypt(sourceStream, targetStream);

        Assert.IsTrue(File.Exists(targetPath));
        var hash = targetStream.SHA256Hash();
        Assert.AreEqual(expected, hash);
    }

    [TestMethod]
    [DataRow("AES")]
    [DataRow("DES")]
    [DataRow("3DES")]
    public void DecryptFileTest(string algorithmName)
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Symmetric/Foo-{algorithmName}.txt");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Symmetric/Output/Foo-Decrypt-{algorithmName}.txt");
        targetPath.RemoveFileIfExist();
        new FileInfo(targetPath).DirectoryName?.EnsureDirectoryExist();
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        using var algorithm = GetSymmetricAlgorithm(algorithmName);
        algorithm.Mode = CipherMode.CBC;
        algorithm.Padding = PaddingMode.PKCS7;
        algorithm.SetKey(key);
        algorithm.SetIV(iv);
        algorithm.Decrypt(sourceStream, targetStream);

        Assert.IsTrue(File.Exists(targetPath));
        var hash = targetStream.SHA256Hash();
        Assert.AreEqual(plainFileHash, hash);
    }
}
