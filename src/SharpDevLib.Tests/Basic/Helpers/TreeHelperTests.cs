using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class TreeHelperTests
{
    [TestMethod]
    public void HasNoCycleReferenceTest()
    {
        var users = new List<User>
        {
            new() { Id=1,Name="foo" },
            new() { Id=2,Name="bar",ParentId=1 },
            new() { Id=3,Name="baz",ParentId=2 },
        };
        var (result, ids) = users.HasCycleReference(x => x.Id, x => x.ParentId);
        Assert.IsFalse(result);
        Assert.IsNull(ids);
        Console.WriteLine(result);
        Console.WriteLine(ids);
    }

    [TestMethod]
    public void HasCycleReferenceTest1()
    {
        var users = new List<User>
        {
            new() { Id=1,Name="foo",ParentId=3 },
            new() { Id=2,Name="bar",ParentId=1 },
            new() { Id=3,Name="baz",ParentId=2 },
        };
        var (result, ids) = users.HasCycleReference(x => x.Id, x => x.ParentId);
        Assert.IsTrue(result);
        Assert.IsNotNull(ids);
        Console.WriteLine(result);
        Console.WriteLine(ids);
    }

    [TestMethod]
    public void HasCycleReferenceTest2()
    {
        var users = new List<User>
        {
            new() { Id=1,Name="foo",ParentId=1 },
            new() { Id=2,Name="bar",ParentId=1 },
            new() { Id=3,Name="baz",ParentId=2 },
        };
        var (result, ids) = users.HasCycleReference(x => x.Id, x => x.ParentId);
        Assert.IsTrue(result);
        Assert.IsNotNull(ids);
        Console.WriteLine(result);
        Console.WriteLine(ids);
    }

    [TestMethod]
    public void BuildTreeTest()
    {
        var users = new List<User>
        {
            new() { Id=1,Name="foo" },
            new() { Id=2,Name="bar",ParentId=1 },
            new() { Id=3,Name="baz",ParentId=2 },
            new() { Id=4,Name="qux" },
        };
        var items = users.BuildTree();
        Assert.HasCount(2, items);
        Assert.AreEqual(1, items.FirstOrDefault()?.Children?.Count ?? 0);
        Assert.AreEqual(1, items.FirstOrDefault()?.Children?.FirstOrDefault()?.Children?.Count ?? 0);
        Assert.AreEqual(0, items.LastOrDefault()?.Children?.Count ?? 0);
        Console.WriteLine(items.Serialize(new JsonOption { FormatJson = true }));
    }

    class User
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public List<User>? Children { get; set; }
    }
}
