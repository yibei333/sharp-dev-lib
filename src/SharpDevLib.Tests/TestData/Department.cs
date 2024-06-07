namespace SharpDevLib.Tests.TestData;

public class Department<TId>(TId identity, string name, TId? pId = default)
{
    public TId Identity { get; set; } = identity;
    public string Name { get; set; } = name;
    public TId? PId { get; set; } = pId;
}
