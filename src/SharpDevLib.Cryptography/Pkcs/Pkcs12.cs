using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SharpDevLib.Cryptography;

/// <summary>
/// rfc7292
/// </summary>
internal static class Pkcs12
{
    public static byte[] Encode(X509Certificate2 certificate, string privateKey, string password)
    {
        //PFX::= SEQUENCE {
        //       version INTEGER { v3(3)}
        //(v3,...),
        //       authSafe ContentInfo,
        //       macData     MacData OPTIONAL
        //   }
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        //1.version
        writer.WriteInteger(3);

        //2.authSafe
        var safeContents = GetSafeContents(certificate, privateKey, password);
        var authSafe = Pkcs7.Encode(Pkcs7ContentType.Data, safeContents);
        writer.WriteEncodedValue(authSafe);

        //3.macData
        var mac = GetMac(safeContents, password);
        writer.WriteEncodedValue(mac);

        writer.PopSequence();

        return writer.Encode();
    }

    static byte[] GetSafeContents(X509Certificate2 certificate, string privateKey, string password)
    {
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();

        //1.certificate
        var certificateBag = GetCertificateBag(certificate, password);
        writer.WriteEncodedValue(Pkcs7.Encode(Pkcs7ContentType.EncryptedData, certificateBag));

        //2.private key
        var privateKeyBag = GetPrivateKeyBag(certificate, privateKey, password);
        writer.WriteEncodedValue(Pkcs7.Encode(Pkcs7ContentType.Data, privateKeyBag));

        writer.PopSequence();
        return writer.Encode();
    }

    static byte[] GetCertificateBag(X509Certificate2 certificate, string password)
    {
        //rfc2315(10.1)中定义
        //EncryptedContentInfo ::= SEQUENCE {
        //contentType ContentType,
        //contentEncryptionAlgorithm
        //  ContentEncryptionAlgorithmIdentifier,
        //encryptedContent
        //  [0] IMPLICIT EncryptedContent OPTIONAL }
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();

        //1.ContentType
        writer.WriteObjectIdentifier(Pkcs7.GetOidByType(Pkcs7ContentType.Data));

        //2.contentEncryptionAlgorithm
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

        //3.encryptedContent
        var certificateData = GetCertificateBagData(certificate);
        //Console.WriteLine(certificate.GetRawCertDataString());
        //var certificateData = certificate.GetRawCertData();
        Console.WriteLine(certificateData.Length);
        Console.WriteLine(certificateData.ToHexString());
        using var hMAC = new HMACSHA256(password.ToUtf8Bytes());
        var derivedKey = Pkcs5.PBKDF2(hMAC, salt, iterationCount, 32);
        using var aes = Aes.Create();
        using var transform = aes.CreateEncryptor(derivedKey, iv);
        var encryptedCertificate = transform.TransformFinalBlock(certificateData, 0, certificateData.Length);
        writer.WriteOctetString(encryptedCertificate, new Asn1Tag(TagClass.ContextSpecific, 0));

        writer.PopSequence();
        return writer.Encode();
    }

    static byte[] GetCertificateBagData(X509Certificate2 certificate)
    {
        //SafeBag::= SEQUENCE {
        //           bagId BAG-TYPE.& id({ PKCS12BagSet})
        //    bagValue[0] EXPLICIT BAG-TYPE.& Type({ PKCS12BagSet}
        //           { @bagId}),
        //    bagAttributes SET OF PKCS12Attribute OPTIONAL
        //}
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        writer.PushSequence();

        //1.bagId
        writer.WriteObjectIdentifier("1.2.840.113549.1.12.10.1.3");//pkcs-12-certBag

        //2.bagValue
        var bagTag = new Asn1Tag(TagClass.ContextSpecific, 0);
        writer.PushSequence(bagTag);
        //     CertBag::= SEQUENCE {
        //         certId BAG-TYPE.& id({ CertTypes}),
        //    certValue[0] EXPLICIT BAG-TYPE.& Type({ CertTypes}
        //         { @certId})
        //}
        writer.PushSequence();
        //2.1 certId
        writer.WriteObjectIdentifier("1.2.840.113549.1.9.22.1");//x509certificate
        //2.2 certValue
        var certTag = new Asn1Tag(TagClass.ContextSpecific, 0);
        writer.PushSequence(certTag);
        writer.PushOctetString();
        writer.WriteEncodedValue(certificate.RawData);
        writer.PopOctetString();
        writer.PopSequence(certTag);

        writer.PopSequence();

        writer.PopSequence(bagTag);

        //3.bagAttribute
        writer.PushSetOf();
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.9.21");//localKeyId
        writer.PushSetOf();
        writer.WriteOctetString(certificate.Thumbprint.FromHexString());
        writer.PopSetOf();
        writer.PopSequence();
        writer.PopSetOf();

        writer.PopSequence();
        writer.PopSequence();
        return writer.Encode();
    }

