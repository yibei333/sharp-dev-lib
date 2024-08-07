﻿using System.Data;

namespace SharpDevLib;

/// <summary>
/// 实例化DataTable转换列
/// </summary>
/// <param name="name">源DataTable中的列名,完全匹配,注意空格和*号</param>
[BelongDirectory("DataTable")]
public class DataTableTransferColumn(string name)
{
    /// <summary>
    /// 源DataTable中的列名,完全匹配,注意空格和*号
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 是否必需,如果是则在列名前面加*号
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 目标列的类型,如果不设置则和源列的类型保持一致,注意和ValueConverter返回的数据类型一致
    /// </summary>
    public Type? TargetType { get; set; }

    /// <summary>
    /// 值转换器,参数说明如下
    /// <para>1.第一个参数为源单元格的值</para>
    /// <para>2.第二个参数为DataRow</para>
    /// <para>3.第三个参数为需返回转换后的结果,注意返回的类型需要和TargetType类型一致</para>
    /// </summary>
    public Func<object, DataRow, object>? ValueConverter { get; set; }

    /// <summary>
    /// 列明转换器,参数说明如下
    /// <para>1.第一个参数为源列名</para>
    /// <para>2.第二个参数为需返回转换后的列名</para>
    /// </summary>
    public Func<string, string>? NameConverter { get; set; }
}