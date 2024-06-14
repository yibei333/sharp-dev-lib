using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDevLib.Cryptography;

internal class PemObject
{
    public PemObject(PemHeader header, string body, string footer, PemType pemType)
    {
        Header = header;
        Body = body;
        Footer = footer;
        PemType = pemType;
    }

    public PemHeader Header { get; }

    public string Body { get; }

    public string Footer { get; }

    public PemType PemType { get; }

    public string Write()
    {
        throw new NotImplementedException();
    }

    public static PemObject Read(string key)
    {
        key = key.Trim();
        var reader = new StringReader(key);
        var title = reader.ReadLine();

        //pkcs1 private key
        if (title.Equals(PemStatics.RsaPkcs1PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaPkcs1PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPkcs1PrivateEnd}'");
            var procType = reader.ReadLine();
            // encrypted pkcs1 private key
            if (procType.StartsWith("Proc-Type"))
            {
                var dekInfo = reader.ReadLine();
                var body = key.Replace(title, "").Replace(PemStatics.RsaPkcs1PrivateEnd, "").Replace(procType, "").Replace(dekInfo, "");
                return new PemObject(new PemHeader(title, procType, dekInfo), RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaEncryptedPkcs1PrivateKey);
            }
            //pkcs1 private key
            else
            {
                var body = key.Replace(title, "").Replace(PemStatics.RsaPkcs1PrivateEnd, "");
                return new PemObject(new PemHeader(title, null, null), RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs1PrivateEnd, PemType.RsaPkcs1PrivateKey);
            }
        }
        //pkcs8 private key
        else if (title.Equals(PemStatics.RsaPkcs8PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaPkcs8PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPkcs8PrivateEnd}'");
            var body = key.Replace(title, "").Replace(PemStatics.RsaPkcs8PrivateEnd, "");
            return new PemObject(new PemHeader(title, null, null), RemoveWrapLineAndTrim(body), PemStatics.RsaPkcs8PrivateEnd, PemType.RsaPkcs8PrivateKey);
        }
        //encrypted pkcs8 private key
        else if (title.Equals(PemStatics.RsaEncryptedPkcs8PrivateStart))
        {
            if (!key.EndsWith(PemStatics.RsaEncryptedPkcs8PrivateEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaEncryptedPkcs8PrivateEnd}'");
            var body = key.Replace(title, "").Replace(PemStatics.RsaEncryptedPkcs8PrivateEnd, "");
            return new PemObject(new PemHeader(title, null, null), RemoveWrapLineAndTrim(body), PemStatics.RsaEncryptedPkcs8PrivateEnd, PemType.RsaEncryptedPkcs8PrivateKey);
        }
        //public key
        else if (title.Equals(PemStatics.RsaPublicStart))
        {
            if (!key.EndsWith(PemStatics.RsaPublicEnd)) throw new InvalidDataException($"key should ends with '{PemStatics.RsaPublicEnd}'");
            var body = key.Replace(title, "").Replace(PemStatics.RsaPublicEnd, "");
            return new PemObject(new PemHeader(title, null, null), RemoveWrapLineAndTrim(body), PemStatics.RsaPublicEnd, PemType.RsaPublicKey);
        }
        //unkonw
        else
        {
            throw new InvalidDataException($"unknow format with '{title}'");
        }
    }

    static string RemoveWrapLineAndTrim(string source)
    {
        return source.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Trim();
    }
}
