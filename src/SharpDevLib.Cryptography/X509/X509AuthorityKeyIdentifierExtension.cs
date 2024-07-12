using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

internal class X509AuthorityKeyIdentifierExtension : X509Extension
{
    public X509AuthorityKeyIdentifierExtension(byte[] subjectKeyIdentifierRawData, bool critical) : base(Oids.AuthorityKeyIdentifier, EncodeExtension(subjectKeyIdentifierRawData), critical)
    {
    }

    static byte[] EncodeExtension(byte[] subjectKeyIdentifierRawData)
    {
        var segment = new ArraySegment<byte>(subjectKeyIdentifierRawData, 2, subjectKeyIdentifierRawData.Length - 2);
        var authorityKeyIdentifier = new byte[segment.Count + 4];
        authorityKeyIdentifier[0] = 0x30;
        authorityKeyIdentifier[1] = 0x16;
        authorityKeyIdentifier[2] = 0x80;
        authorityKeyIdentifier[3] = 0x14;
        segment.ToArray().CopyTo(authorityKeyIdentifier, 4);
        return authorityKeyIdentifier;
    }
}
