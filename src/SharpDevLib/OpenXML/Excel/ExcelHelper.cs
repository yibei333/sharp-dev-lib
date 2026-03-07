using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SharpDevLib.OpenXML.References.ExcelEncryption;
using System.Data;

namespace SharpDevLib;

/// <summary>
/// Excel 文件读写扩展类
/// 提供 Excel 文件的密码加密解密、DataTable/DataSet 读写等常用功能
/// </summary>
public static class ExcelHelper
{
    /// <summary>
    /// 为 Excel 文件设置密码保护
    /// </summary>
    /// <param name="inputStream">输入的 Excel 文件流</param>
    /// <param name="outputStream">输出密码保护的 Excel 文件流</param>
    /// <param name="password">密码字符串</param>
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
    /// 去除 Excel 文件的密码保护
    /// </summary>
    /// <param name="inputStream">密码保护的 Excel 文件流</param>
    /// <param name="outputStream">输出去除密码的 Excel 文件流</param>
    /// <param name="password">原密码字符串</param>
    public static void Decrypt(Stream inputStream, Stream outputStream, string password)
    {
        using var memoryStream = new MemoryStream();
        inputStream.CopyTo(memoryStream);
        using var decryptedStream = new EncryptedPackageHandler().DecryptPackage(memoryStream, new ExcelEncryption { Password = password, IsEncrypted = true });
        var zipPackage = new ZipPackage(decryptedStream);
        zipPackage.Save(outputStream);
    }

    /// <summary>
    /// 从 Excel 流读取指定工作表的数据到 DataTable
    /// </summary>
    /// <param name="stream">Excel 文件流</param>
    /// <param name="index">工作表索引,从 0 开始</param>
    /// <returns>包含工作表数据的 DataTable</returns>
    /// <exception cref="Exception">读取失败或找不到工作表时引发异常</exception>
    /// <remarks>
    /// 标准 Excel 格式要求:
    /// 1. 第一行为表头
    /// 2. 每行的列不能超出表头的长度范围
    /// 3. 读取的结果中所有列的类型都为 string 类型
    /// </remarks>
    public static DataTable ReadTable(Stream stream, int index = 0) => ReadTable(stream, null, index);

    /// <summary>
    /// 从 Excel 流读取指定工作表的数据到 DataTable,并使用自定义列名
    /// </summary>
    /// <param name="stream">Excel 文件流</param>
    /// <param name="columnNames">自定义列名数组,如果为 null 则使用 Excel 中的表头</param>
    /// <param name="index">工作表索引,从 0 开始</param>
    /// <returns>包含工作表数据的 DataTable</returns>
    /// <exception cref="Exception">读取失败或找不到工作表时引发异常</exception>
    /// <exception cref="ArgumentException">当自定义列名数量与 Excel 列数不匹配时引发异常</exception>
    /// <remarks>
    /// 标准 Excel 格式要求:
    /// 1. 第一行为表头
    /// 2. 每行的列不能超出表头的长度范围
    /// 3. 读取的结果中所有列的类型都为 string 类型
    /// </remarks>
    public static DataTable ReadTable(Stream stream, string[]? columnNames, int index = 0)
    {
        using var doc = SpreadsheetDocument.Open(stream, false);
        var workbookPart = doc.WorkbookPart ?? throw new Exception($"找不到WorkbookPart");
        var worksheetPart = workbookPart.GetPartsOfType<WorksheetPart>().ElementAt(index);
        return ReadTable(workbookPart, worksheetPart, columnNames);
    }

    /// <summary>
    /// 从 Excel 流读取所有工作表的数据到 DataSet
    /// </summary>
    /// <param name="stream">Excel 文件流</param>
    /// <returns>包含所有工作表数据的 DataSet</returns>
    /// <exception cref="Exception">读取失败时引发异常</exception>
    /// <remarks>
    /// 标准 Excel 格式要求:
    /// 1. 第一行为表头
    /// 2. 每行的列不能超出表头的长度范围
    /// 3. 读取的结果中所有列的类型都为 string 类型
    /// </remarks>
    public static DataSet ReadSet(Stream stream) => ReadSet(stream, null);

