using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

internal static class X509
{
    public static RSAParameters DecodeSubjectPublicInfo(byte[] key)
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
        return Pkcs1.DecodePublicKey(publicKey);
    }

    public static byte[] EncodeSubjectPublicKeyInfo(RSAParameters parameters)
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
}
