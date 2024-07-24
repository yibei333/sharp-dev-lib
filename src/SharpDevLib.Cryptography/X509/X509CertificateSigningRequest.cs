using SharpDevLib.Cryptography.Internal.References;
using SharpDevLib.Cryptography.Pem;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 证书签名请求
/// </summary>
public class X509CertificateSigningRequest
{
    //rfc2986
    // CertificationRequest ::= SEQUENCE {
    //     certificationRequestInfo CertificationRequestInfo,
    //     signatureAlgorithm AlgorithmIdentifier{{ SignatureAlgorithms }},
    //     signature          BIT STRING
    //}
    //    CertificationRequestInfo::= SEQUENCE {
    //        version INTEGER { v1(0) }
    //(v1,...),
    //        subject Name,
    //        subjectPKInfo SubjectPublicKeyInfo{{ PKInfoAlgorithms }},
    //        attributes[0] Attributes{{ CRIAttributes }}
    //   }

    /// <summary>
    /// 实例化证书签名请求
    /// </summary>
    /// <param name="subject">subject</param>
    /// <param name="privateKey">私钥,当前仅支持RSA</param>
    public X509CertificateSigningRequest(string subject, string privateKey)
    {
        Subject = new X500DistinguishedName(subject);
        using var rsa = RSA.Create();
        rsa.ImportPem(privateKey);
        PublicKey = Convert.FromBase64String(PemObject.Read(rsa.ExportPem(PemType.X509SubjectPublicKey)).Body);

        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.WriteIntegerValue(0);
        writer.WriteEncodedValue(Subject.RawData);
        writer.WriteEncodedValue(PublicKey);
        writer.WriteNull(new Asn1Tag(TagClass.ContextSpecific, 0));
        writer.PopSequence();
        CertificationRequestInfo = writer.Encode();

        var hashAlgorithm = SHA256.Create();
        var hash = hashAlgorithm.ComputeHash(CertificationRequestInfo);
        Signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    internal X509CertificateSigningRequest(X500DistinguishedName subject, byte[] publicKey, byte[] signature, byte[] certificationRequestInfo)
    {
        Subject = subject;
        PublicKey = publicKey;
        Signature = signature;
        CertificationRequestInfo = certificationRequestInfo;
    }

    /// <summary>
    /// subject
    /// </summary>
    public X500DistinguishedName Subject { get; }

    /// <summary>
    /// 公钥
    /// </summary>
    public byte[] PublicKey { get; }

    /// <summary>
    /// 签名
    /// </summary>
    public byte[] Signature { get; }

    /// <summary>
    /// 证书请求信息
    /// </summary>
    public byte[] CertificationRequestInfo { get; }

    /// <summary>
    /// 导入
    /// </summary>
    /// <param name="pemText">pem格式的请求</param>
    /// <returns>证书签名请求</returns>
    public static X509CertificateSigningRequest Import(string pemText)
    {
        var pem = PemObject.Read(pemText);
        var body = Convert.FromBase64String(pem.Body);
        var asnReader = new AsnReader(body, AsnEncodingRules.DER);
        var sequence = asnReader.ReadSequence();

        //certificationRequestInfo
        var infoRawData = sequence.PeekEncodedValue().ToArray();
        var infoSequence = sequence.ReadSequence();
        _ = infoSequence.ReadInteger();//version
        var subject = new X500DistinguishedName(infoSequence.ReadEncodedValue().ToArray());//subject
        var publicKey = infoSequence.PeekEncodedValue().ToArray();

        //signatureAlgorithm
        _ = sequence.ReadSequence();//current only support sha256rsa

        //signature
        var signature = sequence.ReadBitString(out _);
        return new X509CertificateSigningRequest(subject, publicKey, signature, infoRawData);
    }

    /// <summary>
    /// 导出PEM格式的请求
    /// </summary>
    /// <returns>PEM格式的请求</returns>
    public string Export()
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.WriteEncodedValue(CertificationRequestInfo);
        writer.PushSequence();
        writer.WriteObjectIdentifier(Oids.RsaPkcs1Sha256);
        writer.WriteNull();
        writer.PopSequence();
        writer.WriteBitString(Signature, 0);
        writer.PopSequence();
        var bytes = writer.Encode();
        var body = Convert.ToBase64String(bytes);
        var pemObject = new PemObject(PemStatics.X509CertificateSigningRequestStart, body, PemStatics.X509CertificateSigningRequestEnd, PemType.X509CertificateSigningRequest);
        return pemObject.Write();
    }

    /// <summary>
    /// 生成证书
    /// </summary>
    /// <param name="issuerPrivateKey">颁发者私钥</param>
    /// <param name="issuer">颁发者</param>
    /// <param name="serialNumber">序列号</param>
    /// <param name="days">过期天数</param>
    /// <param name="extensions">扩展集合</param>
    /// <param name="friendlyName">友好名称</param>
    /// <returns>X509Certificate2</returns>
    public X509Certificate2 GenerateCert(string issuerPrivateKey, X500DistinguishedName issuer, byte[] serialNumber, int days, List<X509Extension> extensions, string? friendlyName = null)
    {
        var tbsCertificate = new TBSCertificate(serialNumber, issuer, days, Subject, PublicKey, extensions);
        var tbsCertificateBytes = tbsCertificate.Encode();
        using var hashAlgorithm = SHA256.Create();
        var hash = hashAlgorithm.ComputeHash(tbsCertificateBytes);
        using var rsa = RSA.Create();
        rsa.ImportPem(issuerPrivateKey);
        var signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        //Certificate  ::=  SEQUENCE  {
        //     tbsCertificate       TBSCertificate,
        //     signatureAlgorithm   AlgorithmIdentifier,
        //     signature            BIT STRING  }
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.WriteEncodedValue(tbsCertificateBytes);
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.1.11");//sha256rsa
        writer.WriteNull();
        writer.PopSequence();
        writer.WriteBitString(signature, 0);
        writer.PopSequence();
        var rawData = writer.Encode();
        var cert = new X509Certificate2(rawData);
        if (friendlyName.NotNullOrWhiteSpace()) cert.FriendlyName = friendlyName;
        return cert;
    }

    /// <summary>
    /// 生成自签名证书
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="serialNumber">序列号</param>
    /// <param name="days">过期天数</param>
    /// <param name="extensions">扩展集合</param>
    /// <param name="friendlyName">友好名称</param>
    /// <returns>X509Certificate2</returns>    
    public X509Certificate2 GenerateSelfSignedCert(string privateKey, byte[] serialNumber, int days, List<X509Extension> extensions, string? friendlyName = null)
    {
        return GenerateCert(privateKey, Subject, serialNumber, days, extensions, friendlyName);
    }
}
