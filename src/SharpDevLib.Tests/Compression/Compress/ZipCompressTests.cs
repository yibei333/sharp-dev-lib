using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Compression.Compress;

[TestClass]
public class ZipCompressTests
{
    [TestMethod]
    public async Task CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-create.zip");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public async Task CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-password-create.zip");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public async Task SyncCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-create-sync.zip");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public async Task SyncCompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-password-create-sync.zip");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }
}
