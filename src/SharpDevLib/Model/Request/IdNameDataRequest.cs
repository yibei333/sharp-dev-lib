namespace SharpDevLib;

/// <summary>
/// id name data request
/// </summary>
/// <typeparam name="TId">id type</typeparam>
/// <typeparam name="TData">data type</typeparam>
public class IdNameDataRequest<TId, TData> : IdDataRequest<TId, TData>
{
    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    public IdNameDataRequest()
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataRequest(TData? data)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(TId id, TData data) : base(id)
    {
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(string? name, TData? data)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// 实例化id name data requesst
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataRequest(TId id, string? name, TData? data) : base(id)
    {
        Name = name;
        Data = data;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}