    /// <summary>
    /// 从 Excel 流读取所有工作表的数据到 DataSet,并使用自定义列名
    /// </summary>
    /// <param name="stream">Excel 文件流</param>
    /// <param name="columnNames">二维数组,每个元素对应一个工作表的自定义列名数组</param>
    /// <returns>包含所有工作表数据的 DataSet</returns>
    /// <exception cref="Exception">读取失败时引发异常</exception>
    /// <exception cref="ArgumentException">当自定义列名数量与工作表数量不匹配时引发异常</exception>
    /// <remarks>
    /// 标准 Excel 格式要求:
    /// 1. 第一行为表头
    /// 2. 每行的列不能超出表头的长度范围
    /// 3. 读取的结果中所有列的类型都为 string 类型
    /// </remarks>
    public static DataSet ReadSet(Stream stream, params string[][]? columnNames)
    {
        using var doc = SpreadsheetDocument.Open(stream, false);
        var workbookPart = doc.WorkbookPart ?? throw new Exception($"找不到WorkbookPart");
        var dataSet = new DataSet();

        var sheetIndex = -1;
        if (columnNames.NotNullOrEmpty() && columnNames.Count() != workbookPart.GetPartsOfType<WorksheetPart>().Count()) throw new ArgumentException($"参数'{nameof(columnNames)}'的数量与工作表数量不匹配");
        foreach (var worksheetPart in workbookPart.GetPartsOfType<WorksheetPart>())
        {
            sheetIndex++;
            var sheetColumnNames = columnNames.NotNullOrEmpty() ? columnNames[sheetIndex] : null;
            dataSet.Tables.Add(ReadTable(workbookPart, worksheetPart, sheetColumnNames));
        }
        return dataSet;
    }

    /// <summary>
    /// 将 DataTable 数据写入 Excel 文件
    /// </summary>
    /// <param name="dataTable">要写入的数据表</param>
    /// <param name="stream">Excel 文件流</param>
    public static void Write(DataTable dataTable, Stream stream) => Write(dataTable, stream, null);

    /// <summary>
    /// 将 DataTable 数据写入 Excel 文件,并使用自定义列名
    /// </summary>
    /// <param name="dataTable">要写入的数据表</param>
    /// <param name="stream">Excel 文件流</param>
    /// <param name="columnNames">自定义列名数组,如果为 null 则使用 DataTable 的列名</param>
    public static void Write(DataTable dataTable, Stream stream, string[]? columnNames)
    {
        var set = new DataSet();
        set.Tables.Add(dataTable);
        if (columnNames.NotNullOrEmpty()) Write(set, stream, [columnNames]);
        else Write(set, stream, null);
    }

    /// <summary>
    /// 将 DataSet 数据写入 Excel 文件
    /// </summary>
    /// <param name="dataSet">要写入的数据集</param>
    /// <param name="stream">Excel 文件流</param>
    public static void Write(DataSet dataSet, Stream stream) => Write(dataSet, stream, null);

    /// <summary>
    /// 将 DataSet 数据写入 Excel 文件,并使用自定义列名
    /// </summary>
    /// <param name="dataSet">要写入的数据集</param>
    /// <param name="stream">Excel 文件流</param>
    /// <param name="columnNames">二维数组,每个元素对应一个 DataTable 的自定义列名数组</param>
    /// <exception cref="ArgumentException">当自定义列名数量与 DataTable 数量不匹配时引发异常</exception>
    public static void Write(DataSet dataSet, Stream stream, params string[][]? columnNames)
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
        if (columnNames.NotNullOrEmpty() && columnNames.Length != dataSet.Tables.Count) throw new ArgumentException($"参数'{nameof(columnNames)}'与数据表数量不匹配");
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
    static DataTable ReadTable(WorkbookPart workbookPart, WorksheetPart worksheetPart, string[]? columnNames)
    {
        var rid = workbookPart.GetIdOfPart(worksheetPart);
        var tableName = workbookPart.Workbook?.Descendants<Sheet>().FirstOrDefault(x => x.Id == rid)?.Name ?? throw new Exception($"通过rid('{rid}')获取工作表失败");
        var table = new DataTable(tableName);
        var rows = worksheetPart.Worksheet?.Descendants<Row>();
        var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? [];

        //header
        var headerNameMap = new Dictionary<string, string>();
        var headerRow = rows.ElementAt(0);
        if (headerRow is null) return table;
        if (columnNames.NotNullOrEmpty() && headerRow.Elements<Cell>().Count() != columnNames.Count()) throw new ArgumentException($"工作表'{tableName}'的列数量不匹配");
        var index = 0;
        foreach (Cell headerCell in headerRow.Elements<Cell>())
        {
            var tableColumnName = string.Empty;
            if (columnNames.NotNullOrEmpty())
            {
                tableColumnName = columnNames[index++];
            }
            else
            {
                tableColumnName = headerCell.GetValue(sharedStringItems);
            }
            if (tableColumnName.IsNullOrWhiteSpace()) throw new Exception($"无法获取引用'{headerCell.CellReference}'对应的单元格值");
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
        return table;
    }

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
