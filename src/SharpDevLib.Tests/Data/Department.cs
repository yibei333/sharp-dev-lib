namespace SharpDevLib.Tests.Data;

public class Department<TId>
{
    public Department(TId identity, string name, TId? pId = default)
    {
        Identity = identity;
        Name = name;
        PId = pId;
    }

    public TId Identity { get; set; }
    public string Name { get; set; }
    public TId? PId { get; set; }
}
