namespace SharpDevLib.Tests.Data;

public class User
{
    public User()
    {
        
    }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string? Name { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"name:{Name},age={Age}";
    }
}
