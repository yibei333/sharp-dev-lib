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
        public long TimestampValue { get; set; }
        public bool BoolValue { get; set; }
        public Bar? Bar { get; set; }
        public List<Foo>? FooList { get; set; }
    }

    static readonly List<Baz> TestData =
    [
        new() {BazValue="foo",FooValue=1.1,BarValue=1.2,FooBase=1,FooString="1.4",NullalbelDouble=1.5,Gender=Gender.Male,CreateTime=new DateTime(2024,1,1),BoolValue=true,TimestampValue= new DateTime(2024,1,1).ToUtcTimestamp()},
        new() {BazValue="bar",FooValue=2.1,BarValue=2.2,FooBase=2,FooString="2.4",Gender=Gender.Female,CreateTime=new DateTime(2024,2,1),BoolValue=false,TimestampValue= new DateTime(2024,2,1).ToUtcTimestamp()},
    ];

    [TestMethod]
    public void ListToDataTableTest()
    {
        var table = TestData.ToDataTable();
        Assert.AreEqual(10, table.Columns.Count);
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
        table.Columns.Add(new DataColumn("TimestampValue"));
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
        fooRow["TimestampValue"] = new DateTime(2024, 1, 1).ToUtcTimestamp();
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
        barRow["TimestampValue"] = new DateTime(2024, 2, 1).ToUtcTimestamp();
        table.Rows.Add(barRow);

        var list = table.ToList<Baz>();
        var listString = list.Serialize(new JsonOption { FormatJson = true });
        Console.WriteLine(listString);
        Assert.AreEqual(TestData.Serialize(new JsonOption { FormatJson = true }), listString);
    }

    [TestMethod]
    public void TransferTest()
    {
        var sourceTable = TestData.ToDataTable();
        var columns = new DataTableTransferColumn[]
        {
            new("FooValue"),
            new("BazValue") {NameConverter=key=>key+"_Converted",ValueConverter=(v,row)=>$"{v}_{row["FooValue"]}_converted",IsRequired=true},
            new("CreateTime") {NameConverter=key=>"创建时间",ValueConverter=(v,row)=>Convert.ToDateTime(v.ToString()).ToString("yyyy-MM-dd"),IsRequired=true},
            new("TimestampValue") {NameConverter=key=>"创建时间戳",ValueConverter=(v,row)=>long.Parse(v.ToString()!).ToUtcTime().ToTimeString(),IsRequired=true,TargetType=typeof(string)},
        };
        var targetTable = sourceTable.Transfer(columns);
        Assert.AreEqual(4, targetTable.Columns.Count);
        Assert.AreEqual(2, targetTable.Rows.Count);

        var backColumns = new DataTableTransferColumn[]
        {
            new("FooValue"),
            new("* BazValue_Converted") {NameConverter=key=>key.Split("_")[0],ValueConverter=(v,row)=>v.ToString()!.Split("_")[0]},
            new("* 创建时间") {NameConverter=key=>"CreateTime"},
            new("* 创建时间戳") {NameConverter=key=>"TimestampValue",ValueConverter=(v,row)=>Convert.ToDateTime(v.ToString()!).ToUtcTimestamp(),TargetType=typeof(long)},
        };
        var tarnsferBackTable = targetTable.Transfer(backColumns);
        Assert.AreEqual(4, tarnsferBackTable.Columns.Count);
        Assert.AreEqual(2, tarnsferBackTable.Rows.Count);
    }
}
