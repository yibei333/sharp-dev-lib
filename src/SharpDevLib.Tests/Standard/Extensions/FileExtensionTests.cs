using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
        var bytes = "foo.bar".ToUtf8Bytes();
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
        var bytes = "foo.bar".ToUtf8Bytes();
        var path = string.Empty;
        bytes.SaveToFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveBytesToFileExistedExceptionTest()
    {
        var bytes = "foo.bar".ToUtf8Bytes();
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        bytes.SaveToFile(path, true);
    }

    [TestMethod]
    public void SaveStreamToFileTest()
    {
        var bytes = "foo.bar".ToUtf8Bytes();
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
        var bytes = "foo.bar".ToUtf8Bytes();
        using var stream = new MemoryStream(bytes);
        var path = string.Empty;
        stream.SaveToFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveStreamToFileExistedExceptionTest()
    {
        var bytes = "foo.bar".ToUtf8Bytes();
        using var stream = new MemoryStream(bytes);
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
        stream.SaveToFile(path, true);
    }

    [TestMethod]
    public void EnsureDirectoryExistTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data");
        path.EnsureDirectoryExist();
        Assert.IsTrue(Directory.Exists(path));

        path = path.CombinePath("Foo Bar");
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
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
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
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/xxxx.txt");
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
    [DataRow(5079743720325120, "4.51PB")]
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

    [TestMethod]
    public void FileBase64EncodeTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/c-sharp.png");
        var actual = path.FileBase64Encode();
        var expected = "data:image/png;base64,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";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FileBase64DecodeTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/c-sharp.png");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/c-sharp-decode.png");
        targetPath.RemoveFileIfExist();
        var base64 = "data:image/png;base64,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";

        var bytes = base64.FileBase64Decode();
        var actual = bytes.MD5Hash();
        var expected = File.ReadAllBytes(sourcePath).MD5Hash();
        Assert.AreEqual(expected, actual);

        bytes.SaveToFile(targetPath);
        Assert.IsTrue(File.Exists(targetPath));
    }
}
