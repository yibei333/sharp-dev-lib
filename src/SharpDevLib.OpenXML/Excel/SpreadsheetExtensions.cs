using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SharpDevLib.OpenXML;

/// <summary>
/// 电子表格扩展
/// </summary>
public static class SpreadsheetExtensions
{
    #region Common
    /// <summary>
    /// 根据类型获取父节点(跨级)
    /// </summary>
    /// <typeparam name="TParent">父节点类型</typeparam>
    /// <param name="element">元素</param>
    /// <returns>父节点</returns>
    static TParent? GetParent<TParent>(this object? element) where TParent : class
    {
        if (element is null) return null;

        if (element is OpenXmlPartRootElement xmlElementRoot)
        {
            if (xmlElementRoot.OpenXmlPart is TParent parent) return parent;
            return xmlElementRoot.OpenXmlPart.GetParent<TParent>();
        }
        else if (element is OpenXmlElement xmlElement)
        {
            if (xmlElement.Parent is TParent parent) return parent;
            return xmlElement.Parent.GetParent<TParent>();
        }
        else if (element is OpenXmlPart part)
        {
            foreach (var item in part.GetParentParts())
            {
                if (item is TParent parent) return parent;
                else return item.GetParent<TParent>();
            }
        }
        return null;
    }
    #endregion

    #region Workbook
    /// <summary>
    /// 获取工作表格
    /// </summary>
    /// <param name="workbookPart">工作簿部件</param>
    /// <param name="tableName">表明,如sheet1</param>
    /// <returns>工作表格</returns>
    public static Worksheet GetWorksheet(this WorkbookPart workbookPart, string tableName)
    {
        Sheet sheet = workbookPart.Workbook.Elements<Sheets>().SelectMany(x => x.Elements<Sheet>().Where(y => y.Name == tableName)).FirstOrDefault() ?? throw new Exception($"table with name '{tableName}' not found");
        string worksheetPartId = sheet.Id?.ToString() ?? string.Empty;
        if (worksheetPartId.IsNullOrWhiteSpace()) throw new Exception();
        var worksheetPart = workbookPart.GetPartById(worksheetPartId) as WorksheetPart ?? throw new Exception($"WorksheetPart with id '{worksheetPartId}' not found");
        return worksheetPart.Worksheet;
    }

    /// <summary>
    /// 设置SharedString
    /// </summary>
    /// <param name="workbookPart">工作簿部件</param>
    /// <param name="text">字符串</param>
    /// <returns>SharedStringItem索引</returns>
    public static int SetSharedStringItem(this WorkbookPart workbookPart, string text)
    {
        var sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault() ?? workbookPart.AddNewPart<SharedStringTablePart>();
        sharedStringTablePart.SharedStringTable ??= new SharedStringTable();
        var sharedStringTable = sharedStringTablePart.SharedStringTable;

        int i = 0;
        foreach (SharedStringItem item in sharedStringTable.Elements<SharedStringItem>())
        {
            if (item.InnerText == text) return i;
            i++;
        }

        sharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
        sharedStringTable.Save();
        return i;
    }
    #endregion

    #region Worksheet
    /// <summary>
    /// 获取单元格集合
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="condition">查询条件</param>
    /// <returns>单元格集合</returns>
    public static IEnumerable<Cell> GetCells(this Worksheet worksheet, Func<Cell, bool> condition) => worksheet.Descendants<Cell>().Where(condition);

    /// <summary>
    /// 删除行(如果该行有合并单元格,则会产生不可预期的效果)
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="rowIndex">行号</param>
    public static void DeleteRow(this Worksheet worksheet, uint rowIndex)
    {
        var currentRow = worksheet.Descendants<Row>().FirstOrDefault(x => x.RowIndex is not null && x.RowIndex == rowIndex);
        if (currentRow is null) return;
        currentRow.Remove();

        var belowRows = worksheet.Descendants<Row>().Where(x => x.RowIndex is not null && x.RowIndex > rowIndex);
        foreach (Row belowRow in belowRows)
        {
            if (belowRow.RowIndex is not null) belowRow.RowIndex -= 1;

            foreach (Cell cell in belowRow.Elements<Cell>())
            {
                var currentCellReference = new CellReference(cell.CellReference);
                cell.CellReference = $"{currentCellReference.ColumnName}{currentCellReference.RowIndex - 1}";
            }
        }
    }

