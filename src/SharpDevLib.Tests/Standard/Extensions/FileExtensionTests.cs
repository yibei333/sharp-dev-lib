using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
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
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
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
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt");
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
