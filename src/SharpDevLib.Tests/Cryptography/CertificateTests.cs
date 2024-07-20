using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Tests.Cryptography;

[TestClass]
public class CertificateTests
{
    [TestMethod]
    public void GenerateSelfSignedCATest()
    {
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-ca.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-ca.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-ca.csr");
        var pfxPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-ca.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestRootCA") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateSelfSignedCACert(privateKey, csr, serialNumber, 360000, "Test CA Cert");
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
    }

    [TestMethod]
    public void GenerateCATest()
    {
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/ca.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/ca.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/ca.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestCA") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateCACert(caKey, caCert, csr, serialNumber, 360, "Test Second Level CA Cert");
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
    }

    [TestMethod]
    public void GenerateSelfSignedServerCertTest()
    {
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-server.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-server.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-server.csr");
        var pfxCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-server.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxCertPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestSelfSignedServer") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var altNames = new List<SubjectAlternativeName>
        {
            new(SubjectAlternativeNameType.Dns,"localhost"),
            new(SubjectAlternativeNameType.Dns,"*.localhost"),
            new(SubjectAlternativeNameType.IP,"127.0.0.1"),
        };
        var cert = X509.GenerateSelfSignedServerCert(privateKey, csr, serialNumber, 360, altNames, "Test Self Signed Server Cert");
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxCertPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
    }

    [TestMethod]
    public void GenerateServerCertTest()
    {
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.crt");
        var pfxCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.pfx");
        var derCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.der");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxCertPath.RemoveFileIfExist();
        derCertPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestServer") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var altNames = new List<SubjectAlternativeName>
        {
            new(SubjectAlternativeNameType.Dns,"localhost"),
            new(SubjectAlternativeNameType.Dns,"*.localhost"),
            new(SubjectAlternativeNameType.IP,"127.0.0.1"),
        };
        var cert = X509.GenerateServerCert(caKey, caCert, csr, serialNumber, 360, altNames, "Test Server Cert");
        cert.SaveCrt(certPath);
        cert.SaveDer(derCertPath);
        SavePfxTest(cert, keyPath, pfxCertPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
        Assert.IsTrue(new FileInfo(derCertPath).Exists);
        Assert.IsTrue(new FileInfo(derCertPath).Length > 0);
        Assert.IsTrue(new FileInfo(pfxCertPath).Exists);
        Assert.IsTrue(new FileInfo(pfxCertPath).Length > 0);
    }

    static void SavePfxTest(X509Certificate2 certificate, string keyPath, string pfxPath)
    {
        certificate.SavePfx(pfxPath, File.ReadAllText(keyPath), "foo");

        Assert.IsTrue(File.Exists(pfxPath));
        Assert.IsTrue(new FileInfo(pfxPath).Length > 0);
        Assert.IsTrue(new X509Certificate2(pfxPath, "foo").Subject.NotNullOrEmpty());
    }

    [TestMethod]
    public void GenerateSelfSignedClientCertTest()
    {
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-client.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-client.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/self-client.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestSelfSignedClient") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateSelfSignedClientCert(privateKey, csr, serialNumber, 360, "Test Self Signed Client Cert");
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
    }

    [TestMethod]
    public void GenerateClientCertTest()
    {
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Cryptography/Certificate/ca.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/client.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/client.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/client.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.ToUtf8Bytes().SaveToFile(keyPath);

        var subject = new X509Subject("TestClient") { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().ToUtf8Bytes().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateClientCert(caKey, caCert, csr, serialNumber, 360, "Test Client Cert");
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsTrue(new FileInfo(keyPath).Length > 0);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsTrue(new FileInfo(certPath).Length > 0);
    }
}
