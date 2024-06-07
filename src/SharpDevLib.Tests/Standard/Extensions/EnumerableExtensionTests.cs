using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class EnumerableExtensionTests
{
    [TestMethod]
    public void DistinctByObjectValueTest()
    {
        List<User?> users =
        [
            null,
            new ("foo",10),
            new ("foo",10),
            new ("foo",20),
            new ("bar",20),
            null
        ];
        var distinctedUsers = users.DistinctByObjectValue().ToList();
        Console.WriteLine(distinctedUsers.Serialize());
        Assert.AreEqual(4, distinctedUsers?.Count);
    }


    [TestMethod]
    public void EnumerableOrderByDynamicTest()
    {
        var list = new List<User>
        {
            new("foo",10),
            new("bar",20),
        };
        Assert.AreEqual("name=bar,age=20,name=foo,age=10", string.Join(",", list.OrderByDynamic("Name").Select(x => x.ToString())));
        Assert.AreEqual("name=foo,age=10,name=bar,age=20", string.Join(",", list.OrderByDynamic("Name", true).Select(x => x.ToString())));
        Assert.AreEqual("name=foo,age=10,name=bar,age=20", string.Join(",", list.OrderByDynamic("Age").Select(x => x.ToString())));
        Assert.AreEqual("name=bar,age=20,name=foo,age=10", string.Join(",", list.OrderByDynamic("Age", true).Select(x => x.ToString())));
    }

    [TestMethod]
    public void QuerybleOrderByDynamicTest()
    {
        IQueryable<User> list = new List<User>
        {
            new("foo",10),
            new("bar",20),
        }.AsQueryable();
        Assert.AreEqual("name=bar,age=20,name=foo,age=10", string.Join(",", list.OrderByDynamic("Name").Select(x => x.ToString())));
        Assert.AreEqual("name=foo,age=10,name=bar,age=20", string.Join(",", list.OrderByDynamic("Name", true).Select(x => x.ToString())));
        Assert.AreEqual("name=foo,age=10,name=bar,age=20", string.Join(",", list.OrderByDynamic("Age").Select(x => x.ToString())));
        Assert.AreEqual("name=bar,age=20,name=foo,age=10", string.Join(",", list.OrderByDynamic("Age", true).Select(x => x.ToString())));
    }
}