using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SharpDevLib.Cryptography;

internal sealed class SubjectAlternativeNameBuilder
{
    private static readonly IdnMapping s_idnMapping = new();

    // Because GeneralNames is a SEQUENCE, just make a rolling list, it doesn't need to be re-sorted.
    private readonly List<byte[]> _encodedNames = new();

    public void AddEmailAddress(string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress))
            throw new ArgumentOutOfRangeException(nameof(emailAddress), SR.Arg_EmptyOrNullString);

        AddGeneralName(new GeneralNameAsn { Rfc822Name = emailAddress });
    }

    public void AddDnsName(string dnsName)
    {
        if (string.IsNullOrEmpty(dnsName))
            throw new ArgumentOutOfRangeException(nameof(dnsName), SR.Arg_EmptyOrNullString);

        AddGeneralName(new GeneralNameAsn { DnsName = s_idnMapping.GetAscii(dnsName) });
    }

    public void AddUri(Uri uri)
    {
        if (uri is null) throw new ArgumentNullException();
        AddGeneralName(new GeneralNameAsn { Uri = uri.AbsoluteUri.ToString() });
    }

    public void AddIpAddress(IPAddress ipAddress)
    {
        if (ipAddress is null) throw new ArgumentNullException();
        AddGeneralName(new GeneralNameAsn { IPAddress = ipAddress.GetAddressBytes() });
    }

    public void AddUserPrincipalName(string upn)
    {
        if (string.IsNullOrEmpty(upn))
            throw new ArgumentOutOfRangeException(nameof(upn), SR.Arg_EmptyOrNullString);

        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.WriteCharacterString(UniversalTagNumber.UTF8String, upn);
        byte[] otherNameValue = writer.Encode();

        var otherName = new OtherNameAsn
        {
            TypeId = Oids.UserPrincipalName,
            Value = otherNameValue,
        };

        AddGeneralName(new GeneralNameAsn { OtherName = otherName });
    }

    public X509Extension Build(bool critical = false)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);

        using (writer.PushSequence())
        {
            foreach (byte[] encodedName in _encodedNames)
            {
                writer.WriteEncodedValue(encodedName);
            }
        }

        return new X509Extension(
            Oids.SubjectAltName,
            writer.Encode(),
            critical);
    }

    private void AddGeneralName(GeneralNameAsn generalName)
    {
        try
        {
            // Verify that the general name can be serialized and store it.
            var writer = new AsnWriter(AsnEncodingRules.DER);
            generalName.Encode(writer);
            _encodedNames.Add(writer.Encode());
        }
        catch (EncoderFallbackException)
        {
            throw new CryptographicException(SR.Cryptography_Invalid_IA5String);
        }
    }
}

/// <summary>
/// https://www.rfc-editor.org/rfc/rfc5280.html#section-4.2.1.6
/// </summary>
internal struct GeneralNameAsn
{
    internal OtherNameAsn? OtherName = null;
    internal string? Rfc822Name = null;
    internal string? DnsName = null;
    internal ReadOnlyMemory<byte>? X400Address = null;
    internal ReadOnlyMemory<byte>? DirectoryName = null;
    internal EdiPartyNameAsn? EdiPartyName = null;
    internal ReadOnlyMemory<byte>? IPAddress = null;
    internal string? RegisteredId = null;
    internal string? Uri = null;

    public GeneralNameAsn()
    {
    }

#if DEBUG
    static GeneralNameAsn()
    {
        var usedTags = new Dictionary<Asn1Tag, string>();
        void ensureUniqueTag(Asn1Tag tag, string fieldName)
        {
            if (usedTags.TryGetValue(tag, out string? existing))
            {
                throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
            }

            usedTags.Add(tag, fieldName);
        }

        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 0), "OtherName");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 1), "Rfc822Name");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 2), "DnsName");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 3), "X400Address");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 4), "DirectoryName");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 5), "EdiPartyName");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 6), "Uri");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 7), "IPAddress");
        ensureUniqueTag(new Asn1Tag(TagClass.ContextSpecific, 8), "RegisteredId");
    }
#endif

    internal readonly void Encode(AsnWriter writer)
    {
        bool wroteValue = false;

        if (OtherName.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            OtherName.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 0));
            wroteValue = true;
        }

        if (Rfc822Name != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.IA5String, Rfc822Name, new Asn1Tag(TagClass.ContextSpecific, 1));
            wroteValue = true;
        }

        if (DnsName != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.IA5String, DnsName, new Asn1Tag(TagClass.ContextSpecific, 2));
            wroteValue = true;
        }

        if (X400Address.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            // Validator for tag constraint for X400Address
            {
                if (!Asn1Tag.TryDecode(X400Address.Value.Span, out Asn1Tag validateTag, out _) ||
                    !validateTag.HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 3)))
                {
                    throw new CryptographicException();
                }
            }

            try
            {
                writer.WriteEncodedValue(X400Address.Value.Span);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
            }
            wroteValue = true;
        }

        if (DirectoryName.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 4));
            try
            {
                writer.WriteEncodedValue(DirectoryName.Value.Span);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
            }
            writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 4));
            wroteValue = true;
        }

        if (EdiPartyName.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            EdiPartyName.Value.Encode(writer, new Asn1Tag(TagClass.ContextSpecific, 5));
            wroteValue = true;
        }

        if (Uri != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.IA5String, Uri, new Asn1Tag(TagClass.ContextSpecific, 6));
            wroteValue = true;
        }

        if (IPAddress.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteOctetString(IPAddress.Value.Span, new Asn1Tag(TagClass.ContextSpecific, 7));
            wroteValue = true;
        }

        if (RegisteredId != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            try
            {
                writer.WriteObjectIdentifier(RegisteredId, new Asn1Tag(TagClass.ContextSpecific, 8));
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
            }
            wroteValue = true;
        }

        if (!wroteValue)
        {
            throw new CryptographicException();
        }
    }
}

