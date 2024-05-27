using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class NullCheckExtensionTests
{
    #region string
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
    #endregion

    #region guid
    [TestMethod]
    [DataRow("00000000-0000-0000-0000-000000000000", true)]
    [DataRow("00000000-0000-0000-0000-000000000001", false)]
    public void GuidIsEmptyTest(string source, bool expected)
    {
        var id = Guid.TryParse(source, out var x) ? x : Guid.Empty;
        var actual = id.IsEmpty();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("00000000-0000-0000-0000-000000000000", false)]
    [DataRow("00000000-0000-0000-0000-000000000001", true)]
    public void GuidNotEmptyTest(string source, bool expected)
    {
        var id = Guid.TryParse(source, out var x) ? x : Guid.Empty;
        var actual = id.NotEmpty();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, true)]
    [DataRow("00000000-0000-0000-0000-000000000000", true)]
    [DataRow("00000000-0000-0000-0000-000000000001", false)]
    public void GuidIsNullOrEmptyTest(string? source, bool expected)
    {
        Guid? id = Guid.TryParse(source, out var x) ? x : null;
        var actual = id.IsNullOrEmpty();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(null, false)]
    [DataRow("00000000-0000-0000-0000-000000000000", false)]
    [DataRow("00000000-0000-0000-0000-000000000001", true)]
    public void GuidNotNullOrEmptyTest(string? source, bool expected)
    {
        Guid? id = Guid.TryParse(source, out var x) ? x : null;
        var actual = id.NotNullOrEmpty();
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region enumerable
    [TestMethod]
    public void EnumerableIsNullOrEmptyTest()
    {
        List<int>? source = null;
        Assert.AreEqual(true, source.IsNullOrEmpty());
        source = [];
        Assert.AreEqual(true, source.IsNullOrEmpty());
        source = [1];
        Assert.AreEqual(false, source.IsNullOrEmpty());
    }

    [TestMethod]
    public void EnumerableNotNullOrEmptyTest()
    {
        List<int>? source = null;
        Assert.AreEqual(false, source.NotNullOrEmpty());
        source = [];
        Assert.AreEqual(false, source.NotNullOrEmpty());
        source = [1];
        Assert.AreEqual(true, source.NotNullOrEmpty());
    }
    #endregion
}
