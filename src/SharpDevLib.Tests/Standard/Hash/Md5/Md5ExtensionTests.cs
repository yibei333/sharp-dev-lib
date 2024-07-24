using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Hash.Md5;

[TestClass]
public class Md5ExtensionTests : HashTests
{
    #region MD5
    [TestMethod]
    public void MD5HashBytesTest()
    {
        Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", _emptyBytes.Md5());
        Assert.AreEqual("3858f62230ac3c915f300c664312c63f", _bytes.Md5());
        Console.WriteLine(_bytes.Md5(Md5OutputLength.Sixteen));
    }

    [TestMethod]
    public void MD5HashStreamTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("98f97a791ef1457579a5b7e88a495063", stream.Md5());
        Assert.AreEqual("1ef1457579a5b7e8", stream.Md5(Md5OutputLength.Sixteen));
    }
    #endregion

    #region HMACMD5
    [TestMethod]
    public void HMACMD5HashBytesTest()
    {
        Assert.AreEqual("cab1380ea86d8acc9aa62390a58406aa", _emptyBytes.HmacMd5(_secret));
        Assert.AreEqual("16e0cbbcfe1b7e7716e50b1787442bca", _bytes.HmacMd5(_secret));
    }

    [TestMethod]
    public void HMACMD5HashStreamTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("2a5578d2b2dd93e0978cad09dd190f8f", stream.HmacMd5(_secret));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void HMACMD5HashExceptionTest()
    {
        _bytes.HmacMd5("01234567890123456789012345678901234567890123456789012345678901234".Utf8Decode());
    }
    #endregion
}
