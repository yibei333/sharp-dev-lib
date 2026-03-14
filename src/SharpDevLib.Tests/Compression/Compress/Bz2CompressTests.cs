using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Compression.Compress;

[TestClass]
public class Bz2CompressTests
{
    [TestMethod]
    public async Task TarCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-create.tar.bz2");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
            IncludeSourceDiretory = true
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public async Task CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-create.bz2");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root/foo.txt")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
            IncludeSourceDiretory = true
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public void CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/bz2-password-create.bz2");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        Assert.ThrowsAsync<InvalidDataException>(option.CompressAsync);
    }
}
