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
        var name = "TestCASelfSigned";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.csr");
        var pfxPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateSelfSignedCACert(privateKey, csr, serialNumber, 360000, subject.CommonName);
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }

    [TestMethod]
    public void GenerateCATest()
    {
        var rootCaName = "TestRootCA";
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var name = "TestCASecondLevel";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.csr");
        var pfxPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateCACert(caKey, caCert, csr, serialNumber, 360, subject.CommonName);
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);

        GenerateServerCertByCATest(keyPath, certPath, "TestServerThirdLevel");
    }

    [TestMethod]
    public void GenerateSelfSignedServerCertTest()
    {
        var name = "TestServerSelfSigned";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.csr");
        var pfxCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxCertPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var altNames = new List<SubjectAlternativeName>
        {
            new(SubjectAlternativeNameType.Dns,"localhost"),
            new(SubjectAlternativeNameType.Dns,"*.localhost"),
            new(SubjectAlternativeNameType.IP,"127.0.0.1"),
        };
        var cert = X509.GenerateSelfSignedServerCert(privateKey, csr, serialNumber, 360, altNames, subject.CommonName);
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxCertPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }

    [TestMethod]
    public void GenerateServerCertTest()
    {
        var rootCaName = "TestRootCA";
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.crt");
        GenerateServerCertByCATest(caKeyPath, caCertPath, "TestServerSecondLevel");
    }

    static void GenerateServerCertByCATest(string caKeyPath, string caCertPath, string keyName)
    {
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{keyName}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{keyName}.crt");
        var pfxCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{keyName}.pfx");
        var derCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{keyName}.der");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{keyName}.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();
        pfxCertPath.RemoveFileIfExist();
        derCertPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(keyName) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var altNames = new List<SubjectAlternativeName>
        {
            new(SubjectAlternativeNameType.Dns,"localhost"),
            new(SubjectAlternativeNameType.Dns,"*.localhost"),
            new(SubjectAlternativeNameType.IP,"127.0.0.1"),
        };
        var cert = X509.GenerateServerCert(caKey, caCert, csr, serialNumber, 360, altNames, subject.CommonName);
        cert.SaveCrt(certPath);
        cert.SaveDer(derCertPath);
        SavePfxTest(cert, keyPath, pfxCertPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
        Assert.IsTrue(new FileInfo(derCertPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(derCertPath).Length);
        Assert.IsTrue(new FileInfo(pfxCertPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(pfxCertPath).Length);
    }

    static void SavePfxTest(X509Certificate2 certificate, string keyPath, string pfxPath)
    {
        certificate.SavePfx(pfxPath, File.ReadAllText(keyPath), "foo");

        Assert.IsTrue(File.Exists(pfxPath));
        Assert.IsGreaterThan(0, new FileInfo(pfxPath).Length);
        Assert.IsTrue(new X509Certificate2(pfxPath, "foo").Subject.NotNullOrEmpty());
    }

    [TestMethod]
    public void GenerateSelfSignedClientCertTest()
    {
        var name = "TestClientSelfSigned";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests{name}.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateSelfSignedClientCert(privateKey, csr, serialNumber, 360, subject.CommonName);
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }

    [TestMethod]
    public void GenerateClientCertTest()
    {
        var rootCaName = "TestRootCA";
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var name = "TestClient";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateClientCert(caKey, caCert, csr, serialNumber, 360, subject.CommonName);
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }

    [TestMethod]
    public void GenerateSelfSignedCodeSigningCertTest()
    {
        var name = "TestCodeSigningSelfSigned";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests{name}.csr");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateCodeSigningCert(privateKey, csr, serialNumber, 360, subject.CommonName);
        cert.SaveCrt(certPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }

    [TestMethod]
    public void GenerateCodeSigningCertTest()
    {
        var rootCaName = "TestRootCA";
        var caKeyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.key");
        var caCertPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Cryptography/Certificate/{rootCaName}.crt");
        var caKey = File.ReadAllText(caKeyPath);
        var caCert = new X509Certificate2(caCertPath);

        var name = "TestCodeSigning";
        var keyPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.key");
        var certPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.crt");
        var csrPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.csr");
        var pfxPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.pfx");
        keyPath.RemoveFileIfExist();
        certPath.RemoveFileIfExist();
        csrPath.RemoveFileIfExist();

        using var rsa = RSA.Create();
        rsa.KeySize = 2048;
        var privateKey = rsa.ExportPem(PemType.Pkcs1PrivateKey);
        var publicKey = rsa.ExportPem(PemType.X509SubjectPublicKey);
        privateKey.Utf8Decode().SaveToFile(keyPath);

        var subject = new X509Subject(name) { Country = "CN", City = "Random City", Province = "Random Province", Organization = "Random Organization", OrganizationalUnit = "Random Organization Unit" };
        var csr = new X509CertificateSigningRequest(subject.Text(), privateKey);
        csr.Export().Utf8Decode().SaveToFile(csrPath);
        var serialNumber = X509.GenerateSerialNumber();
        var cert = X509.GenerateCodeSigningCert(caKey, caCert, csr, serialNumber, 360, subject.CommonName);
        cert.SaveCrt(certPath);
        SavePfxTest(cert, keyPath, pfxPath);

        Assert.IsTrue(new FileInfo(keyPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(keyPath).Length);
        Assert.IsTrue(new FileInfo(certPath).Exists);
        Assert.IsGreaterThan(0, new FileInfo(certPath).Length);
    }
}
