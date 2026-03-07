namespace SharpDevLib;

/// <summary>
/// X.509证书主体信息，包含证书主题的各种属性
/// </summary>
/// <param name="commonName">通用名称（例如：您的姓名或服务器的主机名）</param>
public class X509Subject(string commonName)
{
    /// <summary>
    /// 获取或设置国家/地区名称（2字母代码）
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 获取或设置省/州名称（全名）
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 获取或设置城市/地区名称
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 获取或设置组织名称（例如：公司）
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// 获取或设置组织单位名称（例如：部门）
    /// </summary>
    public string? OrganizationalUnit { get; set; }

    /// <summary>
    /// 获取或设置通用名称（例如：您的姓名或服务器的主机名）
    /// </summary>
    public string CommonName { get; set; } = commonName;

    /// <summary>
    /// 将主体信息转换为文本格式
    /// </summary>
    /// <returns>主体信息的文本字符串</returns>
    /// <exception cref="Exception">当所有属性都为空时抛出异常</exception>
    public string Text()
    {
        var collection = new List<string>();
        if (CommonName.NotNullOrWhiteSpace()) collection.Add($"CN = {CommonName}");
        if (Country.NotNullOrWhiteSpace()) collection.Add($"C = {Country}");
        if (Province.NotNullOrWhiteSpace()) collection.Add($"ST = {Province}");
        if (City.NotNullOrWhiteSpace()) collection.Add($"L = {City}");
        if (Organization.NotNullOrWhiteSpace()) collection.Add($"O = {Organization}");
        if (OrganizationalUnit.NotNullOrWhiteSpace()) collection.Add($"OU = {OrganizationalUnit}");
        if (collection.IsNullOrEmpty()) throw new Exception($"主题信息不能为空");
        return string.Join(",", collection);
    }
}