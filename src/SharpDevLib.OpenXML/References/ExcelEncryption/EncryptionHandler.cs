/*******************************************************************************
 * You may amend and distribute as you like, but don't remove this header!
 *
 * EPPlus provides server-side generation of Excel 2007/2010 spreadsheets.
 * See https://github.com/JanKallman/EPPlus for details.
 *
 * Copyright (C) 2011  Jan Källman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.

 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU Lesser General Public License for more details.
 *
 * The GNU Lesser General Public License can be viewed at http://www.opensource.org/licenses/lgpl-license.php
 * If you unfamiliar with this license or have questions about it, here is an http://www.gnu.org/licenses/gpl-faq.html
 *
 * All code and executables are provided "as is" with no warranty either express or implied. 
 * The author accepts no liability for any damage or loss of business that this product may cause.
 *
 * Code change notes:
 * 
 * Author							Change						Date
 * ******************************************************************************
 * Jan Källman		    Added       		        2013-01-05
 *******************************************************************************/
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SharpDevLib.OpenXML.References.ExcelEncryption;

internal class EncryptedPackageHandler
{
    internal MemoryStream DecryptPackage(MemoryStream stream, ExcelEncryption encryption)
    {
        if (CompoundDocument.IsCompoundDocument(stream)) return GetStreamFromPackage(new CompoundDocument(stream), encryption);
        throw new InvalidDataException("The stream is not an valid/supported encrypted document.");
    }

    internal MemoryStream EncryptPackage(byte[] package, ExcelEncryption encryption)
    {
        if (encryption.Version == EncryptionVersion.Standard) return EncryptPackageBinary(package, encryption);
        else if (encryption.Version == EncryptionVersion.Agile) return EncryptPackageAgile(package, encryption);
        throw new ArgumentException("Unsupported encryption version.");
    }

    private MemoryStream EncryptPackageAgile(byte[] package, ExcelEncryption encryption)
    {
        var xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n";
        xml += "<encryption xmlns=\"http://schemas.microsoft.com/office/2006/encryption\" xmlns:p=\"http://schemas.microsoft.com/office/2006/keyEncryptor/password\" xmlns:c=\"http://schemas.microsoft.com/office/2006/keyEncryptor/certificate\">";
        xml += "<keyData saltSize=\"16\" blockSize=\"16\" keyBits=\"256\" hashSize=\"64\" cipherAlgorithm=\"AES\" cipherChaining=\"ChainingModeCBC\" hashAlgorithm=\"SHA512\" saltValue=\"\"/>";
        xml += "<dataIntegrity encryptedHmacKey=\"\" encryptedHmacValue=\"\"/>";
        xml += "<keyEncryptors>";
        xml += "<keyEncryptor uri=\"http://schemas.microsoft.com/office/2006/keyEncryptor/password\">";
        xml += "<p:encryptedKey spinCount=\"100000\" saltSize=\"16\" blockSize=\"16\" keyBits=\"256\" hashSize=\"64\" cipherAlgorithm=\"AES\" cipherChaining=\"ChainingModeCBC\" hashAlgorithm=\"SHA512\" saltValue=\"\" encryptedVerifierHashInput=\"\" encryptedVerifierHashValue=\"\" encryptedKeyValue=\"\" />";
        xml += "</keyEncryptor></keyEncryptors></encryption>";

        var encryptionInfo = new EncryptionInfoAgile();
        encryptionInfo.ReadFromXml(xml);
        var encr = encryptionInfo.KeyEncryptors[0];
        using var rnd = RandomNumberGenerator.Create();

        var s = new byte[16];
        rnd.GetBytes(s);
        if (encryptionInfo.KeyData != null) encryptionInfo.KeyData.SaltValue = s;

        rnd.GetBytes(s);
        encr.SaltValue = s;

        encr.KeyValue = new byte[encr.KeyBits / 8];
        rnd.GetBytes(encr.KeyValue);

        //Get the password key.
        using var hashProvider = GetHashProvider(encryptionInfo.KeyEncryptors[0]);
        var baseHash = GetPasswordHash(hashProvider, encr.SaltValue, encryption.Password, encr.SpinCount, encr.HashSize);

        var encrData = EncryptDataAgile(package, encryptionInfo, hashProvider);

        /**** Data Integrity ****/
        var saltHMAC = new byte[64];
        rnd.GetBytes(saltHMAC);

        SetHMAC(encryptionInfo, hashProvider, saltHMAC, encrData);

        /**** Verifier ****/
        encr.VerifierHashInput = new byte[16];
        rnd.GetBytes(encr.VerifierHashInput);

        encr.VerifierHash = hashProvider.ComputeHash(encr.VerifierHashInput);

        var VerifierInputKey = GetFinalHash(hashProvider, BlockKey_HashInput, baseHash);
        var VerifierHashKey = GetFinalHash(hashProvider, BlockKey_HashValue, baseHash);
        var KeyValueKey = GetFinalHash(hashProvider, BlockKey_KeyValue, baseHash);

        using var ms = new MemoryStream();
        EncryptAgileFromKey(encr, VerifierInputKey, encr.VerifierHashInput, 0, encr.VerifierHashInput.Length, encr.SaltValue, ms);
        encr.EncryptedVerifierHashInput = ms.ToArray();

        using var ms1 = new MemoryStream();
        EncryptAgileFromKey(encr, VerifierHashKey, encr.VerifierHash, 0, encr.VerifierHash.Length, encr.SaltValue, ms1);
        encr.EncryptedVerifierHash = ms1.ToArray();

        using var ms2 = new MemoryStream();
        EncryptAgileFromKey(encr, KeyValueKey, encr.KeyValue, 0, encr.KeyValue.Length, encr.SaltValue, ms2);
        encr.EncryptedKeyValue = ms2.ToArray();

        xml = encryptionInfo.Xml?.OuterXml;

        var byXml = xml == null ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(xml);

        using var ms3 = new MemoryStream();
        ms3.Write(BitConverter.GetBytes((ushort)4), 0, 2); //Major Version
        ms3.Write(BitConverter.GetBytes((ushort)4), 0, 2); //Minor Version
        ms3.Write(BitConverter.GetBytes((uint)0x40), 0, 4); //Reserved
        ms3.Write(byXml, 0, byXml.Length);

        var doc = new CompoundDocument();

        //Add the dataspace streams
        CreateDataSpaces(doc);
        //EncryptionInfo...
        doc.Storage.DataStreams.Add("EncryptionInfo", ms3.ToArray());
        //...and the encrypted package
        doc.Storage.DataStreams.Add("EncryptedPackage", encrData);

        using var ms4 = new MemoryStream();
        doc.Save(ms4);
        //ms.Write(e,0,e.Length);
        return ms4;
    }

