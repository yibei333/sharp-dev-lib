using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Compression;
using SharpDevLib.Tests.Standard.Json;
using System;
using System.IO;

namespace SharpDevLib.Tests.Compression.DeCompress;

[TestClass]
public class RarDeCompressTests
{
    [TestMethod]
    public void DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/rar-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/rar.rar"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonTests.FormatJsonOption)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public void DeCompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/rar-decompress-password");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/rar-password.rar"), targetPath)
        {
            Password = "foobar",
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonTests.FormatJsonOption)),
        };
        option.DeCompressAsync().GetAwaiter().GetResult();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
