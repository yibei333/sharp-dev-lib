﻿namespace SharpDevLib;

/// <summary>
/// 反射扩展
/// </summary>
internal static class ReflectionExtension
{
    /// <summary>
    /// 获取类型名称(支持泛型)
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="isFullName">是否全名</param>
    /// <returns>名称</returns>
    public static string GetTypeDefinitionName(this Type type, bool isFullName = false)
    {
        if (!type.IsGenericType) return isFullName ? type.FullName ?? type.Name : type.Name;

        var names = new List<string>();
        foreach (var item in type.GetGenericArguments())
        {
            names.Add(GetTypeDefinitionName(item, isFullName));
        };
        var typeName = isFullName ? type.FullName ?? type.Name : type.Name;
        return $"{typeName.Split('`')[0]}<{string.Join(",", names)}>";
    }

    /// <summary>
    /// 获取对象类型名称(支持泛型)
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="isFullName">是否全名</param>
    /// <returns>名称</returns>
    public static string GetTypeDefinitionName(this object obj, bool isFullName = false) => obj?.GetType()?.GetTypeDefinitionName(isFullName) ?? string.Empty;
}
