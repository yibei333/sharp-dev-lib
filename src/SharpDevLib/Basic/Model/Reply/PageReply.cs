namespace SharpDevLib;

/// <summary>
/// 分页响应对象
/// 继承自 BaseReply,在基础响应字段上增加了分页相关字段和数据列表,适用于分页查询场景
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class PageReply<TData> : BaseReply
{
    /// <summary>
    /// 当前页索引,从 0 开始
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页数据条数
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// 总页数,根据 TotalCount 和 Size 自动计算
    /// </summary>
    public long PageCount => Size <= 0 ? 0 : (long)Math.Ceiling(TotalCount * 1.0 / Size);

    /// <summary>
    /// 当前页的数据列表
    /// </summary>
    public List<TData>? Data { get; set; }
}

/// <summary>
/// 分页响应的静态工厂类,提供便捷的分页响应构建方法
/// </summary>
public static class PageReply
{
    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <param name="data">当前页的数据列表</param>
    /// <param name="total">总记录数</param>
    /// <param name="index">当前页索引,从 0 开始</param>
    /// <param name="size">每页数据条数</param>
    /// <param name="description">可选的成功描述信息</param>
    /// <returns>Success 为 true 的分页响应对象</returns>
    public static PageReply<TData> Succeed<TData>(List<TData> data, long total, int index, int size, string? description = null) => new() { Success = true, Description = description, Data = data, TotalCount = total, Index = index, Size = size };

    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <param name="data">当前页的数据列表</param>
    /// <param name="total">总记录数</param>
    /// <param name="request">分页请求对象,包含索引和每页条数</param>
    /// <param name="description">可选的成功描述信息</param>
    /// <returns>Success 为 true 的分页响应对象</returns>
    public static PageReply<TData> Succeed<TData>(List<TData> data, long total, PageRequest request, string? description = null) => Succeed(data, total, request.Index, request.Size, description);

    /// <summary>
    /// 构建失败的分页响应
    /// </summary>
    /// <typeparam name="TData">数据类型</typeparam>
    /// <param name="description">可选的失败描述信息</param>
    /// <returns>Success 为 false 的分页响应对象</returns>
    public static PageReply<TData> Failed<TData>(string? description = null) => new() { Success = false, Description = description };
}
