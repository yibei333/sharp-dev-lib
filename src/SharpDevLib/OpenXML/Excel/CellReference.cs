using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace SharpDevLib;

/// <summary>
/// Excel 单元格引用类
/// 用于表示 Excel 中的单元格位置,支持通过行号、列号或单元格引用字符串创建
/// </summary>
public class CellReference
{
    const string _columnExpression = "[A-Za-z]+";
    const string _rowExpression = "[0-9]+";
    static readonly ConcurrentDictionary<uint, string> _columnNameCache = [];

    /// <summary>
    /// 实例化单元格引用对象
    /// </summary>
    /// <param name="rowIndex">行号,从 1 开始</param>
    /// <param name="columnName">列名,如 A、B、C</param>
    public CellReference(uint rowIndex, string columnName)
    {
        RowIndex = rowIndex;
        ColumnName = columnName.ToUpper();
        ColumnIndex = GetColumnIndex(ColumnName);
        Reference = ColumnName + rowIndex;
    }

    /// <summary>
    /// 实例化单元格引用对象
    /// </summary>
    /// <param name="rowIndex">行号,从 1 开始</param>
    /// <param name="columnIndex">列号,从 1 开始</param>
    /// <exception cref="NotSupportedException">当列号超过 ZZ (最大支持到 702) 时引发异常</exception>
    public CellReference(uint rowIndex, uint columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;

        if (!_columnNameCache.TryGetValue(columnIndex, out var columnName))
        {
            var prefixCount = columnIndex / 26;
            if (prefixCount >= 26) throw new NotSupportedException($"max cellreference is ZZ");
            var prefix = prefixCount > 0 ? ((char)(prefixCount + 65 - 1)).ToString() : "";
            var nameCount = columnIndex % 26;
            var name = ((char)(nameCount + 65 - 1)).ToString();
            columnName = prefix + name;
            _columnNameCache.TryAdd(columnIndex, columnName);
        }

        ColumnName = columnName;
        Reference = ColumnName + rowIndex;
    }

    /// <summary>
    /// 实例化单元格引用对象
    /// </summary>
    /// <param name="reference">单元格引用字符串,如 A1、B2</param>
    /// <exception cref="ArgumentNullException">当参数 reference 为 null 或空白字符时引发异常</exception>
    /// <exception cref="Exception">当参数 reference 不合法时引发异常</exception>
    public CellReference(string? reference)
    {
        if (reference.IsNullOrWhiteSpace()) throw new ArgumentNullException("reference could not be null or whitespace");
        Reference = reference.ToUpper();

        var match = Regex.Match(Reference, _columnExpression);
        if (!match.Success) throw new Exception($"{Reference}不是有效的单元格引用");
        ColumnName = match.Value;
        ColumnIndex = GetColumnIndex(ColumnName);
        RowIndex = uint.Parse(Regex.Match(Reference, _rowExpression).Value);
    }

    uint GetColumnIndex(string columnName)
    {
        if (columnName.Length > 2) throw new NotSupportedException($"max cellreference is ZZ");
        if (columnName.Length == 2) return (uint)((columnName[0] - 65 + 1) * 26 + columnName[1] - 65) + 1;
        else return (uint)(columnName[0] - 65) + 1;
    }

    /// <summary>
    /// 行号,从 1 开始
    /// </summary>
    public uint RowIndex { get; }

    /// <summary>
    /// 列号,从 1 开始
    /// </summary>
    public uint ColumnIndex { get; }

    /// <summary>
    /// 列名,如 A、B、C
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// 单元格引用,如 A1、B2
    /// </summary>
    public string Reference { get; }
}
