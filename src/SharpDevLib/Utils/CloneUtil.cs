using System;

namespace SharpDevLib;

/// <summary>
/// clone util
/// </summary>
public static class CloneUtil
{
    /// <summary>
    /// deep clone a object
    /// </summary>
    /// <typeparam name="T">the type of object to clone</typeparam>
    /// <param name="source"></param>
    /// <returns>deep clone object</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T DeepClone<T>(this T? source) where T : class
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));
        return source.Serialize().DeSerialize<T>();
    }
}
