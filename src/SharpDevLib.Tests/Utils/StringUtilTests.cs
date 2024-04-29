using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests;

[TestClass]
public class StringUtilTests
{
    [TestMethod]
    public void TrimStartTest()
    {
        var source = "startstartsAbcstart";
        var target = "start";
        var result=source.TrimStart(target);
        var expected = "startsAbcstart";
        Assert.AreEqual(expected, result );
    }

    [TestMethod]
    public void TrimEndTest()
    {
        var source = "endAbceendend";
        var target = "end";
        var result = source.TrimEnd(target);
        var expected = "endAbceend";
        Assert.AreEqual(expected, result);
    }
}
