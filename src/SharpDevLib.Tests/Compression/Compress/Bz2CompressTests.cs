using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Compression;
using System;
using System.IO;

namespace SharpDevLib.Tests.Compression.Compress;

[TestClass]
public class Bz2CompressTests
{
    [TestMethod]
    public void TarCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-create.tar.bz2");
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
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-create.bz2");
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
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-password-create.bz2");
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
