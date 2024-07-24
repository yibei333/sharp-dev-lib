namespace SharpDevLib;

/// <summary>
/// id data dto
/// </summary>
/// <typeparam name="TId">id type</typeparam>
/// <typeparam name="TData">data type</typeparam>
public class IdDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 实例化id data dto
    /// </summary>
    public IdDataDto()
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="data">data</param>
    public IdDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 实例化id data dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="data">data</param>
    public IdDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}