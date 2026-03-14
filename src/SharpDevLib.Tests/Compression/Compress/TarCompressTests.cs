using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Compression.Compress;

[TestClass]
public class TarCompressTests
{
    [TestMethod]
    public async Task CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/tar-create.tar");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        await option.CompressAsync();
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsGreaterThan(0, new FileInfo(targetPath).Length);
    }

    [TestMethod]
    public void CompressWithPasswordTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/tar-password-create.tar");
        var option = new CompressRequest([AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Compression/Root")], targetPath)
        {
            Password = "foobar",
            Level = CompressionLevel.MinimumSize,
            IncludeSourceDiretory = true,
            OnProgress = (p) => Console.WriteLine(p.Serialize(JsonHelperTests.FormatJsonOption)),
        };
        Assert.ThrowsExactlyAsync<InvalidDataException>(option.CompressAsync);
    }
}
