using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.DeCompress;

[TestClass]
public class SevenZipDeCompressTests
{
    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/sevenzip-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/sevenzip.7z"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(true))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void DeCompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/sevenzip-decompress-password");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/sevenzip-password.7z"), targetPath)
        {
            Password = "foobar",
            OnProgress = (p) => Console.WriteLine(p.Serialize(true)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
