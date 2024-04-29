using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib;

/// <summary>
/// enum util
/// </summary>
public static class EnumUtil
{
    /// <summary>
    /// check enum is defined
    /// </summary>
    /// <typeparam name="TEnum">enum type</typeparam>
    /// <param name="enumValue">enum value</param>
    /// <returns>enum is definded</returns>
    public static bool IsEnum<TEnum>(this Enum? enumValue) where TEnum : Enum => !enumValue.IsNull() && Enum.IsDefined(typeof(TEnum), enumValue);

    /// <summary>
    /// get string value from enum
    /// </summary>
    /// <param name="enumValue">enum value</param>
    /// <returns>string value</returns>
    public static string GetString(this Enum? enumValue)
    {
        if (enumValue.IsNull()) return string.Empty;
        if (!Enum.IsDefined(enumValue.GetType(), enumValue)) return string.Empty;
        return enumValue.ToString();
    }

    /// <summary>
    /// get int value from enum
    /// </summary>
    /// <param name="enumValue">enum value</param>
    /// <returns>int value</returns>
    /// <exception cref="ArgumentException">if enumValue is not defined</exception>
    public static int GetInt(this Enum? enumValue)
    {
        if (enumValue.IsNull()) throw new ArgumentNullException(nameof(enumValue));
        if (!Enum.IsDefined(enumValue.GetType(), enumValue)) throw new ArgumentException();
        return Convert.ToInt32(enumValue);
    }

    /// <summary>
    /// get dictionary from enum
    /// </summary>
    /// <typeparam name="TEnum">enum type</typeparam>
    /// <returns>dictionary</returns>
    public static Dictionary<string, int> GetDictionary<TEnum>() where TEnum : Enum => GetKeyValues<TEnum>().ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// get key value collection from enum
    /// </summary>
    /// <typeparam name="TEnum">enum type</typeparam>
    /// <returns>key value collection</returns>
    public static IEnumerable<KeyValuePair<string, int>> GetKeyValues<TEnum>() where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum));
        foreach (TEnum value in values)
        {
            yield return new KeyValuePair<string, int>(value.GetString(), value.GetInt());
        }
    }
}
