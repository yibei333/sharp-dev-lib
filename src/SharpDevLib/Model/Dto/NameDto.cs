namespace SharpDevLib;

/// <summary>
/// name dto
/// </summary>
public class NameDto : BaseDto
{
    /// <summary>
    /// 实例化name dto
    /// </summary>
    public NameDto()
    {

    }

    /// <summary>
    /// 实例化name dto
    /// </summary>
    /// <param name="name">name</param>
    public NameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}