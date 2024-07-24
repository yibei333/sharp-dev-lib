using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class EnumerableNullCheckTests
{
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
}
