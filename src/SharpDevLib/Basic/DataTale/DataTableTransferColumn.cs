using System.Data;

namespace SharpDevLib;

/// <summary>
/// DataTable列转换配置类，用于定义DataTable转换过程中的列映射规则
/// </summary>
/// <param name="name">源DataTable中的列名，完全匹配，注意空格和*号</param>
public class DataTableTransferColumn(string name)
{
    /// <summary>
    /// 源DataTable中的列名，完全匹配，注意空格和*号
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 是否为必需列，如果为true则在目标列名前面加*号标记
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 目标列的数据类型，如果不设置则与源列的类型保持一致
    /// <para>注意：返回的数据类型需要与TargetType类型一致</para>
    /// </summary>
    public Type? TargetType { get; set; }

    /// <summary>
    /// 值转换器委托，用于自定义值的转换逻辑
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为源单元格的值</para>
    /// <para>2.第二个参数为当前DataRow对象</para>
    /// <para>返回值：转换后的结果，返回的类型需要与TargetType类型一致</para>
    /// </summary>
    public Func<object, DataRow, object>? ValueConverter { get; set; }

    /// <summary>
    /// 列名转换器委托，用于自定义列名的转换逻辑
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为源列名</para>
    /// <para>返回值：转换后的列名</para>
    /// </summary>
    public Func<string, string>? NameConverter { get; set; }
}