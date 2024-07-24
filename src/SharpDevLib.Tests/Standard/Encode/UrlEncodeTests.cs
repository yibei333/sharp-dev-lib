using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Standard.Encode;

[TestClass]
public class UrlEncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.UrlEncode());
        Assert.AreEqual(_urlEncode, _url.Utf8Decode().UrlEncode(), true);
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".UrlDecode().Serialize());
        Assert.AreEqual(" ", " ".UrlDecode().Utf8Encode());
        Assert.AreEqual(_url, _urlEncode.UrlDecode()?.Utf8Encode());
    }
}
