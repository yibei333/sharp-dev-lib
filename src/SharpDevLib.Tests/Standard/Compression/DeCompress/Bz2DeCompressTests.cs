using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.DeCompress;

[TestClass]
public class Bz2DeCompressTests
{
    [TestMethod]
    public void TarDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/bz2-tar-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/bz2.tar.bz2"), targetPath)
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
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/bz2-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/bz2.bz2"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonOption.DefaultWithFormat))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("bz2")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("bz2")));
    }
}
