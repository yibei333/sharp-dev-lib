using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Basic.Encode;

[TestClass]
public class UrlEncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.Utf8Encode().UrlEncode());
        Assert.AreEqual(_urlEncode, _url.UrlEncode(), true);
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".UrlDecode().Serialize());
        Assert.AreEqual(" ", " ".UrlDecode());
        Assert.AreEqual(_url, _urlEncode.UrlDecode());
    }
}
