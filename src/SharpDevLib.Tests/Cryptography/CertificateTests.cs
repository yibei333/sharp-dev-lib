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

    #region Verify
    //[TestMethod]
    //public void PfxTest()
    //{
    //    var pfxPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/server.pfx");
    //    var certificate = new X509Certificate2(pfxPath, "foo");
    //    Assert.IsTrue(certificate.HasPrivateKey);
    //    Console.WriteLine(certificate.Issuer);
    //    Console.WriteLine(certificate.Subject);
    //    Console.WriteLine(certificate.FriendlyName);
    //    Console.WriteLine(certificate.Thumbprint);
    //    Console.WriteLine(certificate.Extensions.Select(x => x.Oid?.Value).Serialize());

    //}

    [TestMethod]
    public void CertificateBagDecryptRightTest()
    {
        var password = "foo";
        var data = @"
bc 6f ad 50 4a 1f 92 fc  f4 17 a1 eb 63 07 80 4c
51 22 55 d0 15 af a2 a3  cc 96 e1 31 23 0f 93 76
d3 fb 11 07 f7 a8 6a 23  2c e3 7b 25 aa ad fe e2
23 79 ba 4a 95 f5 e2 b1  4c 9a 50 8e 77 a7 d0 b4
cf d6 ad 5c 5e be 24 91  92 e4 05 44 53 4e af 76
4f 49 5d f6 ef 16 ed 84  ea 11 15 f7 4e c4 e8 87
42 a5 a6 7b e8 86 0f f0  34 99 34 a5 10 d3 0a 32
7b e8 0c d3 f7 36 99 3c  8e 78 09 7f 78 3b b1 26
49 7c 53 ce 80 52 fe 16  74 3c 8f d4 26 5d 60 08
c4 d8 cb 3f ff f4 22 03  11 33 dc 18 b5 a0 2b 17
f3 73 6d 47 1b 8f 8b 4b  26 a2 77 db bc c9 30 eb
eb f7 90 e7 3c 03 30 84  b8 cb 33 06 08 10 6b 45
c5 59 1f f4 6f 42 90 4f  17 f1 66 6c 5e 68 b3 ee
08 c3 ad 58 32 61 b8 82  16 32 c0 78 c3 cf b5 57
bc 98 84 68 75 6e cd 7f  d0 a5 7d 6b 83 4b 29 7a
45 ce 16 59 09 fe 3b 82  49 71 54 d3 3e 9d 1f a7
21 cc 07 04 70 0d 04 3a  ad 8f 0e 72 81 be b5 29
ad 1c 33 11 d0 ea 67 ac  ae 4e 2c a7 32 76 66 a7
75 99 1e 4f 9a 20 d0 c7  32 2a 67 c7 b3 8f 3f 10
82 8a 25 b0 75 25 52 44  86 1e ae 67 b1 4c 56 16
b3 a7 7f 29 0d a1 da f3  c4 48 d0 5b 69 92 68 50
16 ed c0 23 07 01 8a 69  24 28 b4 9c bb 62 1b 9f
0a a3 53 c3 01 ca 3a 25  74 13 97 fd 06 08 85 d1
a7 4c 72 2b 1e f8 43 e2  df c9 e3 58 43 a8 4b 48
8b fe 3a 24 7e 51 c9 2a  34 1c 43 7e 64 90 a0 6f
18 bb ab ac 15 b7 eb 8b  71 04 d8 f1 b0 91 19 93
34 60 7e bb 91 c7 8f ef  04 01 b2 d0 22 7e cd e9
54 f1 56 4f 48 4d 9c a6  75 1a 83 82 e5 51 e0 2c
88 1e 6e 10 06 11 39 dd  34 c1 d6 01 a8 7e 96 fb
05 ae cc 68 41 20 ae d4  34 d9 e7 65 72 13 8c d4
32 93 17 bc b9 e0 1e 77  59 20 b8 d2 57 45 17 6e
24 40 5d 9a b4 10 a3 8c  60 4d 59 15 9d b5 c9 32
f5 2e eb 5d 03 be 5a 66  7b 74 08 50 25 e0 6a e0
11 b0 81 7b 22 0c 47 7a  66 87 af b4 16 6a 18 be
09 38 ab c2 f7 66 a6 f3  a1 a7 6a 63 16 d9 76 78
49 b5 f8 3c 6a 15 5a 1d  19 9d bc 6d 15 fe f3 9a
12 5e cf 9b a6 b3 be ff  54 8d 8d 85 40 6c 37 24
7f 4c a4 75 cd 71 3b a7  4b 77 d1 cf db 80 1f 4b
69 ea 87 fe 12 cd 68 9a  6c 71 50 18 a7 e9 56 39
32 f6 31 56 97 9c 3a b1  73 0f 6a 83 80 94 68 38
cb 12 6f e6 3a a5 d3 20  80 40 5f 54 43 78 12 13
10 6c 6b 71 a6 87 ba a5  55 f9 a8 02 e9 2b 69 97
5d 91 35 a1 9e f2 3d ba  fd c7 dc 08 a4 d2 28 60
49 7d bd 12 6b 11 6c eb  36 39 3f 6b 60 e9 48 09
3c 6a 61 9d 0b fd 5b a7  08 85 61 13 c1 26 56 2e
34 b2 b4 43 61 45 2d 71  0f fd 3e 8f 55 f8 57 50
28 a0 3a 89 3e 65 cd 2b  2e cf 30 5a 31 c1 1e c9
27 f2 27 fd 00 f2 43 01  59 82 4c fc 91 de 30 b9
01 f2 79 1c d6 cb d6 e4  31 47 61 d1 cb c6 3a bc
38 bd a3 11 5f 24 59 df  07 33 d7 9b b2 e7 d0 11
3f 99 59 d4 7c 3d f9 f2  b1 09 26 c8 5d bd e6 47
a7 03 57 62 bb be fa 81  12 93 5f 60 5e 9b d0 89
7c c2 b0 ca d3 b3 9e 6f  b4 3b 32 92 7a 0f c3 bd
8b bd 81 6e 7d f9 cb e3  c5 8f 3b 7e e5 5a c5 6d
a5 c3 b4 66 b1 33 49 74  46 89 e0 55 e0 4e 86 97
a5 4a cd 99 83 f1 21 72  a5 92 79 76 55 86 35 31
38 e5 b2 c3 dc ee ca a4  28 dc 88 e8 c7 cc d9 dc
6d 28 f7 0e 23 56 43 3b  23 c0 62 ea 7e ca cc ef
48 a3 ec b1 70 cf 4a 41  42 e5 89 73 ed a9 4c ec
1e 31 b1 dc b9 ae 7f 56  6f 7c 48 bd e2 53 3c 9e

";
        var salt = "32 37 13 5d e5 55 d7 67  ";
        var iv = "34 5f 1a 72 79 2f f7 b1  94 55 f1 65 9a e1 88 44\r\n\r\n ";
        var iterationCount = 2000;
        BagDecryptTest(password, data, salt, iv, iterationCount);
    }

    [TestMethod]
    public void CertificateBagDecryptWrongTest()
    {
        var password = "foo";
        var data = @"
2d 96 9f 77 b3 74 83 ca  53 d4 66 93 d0 ab a7 0f
1a e5 7c d2 23 45 45 cd  00 db 52 8d 40 ed 64 30
d5 36 ca 25 aa de 55 47  b8 c4 fd a3 02 6b 45 e0
bb d0 da 46 64 85 72 8c  b7 c9 19 4a e3 9e ab 5a
a2 b1 4b f6 33 81 fd f7  99 09 bb 03 92 14 ea 6c
64 8c e5 5b 00 fc 18 ee  01 8f 21 1a 71 a4 c2 11
95 2c 97 14 59 20 c6 21  9d 1a 73 8f 06 9e 85 20
29 a6 e5 b2 46 3d bf fe  02 dc 10 2f 1f e4 03 ca
b7 50 87 90 d0 48 9c 08  ff 2d 9f 22 ef 36 36 25
d2 d0 58 48 58 6a b1 00  ad 27 fa 05 2c 5a c2 80
86 be 01 46 2a 11 f2 a1  a9 84 95 f2 db df ec 55
e7 09 c5 11 e5 a3 2a fe  6b 8c 10 7d ae 8e a5 72
12 51 b9 1d 08 93 0f 34  81 93 27 1a 1c 26 8f 9b
fd 75 41 60 f2 c0 d5 5d  71 7c ac 58 d7 2b 9d ba
bb be 09 4f a1 33 8f 44  69 aa c9 d9 84 79 8f 14
09 bc 51 68 18 12 78 5b  57 7d 8e 1d ed 5b 81 41
42 f9 38 8f a5 84 dc 20  8b 8a f3 6e 7d 23 92 ca
54 b9 b0 02 26 69 30 9d  2b f2 d1 84 dc b7 80 9b
0f b4 05 98 4c 95 5b f5  db a3 a9 16 2c 39 c8 b6
57 31 90 a9 16 21 48 cb  d4 17 7a dd af 16 38 7c
78 36 33 0d 77 fb 6a e8  3c fa 51 35 c6 ee c1 fc
ca ce 87 4c 33 ea ab da  85 60 a0 93 b4 e4 a3 7b
89 07 bd 2f 27 22 96 a1  02 fe 0a 24 e7 a0 d8 f4
9c 0d fb 70 a6 a8 fc 8a  5f 32 ac 2b fa 47 00 c0
d8 63 8d 9d 3d 65 75 25  f2 04 a3 45 d8 52 a4 58
50 27 60 e3 ef a7 33 ed  5d 27 5b 68 07 49 96 fa
89 27 7b be 84 bc 2b 47  dd 6c ee f6 0a 56 ee b6
7e 61 de 68 9b 7b 23 ff  11 92 d0 8a f7 52 c0 02
4a 36 a4 3f df 25 de 48  4f 32 d4 5f 5f 3c 63 28
1f cd e4 dd c6 a1 78 e8  cc 30 a6 2e bd eb 05 5b
5f 75 db c8 a0 59 c1 a4  a9 ed 62 95 b9 2c 18 25
a9 19 44 e6 fd 73 54 cd  0c c8 b3 56 8c 5c 1a 58
56 e9 42 27 90 1b b5 24  eb 1d 11 f5 96 bf 1f 80
42 ff 5c a9 12 d0 21 59  dc a1 e3 9b 94 8c 5c 8d
5e 40 6e 6f d6 a9 57 d7  72 86 17 f2 da 1b a9 00
2c f4 23 a6 15 c7 e1 fe  5d 54 05 15 11 0b 1d ea
4a 8e 54 fc ca 0c 32 59  6d d8 ee 0c f1 30 29 33
06 d9 3f 09 8b b2 92 8b  4e a9 69 0f 35 4a 88 2f
c3 52 ca 5a b8 c2 a6 f9  3c 32 e4 ab f6 24 14 20
38 7f a6 fc 4a 0c 12 ea  10 a9 7d 75 07 12 b0 e2
f8 c6 24 c0 91 ca a4 2e  eb 4f e4 45 aa 6c 3c 59
d7 00 9b f7 f7 42 9d 65  ba b5 88 30 53 19 e0 23
57 5a 2a 2e 63 ef af 8e  a9 d8 68 f9 8e 09 9e 5d
99 6c 79 22 36 74 88 7c  f8 46 f1 f8 f1 ac af 97
72 1d 76 35 b6 54 3b 9a  1d 34 11 54 3a 8b 8e d8
b2 a7 3a f2 e4 a9 7d 63  d4 42 df 35 c2 b1 e1 fc
3d 92 f8 2b aa 76 9d 57  82 a1 7b 7c db 96 db 7a
57 86 34 16 c8 17 c2 6d  55 43 61 cb a8 39 dd 3a
df 1f 5c 85 1c 8a 8e 2b  f2 64 94 67 41 ef 6a ce
8e a5 f6 d6 9a fe e3 a0  30 42 a2 ba ad f1 66 68
5b 1b 24 33 0e 10 d5 dd  1a 3f f7 fb 43 0a 3d 8b
bb cb c7 2c 3b 39 bf a5  49 55 64 f9 60 00 30 d5
ef 2f 1f 95 9d ba 19 f8  54 b8 3b 91 1e d2 9f bb
0a 4d 3f d2 5f b9 30 67  1a 14 1f 9d ae 60 3e e0
6e 25 b6 2a 57 a8 b6 9d  83 95 3a 44 02 6e 89 4b
fa 91 f0 0a 7e a2 2c c9  cf 5a 69 72 a5 5e cd 4c
60 20 4e 4b ad 53 27 d0  16 59 5e 4c 78 59 7b 90
94 a7 6a 49 97 12 62 ad  26 6e ae c8 37 35 c2 e3
00 b4 13 79 b5 ae 59 20  7a bd ba c2 37 f1 c6 29
88 5e e9 6d 03 66 58 b7  3a c1 8c 66 61 00 6d e0
95 fe 21 49 da 07 01 15  85 3b dc f3 e7 51 9a 60
6c 25 d4 51 75 04 c4 10  49 db 0e d1 fe 1a 19 d8
7b 8f bd 07 f3 a1 91 9f  45 f9 8e e7 d5 76 a2 33
99 ad 4b 91 e0 ed 10 f4  59 40 d8 0d 15 30 a5 30
ef ba c3 8c 24 cf 4d a9  f8 71 9c b7 ba 74 2c 82
84 43 7e 22 8b 8d cb 68  bb 68 b8 be 0d a0 fa 85
51 4a 32 69 5b f3 96 e8  16 86 1c 24 da 97 22 58
4f df 77 f9 41 fd 31 fa  6e 54 41 34 88 54 2a 4c
2c 5e c0 65 d7 1d 7a d9  c7 ff 25 7c de 5f 23 66
fe 3d 4a ca 11 c0 42 84  28 1b 04 d3 f8 bd a7 68
f6 a8 78 78 aa ab 80 c4  e1 f2 a7 85 3c c5 d7 c0
62 18 15 ad 62 2a 77 09  22 59 c5 29 91 2f 32 4a
60 b3 fb 4f 27 14 b9 e0  31 c8 2c 98 67 4e 28 b5
f1 05 64 dd 87 81 83 a7  21 38 b7 79 b7 f1 0d 06
ce 7b 8d 4d cc 91 2a 3c  02 5a 73 79 27 b0 e4 f7

";
        var salt = "f1 04 b0 eb 94 25 94 08  4e cf 98 b7 ad c6 df da";
        var iv = "7b d7 29 8e f1 ab db f1  88 94 c6 ad b4 fb 9c 84";
        var iterationCount = 2048;
        BagDecryptTest(password, data, salt, iv, iterationCount);
    }

    [TestMethod]
    public void PrivateKeyBagDecryptRightTest()
    {
        var password = "foo";
        var data = @"
51 ce 94 72 7a a9 d1 2f  70 aa 1f 25 19 0e 84 64
6c db a1 55 41 66 6c 48  f4 89 1e 91 f4 06 04 d6
1d c8 f3 e3 20 d7 6a 7b  1b 30 a0 13 9b 12 c7 3f
b7 97 db af a7 b9 bf 4c  6c 69 ae 1b 55 7d 37 f3
71 af e9 b8 a8 e0 88 a8  e1 91 62 20 d0 d9 d5 63
5c e0 68 78 ab 42 b6 57  1d d7 f5 00 8d 5a be 3e
15 2f 8d 02 0d 13 16 31  7a fa af f7 a4 4a a0 08
57 62 ad 1a 5a a7 58 e0  9b 58 5c eb 2f bf e2 bd
db 28 e0 22 5f d2 b1 aa  b5 64 95 83 6f cd 97 b3
a3 c1 67 d9 64 be da de  88 fe 01 4e 4e 23 ae ae
c0 32 7d 49 28 e0 91 7e  27 3a 80 37 09 ec c8 4c
85 34 00 9b 8e be 60 05  82 93 4a d0 2f 8d 32 11
d8 b9 c9 7b c5 c6 5f 43  23 d2 4e e7 f9 69 cb 43
64 dd d2 db b7 08 73 4f  8d b2 e5 a4 8f 2a 97 b7
ad e1 54 f5 32 d3 44 dc  1f 39 b1 b4 a8 c8 09 90
7c 7b 9b a3 b1 2f 71 0f  91 df 5a 42 5f 34 c4 ad
23 bb 86 a7 23 d9 fd 8e  19 7b a3 95 cf a2 f1 18
87 e6 60 c1 60 83 cd 1c  6e 74 93 24 eb f2 c4 40
8e fc 8b d5 64 78 6f fe  0b a4 dc 89 0e a6 dc 7d
62 ab 53 69 0d 5f 63 e5  d0 8b c6 4f 4f 78 03 b7
e3 f3 5b 42 92 7a 97 e3  d1 2f 72 27 85 1d 83 1c
fc 3a 04 18 dc d3 f1 c6  8a 0d e0 90 f2 2a 13 00
52 75 f5 c7 e5 58 04 e0  90 79 41 9c d3 7e 84 13
4f 7e 72 a7 da 3f 7d 74  90 ea f2 0d 84 66 2f 49
8a 97 3b c5 8a 5b 8b 05  09 d5 4b f0 ba 8a 8b 6b
1e 76 4e 5a 2f 80 a2 0e  d8 b8 94 5c 6f c7 ae 9c
ff 89 fb 7f 6c fb 79 c7  14 99 1f 30 04 a8 70 5a
c4 d8 e2 c8 e7 0e 2f 18  e8 a2 b8 83 52 f9 52 95
0e 37 8c e1 91 82 05 47  68 47 9f 16 80 e6 d9 76
79 e7 fc 5f aa 27 d6 28  52 ae 63 9e f4 77 c4 4f
68 11 d8 b1 ba f4 e8 0c  77 46 c9 c4 8f 63 db fb
3b 8f c8 97 36 36 0d a5  83 a6 3b 51 5f 83 5c d1
09 26 79 5d 55 c9 27 68  f1 cd 07 b6 b2 09 91 3f
20 40 83 ce d1 dc f0 ec  a8 c9 6a 1d b8 f1 4f cb
ad ca 78 2f 0a 81 96 9f  24 9e 66 ef ca d5 d1 e7
57 e8 fb 99 d8 e6 9c 94  c0 89 1d b7 22 09 5e 4e
78 82 64 bd b3 42 50 48  a4 60 3f 26 d2 8b 5e 8c
eb 0f 20 69 af 2f 5c 3d  ed 96 cc 97 d7 b3 08 f0
e5 7a d7 d6 b3 9f c3 fd  b4 29 1d b2 4d f3 d4 61
eb a8 50 90 1a b0 16 1c  06 86 61 88 35 06 ff 70
33 40 20 fd 3d ae 6f 7d  ac 06 bf 27 18 2e 7d 2a
20 bf 9e 22 0d e8 06 dc  53 dd 03 31 ec eb 18 9a
d7 13 31 08 4c 53 6f 6b  26 f0 da 69 c3 d5 25 92
63 4c e1 b9 50 2c b8 6c  df ea 95 94 e9 c5 05 f0
86 7c 72 33 15 01 ef a8  2b 84 58 8d 66 87 22 74
1f 87 47 69 b7 a7 74 07  b5 1b e4 f0 b4 03 2f da
be 3e 4e 67 57 58 c2 9e  46 a3 97 7b 55 3b c6 9d
c0 48 84 58 14 ab bb 7e  37 a8 e7 4f 1c c7 d7 78
2b b9 2d 45 48 88 8f a2  6f a8 7f 0b 0e c3 3a 72
72 34 16 c8 ce ce dd 90  e3 54 77 b2 05 7c 10 79
84 dc 8c 8d 3c b1 6d 65  37 06 60 d5 1a f7 8a 47
8b 54 3e 3d e0 82 f5 ed  ad 7f 2d b6 81 58 39 bf
63 57 f4 57 da 64 74 c9  8e b4 76 b4 5c 10 6d 2a
d6 a6 3c 72 1b ab 84 87  8a 3f 4a 56 23 3d cf 10
41 dd 7b 57 d9 d0 36 49  1b 39 40 da e1 c5 a1 d4
86 10 f2 ff 6d 31 b7 f3  a2 cf 44 26 60 50 74 72
f8 0a 02 5b 92 f9 ee 70  7b d7 53 6a cf a7 41 c6
cb 2d ad cd f8 a4 3d 9a  d6 48 2f e2 13 53 16 67
fb 29 38 d3 81 51 88 9b  38 3b 3b 18 d9 4d 94 9e
3b df e1 50 c7 d4 d0 bc  0c 4c b9 e4 d5 e0 97 47
aa 36 b1 c3 35 07 fc 0b  b0 76 d8 c9 84 aa 57 c2
7a 1b 5e 15 7e a1 91 73  b0 18 30 e0 80 00 37 c2
d5 95 e1 ad 77 e3 a5 48  7f 58 1c ac 72 6d 6b b1
a1 7b 5a a1 04 18 18 83  24 e4 0c e9 6b 60 00 32
07 d2 93 5c db b7 d5 46  b9 90 b6 8d 8a e2 cd 31
b9 4a 4a 22 cd f5 77 26  45 6f b1 86 33 b5 51 05
ea 56 28 80 40 cc b5 07  a8 f7 c8 19 ec 1f e3 33
70 20 a9 3c 2b 01 0b 91  3e 15 85 96 0f 40 d1 78
43 8e a0 50 50 2e cf 9d  d7 b4 16 f7 f4 93 ef 2d
dd 7d 18 c5 3d 30 6d 11  b1 f9 ad e8 fe 88 35 83
18 e8 c9 68 c0 8a 56 bd  02 ea 08 b6 4b 45 06 f5
d7 3a a8 c2 67 02 07 6b  de 03 33 01 20 15 45 5f
9a 49 e1 61 f1 7f dc 01  84 9b cd 0c ac c0 4d 55
88 a4 fd e9 ff 5e 0c 0e  d4 86 d2 6c 4f 73 5e de
4a be 72 30 6d e3 21 2c  c9 e0 85 08 e4 79 ca ac
79 47 cc 4b 97 5b d8 80  34 3a 9e a8 54 42 0b 69
e7 f7 bb 1f e1 1d a7 76  9a 9f 61 7e 1b c7 87 69
";
        var salt = "78 30 3e 11 af a1 06 7a  \r\n";
        var iv = "d8 b1 bf 01 f1 be 22 30  07 4b 53 72 9a 5d cd 6b\r\n";
        var iterationCount = 2000;
        BagDecryptTest(password, data, salt, iv, iterationCount);
    }

    [TestMethod]
    public void PrivateKeyBagDecryptWrongTest()
    {
        var password = "foo";
        var data = @"
ec 82 78 15 55 5e 1f 8f  c4 75 8f e2 1d 62 e4 8a
a7 be 61 af 6f 14 33 66  1e fb 0b 3d 45 73 cb 7b
bd 68 03 ec e1 19 38 ad  95 f9 fe df 83 80 69 97
e2 27 98 c7 6a ec ba 0f  a3 fc 3c e4 47 b0 7c a4
1e fa 2e 87 e4 62 65 91  65 8a 0e 95 ed 27 5e d2
28 35 3c 75 13 94 85 bc  01 1b a6 be fc a8 a3 54
b6 34 bc 8b a5 ad a2 cb  a4 ba a6 e7 be 5a ad 52
bd 94 00 1e d2 32 0a c6  e1 36 b3 b3 24 e4 7c 6f
dc f5 04 df f0 f9 52 48  3a 83 f3 e2 fe db 83 4f
fa ad a5 4f fc a6 1d 20  64 4b 82 98 60 13 d5 58
ca 65 42 32 d1 91 ea 8a  a2 81 d7 99 75 87 21 c0
f4 74 79 b2 76 db d9 7e  5c 2c 76 90 46 f1 c4 e3
b0 7f 0f 74 25 f5 93 d7  bf c7 74 0c 3a 43 8d 8f
a9 29 50 f9 2f 0f 38 68  10 5b 07 9d 11 bb 64 eb
9d bc 4f c6 db cd 97 74  d9 79 88 40 30 48 3c ed
8d 12 ce 3d f1 93 3a 49  75 0b 30 8a a8 b3 f1 c9
d7 bb aa c2 a5 a6 15 ce  0b 66 8a 6f 9d d7 30 62
b7 57 85 bd 0e 99 87 d4  c4 f0 f0 f3 a3 28 5d 65
d4 9a 46 41 5f ab 78 3f  7c 73 14 12 9e ae d4 20
3a 64 7b 25 b4 fc 1b 19  ab 07 ff 2d 1b 54 a3 1a
b0 22 7c b9 73 15 c1 f0  1e 29 ce 64 1d 48 c4 c3
91 c2 a0 c1 c5 82 77 9b  4a d4 c5 56 f3 f4 5b c6
48 4d 60 12 1d 5c 1d 9b  66 91 3d fd 2a 4e fc c7
6f 59 83 ce 71 a7 3c b9  1a 5f 3e 89 f8 a3 b1 91
44 fb ca 35 05 0e ad 81  40 c3 26 fe 55 22 09 b1
e9 f8 05 d4 ec 5a e6 99  b3 86 b6 52 c3 c2 6f 0c
25 b8 6a 93 c9 57 ec 73  e8 00 4d 92 91 01 72 1a
4a 57 4b 2b 11 fd 35 76  cd 4b 13 84 c4 e4 4b 65
ac aa dc af 18 35 8e 3e  58 b1 2f 21 66 ca 74 b0
69 bd cb 81 90 e7 72 3d  aa 47 ae e9 c8 6f 98 40
d2 fe a3 16 3f fc d6 35  89 60 62 20 bb 68 91 00
e5 3d 2c 7b 6f 86 9d ab  f3 53 66 d6 5b e2 15 ca
f7 1e c5 d8 39 23 29 b7  f2 63 ae 8c 10 d7 0a 39
6b d0 42 d1 1c 53 63 93  e5 b9 74 65 1d c7 e2 59
28 dd f9 b3 8a 10 09 62  d8 e5 85 8f cf 36 32 45
c7 0c 08 03 a7 e3 03 15  c2 a7 b2 b8 c9 7f da 1c
a5 eb 08 36 43 9d 16 ec  df 56 a2 96 15 19 e5 0d
51 2d df bc 31 18 e3 84  f8 12 b7 81 9d 11 ac 56
e6 6e 2b 1e a5 f9 30 f4  dc f0 77 77 7c 6d 69 7f
33 aa 81 8d 71 9d 19 f2  4b d5 88 65 a7 e1 a5 41
a6 65 c1 f3 8b 57 be 1b  de c7 0a 68 16 59 43 d2
99 9c 3e d6 6d 35 be 62  c2 45 2f b9 35 b8 3f fb
7e fb c9 99 e9 2d 23 d3  24 2d 38 79 6d 90 6f 71
f5 0a bf 73 85 49 a5 95  a8 0c ac 07 46 f7 42 cd
f7 8d da d9 fb c9 b6 2f  fe 95 0d 51 8b 56 44 66
6a 74 dc 34 c0 ef d4 04  3f e7 61 8e f4 41 fe f2
eb 7a 71 60 41 4e 80 b2  84 47 cf 3b 5b b1 4b a3
3e fd 96 76 ac 50 8c 44  db 7e 22 60 e8 69 67 19
2a 5a 32 63 50 00 73 d4  e4 03 11 44 1b 75 fe 94
9c dc 8c f7 0d 7d 3e 51  f0 21 f9 62 78 06 6f 06
60 4c 6d dd 3e 38 4d 38  f0 d6 d5 67 4a 7c bb e3
7c 89 c9 44 50 67 33 dd  f3 ae 44 3c 0d c0 5d 8f
d9 59 8b d3 6e d3 6b 03  33 0f 84 d8 cf 27 64 fa
b1 be 23 51 bb a1 7f ce  fe 56 e8 ca b6 98 9f fb
16 51 21 47 9c 78 ff ff  53 0c 4c a0 9d 8a 99 7e
3f a2 ce b3 56 cf d5 fc  cf bd 09 73 e6 75 e4 cc
47 8c 5f aa 67 8e 23 ca  75 22 ab 22 2a 9c 7d f0
63 e4 38 fe 4a 13 13 37  8d 07 fe 5a 86 49 3b c8
a4 a5 b6 9c 40 6e 09 ac  e5 cb 6b 8c a6 57 3b 0a
a9 d5 bc 2b 6d 58 4f 7c  20 9d 21 29 68 8b 29 f1
bc 2c 3e e5 02 33 5c c5  dc 59 be 35 e6 f1 ba bc
60 0e 36 b8 ef 2c 73 d0  10 19 9c a5 9f 62 ef 8e
aa f8 b5 f8 b7 12 0e 39  ba 2c e6 84 8b 09 ca fb
1b d6 63 4c 18 6d ee 6f  6e 3a 0f 9b 4f 07 69 ff
c4 64 8d 7e fb 38 f4 81  4a 89 3e 31 42 f6 c0 f0
44 b3 15 4b dc de eb bc  d2 ec 20 0a 16 e9 3c ac
00 c7 b7 7e be 01 a0 e7  95 38 7b d8 36 df f6 fb
a3 5e 40 6f c8 0f 9b 46  55 54 02 6b 51 0c 86 8b
60 d1 c5 c6 70 b7 dd 8c  51 f2 7c 4f 9c 61 c3 b4
27 af 73 eb e8 f9 9e 91  30 a7 a9 c5 eb aa 17 65
bb 3c cf 1f 1b 80 dc af  58 5d 49 cc a1 c2 68 58
b0 9b f7 2e c7 e6 41 d3  d6 f8 fd 49 52 73 79 2a
a5 5f 60 c3 f5 da d2 04  f4 6a 9c 3c 59 f6 15 e3
c7 9a 1c 71 78 17 0d ce  d0 55 0f 70 9b 33 f0 38
2e 16 f3 04 36 89 84 a8  10 15 60 e8 0c a1 26 49
38 f1 9f 96 5e 31 65 2b  e3 ce ff bf 53 30 97 08
13 d7 50 00 f7 50 2b af  09 81 ce cf 57 ab d4 c6
";
        var salt = "11 12 91 bf 9e 86 af 7c  5c 47 a3 40 5a 17 d1 93";
        var iv = "b2 80 35 f9 ca 77 eb fe  45 42 82 4c d5 78 9d ff";
        var iterationCount = 2048;
        BagDecryptTest(password, data, salt, iv, iterationCount);
    }

    static void BagDecryptTest(string password, string data, string salt, string iv, int iterationCount)
    {
        using var hmac = new HMACSHA256(password.ToUtf8Bytes());
        var derivedKey = Pkcs5.PBKDF2(hmac, salt.RemoveLineBreak().RemoveSpace().FromHexString(), iterationCount, 32);
        using var aes = Aes.Create();
        aes.SetIV(iv.RemoveLineBreak().RemoveSpace().FromHexString());
        aes.SetKey(derivedKey);
        var decrypted = aes.Decrypt(data.RemoveLineBreak().RemoveSpace().FromHexString());
        Console.WriteLine(decrypted.Base64Encode());
    }
    #endregion
}
