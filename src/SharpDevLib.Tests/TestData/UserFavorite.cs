namespace SharpDevLib.Tests.TestData;

public class UserFavorite(string name, string favorite)
{
    public string? Name { get; set; } = name;
    public string Favorite { get; set; } = favorite;

    public override string ToString()
    {
        return $"name={Name},favorite={Favorite}";
    }
}
