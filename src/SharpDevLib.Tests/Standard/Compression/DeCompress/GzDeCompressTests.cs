using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.DeCompress;

[TestClass]
public class GzDeCompressTests
{
    [TestMethod]
    public void TarGzDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-tar-gz-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.tar.gz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void TgzDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-tgz-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.tgz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/gz-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/gz.gz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
