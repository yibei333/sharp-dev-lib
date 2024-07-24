using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Standard.Encode;

[TestClass]
public class Utf8EncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.Utf8Encode());
        Assert.AreEqual(_str, _bytes.Utf8Encode());
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".Utf8Decode().Serialize());
        Assert.AreNotEqual(_bytes, _str.Utf8Decode());
        Assert.AreEqual(_bytes.Serialize(), _str.Utf8Decode().Serialize());
    }
}
