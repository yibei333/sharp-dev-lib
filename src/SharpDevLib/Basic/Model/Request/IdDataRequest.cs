namespace SharpDevLib;

/// <summary>
/// 包含标识符和数据的复合请求对象
/// 继承自<see cref="IdRequest{TId}"/>,在标识符基础上增加了数据字段,适用于需要同时传输 ID 和数据的请求场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class IdDataRequest<TId, TData> : IdRequest<TId>
{
    /// <summary>
    /// 实例化 IdDataRequest 对象
    /// </summary>
    public IdDataRequest()
    {
    }

    /// <summary>
    /// 实例化 IdDataRequest 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public IdDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化 IdDataRequest 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化 IdDataRequest 对象并初始化标识符和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="data">要包装的数据对象</param>
    public IdDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// 数据对象
    /// </summary>
    public TData? Data { get; set; }
}