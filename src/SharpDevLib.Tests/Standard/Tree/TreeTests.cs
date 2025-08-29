using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Standard.Tree;

[TestClass]
public class TreeTests
{
    [TestMethod]
    public void StringIdBuildTreeTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo"),
            new ("11","foo-1","1"),
            new ("111","foo-1-1","11"),
            new ("112","foo-1-2","11"),
            new ("12","foo-2","1"),
            new ("121","foo-2-1","12"),
            new ("13","foo-3","1"),
            new ("2","bar"),
            new ("3","baz","0"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var jsonOption = new JsonOption { FormatJson = true, OrderByNameProperty = false };
        var tree = departments.BuildTree(option);
        var json = tree.Serialize(jsonOption);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(jsonOption);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, metaList);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, flatList);
    }

    [TestMethod]
    public void GuidBuildTreeTest()
    {
        var level1Id = Guid.NewGuid();
        var level2Id = Guid.NewGuid();
        var level21Id = Guid.NewGuid();
        var departments = new List<Department<Guid>>
        {
            new (level1Id,"foo"),
            new (level2Id,"foo-1",level1Id),
            new (Guid.NewGuid(),"foo-1-1",level2Id),
            new (Guid.NewGuid(),"foo-1-2",level2Id),
            new (level21Id,"foo-2",level1Id),
            new (Guid.NewGuid(),"foo-2-1",level21Id),
            new (Guid.NewGuid(),"foo-3",level1Id),
            new (Guid.NewGuid(),"bar"),
            new (Guid.NewGuid(),"baz",Guid.NewGuid()),
        };
        var option = new TreeBuildOption<Department<Guid>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var jsonOption = new JsonOption { FormatJson = true, OrderByNameProperty = false };
        var tree = departments.BuildTree(option);
        var json = tree.Serialize(jsonOption);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(jsonOption);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, metaList);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, flatList);
    }

    [TestMethod]
    public void IntBuildTreeTest()
    {

        var departments = new List<Department<int>>
        {
            new (1,"foo"),
            new (11,"foo-1",1),
            new (111,"foo-1-1",11),
            new (112,"foo-1-2",11),
            new (12,"foo-2",1),
            new (121,"foo-2-1",12),
            new (13,"foo-3",1),
            new (2,"bar"),
            new (3,"baz",0),
        };
        var option = new TreeBuildOption<Department<int>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var tree = departments.BuildTree(option);
        var jsonOption = new JsonOption { FormatJson = true, OrderByNameProperty = false };
        var json = tree.Serialize(jsonOption);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(jsonOption);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, metaList);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(JsonOption.DefaultWithFormat));
        Assert.HasCount(9, flatList);
    }

    [TestMethod]
    public void NullReferenceExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("","foo"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        Assert.ThrowsExactly<NullReferenceException>(() => departments.BuildTree(option));
    }

    [TestMethod]
    public void CycleExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
            new ("2","foo","1"),
            new ("3","foo","2"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        Assert.ThrowsExactly<InvalidOperationException>(() => departments.BuildTree(option));
    }

    [TestMethod]
    public void SelfCycleExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","1"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        Assert.ThrowsExactly<InvalidOperationException>(() => departments.BuildTree(option));
    }

    [TestMethod]
    [DataRow("XX", "PId", "Identity")]
    [DataRow("Identity", "XX", "Identity")]
    public void PropertyNotFoundExceptionTest1(string idPropertyName, string parentIdPropertyName, string sortPropertyName)
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = idPropertyName,
            ParentIdPropertyName = parentIdPropertyName,
            SortPropertyName = sortPropertyName,
            Descending = true
        };
        Assert.ThrowsExactly<ArgumentException>(() => departments.BuildTree(option));
    }

    [TestMethod]
    [DataRow("Identity", "PId", "XX")]
    public void PropertyNotFoundExceptionTest2(string idPropertyName, string parentIdPropertyName, string sortPropertyName)
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
        };
        Assert.ThrowsExactly<ArgumentException>(() => new TreeBuildOption<Department<string>>
        {
            IdPropertyName = idPropertyName,
            ParentIdPropertyName = parentIdPropertyName,
            SortPropertyName = sortPropertyName,
            Descending = true
        });
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow("Identity", "", "")]
    [DataRow("", "PId", "")]
    public void EmptyPropertyExceptionTest(string idPropertyName, string parentIdPropertyName, string sortPropertyName)
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = idPropertyName,
            ParentIdPropertyName = parentIdPropertyName,
            SortPropertyName = sortPropertyName,
            Descending = true
        };
        Assert.ThrowsExactly<NullReferenceException>(() => departments.BuildTree(option));
    }

    [TestMethod]
    public void RepeatExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
            new ("1","bar","0"),
        };
        var option = new TreeBuildOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        Assert.ThrowsExactly<InvalidDataException>(() => departments.BuildTree(option));
    }
}