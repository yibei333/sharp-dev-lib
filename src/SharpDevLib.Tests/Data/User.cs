namespace SharpDevLib.Tests.Data;

public class User
{
    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string? Name { get; }
    public int Age { get; }

    public override string ToString()
    {
        return $"name={Name},age={Age}";
    }
}