internal partial struct OtherNameAsn
{
    internal string TypeId;
    internal ReadOnlyMemory<byte> Value;

    internal readonly void Encode(AsnWriter writer)
    {
        Encode(writer, Asn1Tag.Sequence);
    }

    internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
    {
        writer.PushSequence(tag);

        try
        {
            writer.WriteObjectIdentifier(TypeId);
        }
        catch (ArgumentException e)
        {
            throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
        }
        writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        try
        {
            writer.WriteEncodedValue(Value.Span);
        }
        catch (ArgumentException e)
        {
            throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
        }
        writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        writer.PopSequence(tag);
    }
}

internal partial struct EdiPartyNameAsn
{
    internal DirectoryStringAsn? NameAssigner = null;
    internal DirectoryStringAsn PartyName = default;

    public EdiPartyNameAsn()
    {
    }

    internal readonly void Encode(AsnWriter writer)
    {
        Encode(writer, Asn1Tag.Sequence);
    }

    internal readonly void Encode(AsnWriter writer, Asn1Tag tag)
    {
        writer.PushSequence(tag);


        if (NameAssigner.HasValue)
        {
            writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
            NameAssigner.Value.Encode(writer);
            writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 0));
        }

        writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
        PartyName.Encode(writer);
        writer.PopSequence(new Asn1Tag(TagClass.ContextSpecific, 1));
        writer.PopSequence(tag);
    }
}

internal partial struct DirectoryStringAsn
{
    internal string? TeletexString = null;
    internal string? PrintableString = null;
    internal ReadOnlyMemory<byte>? UniversalString = null;
    internal string? Utf8String = null;
    internal string? BmpString = null;

    public DirectoryStringAsn()
    {
    }

#if DEBUG
    static DirectoryStringAsn()
    {
        var usedTags = new Dictionary<Asn1Tag, string>();
        void ensureUniqueTag(Asn1Tag tag, string fieldName)
        {
            if (usedTags.TryGetValue(tag, out string? existing))
            {
                throw new InvalidOperationException($"Tag '{tag}' is in use by both '{existing}' and '{fieldName}'");
            }

            usedTags.Add(tag, fieldName);
        }

        ensureUniqueTag(new Asn1Tag(UniversalTagNumber.T61String), "TeletexString");
        ensureUniqueTag(new Asn1Tag(UniversalTagNumber.PrintableString), "PrintableString");
        ensureUniqueTag(new Asn1Tag((UniversalTagNumber)28), "UniversalString");
        ensureUniqueTag(new Asn1Tag(UniversalTagNumber.UTF8String), "Utf8String");
        ensureUniqueTag(new Asn1Tag(UniversalTagNumber.BMPString), "BmpString");
    }
#endif

    internal readonly void Encode(AsnWriter writer)
    {
        bool wroteValue = false;

        if (TeletexString != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.T61String, TeletexString);
            wroteValue = true;
        }

        if (PrintableString != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.PrintableString, PrintableString);
            wroteValue = true;
        }

        if (UniversalString.HasValue)
        {
            if (wroteValue)
                throw new CryptographicException();

            // Validator for tag constraint for UniversalString
            {
                if (!Asn1Tag.TryDecode(UniversalString.Value.Span, out Asn1Tag validateTag, out _) ||
                    !validateTag.HasSameClassAndValue(new Asn1Tag((UniversalTagNumber)28)))
                {
                    throw new CryptographicException();
                }
            }

            try
            {
                writer.WriteEncodedValue(UniversalString.Value.Span);
            }
            catch (ArgumentException e)
            {
                throw new CryptographicException(SR.Cryptography_Der_Invalid_Encoding, e);
            }
            wroteValue = true;
        }

        if (Utf8String != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.UTF8String, Utf8String);
            wroteValue = true;
        }

        if (BmpString != null)
        {
            if (wroteValue)
                throw new CryptographicException();

            writer.WriteCharacterString(UniversalTagNumber.BMPString, BmpString);
            wroteValue = true;
        }

        if (!wroteValue)
        {
            throw new CryptographicException();
        }
    }
}

internal class SR
{
    public const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";
    public const string Arg_EmptyOrNullString = "String cannot be empty or null.";
    public const string Cryptography_Invalid_IA5String = "The string contains a character not in the 7 bit ASCII character set.";
}
