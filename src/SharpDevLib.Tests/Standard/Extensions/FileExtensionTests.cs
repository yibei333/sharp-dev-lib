using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System.IO;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class FileExtensionTests
{
    [TestMethod]
    [DataRow("foo", true, "")]
    [DataRow("foo.txt", true, ".txt")]
    [DataRow("foo.txt", false, "txt")]
    [DataRow("foo.bar.txt", true, ".txt")]
    [DataRow("foo.bar.txt", false, "txt")]
    [DataRow("foo/bar.txt", false, "txt")]
    public void GetFileExtensionTest(string path, bool includePoint, string expected)
    {
        var actual = path.GetFileExtension(includePoint);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetFileExtensionTest(string path)
    {
        path.GetFileExtension();
    }

    [TestMethod]
    [DataRow("foo", true, "foo")]
    [DataRow("foo.txt", true, "foo.txt")]
    [DataRow("foo.txt", false, "foo")]
    [DataRow("foo.bar.txt", true, "foo.bar.txt")]
    [DataRow("foo.bar.txt", false, "foo.bar")]
    [DataRow("foo/bar.txt", true, "bar.txt")]
    [DataRow("foo/bar.txt", false, "bar")]
    public void GetFileNameTest(string path, bool includeExtension, string expected)
    {
        var actual = path.GetFileName(includeExtension);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetFileNameTest(string path)
    {
        path.GetFileName();
    }

    [TestMethod]
    public void SaveBytesToFileTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("foo.txt");
        bytes.SaveToFile(path);
        var actual = File.ReadAllText(path);
        var expected = "foo.bar";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveBytesToFilePathExceptionTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        var path = string.Empty;
        bytes.SaveToFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveBytesToFileExistedExceptionTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        bytes.SaveToFile(path, true);
    }

    [TestMethod]
    public void SaveStreamToFileTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        using var stream = new MemoryStream(bytes);
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("bar.txt");
        stream.SaveToFile(path);
        var actual = File.ReadAllText(path);
        var expected = "foo.bar";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveStreamToFilePathExceptionTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        using var stream = new MemoryStream(bytes);
        var path = string.Empty;
        stream.SaveToFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveStreamToFileExistedExceptionTest()
    {
        var bytes = new byte[] { 102, 111, 111, 46, 98, 97, 114 };
        using var stream = new MemoryStream(bytes);
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        stream.SaveToFile(path, true);
    }

    [TestMethod]
    public void EnsureDirectoryExistTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data");
        path.EnsureDirectoryExist();
        Assert.IsTrue(Directory.Exists(path));

        path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Foo Bar");
        if (Directory.Exists(path)) Directory.Delete(path, true);
        path.EnsureDirectoryExist();
        Assert.IsTrue(Directory.Exists(path));
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EnsureDirectoryExistExceptionTest(string path)
    {
        path.EnsureDirectoryExist();
    }

    [TestMethod]
    public void EnsureFileExistTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        path.EnsureFileExist();
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EnsureFileExistPathExceptionTest(string path)
    {
        path.EnsureFileExist();
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void EnsureFileExistNotFoundExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/xxxx.txt");
        if (File.Exists(path)) File.Delete(path);
        path.EnsureFileExist();
    }

    [TestMethod]
    [DataRow(5, "5Byte")]
    [DataRow(2045, "2KB")]
    [DataRow(4620, "4.51KB")]
    [DataRow(4730880, "4.51MB")]
    [DataRow(4844421120, "4.51GB")]
    [DataRow(4960687226880, "4.51TB")]
    [DataRow(5079743720325120, "4620TB")]
    public void ToFileSizeStringTest(long size, string expected)
    {
        var actual = size.ToFileSizeString();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ToFileSizeStringExceptionTest()
    {
        (-1L).ToFileSizeString();
    }

    [TestMethod]
    [DataRow("foobar", "")]
    [DataRow("foo.txt", "text/plain")]
    [DataRow("foo/bar.txt", "text/plain")]
    public void GetMimeTypeTest(string filePathOrName, string expected)
    {
        var actual = filePathOrName.GetMimeType();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetMimeTypeTest(string filePathOrName)
    {
        filePathOrName.GetMimeType();
    }

    [TestMethod]
    [DataRow("foo", "bar", "foo/bar")]
    [DataRow("foo/bar", "baz", "foo/bar/baz")]
    [DataRow("foo\\bar", "baz", "foo/bar/baz")]
    [DataRow("foo\\bar ", " baz", "foo/bar/baz")]
    public void CombinePathTest(string leftPath, string rightPath, string expected)
    {
        var actual = leftPath.CombinePath(rightPath);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(" ", "")]
    [DataRow(" foo", "foo")]
    [DataRow(" foo\\bar", "foo/bar")]
    [DataRow(" foo\\bar ", "foo/bar")]
    public void FormatPathTest(string path, string expected)
    {
        var actual = path.FormatPath();
        Assert.AreEqual(expected, actual);
    }
}
