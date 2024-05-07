using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System.IO;
using System;
using static SharpDevLib.Standard.HashExtension;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class HashExtensionTests
{
    static readonly byte[] _emptyBytes = Array.Empty<byte>();
    static readonly byte[] _bytes = "foobar".ToUtf8Bytes();
    const string _secret = "123456";

    #region MD5
    [TestMethod]
    public void MD5HashBytesTest()
    {
        Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", _emptyBytes.MD5Hash());
        Assert.AreEqual("3858f62230ac3c915f300c664312c63f", _bytes.MD5Hash());
    }

    [TestMethod]
    public void MD5HashStreamTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("98f97a791ef1457579a5b7e88a495063", stream.MD5Hash());
        Assert.AreEqual("1ef1457579a5b7e8", stream.MD5Hash(MD5Length.Sixteen));
    }

    [TestMethod]
    public void HMACMD5HashBytesTest()
    {
        Assert.AreEqual("cab1380ea86d8acc9aa62390a58406aa", _emptyBytes.MD5Hash(_secret));
        Assert.AreEqual("16e0cbbcfe1b7e7716e50b1787442bca", _bytes.MD5Hash(_secret));
    }

    [TestMethod]
    public void HMACMD5HashStreamTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("2a5578d2b2dd93e0978cad09dd190f8f", stream.MD5Hash(_secret));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void HMACMD5HashExceptionTest()
    {
        _bytes.MD5Hash("01234567890123456789012345678901234567890123456789012345678901234");
    }
    #endregion

    #region SHA128
    [TestMethod]
    public void SHA128HashBytesTest()
    {
        Assert.AreEqual("da39a3ee5e6b4b0d3255bfef95601890afd80709", _emptyBytes.SHA128Hash());
        Assert.AreEqual("8843d7f92416211de9ebb963ff4ce28125932878", _bytes.SHA128Hash());
    }

    [TestMethod]
    public void SHA128HashStreamTest()
    {
        Assert.AreEqual("da39a3ee5e6b4b0d3255bfef95601890afd80709", new MemoryStream().SHA128Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("16ad856b462e68f965f6e93f66282a7ae891fdbc", stream.SHA128Hash());
    }

    [TestMethod]
    public void HMACSHA128HashBytesTest()
    {
        Assert.AreEqual("823688dafca7393d24c871a2da98a84d8732e927", _emptyBytes.SHA128Hash(_secret));
        Assert.AreEqual("434d11195a10d3df19b0fcebc6c0c147e3bc5ffa", _bytes.SHA128Hash(_secret));
    }

    [TestMethod]
    public void HMACSHA128HashStreamTest()
    {
        Assert.AreEqual("823688dafca7393d24c871a2da98a84d8732e927", new MemoryStream().SHA128Hash(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("8dbc2c077885f13bbb1e15171a4971a4d87cff50", stream.SHA128Hash(_secret));
    }
    #endregion

    #region SHA256
    [TestMethod]
    public void SHA256HashBytesTest()
    {
        Assert.AreEqual("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", _emptyBytes.SHA256Hash());
        Assert.AreEqual("c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2", _bytes.SHA256Hash());
    }

    [TestMethod]
    public void SHA256HashStreamTest()
    {
        Assert.AreEqual("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", new MemoryStream().SHA256Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("8f4ec1811c6c4261c97a7423b3a56d69f0f160074f39745af20bb5fcf65ccf78", stream.SHA256Hash());
    }

    [TestMethod]
    public void HMACSHA256HashBytesTest()
    {
        Assert.AreEqual("b946ccc987465afcda7e45b1715219711a13518d1f1663b8c53b848cb0143441", _emptyBytes.SHA256Hash(_secret));
        Assert.AreEqual("9708280ad4dad450336407679f91be06a815ec7aacf8e489e0c2e5dcbd103087", _bytes.SHA256Hash(_secret));
    }

    [TestMethod]
    public void HMACSHA256HashStreamTest()
    {
        Assert.AreEqual("b946ccc987465afcda7e45b1715219711a13518d1f1663b8c53b848cb0143441", new MemoryStream().SHA256Hash(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("0b813213ecb33353e90ccf0f94ccbbf5f351ba7b9bef10f848e9e7e215729f7e", stream.SHA256Hash(_secret));
    }
    #endregion

    #region SHA384
    [TestMethod]
    public void SHA384HashBytesTest()
    {
        Assert.AreEqual("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", _emptyBytes.SHA384Hash());
        Assert.AreEqual("3c9c30d9f665e74d515c842960d4a451c83a0125fd3de7392d7b37231af10c72ea58aedfcdf89a5765bf902af93ecf06", _bytes.SHA384Hash());
    }

    [TestMethod]
    public void SHA384HashStreamTest()
    {
        Assert.AreEqual("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", new MemoryStream().SHA384Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("75cc227fe8076e0456123113694e0fed43d28f45f8a4a67894732fe9893b0ab6194cb86b57f5f67316263382e2d72c4b", stream.SHA384Hash());
    }

    [TestMethod]
    public void HMACSHA384HashBytesTest()
    {
        Assert.AreEqual("e803f868ad733ea86c6d68c7f8ed6727f5e48d8bf40bcd7f07afe69324fec6567d547c1b7eeabfa33ef3bc34bf9011c8", _emptyBytes.SHA384Hash(_secret));
        Assert.AreEqual("7773f91bd2e7da73f676ab9da486c517d1efb0065ac49eaf1d28f70e4b1a3c5f6705a04de6e614d8e5099e8e30c8d7f3", _bytes.SHA384Hash(_secret));
    }

    [TestMethod]
    public void HMACSHA384HashStreamTest()
    {
        Assert.AreEqual("e803f868ad733ea86c6d68c7f8ed6727f5e48d8bf40bcd7f07afe69324fec6567d547c1b7eeabfa33ef3bc34bf9011c8", new MemoryStream().SHA384Hash(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("90e32a38d270e5c2c43845768e1d449e7a85803b24d822792e1a994dfd2d6e19577bde568d8c98e933d5f7d5953e6ccd", stream.SHA384Hash(_secret));
    }
    #endregion

    #region SHA512
    [TestMethod]
    public void SHA512HashBytesTest()
    {
        Assert.AreEqual("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", _emptyBytes.SHA512Hash());
        Assert.AreEqual("0a50261ebd1a390fed2bf326f2673c145582a6342d523204973d0219337f81616a8069b012587cf5635f6925f1b56c360230c19b273500ee013e030601bf2425", _bytes.SHA512Hash());
    }

    [TestMethod]
    public void SHA512HashStreamTest()
    {
        Assert.AreEqual("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e", new MemoryStream().SHA512Hash());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("75fbed00cbfdfaa6eba25791df285ec7fd6135fe8443adf86d4dc2bbfe061e84c3061e280c01366286c0366276976903666627c6d8e0a69b192c665bae8adb0b", stream.SHA512Hash());
    }

    [TestMethod]
    public void HMACSHA512HashBytesTest()
    {
        Assert.AreEqual("d3f2f066f0da13b4cd51085457a9c50f4dfc3ddc2b790133d49f6a11bd048ab7bf4292abaae52d5c2841f7eda24f51bce0858ef75dd0ee02283c73783d63c6a4", _emptyBytes.SHA512Hash(_secret));
        Assert.AreEqual("60287c042116ec7cc71ccc442bd961117a1a3a059b4debe5cc9e87856a2ffabf1f47c2fddf24e3a0b4eb0fb07e15d980dac9cf4f7bdd1548ef5db8d9acb79317", _bytes.SHA512Hash(_secret));
    }

    [TestMethod]
    public void HMACSHA512HashStreamTest()
    {
        Assert.AreEqual("d3f2f066f0da13b4cd51085457a9c50f4dfc3ddc2b790133d49f6a11bd048ab7bf4292abaae52d5c2841f7eda24f51bce0858ef75dd0ee02283c73783d63c6a4", new MemoryStream().SHA512Hash(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("6ad68a997b23d75c73f13872b8f56f4841f27b4d91cc4cbf94211a57d61b98e5442abd1f3363e25eead1efc0e70e7f438f118b2fe0050d728ecd07958d2a3d99", stream.SHA512Hash(_secret));
    }
    #endregion
}