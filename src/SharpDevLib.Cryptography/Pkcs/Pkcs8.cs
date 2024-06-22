using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SharpDevLib.Cryptography;

//rfc5958
internal class Pkcs8
{
    public static byte[] DecodePrivateKeyInfo(byte[] bytes)
    {
        //PrivateKeyInfo ::= OneAsymmetricKey
        //-- PrivateKeyInfo is used by [P12].  If any items tagged as version
        //-- 2 are used, the version must be v2, else the version should be
        //-- v1.  When v1, PrivateKeyInfo is the same as it was in [RFC5208].
        //Version ::= INTEGER { v1(0), v2(1) } (v1, ..., v2)
        //PrivateKeyAlgorithmIdentifier ::= AlgorithmIdentifier
        //                                   { PUBLIC-KEY,
        //                                     { PrivateKeyAlgorithms } }
        //PrivateKey ::= OCTET STRING
        //                   -- Content varies based on type of key.  The
        //                   -- algorithm identifier dictates the format of
        //                   -- the key.
        //PublicKey ::= BIT STRING
        //                   -- Content varies based on type of key.  The
        //                   -- algorithm identifier dictates the format of
        //                   -- the key.
        //Attributes ::= SET OF Attribute { { OneAsymmetricKeyAttributes } }

        var reader = new AsnReader(bytes, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence();
        var version = sequence.ReadInteger();

        //1.version
        if (version != 0) throw new NotSupportedException("current only support v1");

        //2.PrivateKeyAlgorithmIdentifier
        var privateKeyAlgorithmIdentifierSequence = sequence.ReadSequence();
        var privateKeyAlgorithmIdentifier = privateKeyAlgorithmIdentifierSequence.ReadObjectIdentifier();

        //rfc3447
        //-- ============================
        //--   Basic object identifiers
        //-- ============================
        //-- The DER encoding of this in hexadecimal is:
        //-- (0x)06 08
        //--        2A 86 48 86 F7 0D 01 01
        //--
        //pkcs-1    OBJECT IDENTIFIER ::= {
        //Jonsson & Kaliski            Informational                     [Page 56]
        //RFC 3447        PKCS #1: RSA Cryptography Specifications   February 2003
        //    iso(1) member-body(2) us(840) rsadsi(113549) pkcs(1) 1
        //}
        //--
        //-- When rsaEncryption is used in an AlgorithmIdentifier the
        //-- parameters MUST be present and MUST be NULL.
        //--
        //rsaEncryption    OBJECT IDENTIFIER ::= { pkcs-1 1 }
        if (privateKeyAlgorithmIdentifier != "1.2.840.113549.1.1.1") throw new NotSupportedException("current only support rsaEncryption");

        //3.PrivateKey
        var privateKey = sequence.ReadOctetString();
        return privateKey;
    }

    public static byte[] DecodeEncryptedPrivateKeyInfo(byte[] bytes, byte[] password)
    {
        //EncryptedPrivateKeyInfo::= SEQUENCE {
        //    encryptionAlgorithm EncryptionAlgorithmIdentifier,
        //    encryptedData        EncryptedData }
        //EncryptionAlgorithmIdentifier::= AlgorithmIdentifier
        //                              {
        //                                  CONTENT - ENCRYPTION,
        //                                 { KeyEncryptionAlgorithms }
        //                              }
        //EncryptedData::= OCTET STRING-- Encrypted PrivateKeyInfo

        var reader = new AsnReader(bytes, AsnEncodingRules.DER);
        var sequence = reader.ReadSequence();

        //1.EncryptionAlgorithmIdentifier
        var encryptionAlgorithmSequence = sequence.ReadSequence();
        //rfc8018
        //rsadsi OBJECT IDENTIFIER ::= {iso(1) member-body(2) us(840) 113549}
        //pkcs OBJECT IDENTIFIER::= { rsadsi 1}
        //pkcs - 5 OBJECT IDENTIFIER ::= { pkcs 5}
        var encryptionAlgorithmSequenceIdentifier = encryptionAlgorithmSequence.ReadObjectIdentifier();
        if (encryptionAlgorithmSequenceIdentifier != "1.2.840.113549.1.5.13") throw new NotSupportedException("current only support PBES2"); //id-PBES2 OBJECT IDENTIFIER ::= {pkcs-5 13}

        //1.1PBES2-params
        //PBES2-params ::= SEQUENCE {
        //keyDerivationFunc AlgorithmIdentifier {{PBES2-KDFs}},
        //encryptionScheme AlgorithmIdentifier {{PBES2-Encs}}
        var pbes2Sequence = encryptionAlgorithmSequence.ReadSequence();

        //1.1.1PBES2-KDFs
        var kdfSequence = pbes2Sequence.ReadSequence();
        var kdfIdentifier = kdfSequence.ReadObjectIdentifier();
        if (kdfIdentifier != "1.2.840.113549.1.5.12") throw new NotSupportedException("KDF should be PBKDF2"); //id-PBKDF2 OBJECT IDENTIFIER ::= {pkcs-5 12}
        var kdfParamsSequence = kdfSequence.ReadSequence();
        // PBKDF2-params ::= SEQUENCE {
        //  salt CHOICE {
        //  specified OCTET STRING,
        //  otherSource AlgorithmIdentifier { { PBKDF2 - SaltSources} }
        //},
        //iterationCount INTEGER(1..MAX),
        //keyLength INTEGER(1..MAX) OPTIONAL,
        //prf AlgorithmIdentifier { { PBKDF2 - PRFs} } DEFAULT
        //algid - hmacWithSHA1 }
        var saltTag = kdfParamsSequence.PeekTag();
        if (saltTag == Asn1Tag.ObjectIdentifier) throw new NotSupportedException("nesting salt not supported yet");
        var salt = kdfParamsSequence.ReadOctetString();
        var iterationCount = kdfParamsSequence.ReadInteger();
        var keyLength = -1;
        if (kdfParamsSequence.PeekTag() == Asn1Tag.Integer) keyLength = (int)kdfParamsSequence.ReadInteger();
        var prfSequence = kdfParamsSequence.ReadSequence();
        var prfIdentifier = prfSequence.ReadObjectIdentifier();
        //PBKDF2-PRFs ALGORITHM-IDENTIFIER ::= {
        //{ NULL IDENTIFIED BY id-hmacWithSHA1},
        //{ NULL IDENTIFIED BY id-hmacWithSHA224},
        //{ NULL IDENTIFIED BY id-hmacWithSHA256},
        //{ NULL IDENTIFIED BY id-hmacWithSHA384},
        //{ NULL IDENTIFIED BY id-hmacWithSHA512},
        //{ NULL IDENTIFIED BY id-hmacWithSHA512 - 224},
        //{ NULL IDENTIFIED BY id-hmacWithSHA512 - 256},
        //...
        //}
        // rsadsi OBJECT IDENTIFIER::=
        //{ iso(1) member - body(2) us(840) rsadsi(113549)}
        // digestAlgorithm OBJECT IDENTIFIER::= { rsadsi 2}
        // id - hmacWithSHA224 OBJECT IDENTIFIER ::= { digestAlgorithm 8}
        // id - hmacWithSHA256 OBJECT IDENTIFIER ::= { digestAlgorithm 9}
        // id - hmacWithSHA384 OBJECT IDENTIFIER ::= { digestAlgorithm 10}
        // id - hmacWithSHA512 OBJECT IDENTIFIER ::= { digestAlgorithm 11}
        HMAC hMAC;
        if (prfIdentifier == "1.2.840.113549.2.7") hMAC = new HMACSHA1(password);
        else if (prfIdentifier == "1.2.840.113549.2.9") hMAC = new HMACSHA256(password);
        else if (prfIdentifier == "1.2.840.113549.2.10") hMAC = new HMACSHA384(password);
        else if (prfIdentifier == "1.2.840.113549.2.11") hMAC = new HMACSHA512(password);
        else throw new NotSupportedException($"digestAlgorithm with oid '{prfIdentifier}' not supported yet");
        //prf parameter null,ignored

        //1.1.2PBES2-Encs
        var encsSequence = pbes2Sequence.ReadSequence();
        var encsIdentifier = encsSequence.ReadObjectIdentifier();
        SymmetricAlgorithm algorithm;
        byte[] iv;
        if (encsIdentifier == "2.16.840.1.101.3.4.1.42")//aes256-CBC
        {
            //rfc3565
            //identified by the following object identifiers:
            //id-aes128-CBC OBJECT IDENTIFIER ::= { aes 2 }
            //id-aes192-CBC OBJECT IDENTIFIER ::= { aes 22 }
            //id-aes256-CBC OBJECT IDENTIFIER ::= { aes 42 }
            //The AlgorithmIdentifier parameters field MUST be present, and the
            //parameters field MUST contain a AES-IV:
            //AES-IV ::= OCTET STRING (SIZE(16))
            var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            iv = encsSequence.ReadOctetString();
            if (keyLength == -1) keyLength = 256 / 8;
            algorithm = aes;
        }
        else throw new NotSupportedException($"current only support 'aes256-CBC' Encs");

        //2.EncryptedData
        var encryptedData = sequence.ReadOctetString();

        var derivedKey = Pkcs5.PBKDF2(hMAC, salt, (int)iterationCount, keyLength);
        using var transform = algorithm.CreateDecryptor(derivedKey, iv);
        return transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
    }

    public static byte[] EncodePrivateKey(RSAParameters parameters)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();

        //1.version
        writer.WriteInteger(0x00);

        //2.PrivateKeyAlgorithmIdentifier
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.1.1");
        writer.WriteNull();
        writer.PopSequence();

        //3.PrivateKey
        var privateKey = Pkcs1.EncodePrivateKey(parameters);
        writer.WriteOctetString(privateKey);

        writer.PopSequence();
        var length = writer.GetEncodedLength();
        var bytes = new byte[length];
        writer.Encode(bytes);
        return bytes;
    }

