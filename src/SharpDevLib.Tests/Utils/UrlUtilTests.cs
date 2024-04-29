using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests;

[TestClass]
public class UrlUtilTests
{
    [TestMethod]
    [DataRow("","","")]
    [DataRow("",null,"")]
    [DataRow(null,"","")]
    [DataRow(null,null,"")]
    [DataRow("", "abc", "abc")]
    [DataRow("abc", "", "abc")]
    [DataRow("abc", "def", "abc/def")]
    [DataRow("abc/", "def", "abc/def")]
    [DataRow("abc", "/def", "abc/def")]
    [DataRow("abc/", "/def", "abc/def")]
    [DataRow("abc\\", "/def", "abc/def")]
    [DataRow("abc\\", "\\def", "abc/def")]
    [DataRow("abc", "\\def", "abc/def")]
    public void CombinePathTest(string first,string second,string expected)
    {
        var actual = first.CombinePath(second);
        Assert.AreEqual(expected,actual);
    }
}
