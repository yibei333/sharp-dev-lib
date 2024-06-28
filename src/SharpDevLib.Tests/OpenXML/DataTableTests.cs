using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.OpenXML;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Data;

namespace SharpDevLib.Tests.OpenXML;

[TestClass]
public class DataTableTests
{
    class Foo
    {
        public uint FooValue { get; set; }
        public uint FooBase { get; set; }
        public string? FooString { get; set; }
    }

    class Bar : Foo
    {
        public decimal BarValue { get; set; }
        public new int FooBase { get; set; }
    }

    class Baz : Bar
    {
        public string? BazValue { get; set; }
        public new double FooValue { get; set; }
        public new double BarValue { get; set; }
        public double? NullalbelDouble { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreateTime { get; set; }
        public bool BoolValue { get; set; }
        public Bar? Bar { get; set; }
        public List<Foo>? FooList { get; set; }
    }

    static readonly List<Baz> TestData =
    [
        new() {BazValue="foo",FooValue=1.1,BarValue=1.2,FooBase=1,FooString="1.4",NullalbelDouble=1.5,Gender=Gender.Male,CreateTime=new DateTime(2024,1,1),BoolValue=true },
        new() {BazValue="bar",FooValue=2.1,BarValue=2.2,FooBase=2,FooString="2.4",Gender=Gender.Female,CreateTime=new DateTime(2024,2,1),BoolValue=false},
    ];

    [TestMethod]
    public void ListToDataTableTest()
    {
        var table = TestData.ToDataTable();
        Assert.AreEqual(9, table.Columns.Count);
        Assert.AreEqual(2, table.Rows.Count);
    }

    [TestMethod]
    public void DataTableToListTest1()
    {
        var table = TestData.ToDataTable();
        var list = table.ToList<Baz>();
        var listString = list.Serialize(new JsonOption { FormatJson = true });
        Console.WriteLine(listString);
        Assert.AreEqual(TestData.Serialize(new JsonOption { FormatJson = true }), listString);
    }

    [TestMethod]
    public void DataTableToListTest2()
    {
        var table = new DataTable();
        table.Columns.Add(new DataColumn("BazValue"));
        table.Columns.Add(new DataColumn("FooValue"));
        table.Columns.Add(new DataColumn("BarValue"));
        table.Columns.Add(new DataColumn("FooBase"));
        table.Columns.Add(new DataColumn("FooString"));
        table.Columns.Add(new DataColumn("NullalbelDouble"));
        table.Columns.Add(new DataColumn("Gender"));
        table.Columns.Add(new DataColumn("CreateTime"));
        table.Columns.Add(new DataColumn("BoolValue"));
        var fooRow = table.NewRow();
        fooRow["BazValue"] = "foo";
        fooRow["FooValue"] = 1.1;
        fooRow["BarValue"] = 1.2;
        fooRow["FooBase"] = 1;
        fooRow["FooString"] = "1.4";
        fooRow["NullalbelDouble"] = 1.5;
        fooRow["Gender"] = (int)Gender.Male;
        fooRow["CreateTime"] = new DateTime(2024, 1, 1);
        fooRow["BoolValue"] = true;
        table.Rows.Add(fooRow);
        var barRow = table.NewRow();
        barRow["BazValue"] = "bar";
        barRow["FooValue"] = 2.1;
        barRow["BarValue"] = 2.2;
        barRow["FooBase"] = 2;
        barRow["FooString"] = "2.4";
        barRow["Gender"] = (int)Gender.Female;
        barRow["CreateTime"] = new DateTime(2024, 2, 1);
        barRow["BoolValue"] = false;
        table.Rows.Add(barRow);

        var list = table.ToList<Baz>();
        var listString = list.Serialize(new JsonOption { FormatJson = true });
        Console.WriteLine(listString);
        Assert.AreEqual(TestData.Serialize(new JsonOption { FormatJson = true }), listString);
    }
}
