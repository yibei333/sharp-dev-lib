namespace SharpDevLib;

/// <summary>
/// 包含标识符和名称的复合请求对象
/// 继承自<see cref="IdRequest{TId}"/>,在标识符基础上增加了名称字段,适用于需要同时传输 ID 和名称的请求场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
public class IdNameRequest<TId> : IdRequest<TId>
{
    /// <summary>
    /// 示例化 IdNameRequest 对象
    /// </summary>
    public IdNameRequest()
    {
    }

    /// <summary>
    /// 示例化 IdNameRequest 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdNameRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 示例化 IdNameRequest 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public IdNameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameRequest 对象并初始化标识符和名称
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    public IdNameRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 名称,用于描述或标识对象
    /// </summary>
    public string? Name { get; set; }
}