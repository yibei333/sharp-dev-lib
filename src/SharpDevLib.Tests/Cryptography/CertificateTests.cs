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

    [TestMethod]
    public void Test()
    {
        var pem = @"
-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAy73ZQN3YT9SSXxMRVwZxDH4UE6YpSQ4MNqTj/GEpM9/m88TE
ZQUvq/jPP7UzNbWLR35bTPCEkHspYjb5f+c/Na6kU97HQAn6lF1zLjxkrICTggGO
bmr8zxE8NNuDe2DaS6EfciPgZgoj7izAEVShIPw3k7adky74bt96mdT9eet4iEUU
tdZcyw1QBnBMNWzwPx9g/bp1v52nGIiJLNjqAE9UJ8jC8f+cj1WiHa1JlSmc50bR
q3zrsSkpWqtv0xZR9LUZAxnWh+V4trwORaGs/X1Wy5snTi5ZY4Z3HausK+uLc+19
tOV8B0Y/PN5MYYIWnzsaNduTbMjf/uDDjTklcQIDAQABAoIBAAC3C9okbSvqjRD0
Wi06Ao+Oqbdf7+knuXc/oOUz2hcqg/77A81u9TnfR6rrkeBwYKrBkMR2W6C+LekA
VGPUhe5ETKNWttBMuKpooYzZ/Wh1kw7zvnI4weZgIxv0YUO/lUSKrjeSVKlHA9VE
JvBooaAiAnfsPiVUFt8cvatoifMxlMJUGaqSHmmtQEH6HyxPb4kuo6iw1+gzeoTL
QNgS4sCjoCoPc90/SQniW0OBkVkdhlrCewlxZ88W0DERmVc3+RR6FDTrDezDYyFj
hnn3Yjb9Hsb56/mzpWJGShvl9KdGawU0ZusxVkNqeB9aw2tGFxHaYk6bLIGgjGo0
PR+vDDkCgYEA5elTYJZm9Cs+MZj586Gg29SDApfT0GgK95KkfQmmyP1K+QNFNGwi
1fILfqj5vw7Rd8JTxtYWiaOrfj7drRGkIHnOYI+9+6Hm0Fk9MkL0iYyz+A3AZJoW
FXrxAI0ql18poDvvNVI9xcotdVp0aTjZRfg9h09PCyC2OS8lWPFvR9MCgYEA4txR
bl4BPFR/DXple7a8qxpG11o2Hp1D+K420Mg6HRahg4/K4Bv08dWdc792fGhu/vvz
RMwA0M53yme5RS9JypRTCbS83VPWtNunFDp79oEb/umZJRIDBaqd8MpPfkg5zHkx
WGtGXpVTLkEmPmMVgs1xJvJB1VGTt8hTidwndysCgYEA3n7KCJwk8ED1BEyV//e3
02Y6jwTdoD8/9c09UCBJ/xJZPtiyXYQUxlViWGYwo3w0rKAsMS24S+VPrnIqVzXO
TvtyBLK22dEZrSHffkebnY3EENdGDFWt74W1u6HhyPH4N7Ao08JOM55wFbS/GaFB
a1xMNylCRnOWxYphq0yjM68CgYBDZKn4PIrnbj6UNXEicGXZ+qNi5FTBgXyoyrBU
E7dX3to6aCQfsY5xrUDqGs3LJYcbzqM1I9l2Lm/dvDSIvgSQ3sFQV9XndjmJXtti
ogEjeLVlY+Xv4krtiwMCfkdhP3mUKcij+LJd9MRSMF55GBxS0E81/6/Y/DQC28sd
YD6FIwKBgQDVMsQeisp3Ah/BAWvSRRtZkmH+z+IrunRGD3ogOn+5KsibhPDPLbgG
8ArSy+OQi10YVKcCZInu2iXE2L2Uko18L7kw9WamdBkg+j0P7B0BHDuQDT3Q6E4O
gMgAbDrNUTe7MOjRjTVGa7zgV2EyvK9fA0DivME8saO0Jjx/fTOXVA==
-----END RSA PRIVATE KEY-----";
        using var rsa = RSA.Create();
        rsa.ImportPem(pem);
        Console.WriteLine(rsa.KeySize);
    }
}
