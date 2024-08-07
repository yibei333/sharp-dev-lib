namespace SharpDevLib;

/// <summary>
/// 枚举扩展
/// </summary>
[BelongDirectory("Extensions")]
public static class EnumExtension
{
    /// <summary>
    /// 将整型值转换为指定的枚举
    /// </summary>
    /// <typeparam name="TEnum">指定的枚举类型</typeparam>
    /// <param name="intValue">整型值</param>
    /// <returns>枚举</returns>
    /// <exception cref="InvalidDataException">转换失败时引发异常</exception>
    public static TEnum ToEnum<TEnum>(this int intValue) where TEnum : struct
    {
        var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), intValue);
        if (!Enum.IsDefined(typeof(TEnum), enumValue)) throw new InvalidDataException($"value '{intValue}' not defined in type '{typeof(TEnum).FullName}'");
        return enumValue;
    }

    /// <summary>
    /// 将字符串转换为指定的枚举
    /// </summary>
    /// <typeparam name="TEnum">指定的枚举类型</typeparam>
    /// <param name="stringValue">字符串</param>
    /// <param name="ignoreCase">是否忽略大小写</param>
    /// <returns>枚举</returns>
    /// <exception cref="InvalidDataException">转换失败时引发异常</exception>
    public static TEnum ToEnum<TEnum>(this string stringValue, bool ignoreCase = true) where TEnum : struct
    {
        if (Enum.TryParse<TEnum>(stringValue, ignoreCase, out var enumValue)) return (TEnum)enumValue;
        throw new InvalidDataException($"value '{stringValue}' not defined in type '{typeof(TEnum).FullName}'");
    }

    /// <summary>
    /// 获取枚举类型的键值对集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>字典</returns>
    public static Dictionary<string, int> GetDictionary<TEnum>() where TEnum : struct => GetKeyValues<TEnum>().ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// 获取枚举类型的键值对集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>键值对集合</returns>
    public static IEnumerable<KeyValuePair<string, int>> GetKeyValues<TEnum>() where TEnum : struct
    {
        var values = Enum.GetValues(typeof(TEnum));
        foreach (TEnum value in values)
        {
            yield return new KeyValuePair<string, int>(value.ToString(), Convert.ToInt32(value));
        }
    }
}
