using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Standard.Encode;

[TestClass]
public class Base64EncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.Base64Encode());
        Assert.AreEqual(_base64, _str.Utf8Decode().Base64Encode());
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".Base64Decode().Serialize());
        Assert.AreEqual(_emptyBytes.Serialize(), " ".Base64Decode().Serialize());
        Assert.AreNotEqual(_bytes, _base64.Base64Decode());
        Assert.AreEqual(_str, _base64.Base64Decode().Utf8Encode());
        Assert.AreEqual(_bytes.Serialize(), _base64.Base64Decode().Serialize());
    }
}
