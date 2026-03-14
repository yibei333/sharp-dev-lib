using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpConfigTests
{
    [TestMethod]
    public void CustomConfigTest()
    {
        var config = new HttpConfig
        {
            RetryCount = 5,
            Timeout = TimeSpan.FromSeconds(10),
        };
        Assert.AreEqual(5, config.RetryCount);
        Assert.AreEqual(TimeSpan.FromSeconds(10), config.Timeout);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(10)]
    public void VariousRetryCountTest(int retryCount)
    {
        var config = new HttpConfig { RetryCount = retryCount };
        Assert.AreEqual(retryCount, config.RetryCount);
    }

    [TestMethod]
    [DataRow(1000)]
    [DataRow(10000)]
    [DataRow(120000)]
    public void VariousTimeoutTest(int timeout)
    {
        var config = new HttpConfig { Timeout = TimeSpan.FromSeconds(timeout) };
        Assert.AreEqual(TimeSpan.FromSeconds(timeout), config.Timeout);
    }
}

[TestClass]
public class HttpFormFileTests
{
    [TestMethod]
    public void WithAllFieldsTest()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var file = new HttpFormFile("file", "test.txt", bytes);
        Assert.AreEqual("file", file.ParameterName);
        Assert.AreEqual("test.txt", file.FileName);
        Assert.AreEqual(bytes, file.Bytes);
    }

    [TestMethod]
    public void ConstructorWithNameAndFileNameTest()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var file = new HttpFormFile("file", "test.txt", bytes);
        Assert.AreEqual("file", file.ParameterName);
        Assert.AreEqual("test.txt", file.FileName);
        Assert.AreEqual(bytes, file.Bytes);
    }

    [TestMethod]
    public void ConstructorWithoutContentTypeTest()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var file = new HttpFormFile("file", "test.txt", bytes);

        Assert.AreEqual("file", file.ParameterName);
        Assert.AreEqual("test.txt", file.FileName);
        Assert.AreEqual(bytes, file.Bytes);
    }

    [TestMethod]
    public void EmptyBytesTest()
    {
        var file = new HttpFormFile("file", "test.txt", []);
        Assert.IsNotNull(file.Bytes);
        Assert.IsEmpty(file.Bytes);
    }
}

[TestClass]
public class HttpProgressTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var progress = new HttpProgress();
        Assert.AreEqual(0, progress.Transfered);
        Assert.AreEqual(0, progress.Total);
        Assert.AreEqual(0, progress.Progress);
    }
}
