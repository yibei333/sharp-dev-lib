using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SharpDevLib.OpenXML.References.ExcelEncryption;
using System.Data;
using System.Text.RegularExpressions;

namespace SharpDevLib.OpenXML;

/// <summary>
/// excel扩展
/// </summary>
public static class Excel
{
    /// <summary>
    /// 密码保护excel
    /// </summary>
    /// <param name="inputStream">excel文件流</param>
    /// <param name="outputStream">密码保护的excel文件流</param>
    /// <param name="password">密码</param>
    public static void Encrypt(Stream inputStream, Stream outputStream, string password)
    {
        using var memoryStream = new MemoryStream(); ;
        inputStream.CopyTo(memoryStream);

        using var encryptedStream = new EncryptedPackageHandler().EncryptPackage(memoryStream.ToArray(), new ExcelEncryption { Password = password, Version = EncryptionVersion.Standard, IsEncrypted = true });
        var array = encryptedStream.ToArray();
        outputStream.Write(array, 0, array.Length);
        outputStream.Flush();
    }

    /// <summary>
    /// 去除excel密码保护
    /// </summary>
    /// <param name="inputStream">密码保护的excel文件流</param>
    /// <param name="outputStream">去除密码的excel文件流</param>
    /// <param name="password">密码</param>
    public static void Decrypt(Stream inputStream, Stream outputStream, string password)
    {
        using var memoryStream = new MemoryStream(); ;
        inputStream.CopyTo(memoryStream);
        using var decryptedStream = new EncryptedPackageHandler().DecryptPackage(memoryStream, new ExcelEncryption { Password = password, IsEncrypted = true });
        var zipPackage = new ZipPackage(decryptedStream);
        zipPackage.Save(outputStream);
    }

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
                    var dataRow = table.NewRow();
                    table.Rows.Add(dataRow);

