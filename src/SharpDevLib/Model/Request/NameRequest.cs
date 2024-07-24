namespace SharpDevLib;

/// <summary>
/// name request
/// </summary>
public class NameRequest : BaseRequest
{
    /// <summary>
    /// 实例化name request
    /// </summary>
    public NameRequest()
    {
    }

    /// <summary>
    /// 实例化name request
    /// </summary>
    /// <param name="name">name</param>
    public NameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}