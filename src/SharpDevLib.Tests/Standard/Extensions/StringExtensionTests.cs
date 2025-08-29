using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class StringExtensionTests
{
    const string _emptyId = "00000000-0000-0000-0000-000000000000";
    const string _id = "b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5";

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow("foo", "", "foo")]
    [DataRow("", "bar", "")]
    [DataRow(" ", "bar", "")]
    [DataRow("foo", "bar", "foo")]
    [DataRow("foofoobar", "foo", "foobar")]
    [DataRow(" foofoobar", "foo", "foobar")]
    [DataRow("foofoobar ", "foo", "foobar")]
    [DataRow(" foofoobar ", "foo", "foobar")]
    public void TrimStartTest(string source, string target, string expected)
    {
        var actual = source.TrimStart(target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow("foo", "", "foo")]
    [DataRow("", "bar", "")]
    [DataRow(" ", "bar", "")]
    [DataRow("foo", "bar", "foo")]
    [DataRow("foofoobar", "foo", "foofoobar")]
    [DataRow("foofoobar", "bar", "foofoo")]
    [DataRow(" foofoobar", "bar", "foofoo")]
    [DataRow("foofoobar ", "bar", "foofoo")]
    [DataRow(" foofoobar ", "bar", "foofoo")]
    public void TrimEndTest(string source, string target, string expected)
    {
        var actual = source.TrimEnd(target);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, _emptyId)]
    [DataRow("", _emptyId)]
    [DataRow(" ", _emptyId)]
    [DataRow("a", _emptyId)]
    [DataRow(_id, _id)]
    public void ToGuidTest(string? str, string expected)
    {
        var actual = str.ToGuid();
        Assert.AreEqual(Guid.Parse(expected), actual);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("a")]
    public void ToGuidExceptionTest(string? str)
    {
        Assert.ThrowsExactly<InvalidCastException>(() => str.ToGuid(true));
    }

    [TestMethod]
    [DataRow(null, ',', true, true, 0)]
    [DataRow("", ',', true, true, 0)]
    [DataRow(" ", ',', true, true, 0)]
    [DataRow("1", ',', true, true, 1)]
    [DataRow("1,2", ',', true, true, 1)]
    [DataRow("1,2,", ',', true, true, 1)]
    [DataRow("1,2,", ',', false, true, 1)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, true, 2)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5", ',', false, true, 2)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, true, 2)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, false, 4)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', true, false, 3)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', true, true, 2)]
    [DataRow("1;b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5;b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5;", ';', true, true, 2)]
    public void SplitToGuidListTest(string? str, char separator, bool removeEmptyEntries, bool distinct, int expected)
    {
        var actual = str.SplitToGuidList(separator, removeEmptyEntries, false, distinct);
        Console.WriteLine(actual.Serialize());
        Assert.AreEqual(expected, actual.Count);
    }

    [TestMethod]
    [DataRow("1", ',', true, true)]
    [DataRow("1,2", ',', true, true)]
    [DataRow("1,2,", ',', true, true)]
    [DataRow("1,2,", ',', false, true)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, true)]
    public void SplitToGuidListExceptionTest(string? str, char separator, bool removeEmptyEntries, bool distinct)
    {
        Assert.ThrowsExactly<InvalidCastException>(() => str.SplitToGuidList(separator, removeEmptyEntries, true, distinct));
    }

    [TestMethod]
    [DataRow("1", '-', true, true)]
    public void SplitToGuidListSeperatorExceptionTest(string? str, char separator, bool removeEmptyEntries, bool distinct)
    {
        Assert.ThrowsExactly<ArgumentException>(() => str.SplitToGuidList(separator, removeEmptyEntries, true, distinct));
    }

    [TestMethod]
    [DataRow(null, ',', true, true, 0)]
    [DataRow("", ',', true, true, 0)]
    [DataRow(" ", ',', true, true, 0)]
    [DataRow("1", ',', true, true, 1)]
    [DataRow("1,2", ',', true, true, 2)]
    [DataRow("1,2,", ',', true, true, 2)]
    [DataRow("1,2,", ',', false, true, 3)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, true, 3)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5", ',', false, true, 2)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, true, 3)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', false, false, 4)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', true, false, 3)]
    [DataRow("1,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,", ',', true, true, 2)]
    [DataRow("1;b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5;b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5;", ';', true, true, 2)]
    public void SplitToListTest(string? str, char separator, bool removeEmptyEntries, bool distinct, int expected)
    {
        var actual = str.SplitToList(separator, removeEmptyEntries, distinct).Count;
        Console.WriteLine(actual.Serialize());
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("1", '-', true, true)]
    public void SplitToListSeperatorExceptionTest(string? str, char separator, bool removeEmptyEntries, bool distinct)
    {
        Assert.ThrowsExactly<ArgumentException>(() => str.SplitToList(separator, removeEmptyEntries, distinct));
    }

    [TestMethod]
    [DataRow("true", true)]
    [DataRow("True", true)]
    [DataRow("TRUE", true)]
    [DataRow("tRue", true)]
    [DataRow("false", false)]
    [DataRow("False", false)]
    [DataRow("FALSE", false)]
    [DataRow("fALSE", false)]
    [DataRow(null, false)]
    [DataRow("", false)]
    [DataRow(" ", false)]
    [DataRow("-1", false)]
    [DataRow("0", false)]
    [DataRow("1", false)]
    [DataRow("foo", false)]
    public void ToBooleanTest(string? str, bool expected)
    {
        var actual = str.ToBoolean();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("{\"foo\":\"bar\"}", "{\\\"foo\\\":\\\"bar\\\"}")]
    [DataRow("{\\\"foo\\\":\\\"bar\\\"}", "{\\\\\\\"foo\\\\\\\":\\\\\\\"bar\\\\\\\"}")]
    public void EscapeTest(string str, string expected)
    {
        var actual = str.Escape();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("{\\\"foo\\\":\\\"bar\\\"}", "{\"foo\":\"bar\"}")]
    [DataRow("{\\\\\\\"foo\\\\\\\":\\\\\\\"bar\\\\\\\"}", "{\\\"foo\\\":\\\"bar\\\"}")]
    public void RemoveEscapeTest(string str, string expected)
    {
        var actual = str.RemoveEscape();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("foo\r\r\n", "foo")]
    [DataRow("foo\r", "foo")]
    [DataRow("foo\n", "foo")]
    [DataRow("foo\n\r", "foo")]
    [DataRow("foo\r\n", "foo")]
    public void RemoveLineBreakTest(string str, string expected)
    {
        var actual = str.RemoveLineBreak();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("http:/foo/a/a1/a11/a.md", "http:/foo/a/a1/a11/a111/b.md", "./a111/b.md")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a11/a111/b.md", "./a111/b.md")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a11/b.md", "./b.md")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/b.md", "../b.md")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a12/b.md", "../a12/b.md")]
    [DataRow("d:/foo/a.md", "e:/foo/b.md", "../../e:/foo/b.md")]
    public void GetUrlRelativePathTest(string sourcePath, string targetPath, string expectedPath)
    {
        var actual = sourcePath.GetUrlRelativePath(targetPath);
        Assert.AreEqual(expectedPath, actual);
    }

    [TestMethod]
    [DataRow("http:/foo/a/a1/a11/a.md", "http:/foo/a/a1/a11/a111/b.md", "http:/foo/a/a1/a11")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a11/a111/b.md", "d:/foo/a/a1/a11")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a11/b.md", "d:/foo/a/a1/a11")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/b.md", "d:/foo/a/a1")]
    [DataRow("d:/foo/a/a1/a11/a.md", "d:/foo/a/a1/a12/b.md", "d:/foo/a/a1")]
    public void GetUrlCommonPrefixTest(string sourcePath, string targetPath, string expectedPath)
    {
        var actual = sourcePath.GetUrlCommonPrefix(targetPath);
        Assert.AreEqual(expectedPath, actual);
    }

    [TestMethod]
    [DataRow("abcd:/ef", "abcd:/efg", "abcd:/ef")]
    [DataRow("abcd:/efg", "abcd:/ef", "abcd:/ef")]
    [DataRow("abcd:/efg", "abcd:/efi", "abcd:/ef")]
    [DataRow("xabcd:/efg", "abcd:/efi", "")]
    public void GetCommonPrefixTest(string str1, string str2, string expectedPath)
    {
        var actual = str1.GetCommonPrefix(str2);
        Assert.AreEqual(expectedPath, actual);
    }
}
