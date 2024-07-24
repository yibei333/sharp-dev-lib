using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Hash.Sha;

[TestClass]
public class Sha128ExtensionTests : HashTests
{
    #region SHA128
    [TestMethod]
    public void SHA128HashBytesTest()
    {
        Assert.AreEqual("da39a3ee5e6b4b0d3255bfef95601890afd80709", _emptyBytes.Sha128());
        Assert.AreEqual("8843d7f92416211de9ebb963ff4ce28125932878", _bytes.Sha128());
    }

    [TestMethod]
    public void SHA128HashStreamTest()
    {
        Assert.AreEqual("da39a3ee5e6b4b0d3255bfef95601890afd80709", new MemoryStream().Sha128());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc", stream.Sha128());
    }
    #endregion

    #region HMACSHA128
    [TestMethod]
    public void HMACSHA128HashBytesTest()
    {
        Assert.AreEqual("823688dafca7393d24c871a2da98a84d8732e927", _emptyBytes.HmacSha128(_secret));
        Assert.AreEqual("434d11195a10d3df19b0fcebc6c0c147e3bc5ffa", _bytes.HmacSha128(_secret));
    }

    [TestMethod]
    public void HMACSHA128HashStreamTest()
    {
        Assert.AreEqual("823688dafca7393d24c871a2da98a84d8732e927", new MemoryStream().HmacSha128(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("8dbc2c077885f13bbb1e15171a4971a4d87cff50", stream.HmacSha128(_secret));
    }
    #endregion
}
