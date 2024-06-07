namespace SharpDevLib.Tests.TestData;

public class User(string name, int age)
{
    public string? Name { get; set; } = name;
    public int Age { get; set; } = age;

    public override string ToString()
    {
        return $"name={Name},age={Age}";
    }
}
