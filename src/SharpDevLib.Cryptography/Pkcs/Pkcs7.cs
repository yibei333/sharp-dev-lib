using System.Formats.Asn1;

namespace SharpDevLib.Cryptography;

//rfc2315
internal static class Pkcs7
{
    //pkcs-7 OBJECT IDENTIFIER ::=
    //{
    //     iso(1) member - body(2) US(840) rsadsi(113549)
    //      pkcs(1) 7 }
    // The object identifiers data, signedData, envelopedData,
    //signedAndEnvelopedData, digestedData, and encryptedData, identify,
    //respectively, the data, signed-data, enveloped-data, signed-and-
    //enveloped-data, digested-data, and encrypted-data content types
    //defined in Sections 8-13.
    //data OBJECT IDENTIFIER::= { pkcs-7 1 }
    //signedData OBJECT IDENTIFIER::= { pkcs-7 2 }
    //envelopedData OBJECT IDENTIFIER ::= { pkcs-7 3 }
    //signedAndEnvelopedData OBJECT IDENTIFIER ::=
    //   { pkcs-7 4 }
    //digestedData OBJECT IDENTIFIER ::= { pkcs-7 5 }
    //encryptedData OBJECT IDENTIFIER ::= { pkcs-7 6 }
    static readonly Dictionary<Pkcs7ContentType, string> _contentTypeIdentifier = new()
    {
        { Pkcs7ContentType.Data,"1.2.840.113549.1.7.1"},
        { Pkcs7ContentType.SignedData,"1.2.840.113549.1.7.2"},
        { Pkcs7ContentType.EnvelopedData,"1.2.840.113549.1.7.3"},
        { Pkcs7ContentType.SignedAndEnvelopedData,"1.2.840.113549.1.7.4"},
        { Pkcs7ContentType.DigestedData,"1.2.840.113549.1.7.5"},
        { Pkcs7ContentType.EncryptedData,"1.2.840.113549.1.7.6"},
    };

    public static string GetOidByType(Pkcs7ContentType type) => _contentTypeIdentifier[type];

    public static byte[] Encode(Pkcs7ContentType contentType, byte[] content)
    {
        //ContentInfo::= SEQUENCE {
        //    contentType ContentType,
        //    content
        //      [0] EXPLICIT ANY DEFINED BY contentType OPTIONAL }

        //ContentType::= OBJECT IDENTIFIER

        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.WriteObjectIdentifier(_contentTypeIdentifier[contentType]);
        WriteContent(writer, contentType, content);
        writer.PopSequence();
        return writer.Encode();
    }

    public static void WriteContent(AsnWriter writer, Pkcs7ContentType contentType, byte[] content)
    {
        if (contentType == Pkcs7ContentType.Data)
        {
            var contentTag = new Asn1Tag(TagClass.ContextSpecific, 0);
            writer.PushSequence(contentTag);
            writer.PushOctetString();
            writer.WriteEncodedValue(content);
            writer.PopOctetString();
            writer.PopSequence(contentTag);
        }
        else if (contentType == Pkcs7ContentType.EncryptedData)
        {
            var contentTag = new Asn1Tag(TagClass.ContextSpecific, 0);
            writer.PushSequence(contentTag);

            //EncryptedData::= SEQUENCE {
            //    version Version,
            //    encryptedContentInfo EncryptedContentInfo }
            writer.PushSequence();

            //1.version
            writer.WriteInteger(0);

            //2.encryptedContentInfo
            writer.WriteEncodedValue(content);

            writer.PopSequence();
            writer.PopSequence(contentTag);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}

internal enum Pkcs7ContentType
{
    Data,
    SignedData,
    EnvelopedData,
    SignedAndEnvelopedData,
    DigestedData,
    EncryptedData
}