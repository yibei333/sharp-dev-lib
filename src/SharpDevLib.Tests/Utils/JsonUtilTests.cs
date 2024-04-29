using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests;

[TestClass]
public class JsonUtilTests
{
    [TestMethod]
    public void SerializeTest()
    {
        object? obj = null;
        Assert.AreEqual(string.Empty, obj.Serialize());

        var department = Department.Create();
        department.Id = Guid.Empty;
        obj = department;
        var expected = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"Name\":\"IT\",\"ParentId\":null,\"Order\":1,\"Children\":[]}";
        Assert.AreEqual(expected, obj.Serialize());
    }

    [TestMethod]
    public void DeSerializeTest()
    {
        var json = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"Name\":\"IT\",\"ParentId\":null,\"Order\":1,\"Children\":[]}";
        var department = json.DeSerialize<Department>();
        Assert.AreEqual(Guid.Empty,department.Id);
        Assert.AreEqual("IT",department.Name);
        Assert.AreEqual(null,department.ParentId);
        Assert.AreEqual(1,department.Order);
        Assert.AreEqual(0,department.Children.Count);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [ExpectedException(typeof(ArgumentException))]
    public void DeSerializeArgumentExceptionTest(string json)
    {
        _ = json.DeSerialize<object>();
    }

    [TestMethod]
    [DataRow("abc")]
    [DataRow("{\"abc\":}")]
    [ExpectedException(typeof(JsonSerializationException))]
    public void DeSerializeJsonExceptionTest(string json)
    {
        _ = json.DeSerialize<Department>();
    }

    [TestMethod]
    public void FormatJsonTest()
    {
        var json = string.Empty;
        Assert.AreEqual(string.Empty, json.FormatJson());
        json = null;
        Assert.AreEqual(string.Empty, json.FormatJson());

        json = "{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"Name\":\"IT\",\"ParentId\":null,\"Order\":1,\"Children\":[]}";
        var result = json.FormatJson();
        Assert.AreNotEqual(json, result);
        Assert.IsTrue(result.NotNull());
        Console.WriteLine(result);
    }
}
