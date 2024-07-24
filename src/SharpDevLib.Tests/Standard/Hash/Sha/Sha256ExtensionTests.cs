using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Hash.Sha;

[TestClass]
public class Sha256ExtensionTests : HashTests
{
    #region SHA256
    [TestMethod]
    public void SHA256HashBytesTest()
    {
        Assert.AreEqual("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", _emptyBytes.Sha256());
        Assert.AreEqual("c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2", _bytes.Sha256());
    }

    [TestMethod]
    public void SHA256HashStreamTest()
    {
        Assert.AreEqual("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", new MemoryStream().Sha256());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("8f4ec1811c6c4261c97a7423b3a56d69f0f160074f39745af20bb5fcf65ccf78", stream.Sha256());
    }
    #endregion

    #region HMACSHA256
    [TestMethod]
    public void HMACSHA256HashBytesTest()
    {
        Assert.AreEqual("b946ccc987465afcda7e45b1715219711a13518d1f1663b8c53b848cb0143441", _emptyBytes.HmacSha256(_secret));
        Assert.AreEqual("9708280ad4dad450336407679f91be06a815ec7aacf8e489e0c2e5dcbd103087", _bytes.HmacSha256(_secret));
    }

    [TestMethod]
    public void HMACSHA256HashStreamTest()
    {
        Assert.AreEqual("b946ccc987465afcda7e45b1715219711a13518d1f1663b8c53b848cb0143441", new MemoryStream().HmacSha256(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("0b813213ecb33353e90ccf0f94ccbbf5f351ba7b9bef10f848e9e7e215729f7e", stream.HmacSha256(_secret));
    }
    #endregion
}
