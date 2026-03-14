using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class JsonHelperTests
{
    internal static readonly JsonOption FormatJsonOption = new() { FormatJson = true };
    static readonly User _user = new("foo", 10);
    static readonly string _userString = _user.ToString();
    const string _json = "{\"Name\":\"foo\",\"Age\":10}";
    const string _formatedJson = @"{
  ""Name"": ""foo"",
  ""Age"": 10
}";

    [TestMethod]
    public void SerializeTest()
    {
        var json = _user.Serialize();
        Assert.AreEqual(_json, json);

        var formatedJson = _user.Serialize(FormatJsonOption);
        Assert.AreEqual(_formatedJson, formatedJson);

        Console.WriteLine(new { Id="foo" }.Serialize());
    }

    [TestMethod]
    public void DeSerializeTest()
    {
        Assert.AreEqual(_userString, _json.DeSerialize<User>()?.ToString());
        Assert.AreEqual(_userString, _formatedJson.DeSerialize<User>()?.ToString());
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    public void DeSerializeExceptionTest(string json)
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => json.DeSerialize<User>());
    }

    [TestMethod]
    public void FormatJsonTest()
    {
        Assert.AreEqual(_formatedJson, _json.FormatJson());
    }

    [TestMethod]
    public void CompressJsonTest()
    {
        Assert.AreEqual(_json, _formatedJson.CompressJson());
    }

    [TestMethod]
    public void DictionarySerializeTest()
    {
        var dictionary = new Dictionary<string, string>
        {
            { "foo","foo value" },
            { "bar","bar value" }
        };
        var actual = dictionary.Serialize();
        var expected = "{\"foo\":\"foo value\",\"bar\":\"bar value\"}";
        Assert.AreEqual(expected, actual);
    }

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