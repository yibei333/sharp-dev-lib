namespace SharpDevLib;

/// <summary>
/// 包含标识符、名称和数据的复合请求对象
/// 继承自<see cref="IdDataRequest{TId,TData}"/>,在标识符和数据基础上增加了名称字段,适用于需要同时传输 ID、名称和数据的请求场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class IdNameDataRequest<TId, TData> : IdDataRequest<TId, TData>
{
    /// <summary>
    /// 示例化 IdNameDataRequest 对象
    /// </summary>
    public IdNameDataRequest()
    {
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdNameDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public IdNameDataRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化标识符和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化标识符和名称
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    public IdNameDataRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化名称和数据
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataRequest(string? name, TData? data)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 示例化 IdNameDataRequest 对象并初始化标识符、名称和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataRequest(TId id, string? name, TData? data) : base(id)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 名称,用于描述或标识数据对象
    /// </summary>
    public string? Name { get; set; }
}