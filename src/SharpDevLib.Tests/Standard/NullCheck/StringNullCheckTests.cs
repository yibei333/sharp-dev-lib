using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Standard.NullCheck;

public class StringNullCheckTests
{
    [TestMethod]
    [DataRow(null, true)]
    [DataRow("", true)]
    [DataRow(" ", false)]
    [DataRow("foo", false)]
    public void StringIsNullOrEmptyTest(string? source, bool expected)
    {
        var actual = source.IsNullOrEmpty();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow(" ", true)]
    [DataRow("foo", true)]
    public void StringNotNullOrEmptyTest(string? source, bool expected)
    {
        var actual = source.NotNullOrEmpty();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, true)]
    [DataRow("", true)]
    [DataRow(" ", true)]
    [DataRow("foo", false)]
    public void StringIsNullOrWhiteSpaceTest(string? source, bool expected)
    {
        var actual = source.IsNullOrWhiteSpace();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow(" ", false)]
    [DataRow("foo", true)]
    public void StringNotNullOrWhiteSpaceTest(string? source, bool expected)
    {
        var actual = source.NotNullOrWhiteSpace();
        Assert.AreEqual(expected, actual);
    }
}
