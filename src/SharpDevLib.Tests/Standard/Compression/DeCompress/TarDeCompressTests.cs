using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Compression.DeCompress;

[TestClass]
public class TarDeCompressTests
{
    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/tar-decompress");
        var option = new DeCompressOption(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/tar.tar"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(true))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
