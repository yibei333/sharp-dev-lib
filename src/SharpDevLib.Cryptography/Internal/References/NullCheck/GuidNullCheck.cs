using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// Guid空断言扩展
/// </summary>
internal static class GuidNullCheck
{
    /// <summary>
    /// 断言一个guid是否为'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否为'00000000-0000-0000-0000-000000000000'</returns>
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

    /// <summary>
    /// 断言一个guid是否不为'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否不为'00000000-0000-0000-0000-000000000000'</returns>
    public static bool NotEmpty(this Guid guid) => guid != Guid.Empty;

    /// <summary>
    /// 断言一个guid是否为null或者'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否为null或者'00000000-0000-0000-0000-000000000000'</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? guid) => guid is null || guid == Guid.Empty;

    /// <summary>
    /// 断言一个guid是否不为null且不等于'00000000-0000-0000-0000-000000000000'
    /// </summary>
    /// <param name="guid">需要断言的guid</param>
    /// <returns>guid是否不为null且不等于'00000000-0000-0000-0000-000000000000'</returns>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this Guid? guid) => guid is not null && guid != Guid.Empty;
}
