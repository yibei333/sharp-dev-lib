using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SharpDevLib.OpenXML.References.ExcelEncryption;
using System.Data;

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
        using var memoryStream = new MemoryStream();
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
    /// <exception cref="Exception">读取失败时引发异常</exception>
    public static DataSet Read(Stream stream) => Read(stream, null);

    /// <summary>
    /// 读取标准的Excel流,标准的定义为
    /// <para>1.第一行为表头</para>
    /// <para>2.每行的列不能超出表头的长度范围</para>
    /// <para>3.读取的结果中所有列的类型都为string类型</para>
    /// </summary>
    /// <param name="stream">标准的Excel流</param>
    /// <param name="columnNames">自定义列名</param>
    /// <returns>DataSet</returns>
    /// <exception cref="Exception">读取失败时引发异常</exception>
    public static DataSet Read(Stream stream, string[][]? columnNames)
    {
        using var doc = SpreadsheetDocument.Open(stream, false);
        var workbookPart = doc.WorkbookPart ?? throw new Exception($"WorkbookPart not found");
        var dataSet = new DataSet();

        var sheetIndex = -1;
        if (columnNames.NotNullOrEmpty() && columnNames.Count() != workbookPart.GetPartsOfType<WorksheetPart>().Count()) throw new ArgumentException($"parameter '{nameof(columnNames)}' count not match sheet count");
        foreach (var worksheetPart in workbookPart.GetPartsOfType<WorksheetPart>())
        {
            sheetIndex++;
            var rid = workbookPart.GetIdOfPart(worksheetPart);
            var tableName = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(x => x.Id == rid)?.Name ?? throw new Exception($"Get Sheet by rid('{rid}') failed");
            var table = new DataTable(tableName);
            dataSet.Tables.Add(table);

            var rows = worksheetPart.Worksheet.Descendants<Row>();
            var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? [];

            //header
            var headerNameMap = new Dictionary<string, string>();
            var headerRow = rows.ElementAt(0);
            if (headerRow is null) break;
            var sheetColumnNames = columnNames.NotNullOrEmpty() ? columnNames[sheetIndex] : null;
            if (sheetColumnNames.NotNullOrEmpty() && headerRow.Elements<Cell>().Count() != sheetColumnNames.Count()) throw new ArgumentException($"sheet '{tableName}' column count not match");
            var index = 0;
            foreach (Cell headerCell in headerRow.Elements<Cell>())
            {
                var tableColumnName = string.Empty;
                if (sheetColumnNames.NotNullOrEmpty())
                {
                    tableColumnName = sheetColumnNames[index++];
                }
                else
                {
                    tableColumnName = headerCell.GetValue(sharedStringItems);
                }
                if (tableColumnName.IsNullOrWhiteSpace()) throw new Exception($"unable to get cell value with reference '{headerCell.CellReference}'");
                var excelColumnName = new CellReference(headerCell.CellReference).ColumnName;
                headerNameMap.Add(excelColumnName, tableColumnName);
                table.Columns.Add(new DataColumn(tableColumnName, typeof(string)));//every cell has different type,so unify to string format
            }

            //contents
            foreach (var row in rows.Skip(1))
            {
                var dataRow = table.NewRow();
                table.Rows.Add(dataRow);

                foreach (var excelCell in row.Elements<Cell>())
                {
                    var excelColumnName = new CellReference(excelCell.CellReference).ColumnName;
                    var tableColumnName = headerNameMap[excelColumnName];
                    var value = excelCell.GetValue(sharedStringItems);
                    dataRow[tableColumnName] = value;
                }
            }
        }
        return dataSet;
    }

    /// <summary>
    /// 将DataTable写入Excel
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    /// <param name="stream">一般为Excel的文件流</param>
    public static void Write(DataTable dataTable, Stream stream) => Write(dataTable, stream, null);

    /// <summary>
    /// 将DataTable写入Excel
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    /// <param name="stream">一般为Excel的文件流</param>
    /// <param name="columnNames">自定义列名</param>
    public static void Write(DataTable dataTable, Stream stream, string[]? columnNames)
    {
        var set = new DataSet();
        set.Tables.Add(dataTable);
        if (columnNames.NotNullOrEmpty()) Write(set, stream, [columnNames]);
        else Write(set, stream, null);
    }

    /// <summary>
    /// 将DataSet写入Excel
    /// </summary>
    /// <param name="dataSet">DataSet</param>
    /// <param name="stream">一般为Excel的文件流</param>
    public static void Write(DataSet dataSet, Stream stream) => Write(dataSet, stream, null);

    /// <summary>
    /// 将DataSet写入Excel
    /// </summary>
    /// <param name="dataSet">DataSet</param>
    /// <param name="stream">一般为Excel的文件流</param>
    /// <param name="columnNames">自定义列名</param>
    public static void Write(DataSet dataSet, Stream stream, string[][]? columnNames)
    {
        //structure
        using var doc = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
        var workbookPart = doc.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        var sheets = new Sheets();
        workbookPart.Workbook.AppendChild(sheets);

        //write table
        uint sheetIndex = 1;
        var sharedStringTable = workbookPart.GetSharedStringTable();
        var sharedStringDictionary = new Dictionary<string, int>();
        if (columnNames.NotNullOrEmpty() && columnNames.Length != dataSet.Tables.Count) throw new ArgumentException($"argument '{nameof(columnNames)}' not match table's count");
        var columnNameIndex = 0;
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

            var customColumnNames = columnNames.NotNullOrEmpty() ? columnNames[columnNameIndex++] : null;
            SetTableData(sheetData, table, sharedStringTable, sharedStringDictionary, customColumnNames);
            SetColumns(worksheetPart.Worksheet, table.Columns.Count);
        }

        doc.Save();
    }

    #region Private
    static void SetTableData(SheetData sheetData, DataTable table, SharedStringTable sharedStringTable, Dictionary<string, int> sharedStringDictionary, string[]? columnNames)
    {
        //header
        var headerRow = new Row { RowIndex = 1 };
        sheetData.AppendChild(headerRow);
        uint headerColumnIndex = 1;
        if (columnNames.NotNullOrEmpty() && columnNames.Count() != table.Columns.Count) throw new ArgumentException($"column count not match");
        var columnNameIndex = -1;

        foreach (DataColumn item in table.Columns)
        {
            columnNameIndex++;
            var cell = new Cell
            {
                DataType = CellValues.SharedString,
                CellReference = new CellReference(1, headerColumnIndex++).Reference
            };
            var columnName = columnNames.NotNullOrEmpty() ? columnNames[columnNameIndex] : item.ColumnName;
            cell.SetValue(columnName, sharedStringTable, sharedStringDictionary);
            headerRow.AppendChild(cell);
        }

        //content
        uint rowIndex = 2;
        foreach (DataRow dataRow in table.Rows)
        {
            var contentRow = new Row { RowIndex = rowIndex };
            sheetData.AppendChild(contentRow);
            uint contentColumnIndex = 1;

            foreach (DataColumn item in table.Columns)
            {
                var cell = new Cell
                {
                    CellReference = new CellReference(rowIndex, contentColumnIndex++).Reference
                };
                cell.SetValue(dataRow[item.ColumnName], sharedStringTable, sharedStringDictionary);
                contentRow.AppendChild(cell);
            }
            rowIndex++;
        }
    }

    static void SetColumns(Worksheet worksheet, int columnCount)
    {
        var columns = new Columns();
        for (uint i = 1; i <= columnCount; i++)
        {
            var column = new Column
            {
                Min = i,
                Max = i,
                CustomWidth = false,
                BestFit = true,
                Width = 10
            };
            columns.AppendChild(column);
        }
        worksheet.InsertBefore(columns, worksheet.GetFirstChild<SheetData>());
    }
    #endregion
}
