using System.Data;
using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// DataTable->列表映射
/// </summary>
/// <param name="name">原名称</param>
public class TableToListMapping(string name)
{
    internal static Func<string, string> InternalDefaultNameConventer => name => name;

    internal static Func<object?, DataRow, object?> InternalDefaultValueConverter => (value, _) => value;

    /// <summary>
    /// 默认名称转换器,什么也不做
    /// </summary>
    public static Func<string, string> DefaultNameConventer { get; set; } = InternalDefaultNameConventer;

    /// <summary>
    /// 默认值转换器,什么也不做
    /// </summary>
    public static Func<object?, DataRow, object?> DefaultValueConverter { get; set; } = InternalDefaultValueConverter;

    /// <summary>
    /// 原名
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 值转换器
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为单元格的值</para>
    /// <para>2.第二个参数为当前DataRow对象</para>
    /// <para>返回值：转换后的结果</para>
    /// </summary>
    public Func<object?, DataRow, object?>? ValueConverter { get; set; } = DefaultValueConverter;

    /// <summary>
    /// 列名转换器
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为原名</para>
    /// <para>返回值：转换后的列名</para>
    /// </summary>
    public Func<string, string>? NameConverter { get; set; } = DefaultNameConventer;

    internal PropertyInfo Property { get; set; } = null!;
}