    private static byte[] EncryptDataAgile(byte[] data, EncryptionInfoAgile encryptionInfo, HashAlgorithm hashProvider)
    {
        var ke = encryptionInfo.KeyEncryptors[0];
        var aes = Aes.Create();
        aes.KeySize = ke.KeyBits;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.Zeros;

        int pos = 0;
        int segment = 0;

        //Encrypt the data
        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes((ulong)data.Length), 0, 8);
        while (pos < data.Length)
        {
            var segmentSize = (int)(data.Length - pos > 4096 ? 4096 : data.Length - pos);

            var ivTmp = new byte[4 + (encryptionInfo.KeyData?.SaltSize ?? 0)];
            Array.Copy(encryptionInfo.KeyData?.SaltValue ?? Array.Empty<byte>(), 0, ivTmp, 0, encryptionInfo.KeyData?.SaltSize ?? 0);
            Array.Copy(BitConverter.GetBytes(segment), 0, ivTmp, encryptionInfo.KeyData?.SaltSize ?? 0, 4);
            var iv = hashProvider.ComputeHash(ivTmp);

            EncryptAgileFromKey(ke, ke.KeyValue, data, pos, segmentSize, iv, ms);
            pos += segmentSize;
            segment++;
        }
        ms.Flush();
        return ms.ToArray();
    }

    // Set the dataintegrity
    private void SetHMAC(EncryptionInfoAgile ei, HashAlgorithm hashProvider, byte[] salt, byte[] data)
    {
        var iv = GetFinalHash(hashProvider, BlockKey_HmacKey, ei.KeyData?.SaltValue ?? Array.Empty<byte>());
        using var ms = new MemoryStream();
        EncryptAgileFromKey(ei.KeyEncryptors[0], ei.KeyEncryptors[0].KeyValue, salt, 0L, salt.Length, iv, ms);
        if (ei.DataIntegrity != null) ei.DataIntegrity.EncryptedHmacKey = ms.ToArray();

        using var h = GetHmacProvider(ei.KeyEncryptors[0], salt);
        var hmacValue = h.ComputeHash(data);

        using var msOther = new MemoryStream();
        iv = GetFinalHash(hashProvider, BlockKey_HmacValue, ei.KeyData?.SaltValue ?? Array.Empty<byte>());
        EncryptAgileFromKey(ei.KeyEncryptors[0], ei.KeyEncryptors[0].KeyValue, hmacValue, 0L, hmacValue.Length, iv, msOther);
        if (ei.DataIntegrity != null) ei.DataIntegrity.EncryptedHmacValue = msOther.ToArray();
    }

    private static HMAC GetHmacProvider(EncryptionInfoAgile.EncryptionKeyData ei, byte[] salt)
    {
        return ei.HashAlgorithm switch
        {
            InternalHashAlogorithm.MD5 => new HMACMD5(salt),
            InternalHashAlogorithm.SHA1 => new HMACSHA1(salt),
            InternalHashAlogorithm.SHA256 => new HMACSHA256(salt),
            InternalHashAlogorithm.SHA384 => new HMACSHA384(salt),
            InternalHashAlogorithm.SHA512 => new HMACSHA512(salt),
            _ => throw (new NotSupportedException(string.Format("Hash method {0} not supported.", ei.HashAlgorithm))),
        };
    }

    private static MemoryStream EncryptPackageBinary(byte[] package, ExcelEncryption encryption)
    {
        var encryptionInfo = CreateEncryptionInfo(encryption.Password, encryption.Algorithm == EncryptionAlgorithm.AES128 ? AlgorithmID.AES128 : (encryption.Algorithm == EncryptionAlgorithm.AES192 ? AlgorithmID.AES192 : AlgorithmID.AES256), out var encryptionKey);
        var doc = new CompoundDocument();
        CreateDataSpaces(doc);

        doc.Storage.DataStreams.Add("EncryptionInfo", encryptionInfo.WriteBinary());

        //Encrypt the package
        var encryptedPackage = EncryptData(encryptionKey, package, false);
        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes((ulong)package.Length), 0, 8);
        ms.Write(encryptedPackage, 0, encryptedPackage.Length);
        doc.Storage.DataStreams.Add("EncryptedPackage", ms.ToArray());

        using var ret = new MemoryStream();
        doc.Save(ret);

        return ret;
    }

    #region "Dataspaces Stream methods"
    private static void CreateDataSpaces(CompoundDocument doc)
    {
        var ds = new CompoundDocument.StoragePart();
        doc.Storage.SubStorage.Add("\x06" + "DataSpaces", ds);
        ds.DataStreams.Add("Version", CreateVersionStream());
        ds.DataStreams.Add("DataSpaceMap", CreateDataSpaceMap());

        var dsInfo = new CompoundDocument.StoragePart();
        ds.SubStorage.Add("DataSpaceInfo", dsInfo);
        dsInfo.DataStreams.Add("StrongEncryptionDataSpace", CreateStrongEncryptionDataSpaceStream());

        var transInfo = new CompoundDocument.StoragePart();
        ds.SubStorage.Add("TransformInfo", transInfo);

        var strEncTrans = new CompoundDocument.StoragePart();
        transInfo.SubStorage.Add("StrongEncryptionTransform", strEncTrans);

        strEncTrans.DataStreams.Add("\x06Primary", CreateTransformInfoPrimary());
    }

    private static byte[] CreateStrongEncryptionDataSpaceStream()
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        bw.Write(8);       //HeaderLength
        bw.Write(1);       //EntryCount

        var tr = "StrongEncryptionTransform";
        bw.Write(tr.Length * 2);
        bw.Write(Encoding.Unicode.GetBytes(tr + "\0")); // end \0 is for padding

        bw.Flush();
        return ms.ToArray();
    }

    private static byte[] CreateVersionStream()
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        bw.Write((short)0x3C);  //Major
        bw.Write((short)0);     //Minor
        bw.Write(Encoding.Unicode.GetBytes("Microsoft.Container.DataSpaces"));
        bw.Write(1);       //ReaderVersion
        bw.Write(1);       //UpdaterVersion
        bw.Write(1);       //WriterVersion

        bw.Flush();
        return ms.ToArray();
    }

    private static byte[] CreateDataSpaceMap()
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        bw.Write(8);       //HeaderLength
        bw.Write(1);       //EntryCount
        var s1 = "EncryptedPackage";
        var s2 = "StrongEncryptionDataSpace";
        bw.Write((s1.Length + s2.Length) * 2 + 0x16);
        bw.Write(1);       //ReferenceComponentCount
        bw.Write(0);       //Stream=0
        bw.Write(s1.Length * 2); //Length s1
        bw.Write(Encoding.Unicode.GetBytes(s1));
        bw.Write(s2.Length * 2);   //Length s2
        bw.Write(Encoding.Unicode.GetBytes(s2 + "\0"));   // end \0 is for padding

        bw.Flush();
        return ms.ToArray();
    }

    private static byte[] CreateTransformInfoPrimary()
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);
        var TransformID = "{FF9A3F03-56EF-4613-BDD5-5A41C1D07246}";
        var TransformName = "Microsoft.Container.EncryptionTransform";
        bw.Write(TransformID.Length * 2 + 12);
        bw.Write(1);
        bw.Write(TransformID.Length * 2);
        bw.Write(Encoding.Unicode.GetBytes(TransformID));
        bw.Write(TransformName.Length * 2);
        bw.Write(Encoding.Unicode.GetBytes(TransformName + "\0"));
        bw.Write(1);   //ReaderVersion
        bw.Write(1);   //UpdaterVersion
        bw.Write(1);   //WriterVersion

        bw.Write(0);
        bw.Write(0);
        bw.Write(0);       //CipherMode
        bw.Write(4);       //Reserved

        bw.Flush();
        return ms.ToArray();
    }
    #endregion

    private static EncryptionInfoBinary CreateEncryptionInfo(string password, AlgorithmID algID, out byte[] key)
    {
        if (algID == AlgorithmID.Flags || algID == AlgorithmID.RC4) throw new ArgumentException("algID must be AES128, AES192 or AES256");
        var encryptionInfo = new EncryptionInfoBinary
        {
            MajorVersion = 4,
            MinorVersion = 2,
            Flags = Flags.fAES | Flags.fCryptoAPI,

            //Header
            Header = new EncryptionHeader()
        };

        encryptionInfo.Header.AlgID = algID;
        encryptionInfo.Header.AlgIDHash = AlgorithmHashID.SHA1;
        encryptionInfo.Header.Flags = encryptionInfo.Flags;
        encryptionInfo.Header.KeySize = algID == AlgorithmID.AES128 ? 0x80 : (algID == AlgorithmID.AES192 ? 0xC0 : 0x100);
        encryptionInfo.Header.ProviderType = ProviderType.AES;
        encryptionInfo.Header.CSPName = "Microsoft Enhanced RSA and AES Cryptographic Provider\0";
        encryptionInfo.Header.Reserved1 = 0;
        encryptionInfo.Header.Reserved2 = 0;
        encryptionInfo.Header.SizeExtra = 0;

        //Verifier
        encryptionInfo.Verifier = new EncryptionVerifier { Salt = new byte[16] };

        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(encryptionInfo.Verifier.Salt);
        encryptionInfo.Verifier.SaltSize = 0x10;

        key = GetPasswordHashBinary(password, encryptionInfo);

        var verifier = new byte[16];
        rnd.GetBytes(verifier);
        encryptionInfo.Verifier.EncryptedVerifier = EncryptData(key, verifier, true);

        //AES = 32 Bits
        encryptionInfo.Verifier.VerifierHashSize = 0x20;
        using var sha = SHA1.Create();
        var verifierHash = sha.ComputeHash(verifier);

        encryptionInfo.Verifier.EncryptedVerifierHash = EncryptData(key, verifierHash, false);

        return encryptionInfo;
    }

    private static byte[] EncryptData(byte[] key, byte[] data, bool useDataSize)
    {
        using var aes = Aes.Create();
        aes.KeySize = key.Length * 8;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.Zeros;

        //Encrypt the data
        var crypt = aes.CreateEncryptor(key, null);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write);
        cs.Write(data, 0, data.Length);

        cs.FlushFinalBlock();

        if (useDataSize)
        {
            var ret = new byte[data.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(ret, 0, data.Length);  //Truncate any padded Zeros
            return ret;
        }
        else
        {
            return ms.ToArray();
        }
    }

    private MemoryStream GetStreamFromPackage(CompoundDocument doc, ExcelEncryption encryption)
    {
        if (doc.Storage.DataStreams.ContainsKey("EncryptionInfo") || doc.Storage.DataStreams.ContainsKey("EncryptedPackage"))
        {
            var encryptionInfo = EncryptionInfo.ReadBinary(doc.Storage.DataStreams["EncryptionInfo"]);
            return DecryptDocument(doc.Storage.DataStreams["EncryptedPackage"], encryptionInfo, encryption.Password);
        }
        throw (new InvalidDataException("Invalid document. EncryptionInfo or EncryptedPackage stream is missing"));
    }

    private MemoryStream DecryptDocument(byte[] data, EncryptionInfo encryptionInfo, string password)
    {
        var size = BitConverter.ToInt64(data, 0);

        var encryptedData = new byte[data.Length - 8];
        Array.Copy(data, 8, encryptedData, 0, encryptedData.Length);

        return encryptionInfo is EncryptionInfoBinary binary ? DecryptBinary(binary, password, size, encryptedData) : DecryptAgile((EncryptionInfoAgile)encryptionInfo, password, size, encryptedData, data);
    }

    readonly byte[] BlockKey_HashInput = new byte[] { 0xfe, 0xa7, 0xd2, 0x76, 0x3b, 0x4b, 0x9e, 0x79 };
    readonly byte[] BlockKey_HashValue = new byte[] { 0xd7, 0xaa, 0x0f, 0x6d, 0x30, 0x61, 0x34, 0x4e };
    readonly byte[] BlockKey_KeyValue = new byte[] { 0x14, 0x6e, 0x0b, 0xe7, 0xab, 0xac, 0xd0, 0xd6 };
    readonly byte[] BlockKey_HmacKey = new byte[] { 0x5f, 0xb2, 0xad, 0x01, 0x0c, 0xb9, 0xe1, 0xf6 };//MSOFFCRYPTO 2.3.4.14 section 3
    readonly byte[] BlockKey_HmacValue = new byte[] { 0xa0, 0x67, 0x7f, 0x02, 0xb2, 0x2c, 0x84, 0x33 };//MSOFFCRYPTO 2.3.4.14 section 5

    private MemoryStream DecryptAgile(EncryptionInfoAgile encryptionInfo, string password, long size, byte[] encryptedData, byte[] data)
    {
        if (encryptionInfo.KeyData?.CipherAlgorithm != InternalCipherAlgorithm.AES) throw new NotSupportedException($"CipherAlgorithm({encryptionInfo.KeyData?.CipherAlgorithm}) not supported");

        var encr = encryptionInfo.KeyEncryptors[0];
        using var hashProvider = GetHashProvider(encr);
        using var hashProviderDataKey = GetHashProvider(encryptionInfo.KeyData);

        var baseHash = GetPasswordHash(hashProvider, encr.SaltValue, password, encr.SpinCount, encr.HashSize);

        //Get the keys for the verifiers and the key value
        var valInputKey = GetFinalHash(hashProvider, BlockKey_HashInput, baseHash);
        var valHashKey = GetFinalHash(hashProvider, BlockKey_HashValue, baseHash);
        var valKeySizeKey = GetFinalHash(hashProvider, BlockKey_KeyValue, baseHash);

        //Decrypt
        encr.VerifierHashInput = DecryptAgileFromKey(encr, valInputKey, encr.EncryptedVerifierHashInput, encr.SaltSize, encr.SaltValue);
        encr.VerifierHash = DecryptAgileFromKey(encr, valHashKey, encr.EncryptedVerifierHash, encr.HashSize, encr.SaltValue);
        encr.KeyValue = DecryptAgileFromKey(encr, valKeySizeKey, encr.EncryptedKeyValue, encryptionInfo.KeyData.KeyBits / 8, encr.SaltValue);

        if (!IsPasswordValid(hashProvider, encr)) throw (new SecurityException("Invalid password"));

        var ivhmac = GetFinalHash(hashProviderDataKey, BlockKey_HmacKey, encryptionInfo.KeyData.SaltValue);
        var key = DecryptAgileFromKey(encryptionInfo.KeyData, encr.KeyValue, encryptionInfo.DataIntegrity?.EncryptedHmacKey ?? Array.Empty<byte>(), encryptionInfo.KeyData.HashSize, ivhmac);

        ivhmac = GetFinalHash(hashProviderDataKey, BlockKey_HmacValue, encryptionInfo.KeyData.SaltValue);
        var value = DecryptAgileFromKey(encryptionInfo.KeyData, encr.KeyValue, encryptionInfo.DataIntegrity?.EncryptedHmacValue ?? Array.Empty<byte>(), encryptionInfo.KeyData.HashSize, ivhmac);

        using var hmca = GetHmacProvider(encryptionInfo.KeyData, key);
        var v2 = hmca.ComputeHash(data);

        for (int i = 0; i < v2.Length; i++)
        {
            if (value[i] != v2[i]) throw (new Exception("Dataintegrity key mismatch"));
        }

        int pos = 0;
        uint segment = 0;
        var doc = new MemoryStream();
        while (pos < size)
        {
            var segmentSize = (int)(size - pos > 4096 ? 4096 : size - pos);
            var bufferSize = (int)(encryptedData.Length - pos > 4096 ? 4096 : encryptedData.Length - pos);
            var ivTmp = new byte[4 + encryptionInfo.KeyData.SaltSize];
            Array.Copy(encryptionInfo.KeyData.SaltValue, 0, ivTmp, 0, encryptionInfo.KeyData.SaltSize);
            Array.Copy(BitConverter.GetBytes(segment), 0, ivTmp, encryptionInfo.KeyData.SaltSize, 4);
            var iv = hashProviderDataKey.ComputeHash(ivTmp);
            var buffer = new byte[bufferSize];
            Array.Copy(encryptedData, pos, buffer, 0, bufferSize);

            var b = DecryptAgileFromKey(encryptionInfo.KeyData, encr.KeyValue, buffer, segmentSize, iv);
            doc.Write(b, 0, b.Length);
            pos += segmentSize;
            segment++;
        }
        doc.Flush();
        return doc;
    }

    private static HashAlgorithm GetHashProvider(EncryptionInfoAgile.EncryptionKeyData encr)
    {
        return encr.HashAlgorithm switch
        {
            InternalHashAlogorithm.MD5 => MD5.Create(),
            InternalHashAlogorithm.SHA1 => SHA1.Create(),
            InternalHashAlogorithm.SHA256 => SHA256.Create(),
            InternalHashAlogorithm.SHA384 => SHA384.Create(),
            InternalHashAlogorithm.SHA512 => SHA512.Create(),
            _ => throw new NotSupportedException(string.Format("Hash provider is unsupported. {0}", encr.HashAlgorithm)),
        };
    }

    private static MemoryStream DecryptBinary(EncryptionInfoBinary encryptionInfo, string password, long size, byte[] encryptedData)
    {
        var doc = new MemoryStream();
        if (encryptionInfo.Header?.AlgID == AlgorithmID.AES128 || (encryptionInfo.Header?.AlgID == AlgorithmID.Flags && ((encryptionInfo.Flags & (Flags.fAES | Flags.fExternal | Flags.fCryptoAPI)) == (Flags.fAES | Flags.fCryptoAPI))) || encryptionInfo.Header?.AlgID == AlgorithmID.AES192 || encryptionInfo.Header?.AlgID == AlgorithmID.AES256)
        {
            using var decryptKey = Aes.Create();
            decryptKey.KeySize = encryptionInfo.Header.KeySize;
            decryptKey.Mode = CipherMode.ECB;
            decryptKey.Padding = PaddingMode.None;

            var key = GetPasswordHashBinary(password, encryptionInfo);
            if (!IsPasswordValid(key, encryptionInfo)) throw (new UnauthorizedAccessException("Invalid password"));

            using var decryptor = decryptKey.CreateDecryptor(key, null);
            using var dataStream = new MemoryStream(encryptedData);
            using var cryptoStream = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Read);

            var decryptedData = new byte[size];

            cryptoStream.Read(decryptedData, 0, (int)size);
            doc.Write(decryptedData, 0, (int)size);
        }
        return doc;
    }

    private static bool IsPasswordValid(byte[] key, EncryptionInfoBinary encryptionInfo)
    {
        using var decryptKey = Aes.Create();
        decryptKey.KeySize = encryptionInfo.Header?.KeySize ?? 0;
        decryptKey.Mode = CipherMode.ECB;
        decryptKey.Padding = PaddingMode.None;

        using var decryptor = decryptKey.CreateDecryptor(key, null);
        //Decrypt the verifier
        using var dataStream = new MemoryStream(encryptionInfo.Verifier?.EncryptedVerifier ?? Array.Empty<byte>());
        using var cryptoStream = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Read);
        var decryptedVerifier = new byte[16];
        cryptoStream.Read(decryptedVerifier, 0, 16);

        using var dataStream1 = new MemoryStream(encryptionInfo.Verifier?.EncryptedVerifierHash ?? Array.Empty<byte>());
        using var cryptoStream1 = new CryptoStream(dataStream1, decryptor, CryptoStreamMode.Read);

        //Decrypt the verifier hash
        var decryptedVerifierHash = new byte[16];
        cryptoStream1.Read(decryptedVerifierHash, 0, (int)16);

        //Get the hash for the decrypted verifier
        using var sha = SHA1.Create();
        var hash = sha.ComputeHash(decryptedVerifier);

        //Equal?
        for (int i = 0; i < 16; i++)
        {
            if (hash[i] != decryptedVerifierHash[i]) return false;
        }
        return true;
    }

    private static bool IsPasswordValid(HashAlgorithm sha, EncryptionInfoAgile.EncryptionKeyEncryptor encr)
    {
        var valHash = sha.ComputeHash(encr.VerifierHashInput);
        for (int i = 0; i < valHash.Length; i++)
        {
            if (encr.VerifierHash[i] != valHash[i]) return false;
        }
        return true;
    }

    private static byte[] DecryptAgileFromKey(EncryptionInfoAgile.EncryptionKeyData encr, byte[] key, byte[] encryptedData, long size, byte[] iv)
    {
        using var decryptKey = GetEncryptionAlgorithm(encr);
        decryptKey.BlockSize = encr.BlockSize << 3;
        decryptKey.KeySize = encr.KeyBits;
        decryptKey.Mode = CipherMode.CBC;
        decryptKey.Padding = PaddingMode.Zeros;

        using var decryptor = decryptKey.CreateDecryptor(FixHashSize(key, encr.KeyBits / 8), FixHashSize(iv, encr.BlockSize, 0x36));
        using var dataStream = new MemoryStream(encryptedData);
        using var cryptoStream = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Read);

        var decryptedData = new byte[size];
        cryptoStream.Read(decryptedData, 0, (int)size);
        return decryptedData;
    }

    private static SymmetricAlgorithm GetEncryptionAlgorithm(EncryptionInfoAgile.EncryptionKeyData encr)
    {
        return encr.CipherAlgorithm switch
        {
            InternalCipherAlgorithm.AES => Aes.Create(),
            InternalCipherAlgorithm.TRIPLE_DES or InternalCipherAlgorithm.TRIPLE_DES_112 => TripleDES.Create(),
            _ => throw (new NotSupportedException(string.Format("Unsupported Cipher Algorithm: {0}", encr.CipherAlgorithm.ToString()))),
        };
    }

    private static void EncryptAgileFromKey(EncryptionInfoAgile.EncryptionKeyEncryptor encr, byte[] key, byte[] data, long pos, long size, byte[] iv, MemoryStream ms)
    {
        using var encryptKey = GetEncryptionAlgorithm(encr);
        encryptKey.BlockSize = encr.BlockSize << 3;
        encryptKey.KeySize = encr.KeyBits;
        encryptKey.Mode = CipherMode.CBC;
        encryptKey.Padding = PaddingMode.Zeros;

        using var encryptor = encryptKey.CreateEncryptor(FixHashSize(key, encr.KeyBits / 8), FixHashSize(iv, 16, 0x36));
        var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        var cryptoSize = size % encr.BlockSize == 0 ? size : (size + (encr.BlockSize - (size % encr.BlockSize)));
        var buffer = new byte[size];
        Array.Copy(data, (int)pos, buffer, 0, (int)size);
        cryptoStream.Write(buffer, 0, (int)size);
        while (size % encr.BlockSize != 0)
        {
            cryptoStream.WriteByte(0);
            size++;
        }
    }

    private static byte[] GetPasswordHashBinary(string password, EncryptionInfoBinary encryptionInfo)
    {
        try
        {
            byte[] tempHash = new byte[4 + 20];    //Iterator + prev. hash
            HashAlgorithm hashProvider;
            if (encryptionInfo.Header?.AlgIDHash == AlgorithmHashID.SHA1 || encryptionInfo.Header?.AlgIDHash == AlgorithmHashID.App && (encryptionInfo.Flags & Flags.fExternal) == 0)
            {
                hashProvider = SHA1.Create();
            }
            else if (encryptionInfo.Header?.KeySize > 0 && encryptionInfo.Header.KeySize < 80)
            {
                throw new NotSupportedException("RC4 Hash provider is not supported. Must be SHA1(AlgIDHash == 0x8004)");
            }
            else
            {
                throw new NotSupportedException("Hash provider is invalid. Must be SHA1(AlgIDHash == 0x8004)");
            }

            var hash = GetPasswordHash(hashProvider, encryptionInfo.Verifier?.Salt ?? Array.Empty<byte>(), password, 50000, 20);

            // Append "block" (0)
            Array.Copy(hash, tempHash, hash.Length);
            Array.Copy(BitConverter.GetBytes(0), 0, tempHash, hash.Length, 4);
            hash = hashProvider.ComputeHash(tempHash);

            /***** Now use the derived key algorithm *****/
            var derivedKey = new byte[64];
            int keySizeBytes = encryptionInfo.Header.KeySize / 8;

            //First XOR hash bytes with 0x36 and fill the rest with 0x36
            for (int i = 0; i < derivedKey.Length; i++) derivedKey[i] = (byte)(i < hash.Length ? 0x36 ^ hash[i] : 0x36);


            var X1 = hashProvider.ComputeHash(derivedKey);

            //if verifier size is bigger than the key size we can return X1
            if ((int)(encryptionInfo.Verifier?.VerifierHashSize ?? 0) > keySizeBytes) return FixHashSize(X1, keySizeBytes);

            //Else XOR hash bytes with 0x5C and fill the rest with 0x5C
            for (int i = 0; i < derivedKey.Length; i++) derivedKey[i] = (byte)(i < hash.Length ? 0x5C ^ hash[i] : 0x5C);

            var X2 = hashProvider.ComputeHash(derivedKey);

            //Join the two and return 
            var join = new byte[X1.Length + X2.Length];

            Array.Copy(X1, 0, join, 0, X1.Length);
            Array.Copy(X2, 0, join, X1.Length, X2.Length);

            hashProvider.Dispose();
            return FixHashSize(join, keySizeBytes);
        }
        catch (Exception ex)
        {
            throw (new Exception("An error occured when the encryptionkey was created", ex));
        }
    }

    private static byte[] GetFinalHash(HashAlgorithm hashProvider, byte[] blockKey, byte[] hash)
    {
        //2.3.4.13 MS-OFFCRYPTO
        var tempHash = new byte[hash.Length + blockKey.Length];
        Array.Copy(hash, tempHash, hash.Length);
        Array.Copy(blockKey, 0, tempHash, hash.Length, blockKey.Length);
        var hashFinal = hashProvider.ComputeHash(tempHash);
        return hashFinal;
    }

    private static byte[] GetPasswordHash(HashAlgorithm hashProvider, byte[] salt, string password, int spinCount, int hashSize)
    {
        var tempHash = new byte[4 + hashSize];    //Iterator + prev. hash
        var hash = hashProvider.ComputeHash(CombinePassword(salt, password));

        //Iterate "spinCount" times, inserting i in first 4 bytes and then the prev. hash in byte 5-24
        for (int i = 0; i < spinCount; i++)
        {
            Array.Copy(BitConverter.GetBytes(i), tempHash, 4);
            Array.Copy(hash, 0, tempHash, 4, hash.Length);
            hash = hashProvider.ComputeHash(tempHash);
        }
        return hash;
    }

    private static byte[] FixHashSize(byte[] hash, int size, byte fill = 0)
    {
        if (hash.Length == size) return hash;
        else if (hash.Length < size)
        {
            var buff = new byte[size];
            Array.Copy(hash, buff, hash.Length);
            for (int i = hash.Length; i < size; i++) buff[i] = fill;
            return buff;
        }
        else
        {
            var buff = new byte[size];
            Array.Copy(hash, buff, size);
            return buff;
        }
    }

    private static byte[] CombinePassword(byte[] salt, string password)
    {
        if (password == "") password = "VelvetSweatshop";   //Used if Password is blank
        // Convert password to unicode...
        var passwordBuf = Encoding.Unicode.GetBytes(password);

        var inputBuf = new byte[salt.Length + passwordBuf.Length];
        Array.Copy(salt, inputBuf, salt.Length);
        Array.Copy(passwordBuf, 0, inputBuf, salt.Length, passwordBuf.Length);
        return inputBuf;
    }
}
