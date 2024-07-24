using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Hash.Sha;

[TestClass]
public class Sha384ExtensionTests : HashTests
{
    #region SHA384
    [TestMethod]
    public void SHA384HashBytesTest()
    {
        Assert.AreEqual("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", _emptyBytes.Sha384());
        Assert.AreEqual("3c9c30d9f665e74d515c842960d4a451c83a0125fd3de7392d7b37231af10c72ea58aedfcdf89a5765bf902af93ecf06", _bytes.Sha384());
    }

    [TestMethod]
    public void SHA384HashStreamTest()
    {
        Assert.AreEqual("38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b", new MemoryStream().Sha384());
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("75cc227fe8076e0456123113694e0fed43d28f45f8a4a67894732fe9893b0ab6194cb86b57f5f67316263382e2d72c4b", stream.Sha384());
    }
    #endregion

    #region HMACSHA384
    [TestMethod]
    public void HMACSHA384HashBytesTest()
    {
        Assert.AreEqual("e803f868ad733ea86c6d68c7f8ed6727f5e48d8bf40bcd7f07afe69324fec6567d547c1b7eeabfa33ef3bc34bf9011c8", _emptyBytes.HmacSha384(_secret));
        Assert.AreEqual("7773f91bd2e7da73f676ab9da486c517d1efb0065ac49eaf1d28f70e4b1a3c5f6705a04de6e614d8e5099e8e30c8d7f3", _bytes.HmacSha384(_secret));
    }

    [TestMethod]
    public void HMACSHA384HashStreamTest()
    {
        Assert.AreEqual("e803f868ad733ea86c6d68c7f8ed6727f5e48d8bf40bcd7f07afe69324fec6567d547c1b7eeabfa33ef3bc34bf9011c8", new MemoryStream().HmacSha384(_secret));
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        var stream = File.OpenRead(path);
        Assert.AreEqual("90e32a38d270e5c2c43845768e1d449e7a85803b24d822792e1a994dfd2d6e19577bde568d8c98e933d5f7d5953e6ccd", stream.HmacSha384(_secret));
    }
    #endregion
}
