using SharpDevLib.Cryptography.Internal.Pkcs;
using SharpDevLib.Cryptography.Pem;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib;

/// <summary>
/// X509证书扩展，提供证书生成、保存和转换功能
/// </summary>
public static class X509Helper
{
    /// <summary>
    /// 生成证书随机序列号
    /// </summary>
    /// <returns>8字节的随机序列号</returns>
    public static byte[] GenerateSerialNumber()
    {
        var bytes = new byte[8];
        var random = new Random();
        do
        {
            random.NextBytes(bytes);
        } while (bytes[0] == 0);//may cause exception:The first 9 bits of the integer value all have the same value. Ensure the input is in big-endian byte order and that all redundant leading bytes have been removed. (Parameter 'value')
        return bytes;
    }

    /// <summary>
    /// 生成X509证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥，PEM格式</param>
    /// <param name="issuerCert">颁发者证书</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="extensions">证书扩展集合</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的X509证书</returns>
    public static X509Certificate2 GenerateCert(string issuerPrivateKey, X509Certificate2 issuerCert, X509CertificateSigningRequest csr, byte[] serialNumber, int days, List<X509Extension> extensions, string? friendlyName = null) => csr.GenerateCert(issuerPrivateKey, issuerCert.SubjectName, serialNumber, days, extensions, friendlyName);

    /// <summary>
    /// 生成自签名X509证书
    /// </summary>
    /// <param name="privateKey">证书私钥，PEM格式</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="extensions">证书扩展集合</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的自签名X509证书</returns>
    public static X509Certificate2 GenerateSelfSignedCert(string privateKey, X509CertificateSigningRequest csr, byte[] serialNumber, int days, List<X509Extension> extensions, string? friendlyName = null)
    {
        return csr.GenerateSelfSignedCert(privateKey, serialNumber, days, extensions, friendlyName);
    }

    /// <summary>
    /// 生成CA（证书颁发机构）证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥，PEM格式</param>
    /// <param name="issuerCert">颁发者证书</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的CA证书</returns>
    public static X509Certificate2 GenerateCACert(string issuerPrivateKey, X509Certificate2 issuerCert, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateCert(issuerPrivateKey, issuerCert, csr, serialNumber, days, X509ExtensionHelper.CreateCAExtensions(csr.PublicKey, issuerCert), friendlyName);
    }

    /// <summary>
    /// 生成自签名CA（证书颁发机构）证书
    /// </summary>
    /// <param name="privateKey">证书私钥，PEM格式</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的自签名CA证书</returns>
    public static X509Certificate2 GenerateSelfSignedCACert(string privateKey, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateSelfSignedCert(privateKey, csr, serialNumber, days, X509ExtensionHelper.CreateCAExtensions(csr.PublicKey, null), friendlyName);
    }

    /// <summary>
    /// 生成服务器证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥，PEM格式</param>
    /// <param name="issuerCert">颁发者证书</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="alternativeNames">主题备用名称集合</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的服务器证书</returns>
    public static X509Certificate2 GenerateServerCert(string issuerPrivateKey, X509Certificate2 issuerCert, X509CertificateSigningRequest csr, byte[] serialNumber, int days, List<SubjectAlternativeName> alternativeNames, string? friendlyName = null)
    {
        return GenerateCert(issuerPrivateKey, issuerCert, csr, serialNumber, days, X509ExtensionHelper.CreateServerExtensions(csr.PublicKey, issuerCert, alternativeNames), friendlyName);
    }

    /// <summary>
    /// 生成自签名服务器证书
    /// </summary>
    /// <param name="privateKey">证书私钥，PEM格式</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="alternativeNames">主题备用名称集合</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的自签名服务器证书</returns>
    public static X509Certificate2 GenerateSelfSignedServerCert(string privateKey, X509CertificateSigningRequest csr, byte[] serialNumber, int days, List<SubjectAlternativeName> alternativeNames, string? friendlyName = null)
    {
        return GenerateSelfSignedCert(privateKey, csr, serialNumber, days, X509ExtensionHelper.CreateServerExtensions(csr.PublicKey, null, alternativeNames), friendlyName);
    }

    /// <summary>
    /// 生成客户端证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥，PEM格式</param>
    /// <param name="issuerCert">颁发者证书</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的客户端证书</returns>
    public static X509Certificate2 GenerateClientCert(string issuerPrivateKey, X509Certificate2 issuerCert, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateCert(issuerPrivateKey, issuerCert, csr, serialNumber, days, X509ExtensionHelper.CreateClientExtensions(csr.PublicKey, issuerCert), friendlyName);
    }

    /// <summary>
    /// 生成自签名客户端证书
    /// </summary>
    /// <param name="privateKey">证书私钥，PEM格式</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的自签名客户端证书</returns>
    public static X509Certificate2 GenerateSelfSignedClientCert(string privateKey, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateSelfSignedCert(privateKey, csr, serialNumber, days, X509ExtensionHelper.CreateClientExtensions(csr.PublicKey, null), friendlyName);
    }

    /// <summary>
    /// 生成代码签名证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥，PEM格式</param>
    /// <param name="issuerCert">颁发者证书</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的代码签名证书</returns>
    public static X509Certificate2 GenerateCodeSigningCert(string issuerPrivateKey, X509Certificate2 issuerCert, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateCert(issuerPrivateKey, issuerCert, csr, serialNumber, days, X509ExtensionHelper.CreateCodeSigningExtensions(csr.PublicKey, issuerCert), friendlyName);
    }