    /// <summary>
    /// 插入空行(如果该行有合并单元格,则会产生不可预期的效果)
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="rowIndex">行号</param>
    /// <returns>插入的空行</returns>
    public static Row InsertRow(this Worksheet worksheet, uint rowIndex)
    {
        var belowRows = worksheet.Descendants<Row>().Where(x => x.RowIndex is not null && x.RowIndex >= rowIndex);
        foreach (var belowRow in belowRows)
        {
            if (belowRow.RowIndex is not null) belowRow.RowIndex += 1;

            foreach (Cell cell in belowRow.Elements<Cell>())
            {
                var referenceInfo = new CellReference(cell.CellReference);
                cell.CellReference = $"{referenceInfo.ColumnName}{referenceInfo.RowIndex + 1}";
            }
        }

        var newRow = new Row { RowIndex = rowIndex };
        worksheet.Elements<SheetData>().FirstOrDefault()?.InsertBefore(newRow, belowRows.FirstOrDefault());
        return newRow;
    }

    /// <summary>
    /// 插入空白列(如果该行有合并单元格,则会产生不可预期的效果)
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="columnName">列明,如A,B,C</param>
    /// <param name="insertColumnCells">是否要插入单元格</param>
    public static void InsertColumn(this Worksheet worksheet, string columnName, bool insertColumnCells)
    {
        var cellReference = new CellReference(columnName + 1);
        var sheetData = worksheet.Elements<SheetData>().FirstOrDefault();
        if (sheetData is null)
        {
            sheetData = new SheetData();
            worksheet.AppendChild(sheetData);
        }

        //column
        var columns = worksheet.Elements<Columns>().FirstOrDefault();
        if (columns is null)
        {
            columns = new Columns();
            worksheet.InsertBefore(columns, sheetData);
        }

        var columnCollection = columns.Elements<Column>().Where(x => x.Max is not null && x.Max >= cellReference.ColumnIndex).ToList();
        foreach (var item in columnCollection)
        {
            if (item.Min is null || item.Max is null) continue;

            if (item.Min >= cellReference.ColumnIndex)
            {
                item.Min += 1;
                item.Max += 1;
            }
            else if (item.Max == cellReference.ColumnIndex)
            {
                item.Max -= 1;
            }
            else
            {
                columns.AppendChild(new Column
                {
                    Min = cellReference.ColumnIndex + 1,
                    Max = item.Max,
                    BestFit = item.BestFit,
                    CustomWidth = item.CustomWidth,
                    Width = item.Width,
                    Collapsed = item.Collapsed,
                    Style = item.Style,
                });
                item.Max = cellReference.ColumnIndex - 1;
            }
        }
        columns.AppendChild(new Column
        {
            Min = cellReference.ColumnIndex,
            Max = cellReference.ColumnIndex,
            Width = 10,
            CustomWidth = false,
            BestFit = true
        });


        //after cells
        var afterCells = worksheet.GetCells(x => new CellReference(x.CellReference).ColumnIndex >= cellReference.ColumnIndex);
        foreach (var item in afterCells)
        {
            var currentReference = new CellReference(item.CellReference);
            var newReference = new CellReference(currentReference.RowIndex, currentReference.ColumnIndex + 1);
            item.CellReference = newReference.Reference;
        }

        //new cells
        if (insertColumnCells)
        {
            foreach (var item in worksheet.Descendants<Row>().Where(x => x.RowIndex is not null))
            {
                var cell = new Cell
                {
                    CellReference = new CellReference(item.RowIndex!, cellReference.ColumnIndex).Reference,
                };
                var afterCell = item.Elements<Cell>().FirstOrDefault(x => new CellReference(x.CellReference).ColumnIndex > cellReference.ColumnIndex);

                if (afterCell is not null) item.InsertBefore(cell, afterCell);
                else item.AppendChild(cell);
            }
        }
    }

