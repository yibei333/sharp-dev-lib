namespace SharpDevLib;

/// <summary>
/// id data requesst
/// </summary>
/// <typeparam name="TId">id type</typeparam>
/// <typeparam name="TData">data type</typeparam>
public class IdDataRequest<TId, TData> : IdRequest<TId>
{
    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    public IdDataRequest()
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// data
    /// </summary>
    public TData? Data { get; set; }
}