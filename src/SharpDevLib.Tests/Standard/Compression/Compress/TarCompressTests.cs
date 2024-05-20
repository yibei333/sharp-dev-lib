﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.Compress;

[TestClass]
public class TarCompressTests
{
    [TestMethod]
    public void CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/tar-create.tar");
        var option = new CompressOption(new List<string> { AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/Root") }, targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/tar-password-create.tar");
        var option = new CompressOption(new List<string> { AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/Root") }, targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.CompressAsync().GetAwaiter().GetResult();
    }
}