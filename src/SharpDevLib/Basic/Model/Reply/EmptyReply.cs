namespace SharpDevLib;

/// <summary>
/// 空响应对象
/// 继承自 BaseReply,用于仅返回操作结果而不包含具体数据的场景
/// 适用于删除、更新等不返回数据的操作
/// </summary>
public class EmptyReply : BaseReply
{
    /// <summary>
    /// 构建成功的空响应
    /// </summary>
    /// <param name="description">可选的成功描述信息</param>
    /// <returns>Success 为 true 的空响应对象</returns>
    public static EmptyReply Succeed(string? description = null) => new() { Success = true, Message = description };

    /// <summary>
    /// 构建失败的空响应
    /// </summary>
    /// <param name="description">可选的失败描述信息</param>
    /// <returns>Success 为 false 的空响应对象</returns>
    public static EmptyReply Failed(string? description = null) => new() { Success = false, Message = description };
}