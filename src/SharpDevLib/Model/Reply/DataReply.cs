namespace SharpDevLib;

/// <summary>
/// 响应
/// </summary>
/// <typeparam name="TData">数据类型</typeparam>
public class DataReply<TData> : BaseReply
{
    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }

    /// <summary>
    /// 构建成功的响应
    /// </summary>
    /// <param name="data">data</param>
    /// <param name="description">描述</param>
    /// <returns>成功的响应</returns>
    public static DataReply<TData> Succeed(TData data, string? description = null) => new() { Success = true, Description = description, Data = data };

    /// <summary>
    /// 构建失败的响应
    /// </summary>
    /// <param name="description">描述</param>
    /// <returns>失败的响应</returns>
    public static DataReply<TData> Failed(string? description = null) => new() { Success = false, Description = description };
}