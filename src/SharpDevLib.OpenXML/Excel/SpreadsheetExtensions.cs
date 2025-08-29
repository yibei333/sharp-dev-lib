using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Concurrent;
using System.Text;

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

    static void AppendCell(this Row row, Cell cell)
    {
        var columnIndex = new CellReference(cell.CellReference).ColumnIndex;
        var afterCell = row.Elements<Cell>().FirstOrDefault(x => new CellReference(x.CellReference).ColumnIndex > columnIndex);
        if (afterCell is null) row.Append(cell);
        else row.InsertBefore(cell, afterCell);
    }

    static void AppendRow(this SheetData sheetData, Row row)
    {
        var afterRow = sheetData.Elements<Row>().FirstOrDefault(x => x.RowIndex > row.RowIndex);
        if (afterRow is null) sheetData.Append(row);
        else sheetData.InsertBefore(row, afterRow);
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
    /// 获取SharedStringTable
    /// </summary>
    /// <param name="workbookPart">工作簿部件</param>
    /// <returns>SharedStringTable</returns>
    public static SharedStringTable GetSharedStringTable(this WorkbookPart workbookPart)
    {
        var sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault() ?? workbookPart.AddNewPart<SharedStringTablePart>();
        sharedStringTablePart.SharedStringTable ??= new SharedStringTable();
        return sharedStringTablePart.SharedStringTable;
    }

    /// <summary>
    /// 设置SharedString
    /// </summary>
    /// <param name="sharedStringTable">SharedStringTable</param>
    /// <param name="text">字符串</param>
    /// <returns>SharedStringItem索引</returns>
    public static int SetSharedStringItem(this SharedStringTable sharedStringTable, string text)
    {
        var items = sharedStringTable.Elements<SharedStringItem>().ToArray();
        var item = items.FirstOrDefault(x => x.InnerText == text);
        var index = Array.IndexOf(items, item);
        if (index >= 0) return index;

        sharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
        sharedStringTable.Save();
        return items.Count();
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
        worksheet.Elements<SheetData>().First().AppendRow(newRow);
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
                item.AppendCell(cell);
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
    /// <param name="sharedStringItems">共享字符串集合</param>
    /// <returns>值</returns>
    public static string? GetValue(this Cell cell, IEnumerable<SharedStringItem> sharedStringItems)
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

    /// <summary>
    /// 根据值类型设置值
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="value">值</param>
    /// <param name="sharedStringTable">SharedStringTable</param>
    public static void SetValue(this Cell cell, object? value, SharedStringTable? sharedStringTable)
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
            if (sharedStringTable is null)
            {
                cell.CellValue = new CellValue(value.ToString());
                cell.DataType = CellValues.String;
            }
            else
            {
                var index = sharedStringTable.SetSharedStringItem(value.ToString());
                cell.CellValue = new CellValue(index);
                cell.DataType = CellValues.SharedString;
            }
        }
    }

    internal static void SetValue(this Cell cell, object? value, SharedStringTable sharedStringTable, Dictionary<string, int>? sharedStringCache)
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
            if (sharedStringCache is null)
            {
                cell.CellValue = new CellValue(value.ToString());
                cell.DataType = CellValues.String;
            }
            else
            {
                var text = value.ToString();
                if (!sharedStringCache.TryGetValue(text, out var index))
                {
                    index = sharedStringCache.Count;
                    sharedStringCache[text] = index;
                    sharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
                }
                cell.CellValue = new CellValue(index);
                cell.DataType = CellValues.SharedString;
            }
        }
    }

    /// <summary>
    /// 合并单元格,只保存左上角的单元格(如果值为空,则根据RowIndex,ColumnIndex排序找到一个有值的单元格赋值),其余单元格删除
    /// </summary>
    /// <param name="worksheet">工作表格</param>
    /// <param name="cellReference1">第一个单元格</param>
    /// <param name="cellReference2">第二个单元格</param>
    /// <returns>合并单元格</returns>
    public static MergeCell MergeCells(this Worksheet worksheet, string cellReference1, string cellReference2)
    {
        var workbookPart = worksheet.GetParent<WorkbookPart>() ?? throw new Exception("WorkbookPart not found");
        var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? [];
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

        //cells
        var cells = mergeCell.GetCells();
        var leftTopCellReference = new CellReference(Math.Min(new CellReference(cellReference1).RowIndex, new CellReference(cellReference2).RowIndex), Math.Min(new CellReference(cellReference1).ColumnIndex, new CellReference(cellReference2).ColumnIndex));
        var leftTopCell = worksheet.Descendants<Cell>().FirstOrDefault(x => x.CellReference == leftTopCellReference.Reference);
        if (leftTopCell is null)
        {
            var row = worksheet.Descendants<Row>().FirstOrDefault(x => x.RowIndex is not null && x.RowIndex == leftTopCellReference.RowIndex);
            if (row is null)
            {
                row = new Row { RowIndex = leftTopCellReference.RowIndex };
                worksheet.Elements<SheetData>().First().AppendRow(row);
            }
            leftTopCell = new Cell { CellReference = leftTopCellReference.Reference };
            row.AppendCell(leftTopCell);
        }
        var mainValueCell = cells.Where(x => x.GetValue(sharedStringItems).NotNullOrWhiteSpace()).OrderBy(x => new CellReference(x.CellReference).RowIndex).ThenBy(x => new CellReference(x.CellReference).ColumnIndex).FirstOrDefault();
        if (mainValueCell is not null)
        {
            //copy value to leftTopCell
            leftTopCell.DataType = mainValueCell.DataType;
            leftTopCell.CellValue = new CellValue(mainValueCell.CellValue!.Text);
        }
        cells.Where(x => x != leftTopCell).ToList().ForEach(x => x.Remove());

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

        var row = worksheet.Descendants<Row>().FirstOrDefault(r => r.RowIndex?.Value == reference.RowIndex);
        if (row is null)
        {
            row = new Row() { RowIndex = reference.RowIndex };
            worksheet.Descendants<SheetData>().First().AppendRow(row);
            worksheet.Save();
        }

        var cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference?.Value == reference.Reference);
        if (cell is null)
        {
            cell = new Cell() { CellReference = reference.Reference };
            row.AppendCell(cell);
            worksheet.Save();
        }

        return cell;
    }

    static readonly ConcurrentDictionary<Type, bool> _numericDataTypesCache = [];
    static readonly List<TypeCode> _numericDataTypeCodes = [TypeCode.Byte, TypeCode.SByte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Decimal, TypeCode.Double, TypeCode.Single];
    static bool IsNumericDatatype(this Type type)
    {
        if (!_numericDataTypesCache.TryGetValue(type, out var result))
        {
            result = _numericDataTypeCodes.Contains(Type.GetTypeCode(type));
            _numericDataTypesCache.TryAdd(type, result);
        }
        return result;
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
        var sharedStringItems = workbookPart.GetPartsOfType<SharedStringTablePart>()?.FirstOrDefault()?.SharedStringTable?.Elements<SharedStringItem>()?.ToList() ?? [];
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
            var cellValue = cell.GetValue(sharedStringItems);
            if (double.TryParse(cellValue, out var doubleValue))
            {
                if ((cellValue?.Split('.')?.LastOrDefault()?.Length ?? 0) > 10) cellValue = doubleValue.ToString("0.0000");//float number
            }
            wordLength.Length = Math.Max(wordLength.Length, cellValue?.Length ?? 0);
        }
        columnWordLength = [.. columnWordLength.OrderBy(x => x.ColumnIndex)];

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

    class ColumnWordLength(uint columnIndex, string columnName)
    {
        public uint ColumnIndex { get; set; } = columnIndex;
        public string ColumnName { get; set; } = columnName;
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
                var cell = worksheet.Descendants<Cell>().FirstOrDefault(x => x.CellReference == cellReference.Reference);
                if (cell is not null) cells.Add(cell);
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

    #region Comments
    /// <summary>
    /// 设置单元格批注
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="author">批注者</param>
    /// <param name="comment">批注</param>
    public static void SetComment(this Cell cell, string author, string comment)
    {
        var workbookPart = cell.GetParent<WorkbookPart>() ?? throw new Exception("WorkbookPart not found");
        var worksheetPart = cell.GetParent<WorksheetPart>() ?? throw new Exception("WorksheetPart not found");
        var reference = new CellReference(cell.CellReference);
        var worksheetCommentsPart = worksheetPart.WorksheetCommentsPart ?? worksheetPart.AddNewPart<WorksheetCommentsPart>();
        worksheetCommentsPart.Comments ??= new Comments();
        worksheetCommentsPart.Comments.Authors ??= new Authors();
        worksheetCommentsPart.Comments.CommentList ??= new CommentList();
        var authorNode = worksheetCommentsPart.Comments.Authors.Elements<Author>().FirstOrDefault(x => x.Text == author);
        if (authorNode is null)
        {
            authorNode = new Author(author);
            worksheetCommentsPart.Comments.Authors.AppendChild(authorNode);
        }
        var authorId = (uint)worksheetCommentsPart.Comments.Authors.Elements().ToList().IndexOf(authorNode);
        var commentNode = new Comment(new CommentText(new Run(new Text(comment)))) { Reference = cell.CellReference, AuthorId = authorId };
        worksheetCommentsPart.Comments.CommentList.AppendChild(commentNode);

        var shapeTemplate = "<v:shape type=\"#_x0000_t202\" style='position:absolute;margin-left:78pt;margin-top:1.2pt;width:100.8pt;height:56.4pt;z-index:1;visibility:hidden' fillcolor=\"infoBackground [80]\" strokecolor=\"none [81]\" o:insetmode=\"auto\"><v:fill color2=\"infoBackground [80]\"/><v:shadow color=\"none [81]\" obscured=\"t\"/><v:path o:connecttype=\"none\"/><v:textbox style='mso-direction-alt:auto'></v:textbox><x:ClientData ObjectType=\"Note\"><x:MoveWithCells/><x:SizeWithCells/><x:Anchor>1, 12, 0, 1, 3, 18, 4, 3</x:Anchor><x:AutoFill>False</x:AutoFill><x:Row>{0}</x:Row><x:Column>{1}</x:Column></x:ClientData></v:shape>";
        var shapes = string.Format(shapeTemplate, reference.RowIndex - 1, reference.ColumnIndex - 1);
        var vmlStyleText = $"<xml xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"><o:shapelayout v:ext=\"edit\"><o:idmap v:ext=\"edit\" data=\"1\"/></o:shapelayout><v:shapetype id=\"_x0000_t202\" coordsize=\"21600,21600\" o:spt=\"202\" path=\"m,l,21600r21600,l21600,xe\"><v:stroke joinstyle=\"miter\"/><v:path gradientshapeok=\"t\" o:connecttype=\"rect\"/></v:shapetype>{shapes}</xml>";

        var vmlDrawingPart = worksheetPart.GetPartsOfType<VmlDrawingPart>().FirstOrDefault() ?? worksheetPart.AddNewPart<VmlDrawingPart>();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(vmlStyleText));
        vmlDrawingPart.FeedData(stream);
        worksheetPart.Worksheet.AppendChild(new LegacyDrawing() { Id = worksheetPart.GetIdOfPart(vmlDrawingPart) });
        workbookPart.Workbook.Save();
    }
    #endregion

    #region Image
    /// <summary>
    /// 添加背景
    /// </summary>
    /// <param name="worksheetPart">工作表格部件</param>
    /// <param name="background">背景图文件流</param>
    /// <param name="contentType">content type</param>
    public static void AddBackground(this WorksheetPart worksheetPart, Stream background, string contentType = "image/png")
    {
        var imagePart = worksheetPart.ImageParts.FirstOrDefault();
        imagePart ??= worksheetPart.AddNewPart<ImagePart>(contentType);
        var imageId = worksheetPart.GetIdOfPart(imagePart);
        imagePart.FeedData(background);
        var picture = new Picture { Id = imageId };
        worksheetPart.Worksheet.Append(picture);
        worksheetPart.Worksheet.Save();
    }

    /// <summary>
    /// 删除背景
    /// </summary>
    /// <param name="worksheetPart">工作表格部件</param>
    public static void RemoveBackground(this WorksheetPart worksheetPart)
    {
        worksheetPart.ImageParts.ToList().ForEach(x =>
        {
            worksheetPart.DeletePart(x);
        });
        worksheetPart.Worksheet.Elements<Picture>().ToList().ForEach(x =>
        {
            x.Remove();
        });
        worksheetPart.Worksheet.Save();
    }
    #endregion
}
