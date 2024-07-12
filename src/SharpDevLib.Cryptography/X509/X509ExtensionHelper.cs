using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

internal static class X509ExtensionHelper
{
    public static List<X509Extension> CreateCAExtensions(string publicKey, X509Certificate2? caCert)
    {
        var subjectKeyIdentifierExtension = CreateSubjectKeyIdentifierExtension(publicKey);
        var subjectKeyIdentifierRawData = subjectKeyIdentifierExtension.RawData;
        if (caCert is not null) subjectKeyIdentifierRawData = caCert.Extensions.OfType<X509SubjectKeyIdentifierExtension>().FirstOrDefault()?.RawData;
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
            subjectKeyIdentifierExtension,
            new X509AuthorityKeyIdentifierExtension(subjectKeyIdentifierRawData,false)
        };
        return extensions;
    }

    public static List<X509Extension> CreateServerExtensions(List<SubjectAlternativeName> alternativeNames)
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
        var subjectAlternativeNameExtension = subjectAlternativeNameBuilder.Build();
        var extensions = new List<X509Extension>
        {
            new X509BasicConstraintsExtension(false, false, 0, false),
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false),
            new X509EnhancedKeyUsageExtension(new OidCollection { Oid.FromOidValue("1.3.6.1.5.5.7.3.1", OidGroup.All) }, false),
            subjectAlternativeNameExtension
        };
        return extensions;
    }

    public static List<X509Extension> CreateClientExtensions()
    {
        var extensions = new List<X509Extension>
        {
           new X509BasicConstraintsExtension(false, false, 0, false),
            new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false),
            new X509EnhancedKeyUsageExtension(new OidCollection { Oid.FromOidValue("1.3.6.1.5.5.7.3.2", OidGroup.All) }, false),
        };
        return extensions;
    }

    static X509SubjectKeyIdentifierExtension CreateSubjectKeyIdentifierExtension(string publicKey)
    {
        var pem = PemObject.Read(publicKey);
        var keyValue = new AsnEncodedData(Convert.FromBase64String(pem.Body));
        var keyParam = new AsnEncodedData(new byte[] { 05, 00 });
        var key = new PublicKey(new Oid(Oids.Rsa), keyParam, keyValue);
        return new X509SubjectKeyIdentifierExtension(key, false);
    }
}
