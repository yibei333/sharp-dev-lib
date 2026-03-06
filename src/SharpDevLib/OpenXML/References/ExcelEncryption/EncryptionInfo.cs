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
using System.Text;
using System.Xml;

namespace SharpDevLib.OpenXML.References.ExcelEncryption;

internal abstract class EncryptionInfo
{
    internal short MajorVersion;
    internal short MinorVersion;
    internal abstract void Read(byte[] data);

    internal static EncryptionInfo ReadBinary(byte[] data)
    {
        var majorVersion = BitConverter.ToInt16(data, 0);
        var minorVersion = BitConverter.ToInt16(data, 2);
        EncryptionInfo ret;
        if ((minorVersion == 2 || minorVersion == 3) && majorVersion <= 4) ret = new EncryptionInfoBinary();
        else if (majorVersion == 4 && minorVersion == 4) ret = new EncryptionInfoAgile();
        else throw (new NotSupportedException("Unsupported encryption format"));
        ret.MajorVersion = majorVersion;
        ret.MinorVersion = minorVersion;
        ret.Read(data);
        return ret;
    }
}

internal enum InternalCipherAlgorithm
{
    /// <summary>
    /// AES. MUST conform to the AES algorithm.
    /// </summary>
    AES,
    /// <summary>
    /// RC2. MUST conform to [RFC2268].
    /// </summary>
    RC2,
    /// <summary>
    /// RC4. 
    /// </summary>
    RC4,
    /// <summary>
    /// MUST conform to the DES algorithm.
    /// </summary>
    DES,
    /// <summary>
    /// MUST conform to the [DRAFT-DESX] algorithm.
    /// </summary>
    DESX,
    /// <summary>
    /// 3DES. MUST conform to the [RFC1851] algorithm. 
    /// </summary>
    TRIPLE_DES,
    /// 3DES_112 MUST conform to the [RFC1851] algorithm. 
    TRIPLE_DES_112
}
/// <summary>
/// Hashalgorithm
/// </summary>
internal enum InternalHashAlogorithm
{
    /// <summary>
    /// Sha 1-MUST conform to [RFC4634]
    /// </summary>
    SHA1,
    /// <summary>
    /// Sha 256-MUST conform to [RFC4634]
    /// </summary>
    SHA256,
    /// <summary>
    /// Sha 384-MUST conform to [RFC4634]
    /// </summary>
    SHA384,
    /// <summary>
    /// Sha 512-MUST conform to [RFC4634]
    /// </summary>
    SHA512,
    /// <summary>
    /// MD5
    /// </summary>
    MD5,
    /// <summary>
    /// MD4
    /// </summary>
    MD4,
    /// <summary>
    /// MD2
    /// </summary>
    MD2,
    /// <summary>
    /// RIPEMD-128 MUST conform to [ISO/IEC 10118]
    /// </summary>
    RIPEMD128,
    /// <summary>
    /// RIPEMD-160 MUST conform to [ISO/IEC 10118]
    /// </summary>
    RIPEMD160,
    /// <summary>
    /// WHIRLPOOL MUST conform to [ISO/IEC 10118]
    /// </summary>
    WHIRLPOOL
}
/// <summary>
/// Handels the agile encryption
/// </summary>
internal class EncryptionInfoAgile : EncryptionInfo
{
    private readonly XmlNamespaceManager _nsm;
    public EncryptionInfoAgile()
    {
        var nt = new NameTable();
        _nsm = new XmlNamespaceManager(nt);
        _nsm.AddNamespace("d", "http://schemas.microsoft.com/office/2006/encryption");
        _nsm.AddNamespace("c", "http://schemas.microsoft.com/office/2006/keyEncryptor/certificate");
        _nsm.AddNamespace("p", "http://schemas.microsoft.com/office/2006/keyEncryptor/password");
        KeyEncryptors = new List<EncryptionKeyEncryptor>();
    }
    internal class EncryptionKeyData : XmlHelper
    {
        public EncryptionKeyData(XmlNamespaceManager nsm, XmlNode topNode) : base(nsm, topNode) { }
        internal byte[] SaltValue
        {
            get
            {
                var s = GetXmlNodeString("@saltValue");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@saltValue", Convert.ToBase64String(value));
        }

        internal InternalHashAlogorithm HashAlgorithm { get => GetHashAlgorithm(GetXmlNodeString("@hashAlgorithm")); set => SetXmlNodeString("@hashAlgorithm", GetHashAlgorithmString(value)); }

        static InternalHashAlogorithm GetHashAlgorithm(string v)
        {
            return v switch
            {
                "RIPEMD-128" => InternalHashAlogorithm.RIPEMD128,
                "RIPEMD-160" => InternalHashAlogorithm.RIPEMD160,
                "SHA-1" => InternalHashAlogorithm.SHA1,
                _ => Enum.TryParse<InternalHashAlogorithm>(v, out var result) ? result : throw new InvalidDataException("Invalid Hash algorithm"),
            };
        }

        static string GetHashAlgorithmString(InternalHashAlogorithm value)
        {
            return value switch
            {
                InternalHashAlogorithm.RIPEMD128 => "RIPEMD-128",
                InternalHashAlogorithm.RIPEMD160 => "RIPEMD-160",
                InternalHashAlogorithm.SHA1 => "SHA-1",
                _ => value.ToString(),
            };
        }

        internal InternalCipherAlgorithm CipherAlgorithm
        {
            get => GetCipherAlgorithm(GetXmlNodeString("@cipherAlgorithm"));
            set => SetXmlNodeString("@cipherAlgorithm", GetCipherAlgorithmString(value));
        }

        static InternalCipherAlgorithm GetCipherAlgorithm(string v)
        {
            return v switch
            {
                "3DES" => InternalCipherAlgorithm.TRIPLE_DES,
                "3DES_112" => InternalCipherAlgorithm.TRIPLE_DES_112,
                _ => Enum.TryParse<InternalCipherAlgorithm>(v, out var result) ? result : throw new InvalidDataException("Invalid Hash algorithm"),
            };
        }

        static string GetCipherAlgorithmString(InternalCipherAlgorithm alg)
        {
            return alg switch
            {
                InternalCipherAlgorithm.TRIPLE_DES => "3DES",
                InternalCipherAlgorithm.TRIPLE_DES_112 => "3DES_112",
                _ => alg.ToString(),
            };
        }
        internal int HashSize
        {
            get => GetXmlNodeInt("@hashSize");
            set => SetXmlNodeString("@hashSize", value.ToString());
        }
        internal int KeyBits
        {
            get => GetXmlNodeInt("@keyBits");
            set => SetXmlNodeString("@keyBits", value.ToString());
        }
        internal int BlockSize
        {
            get => GetXmlNodeInt("@blockSize");
            set => SetXmlNodeString("@blockSize", value.ToString());
        }
        internal int SaltSize
        {
            get => GetXmlNodeInt("@saltSize");
            set => SetXmlNodeString("@saltSize", value.ToString());
        }
    }
    internal class EncryptionDataIntegrity : XmlHelper
    {
        public EncryptionDataIntegrity(XmlNamespaceManager nsm, XmlNode topNode) : base(nsm, topNode) { }
        internal byte[] EncryptedHmacValue
        {
            get
            {
                var s = GetXmlNodeString("@encryptedHmacValue");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@encryptedHmacValue", Convert.ToBase64String(value));
        }
        internal byte[] EncryptedHmacKey
        {
            get
            {
                var s = GetXmlNodeString("@encryptedHmacKey");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@encryptedHmacKey", Convert.ToBase64String(value));
        }
    }
    internal class EncryptionKeyEncryptor : EncryptionKeyData
    {
        public EncryptionKeyEncryptor(XmlNamespaceManager nsm, XmlNode topNode) : base(nsm, topNode)
        {
            VerifierHashInput = Array.Empty<byte>();
            VerifierHash = Array.Empty<byte>();
            KeyValue = Array.Empty<byte>();
        }
        internal byte[] EncryptedKeyValue
        {
            get
            {
                var s = GetXmlNodeString("@encryptedKeyValue");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@encryptedKeyValue", Convert.ToBase64String(value));
        }
        internal byte[] EncryptedVerifierHash
        {
            get
            {
                var s = GetXmlNodeString("@encryptedVerifierHashValue");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@encryptedVerifierHashValue", Convert.ToBase64String(value));
        }
        internal byte[] EncryptedVerifierHashInput
        {
            get
            {
                var s = GetXmlNodeString("@encryptedVerifierHashInput");
                if (!string.IsNullOrEmpty(s)) return Convert.FromBase64String(s);
                return Array.Empty<byte>();
            }
            set => SetXmlNodeString("@encryptedVerifierHashInput", Convert.ToBase64String(value));
        }
        internal byte[] VerifierHashInput { get; set; }
        internal byte[] VerifierHash { get; set; }
        internal byte[] KeyValue { get; set; }
        internal int SpinCount
        {
            get => GetXmlNodeInt("@spinCount");
            set => SetXmlNodeString("@spinCount", value.ToString());
        }
    }

    internal EncryptionDataIntegrity? DataIntegrity { get; set; }
    internal EncryptionKeyData? KeyData { get; set; }
    internal List<EncryptionKeyEncryptor> KeyEncryptors { get; private set; }

    internal XmlDocument? Xml { get; set; }
    internal override void Read(byte[] data)
    {
        var byXml = new byte[data.Length - 8];
        Array.Copy(data, 8, byXml, 0, data.Length - 8);
        var xml = Encoding.UTF8.GetString(byXml);
        ReadFromXml(xml);
    }

    internal void ReadFromXml(string xml)
    {
        Xml = new XmlDocument();
        XmlHelper.LoadXmlSafe(Xml, xml, Encoding.UTF8);
        var node = Xml.SelectSingleNode("/d:encryption/d:keyData", _nsm);
        KeyData = new EncryptionKeyData(_nsm, node!);
        node = Xml.SelectSingleNode("/d:encryption/d:dataIntegrity", _nsm);
        DataIntegrity = new EncryptionDataIntegrity(_nsm, node!);
        KeyEncryptors = new List<EncryptionKeyEncryptor>();

        var list = Xml.SelectNodes("/d:encryption/d:keyEncryptors/d:keyEncryptor/p:encryptedKey", _nsm);
        if (list == null) return;
        foreach (XmlNode n in list) KeyEncryptors.Add(new EncryptionKeyEncryptor(_nsm, n));
    }
}

/// <summary>
/// Handles the EncryptionInfo stream
/// </summary>
internal class EncryptionInfoBinary : EncryptionInfo
{
    internal Flags Flags;
    internal uint HeaderSize;
    internal EncryptionHeader? Header;
    internal EncryptionVerifier? Verifier;
    internal override void Read(byte[] data)
    {
        Flags = (Flags)BitConverter.ToInt32(data, 4);
        HeaderSize = (uint)BitConverter.ToInt32(data, 8);

        /**** EncryptionHeader ****/
        Header = new EncryptionHeader
        {
            Flags = (Flags)BitConverter.ToInt32(data, 12),
            SizeExtra = BitConverter.ToInt32(data, 16),
            AlgID = (AlgorithmID)BitConverter.ToInt32(data, 20),
            AlgIDHash = (AlgorithmHashID)BitConverter.ToInt32(data, 24),
            KeySize = BitConverter.ToInt32(data, 28),
            ProviderType = (ProviderType)BitConverter.ToInt32(data, 32),
            Reserved1 = BitConverter.ToInt32(data, 36),
            Reserved2 = BitConverter.ToInt32(data, 40)
        };

        var text = new byte[(int)HeaderSize - 34];
        Array.Copy(data, 44, text, 0, (int)HeaderSize - 34);
        Header.CSPName = Encoding.Unicode.GetString(text);

        int pos = (int)HeaderSize + 12;

        /**** EncryptionVerifier ****/
        Verifier = new EncryptionVerifier { SaltSize = (uint)BitConverter.ToInt32(data, pos) };
        Verifier.Salt = new byte[Verifier.SaltSize];

        Array.Copy(data, pos + 4, Verifier.Salt, 0, (int)Verifier.SaltSize);

        Verifier.EncryptedVerifier = new byte[16];
        Array.Copy(data, pos + 20, Verifier.EncryptedVerifier, 0, 16);

        Verifier.VerifierHashSize = (uint)BitConverter.ToInt32(data, pos + 36);
        Verifier.EncryptedVerifierHash = new byte[Verifier.VerifierHashSize];
        Array.Copy(data, pos + 40, Verifier.EncryptedVerifierHash, 0, (int)Verifier.VerifierHashSize);
    }

    internal byte[] WriteBinary()
    {
        if (Header == null || Verifier == null) throw new InvalidOperationException("read first");
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        bw.Write(MajorVersion);
        bw.Write(MinorVersion);
        bw.Write((int)Flags);
        var header = Header.WriteBinary();
        bw.Write((uint)header.Length);
        bw.Write(header);
        bw.Write(Verifier.WriteBinary());

        bw.Flush();
        return ms.ToArray();
    }
}
