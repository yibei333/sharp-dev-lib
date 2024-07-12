using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

internal class TBSCertificate
{
    public TBSCertificate(byte[] serialNumber, X500DistinguishedName issuer, int days, X509Subject subject, string subjectPublicKey, List<X509Extension> extensions)
    {
        Version = 2;
        SerialNumber = serialNumber;
        SignatureAlgorithmIdentifier = "1.2.840.113549.1.1.11";//sha256rsa
        Issuer = issuer;
        NotBefore = DateTimeOffset.Now;
        NotAfter = DateTimeOffset.Now.AddDays(days);
        Subject = subject;
        SubjectPublicKey = subjectPublicKey;
        Extensions = extensions;
    }

    public int Version { get; }
    public byte[] SerialNumber { get; }
    public string SignatureAlgorithmIdentifier { get; }
    public X500DistinguishedName Issuer { get; }
    public DateTimeOffset NotBefore { get; }
    public DateTimeOffset NotAfter { get; }
    public X509Subject Subject { get; }
    public string SubjectPublicKey { get; }
    public List<X509Extension> Extensions { get; }


    public byte[] Encode()
    {
        //TBSCertificate  ::=  SEQUENCE  {
        //     version         [0]  Version DEFAULT v1,
        //     serialNumber         CertificateSerialNumber,
        //     signature            AlgorithmIdentifier,
        //     issuer               Name,
        //     validity             Validity,
        //     subject              Name,
        //     subjectPublicKeyInfo SubjectPublicKeyInfo,
        //     issuerUniqueID  [1]  IMPLICIT UniqueIdentifier OPTIONAL,
        //                          -- If present, version MUST be v2 or v3
        //     subjectUniqueID [2]  IMPLICIT UniqueIdentifier OPTIONAL,
        //                          -- If present, version MUST be v2 or v3
        //     extensions      [3]  Extensions OPTIONAL
        //                          -- If present, version MUST be v3 --  }
        //Version  ::=  INTEGER  {  v1(0), v2(1), v3(2)  }
        //CertificateSerialNumber  ::=  INTEGER
        //Validity ::= SEQUENCE {
        //     notBefore      Time,
        //     notAfter       Time  }
        //Time ::= CHOICE {
        //     utcTime        UTCTime,
        //     generalTime    GeneralizedTime }
        //UniqueIdentifier  ::=  BIT STRING
        //SubjectPublicKeyInfo  ::=  SEQUENCE  {
        //     algorithm            AlgorithmIdentifier,
        //     subjectPublicKey     BIT STRING  }
        //Extensions  ::=  SEQUENCE SIZE (1..MAX) OF Extension
        //Extension  ::=  SEQUENCE  {
        //     extnID      OBJECT IDENTIFIER,
        //     critical    BOOLEAN DEFAULT FALSE,
        //     extnValue   OCTET STRING
        //                 -- contains the DER encoding of an ASN.1 value
        //                 -- corresponding to the extension type identified
        //                 -- by extnID
        //     }

        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();

        //version
        var versionTag = new Asn1Tag(TagClass.ContextSpecific, 0);
        writer.PushSequence(versionTag);
        writer.WriteInteger(Version);
        writer.PopSequence(versionTag);

        //serialNumber
        writer.WriteInteger(SerialNumber);

        //signature
        writer.PushSequence();
        writer.WriteObjectIdentifier(SignatureAlgorithmIdentifier);
        writer.WriteNull();
        writer.PopSequence();

        //issuer
        writer.WriteEncodedValue(Issuer.RawData);

        //validity
        writer.PushSequence();
        writer.WriteGeneralizedTime(NotBefore);
        writer.WriteGeneralizedTime(NotAfter);
        writer.PopSequence();

        //subject
        writer.WriteEncodedValue(Subject.CreateX500DistinguishedName().RawData);

        //subjectPublicKeyInfo
        using var rsa = RSA.Create();
        rsa.ImportPem(SubjectPublicKey);
        var key = X509.EncodeSubjectPublicKeyInfo(rsa.ExportParameters(false));
        writer.WriteEncodedValue(key);

        //extensions
        if (Extensions.NotNullOrEmpty())
        {
            var extensionTag = new Asn1Tag(TagClass.ContextSpecific, 3);
            writer.PushSequence(extensionTag);

            writer.PushSequence();
            Extensions.ForEach(extension =>
            {
                writer.PushSequence();
                writer.WriteObjectIdentifier(extension.Oid.Value);
                writer.PushOctetString();
                writer.WriteEncodedValue(extension.RawData);
                writer.PopOctetString();
                writer.PopSequence();
            });
            writer.PopSequence();

            writer.PopSequence(extensionTag);
        }

        writer.PopSequence();
        return writer.Encode();
    }
}
