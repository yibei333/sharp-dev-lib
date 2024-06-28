namespace SharpDevLib.OpenXML;

/// <summary>
/// DataTable转换列
/// </summary>
public class DataTableTransferColumn
{
    /// <summary>
    /// 实例化DataTable转换列
    /// </summary>
    /// <param name="name">源DataTable中的列名,完全匹配,注意空格和*号</param>
    public DataTableTransferColumn(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 源DataTable中的列名,完全匹配,注意空格和*号
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否必需,如果是则在列名签名加*号
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 目标列的类型,如果不设置则和源列的类型保持一致,注意和ValueConverter返回的数据类型一致
    /// </summary>
    public Type? TargetType { get; set; }

    /// <summary>
    /// 值转换器,第一个参数为源单元格的值,需返回转换后的结果,注意返回的类型需要和TargetType类型一致
    /// </summary>
    public Func<object, object>? ValueConverter { get; set; }

    /// <summary>
    /// 列明转换器,第一个参数为源列名,需返回转换后的列名
    /// </summary>
    public Func<string, string>? NameConverter { get; set; }
}