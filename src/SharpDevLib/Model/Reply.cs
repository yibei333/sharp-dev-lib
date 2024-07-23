namespace SharpDevLib;

/// <summary>
/// 响应
/// </summary>
public class Reply
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 额外字段
    /// </summary>
    public object? ExtraData { get; set; }

    /// <summary>
    /// 构建成功的响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>成功的响应</returns>
    public static Reply Succeed(string? description = null) => new() { Success = true, Description = description };

    /// <summary>
    /// 构建失败的响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>失败的响应</returns>
    public static Reply Failed(string? description = null) => new() { Success = false, Description = description };

    /// <summary>
    /// 构建成功的响应
    /// </summary>
    /// <typeparam name="TData">data类型</typeparam>
    /// <param name="data">data</param>
    /// <param name="description">描述</param>
    /// <returns>成功的响应</returns>
    public static Reply<TData> Succeed<TData>(TData data, string? description = null) => new() { Success = true, Description = description, Data = data };

    /// <summary>
    /// 构建失败的响应
    /// </summary>
    /// <typeparam name="TData">data类型</typeparam>
    /// <param name="description">描述</param>
    /// <returns>失败的响应</returns>
    public static Reply<TData> Failed<TData>(string? description = null) => new() { Success = false, Description = description };

    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <typeparam name="TData">data类型</typeparam>
    /// <param name="data">data</param>
    /// <param name="total">总数</param>
    /// <param name="index">索引</param>
    /// <param name="size">每页数据条数</param>
    /// <param name="description">描述</param>
    /// <returns>成功的分页响应</returns>
    public static PageReply<TData> PageSucceed<TData>(List<TData> data, long total, int index, int size, string? description = null) => new() { Success = true, Description = description, Data = data, TotalCount = total, Index = index, Size = size };

    /// <summary>
    /// 构建成功的分页响应
    /// </summary>
    /// <typeparam name="TData">data类型</typeparam>
    /// <param name="data">data</param>
    /// <param name="total">总数</param>
    /// <param name="request">请求</param>
    /// <param name="description">描述</param>
    /// <returns>成功的分页响应</returns>
    public static PageReply<TData> PageSucceed<TData>(List<TData> data, long total, PageRequest request, string? description = null) => new() { Success = true, Description = description, Data = data, TotalCount = total, Index = request.Index, Size = request.Size };

    /// <summary>
    /// 构建失败的分页响应
    /// </summary>
    /// <typeparam name="TData">data类型</typeparam>
    /// <param name="description">描述</param>
    /// <returns>失败的分页响应</returns>
    public static PageReply<TData> PageFailed<TData>(string? description = null) => new() { Success = false, Description = description };
}

/// <summary>
/// 响应
/// </summary>
/// <typeparam name="TData">数据类型</typeparam>
public class Reply<TData> : Reply
{
    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}

/// <summary>
/// 分页响应
/// </summary>
/// <typeparam name="TData">数据类型</typeparam>
public class PageReply<TData> : Reply
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
}