    /// <summary>
    /// 删除行(如果该行有合并单元格,则会产生不可预期的效果)
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="columnName">列明,如A,B,C</param>
    public static void DeleteColumn(this Worksheet worksheet, string columnName)
    {
        var cellReference = new CellReference(columnName + 1);
        var sheetData = worksheet.Elements<SheetData>().FirstOrDefault();
        if (sheetData is null)
        {
            sheetData = new SheetData();
            worksheet.AppendChild(sheetData);
        }

        //column
        var columns = worksheet.Elements<Columns>().FirstOrDefault();
        if (columns is not null)
        {
            var columnCollection = columns.Elements<Column>().Where(x => x.Max is not null && x.Max >= cellReference.ColumnIndex).ToList();
            foreach (var item in columnCollection)
            {
                if (item.Min is null || item.Max is null) continue;
                if (item.Min > cellReference.ColumnIndex)
                {
                    item.Min -= 1;
                    item.Max -= 1;
                }
                else
                {
                    item.Max -= 1;
                }
            }
        }

        //current cells
        foreach (var item in worksheet.Descendants<Cell>().Where(x => new CellReference(x.CellReference).ColumnIndex == cellReference.ColumnIndex))
        {
            item.Remove();
        }

        //after cells
        var afterCells = worksheet.GetCells(x => new CellReference(x.CellReference).ColumnIndex > cellReference.ColumnIndex);
        foreach (var item in afterCells)
        {
            var currentReference = new CellReference(item.CellReference);
            var newReference = new CellReference(currentReference.RowIndex, currentReference.ColumnIndex - 1);
            item.CellReference = newReference.Reference;
        }
    }
    #endregion

    #region Cell
    /// <summary>
    /// 获取单元格值
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="workbookPart">工作簿部件</param>
    /// <returns>值</returns>
    public static string? GetValue(this Cell cell, WorkbookPart workbookPart)
    {
        var text = cell.CellValue?.Text;
        var dataType = cell.DataType;
        if (dataType is null) return text;
        else if (dataType == CellValues.SharedString)
        {
            var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? new List<SharedStringItem>();
            var sharedItem = sharedStringItems.ElementAt(int.Parse(text));
            return sharedItem.Text?.Text ?? string.Empty;
        }
        else
        {
            return text;
        }
    }

