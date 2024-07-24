namespace SharpDevLib;

/// <summary>
/// id request
/// </summary>
/// <typeparam name="TId">id type</typeparam>
public class IdRequest<TId> : BaseRequest
{
    /// <summary>
    /// 实例化id request
    /// </summary>
    public IdRequest()
    {
    }

    /// <summary>
    /// 实例化id request
    /// </summary>
    /// <param name="id">id</param>
    public IdRequest(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// id
    /// </summary>
    public TId Id { get; set; } = default!;
}