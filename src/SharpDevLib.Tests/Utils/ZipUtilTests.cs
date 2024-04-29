using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests;

[TestClass]
public class ZipUtilTests
{
    [TestMethod]
    [DataRow("Data/Directory", "Data/Directory1/null_password.zip", null)]
    [DataRow("Data/Directory", "Data/Directory1/empty_password.zip", "")]
    [DataRow("Data/Directory", "Data/Directory1/empty_password.7z", "")]
    [DataRow("Data/Directory", "Data/Directory1/password1.zip", "123456")]
    [DataRow("Data/Directory", "Data/Directory1/password2.zip", "abcdefg")]
    [DataRow("Data/Directory", "Data/Directory1/password3.7z", "123456")]
    public void CompressDirectoryTest(string directory, string target, string password)
    {
        directory.Compress(target, password, (int progress) => Console.WriteLine(progress));
        Assert.IsTrue(File.Exists(target));
    }

    [TestMethod]
    public void CompressFilesTest()
    {
        var target = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Directory1/files.zip");
        var files = new List<string>
        {
            AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Directory/Root/a.txt"),
            AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Directory/TestFile.txt")
        };
        files.Compress(target, "123456", (int progress) => Console.WriteLine(progress));
        Assert.IsTrue(File.Exists(target));
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CompressExceptionTest(string directory)
    {
        directory.Compress("");
    }

    [TestMethod]
    [DataRow("Data/null_password.zip", "Data/null_password_zip", "")]
    [DataRow("Data/password.zip", "Data/password_zip", "123456")]
    [DataRow("Data/null_password.7z", "Data/null_password_7z", "")]
    [DataRow("Data/password.7z", "Data/password_7z", "123456")]
    public void ExtractTest(string filePath, string directory, string password)
    {
        AppDomain.CurrentDomain.BaseDirectory.CombinePath(filePath).Extract(AppDomain.CurrentDomain.BaseDirectory.CombinePath(directory), password, (int progress) => Console.WriteLine(progress));
    }
}
