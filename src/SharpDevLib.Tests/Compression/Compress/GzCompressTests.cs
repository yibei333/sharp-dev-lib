using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests.Compression.Compress;

[TestClass]
public class GzCompressTests
{
    [TestMethod]
    public void TarCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-create.tgz");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize()),
            IncludeSourceDiretory = true
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public void CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-create.gz");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root/foo.txt")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize()),
            IncludeSourceDiretory = true
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public void CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-password-create.tar");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize()),
        };
        Assert.ThrowsExactly<InvalidDataException>(() => option.CompressAsync().GetAwaiter().GetResult());
    }
}
