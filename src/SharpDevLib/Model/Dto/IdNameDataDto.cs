namespace SharpDevLib;

/// <summary>
/// id name data dto
/// </summary>
/// <typeparam name="TId">id type</typeparam>
/// <typeparam name="TData">data type</typeparam>
public class IdNameDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    public IdNameDataDto()
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdNameDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDataDto(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdNameDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDataDto(TId id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(string name, TData? data) : base(data)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    /// <param name="data">data</param>
    public IdNameDataDto(TId id, string name, TData? data) : base(data)
    {
        Id = id;
        Name = name;
    }
    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}