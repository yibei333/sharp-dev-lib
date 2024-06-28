using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Text.RegularExpressions;

namespace SharpDevLib.OpenXML;

/// <summary>
/// excel扩展
/// </summary>
public static class Excel
{
    /// <summary>
    /// 读取标准的Excel流,标准的定义为
    /// <para>1.第一行为表头</para>
    /// <para>2.每行的列不能超出表头的长度范围</para>
    /// <para>3.读取的结果中所有列的类型都为string类型</para>
    /// </summary>
    /// <param name="stream">标准的Excel流</param>
    /// <returns>DataSet</returns>
    public static DataSet Read(Stream stream)
    {
        using var doc = SpreadsheetDocument.Open(stream, false);
        var workbookPart = doc.GetPartsOfType<WorkbookPart>().FirstOrDefault() ?? throw new Exception($"WorkbookPart not found");
        var worksheetParts = workbookPart.GetPartsOfType<WorksheetPart>();
        if (worksheetParts.Count() <= 0) throw new Exception("WorksheetPart not found");
        var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? new List<SharedStringItem>();
        var sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>();
        if (sheets.Count() <= 0) throw new Exception("Sheets not found");
        if (sheets.Count() != worksheetParts.Count()) throw new Exception("Sheets count not match WorkseetParts count");

        var dataSet = new DataSet();

        foreach (var worksheetPart in worksheetParts)
        {
            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault() ?? throw new Exception("SheetData not found");
            var rid = workbookPart.GetIdOfPart(worksheetPart);
            var tableName = sheets.FirstOrDefault(x => x.Id == rid)?.Name ?? throw new Exception($"Get Sheet by rid('{rid}') failed");
            var table = new DataTable(tableName);
            dataSet.Tables.Add(table);

            var excelRowIndex = 0;
            var columnReferences = new List<string>();
            foreach (var excelRow in sheetData.Elements<Row>())
            {
                //header
                if (excelRowIndex == 0)
                {
                    foreach (var cell in excelRow.Elements<Cell>())
                    {
                        table.Columns.Add(new DataColumn(GetCellValue(cell, sharedStringItems), typeof(string)));
                        columnReferences.Add(AnalysisCellReference(cell.CellReference).Item1);
                    }
                }
                //contents
                else
                {
                    var row = table.NewRow();
                    table.Rows.Add(row);

                    var cells = excelRow.Elements<Cell>().Select(x => new { ColumnReference = columnReferences.FirstOrDefault(y => y == AnalysisCellReference(x.CellReference).Item1), Cell = x }).ToList();
                    for (int i = 0; i < columnReferences.Count; i++)
                    {
                        var cell = cells.FirstOrDefault(x => x.ColumnReference == columnReferences[i]);
                        var columnName = table.Columns[i].ColumnName;
                        row[columnName] = cell is null ? null : GetCellValue(cell.Cell, sharedStringItems);
                    }
                }
                excelRowIndex++;
            }
        }
        return dataSet;
    }

    public static SpreadsheetDocument Write(DataSet dataSet)
    {
        throw new NotImplementedException();
    }

    private static string? GetCellValue(Cell cell, List<SharedStringItem> sharedStringItems)
    {
        var text = cell.CellValue?.Text;
        var dataType = cell.DataType;
        if (dataType is null) return text;
        else if (dataType == CellValues.SharedString)
        {
            var sharedItem = sharedStringItems.ElementAt(int.Parse(text));
            return sharedItem.Text?.Text ?? string.Empty;
        }
        else
        {
            return text;
        }
    }

    #region CellReference
    private const string _columnExpression = "[A-Za-z]+";
    private const string _rowExpression = "[0-9]+";
    private static Tuple<string, int> AnalysisCellReference(string? cellReference)
    {
        if (cellReference.IsNullOrWhiteSpace()) return new Tuple<string, int>(string.Empty, 0);

        var match = Regex.Match(cellReference, _columnExpression);
        if (!match.Success) throw new Exception($"{cellReference} is not a valid CellReference");
        var column = match.Value;
        var row = int.Parse(Regex.Match(cellReference, _rowExpression).Value);
        return new Tuple<string, int>(column, row);
    }
    #endregion
}
