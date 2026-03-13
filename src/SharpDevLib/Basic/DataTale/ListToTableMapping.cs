using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// 列表->DataTable映射
/// </summary>
/// <param name="name">原名称</param>
public class ListToTableMapping(string name)
{
    internal static Func<string, string> InternalDefaultNameConventer => name => name;
    internal static Func<object?, object, object?> InternalValueConventer => (value, _) => value;

    /// <summary>
    /// 默认名称转换器,什么也不做
    /// </summary>
    public static Func<string, string> DefaultNameConventer { get; set; } = InternalDefaultNameConventer;

    /// <summary>
    /// 默认值转换器,什么也不做
    /// </summary>
    public static Func<object?, object, object?> DefaultValueConverter { get; set; } = InternalValueConventer;

    /// <summary>
    /// 原名
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 值转换器
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为当前值</para>
    /// <para>2.第二个参数为当前对象的值</para>
    /// <para>返回值：转换后的结果</para>
    /// </summary>
    public Func<object?, object, object?>? ValueConverter { get; set; } = DefaultValueConverter;

    /// <summary>
    /// 名称转换器
    /// <para>参数说明：</para>
    /// <para>1.第一个参数为原名</para>
    /// <para>返回值：转换后的名称</para>
    /// </summary>
    public Func<string, string>? NameConverter { get; set; } = DefaultNameConventer;

    internal PropertyInfo Property { get; set; } = null!;
    internal string ColumnName { get; set; } = null!;
}