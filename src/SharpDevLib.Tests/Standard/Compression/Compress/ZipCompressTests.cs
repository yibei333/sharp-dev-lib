using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.Compress;

[TestClass]
public class ZipCompressTests
{
    [TestMethod]
    public void ZipTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/zip-create.zip");
        var option = new CompressOption(new List<string> { AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/Root") }, targetPath);
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void ZipWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/zip-password-create.zip");
        var option = new CompressOption(new List<string> { AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/Root") }, targetPath)
        {
            Password = "abc",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }
}
