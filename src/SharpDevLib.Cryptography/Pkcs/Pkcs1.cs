using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

internal static class Pkcs1
{
    //rfc8017
    internal static RSAParameters DecodePrivateKey(byte[] key)
    {
        //RSAPrivateKey::= SEQUENCE {
        //    version Version,
        //    modulus           INTEGER,  --n
        //     publicExponent INTEGER,  --e
        //     privateExponent INTEGER,  --d
        //     prime1 INTEGER,  --p
        //     prime2 INTEGER,  --q
        //     exponent1 INTEGER,  --d mod(p - 1)
        //     exponent2 INTEGER,  --d mod(q - 1)
        //     coefficient INTEGER,  --(inverse of q) mod p
        //     otherPrimeInfos OtherPrimeInfos OPTIONAL
        // }

        var reader = new AsnReader(key, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence();
        _ = sequence.ReadInteger();//version
        var module = sequence.ReadIntegerValue();
        var publicExponent = sequence.ReadIntegerValue();
        var privateExponent = sequence.ReadIntegerValue();
        var p = sequence.ReadIntegerValue();
        var q = sequence.ReadIntegerValue();
        var dp = sequence.ReadIntegerValue();
        var dq = sequence.ReadIntegerValue();
        var inverseQ = sequence.ReadIntegerValue();

        return new RSAParameters
        {
            Modulus = module,
            Exponent = publicExponent,
            D = privateExponent,
            P = p,
            Q = q,
            DP = dp,
            DQ = dq,
            InverseQ = inverseQ,
        };
    }

    static byte[] ReadIntegerValue(this AsnReader reader)
    {
        var tag = reader.PeekTag();
        if (tag != Asn1Tag.Integer) throw new InvalidDataException($"expected tag is '{Asn1Tag.Integer}',current is '{tag}'");
        var array = reader.ReadInteger().ToByteArray().Reverse();
        if (array.First() == 0x00 && array.Count() % 8 != 0) array = array.Skip(1);//https://stackoverflow.com/questions/48404917/rsa-private-key-pem-in-asn-1-format-contains-extra-bytes
        return array.ToArray();
    }

    internal static byte[] EncodePrivateKey(RSAParameters parameters)
    {
        try
        {
            var writer = new AsnWriter(AsnEncodingRules.DER);

            writer.PushSequence();
            writer.WriteInteger(0x00);//version
            writer.WriteIntegerValue(parameters.Modulus);//module
            writer.WriteIntegerValue(parameters.Exponent);//publicExponent
            writer.WriteIntegerValue(parameters.D);//privateExponent
            writer.WriteIntegerValue(parameters.P);//prime1
            writer.WriteIntegerValue(parameters.Q);//prime2
            writer.WriteIntegerValue(parameters.DP);//exponent1
            writer.WriteIntegerValue(parameters.DQ);//exponent2
            writer.WriteIntegerValue(parameters.InverseQ);//coefficient
            writer.PopSequence();

            var length = writer.GetEncodedLength();
            var bytes = new byte[length];
            writer.Encode(bytes);
            return bytes;
        }
        catch (Exception ex)
        {
            //todo：some bug
            throw ex;
        }
    }

    static void WriteIntegerValue(this AsnWriter writer, byte[] bytes)
    {
        if (bytes.First() < 128) writer.WriteInteger(bytes);
        else writer.WriteIntegerUnsigned(bytes);
    }

    internal static RSAParameters DecodePublicKey(byte[] key)
    {
        //RSAPublicKey::= SEQUENCE {
        //  modulus INTEGER,  --n
        //  publicExponent INTEGER   --e
        // }
        //The fields of type RSAPublicKey have the following meanings:
        //modulus is the RSA modulus n.
        //publicExponent is the RSA public exponent e.

        var reader = new AsnReader(key, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence();
        var module = sequence.ReadIntegerValue();
        var publicExponent = sequence.ReadIntegerValue();

        return new RSAParameters
        {
            Modulus = module,
            Exponent = publicExponent,
        };
    }

    internal static byte[] EncodePublicKey(RSAParameters parameters)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);

        writer.PushSequence();
        writer.WriteIntegerValue(parameters.Modulus);//module
        writer.WriteIntegerValue(parameters.Exponent);//publicExponent
        writer.PopSequence();

        var length = writer.GetEncodedLength();
        var bytes = new byte[length];
        writer.Encode(bytes);
        return bytes;
    }
}
