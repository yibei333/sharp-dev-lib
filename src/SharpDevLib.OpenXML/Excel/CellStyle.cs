using DocumentFormat.OpenXml.Spreadsheet;

namespace SharpDevLib.OpenXML;

/// <summary>
/// 单元格格式
/// </summary>
public class CellStyle
{
    /// <summary>
    /// 是否粗体
    /// </summary>
    public bool Bold { get; set; }

    /// <summary>
    /// 字体大小,默认11
    /// </summary>
    public uint FontSize { get; set; } = 11;

    /// <summary>
    /// 字体颜色,默认#000000
    /// </summary>
    public string FontColor { get; set; } = "#000000";

    /// <summary>
    /// 背景颜色
    /// </summary>
    public string BackgroundColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// 水平对齐,默认为左对齐
    /// </summary>
    public HorizontalAlignmentValues HorizontalAlignment { get; set; } = HorizontalAlignmentValues.Left;

    /// <summary>
    /// 垂直对其,默认居中
    /// </summary>
    public VerticalAlignmentValues VerticalAlignment { get; set; } = VerticalAlignmentValues.Center;

    /// <summary>
    /// 是否自动换行
    /// </summary>
    public bool WrapText { get; set; }

    /// <summary>
    /// 边框格式
    /// </summary>
    public BorderStyleValues BorderStyle { get; set; } = BorderStyleValues.None;

    /// <summary>
    /// 边框颜色,默认为#000000
    /// </summary>
    public string BorderColor { get; set; } = "#000000";

    /// <summary>
    /// 是否斜体
    /// </summary>
    public bool Italic { get; set; }
}