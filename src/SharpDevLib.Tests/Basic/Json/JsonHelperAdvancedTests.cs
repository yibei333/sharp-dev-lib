using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Basic.Json;

[TestClass]
public class JsonNameFormatTests
{
    [TestMethod]
    public void EnumValuesTest()
    {
        var values = Enum.GetValues<JsonNameFormat>();
        Assert.HasCount(6, values);
    }

    [TestMethod]
    public void EnumParseTest()
    {
        var camelCaseLower = Enum.Parse<JsonNameFormat>("CamelCaseLower");
        var snakeCaseLower = Enum.Parse<JsonNameFormat>("SnakeCaseLower");
        var kebabCaseLower = Enum.Parse<JsonNameFormat>("KebabCaseLower");

        Assert.AreEqual(JsonNameFormat.CamelCaseLower, camelCaseLower);
        Assert.AreEqual(JsonNameFormat.SnakeCaseLower, snakeCaseLower);
        Assert.AreEqual(JsonNameFormat.KebabCaseLower, kebabCaseLower);
    }
}