using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Json;

[TestClass]
public class JsonTests
{
    static readonly User _user = new("foo", 10);
    static readonly string _userString = _user.ToString();
    const string _json = "{\"Age\":10,\"Name\":\"foo\"}";
    const string _formatedJson = @"{
  ""Age"": 10,
  ""Name"": ""foo""
}";

    [TestMethod]
    public void SerializeTest()
    {
        var json = _user.Serialize();
        Assert.AreEqual(_json, json);

        var formatedJson = _user.Serialize(JsonOption.DefaultWithFormat);
        Assert.AreEqual(_formatedJson, formatedJson);
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
}