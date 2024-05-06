﻿using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib.Standard;

/// <summary>
/// 枚举扩展
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// 将整型值转换为指定的枚举
    /// </summary>
    /// <typeparam name="TEnum">指定的枚举类型</typeparam>
    /// <param name="intValue">整型值</param>
    /// <returns>枚举</returns>
    public static TEnum ToEnum<TEnum>(this int intValue) where TEnum : Enum
    {
        var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), intValue);
        if (!Enum.IsDefined(typeof(TEnum), enumValue)) throw new ArgumentException($"value '{intValue}' not defined in type '{typeof(TEnum).FullName}'");
        return enumValue;
    }

    /// <summary>
    /// 将字符串转换为指定的枚举
    /// </summary>
    /// <typeparam name="TEnum">指定的枚举类型</typeparam>
    /// <param name="stringValue">字符串</param>
    /// <param name="ignoreCase">是否忽略大小写</param>
    /// <returns>枚举</returns>
    public static TEnum ToEnum<TEnum>(this string stringValue, bool ignoreCase=true) where TEnum : Enum
    {
        if (Enum.TryParse(typeof(TEnum), stringValue, ignoreCase, out var enumValue)) return (TEnum)enumValue;
        throw new ArgumentException($"value '{stringValue}' not defined in type '{typeof(TEnum).FullName}'");
    }

    /// <summary>
    /// 获取枚举类型的键值对集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>字典</returns>
    public static Dictionary<string, int> GetDictionary<TEnum>() where TEnum : Enum => GetKeyValues<TEnum>().ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// 获取枚举类型的键值对集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>键值对集合</returns>
    public static IEnumerable<KeyValuePair<string, int>> GetKeyValues<TEnum>() where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum));
        foreach (TEnum value in values)
        {
            yield return new KeyValuePair<string, int>(value.ToString(), Convert.ToInt32(value));
        }
    }
}
