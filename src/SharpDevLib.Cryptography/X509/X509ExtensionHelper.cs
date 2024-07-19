using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

internal static class X509ExtensionHelper
{
    public static List<X509Extension> CreateCAExtensions(byte[] publicKey, X509Certificate2? caCert)
    {
        var extensions = new List<X509Extension>
        {
            new X509BasicConstraintsExtension(true, false, 0, false),
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, false),
            new X509EnhancedKeyUsageExtension(new OidCollection {
                Oid.FromOidValue("1.3.6.1.5.5.7.3.2", OidGroup.All),//Client Authentication
                Oid.FromOidValue("1.3.6.1.5.5.7.3.1", OidGroup.All),//Server Authentication
                Oid.FromOidValue("1.3.6.1.5.5.7.3.3", OidGroup.All),//Code Signing
                Oid.FromOidValue("1.3.6.1.4.1.311.10.3.4", OidGroup.All),//Encrypting File System
                Oid.FromOidValue("1.3.6.1.5.5.7.3.4", OidGroup.All),//Secure Email
                Oid.FromOidValue("1.3.6.1.5.5.7.3.7", OidGroup.All),//IP security user
                Oid.FromOidValue("1.3.6.1.5.5.7.3.6", OidGroup.All),//IP security tunnel termination
                Oid.FromOidValue("1.3.6.1.5.5.7.3.8", OidGroup.All),//Time Stamping
            }, false),
        };
        extensions.AddRange(GetKeyIdentifierExtension(publicKey, caCert));
        return extensions;
    }

    public static List<X509Extension> CreateServerExtensions(byte[] publicKey, X509Certificate2? caCert, List<SubjectAlternativeName> alternativeNames)
    {

        var extensions = new List<X509Extension>
        {
            new X509BasicConstraintsExtension(false, false, 0, false),
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false),
            new X509EnhancedKeyUsageExtension(new OidCollection { Oid.FromOidValue("1.3.6.1.5.5.7.3.1", OidGroup.All) }, false),
        };
        extensions.AddRange(GetKeyIdentifierExtension(publicKey, caCert));
        extensions.Add(GetSubjectAlternativeNameExtension(alternativeNames));
        return extensions;
    }

    public static List<X509Extension> CreateClientExtensions(byte[] publicKey, X509Certificate2? caCert)
    {
        var extensions = new List<X509Extension>
        {
            new X509BasicConstraintsExtension(false, false, 0, false),
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false),
            new X509EnhancedKeyUsageExtension(new OidCollection { Oid.FromOidValue("1.3.6.1.5.5.7.3.2", OidGroup.All) }, false),
        };
        extensions.AddRange(GetKeyIdentifierExtension(publicKey, caCert));
        return extensions;
    }

    /// <summary>
    /// 获取使用者密钥标识符和授权密钥标识符扩展
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="caCert">ca证书</param>
    /// <returns>使用者密钥标识符和授权密钥标识符扩展集合</returns>
    static List<X509Extension> GetKeyIdentifierExtension(byte[] publicKey, X509Certificate2? caCert)
    {
        //使用者密钥标识
        var subjectKeyIdentifierExtension = CreateSubjectKeyIdentifierExtension(publicKey);
        if (caCert is null) return new List<X509Extension> { subjectKeyIdentifierExtension };

        var subjectKeyIdentifierRawData = caCert.Extensions.OfType<X509SubjectKeyIdentifierExtension>().FirstOrDefault()?.RawData ?? throw new Exception("unable to find ca X509SubjectKeyIdentifierExtension");
        //授权密钥标识符
        var authorityKeyIdentifierExtension = new X509AuthorityKeyIdentifierExtension(subjectKeyIdentifierRawData, false);
        return new List<X509Extension> { subjectKeyIdentifierExtension, authorityKeyIdentifierExtension };
    }

    static X509SubjectKeyIdentifierExtension CreateSubjectKeyIdentifierExtension(byte[] publicKey)
    {
        var keyValue = new AsnEncodedData(publicKey);
        var keyParam = new AsnEncodedData(new byte[] { 05, 00 });
        var key = new PublicKey(new Oid(Oids.Rsa), keyParam, keyValue);
        return new X509SubjectKeyIdentifierExtension(key, false);
    }

    static X509Extension GetSubjectAlternativeNameExtension(List<SubjectAlternativeName> alternativeNames)
    {
        if (alternativeNames.IsNullOrEmpty()) throw new ArgumentException($"argument '{nameof(alternativeNames)}' can not be empty");

        var subjectAlternativeNameBuilder = new SubjectAlternativeNameBuilder();
        foreach (var item in alternativeNames)
        {
            if (item.Type == SubjectAlternativeNameType.Uri) subjectAlternativeNameBuilder.AddUri(new Uri(item.Value));
            else if (item.Type == SubjectAlternativeNameType.Email) subjectAlternativeNameBuilder.AddEmailAddress(item.Value);
            else if (item.Type == SubjectAlternativeNameType.UPN) subjectAlternativeNameBuilder.AddUserPrincipalName(item.Value);
            else if (item.Type == SubjectAlternativeNameType.Dns) subjectAlternativeNameBuilder.AddDnsName(item.Value);
            else if (item.Type == SubjectAlternativeNameType.IP) subjectAlternativeNameBuilder.AddIpAddress(IPAddress.Parse(item.Value));
            else throw new NotSupportedException($"subject alternative name type [{item.Type}] not supported");
        }
        return subjectAlternativeNameBuilder.Build(false);
    }
}