                    var cells = excelRow.Elements<Cell>().Select(x => new { ColumnReference = columnReferences.FirstOrDefault(y => y == AnalysisCellReference(x.CellReference).Item1), Cell = x }).ToList();
                    for (int i = 0; i < columnReferences.Count; i++)
                    {
                        var cell = cells.FirstOrDefault(x => x.ColumnReference == columnReferences[i]);
                        var columnName = table.Columns[i].ColumnName;
                        dataRow[columnName] = cell is null ? null : GetCellValue(cell.Cell, sharedStringItems);
                    }
                }
                excelRowIndex++;
            }
        }
        return dataSet;
    }

    /// <summary>
    /// 将DataTable写入Excel
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    /// <param name="stream">一般为Excel的文件流</param>
    /// <param name="autoColumnWidth">是否自动设置列的宽度</param>
    public static void Write(DataTable dataTable, Stream stream, bool autoColumnWidth = true)
    {
        var set = new DataSet();
        set.Tables.Add(dataTable);
        Write(set, stream, autoColumnWidth);
    }

    /// <summary>
    /// 将DataSet写入Excel
    /// </summary>
    /// <param name="dataSet">DataSet</param>
    /// <param name="stream">一般为Excel的文件流</param>
    /// <param name="autoColumnWidth">是否自动设置列的宽度</param>
    public static void Write(DataSet dataSet, Stream stream, bool autoColumnWidth = true)
    {
        //structure
        using var doc = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
        var workbookPart = doc.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        var sheets = new Sheets();
        workbookPart.Workbook.AppendChild(sheets);
        var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
        sharedStringTablePart.SharedStringTable = new SharedStringTable();

        //write table
        uint sheetIndex = 1;
        foreach (DataTable table in dataSet.Tables)
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet();
            var sheetData = new SheetData();
            worksheetPart.Worksheet.AppendChild(sheetData);

            var tableName = table.TableName.IsNullOrWhiteSpace() ? $"Sheet{sheetIndex}" : table.TableName;
            var sheet = new Sheet { Id = workbookPart.GetIdOfPart(worksheetPart), Name = tableName, SheetId = sheetIndex };
            sheets.AppendChild(sheet);
            sheetIndex++;

            var columnWordLength = SetTableData(sharedStringTablePart.SharedStringTable, sheetData, table);
            SetColumns(worksheetPart.Worksheet, autoColumnWidth, columnWordLength);
        }

        doc.Save();
    }

    static Dictionary<string, int> SetTableData(SharedStringTable sharedStringTable, SheetData sheetData, DataTable table)
    {
        var columnWordLength = new Dictionary<string, int>();
        var rowIndex = 1;

        //header
        var headerRow = new Row();
        sheetData.AppendChild(headerRow);
        var headerColumnIndex = 0;

        foreach (DataColumn item in table.Columns)
        {
            var xx = sharedStringTable.AppendChild(new SharedStringItem(new Text("")));

            var cell = new Cell
            {
                DataType = CellValues.SharedString,
                CellValue = new CellValue(SetSharedStringItem(sharedStringTable, item.ColumnName)),
                CellReference = GetCellReference(rowIndex, headerColumnIndex++)
            };
            headerRow.AppendChild(cell);
            SetWordLength(columnWordLength, cell.CellReference, item.ColumnName.Length);
        }
        rowIndex++;

        //content
        foreach (DataRow dataRow in table.Rows)
        {
            var contentRow = new Row();
            sheetData.AppendChild(contentRow);
            var contentColumnIndex = 0;

            foreach (DataColumn item in table.Columns)
            {
                var cell = new Cell
                {
                    CellReference = GetCellReference(rowIndex, contentColumnIndex++)
                };
                SetCellValue(sharedStringTable, cell, dataRow[item.ColumnName]);
                contentRow.AppendChild(cell);
                SetWordLength(columnWordLength, cell.CellReference, dataRow[item.ColumnName]?.ToString()?.Length ?? 0);
            }
            rowIndex++;
        }
        return columnWordLength;
    }

    static void SetCellValue(SharedStringTable sharedStringTable, Cell cell, object? value)
    {
        if (value is null)
        {
            cell.CellValue = null;
            cell.DataType = CellValues.String;
            return;
        }

        var type = value.GetType();
        if (type.IsNumericDatatype())
        {
            cell.DataType = CellValues.Number;
            cell.CellValue = new CellValue(value.ToString());
        }
        else if (type == typeof(bool))
        {
            cell.CellValue = new CellValue(bool.Parse(value.ToString()));
            cell.DataType = CellValues.Boolean;
        }
        else
        {
            cell.CellValue = new CellValue(SetSharedStringItem(sharedStringTable, value.ToString()));
            cell.DataType = CellValues.SharedString;
        }
    }

    static int SetSharedStringItem(SharedStringTable shareStringTable, string text)
    {
        int i = 0;

        foreach (SharedStringItem item in shareStringTable.Elements<SharedStringItem>())
        {
            if (item.InnerText == text) return i;
            i++;
        }

        shareStringTable.AppendChild(new SharedStringItem(new Text(text)));
        shareStringTable.Save();
        return i;
    }

    static bool IsNumericDatatype(this Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
            _ => false,
        };
    }

    static void SetWordLength(Dictionary<string, int> container, string? cellReference, int length)
    {
        if (cellReference.IsNullOrWhiteSpace()) return;
        cellReference = AnalysisCellReference(cellReference).Item1;
        if (container.ContainsKey(cellReference))
        {
            container[cellReference] = Math.Max(container[cellReference], length);
        }
        else
        {
            container[cellReference] = length;
        }
    }

    static string? GetCellValue(Cell cell, List<SharedStringItem> sharedStringItems)
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

    static void SetColumns(Worksheet worksheet, bool autoColumnWidth, Dictionary<string, int> columnWordLength)
    {
        var columns = new Columns();
        uint index = 1;
        foreach (var item in columnWordLength)
        {
            var column = new Column
            {
                Min = index,
                Max = index,
            };
            if (autoColumnWidth)
            {
                column.Width = item.Value;
                column.CustomWidth = true;
            }
            else
            {
                column.CustomWidth = false;
                column.BestFit = true;
                column.Width = 10;
            }
            index++;
            columns.AppendChild(column);
        }
        worksheet.InsertBefore(columns, worksheet.GetFirstChild<SheetData>());
    }

    #region CellReference
    const string _columnExpression = "[A-Za-z]+";
    const string _rowExpression = "[0-9]+";

    static Tuple<string, int> AnalysisCellReference(string? cellReference)
    {
        if (cellReference.IsNullOrWhiteSpace()) return new Tuple<string, int>(string.Empty, 0);

        var match = Regex.Match(cellReference, _columnExpression);
        if (!match.Success) throw new Exception($"{cellReference} is not a valid CellReference");
        var column = match.Value;
        var row = int.Parse(Regex.Match(cellReference, _rowExpression).Value);
        return new Tuple<string, int>(column, row);
    }

    static string GetCellReference(int rowIndex, int colIndex)
    {
        var prefixCount = colIndex / 26;
        if (prefixCount >= 26) throw new NotSupportedException($"max cellreference is ZZ");
        var prefix = prefixCount > 0 ? ((char)(prefixCount + 65)).ToString() : "";
        var nameCount = colIndex % 26;
        var name = ((char)(nameCount + 65)).ToString();
        return $"{prefix}{name}{rowIndex}";
    }
    #endregion
}
