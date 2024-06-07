using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Compression;
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
        var option = new CompressOption([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat)),
            IncludeSourceDiretory = true
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-create.gz");
        var option = new CompressOption([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root/foo.txt")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat)),
            IncludeSourceDiretory = true
        };
        option.CompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-password-create.tar");
        var option = new CompressOption([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat)),
        };
        option.CompressAsync().GetAwaiter().GetResult();
    }
}
