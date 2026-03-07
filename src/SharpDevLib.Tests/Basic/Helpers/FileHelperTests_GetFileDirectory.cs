using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class FileHelperTests_GetFileDirectory
{
    [TestMethod]
    [DataRow("", "")]
    [DataRow("  ", "")]
    [DataRow(null, "")]
    [DataRow("foo.txt", "")]
    [DataRow("/path/to/foo.txt", "/path/to")]
    [DataRow("C:\\Users\\Test\\foo.txt", "C:\\Users\\Test")]
    [DataRow("/path/to/nested/foo.bar.txt", "/path/to/nested")]
    [DataRow("foo/bar/baz.txt", "foo/bar")]
    [DataRow("foo\\bar\\baz.txt", "foo\\bar")]
    [DataRow("test.txt", "")]
    [DataRow("/test.txt", "")]
    public void GetFileDirectoryTest(string filePath, string expected)
    {
        var actual = filePath.GetFileDirectory();
        Assert.AreEqual(expected.FormatPath(), actual);
    }
}
