using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class GuidNullCheckTests
{
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
}