    /// <summary>
    /// 根据值类型设置值
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="workbookPart">工作簿部件</param>
    /// <param name="value">值</param>
    public static void SetValue(this Cell cell, WorkbookPart workbookPart, object? value)
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
            cell.CellValue = new CellValue(workbookPart.SetSharedStringItem(value.ToString()));
            cell.DataType = CellValues.SharedString;
        }
    }

    /// <summary>
    /// 合并表格
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="cellReference1">第一个单元格</param>
    /// <param name="cellReference2">第二个单元格</param>
    /// <returns>合并单元格</returns>
    public static MergeCell MergeCells(this Worksheet worksheet, string cellReference1, string cellReference2)
    {
        CreateCellIfNotExist(worksheet, cellReference1);
        CreateCellIfNotExist(worksheet, cellReference2);

        MergeCells mergeCells;
        if (worksheet.Elements<MergeCells>().Count() > 0)
        {
            mergeCells = worksheet.Elements<MergeCells>().First();
        }
        else
        {
            mergeCells = new MergeCells();

            // Insert a MergeCells object into the specified position.
            if (worksheet.Elements<CustomSheetView>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
            }
            else if (worksheet.Elements<DataConsolidate>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
            }
            else if (worksheet.Elements<SortState>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
            }
            else if (worksheet.Elements<AutoFilter>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
            }
            else if (worksheet.Elements<Scenarios>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
            }
            else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
            }
            else if (worksheet.Elements<SheetProtection>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
            }
            else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
            }
            else
            {
                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
            }
        }

        // Create the merged cell and append it to the MergeCells collection.
        var mergeCell = new MergeCell() { Reference = $"{cellReference1}:{cellReference2}".ToUpper() };
        mergeCells.AppendChild(mergeCell);

        worksheet.Save();
        return mergeCell;
    }

    /// <summary>
    /// 如果单元格不存在则创建
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="cellReference">单元格地址</param>
    /// <returns>单元格</returns>
    public static Cell CreateCellIfNotExist(this Worksheet worksheet, string cellReference)
    {
        var reference = new CellReference(cellReference);

        //cells
        var row = worksheet.Descendants<Row>().FirstOrDefault(r => r.RowIndex?.Value == reference.RowIndex);
        if (row is null)
        {
            row = new Row() { RowIndex = reference.RowIndex };
            worksheet.Descendants<SheetData>().First().Append(row);
            worksheet.Save();
        }

        var cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference?.Value == reference.Reference);
        if (cell is null)
        {
            cell = new Cell() { CellReference = reference.Reference };

            var afterCell = row.Elements<Cell>().FirstOrDefault(c => new CellReference(c.CellReference).ColumnIndex > reference.ColumnIndex);
            if (afterCell is not null) row.InsertBefore(cell, afterCell);
            else row.AppendChild(cell);
            worksheet.Save();
        }

        return cell;
    }

    static bool IsNumericDatatype(this Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
            _ => false,
        };
    }
    #endregion

    #region Style
    /// <summary>
    /// 根据单元格内容长度设置列宽
    /// </summary>
    /// <param name="doc">文档</param>
    public static void AutoColumnWidth(this SpreadsheetDocument doc)
    {
        doc.WorkbookPart?.GetPartsOfType<WorksheetPart>().ToList().ForEach(x => x.AutoColumnWidth());
        doc.Save();
    }

    /// <summary>
    /// 根据单元格内容长度设置列宽
    /// </summary>
    /// <param name="worksheetPart">工作表格配件</param>
    public static void AutoColumnWidth(this WorksheetPart worksheetPart)
    {
        var workbookPart = worksheetPart.GetParent<WorkbookPart>() ?? throw new Exception($"unable to find WorkbookPart");
        var worksheet = worksheetPart.Worksheet;

        //analysis word length
        var columnWordLength = new List<ColumnWordLength>();
        foreach (Cell cell in worksheet.Descendants<Cell>())
        {
            var cellReference = new CellReference(cell.CellReference);
            var wordLength = columnWordLength.FirstOrDefault(x => x.ColumnName == cellReference.ColumnName);
            if (wordLength is null)
            {
                wordLength = new ColumnWordLength(cellReference.ColumnIndex, cellReference.ColumnName);
                columnWordLength.Add(wordLength);
            }
            var cellValue = cell.GetValue(workbookPart);
            if (double.TryParse(cellValue, out var doubleValue))
            {
                if ((cellValue?.Split('.')?.LastOrDefault()?.Length ?? 0) > 10) cellValue = doubleValue.ToString("0.0000");//float number
            }
            wordLength.Length = Math.Max(wordLength.Length, cellValue?.Length ?? 0);
        }
        columnWordLength = columnWordLength.OrderBy(x => x.ColumnIndex).ToList();

        var columns = worksheet.Elements<Columns>().FirstOrDefault();
        if (columns is null)
        {
            columns = new Columns();
            worksheet.InsertBefore(columns, worksheet.Elements<SheetData>().FirstOrDefault());
            worksheet.Save();
        }
        var oldColumns = worksheet.Descendants<Column>().ToList();
        columnWordLength.ForEach(x =>
        {
            var old = oldColumns.FirstOrDefault(y => y.Min is not null && y.Max is not null && y.Min <= x.ColumnIndex && y.Max >= x.ColumnIndex);
            var column = new Column
            {
                Min = x.ColumnIndex,
                Max = x.ColumnIndex,
                CustomWidth = true,
                BestFit = false,
                Width = x.Length + 2,
                Collapsed = old?.Collapsed//copy some value
            };
            columns.AppendChild(column);
        });

        oldColumns.ForEach(x => x.Remove());
        worksheet.Save();
    }

    class ColumnWordLength
    {
        public ColumnWordLength(uint columnIndex, string columnName)
        {
            ColumnIndex = columnIndex;
            ColumnName = columnName;
        }

        public uint ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public int Length { get; set; }
    }

    /// <summary>
    /// 创建样式
    /// </summary>
    /// <param name="workbookPart">工作簿部件</param>
    /// <param name="style">样式</param>
    /// <returns>样式索引</returns>
    public static uint CreateStyle(this WorkbookPart workbookPart, CellStyle style)
    {
        var workbookStylesPart = workbookPart.GetPartsOfType<WorkbookStylesPart>()?.FirstOrDefault() ?? workbookPart.AddNewPart<WorkbookStylesPart>();
        workbookStylesPart.Stylesheet ??= new Stylesheet();
        workbookStylesPart.Stylesheet.Fonts ??= new Fonts();
        workbookStylesPart.Stylesheet.Fills ??= new Fills();
        workbookStylesPart.Stylesheet.Borders ??= new Borders();
        workbookStylesPart.Stylesheet.CellFormats ??= new CellFormats();

        // fonts
        var font = new Font()
        {
            Italic = style.Italic ? new Italic() : null,
            Color = new Color { Rgb = WrapColor(style.FontColor) },
            FontSize = new FontSize() { Val = style.FontSize },
            Bold = new Bold { Val = style.Bold }
        };
        workbookStylesPart.Stylesheet.Fonts.AppendChild(font);

        // fills
        var fill = new Fill
        {
            PatternFill = new PatternFill
            {
                PatternType = PatternValues.Solid,
                ForegroundColor = new ForegroundColor { Rgb = WrapColor(style.BackgroundColor) }
            }
        };
        workbookStylesPart.Stylesheet.Fills!.AppendChild(fill);

        // borders
        var border = new Border
        {
            Outline = true,
            LeftBorder = new LeftBorder { Style = style.BorderStyle, Color = new Color { Rgb = WrapColor(style.BorderColor) } },
            TopBorder = new TopBorder { Style = style.BorderStyle, Color = new Color { Rgb = WrapColor(style.BorderColor) } },
            RightBorder = new RightBorder { Style = style.BorderStyle, Color = new Color { Rgb = WrapColor(style.BorderColor) } },
            BottomBorder = new BottomBorder { Style = style.BorderStyle, Color = new Color { Rgb = WrapColor(style.BorderColor) } },
        };
        workbookStylesPart.Stylesheet.Borders!.AppendChild(border);

        //alignment
        var alignment = new Alignment
        {
            Horizontal = style.HorizontalAlignment,
            Vertical = style.VerticalAlignment,
            WrapText = new BooleanValue(style.WrapText)
        };

        // styles(style index)
        var cellFormat = new CellFormat
        {
            Alignment = alignment,
            FontId = (uint)workbookStylesPart.Stylesheet.Fonts.Elements<Font>().ToList().IndexOf(font),
            FillId = (uint)workbookStylesPart.Stylesheet.Fills.Elements<Fill>().ToList().IndexOf(fill),
            BorderId = (uint)workbookStylesPart.Stylesheet.Borders.Elements<Border>().ToList().IndexOf(border)
        };

        workbookStylesPart.Stylesheet.CellFormats.AppendChild(cellFormat);
        return (uint)(workbookStylesPart.Stylesheet.CellFormats.ToList().IndexOf(cellFormat));
    }

    /// <summary>
    /// 创建样式
    /// </summary>
    /// <param name="workbookPart">工作簿部件</param>
    /// <param name="cellFormat">单元格格式,更灵活</param>
    /// <returns>样式索引</returns>
    public static uint CreateStyle(this WorkbookPart workbookPart, CellFormat cellFormat)
    {
        var workbookStylesPart = workbookPart.GetPartsOfType<WorkbookStylesPart>()?.FirstOrDefault() ?? workbookPart.AddNewPart<WorkbookStylesPart>();
        workbookStylesPart.Stylesheet ??= new Stylesheet();
        workbookStylesPart.Stylesheet.Fonts ??= new Fonts();
        workbookStylesPart.Stylesheet.Fills ??= new Fills();
        workbookStylesPart.Stylesheet.Borders ??= new Borders();
        workbookStylesPart.Stylesheet.CellFormats ??= new CellFormats();

        workbookStylesPart.Stylesheet.CellFormats.AppendChild(cellFormat);
        return (uint)(workbookStylesPart.Stylesheet.CellFormats.ToList().IndexOf(cellFormat));
    }

    /// <summary>
    /// 合并单元格样式
    /// </summary>
    /// <param name="mergeCell">合并单元格</param>
    /// <param name="style">样式</param>
    public static void UseStyle(this MergeCell mergeCell, CellStyle style)
    {
        var workbookPart = mergeCell.GetParent<WorkbookPart>() ?? throw new Exception("unable to find WorkbookPart");
        var cells = mergeCell.GetCells();
        var styleId = workbookPart.CreateStyle(style);
        cells.ForEach(x => x.StyleIndex = styleId);
    }

    /// <summary>
    /// 获取合并单元格中的所有单元格
    /// </summary>
    /// <param name="mergeCell">合并单元格</param>
    public static List<Cell> GetCells(this MergeCell mergeCell)
    {
        var worksheet = mergeCell.GetParent<Worksheet>() ?? throw new Exception("unable to find Worksheet");
        var references = mergeCell.Reference!.Value!.Split(':');
        var refernece1 = new CellReference(references[0]);
        var refernece2 = new CellReference(references[1]);
        var cells = new List<Cell>();

        for (uint rowIndex = Math.Min(refernece1.RowIndex, refernece2.RowIndex); rowIndex <= Math.Max(refernece1.RowIndex, refernece2.RowIndex); rowIndex++)
        {
            var row = worksheet.Descendants<Row>().FirstOrDefault(x => x.RowIndex is not null && x.RowIndex == rowIndex);
            if (row is null)
            {
                row = new Row { RowIndex = rowIndex };
                worksheet.AppendChild(row);
            }

            for (uint columnIndex = Math.Min(refernece1.ColumnIndex, refernece2.ColumnIndex); columnIndex <= Math.Max(refernece1.ColumnIndex, refernece2.ColumnIndex); columnIndex++)
            {
                var cellReference = new CellReference(rowIndex, columnIndex);
                var cell = worksheet.CreateCellIfNotExist(cellReference.Reference);
                cells.Add(cell);
            }
        }
        worksheet.Save();
        return cells;
    }

    static HexBinaryValue? WrapColor(string? color)
    {
        if (color.IsNullOrWhiteSpace()) return null;
        color = color.TrimStart('#').ToUpper();
        if (color.Length == 3) return HexBinaryValue.FromString($"FF{string.Join("", color.Select(x => $"{x}{x}"))}");
        if (color.Length == 6) return HexBinaryValue.FromString($"FF{color}");
        else if (color.Length == 8)
        {
            if (color.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) return HexBinaryValue.FromString($"00{color.Substring(2)}");
            return HexBinaryValue.FromString(color);
        }
        else return HexBinaryValue.FromString(color);
    }
    #endregion
}
