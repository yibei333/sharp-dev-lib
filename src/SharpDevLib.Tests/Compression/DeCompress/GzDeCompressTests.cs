using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Compression.DeCompress;

[TestClass]
public class GzDeCompressTests
{
    [TestMethod]
    public async Task TarGzDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-tar-gz-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.tar.gz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption))
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public async Task TgzDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-tgz-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.tgz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption))
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public async Task DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.gz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption))
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
