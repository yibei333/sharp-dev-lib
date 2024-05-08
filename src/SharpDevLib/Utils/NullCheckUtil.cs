using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// null check util
/// </summary>
public static class NullCheckUtil
{
    #region object
    /// <summary>
    /// check a object is null reference
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>indicate the object is null reference</returns>
    public static bool IsNull([NotNullWhen(false)] this object? obj) => obj == null;

    /// <summary>
    /// check a object is not null reference
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>indicate the object is not null reference</returns>
    public static bool NotNull([NotNullWhen(true)] this object? obj) => !IsNull(obj);

    /// <summary>
    /// check a object is dbnull type
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>indicate the object is dbnull type</returns>
    public static bool IsDbNull(this object? obj) => obj?.GetType() == typeof(DBNull);

    /// <summary>
    /// check a object is not dbnull type
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>indicate the object is not dbnull type</returns>
    public static bool NotDbNull(this object? obj) => !obj.IsDbNull();
    #endregion

    #region string
    /// <summary>
    /// check a string is null or white space
    /// </summary>
    /// <param name="str">string value to check</param>
    /// <returns>indicate the string is null or white space</returns>
    public static bool IsNull([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// check a string is not null and not white space
    /// </summary>
    /// <param name="str">string value to check</param>
    /// <returns>indicate the string is not null and not white space</returns>
    public static bool NotNull([NotNullWhen(true)] this string? str) => !IsNull(str);
    #endregion

    #region struct
    /// <summary>
    /// check a nullable struct type value is null
    /// </summary>
    /// <typeparam name="T">struct type</typeparam>
    /// <param name="obj">struct value</param>
    /// <returns>indicate the nullable struct type value is null</returns>
    public static bool IsNull<T>([NotNullWhen(false)] this T? obj) where T : struct => !obj.NotNull();

    /// <summary>
    /// check a nullable struct type value is not null
    /// </summary>
    /// <typeparam name="T">struct type</typeparam>
    /// <param name="obj">struct value</param>
    /// <returns>indicate the nullable struct type value is not null</returns>
    public static bool NotNull<T>([NotNullWhen(true)] this T? obj) where T : struct => obj.HasValue;
    #endregion

    #region guid
    /// <summary>
    /// check guid is empty
    /// </summary>
    /// <param name="guid">guid to check</param>
    /// <returns>indicate guid is empty</returns>
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

    /// <summary>
    /// check guid is not empty
    /// </summary>
    /// <param name="guid">guid to check</param>
    /// <returns>indicate guid is not empty</returns>
    public static bool NotEmpty(this Guid guid) => !IsEmpty(guid);

    /// <summary>
    /// check nullable guid is null or is empty
    /// </summary>
    /// <param name="guid">guid to check</param>
    /// <returns>indicate nullable guid is null or is empty</returns>
    public static bool IsEmpty([NotNullWhen(false)] this Guid? guid) => guid.IsNull() || guid == Guid.Empty;

    /// <summary>
    /// check nullable guid is not null and is not empty
    /// </summary>
    /// <param name="guid">guid to check</param>
    /// <returns>indicate nullable guid is not null and is not empty</returns>
    public static bool NotEmpty([NotNullWhen(true)] this Guid? guid) => !IsEmpty(guid);
    #endregion

    #region enumerable
    /// <summary>
    /// check a nullable enumerable collection is null or has no elements
    /// </summary>
    /// <typeparam name="T">enumerable type</typeparam>
    /// <param name="source">source to check</param>
    /// <returns>indicate a nullable enumerable collection is null or has no elements</returns>
    public static bool IsEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? source) => source == null || !source.Any();

    /// <summary>
    /// check a nullable enumerable collection is not null and has elements
    /// </summary>
    /// <typeparam name="T">enumerable type</typeparam>
    /// <param name="source">source to check</param>
    /// <returns>indicate a nullable enumerable collection is not null and has elements</returns>
    public static bool NotEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? source) => !IsEmpty(source);
    #endregion

    #region dictionary
    /// <summary>
    /// check a dictionary is null or has no elements
    /// </summary>
    /// <typeparam name="TKey">dictionary key type</typeparam>
    /// <typeparam name="TValue">dictionary value type</typeparam>
    /// <param name="source">source to check</param>
    /// <returns>indicate a dictionary is null or has no elements</returns>
    public static bool IsEmpty<TKey, TValue>([NotNullWhen(false)] this IDictionary<TKey, TValue>? source) => source == null || !source.Any();

    /// <summary>
    /// check a dictionary is not null and has elements
    /// </summary>
    /// <typeparam name="TKey">dictionary key type</typeparam>
    /// <typeparam name="TValue">dictionary value type</typeparam>
    /// <param name="source">source to check</param>
    /// <returns>indicate a dictionary is not null and has elements</returns>
    public static bool NotEmpty<TKey, TValue>([NotNullWhen(true)] this IDictionary<TKey, TValue>? source) => !IsEmpty(source);
    #endregion
}
