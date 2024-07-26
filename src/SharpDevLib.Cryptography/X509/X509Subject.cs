namespace SharpDevLib.Cryptography;

/// <summary>
/// X509Subject
/// </summary>
public class X509Subject
{
    /// <summary>
    /// 实例化X509Subject
    /// </summary>
    /// <param name="commonName">Common Name(eg, your name or your server's hostname)</param>
    public X509Subject(string commonName)
    {
        CommonName = commonName;
    }

    /// <summary>
    /// Country Name (2 letter code)
    /// </summary>
    public string? Country { get; set; }
    /// <summary>
    /// State or Province Name(full name)
    /// </summary>
    public string? Province { get; set; }
    /// <summary>
    /// Locality Name(eg, city)
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Organization Name(eg, company)
    /// </summary>
    public string? Organization { get; set; }
    /// <summary>
    /// Organizational Unit Name(eg, section)
    /// </summary>
    public string? OrganizationalUnit { get; set; }
    /// <summary>
    /// Common Name(eg, your name or your server's hostname)
    /// </summary>
    public string CommonName { get; set; }

    /// <summary>
    /// get text
    /// </summary>
    /// <returns>text</returns>
    /// <exception cref="Exception">当所有参数都为空时引发异常</exception>
    public string Text()
    {
        var collection = new List<string>();
        if (CommonName.NotNullOrWhiteSpace()) collection.Add($"CN = {CommonName}");
        if (Country.NotNullOrWhiteSpace()) collection.Add($"C = {Country}");
        if (Province.NotNullOrWhiteSpace()) collection.Add($"ST = {Province}");
        if (City.NotNullOrWhiteSpace()) collection.Add($"L = {City}");
        if (Organization.NotNullOrWhiteSpace()) collection.Add($"O = {Organization}");
        if (OrganizationalUnit.NotNullOrWhiteSpace()) collection.Add($"OU = {OrganizationalUnit}");
        if (collection.IsNullOrEmpty()) throw new Exception($"subject info can not be empty");
        return string.Join(",", collection);
    }
}