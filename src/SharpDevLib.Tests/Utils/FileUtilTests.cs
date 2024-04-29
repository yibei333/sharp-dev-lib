using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace SharpDevLib.Tests;

[TestClass]
public class FileUtilTests
{
    [TestMethod]
    [DataRow(null, "")]
    [DataRow("", "")]
    [DataRow("abc", "")]
    [DataRow("abc.txt", ".txt")]
    [DataRow("abc.png.txt", ".txt")]
    public void GetFileExtensionTest(string fileName, string expected)
    {
        var actual = fileName.GetFileExtension();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SaveFileTest()
    {
        var bytes = Encoding.UTF8.GetBytes("save file test");
        var path = "test.txt";
        bytes.SaveFile(path);
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual("save file test", File.ReadAllText(path));
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveFileArgumentNullExceptionTest(string path)
    {
        var bytes = Array.Empty<byte>();
        bytes.SaveFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveFileInvalidOperationExceptionTest1()
    {
        var bytes = Array.Empty<byte>();
        bytes.SaveFile("aaa");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveFileInvalidOperationExceptionTest2()
    {
        byte[]? bytes = null;
        bytes.SaveFile("aaa");
    }

    [TestMethod]
    public void EnsureDirectoryExistTest()
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data");
        directory.EnsureDirectoryExist();
        Assert.IsTrue(Directory.Exists(directory));
        directory = directory.CombinePath("TestDirectory");
        directory.EnsureDirectoryExist();
        Assert.IsTrue(Directory.Exists(directory));
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EnsureDirectoryExistExceptionTest(string directory)
    {
        directory.EnsureDirectoryExist();
    }

    [TestMethod]
    public void EnsureFileExistTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt");
        path.EnsureFileExist();
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EnsureFileExistArgumentNullExceptionTest(string path)
    {
        path.EnsureFileExist();
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void EnsureFileExistFileNotFoundExceptionTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.text");
        path.EnsureFileExist();
    }
}
