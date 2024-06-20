﻿using System.Text;

namespace SharpDevLib.Cryptography;

//rfc1421
internal class PemObject
{
    public PemObject(string header, string body, string footer, PemType pemType)
    {
        Header = header;
        Body = body;
        Footer = footer;
        PemType = pemType;
    }

    public PemObject(string header, PemHeaderFields headerFileds, string body, string footer, PemType pemType)
    {
        Header = header;
        HeaderFields = headerFileds;
        Body = body;
        Footer = footer;
        PemType = pemType;
    }

    public string Header { get; }
    public PemHeaderFields? HeaderFields { get; }

    public string Body { get; }

    public string Footer { get; }

    public PemType PemType { get; }

    public string Write()
    {
        var builder = new StringBuilder();
        builder.AppendLineWithLFTerminator(Header);
        if (HeaderFields is not null)
        {
            if (HeaderFields.ProcType.NotNullOrWhiteSpace()) builder.AppendLineWithLFTerminator(HeaderFields.ProcType);
            if (HeaderFields.DEKInfo.NotNullOrWhiteSpace())
            {
                builder.AppendLineWithLFTerminator(HeaderFields.DEKInfo);
                builder.AppendLineWithLFTerminator();
            }
        }

        var count = Math.Ceiling(Body.Length / 64.0);
        for (int i = 0; i < count; i++)
        {
            if (i == count - 1)
            {
                var length = Body.Length % 64;
                if (length == 0) length = 64;
                builder.AppendLineWithLFTerminator(Body.Substring(i * 64, length));
            }
            else builder.AppendLineWithLFTerminator(Body.Substring(i * 64, 64));
        }

        builder.Append(Footer);
        return builder.ToString();
    }

    public static PemObject Read(string key)
    {
        key = key.Trim();
        var reader = new StringReader(key);
        var header = reader.ReadLine();

        //pkcs1 private key
        if (header.Equals(PemStatics.RsaPkcs1PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaPkcs1PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPkcs1PrivateEnd}'");
            var procType = reader.ReadLine();
            // encrypted pkcs1 private key
            if (procType.StartsWith("Proc-Type: 4,ENCRYPTED"))
            {
                var dekInfo = reader.ReadLine();
                var body = key.Replace(header, "").Replace(PemStatics.RsaPkcs1PrivateEnd, "").Replace(procType, "").Replace(dekInfo, "");
                return new PemObject(header, new PemHeaderFields(procType, dekInfo), RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaEncryptedPkcs1PrivateKey);
            }
            //pkcs1 private key
            else
            {
                var body = key.Replace(header, "").Replace(PemStatics.RsaPkcs1PrivateEnd, "");
                return new PemObject(header, RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaPkcs1PrivateKey);
            }
        }
        //pkcs8 private key
        else if (header.Equals(PemStatics.RsaPkcs8PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaPkcs8PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPkcs8PrivateEnd}'");
            var body = key.Replace(header, "").Replace(PemStatics.RsaPkcs8PrivateEnd, "");
            return new PemObject(header, RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs8PrivateEnd, PemType.RsaPkcs8PrivateKey);
        }
        //encrypted pkcs8 private key
        else if (header.Equals(PemStatics.RsaEncryptedPkcs8PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaEncryptedPkcs8PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaEncryptedPkcs8PrivateEnd}'");
            var body = key.Replace(header, "").Replace(PemStatics.RsaEncryptedPkcs8PrivateEnd, "");
            return new PemObject(header, RemoveWrapLineAndTrim(body), PemStatics.RsaEncryptedPkcs8PrivateEnd, PemType.RsaEncryptedPkcs8PrivateKey);
        }
        //public key
        else if (header.Equals(PemStatics.RsaPublicStart))
        {
            if (!key.EndsWith(PemStatics.RsaPublicEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPublicEnd}'");
            var body = key.Replace(header, "").Replace(PemStatics.RsaPublicEnd, "");
            return new PemObject(header, RemoveWrapLineAndTrim(body), PemStatics.RsaPublicEnd, PemType.RsaPublicKey);
        }
        //unkonw
        else
        {
            throw new InvalidDataException($"unknow format '{header}'");
        }
    }

    static string RemoveWrapLineAndTrim(string source)
    {
        return source.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Trim();
    }
}