    public static byte[] EncodeEncryptedPrivateKey(RSAParameters parameters, byte[] password)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();

        //1.EncryptionAlgorithmIdentifier
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.5.13");//pbes2
        writer.PushSequence();
        //pbkdf2
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.5.12");//pbkdf2
        writer.PushSequence();
        var salt = new byte[16];//salt
        new Random().NextBytes(salt);
        writer.WriteOctetString(salt);
        var iterationCount = 2048;//iterationCount
        writer.WriteInteger(iterationCount);
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.2.9");//hmacsha259
        writer.WriteNull();
        writer.PopSequence();

        writer.PopSequence();
        writer.PopSequence();
        //encs
        writer.PushSequence();
        writer.WriteObjectIdentifier("2.16.840.1.101.3.4.1.42");//aes-256-cbc
        var iv = new byte[16];//iv
        new Random().NextBytes(iv);
        writer.WriteOctetString(iv);
        writer.PopSequence();
        writer.PopSequence();
        writer.PopSequence();

        //2.EncryptedData
        using var hMAC = new HMACSHA256(password);
        var derivedKey = Pkcs5.PBKDF2(hMAC, salt, iterationCount, 32);
        using var aes = Aes.Create();
        using var transform = aes.CreateEncryptor(derivedKey, iv);
        var key = EncodePrivateKey(parameters);
        var encryptedKey = transform.TransformFinalBlock(key, 0, key.Length);
        writer.WriteOctetString(encryptedKey);

        writer.PopSequence();
        var length = writer.GetEncodedLength();
        var bytes = new byte[length];
        writer.Encode(bytes);
        return bytes;
    }
}
