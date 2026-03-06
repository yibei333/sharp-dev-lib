using SharpDevLib.Cryptography.Internal;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib;

internal class X509AuthorityKeyIdentifierExtension(byte[] subjectKeyIdentifierRawData, bool critical) : X509Extension(Oids.AuthorityKeyIdentifier, EncodeExtension(subjectKeyIdentifierRawData), critical)
{
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
