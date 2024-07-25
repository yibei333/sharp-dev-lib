using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SharpDevLib.Tests.Standard.Encode;

[TestClass]
public class Base64UrlEncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.Base64UrlEncode());
        Assert.AreEqual(_base64UrlEncode, _url.Utf8Decode().Base64UrlEncode());
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".Base64UrlDecode().Serialize());
        Assert.AreEqual(_emptyBytes.Serialize(), " ".Base64UrlDecode().Serialize());
        Assert.AreEqual(_url, _base64UrlEncode.Base64UrlDecode().Utf8Encode());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void DecodeExceptionTest()
    {
        (_base64UrlEncode + "__").Base64UrlDecode();
    }
}
