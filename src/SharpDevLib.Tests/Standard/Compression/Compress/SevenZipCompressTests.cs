using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Compression.Compress;

[TestClass]
public class SevenZipCompressTests
{
    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void CompressTest()
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/sevenzip-create.rar");
        var option = new CompressOption(new List<string> { AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Compression/Root") }, targetPath);
        option.CompressAsync().GetAwaiter().GetResult();
    }
}
