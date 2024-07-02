using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.OpenXML;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SharpDevLib.Tests.OpenXML;

[TestClass]
public class ExcelTests
{
    #region Data
    class Foo
    {
        public string? StringValue { get; set; }
        public int? IntValue { get; set; }
        public double? DoubleValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public bool? BoolValue { get; set; }
        public Gender? EnumValue { get; set; }
    }

    static readonly List<Foo> TestData1 =
    [
        new (){ StringValue="A1 Value",IntValue=1,DoubleValue=1.1d,DecimalValue=1.2m,BoolValue=true,EnumValue=Gender.Male },
        new (){ StringValue="A2 Value",IntValue=null,DoubleValue=null,DecimalValue=null,BoolValue=false,EnumValue=Gender.Female },
        new (){ StringValue="A3 Value is very long",IntValue=3,DoubleValue=3.1d,DecimalValue=3.2m,BoolValue=null },
        new (){ StringValue=null,IntValue=4,DoubleValue=4.1,DecimalValue=4.2m,BoolValue=true },
    ];

    static readonly string TestJson1 = TestData1.Serialize();

    static readonly List<Foo> TestData2 =
    [
        new (){ StringValue="A11 Value",IntValue=1,DoubleValue=1.1d,DecimalValue=1.2m,BoolValue=true,EnumValue=Gender.Male },
        new (){ StringValue="A12 Value",IntValue=null,DoubleValue=null,DecimalValue=null,BoolValue=false,EnumValue=Gender.Female },
        new (){ StringValue="A13 Value",IntValue=3,DoubleValue=3.1d,DecimalValue=3.2m,BoolValue=null },
        new (){ StringValue=null,IntValue=4,DoubleValue=4.1,DecimalValue=4.2m,BoolValue=true },
    ];
    static readonly string TestJson2 = TestData2.Serialize();
    #endregion

    [TestMethod]
    public void ReadTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var set = Excel.Read(stream);
        Assert.AreEqual(2, set.Tables.Count);
        var table1 = set.Tables["T1"];
        Assert.IsNotNull(table1);
        var list1 = table1.ToList<Foo>();
        var json1 = list1.Serialize();
        Console.WriteLine(json1);
        Assert.AreEqual(TestJson1, json1);

        var table2 = set.Tables["T2"];
        Assert.IsNotNull(table2);
        var list2 = table2.ToList<Foo>();
        var json2 = list2.Serialize();
        Console.WriteLine(json2);
        Assert.AreEqual(TestJson2, json2);
    }

    [TestMethod]
    public void WriteSetTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/write-set.xlsx");
        using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        var writeSet = new DataSet();
        var writeTable1 = TestData1.ToDataTable();
        writeSet.Tables.Add(writeTable1);

        var writeTable2 = TestData2.ToDataTable();
        writeSet.Tables.Add(writeTable2);
        Excel.Write(writeSet, stream);
        Assert.IsTrue(File.Exists(path));
        Assert.IsTrue(new FileInfo(path).Length > 0);

        var readSet = Excel.Read(stream);
        Assert.AreEqual(2, readSet.Tables.Count);
        var readTable1 = readSet.Tables["Table1"];
        Assert.IsNotNull(readTable1);
        var readList1 = readTable1.ToList<Foo>();
        var readListJson1 = readList1.Serialize();
        Console.WriteLine(readListJson1);
        Assert.AreEqual(TestJson1, readListJson1);

        var readTable2 = readSet.Tables["Table2"];
        Assert.IsNotNull(readTable2);
        var readList2 = readTable2.ToList<Foo>();
        var readListJson2 = readList2.Serialize();
        Console.WriteLine(readListJson2);
        Assert.AreEqual(TestJson2, readListJson2);
    }

    [TestMethod]
    public void WriteTableTest()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/write-table.xlsx");
        using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        var writeTable1 = TestData1.ToDataTable();
        Excel.Write(writeTable1, stream, false);

        var readSet = Excel.Read(stream);
        Assert.AreEqual(1, readSet.Tables.Count);
        var readTable1 = readSet.Tables["Table1"];
        Assert.IsNotNull(readTable1);
        var readList1 = readTable1.ToList<Foo>();
        var readListJson1 = readList1.Serialize();
        Console.WriteLine(readListJson1);
        Assert.AreEqual(TestJson1, readListJson1);
    }

    [TestMethod]
    public void EncryptTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/encrypt.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        Excel.Encrypt(sourceStream, targetStream, "foo");
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void DecryptTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Encrypted.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/decrypt.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        Excel.Decrypt(sourceStream, targetStream, "foo");
        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }
}
