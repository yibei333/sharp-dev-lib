using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Compression;
using SharpDevLib.Tests.Standard.Json;
using System;
using System.IO;

namespace SharpDevLib.Tests.Compression.DeCompress;

[TestClass]
public class TarDeCompressTests
{
    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/tar-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/tar.tar"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonTests.FormatJsonOption))
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
