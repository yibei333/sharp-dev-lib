using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class TreeExtensionTests
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
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var tree = departments.BuildTree(option);
        var json = tree.Serialize(true, false);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(true, false);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(true));
        Assert.AreEqual(9, metaList.Count);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(true));
        Assert.AreEqual(9, flatList.Count);
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
        var option = new BuildTreeOption<Department<Guid>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var tree = departments.BuildTree(option);
        var json = tree.Serialize(true, false);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(true, false);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(true));
        Assert.AreEqual(9, metaList.Count);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(true));
        Assert.AreEqual(9, flatList.Count);
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
        var option = new BuildTreeOption<Department<int>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };

        var tree = departments.BuildTree(option);
        var json = tree.Serialize(true, false);

        var deserialized = json.DeSerializeTree(option);
        var deserializedJson = deserialized.Serialize(true, false);
        Console.WriteLine(deserializedJson);
        Assert.AreEqual(json, deserializedJson);

        var metaList = deserialized.ToMetaDataList();
        Console.WriteLine(metaList.Serialize(true));
        Assert.AreEqual(9, metaList.Count);

        var flatList = deserialized.ToFlatList();
        Console.WriteLine(flatList.Serialize(true));
        Assert.AreEqual(9, flatList.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void NullReferenceExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("","foo"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        departments.BuildTree(option);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CycleExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
            new ("2","foo","1"),
            new ("3","foo","2"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        departments.BuildTree(option);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SelfCycleExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","1"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        departments.BuildTree(option);
    }

    [TestMethod]
    [DataRow("XX", "PId", "Identity")]
    [DataRow("Identity", "XX", "Identity")]
    [DataRow("Identity", "PId", "XX")]
    [ExpectedException(typeof(ArgumentException))]
    public void PropertyNotFoundExceptionTest(string idPropertyName, string parentIdPropertyName, string sortPropertyName)
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = idPropertyName,
            ParentIdPropertyName = parentIdPropertyName,
            SortPropertyName = sortPropertyName,
            Descending = true
        };
        departments.BuildTree(option);
    }

    [TestMethod]
    [DataRow("", "", "")]
    [DataRow("Identity", "", "")]
    [DataRow("", "PId", "")]
    [ExpectedException(typeof(NullReferenceException))]
    public void EmptyPropertyExceptionTest(string idPropertyName, string parentIdPropertyName, string sortPropertyName)
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = idPropertyName,
            ParentIdPropertyName = parentIdPropertyName,
            SortPropertyName = sortPropertyName,
            Descending = true
        };
        departments.BuildTree(option);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void RepeatExceptionTest()
    {
        var departments = new List<Department<string>>
        {
            new ("1","foo","3"),
            new ("1","bar","0"),
        };
        var option = new BuildTreeOption<Department<string>>
        {
            IdPropertyName = "Identity",
            ParentIdPropertyName = "PId",
            SortPropertyName = "Identity",
            Descending = true
        };
        departments.BuildTree(option);
    }
}