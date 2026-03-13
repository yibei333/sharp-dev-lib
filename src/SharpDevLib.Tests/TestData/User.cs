namespace SharpDevLib.Tests.TestData;

public class User(string name, long age)
{
    public string? Name { get; set; } = name;
    public long Age { get; set; } = age;

    public override string ToString()
    {
        return $"name={Name},age={Age}";
    }
}