    /// <summary>
    /// 生成自签名代码签名证书
    /// </summary>
    /// <param name="privateKey">证书私钥，PEM格式</param>
    /// <param name="csr">证书签名请求</param>
    /// <param name="serialNumber">证书序列号</param>
    /// <param name="days">证书有效期（天数）</param>
    /// <param name="friendlyName">证书友好名称，可选</param>
    /// <returns>生成的自签名代码签名证书</returns>
    public static X509Certificate2 GenerateCodeSigningCert(string privateKey, X509CertificateSigningRequest csr, byte[] serialNumber, int days, string? friendlyName = null)
    {
        return GenerateSelfSignedCert(privateKey, csr, serialNumber, days, X509ExtensionHelper.CreateCodeSigningExtensions(csr.PublicKey, null), friendlyName);
    }

    /// <summary>
    /// 将证书以DER格式保存到文件流
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="stream">目标文件流</param>
    public static void SaveDer(this X509Certificate2 certificate, Stream stream)
    {
        var bytes = certificate.Export(X509ContentType.Cert);
        stream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// 将证书以DER格式保存到文件
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="path">目标文件路径</param>
    public static void SaveDer(this X509Certificate2 certificate, string path)
    {
        var bytes = certificate.Export(X509ContentType.Cert);
        bytes.SaveToFile(path);
    }

    /// <summary>
    /// 将证书以PEM格式的CRT保存到文件流
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="stream">目标文件流</param>
    public static void SaveCrt(this X509Certificate2 certificate, Stream stream)
    {
        var bytes = certificate.Export(X509ContentType.Cert);
        var pem = new PemObject(PemStatics.X509CertificateStart, Convert.ToBase64String(bytes), PemStatics.X509CertificateEnd, PemType.X509Certificate);
        var crt = pem.Write();
        var crtBytes = crt.Utf8Decode();
        stream.Write(crtBytes, 0, crtBytes.Length);
    }

    /// <summary>
    /// 将证书以PEM格式的CRT保存到文件
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="path">目标文件路径</param>
    public static void SaveCrt(this X509Certificate2 certificate, string path)
    {
        var bytes = certificate.Export(X509ContentType.Cert);
        var pem = new PemObject(PemStatics.X509CertificateStart, Convert.ToBase64String(bytes), PemStatics.X509CertificateEnd, PemType.X509Certificate);
        var crt = pem.Write();
        var crtBytes = crt.Utf8Decode();
        crtBytes.SaveToFile(path);
    }

    /// <summary>
    /// 将证书以PFX格式保存到文件流
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="stream">目标文件流</param>
    /// <param name="privateKey">私钥，PEM格式</param>
    /// <param name="password">PFX文件保护密码</param>
    public static void SavePfx(this X509Certificate2 certificate, Stream stream, string privateKey, string password)
    {
        using var rsa = RSA.Create();
        rsa.ImportPem(privateKey);
        certificate.PrivateKey = rsa;
        var bytes = certificate.Export(X509ContentType.Pfx, password);
        stream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// 将证书以PFX格式保存到文件
    /// </summary>
    /// <param name="certificate">要保存的证书</param>
    /// <param name="path">目标文件路径</param>
    /// <param name="privateKey">私钥，PEM格式</param>
    /// <param name="password">PFX文件保护密码</param>
    public static void SavePfx(this X509Certificate2 certificate, string path, string privateKey, string password)
    {
        var bytes = Pkcs12.Encode(certificate, privateKey, password);
        bytes.SaveToFile(path);
    }

    #region Internal
    internal static RSAParameters DecodeSubjectPublicInfo(byte[] key)
    {
        return Pkcs1.DecodePublicKey(DecodeSubjectPublicInfoBytes(key));
    }

    internal static byte[] DecodeSubjectPublicInfoBytes(byte[] key)
    {
        //rfc5280
        //SubjectPublicKeyInfo::= SEQUENCE  {
        //    algorithm AlgorithmIdentifier,
        //    subjectPublicKey     BIT STRING  }
        var reader = new AsnReader(key, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence();
        var algorithmSequence = sequence.ReadSequence();
        var algorithmIdentifier = algorithmSequence.ReadObjectIdentifier();
        if (algorithmIdentifier != "1.2.840.113549.1.1.1") throw new NotSupportedException("current only support rsa key");
        var publicKey = sequence.ReadBitString(out _);
        return publicKey;
    }

    internal static byte[] EncodeSubjectPublicKeyInfo(RSAParameters parameters)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.1.1");
        writer.WriteNull();
        writer.PopSequence();
        var publicKey = Pkcs1.EncodePublicKey(parameters);
        writer.WriteBitString(publicKey);
        writer.PopSequence();

        var length = writer.GetEncodedLength();
        var bytes = new byte[length];
        writer.Encode(bytes);
        return bytes;
    }

    internal static string GetPublicKey(this string privateKey)
    {
        using var keyRsa = RSA.Create();
        keyRsa.ImportPem(privateKey);
        return keyRsa.ExportPem(PemType.X509SubjectPublicKey);
    }
    #endregion
}
