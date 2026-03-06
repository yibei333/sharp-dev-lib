using DocumentFormat.OpenXml.Spreadsheet;

namespace SharpDevLib;

/// <summary>
/// Excel 单元格格式类
/// 用于定义 Excel 单元格的字体、颜色、对齐方式、边框等样式属性
/// </summary>
public class CellStyle
{
    /// <summary>
    /// 是否使用粗体字体
    /// </summary>
    public bool Bold { get; set; }

    /// <summary>
    /// 字体大小,默认为 11
    /// </summary>
    public uint FontSize { get; set; } = 11;

    /// <summary>
    /// 字体颜色,使用十六进制颜色值,默认为 #000000 (黑色)
    /// </summary>
    public string FontColor { get; set; } = "#000000";

    /// <summary>
    /// 背景颜色,使用十六进制颜色值,默认为 #FFFFFF (白色)
    /// </summary>
    public string BackgroundColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// 水平对齐方式,默认为左对齐
    /// </summary>
    public HorizontalAlignmentValues HorizontalAlignment { get; set; } = HorizontalAlignmentValues.Left;

    /// <summary>
    /// 垂直对齐方式,默认为居中
    /// </summary>
    public VerticalAlignmentValues VerticalAlignment { get; set; } = VerticalAlignmentValues.Center;

    /// <summary>
    /// 是否自动换行
    /// </summary>
    public bool WrapText { get; set; }

    /// <summary>
    /// 边框样式,默认为细边框
    /// </summary>
    public BorderStyleValues BorderStyle { get; set; } = BorderStyleValues.Thin;

    /// <summary>
    /// 边框颜色,使用十六进制颜色值,默认为 #DDD (浅灰色)
    /// </summary>
    public string BorderColor { get; set; } = "#DDD";

    /// <summary>
    /// 是否使用斜体字体
    /// </summary>
    public bool Italic { get; set; }
}