    static byte[] GetPrivateKeyBag(X509Certificate2 certificate, string privateKey, string password)
    {
        //SafeContents::= SEQUENCE OF SafeBag
        //SafeBag::= SEQUENCE {
        //           bagId BAG-TYPE.& id({ PKCS12BagSet}),
        //    bagValue[0] EXPLICIT BAG-TYPE.& Type({ PKCS12BagSet}
        //           { @bagId}),
        //    bagAttributes SET OF PKCS12Attribute OPTIONAL
        //}
        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();//SafeContents
        writer.PushSequence();//SafeBag

        //1.pkcs-12-pkcs-8shroudedkeybag
        writer.WriteObjectIdentifier("1.2.840.113549.1.12.10.1.2");//bagId

        //2.bagValue-encryptedPrivateKeyInfo
        var bagTag = new Asn1Tag(TagClass.ContextSpecific, 0);
        writer.PushSetOf(bagTag);
        using var rsa = RSA.Create();
        rsa.ImportPem(privateKey);
        var pkcs8PrivateKey = Pkcs8.EncodePrivateKey(rsa.ExportParameters(true));
        var encryptedPrivateKey = Pkcs8.EncodeEncryptedPrivateKey(pkcs8PrivateKey, password.ToUtf8Bytes());
        writer.WriteEncodedValue(encryptedPrivateKey);
        writer.PopSetOf(bagTag);

        //3.attribute,Thumbprint
        writer.PushSetOf();
        writer.PushSequence();
        writer.WriteObjectIdentifier("1.2.840.113549.1.9.21");//localKeyId
        writer.PushSetOf();
        writer.WriteOctetString(certificate.Thumbprint.FromHexString());
        writer.PopSetOf();
        writer.PopSequence();
        writer.PopSetOf();

        writer.PopSequence();
        writer.PopSequence();
        return writer.Encode();
    }

    static byte[] GetMac(byte[] safeContents, string password)
    {
        //MacData::= SEQUENCE {
        //    mac DigestInfo,
        //    macSalt     OCTET STRING,
        //    iterations  INTEGER DEFAULT 1
        //    -- Note: The default is for historical reasons and its
        //    --       use is deprecated.
        // }

        var salt = new byte[8];
        new Random().NextBytes(salt);
        var iterationCount = 2048;
        var passwordBytes = password.ToUtf8Bytes();
        var macDerivedKey = GetMacDerivedKey(salt, passwordBytes, iterationCount);
        using var hmac = new HMACSHA256(macDerivedKey);
        var digist = hmac.ComputeHash(safeContents);

        var writer = new AsnWriter(AsnEncodingRules.DER);
        writer.PushSequence();
        //1.mac
        writer.PushSequence();
        writer.PushSequence();
        writer.WriteObjectIdentifier("2.16.840.1.101.3.4.2.1");
        writer.WriteNull();
        writer.PopSequence();
        writer.WriteOctetString(digist);
        writer.PopSequence();

        //2.macSalt
        writer.WriteOctetString(salt);

        //3.iterations
        writer.WriteInteger(iterationCount);

        writer.PopSequence();
        return writer.Encode();
    }

    static byte[] GetMacDerivedKey(byte[] saltOrigin, byte[] passwordOrigin, int iterationCount)
    {
        //https://www.rfc-editor.org/rfc/rfc7292.html#appendix-B
        byte id = 3;
        var u = 256;
        var uLen = u / 8;
        var v = 512;
        var vLen = v / 8;
        var r = iterationCount;
        var n = uLen;

        //1.D
        var D = new byte[vLen];
        for (int i = 0; i < D.Length; i++)
        {
            D[i] = id;
        }

        //2.salt
        var saltCount = (int)Math.Ceiling(saltOrigin.Length * 1.0 / vLen);
        var salt = new byte[saltCount * vLen];
        var saltList = new List<byte>();
        while (saltList.Count < salt.Length)
        {
            saltList.AddRange(saltOrigin);
        }
        salt = saltList.Take(salt.Length).ToArray();

        //3.password,rfc7292.html#appendix-B.1
        var bigEndianFormatPassword = GetBigEndianFormatPassword(passwordOrigin);
        var passwordCount = (int)Math.Ceiling(bigEndianFormatPassword.Length * 1.0 / vLen);
        var password = new byte[passwordCount * vLen];
        var passwordList = new List<byte>();
        while (passwordList.Count < password.Length)
        {
            passwordList.AddRange(bigEndianFormatPassword);
        }
        password = passwordList.Take(password.Length).ToArray();

        //4.I
        var I = salt.Concat(password).ToArray();

        //5.c
        var c = Math.Ceiling(n * 1.0 / u);

        //6
        var algorithm = SHA256.Create();
        var AArray = new List<byte>();
        for (int i = 0; i < c; i++)
        {
            //1.A
            var A = algorithm.ComputeHash(D.Concat(I).ToArray());
            for (int j = 1; j < r; j++)
            {
                A = algorithm.ComputeHash(A);
            }
            AArray.AddRange(A);

            //2.B
            var BList = new List<byte>();
            while (BList.Count < vLen)
            {
                BList.AddRange(A);
            }
            var B = BList.Take(vLen).ToArray();

            //3.adjust I
            for (int j = 0; j < I.Length / vLen; j++)
            {
                for (int k = 0; k < B.Length; k++)
                {
                    I[j * vLen + k] = (byte)(I[j * vLen + k] + B[k] + 1);//may be overflow
                }
            }
        }

        return AArray.Take(n).ToArray();
    }

    static byte[] GetBigEndianFormatPassword(byte[] passwordOrigin)
    {
        var bigEndianFormatPasswordList = new List<byte>();
        for (int i = 0; i < passwordOrigin.Length; i++)
        {
            bigEndianFormatPasswordList.Add(0);
            bigEndianFormatPasswordList.Add(passwordOrigin[i]);
        }
        bigEndianFormatPasswordList.AddRange(new byte[] { 0, 0 });
        return bigEndianFormatPasswordList.ToArray();
    }
}
