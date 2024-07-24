namespace SharpDevLib;

/// <summary>
/// 分页响应
/// </summary>
/// <typeparam name="TData">数据类型</typeparam>
public class PageReply<TData> : BaseReply
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 每页数据条数
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 总数
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// 页数
    /// </summary>
    public long PageCount => Size <= 0 ? 0 : (long)Math.Ceiling(TotalCount * 1.0 / Size);

    /// <summary>
    /// 分页数据
    /// </summary>
    public List<TData>? Data { get; set; }

    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <param name="data">data</param>
    /// <param name="total">总数</param>
    /// <param name="index">索引</param>
    /// <param name="size">每页数据条数</param>
    /// <param name="description">描述</param>
    /// <returns>成功的分页响应</returns>
    public static PageReply<TData> Succeed(List<TData> data, long total, int index, int size, string? description = null) => new() { Success = true, Description = description, Data = data, TotalCount = total, Index = index, Size = size };

    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <param name="data">data</param>
    /// <param name="total">总数</param>
    /// <param name="request">请求</param>
    /// <param name="description">描述</param>
    /// <returns>成功的分页响应</returns>
    public static PageReply<TData> Succeed(List<TData> data, long total, PageRequest request, string? description = null) => new() { Success = true, Description = description, Data = data, TotalCount = total, Index = request.Index, Size = request.Size };

    /// <summary>
    /// 构建失败的分页响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>失败的分页响应</returns>
    public static PageReply<TData> Failed(string? description = null) => new() { Success = false, Description = description };
}
