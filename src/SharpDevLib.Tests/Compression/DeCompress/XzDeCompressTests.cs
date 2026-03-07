using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Json;
using System;
using System.IO;

namespace SharpDevLib.Tests.Compression.DeCompress;

[TestClass]
public class XzDeCompressTests
{
    [TestMethod]
    public void TarDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/xz-tar-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/xz.tar.xz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonTests.FormatJsonOption)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/xz-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/xz.xz"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonTests.FormatJsonOption)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("xz")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("xz")));
    }
}
