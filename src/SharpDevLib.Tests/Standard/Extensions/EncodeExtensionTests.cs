using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class EncodeExtensionTests
{
    const string _str = "foo";
    const string _hex = "666f6f";
    const string _base64 = "Zm9v";
    const string _url = "https://foo.com/bar?query=baz";
    const string _urlEncode = "https%3A%2F%2Ffoo.com%2Fbar%3Fquery%3Dbaz";
    const string _base64UrlEncode = "aHR0cHM6Ly9mb28uY29tL2Jhcj9xdWVyeT1iYXo";
    static readonly byte[] _bytes = new byte[] { 102, 111, 111 };

    #region UTF8
    [TestMethod]
    public void ToUtf8StringTest()
    {
        Assert.AreEqual(string.Empty, ((byte[]?)null).ToUtf8String());
        Assert.AreEqual(string.Empty, (Array.Empty<byte>()).ToUtf8String());
        Assert.AreEqual(_str, _bytes.ToUtf8String());
    }

    [TestMethod]
    public void ToUtf8BytesTest()
    {
        Assert.AreEqual(null, ((string?)null).ToUtf8Bytes());
        Assert.AreEqual(null, "".ToUtf8Bytes());
        Assert.AreEqual(null, " ".ToUtf8Bytes());
        Assert.AreNotEqual(_bytes, _str.ToUtf8Bytes());
        Assert.AreEqual(_bytes.Serialize(), _str.ToUtf8Bytes().Serialize());
    }
    #endregion

    #region HexString
    [TestMethod]
    public void ToHexStringTest()
    {
        Assert.AreEqual(string.Empty, ((byte[]?)null).ToHexString());
        Assert.AreEqual(string.Empty, (Array.Empty<byte>()).ToHexString());
        Assert.AreEqual(_hex, _str.ToUtf8Bytes().ToHexString());
    }

    [TestMethod]
    public void FromHexStringTest()
    {
        Assert.AreEqual(null, ((string?)null).FromHexString());
        Assert.AreEqual(null, "".FromHexString());
        Assert.AreEqual(null, " ".FromHexString());
        Assert.AreNotEqual(_bytes, _hex.FromHexString());
        Assert.AreEqual(_bytes.Serialize(), _hex.FromHexString().Serialize());
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void FromHexStringExceptionTest()
    {
        (_hex + "a").FromHexString();
    }
    #endregion

    #region Base64
    [TestMethod]
    public void Base64EncodeTest()
    {
        Assert.AreEqual(string.Empty, ((byte[]?)null).Base64Encode());
        Assert.AreEqual(string.Empty, (Array.Empty<byte>()).Base64Encode());
        Assert.AreEqual(_base64, _str.ToUtf8Bytes().Base64Encode());
    }

    [TestMethod]
    public void Base64DecodeTest()
    {
        Assert.AreEqual(null, ((string?)null).Base64Decode());
        Assert.AreEqual(null, "".Base64Decode());
        Assert.AreEqual(null, " ".Base64Decode());
        Assert.AreNotEqual(_bytes, _base64.Base64Decode());
        Assert.AreEqual(_str, _base64.Base64Decode().ToUtf8String());
        Assert.AreEqual(_bytes.Serialize(), _base64.Base64Decode().Serialize());
    }
    #endregion

    #region Url
    [TestMethod]
    public void UrlEncodeTest()
    {
        Assert.AreEqual(string.Empty, ((byte[]?)null).UrlEncode());
        Assert.AreEqual(string.Empty, (Array.Empty<byte>()).UrlEncode());
        Assert.AreEqual(_urlEncode, _url.ToUtf8Bytes().UrlEncode(), true);
    }

    [TestMethod]
    public void UrlDecodeTest()
    {
        Assert.AreEqual(null, ((string?)null).UrlDecode());
        Assert.AreEqual(null, "".UrlDecode());
        Assert.AreEqual(null, " ".UrlDecode());
        Assert.AreEqual(_url, _urlEncode.UrlDecode()?.ToUtf8String());
    }
    #endregion

    #region Base64Url
    [TestMethod]
    public void Base64UrlEncodeTest()
    {
        Assert.AreEqual(string.Empty, ((byte[]?)null).Base64UrlEncode());
        Assert.AreEqual(string.Empty, (Array.Empty<byte>()).Base64UrlEncode());
        Assert.AreEqual(_base64UrlEncode, _url.ToUtf8Bytes().Base64UrlEncode());
    }

    [TestMethod]
    public void Base64UrlDecodeTest()
    {
        Assert.AreEqual(null, ((string?)null).Base64UrlDecode());
        Assert.AreEqual(null, "".Base64UrlDecode());
        Assert.AreEqual(null, " ".Base64UrlDecode());
        Assert.AreEqual(_url, _base64UrlEncode.Base64UrlDecode().ToUtf8String());
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void Base64UrlDecodeExceptionTest()
    {
        (_base64UrlEncode + "__").Base64UrlDecode();
    }
    #endregion
}
