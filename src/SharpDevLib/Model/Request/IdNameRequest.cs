namespace SharpDevLib;

/// <summary>
/// id name request
/// </summary>
/// <typeparam name="TId">id type</typeparam>
public class IdNameRequest<TId> : IdRequest<TId>
{
    /// <summary>
    /// 实例化id name request
    /// </summary>
    public IdNameRequest()
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    public IdNameRequest(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="name">name</param>
    public IdNameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化id name request
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="name">name</param>
    public IdNameRequest(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// name
    /// </summary>
    public string? Name { get; set; }
}