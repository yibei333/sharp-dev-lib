using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Hash.Sha;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Hash.Sha;

[TestClass]
public class Sha512ExtensionTests : HashTests
{
    #region SHA512
    [TestMethod]
    public void SHA512HashBytesTest()
    {
        Assert.AreEqual("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", _emptyBytes.Sha512());
        Assert.AreEqual("0a50261ebd1a390fed2bf326f2673c145582a6342d523204973d0219337f81616a8069b012587cf5635f6925f1b56c360230c19b273500ee013e030601bf2425", _bytes.Sha512());
    }

    [TestMethod]
    public void SHA512HashStreamTest()
    {
        Assert.AreEqual("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", new MemoryStream().Sha512());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("75fbed00cbfdfaa6eba25791df285ec7fd6135fe8443adf86d4dc2bbfe061e84c3061e280c01366286c0366276976903666627c6d8e0a69b192c665bae8adb0b", stream.Sha512());
    }
    #endregion

    #region HMACSHA512
    [TestMethod]
    public void HMACSHA512HashBytesTest()
    {
        Assert.AreEqual("d3f2f066f0da13b4cd51085457a9c50f4dfc3ddc2b790133d49f6a11bd048ab7bf4292abaae52d5c2841f7eda24f51bce0858ef75dd0ee02283c73783d63c6a4", _emptyBytes.HmacSha512(_secret));
        Assert.AreEqual("60287c042116ec7cc71ccc442bd961117a1a3a059b4debe5cc9e87856a2ffabf1f47c2fddf24e3a0b4eb0fb07e15d980dac9cf4f7bdd1548ef5db8d9acb79317", _bytes.HmacSha512(_secret));
    }

    [TestMethod]
    public void HMACSHA512HashStreamTest()
    {
        Assert.AreEqual("d3f2f066f0da13b4cd51085457a9c50f4dfc3ddc2b790133d49f6a11bd048ab7bf4292abaae52d5c2841f7eda24f51bce0858ef75dd0ee02283c73783d63c6a4", new MemoryStream().HmacSha512(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("6ad68a997b23d75c73f13872b8f56f4841f27b4d91cc4cbf94211a57d61b98e5442abd1f3363e25eead1efc0e70e7f438f118b2fe0050d728ecd07958d2a3d99", stream.HmacSha512(_secret));
    }
    #endregion
}
