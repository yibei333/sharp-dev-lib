namespace SharpDevLib;

/// <summary>
/// 包含标识符的请求对象
/// 用于需要通过标识符进行操作或查询的请求场景,如删除、更新、获取等操作
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
public class IdRequest<TId> : BaseRequest
{
    /// <summary>
    /// 示例化 IdRequest 对象
    /// </summary>
    public IdRequest()
    {
    }

    /// <summary>
    /// 示例化 IdRequest 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdRequest(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 标识符,用于唯一标识操作对象
    /// </summary>
    public TId Id { get; set; } = default!;
}