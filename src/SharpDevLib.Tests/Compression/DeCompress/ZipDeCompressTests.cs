using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Compression.DeCompress;

[TestClass]
public class ZipDeCompressTests
{
    [TestMethod]
    public async Task DeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-decompress");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/zip.zip"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public async Task DeCompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-decompress-password");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/zip-password.zip"), targetPath)
        {
            Password = "foobar",
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public async Task SyncDeCompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-decompress-sync");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/zip.zip"), targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }

    [TestMethod]
    public async Task SyncDeCompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/zip-decompress-password-sync");
        var option = new DeCompressRequest(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/zip-password.zip"), targetPath)
        {
            Password = "foobar",
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.DeCompressAsync();
        Assert.IsTrue(File.Exists(targetPath.CombinePath("foo.txt")));
        Assert.AreEqual("foo text", File.ReadAllText(targetPath.CombinePath("foo.txt")));
    }
}
