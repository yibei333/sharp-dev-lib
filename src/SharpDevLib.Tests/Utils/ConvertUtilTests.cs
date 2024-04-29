using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace SharpDevLib.Tests;

[TestClass]
public class ConvertUtilTests
{
    [TestMethod]
    [DataRow("0e416341-4f6d-4a68-888b-23042316b2e9")]
    [DataRow("b2f7b1fb-3a72-4741-93e6-a89f58e2d96f")]
    public void ToGuidTest(string guidString)
    {
        var expected = guidString.ToGuid();
        Assert.AreEqual(guidString, expected.ToString());
    }

    [TestMethod]
    [DataRow("xx")]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("0e416341-4f6d-4a68-888b-23042316b2eg")]
    public void ToGuidEmptyTest(string guidString)
    {
        var expected = guidString.ToGuid();
        Assert.AreEqual(Guid.Empty, expected);
    }

    [TestMethod]
    [DataRow("xx")]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("0e416341-4f6d-4a68-888b-23042316b2eg")]
    [ExpectedException(typeof(InvalidCastException))]
    public void ToGuidExceptionTest(string guidString)
    {
        _ = guidString.ToGuid(true);
    }

    [TestMethod]
    [DataRow(',', "", "")]
    [DataRow(',', null, "")]
    [DataRow(',', "AA", "")]
    [DataRow(',', "AA,", "")]
    [DataRow(',', "AA,BB", "")]
    [DataRow(',', "0e416341-4f6d-4a68-888b-23042316b211,BB", "0e416341-4f6d-4a68-888b-23042316b211")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211,b2f7b1fb-3a72-4741-93e6-a89f58e2d96f", "0e416341-4f6d-4a68-888b-23042316b211,b2f7b1fb-3a72-4741-93e6-a89f58e2d96f")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211", "0e416341-4f6d-4a68-888b-23042316b211")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211,0e416341-4f6d-4a68-888b-23042316b211", "0e416341-4f6d-4a68-888b-23042316b211")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211,,0e416341-4f6d-4a68-888b-23042316b211", "0e416341-4f6d-4a68-888b-23042316b211")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211,BB,0e416341-4f6d-4a68-888b-23042316b211", "0e416341-4f6d-4a68-888b-23042316b211")]
    [DataRow(',', "AA,0e416341-4f6d-4a68-888b-23042316b211,BB,0e416341-4f6d-4a68-888b-23042316b211,CC,b2f7b1fb-3a72-4741-93e6-a89f58e2d96f", "0e416341-4f6d-4a68-888b-23042316b211,b2f7b1fb-3a72-4741-93e6-a89f58e2d96f")]
    [DataRow('.', "AA.0e416341-4f6d-4a68-888b-23042316b211.BB.0e416341-4f6d-4a68-888b-23042316b211.CC.b2f7b1fb-3a72-4741-93e6-a89f58e2d96f", "0e416341-4f6d-4a68-888b-23042316b211,b2f7b1fb-3a72-4741-93e6-a89f58e2d96f")]
    public void ToGuidListTest(char charater, string guidListString, string expected)
    {
        var actual = guidListString.ToGuidList(charater);
        Assert.AreEqual(expected, string.Join(",", actual));
    }

    [TestMethod]
    [DataRow('-', "0e416341-4f6d-4a68-888b-23042316b211,BB")]
    [ExpectedException(typeof(ArgumentException))]
    public void ToGuidListExceptionTest(char charater, string guidListString)
    {
        _ = guidListString.ToGuidList(charater);
    }

    [TestMethod]
    [DataRow("", false)]
    [DataRow(null, false)]
    [DataRow("abc", false)]
    [DataRow("True", true)]
    [DataRow("true", true)]
    [DataRow("true1", false)]
    [DataRow("False", false)]
    [DataRow("false", false)]
    [DataRow("false1", false)]
    public void ToBooleanTest(string str, bool expected)
    {
        var actual = str.ToBoolean();
        Assert.AreEqual(expected, actual);
    }
}
