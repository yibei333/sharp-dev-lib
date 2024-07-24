namespace SharpDevLib;

/// <summary>
/// 响应
/// </summary>
public class EmptyReply : BaseReply
{
    /// <summary>
    /// 构建成功的响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>成功的响应</returns>
    public static EmptyReply Succeed(string? description = null) => new() { Success = true, Description = description };

    /// <summary>
    /// 构建失败的响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>失败的响应</returns>
    public static EmptyReply Failed(string? description = null) => new() { Success = false, Description = description };
}