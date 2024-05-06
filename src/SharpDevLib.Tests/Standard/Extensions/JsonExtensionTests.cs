﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class JsonExtensionTests
{
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
        Assert.AreEqual(string.Empty, ((User?)null).Serialize());

        var json = _user.Serialize();
        Assert.AreEqual(_json, json);

        var formatedJson = _user.Serialize(true);
        Assert.AreEqual(_formatedJson, formatedJson);
        Assert.AreEqual(_formatedJson, new User { Age = 10, Name = "foo" }.Serialize(true));
        Assert.AreEqual(_formatedJson, new User { Name = "foo", Age = 10 }.Serialize(true));
    }

    [TestMethod]
    public void DeSerializeTest()
    {
        Assert.AreEqual(null, ((string?)null).DeSerialize<User>());
        Assert.AreEqual(null, "".DeSerialize<User>());
        Assert.AreEqual(null, " ".DeSerialize<User>());

        Assert.AreEqual(_userString, _json.DeSerialize<User>()?.ToString());
        Assert.AreEqual(_userString, _formatedJson.DeSerialize<User>()?.ToString());
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
}
