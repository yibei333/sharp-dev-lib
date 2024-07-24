namespace SharpDevLib;

/// <summary>
/// id name dto
/// </summary>
/// <typeparam name="TId">id type</typeparam>
public class IdNameDto<TId> : IdDto<TId>
{
    /// <summary>
    /// 实例化id name dto
    /// </summary>
    public IdNameDto()
    {

    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    public IdNameDto(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="name">name</param>
    public IdNameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name dto
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameDto(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}