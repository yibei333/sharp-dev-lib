using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Data;

namespace SharpDevLib.Tests.Basic.DataTable;

[TestClass]
public class DataTableExtensionTests
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
        Assert.HasCount(10, table.Columns);
        Assert.HasCount(2, table.Rows);
        Console.WriteLine(table.Serialize());
        Console.WriteLine(table.Serialize(x => (x["FooBase"]?.ToString()?.ToInt() ?? 0) == 2));
        Console.WriteLine(table.Serialize(takeCount: 1));
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
        var table = new System.Data.DataTable();
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
    public void ListToTableWithMapping()
    {
        var users = new List<User>
        {
            new() { Id = 1, Name = "张三", Age = 25, IsActive = true, CreateTime = DateTime.Now },
            new() { Id = 2, Name = "李四", Age = 30, IsActive = false, CreateTime = DateTime.Now.AddDays(-1) },
            new() { Id = 3, Name = "王五", Age = 28, IsActive = true, CreateTime = DateTime.Now.AddDays(-2) }
        };

        var table1 = users.ToDataTable(["Id", "Name", "CreateTime"]);
        Console.WriteLine(table1.Serialize());

        ListToTableMapping.DefaultNameConventer = (name) => name.GetTranslate();
        var table2 = users.ToDataTable([
            new ListToTableMapping("Id"),
            new ListToTableMapping("Name"),
            new ListToTableMapping("CreateTime")
            {
                NameConverter=(name)=>$"自定义列名_{name}*",
                ValueConverter=(value,user)=>(user as User)?.CreateTime.ToTimeString()
            },
        ]);
        Console.WriteLine(table2.Serialize());
    }

    class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

public static class I18NService
{
    static readonly Dictionary<string, string> _translates = new()
    {
        {"Id","标识"},
        {"Name","姓名"},
        {"CreateTime","创建时间"},
    };

    public static string GetTranslate(this string key) => _translates.TryGetValue(key, out var value) ? value : key;
}