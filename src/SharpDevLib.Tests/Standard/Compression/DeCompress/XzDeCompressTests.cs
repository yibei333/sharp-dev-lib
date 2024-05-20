﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.DeCompress;

[TestClass]
public class XzDeCompressTests
{
    [TestMethod]
    public void TarDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/xz-tar-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/xz.tar.xz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/xz-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/xz.xz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("xz")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("xz")));
    }
}