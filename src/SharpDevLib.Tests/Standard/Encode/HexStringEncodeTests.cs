using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SharpDevLib.Tests.Standard.Encode;

[TestClass]
public class HexStringEncodeTests : EncodeTests
{
    [TestMethod]
    public void EncodeTest()
    {
        Assert.AreEqual(string.Empty, _emptyBytes.HexStringEncode());
        Assert.AreEqual(_hex, _str.Utf8Decode().HexStringEncode());
    }

    [TestMethod]
    public void DecodeTest()
    {
        Assert.AreEqual(_emptyBytes.Serialize(), "".HexStringDecode().Serialize());
        Assert.AreEqual(_emptyBytes.Serialize(), " ".HexStringDecode().Serialize());
        Assert.AreNotEqual(_bytes, _hex.HexStringDecode());
        Assert.AreEqual(_bytes.Serialize(), _hex.HexStringDecode().Serialize());
    }

    [TestMethod]
    public void DecodeExceptionTest()
    {
        Assert.ThrowsExactly<InvalidDataException>(() => (_hex + "a").HexStringDecode());
    }
}
