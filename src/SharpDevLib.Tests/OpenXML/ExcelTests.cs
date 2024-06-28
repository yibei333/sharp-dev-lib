using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.OpenXML;
using SharpDevLib.Tests.TestData;
using System.Collections.Generic;
using System;
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
        public bool? BoolValue { get; set; }
    }

    static readonly List<Foo> TestData1 =
    [
        new (){ StringValue="A1 Value",IntValue=1,BoolValue=true },
        new (){ StringValue="A2 Value",IntValue=null,BoolValue=false },
        new (){ StringValue="A3 Value",IntValue=3,BoolValue=null },
        new (){ StringValue=null,IntValue=4,BoolValue=true },
    ];

    static readonly List<Foo> TestData2 =
    [
        new (){ StringValue="A11 Value",IntValue=1,BoolValue=true },
        new (){ StringValue="A12 Value",IntValue=null,BoolValue=false },
        new (){ StringValue="A13 Value",IntValue=3,BoolValue=null },
        new (){ StringValue=null,IntValue=4,BoolValue=true },
    ];
    #endregion


    [TestMethod]
    public void ReadTest()
    {
        var path = @"C:\Users\Devel\OneDrive\桌面\2.xlsx";
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var set = Excel.Read(stream);
    }

    [TestMethod]
    public void WriteTest()
    {
        var path = @"C:\Users\Devel\OneDrive\桌面\1.xlsx";
        var document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);

        var workbookPart = document.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        var sheets = workbookPart.Workbook.AppendChild(new Sheets());

        var worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart1.Worksheet = new Worksheet();
        var sheetData1 = new SheetData();
        worksheetPart1.Worksheet.AddChild(sheetData1);
        var sheet1 = new Sheet { Id = workbookPart.GetIdOfPart(worksheetPart1), SheetId = 1, Name = "foo" };
        sheets.Append(sheet1);

        var worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart2.Worksheet = new Worksheet();
        var sheetData2 = new SheetData();
        worksheetPart2.Worksheet.AddChild(sheetData2);
        var sheet2 = new Sheet { Id = workbookPart.GetIdOfPart(worksheetPart2), SheetId = 2, Name = "bar" };
        sheets.Append(sheet2);

        SetColumn(worksheetPart1.Worksheet);
        SetColumn(worksheetPart2.Worksheet);
        WriteData(sheetData1, 1);
        WriteData(sheetData2, 2);

        var customUIContent = @"<customUI xmlns=""http://schemas.microsoft.com/office/2006/01/customui"">
    <ribbon>
        <tabs>
            <tab idMso=""TabAddIns"">
                <group id=""Group1"" label=""Group1"">
                    <button id=""Button1"" label=""Click Me!"" showImage=""false"" onAction=""SampleMacro""/>
                </group>
            </tab>
        </tabs>
    </ribbon>
</customUI>";

        var part = document.RibbonExtensibilityPart;
        part ??= document.AddRibbonExtensibilityPart();

        part.CustomUI = new CustomUI(customUIContent);
        part.CustomUI.Save();

        document.Save();
        document.Dispose();
    }

    static void WriteData(SheetData sheetData, int type)
    {
        var columns = new Row();
        sheetData.Append(columns);
        var column1 = new Cell
        {
            CellValue = new CellValue("foo"),
            DataType = new EnumValue<CellValues>(CellValues.String)
        };
        var column2 = new Cell
        {
            CellValue = new CellValue("bar"),
            DataType = new EnumValue<CellValues>(CellValues.String)
        };
        columns.Append(column1);
        columns.Append(column2);



        for (int i = 0; i < 100 * type; i++)
        {
            var row = new Row();
            sheetData.Append(row);
            var value1 = new Cell
            {
                CellValue = new CellValue($"this is foo field value {i}\r\nthis is foo field value {i}"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            };
            var value2 = new Cell
            {
                CellValue = new CellValue($"this is bar field value {i}\r\nthis is bar field value {i}"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            };
            row.Append(value1);
            row.Append(value2);
        }
    }

    static void SetColumn(Worksheet worksheet)
    {
        var columns = new Columns();
        var column1 = new Column
        {
            Min = 1,
            Max = 1,
            Width = 10d,
            BestFit = true,
            CustomWidth = false
        };
        columns.Append(column1);

        var column2 = new Column
        {
            Min = 2,
            Max = 2,
            Width = 10d,
            BestFit = true,
            CustomWidth = false
        };
        columns.Append(column2);

        worksheet.InsertAt(columns, 0);
    }
}
