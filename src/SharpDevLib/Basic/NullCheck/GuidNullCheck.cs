using System.Diagnostics.CodeAnalysis;

namespace SharpDevLib;

/// <summary>
/// Guid 空断言扩展类
/// 为 Guid 和 Guid? 类型提供便捷的空值判断扩展方法
/// </summary>
public static class GuidNullCheck
{
    /// <summary>
    /// 断言一个 Guid 是否为 '00000000-0000-0000-0000-000000000000' (Guid.Empty)
    /// </summary>
    /// <param name="guid">需要断言的 Guid</param>
    /// <returns>如果 Guid 等于 Guid.Empty 返回 true,否则返回 false</returns>
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

    /// <summary>
    /// 断言一个 Guid 是否不为 '00000000-0000-0000-0000-000000000000' (Guid.Empty)
    /// </summary>
    /// <param name="guid">需要断言的 Guid</param>
    /// <returns>如果 Guid 不等于 Guid.Empty 返回 true,否则返回 false</returns>
    public static bool NotEmpty(this Guid guid) => guid != Guid.Empty;

    /// <summary>
    /// 断言一个可空 Guid 是否为 null 或者 '00000000-0000-0000-0000-000000000000' (Guid.Empty)
    /// </summary>
    /// <param name="guid">需要断言的可空 Guid</param>
    /// <returns>如果可空 Guid 为 null 或等于 Guid.Empty 返回 true,否则返回 false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? guid) => guid is null || guid == Guid.Empty;

    /// <summary>
    /// 断言一个可空 Guid 是否不为 null 且不等于 '00000000-0000-0000-0000-000000000000' (Guid.Empty)
    /// </summary>
    /// <param name="guid">需要断言的可空 Guid</param>
    /// <returns>如果可空 Guid 不为 null 且不等于 Guid.Empty 返回 true,否则返回 false</returns>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this Guid? guid) => guid is not null && guid != Guid.Empty;
}
