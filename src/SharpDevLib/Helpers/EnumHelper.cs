namespace SharpDevLib;

/// <summary>
/// 枚举扩展，提供枚举类型转换和获取功能
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// 将整型值转换为指定的枚举类型
    /// </summary>
    /// <typeparam name="TEnum">目标枚举类型</typeparam>
    /// <param name="intValue">要转换的整型值</param>
    /// <returns>转换后的枚举值</returns>
    /// <exception cref="InvalidDataException">当整型值在目标枚举类型中未定义时引发异常</exception>
    public static TEnum ToEnum<TEnum>(this int intValue) where TEnum : struct
    {
        var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), intValue);
        if (!Enum.IsDefined(typeof(TEnum), enumValue)) throw new InvalidDataException($"value '{intValue}' not defined in type '{typeof(TEnum).FullName}'");
        return enumValue;
    }

    /// <summary>
    /// 将字符串转换为指定的枚举类型
    /// </summary>
    /// <typeparam name="TEnum">目标枚举类型</typeparam>
    /// <param name="stringValue">要转换的字符串</param>
    /// <param name="ignoreCase">是否忽略大小写，默认为true</param>
    /// <returns>转换后的枚举值</returns>
    /// <exception cref="InvalidDataException">当字符串在目标枚举类型中未定义时引发异常</exception>
    public static TEnum ToEnum<TEnum>(this string stringValue, bool ignoreCase = true) where TEnum : struct
    {
        if (Enum.TryParse<TEnum>(stringValue, ignoreCase, out var enumValue)) return (TEnum)enumValue;
        throw new InvalidDataException($"value '{stringValue}' not defined in type '{typeof(TEnum).FullName}'");
    }

    /// <summary>
    /// 获取枚举类型的键值对字典
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>枚举名称与值的字典</returns>
    public static Dictionary<string, int> GetDictionary<TEnum>() where TEnum : struct => GetKeyValues<TEnum>().ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// 获取枚举类型的键值对集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>枚举名称与值的键值对集合</returns>
    public static IEnumerable<KeyValuePair<string, int>> GetKeyValues<TEnum>() where TEnum : struct
    {
        var values = Enum.GetValues(typeof(TEnum));
        foreach (TEnum value in values)
        {
            yield return new KeyValuePair<string, int>(value.ToString(), Convert.ToInt32(value));
        }
    }
}
