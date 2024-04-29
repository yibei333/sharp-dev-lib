using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests;

[TestClass]
public class NullCheckUtilTests
{
    [TestMethod]
    public void ObjectIsNullTest()
    {
        object? obj = null;
        Assert.IsTrue(obj.IsNull());

        obj = Department.Create();
        Assert.IsFalse(obj.IsNull());
    }

    [TestMethod]
    public void ObjectNotNullTest()
    {
        object? obj = null;
        Assert.IsFalse(obj.NotNull());

        obj = Department.Create();
        Assert.IsTrue(obj.NotNull());
    }

    [TestMethod]
    public void IsDbNullTest()
    {
        object? obj = null;
        Assert.IsFalse(obj.IsDbNull());

        obj = DBNull.Value;
        Assert.IsTrue(obj.IsDbNull());

        obj = Department.Create();
        Assert.IsFalse(obj.IsDbNull());
    }

    [TestMethod]
    public void NotDbNullTest()
    {
        object? obj = null;
        Assert.IsTrue(obj.NotDbNull());

        obj = DBNull.Value;
        Assert.IsFalse(obj.NotDbNull());

        obj = Department.Create();
        Assert.IsTrue(obj.NotDbNull());
    }

    [TestMethod]
    [DataRow("",true)]
    [DataRow(null,true)]
    [DataRow("abc", false)]
    public void StringIsNullTest(string str,bool expected)
    {
        var actual = str.IsNull();
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    [DataRow("", false)]
    [DataRow(null, false)]
    [DataRow("abc", true)]
    public void StringNotNullTest(string? str, bool expected)
    {
        Assert.AreEqual(expected, str.NotNull());
    }

    [TestMethod]
    public void NullableStructTypeIsNullTest()
    {
        int? a = null;
        Assert.IsTrue(a.IsNull());

        a = 1;
        Assert.IsFalse(a.IsNull());
    }

    [TestMethod]
    public void NullableStructTypeNotNullTest()
    {
        int? a = null;
        Assert.IsFalse(a.NotNull());

        a = 1;
        Assert.IsTrue(a.NotNull());
    }

    [TestMethod]
    public void GuidIsEmptyTest()
    {
        var id = Guid.Empty;
        Assert.IsTrue(id.IsEmpty());

        id = Guid.NewGuid();
        Assert.IsFalse(id.IsEmpty());
    }

    [TestMethod]
    public void GuidNotEmptyTest()
    {
        var id = Guid.Empty;
        Assert.IsFalse(id.NotEmpty());

        id = Guid.NewGuid();
        Assert.IsTrue(id.NotEmpty());
    }

    [TestMethod]
    public void NullableGuidIsEmptyTest()
    {
        Guid? id = null;
        Assert.IsTrue(id.IsEmpty());

        id = Guid.Empty;
        Assert.IsTrue(id.IsEmpty());

        id = Guid.NewGuid();
        Assert.IsFalse(id.IsEmpty());
    }

    [TestMethod]
    public void NullableGuidNotEmptyTest()
    {
        Guid? id = null;
        Assert.IsFalse(id.NotEmpty());

        id = Guid.Empty;
        Assert.IsFalse(id.NotEmpty());

        id = Guid.NewGuid();
        Assert.IsTrue(id.NotEmpty());
    }

    [TestMethod]
    public void EnumerableIsEmptyTest()
    {
        List<object>? list = null;
        Assert.IsTrue(list.IsEmpty());

        list = new List<object>();
        Assert.IsTrue(list.IsEmpty());

        list.Add(Department.Create());
        Assert.IsFalse(list.IsEmpty());
    }

    [TestMethod]
    public void EnumerableNotEmptyTest()
    {
        List<object>? list = null;
        Assert.IsFalse(list.NotEmpty());

        list = new List<object>();
        Assert.IsFalse(list.NotEmpty());

        list.Add(Department.Create());
        Assert.IsTrue(list.NotEmpty());
    }

    [TestMethod]
    public void DictionaryIsEmptyTest()
    {
        Dictionary<string, object>? dic = null;
        Assert.IsTrue(dic.IsEmpty());

        dic = new Dictionary<string, object>();
        Assert.IsTrue(dic.IsEmpty());

        dic.Add("department", Department.Create());
        Assert.IsFalse(dic.IsEmpty());
    }

    [TestMethod]
    public void DictionaryNotEmptyTest()
    {
        Dictionary<string, object>? dic = null;
        Assert.IsFalse(dic.NotEmpty());

        dic = new Dictionary<string, object>();
        Assert.IsFalse(dic.NotEmpty());

        dic.Add("department", Department.Create());
        Assert.IsTrue(dic.NotEmpty());
    }
}
