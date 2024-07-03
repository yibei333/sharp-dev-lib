using System.Text.RegularExpressions;

namespace SharpDevLib.OpenXML;

/// <summary>
/// 单元格引用
/// </summary>
public class CellReference
{
    const string _columnExpression = "[A-Za-z]+";
    const string _rowExpression = "[0-9]+";

    /// <summary>
    /// 实例化单元格引用
    /// </summary>
    /// <param name="rowIndex">行号,以1开始</param>
    /// <param name="columnName">列明,如A,B,C</param>
    public CellReference(uint rowIndex, string columnName)
    {
        RowIndex = rowIndex;
        ColumnName = columnName;
        ColumnIndex = GetColumnIndex(columnName);
        Reference = columnName + rowIndex;
    }

    /// <summary>
    /// 实例化单元格引用
    /// </summary>
    /// <param name="rowIndex">行号,以1开始</param>
    /// <param name="columnIndex">列号,以0开始</param>
    public CellReference(uint rowIndex, uint columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;

        var prefixCount = columnIndex / 26;
        if (prefixCount >= 26) throw new NotSupportedException($"max cellreference is ZZ");
        var prefix = prefixCount > 0 ? ((char)(prefixCount + 65)).ToString() : "";
        var nameCount = columnIndex % 26;
        var name = ((char)(nameCount + 65)).ToString();
        ColumnName = prefix + name;
        Reference = ColumnName + rowIndex;
    }

    /// <summary>
    /// 实例化单元格引用
    /// </summary>
    /// <param name="reference">引用,如A1,B2</param>
    public CellReference(string? reference)
    {
        if (reference.IsNullOrWhiteSpace()) throw new ArgumentException("reference could not be null or whitespace");
        Reference = reference;

        var match = Regex.Match(reference, _columnExpression);
        if (!match.Success) throw new Exception($"{reference} is not a valid CellReference");
        ColumnName = match.Value;
        ColumnIndex = GetColumnIndex(ColumnName);
        RowIndex = uint.Parse(Regex.Match(reference, _rowExpression).Value);
    }

    uint GetColumnIndex(string columnName)
    {
        if (columnName.Length > 2) throw new NotSupportedException($"max cellreference is ZZ");
        if (columnName.Length == 2) return (uint)((columnName[0] - 65 + 1) * 26 + columnName[1] - 65);
        else return (uint)(columnName[0] - 65);
    }

    /// <summary>
    /// 行号,以1开始
    /// </summary>
    public uint RowIndex { get; }

    /// <summary>
    /// 列号,以0开始
    /// </summary>
    public uint ColumnIndex { get; }

    /// <summary>
    /// 列明,如A,B,C
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// 引用,如A1,B2
    /// </summary>
    public string Reference { get; }
}
