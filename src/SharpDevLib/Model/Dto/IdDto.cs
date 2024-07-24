namespace SharpDevLib;

/// <summary>
/// id dto
/// </summary>
/// <typeparam name="TId">id type</typeparam>
public class IdDto<TId> : BaseDto
{
    /// <summary>
    /// 实例化id dto
    /// </summary>
    public IdDto()
    {

    }

    /// <summary>
    /// 实例化id dto
    /// </summary>
    /// <param name="id">id</param>
    public IdDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}