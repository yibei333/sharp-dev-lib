using System;
using System.Linq;

namespace SharpDevLib;

/// <summary>
/// reflection util
/// </summary>
public static class ReflectionUtil
{
    /// <summary>
    /// ensure type have a parameterless constructor
    /// </summary>
    /// <param name="type">type to check</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MissingMethodException"></exception>
    public static void EnsureTypeContainsPublicParamterLessConstructor(this Type? type)
    {
        if (type.IsNull()) throw new ArgumentNullException();
        if (type.GetConstructors().All(x => x.GetParameters().Any() && x.IsPublic)) throw new MissingMethodException($"type [{type.FullName}] must have a public parameterless constructor");
    }

    /// <summary>
    /// get a object type name
    /// </summary>
    /// <param name="obj">object value</param>
    /// <param name="isFullName">indicate result is full name</param>
    /// <returns>object type name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string GetTypeName(this object? obj, bool isFullName = true) => obj?.GetType().GetTypeName(isFullName) ?? throw new NullReferenceException();

    /// <summary>
    /// get type name
    /// </summary>
    /// <param name="type">type</param>
    /// <param name="isFullName">indicate result is full name</param>
    /// <returns>type name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string GetTypeName(this Type? type, bool isFullName = true)
    {
        if (type.IsNull()) throw new NullReferenceException();
        return isFullName ? type!.FullName : type!.Name;
    }
}
