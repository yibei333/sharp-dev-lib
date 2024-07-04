using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.OpenXML;
using SharpDevLib.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

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
        new (){ StringValue="A12 Value",IntValue=null,DoubleValue=null,DecimalValue=null,BoolValue=false,EnumValue=Gender.SomeVeryLongLongLongLongLongLongValue },
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
        Excel.Write(writeTable1, stream);

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
    public void AutoColumnWidthTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/autowidth.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        doc.AutoColumnWidth();
        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
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

    [TestMethod]
    public void DeleteRowTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_DeleteRow.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        doc.WorkbookPart?.GetWorksheet("T2").DeleteRow(3);
        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);

        var readSet = Excel.Read(targetStream);
        Assert.AreEqual(2, readSet.Tables.Count);
        var readTable2 = readSet.Tables["T2"];
        Assert.IsNotNull(readTable2);
        var readList2 = readTable2.ToList<Foo>();
        Assert.AreEqual(3, readList2.Count);
    }

    [TestMethod]
    public void InsertRowTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_InsertRow.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        var row = doc.WorkbookPart?.GetWorksheet("T2").InsertRow(3);
        var cell = new Cell
        {
            DataType = CellValues.SharedString,
            CellReference = "A3"
        };
        cell.SetValue(doc.WorkbookPart!, "ok");
        row?.AppendChild(cell);
        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);

        var readSet = Excel.Read(targetStream);
        Assert.AreEqual(2, readSet.Tables.Count);
        var readTable2 = readSet.Tables["T2"];
        Assert.IsNotNull(readTable2);
        var readList2 = readTable2.ToList<Foo>();
        Assert.AreEqual(5, readList2.Count);
    }

    [TestMethod]
    public void InsertColumnTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_InsertColumn.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        doc.WorkbookPart?.GetWorksheet("T2").InsertColumn("C", true);
        var cell = doc.WorkbookPart?.GetWorksheet("T2").GetCells(x => x.CellReference == "C2").FirstOrDefault();
        Assert.IsNotNull(cell);
        cell.DataType = CellValues.Number;
        cell.CellValue = new CellValue(100);
        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void DeleteColumnTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_DeleteColumn.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        doc.WorkbookPart?.GetWorksheet("T2").DeleteColumn("C");
        var cell = doc.WorkbookPart?.GetWorksheet("T2").GetCells(x => x.CellReference == "C2").FirstOrDefault();
        Assert.IsNotNull(cell);
        cell.DataType = CellValues.Number;
        cell.CellValue = new CellValue(100);
        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void UpdateCellValueTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_UpdateCellValue.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        var cell = doc.WorkbookPart?.GetWorksheet("T2").GetCells(x => x.CellReference == "A3").FirstOrDefault();
        cell?.SetValue(doc.WorkbookPart!, 2);

        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void UpdateCellFormulaTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_UpdateCellFormula.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        var cell = doc.WorkbookPart?.GetWorksheet("T2").GetCells(x => x.CellReference == "A3").FirstOrDefault();
        cell!.CellFormula = new CellFormula("CONCATENATE(E3,F3)");

        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void UpdateCellStyleTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_UpdateCellStyle.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        var styleIndex = doc.WorkbookPart!.CreateStyle(new SharpDevLib.OpenXML.CellStyle
        {
            BackgroundColor = "#0000FF",
            FontColor = "#FFFFFF",
            WrapText = true,
            FontSize = 30
        });
        var cell = doc.WorkbookPart?.GetWorksheet("T2").GetCells(x => x.CellReference == "A3").FirstOrDefault();
        cell!.StyleIndex = styleIndex;

        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }

    [TestMethod]
    public void MergeCellsTest()
    {
        var sourcePath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/OpenXML/Normal.xlsx");
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/Normal_MergeCells.xlsx");
        using var sourceStream = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();

        using var doc = SpreadsheetDocument.Open(targetStream, true);
        var mergeCell = doc.WorkbookPart?.GetWorksheet("T2").MergeCells("d3", "h5");
        Assert.IsNotNull(mergeCell);
        mergeCell.UseStyle(new SharpDevLib.OpenXML.CellStyle { HorizontalAlignment = HorizontalAlignmentValues.Center, VerticalAlignment = VerticalAlignmentValues.Center, BackgroundColor = "#bcd" });

        doc.Save();
        targetStream.Flush();

        Assert.IsTrue(File.Exists(targetPath));
        Assert.IsTrue(new FileInfo(targetPath).Length > 0);
    }
}
